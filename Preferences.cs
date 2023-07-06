﻿using MelonLoader;
using UnityEngine;

namespace StormHackers {
    internal static class Preferences {
        public static MelonPreferences_Entry<bool> EnableLogging { get; private set; }
        public static MelonPreferences_Entry<bool> ForceUnlockMouse { get; private set; }
        public static MelonPreferences_Entry<KeyCode> ToggleModPanelKey { get; private set; }
        public static MelonPreferences_Entry<KeyCode> ToggleLaptopKey { get; private set; }
        public static MelonPreferences_Entry<KeyCode> QuickJoinKey { get; private set; }
        public static MelonPreferences_Entry<KeyCode> TeleportForwardKey { get; private set; }
        public static MelonPreferences_Entry<float> TeleportForwardDistance { get; private set; }
        public static MelonPreferences_Entry<KeyCode> TeleportUpKey { get; private set; }
        public static MelonPreferences_Entry<float> TeleportUpDistance { get; private set; }
        public static MelonPreferences_Entry<KeyCode> TeleportDownKey { get; private set; }
        public static MelonPreferences_Entry<float> TeleportDownDistance { get; private set; }
        public static MelonPreferences_Entry<KeyCode> LeftIndicatorKey { get; private set; }
        public static MelonPreferences_Entry<KeyCode> RightIndicatorKey { get; private set; }

        public static void Init() {
            var category = MelonPreferences.CreateCategory(baseCategoryName);
            EnableLogging = category.CreateEntry(nameof(EnableLogging), true, is_hidden:true);
            ForceUnlockMouse = category.CreateEntry(nameof(ForceUnlockMouse), true);

            ToggleModPanelKey = category.CreateEntry(nameof(ToggleModPanelKey), KeyCode.F6);
            ToggleLaptopKey = category.CreateEntry(nameof(ToggleLaptopKey), KeyCode.M);
            QuickJoinKey = category.CreateEntry(nameof(QuickJoinKey), KeyCode.Home);

            TeleportForwardKey = category.CreateEntry(nameof(TeleportForwardKey), KeyCode.None);
            TeleportForwardDistance = category.CreateEntry(nameof(TeleportForwardDistance), 5f);
            TeleportUpKey = category.CreateEntry(nameof(TeleportUpKey), KeyCode.None);
            TeleportUpDistance = category.CreateEntry(nameof(TeleportUpDistance), 5f);
            TeleportDownKey = category.CreateEntry(nameof(TeleportDownKey), KeyCode.None);
            TeleportDownDistance = category.CreateEntry(nameof(TeleportDownDistance), 5f);

            LeftIndicatorKey = category.CreateEntry(nameof(LeftIndicatorKey), KeyCode.None);
            RightIndicatorKey = category.CreateEntry(nameof(RightIndicatorKey), KeyCode.None);

            MelonPreferences.Save();
        }

        private static readonly string baseCategoryName = "StormHackers";
    }
}