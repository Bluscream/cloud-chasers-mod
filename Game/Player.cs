using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using StormTweakers;

namespace StormTweakers {
    internal partial class PlayerTweaks {
        private static readonly FieldInfo playerIsInvincible = typeof(Player).GetField("isInvincible", BindingFlags.NonPublic | BindingFlags.Instance);

        #region Player Methods
        internal void SetPlayerInvincible(bool invincible = true, Player player = null) {
            if (Mod.isOnline()) return;
            if (player is null) player = GameController.Instance.getLocalPlayer();
            playerIsInvincible.SetValue(player, invincible);
            Mod.Log($"Set isInvincible to {invincible}");
        }
        #endregion
    }
}
