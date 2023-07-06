using System;

namespace CloudChasers {
    [HarmonyLib.HarmonyPatch(typeof(Snapshot), nameof(StatsTweaks.getPhotoScore))]
    static class GetPhotoScorePatch {
        static void Postfix(ref int __result) {
            var result = (int)(__result * Preferences.PhotoScoreMultiplier.Value);
            Mod.Log($"getPhotoScore: {__result} * {Preferences.PhotoScoreMultiplier.Value} = {result}");
            __result = result;
        }
    }
}
