using MelonLoader;
using UnityEngine;

namespace CloudChasers {
    internal static class Preferences {
        public static MelonPreferences_Entry<bool> EnableLogging { get; private set; }
        public static MelonPreferences_Entry<bool> ForceUnlockMouse { get; private set; }
        public static MelonPreferences_Entry<bool> DisableEventSystemOverride { get; private set; }
        public static MelonPreferences_Entry<float> StartupDelay { get; private set; }
        public static MelonPreferences_Entry<bool> ToggleModPanelOnESC { get; private set; }

        public static MelonPreferences_Entry<float> PhotoScoreMultiplier { get; private set; }
        public static MelonPreferences_Entry<float> ProbeScoreMultiplier { get; private set; }

        public static MelonPreferences_Entry<KeyCode> ToggleModPanelKey { get; private set; }
        public static MelonPreferences_Entry<KeyCode> ToggleModPanelAltKey { get; private set; }
        public static MelonPreferences_Entry<KeyCode> ToggleLaptopKey { get; private set; }
        public static MelonPreferences_Entry<KeyCode> QuickJoinKey { get; private set; }
        public static MelonPreferences_Entry<KeyCode> UnlockMouseKey { get; private set; }
        public static MelonPreferences_Entry<KeyCode> TeleportForwardKey { get; private set; }
        public static MelonPreferences_Entry<float> TeleportForwardDistance { get; private set; }
        public static MelonPreferences_Entry<KeyCode> TeleportUpKey { get; private set; }
        public static MelonPreferences_Entry<float> TeleportUpDistance { get; private set; }
        public static MelonPreferences_Entry<KeyCode> TeleportDownKey { get; private set; }
        public static MelonPreferences_Entry<float> TeleportDownDistance { get; private set; }
        public static MelonPreferences_Entry<KeyCode> LeftIndicatorKey { get; private set; }
        public static MelonPreferences_Entry<KeyCode> RightIndicatorKey { get; private set; }

        public static MelonPreferences_Category TeleportLocations { get; private set; }

        public static void Init() {
            var category = MelonPreferences.CreateCategory(baseCategoryName, "Cloud Chasers");
            EnableLogging = category.CreateEntry(nameof(EnableLogging), true, "Enable Logging", "Wether to enable logging to MelonLoader's Console", is_hidden: true);
            ForceUnlockMouse = category.CreateEntry(nameof(ForceUnlockMouse), true, is_hidden: true);
            DisableEventSystemOverride = category.CreateEntry(nameof(DisableEventSystemOverride), true, is_hidden: true);
            StartupDelay = category.CreateEntry(nameof(StartupDelay), 1f, is_hidden: true);
            ToggleModPanelOnESC = category.CreateEntry(nameof(ToggleModPanelOnESC), true);

            PhotoScoreMultiplier = category.CreateEntry(nameof(PhotoScoreMultiplier), 1f, "Photo Score Multiplier");
            ProbeScoreMultiplier = category.CreateEntry(nameof(ProbeScoreMultiplier), 1f, "Probe Score Multiplier");

            ToggleModPanelKey = category.CreateEntry(nameof(ToggleModPanelKey), KeyCode.F6);
            ToggleModPanelAltKey = category.CreateEntry(nameof(ToggleModPanelAltKey), KeyCode.None);
            ToggleLaptopKey = category.CreateEntry(nameof(ToggleLaptopKey), KeyCode.M);
            QuickJoinKey = category.CreateEntry(nameof(QuickJoinKey), KeyCode.Home);
            UnlockMouseKey = category.CreateEntry(nameof(UnlockMouseKey), KeyCode.None);

            TeleportForwardKey = category.CreateEntry(nameof(TeleportForwardKey), KeyCode.None);
            TeleportForwardDistance = category.CreateEntry(nameof(TeleportForwardDistance), 5f);
            TeleportUpKey = category.CreateEntry(nameof(TeleportUpKey), KeyCode.None);
            TeleportUpDistance = category.CreateEntry(nameof(TeleportUpDistance), 5f);
            TeleportDownKey = category.CreateEntry(nameof(TeleportDownKey), KeyCode.None);
            TeleportDownDistance = category.CreateEntry(nameof(TeleportDownDistance), 5f);

            LeftIndicatorKey = category.CreateEntry(nameof(LeftIndicatorKey), KeyCode.None);
            RightIndicatorKey = category.CreateEntry(nameof(RightIndicatorKey), KeyCode.None);

            TeleportLocations = MelonPreferences.CreateCategory(tpCategoryName, "Teleport Locations");
            TeleportLocations.CreateEntry("Wakota", new Vector3(-1404.233f, 18f, 2016.92f));
            TeleportLocations.CreateEntry("Top Right", new Vector3(2069.779f, 14f, 2028.77f)); 
            TeleportLocations.CreateEntry("Middle", new Vector3(-33.8434f, 16f, 35.0814f));
            TeleportLocations.CreateEntry("Corwan", new Vector3(-1533.423f, 41f, -1838.434f));
            TeleportLocations.CreateEntry("Byron", new Vector3(2592.014f, 26f, -2448.835f));

            MelonPreferences.Save();
        }

        private static readonly string baseCategoryName = "CloudChasers";
        private static readonly string tpCategoryName = "TeleportLocations";
    }
}