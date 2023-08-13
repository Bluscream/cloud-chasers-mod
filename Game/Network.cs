namespace StormChasers {
    internal partial class NetworkTweaks {
        #region Network Methods
        internal static void JoinRoom(string name, string password = null) {
            if (password != null) name += "_" + password;
            PhotonNetwork.JoinRoom(name);
        }
        internal static void CreateRoom(string name, string password = null, int maxPlayers = 32, bool isVisible = true, bool isOpen = true) => CreateRoom(name, password, new RoomOptions() {
            MaxPlayers = (byte)maxPlayers, IsVisible = isVisible, IsOpen = isOpen
        });
        internal static void CreateRoom(string name, string password = null, RoomOptions roomOptions = null) {
            if (password != null) name += "_" + password;
            PhotonNetwork.CreateRoom(name, roomOptions, null);
        }
        #endregion
    }
}