using System;

namespace StormHackers {
    [HarmonyLib.HarmonyPatch(typeof(Snapshot), nameof(StatsTweaks.getPhotoScore))]
    static class GetPhotoScorePatch {
        static void Postfix(ref int __result) {
            Mod.Log($"getPhotoScore: {__result} * {Preferences.PhotoScoreMultiplier.Value}");
            __result = __result * (int)(__result * Preferences.PhotoScoreMultiplier.Value);
        }
    }
}
