using MelonLoader;
using UnityEngine;

namespace StormHackers {
    internal static class Preferences {
        public static MelonPreferences_Entry<bool> EnableLogging { get; private set; }
        public static MelonPreferences_Entry<KeyCode> ToggleModPanelKey { get; private set; }
        public static MelonPreferences_Entry<KeyCode> ToggleLaptopKey { get; private set; }
        public static MelonPreferences_Entry<KeyCode> QuickJoinKey { get; private set; }

        public static void Init() {
            var category = MelonPreferences.CreateCategory(baseCategoryName);
            ToggleModPanelKey = category.CreateEntry(nameof(ToggleModPanelKey), KeyCode.F6);
            ToggleLaptopKey = category.CreateEntry(nameof(ToggleLaptopKey), KeyCode.M);
            QuickJoinKey = category.CreateEntry(nameof(QuickJoinKey), KeyCode.Home);

            category = MelonPreferences.CreateCategory(debugCategoryName);
            EnableLogging = category.CreateEntry(nameof(EnableLogging), true);

            MelonPreferences.Save();
        }

        private static readonly string baseCategoryName = "StormHackers";
        private static readonly string debugCategoryName = baseCategoryName + "_Debug";
    }
}