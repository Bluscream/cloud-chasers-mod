namespace StormChasers {
    [HarmonyLib.HarmonyPatch(typeof(NetworkUIMenu), nameof(NetworkUIMenu.OnReceivedRoomListUpdate))]
    static class OnReceivedRoomListUpdate {
        static bool Prefix() {
            Mod.Log("NetworkUIMenu.OnReceivedRoomListUpdate");
            DebugTweaks.ListRooms();
            return true;
        }
    }
}