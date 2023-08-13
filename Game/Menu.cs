using MelonLoader;
using System.Reflection;

namespace StormChasers {
    internal partial class MenuTweaks : MelonMod {
        MainUIMenu mainMenu;
        NetworkUIMenu netMenu;
        public bool allowExit = false;
        public static readonly MethodInfo joinPublicGame = typeof(NetworkUIMenu).GetMethod("joinPublicGame");

        internal void OnMainMenuLoaded() {
            Mod.Log("OnMainMenuLoaded");
            mainMenu = UnityEngine.Object.FindObjectOfType<MainUIMenu>();
            netMenu = UnityEngine.Object.FindObjectOfType<NetworkUIMenu>();
        }

        #region Menu Methods
        internal void JoinOnlineGame() {
            GlobalValues.Instance.gameType = GlobalValues.GameType.ONLINE_FREE_ROAMING;
            mainMenu.PlayOnlinePublicGame();
            //joinPublicGame.Invoke(null, new object[] { });
            Mod.Log("Joined public Game");
        }
        internal void UnlockMouse() {
            GameController.Instance.isPause = false;
            GameController.Instance.changeCursorLock(false);
        }
        #endregion
    }
}
