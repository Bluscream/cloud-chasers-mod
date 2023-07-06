using MelonLoader;

namespace StormHackers {
    internal static class Preferences {
        public static MelonPreferences_Entry<bool> EnableLogging { get; private set; }

        public static void Init() {
            var category = MelonPreferences.CreateCategory(baseCategoryName);

            category = MelonPreferences.CreateCategory(debugCategoryName);
            EnableLogging = category.CreateEntry(nameof(EnableLogging), true);

            MelonPreferences.Save();
        }

        private static readonly string baseCategoryName = "StormHackers";
        private static readonly string debugCategoryName = baseCategoryName + "_Debug";
    }
}