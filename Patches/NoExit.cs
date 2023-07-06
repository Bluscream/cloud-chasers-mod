namespace StormHackers {
    [HarmonyLib.HarmonyPatch(typeof(GameController), nameof(GameController.exitToMainMenu))]
    static class ExitToMainMenuPatch {
        // static Entity Postfix(Entity __result, GameObject _gameObject, string _className)
        static bool Prefix() {
            Mod.Log($"ExitToMainMenu called");
            if (Mod.menuTweaks.wantExit) {
                Mod.Log("We want exit");
                Mod.menuTweaks.wantExit = false;
                return true;
            }
            return false;
        }
    }
}
