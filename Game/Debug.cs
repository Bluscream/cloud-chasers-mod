using CloudChasers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StormChasers {
    internal class DebugTweaks {
        public static readonly MethodInfo getOrderedPlayersList = typeof(GameController).GetMethod("getOrderedPlayersList", BindingFlags.NonPublic);
        public static readonly MethodInfo getIndexOrderedPlayersList = typeof(GameController).GetMethod("getIndexOrderedPlayersList", BindingFlags.NonPublic);
        #region Debug Methods
        internal void ListPlayers() {
            Mod.Log("Local Player:");
            Mod.Log($"#{GameController.Instance.localPlayer.photonView.OwnerActorNr} {GameController.Instance.localPlayer.name} - {GameController.Instance.localPlayer.photonView.owner.NickName}");
            Mod.Log("Other Players:");
            foreach (var player in GameController.Instance.otherPlayers)
                Mod.Log($"#{player.str()}");
            Mod.Log("Photon Players:");
            var photonPlayers = PhotonNetwork.playerList; // = (List<PhotonPlayer>)getOrderedPlayersList.Invoke(GameController.Instance, null); // new object[] { }
            foreach (var player in photonPlayers) {
                Mod.Log($"#{player.ToStringFull()}");
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
