namespace StormHackers {
    [HarmonyLib.HarmonyPatch(typeof(GameController), nameof(GameController.exitToMainMenu))]
    static class ExitToMainMenuPatch {
        // static Entity Postfix(Entity __result, GameObject _gameObject, string _className)
        static bool Prefix() {
            //Mod.Log($"ExitToMainMenu called");
            if (Mod.menuTweaks.allowExit) {
                //Mod.Log("We want exit");
                Mod.menuTweaks.allowExit = false;
                return true;
            }
            return false;
        }
    }
}
