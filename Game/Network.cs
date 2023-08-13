using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StormChasers {
    internal partial class NetworkTweaks {
        #region Network Methods
        internal static string[] GetDefaultRoomNames() {
            var lst = new string[] { };
            lst.Append($"{GlobalValues.Instance.gameType.ToString()}_{Application.version}_*");
            return lst;
        }
        internal static void JoinRoom(string name, string password = null) {
            if (password != null) name += "_" + password;
            //PhotonNetwork.JoinRoom(name);
            //PhotonNetwork.player.reset();
            //PhotonNetwork.LoadLevelAsync(LoadingUI.nextScene);
            GlobalValues.Instance.onlineMode = GlobalValues.OnlineMode.PLAY_PUBLIC;
            GlobalValues.Instance.gameType = GlobalValues.GameType.ONLINE_FREE_ROAMING;
            PhotonNetwork.automaticallySyncScene = true;
            Fader.Fade("Loading", Color.black, .1f);
            // PhotonNetwork.JoinLobby();
            // GameController.Instance.GetComponent<PauseUIMenu>().;
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