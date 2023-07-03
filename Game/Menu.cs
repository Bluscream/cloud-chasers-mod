using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UniverseLib;

namespace StormTweakers {
    internal partial class MenuTweaks : MelonMod {
        MainUIMenu mainMenu;
        NetworkUIMenu netMenu;
        private static readonly MethodInfo joinPublicGame = typeof(NetworkUIMenu).GetMethod("joinPublicGame");

        internal void OnMainMenuLoaded() {
            Mod.Log("OnMainMenuLoaded");
            mainMenu = UnityEngine.Object.FindObjectOfType<MainUIMenu>();
            netMenu = UnityEngine.Object.FindObjectOfType<NetworkUIMenu>();
        }

        #region Menu Methods
        internal void JoinOnlineGame() {
            GlobalValues.Instance.gameType = GlobalValues.GameType.ONLINE_FREE_ROAMING;
            mainMenu.PlayOnlinePublicGame();
            joinPublicGame.Invoke(null, new object[] { });
            Mod.Log("Joined internal Game");
        }
        #endregion
    }
}
