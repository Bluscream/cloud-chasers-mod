using UnityEngine;
using UniverseLib.UI.Models;
using UniverseLib.UI;
using UnityEngine.UI;
using StormTweakers;
using System;

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

        internal void AddButton(string text, Action action) {
            ButtonRef btn = UIFactory.CreateButton(ContentRoot, null, text);
            UIFactory.SetLayoutElement(btn.Component.gameObject, minWidth: 200, minHeight: 25);
            btn.OnClick += action;
        }

        protected override void ConstructPanelContent() {
            AddButton("Toggle Menu", () => { Toggle(); });
            #region Debug
            AddButton("List Players", () => { Mod.debugTweaks.ListPlayers(); });
            AddButton("List Rooms", () => { Mod.debugTweaks.ListRooms(); });
            #endregion
            #region Truck
            AddButton("Repair Truck", () => { Mod.truckTweaks.RepairTruck(); });
            AddButton("Teleport Player to Truck", () => { Mod.truckTweaks.TeleportPlayerToTruck(); });
            AddButton("Teleport Truck to Player", () => { Mod.truckTweaks.TeleportTruckToPlayer(); });

            Text truckSpeedTXT = UIFactory.CreateLabel(ContentRoot, "", "Truck Speed");
            //UIFactory.SetLayoutElement(myText.gameObject, minWidth: 200, minHeight: 25);
            Slider truckSpeedSlider;
            var truckSpeedSliderObj = UIFactory.CreateSlider(ContentRoot, "Truck Speed", out truckSpeedSlider);
            truckSpeedSlider.minValue = 0f;
            truckSpeedSlider.maxValue = 200f;
            truckSpeedSlider.value = 100f;
            truckSpeedSlider.onValueChanged.AddListener((float value) => { Mod.truckTweaks.SetTruckSpeed(value); });
            UIFactory.SetLayoutElement(truckSpeedSliderObj, minWidth: 200, minHeight: 25);

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

            Toggle invincibleToggle;Text text;
            UIFactory.CreateToggle(ContentRoot, "", out invincibleToggle, out text);
            text.text = "Invincible";

            #endregion
        }

    }
}
