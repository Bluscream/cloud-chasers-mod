using System.Reflection;

namespace StormChasers {
    [HarmonyLib.HarmonyPatch(typeof(CarController), nameof(checkMapSizeLimits))]
    static class checkMapSizeLimitsPatch {
        public static readonly MethodInfo checkMapSizeLimits = typeof(GameController).GetMethod("checkMapSizeLimits");
        static bool Prefix() {
            return true;
        }
    }
    [HarmonyLib.HarmonyPatch(typeof(CarTornado), nameof(checkMapSizeLimits))]
    static class checkMapSizeLimitsPatch2 {
        public static readonly MethodInfo checkMapSizeLimits = typeof(CarTornado).GetMethod("checkMapSizeLimits");
        static bool Prefix() {
            return true;
        }
    }
    [HarmonyLib.HarmonyPatch(typeof(Player), nameof(checkMapSizeLimits))]
    static class checkMapSizeLimitsPatch3 {
        public static readonly MethodInfo checkMapSizeLimits = typeof(Player).GetMethod("checkMapSizeLimits");
        static bool Prefix() {
            return true;
        }
    }
}
