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
    [HarmonyLib.HarmonyPatch(typeof(Player), nameof(Player.photoTaken))]
    static class photoTakenPatch {
        static void Postfix(ref int photoScore) {
            var result = (int)(photoScore * Preferences.PhotoScoreMultiplier.Value);
            Mod.Log($"photoTaken: {photoScore} * {Preferences.PhotoScoreMultiplier.Value} = {result}");
            photoScore = result;
        }
    }
    [HarmonyLib.HarmonyPatch(typeof(Player), nameof(Player.podDataCapture))]
    static class podDataCapturePath {
        static void Prefix(ref int dataScore) {
            var result = (int)(dataScore * Preferences.ProbeScoreMultiplier.Value);
            Mod.Log($"podDataCapture: {dataScore} * {Preferences.ProbeScoreMultiplier.Value} = {result}");
            dataScore = result;
        }
    }
}
