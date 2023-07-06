using StormHackers;

namespace StormChasers {
    internal class DebugTweaks {
        #region Debug Methods
        internal void ListPlayers() {
            Mod.Log($"#{GameController.Instance.localPlayer.photonView.OwnerActorNr} {GameController.Instance.localPlayer.name} - {GameController.Instance.localPlayer.photonView.owner.NickName}");
            Mod.Log("Other Players:");
            foreach (var player in GameController.Instance.otherPlayers) {
                Mod.Log($"#{player.photonView.OwnerActorNr} {player.name} - {player.photonView.owner.NickName}");
            }
        }

        internal void ListRooms() {
            foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList()) {
                Mod.Log($"Room: {roomInfo.Name} ({roomInfo.PlayerCount}/{roomInfo.MaxPlayers})\n\t{roomInfo.ToStringFull()}");
            }
        }
        #endregion
    }
}
