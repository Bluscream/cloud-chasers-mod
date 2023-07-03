using UnityEngine;
using UniverseLib.UI.Models;
using UniverseLib.UI;
using UnityEngine.UI;
using StormTweakers;
using System;
using System.Linq;
using UniverseLib.Utility;

namespace StormChasers {
    internal class MainPanel : UniverseLib.UI.Panels.PanelBase {
        internal MainPanel(UIBase owner) : base(owner) { }
        //public bool Active = false;
        public override string Name => "Storm Tweakers";
        public override int MinWidth => 50;
        public override int MinHeight => 100;
        public override Vector2 DefaultAnchorMin => new Vector2(0.25f, 0.25f);
        public override Vector2 DefaultAnchorMax => new Vector2(0.75f, 0.75f);
        public override bool CanDragAndResize => true;
        public static Slider truckSpeedSlider;
        public static Dropdown truckDropdown;
        public static Dropdown playerDropdown;

        internal void AddButton(string text, Action action) {
            ButtonRef btn = UIFactory.CreateButton(ContentRoot, null, text);
            UIFactory.SetLayoutElement(btn.Component.gameObject, minWidth: 200, minHeight: 25);
            btn.OnClick += action;
        }

        internal void AddSlider(string text, float value = 50f, float minValue = 0f, float maxValue = 100f, UnityEngine.Events.UnityAction<float> action = null) {
            Text sliderTxt = UIFactory.CreateLabel(ContentRoot, "", text);
            //UIFactory.SetLayoutElement(myText.gameObject, minWidth: 200, minHeight: 25);
            var sliderObj = UIFactory.CreateSlider(ContentRoot, "", out truckSpeedSlider);
            truckSpeedSlider.minValue = minValue;
            truckSpeedSlider.maxValue = maxValue;
            truckSpeedSlider.value = value;
            truckSpeedSlider.onValueChanged.AddListener(action);
            UIFactory.SetLayoutElement(sliderObj, minWidth: 200, minHeight: 25);
        }

        internal void PopulateDropdown(Dropdown dropdown, string[] options, int defaultIndex = 0) {
            dropdown.ClearOptions();
            dropdown.AddOptions(options.ToList());
            dropdown.value = defaultIndex;
        }

        internal void AddToDropdown(Dropdown dropdown, string option) {
            dropdown.options.Add(new Dropdown.OptionData(option));
        }

        internal static void PopulatePlayers() {
            playerDropdown.ClearOptions();
            foreach (var player in GameController.Instance.otherPlayers) {
                playerDropdown.options.Add(new Dropdown.OptionData(player.photonView.owner.NickName));
            }
        }

        internal static void PopulateTrucks() {
            truckDropdown.ClearOptions();
            foreach (var player in GameController.Instance.otherPlayers) {
                var car = player.getInteractCar();
                truckDropdown.options.Add(new Dropdown.OptionData(car.photonView.owner.NickName));
            }
        }

        protected override void ConstructPanelContent() {
            AddButton("Toggle Menu", () => { Toggle(); });
            #region Debug
            AddButton("List Players", () => { Mod.debugTweaks.ListPlayers(); });
            AddButton("List Rooms", () => { Mod.debugTweaks.ListRooms(); });
            #endregion
            #region Truck
//CreateDropdown(GameObject parent, string name, out Dropdown dropdown, string defaultItemText, int itemFontSize, Action<int> onValueChanged, string[] defaultOptions = null) {
            var truckDropObj = UIFactory.CreateDropdown(ContentRoot, "", out truckDropdown, "None / Local", 15, (int val) => { });
            UIFactory.SetLayoutElement(truckDropObj, minWidth: 200, minHeight: 25);

            AddButton("Repair Truck", () => { Mod.truckTweaks.RepairTruck(); });
            AddSlider("Truck Fuel", 100f, 0f, 200f, (float val) => { Mod.truckTweaks.RefuelTruck(val); });
            AddSlider("Truck Fuel Consumption", .5f, 0f, 2f, (float val) => { Mod.truckTweaks.SetFuelConsumption(val); });
            AddButton("Teleport Player to Truck", () => { Mod.truckTweaks.TeleportPlayerToTruck(); });
            AddButton("Teleport Truck to Player", () => { Mod.truckTweaks.TeleportTruckToPlayer(); });

            AddSlider("Truck Speed", 27.78f, 0f, 9999f, (float val) => { Mod.truckTweaks.SetTruckSpeed(val); });

            Toggle truckControlToggle;
            var toggleObj = UIFactory.CreateToggle(ContentRoot, "", out truckControlToggle, out Text text2);
            text2.text = "Truck Control";
            truckControlToggle.onValueChanged.AddListener((bool value) => { Mod.truckTweaks.SetTruckControl(value); });
            //UIFactory.SetLayoutElement(toggleObj, minWidth: 200, minHeight: 25);

            //var plates = GameController.Instance.getLocalCar()?.carLicensePlates ?? new Text[] { "","" };
            //var platefieldfront = UIFactory.CreateInputField(ContentRoot, "Front License Plate","");
            //UIFactory.SetLayoutElement(platefieldfront.Component.gameObject, minWidth: 200, minHeight: 25);
            //var platefieldback = UIFactory.CreateInputField(ContentRoot, "Back License Plate","");
            //UIFactory.SetLayoutElement(platefieldback.Component.gameObject, minWidth: 200, minHeight: 25);
            //AddButton("Set License Plates", () => {
            //    plates = GameController.Instance.getLocalCar().carLicensePlates;
            //    plates[0].text = platefieldfront.Text;
            //    plates[1].text = platefieldback.Text;
            //    GameController.Instance.getLocalCar().carLicensePlates = plates;
            //});
            #endregion
            #region Player
            var playerDropObj = UIFactory.CreateDropdown(ContentRoot, "", out playerDropdown, "None / Local", 15, (int val) => { });
            UIFactory.SetLayoutElement(playerDropObj, minWidth: 200, minHeight: 25);
            Toggle invincibleToggle;Text text;
            UIFactory.CreateToggle(ContentRoot, "", out invincibleToggle, out text);
            text.text = "Invincible";

            #endregion
        }

    }
}
