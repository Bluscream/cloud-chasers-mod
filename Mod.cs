using MelonLoader;
using System;
using System.Reflection;
using ExitGames.Demos.DemoAnimator;
using UnityEngine;
using EVP;

namespace StormTweakers {
    public partial class Mod : MelonMod {
        Type InputType;
        MethodInfo GetKeyDownMethod;
        bool fullyLoaded = false;
        MainUIMenu mainMenu;
        NetworkUIMenu netMenu;
        private static readonly FieldInfo playerIsInvincible = typeof(Player).GetField("isInvincible", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo carInForbiddenZoneMaxTime = typeof(Player).GetField("carInForbiddenZoneMaxTime", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo onlineInactivityTime = typeof(Player).GetField("onlineInactivityTime", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo vehicleController = typeof(CarTornado).GetField("vehicleController", BindingFlags.NonPublic | BindingFlags.Instance);
        //private static readonly FieldInfo leftSignalLightOn = typeof(RealisticCarController).GetField("leftSignalLightOn", BindingFlags.NonPublic | BindingFlags.Instance);
        //private static readonly FieldInfo rightSignalLightOn = typeof(RealisticCarController).GetField("rightSignalLightOn", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly MethodInfo joinPublicGame = typeof(NetworkUIMenu).GetMethod("joinPublicGame");

        public void Log(object message) {
            var msg = message.ToString();
            MelonLogger.Msg(msg);
            try { GameController.Instance.sendChatInfoText(msg); } catch { }
        }

        public override void OnInitializeMelon() {
            Log("initializing ...");
            try {
                InputType = GetInputType();
                GetKeyDownMethod = InputType.GetMethod("GetKeyDown", new[] { typeof(string) });
            } catch (Exception e) {
                MelonLogger.Error($"Error while grabbing Input Methods: {e}");
            }
            try {

            } catch (Exception e) {
                MelonLogger.Error($"Error while grabbing stuffs: {e}");
            }
            Log("initialized");
        }

        public override void OnSceneWasInitialized(int buildindex, string sceneName)
        {
            if (!fullyLoaded && sceneName == "Main Menu") {
                fullyLoaded = true;
                OnMainMenuLoaded();
            }
        }

        public void OnMainMenuLoaded() {
            Log("OnMainMenuLoaded");
            mainMenu = UnityEngine.Object.FindObjectOfType<MainUIMenu>();
            netMenu = UnityEngine.Object.FindObjectOfType<NetworkUIMenu>();
        }


        public override void OnUpdate() {
            if (GetKeyDownMethod == null) return;
            if (GetKeyDown("home")) {
                JoinOnlineGame();
            } else if (GetKeyDown("f1")) {
                GameController.Instance.updateGPSCameras();
                Log("Updated GPS Cameras");
                //GameController.Instance.respawnCar(1);
                var localPlayer = GameController.Instance.getLocalPlayer();
                SetPlayerInvincible(localPlayer);
                onlineInactivityTime.SetValue(localPlayer, 99999f);
                Log($"Set onlineInactivityTime to 99999f");
                carInForbiddenZoneMaxTime.SetValue(localPlayer, 99999f);
                Log($"Set carInForbiddenZoneMaxTime to 99999f");
                var myTruck = GameController.Instance.getLocalCar();
                RepairTruck(myTruck);
                myTruck.fuelConsomption = 0f;
                Log($"Set fuelConsomption to {myTruck.fuelConsomption}");
                var owner = GameManager.Instance.photonView.owner.NickName;
                Log($"Owner: {owner}");
            } else if (GetKeyDown("insert")) {
                TeleportTruckToPlayer();
            } else if (GetKeyDown("delete")) {
                TeleportPlayerToTruck();
            } else if (GetKeyDown("f18")) {
                SetTruckSpeed(1000f);
            } else if (GetKeyDown("f8")) {
                ToggleTruckControl();
            } else if (GetKeyDown("f9")) {
                ListRooms();
            } else if (GetKeyDown("f10")) {
                ListPlayers();
            }
            //else if (GetKeyDown("leftarrow")) {
            //    var myTruck = GameController.Instance.getLocalCar();
            //    var oldValue = (bool)leftSignalLightOn.GetValue(myTruck);
            //    leftSignalLightOn.SetValue(myTruck, !oldValue);
            //} else if (GetKeyDown("rightarrow")) {
            //    var myTruck = GameController.Instance.getLocalCar();
            //    var oldValue = (bool)rightSignalLightOn.GetValue(myTruck);
            //    rightSignalLightOn.SetValue(myTruck, !oldValue);
            //}
        }
        #region Menu Methods
        public void JoinOnlineGame() {
            GlobalValues.Instance.gameType = GlobalValues.GameType.ONLINE_FREE_ROAMING;
            mainMenu.PlayOnlinePublicGame();
            joinPublicGame.Invoke(null, new object[] { });
            Log("Joined Public Game");
        }
        #endregion
        #region Player Methods

        public void SetPlayerInvincible(bool invincible = true, Player player = null) {
            if (player is null) player = GameController.Instance.getLocalPlayer();
            playerIsInvincible.SetValue(player, invincible);
            Log($"Set isInvincible to {invincible}");
        }
        #endregion
        #region Truck Methods
        public void RepairTruck(CarTornado truck = null) {
            if (truck is null) truck = GameController.Instance.getLocalCar();
            truck.repairCar();
            Log($"Repaired truck");
            truck.fuel = 150f;
            Log($"Set fuel to {truck.fuel}");
        }

        public void SetTruckSpeed(float speed = 100f, CarTornado truck = null) {
            if (GlobalValues.Instance.gameType != GlobalValues.GameType.SINGLEPLAYER_FREE_ROAMING) {
                Log("Changing truck speed in multiplayer will get you banned"); return;
            }
            if (truck is null) truck = GameController.Instance.getLocalCar();
            var controller = (VehicleController)vehicleController.GetValue(truck);
            controller.maxSpeedForward = speed;
            controller.maxDriveForce = speed;
            Log($"Set truck speed to {speed}");
        }

        public void ToggleTruckControl(CarTornado truck = null) {
            if (GlobalValues.Instance.gameType != GlobalValues.GameType.SINGLEPLAYER_FREE_ROAMING) {
                Log("Remote control in multiplayer will get you banned"); return;
            }
            if (truck is null) truck = GameController.Instance.getLocalCar();
            var truckInput = truck.GetComponent<VehicleInput>();
            truckInput.enabled = !truckInput.enabled;
            var en = truckInput.enabled ? "Enabled" : "Disabled";
            Log($"{en} Truck Remote Control");
        }

        public void TeleportPlayerToTruck(Player player = null, CarTornado truck = null) {
            if (player is null) player = GameController.Instance.getLocalPlayer();
            if (truck is null) truck = GameController.Instance.getLocalCar();
            player.transform.position = truck.transform.position + truck.transform.forward * 10f;
            Log($"Teleported {player.photonView.owner.NickName} to {truck.name}");
        }

        public void TeleportTruckToPlayer(CarTornado truck = null, Player player = null) {
            if (player is null) player = GameController.Instance.getLocalPlayer();
            if (truck is null) truck = GameController.Instance.getLocalCar();
            truck.transform.position = player.transform.position + player.transform.forward * 10f;
            Log($"Teleported {truck.name} to {player.photonView.owner.NickName}");
        }

        public void SetTruckLicensePlate(string front = null, string back = null, CarTornado truck = null) {
            if (truck is null) truck = GameController.Instance.getLocalCar();
            if (front != null) truck.carLicensePlates[0].text = front;
            if (back != null) truck.carLicensePlates[1].text = back;
            Log($"Set license plate of {truck.name} to {front} {back}");
        }
        #endregion
        #region Debug Methods
        private void ListPlayers() {
            Log("Other Players:");
            foreach (var player in GameController.Instance.otherPlayers) {
                Log($"#{player.photonView.OwnerActorNr} {player.name} - {player.photonView.owner.NickName}");
            }
        }

        private void ListRooms() {
            foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList()) {
                Log($"Room: {roomInfo.Name} ({roomInfo.PlayerCount}/{roomInfo.MaxPlayers})\n\t{roomInfo.ToStringFull()}");
            }
        }
        #endregion
        private float GetCommandLineArgument(string name) {
            string[] args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++) {
                if (args[i] == $"-{name}") {
                    return float.Parse(args[i + 1]);
                }
            }
            return 1;
        }

        private Type GetUnityType(string typeName, string moduleName = "CoreModule") {
            return Type.GetType($"UnityEngine.{typeName}, UnityEngine.{moduleName}, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
        }

        private Type GetInputType() {
            return GetUnityType("Input") ?? GetUnityType("Input", "InputLegacyModule");
        }

        private object GetValue(Type type, string propertyName, object instance = null) {
            var property = type.GetProperty(propertyName);
            if (property != null) {
                return property.GetValue(instance, null);
            }
            return type.GetField(propertyName).GetValue(instance);
        }

        private void SetValue(Type type, string propertyName, object value, object instance = null) {
            var property = type.GetProperty(propertyName);
            type.GetProperty(propertyName).SetValue(instance, value, null);
        }

        private bool GetKeyDown(string key) {
            return (bool)GetKeyDownMethod.Invoke(null, new[] { key });
        }
    }
}
