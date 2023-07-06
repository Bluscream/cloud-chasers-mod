using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using StormHackers;
using StormChasers;
using UnityEngine;

namespace StormHackers {
    internal partial class PlayerTweaks {
        private static readonly FieldInfo isInvincible = typeof(Player).GetField("isInvincible", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo onlineInactivityTime = typeof(Player).GetField("onlineInactivityTime", BindingFlags.NonPublic | BindingFlags.Instance);

        #region Player Methods
        internal Player GetPlayerByName(string name) {
            name = name.Trim();
            if (string.IsNullOrWhiteSpace(name)) return null;
            if (name == MainPanel.DefaultDropdownText) return GameController.Instance.getLocalPlayer();
            return GameController.Instance.otherPlayers.FirstOrDefault(p => p.photonView.owner.NickName.Trim() == name);
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
            player.transform.position = pos;
            Mod.Log($"Teleported {player.photonView.owner.NickName} to {pos}");
        }
        #endregion
    }
}
