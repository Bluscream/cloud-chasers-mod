using UnityEngine;
using UniverseLib.UI;
using UniverseLib.UI.Models;

namespace StormChasers {
    internal class ChatPanel : UniverseLib.UI.Panels.PanelBase {
        internal ChatPanel(UIBase owner) : base(owner) { }
        //public bool Active = false;
        public override string Name => "Send Message";
        public override int MinWidth => 100;
        public override int MinHeight => 40;
        public override Vector2 DefaultAnchorMin => new Vector2(0.25f, 0.25f);
        public override Vector2 DefaultAnchorMax => new Vector2(0.75f, 0.75f);
        public override bool CanDragAndResize => true;
        internal InputFieldRef chatMessageInput;

        protected override void ConstructPanelContent() {
            chatMessageInput = this.AddTextInput("Enter message");
            this.AddButton("Send", () => { SendMessage(chatMessageInput.Text); });
        }

        internal void SendMessage(string message, bool isChattingForAllPlayers = true) {
            GameController.Instance.photonView.RPC("receiveChat", PhotonTargets.All, new object[] { PhotonNetwork.player, message, isChattingForAllPlayers });
        }
    }
}
