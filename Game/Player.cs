using System.Linq;
using System.Reflection;
using StormChasers;
using UnityEngine;

namespace StormChasers {
    internal partial class PlayerTweaks {
        private static readonly FieldInfo isInvincible = typeof(Player).GetField("isInvincible", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo onlineInactivityTime = typeof(Player).GetField("onlineInactivityTime", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo carInForbiddenZoneMaxTime = typeof(Player).GetField("carInForbiddenZoneMaxTime", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo _moneyAmountToGive = typeof(Player).GetField("moneyAmountToGive", BindingFlags.NonPublic | BindingFlags.Instance);

        public static int moneyAmountToGive {
            get {
                return (int)_moneyAmountToGive.GetValue(GameController.Instance.getLocalPlayer());
        } set {
                _moneyAmountToGive.SetValue(GameController.Instance.getLocalPlayer(), value);
            }
        }

        #region Player Methods
        internal Player GetPlayerByName(string name) {
            Player player;
            name = name.Trim();
            if (string.IsNullOrWhiteSpace(name)) return null;
            if (name == MainPanel.DefaultDropdownText) player = GameController.Instance.getLocalPlayer();
            else player = GameController.Instance.otherPlayers.FirstOrDefault(p => p.photonView.owner.NickName.Trim() == name);
            //Mod.Log($"GetPlayerByName: {player.str()}");
            return player;
        }
        internal void SetPlayerInvincible(bool invincible = true, Player player = null) {
            if (Mod.isOnline()) return;
            if (player is null) player = GameController.Instance.getLocalPlayer();
            isInvincible.SetValue(player, invincible);
            Mod.Log($"Set {player.photonView.owner.NickName}'s isInvincible to {(bool)isInvincible.GetValue(player)}");
        }
        internal void SetOnlineInactivityTime(float time = 90f, Player player = null) {
            if (Mod.isOnline()) return;
            if (player is null) player = GameController.Instance.getLocalPlayer();
            onlineInactivityTime.SetValue(player, time);
            Mod.Log($"Set {player.photonView.owner.NickName}'s onlineInactivityTime to {(float)onlineInactivityTime.GetValue(player)}");
        }
        internal void TeleportPlayerToPlayer(Player source = null, Player target = null) {
            if (Mod.isOnline()) return;
            if (source is null) source = GameController.Instance.getLocalPlayer();
            if (target is null) target = GameController.Instance.getLocalPlayer();
            source.transform.position = target.transform.position + target.transform.forward / 5f;
            Mod.Log($"Teleported {source.photonView.owner.NickName} to {target.photonView.owner.NickName}");
        }
        internal void TeleportToPos(Vector3 pos, Player player = null) {
            if (Mod.isOnline()) return;
            if (player is null) player = GameController.Instance.getLocalPlayer();
            if (player.isInsideCar) { Mod.truckTweaks.TeleportToPos(pos); return; }
            player.transform.position = pos;
            Mod.Log($"Teleported {player.photonView.owner.NickName} to {pos}");
        }
        internal void TeleportForward(float distance = 5f, Player player = null) {
            if (Mod.isOnline()) return;
            if (player is null) player = GameController.Instance.getLocalPlayer();
            if (player.isInsideCar) { Mod.truckTweaks.TeleportForward(distance); return; }
            player.transform.position += player.transform.forward * distance;
            Mod.Log($"Teleported {player.photonView.owner.NickName} forward {distance} units");
        }
        internal void TeleportUp(float distance = 5f, Player player = null) {
            if (Mod.isOnline()) return;
            if (player is null) player = GameController.Instance.getLocalPlayer();
            if (player.isInsideCar) { Mod.truckTweaks.TeleportUp(distance); return; }
            player.transform.position += player.transform.up * distance;
            Mod.Log($"Teleported {player.photonView.owner.NickName} up {distance} units");
        }
        internal void UnlockDoor(bool _lock = false, Player player = null) {
            player.interactDoorZone.isLocked = _lock;
        }
        #endregion
      }
}
