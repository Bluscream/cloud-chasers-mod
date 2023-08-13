using System;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;
using UniverseLib.UI;
using UniverseLib.UI.Models;

namespace StormChasers {
    internal class RoomPanel : UniverseLib.UI.Panels.PanelBase {
        internal RoomPanel(UIBase owner) : base(owner) { }
        //public bool Active = false;
        public override string Name => "Room";
        public override int MinWidth => 40;
        public override int MinHeight => 100;
        public override Vector2 DefaultAnchorMin => new Vector2(0.25f, 0.25f);
        public override Vector2 DefaultAnchorMax => new Vector2(0.75f, 0.75f);
        public override bool CanDragAndResize => true;
        public const string DefaultDropdownText = "None / Local";

        internal static Dropdown roomDropdown;
        internal static InputFieldRef roomNameField;
        internal static InputFieldRef roomPasswordField;
        internal static Slider maxPlayersSlider;
        internal static Toggle isVisibleToggle;
        internal static Toggle isOpenToggle;

        protected override void ConstructPanelContent() {
            roomDropdown = this.AddDropdown(DefaultDropdownText);
            this.AddButton("Reload", () => { roomDropdown.PopulatePhotonRooms(); });
            roomNameField = this.AddTextInput("Room Name");
            roomPasswordField = this.AddTextInput("Room Password");
            this.AddButton("Join Room", () => {
                if (!string.IsNullOrEmpty(roomDropdown.itemText.text) && roomDropdown.itemText.text != DefaultDropdownText) {
                    NetworkTweaks.JoinRoom(roomDropdown.itemText.text, roomPasswordField.Text);
                } else {
                    NetworkTweaks.JoinRoom(roomNameField.Text, roomPasswordField.Text);
                }
            });
            maxPlayersSlider = this.AddSlider("Max Players", 32, 1, 50);
            isVisibleToggle = this.AddToggle("Visible", true);
            isOpenToggle = this.AddToggle("Open", true);
            this.AddButton("Create Room", () => { NetworkTweaks.CreateRoom(roomNameField.Text, roomPasswordField.Text, (int)maxPlayersSlider.value, isVisibleToggle.enabled, isOpenToggle.enabled); });
            roomDropdown.PopulatePhotonRooms();
        }
    }
}
