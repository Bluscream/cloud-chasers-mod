using EVP;
using MelonLoader;
using StormChasers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StormTweakers {
    internal partial class TruckTweaks : MelonMod {
        //private static readonly FieldInfo carInForbiddenZoneMaxTime = typeof(Player).GetField("carInForbiddenZoneMaxTime", BindingFlags.NonPublic | BindingFlags.Instance);
        //private static readonly FieldInfo onlineInactivityTime = typeof(Player).GetField("onlineInactivityTime", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo vehicleController = typeof(CarTornado).GetField("vehicleController", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo onlineSpeed = typeof(CarTornado).GetField("onlineSpeed", BindingFlags.NonPublic | BindingFlags.Instance);
        //private static readonly FieldInfo leftSignalLightOn = typeof(RealisticCarController).GetField("leftSignalLightOn", BindingFlags.NonPublic | BindingFlags.Instance);
        //private static readonly FieldInfo rightSignalLightOn = typeof(RealisticCarController).GetField("rightSignalLightOn", BindingFlags.NonPublic | BindingFlags.Instance);

        #region Truck Methods
        internal void RepairTruck(CarTornado truck = null) {
            if (Mod.isOnline()) return;
            if (truck is null) truck = GameController.Instance.getLocalCar();
            truck.repairCar();
            Mod.Log($"Repaired truck");
        }
        internal void RefuelTruck(float fuel = 100f, CarTornado truck = null) {
            if (Mod.isOnline()) return;
            if (truck is null) truck = GameController.Instance.getLocalCar();
            Mod.Log($"Set fuel from {truck.fuel} to {fuel}");
            truck.fuel = fuel;
        }

        internal void SetFuelConsumption(float consumption = .5f, CarTornado truck = null) {
            if (Mod.isOnline()) return;
            if (truck is null) truck = GameController.Instance.getLocalCar();
            Mod.Log($"Set truck consumption from {truck.fuelConsomption} to {consumption}");
            truck.fuelConsomption = consumption;
        }

        internal void SetTruckSpeed(float speed = 27.78f, CarTornado truck = null) {
            if (Mod.isOnline()) return;
            if (truck is null) truck = GameController.Instance.getLocalCar();
            var controller = (VehicleController)vehicleController.GetValue(truck);
            Mod.Log($"Set truck speed from {controller.maxSpeedForward} to {speed}");
            onlineSpeed.SetValue(truck, speed);
            controller.maxSpeedForward = speed;
            controller.maxSpeedReverse = speed / 2f;
            controller.speed = speed;
            Mod.Log($"controller.maxDriveForce = {controller.maxDriveForce}");
            controller.maxDriveForce = 999999f;
        }

        internal void SetTruckControl(bool? enabled = null, CarTornado truck = null) {
            if (Mod.isOnline()) return;
            if (truck is null) truck = GameController.Instance.getLocalCar();
            var truckInput = truck.GetComponent<VehicleInput>();
            if (enabled is null) truckInput.enabled = !truckInput.enabled;
            else truckInput.enabled = (bool)enabled;
            var en = truckInput.enabled ? "Enabled" : "Disabled";
            Mod.Log($"{en} Truck Remote Control");
        }

        internal void TeleportPlayerToTruck(Player player = null, CarTornado truck = null) {
            if (Mod.isOnline()) return;
            if (player is null) player = GameController.Instance.getLocalPlayer();
            if (truck is null) truck = GameController.Instance.getLocalCar();
            player.transform.position = truck.transform.position + truck.transform.forward * 10f;
            Mod.Log($"Teleported {player.photonView.owner.NickName} to {truck.name}");
        }

        internal void TeleportTruckToPlayer(CarTornado truck = null, Player player = null) {
            if (Mod.isOnline()) return;
            if (player is null) player = GameController.Instance.getLocalPlayer();
            if (truck is null) truck = GameController.Instance.getLocalCar();
            truck.transform.position = player.transform.position + player.transform.forward * 10f;
            Mod.Log($"Teleported {truck.name} to {player.photonView.owner.NickName}");
            if (truck is null) truck = GameController.Instance.getLocalCar();
            var controller = (VehicleController)vehicleController.GetValue(truck);
            if (MainPanel.truckSpeedSlider != null) MainPanel.truckSpeedSlider.value = controller.maxSpeedForward;
            Mod.Log("Max speed: " + controller.maxSpeedForward);
        }

        internal void SetTruckLicensePlate(string front = null, string back = null, CarTornado truck = null) {
            if (Mod.isOnline()) return;
            if (truck is null) truck = GameController.Instance.getLocalCar();
            if (front != null) truck.carLicensePlates[0].text = front;
            if (back != null) truck.carLicensePlates[1].text = back;
            Mod.Log($"Set license plate of {truck.name} to {front} {back}");
        }
        #endregion
    }
}
