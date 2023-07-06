using MelonLoader;
using System.Reflection;
using static CarTornado;
using static PlayerBody;

namespace CloudChasers {
    internal partial class StatsTweaks {
        private static readonly MethodInfo SetUserData = typeof(OnlineLevel).GetMethod("SetUserData", BindingFlags.Static | BindingFlags.NonPublic);
        private static readonly MethodInfo addXP = typeof(OnlineLevel).GetMethod("addXP", BindingFlags.Static | BindingFlags.NonPublic);
        internal static readonly MethodInfo getPhotoScore = typeof(Snapshot).GetMethod("getPhotoScore", BindingFlags.NonPublic);

        #region Stats Methods
        internal void AddXP(int xp = 1) {
            
            Mod.Log($"Adding {xp} XP");
            addXP.Invoke(null, new object[] { xp });
        }
        internal void SetXP(int xp = 1) {
            Mod.Log($"Setting {xp} XP");
            SetUserData.Invoke(null, new object[] { xp });
        }
        internal void SetLevel(int level) {
            var requiredXP = OnlineLevel.getXPNeededForLevel(level);
            Mod.Log($"{requiredXP} XP required for Level {level}");
            SetXP(requiredXP);
        }
        internal void SetUserPrefs(
        Gender gender, SkinColor skinColor, int topCloth, int bottomCloth, int shoes, VehicleModel vehicleModel, VehicleType vehicleType, VehicleColor vehicleColor,
        VehicleRust vehicleRust, VehicleReflection vehicleReflection) {
            OnlineLevel.SetUserPrefs((int)gender, (int)skinColor, topCloth, bottomCloth, shoes, (int)vehicleType, (int)vehicleColor, (int)vehicleRust, (int)vehicleReflection);
        }
        #endregion
    }
}
