using System.Reflection;
using UnityEngine;

namespace StormChasers {
    [HarmonyLib.HarmonyPatch(typeof(MainUIMenu), nameof(MainUIMenu.SetInitialInfoText))]
    static class customWelcomeText {
        [Obfuscation(Exclude = true)]
        static void Postfix() {
            UnityEngine.UI.Text info = Object.FindObjectOfType<MainUIMenu>().PlayfabInfoText;
            info.supportRichText = true;
            info.text = $"<color=#ffff00>{Mod.ModInfo.Name} Mod Menu</color> - <color=#7d7d7d>version {Mod.ModInfo.Version} </color>\n\nThank you for using my mod! If you have any issues, please contact me on Discord @ <color=#850aff>{Mod.ModInfo.Author}</color>";
            if (Mod.isOnline()) {
                Object.FindObjectOfType<MainUIMenu>().OnlineRoomsButtons.transform.Find("PublicOnlineButton").GetComponent<UnityEngine.UI.Button>().interactable = false;
                Object.FindObjectOfType<MainUIMenu>().OnlineRoomsButtons.transform.Find("JoinPrivateButton").GetComponent<UnityEngine.UI.Button>().interactable = false;
            }
        }
    }
    [HarmonyLib.HarmonyPatch(typeof(GameController), nameof(GameController.exitToMainMenu))]
    static class ExitToMainMenuPatch {
        static bool Prefix() {
            if (Mod.menuTweaks.allowExit) {
                Mod.menuTweaks.allowExit = false;
                return true;
            }
            return false;
        }
    }
    [HarmonyLib.HarmonyPatch(typeof(PauseUIMenu), nameof(PauseUIMenu.ExitGame))]
    static class ExitGamePatch {
        static void Prefix() {
            Mod.menuTweaks.allowExit = true;
        }
    }
    [HarmonyLib.HarmonyPatch(typeof(GameController), nameof(GameController.togglePause))]
    static class PauseMenuPatch {
        static void Prefix(GameController __instance) {
            if (Preferences.ToggleModPanelOnESC.Value) {
                Mod.mainPanel.SetActive(!__instance.isPause);
                Mod.chatPanel.SetActive(!__instance.isPause);
                //Mod.roomPanel.SetActive(!__instance.isPause);
            }
        }
    }
}
