using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniverseLib.UI;
using UniverseLib.UI.Models;

namespace StormChasers {
    internal class MainPanel : UniverseLib.UI.Panels.PanelBase {
        internal MainPanel(UIBase owner) : base(owner) { }
        //public bool Active = false;
        public override string Name => "Cloud Chasers";
        public override int MinWidth => 40;
        public override int MinHeight => 100;
        public override Vector2 DefaultAnchorMin => new Vector2(0.25f, 0.25f);
        public override Vector2 DefaultAnchorMax => new Vector2(0.75f, 0.75f);
        public override bool CanDragAndResize => true;
        public static Slider truckSpeedSlider;
        public static Player selectedPlayer;
        public static CarTornado selectedTruck;
        public static Dropdown truckDropdown;
        public static Dropdown playerDropdown;
        public const string DefaultDropdownText = "None / Local";

        internal void AddButton(string text, Action action) {
            ButtonRef btn = UIFactory.CreateButton(ContentRoot, null, text);
            UIFactory.SetLayoutElement(btn.Component.gameObject, minWidth: 200, minHeight: 25);
            btn.OnClick += action;
        }

        internal void AddSlider(string text, int value = 50, int minValue = 0, int maxValue = 100, UnityEngine.Events.UnityAction<int> action = null) => AddSlider(text, (float)value, (float)minValue, (float)maxValue, (float val) => action?.Invoke((int)val));
        internal void AddSlider(string text, float value = 50f, float minValue = 0f, float maxValue = 100f, UnityEngine.Events.UnityAction<float> action = null) {
            Text sliderTxt = UIFactory.CreateLabel(ContentRoot, "", text);
            UIFactory.SetLayoutElement(sliderTxt.gameObject, minWidth: 200, minHeight: 25);
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
            playerDropdown.options.Add(new Dropdown.OptionData(DefaultDropdownText));
            if (GameController.Instance.localPlayer?.photonView?.owner?.NickName != null)
                playerDropdown.options.Add(new Dropdown.OptionData(GameController.Instance.localPlayer.photonView.owner.NickName));
            foreach (var player in GameController.Instance.otherPlayers) {
                if (player?.photonView?.owner?.NickName != null)
                    playerDropdown.options.Add(new Dropdown.OptionData(player.photonView.owner.NickName));
            }
            selectedPlayer = GameController.Instance.localPlayer;
            playerDropdown.itemText.text = selectedPlayer?.photonView?.owner?.NickName ?? DefaultDropdownText;
        }

        internal CarTornado GetTruckFromDropdown() => GetPlayerFromDropdown().getInteractCar();
        internal Player GetPlayerFromDropdown() {
            var text = playerDropdown.itemText.text;
            var player = Mod.playerTweaks.GetPlayerByName(text);
            Mod.Log($"GetPlayerFromDropdown: \"{text}\" = \"{player.photonView.owner.NickName}\"");
            return player;
        }

        internal static void PopulateTrucks() {
            truckDropdown.ClearOptions();
            truckDropdown.options.Add(new Dropdown.OptionData("None / Local"));
            foreach (var player in GameController.Instance.otherPlayers) {
                var car = player.getInteractCar();
                truckDropdown.options.Add(new Dropdown.OptionData(car.photonView.owner.NickName));
            }
        }

        protected override void ConstructPanelContent() {
            //AddButton("Exit", () => { Mod.menuTweaks.allowExit = true; GameController.Instance.exitToMainMenu(); });
            //AddButton("Toggle Menu", () => { Toggle(); });
            #region Debug
            AddButton("List Players", () => { Mod.debugTweaks.ListPlayers(); });
            AddButton("List Rooms", () => { Mod.debugTweaks.ListRooms(); });
            #endregion
            AddButton("Reload", () => { PopulatePlayers(); });
            var playerDropObj = UIFactory.CreateDropdown(ContentRoot, "", out playerDropdown, DefaultDropdownText, 15, (int val) => {
                selectedPlayer = GetPlayerFromDropdown();
            });
            UIFactory.SetLayoutElement(playerDropObj, minWidth: 200, minHeight: 25);
            #region Truck
            //AddButton("Reload", () => { PopulateTrucks(); });
            //var truckDropObj = UIFactory.CreateDropdown(ContentRoot, "", out truckDropdown, DefaultDropdownText, 15, (int val) => { });
            //UIFactory.SetLayoutElement(truckDropObj, minWidth: 200, minHeight: 25);

            AddButton("Respawn Truck", () => { Mod.truckTweaks.Respawn(); });
            AddButton("Repair Truck", () => { Mod.truckTweaks.Repair(); });
            AddButton("Push Truck", () => { Mod.truckTweaks.Push(); });
            AddButton("Flip Truck", () => { Mod.truckTweaks.Flip(); });
            AddSlider("Truck Fuel", 100f, 0f, 200f, (float val) => { Mod.truckTweaks.Refuel(val, GetTruckFromDropdown()); });
            AddSlider("Truck Fuel Consumption", .5f, 0f, 2f, (float val) => { Mod.truckTweaks.SetFuelConsumption(val, GetTruckFromDropdown()); });
            AddButton("Teleport Player to Truck", () => { Mod.truckTweaks.TeleportPlayerToTruck(GetPlayerFromDropdown(), GetTruckFromDropdown()); });
            AddButton("Teleport Truck to Player", () => { Mod.truckTweaks.TeleportTruckToPlayer(GetTruckFromDropdown(), GetPlayerFromDropdown()); });
            AddButton("Enter Truck", () => { Mod.truckTweaks.Enter(truck: GetTruckFromDropdown(), player: GetPlayerFromDropdown()); });
            //AddSlider("Truck Speed", 27.78f, 0f, 9999f, (float val) => { Mod.truckTweaks.SetSpeed(val, GetTruckFromDropdown()); });

            Toggle truckControlToggle;
            var toggleObj = UIFactory.CreateToggle(ContentRoot, "", out truckControlToggle, out Text text2);
            text2.text = "Truck Control"; truckControlToggle.isOn = false;
            truckControlToggle.onValueChanged.AddListener((bool value) => { Mod.truckTweaks.SetControl(value, GetTruckFromDropdown()); });
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
            AddButton("Kill", () => { GetPlayerFromDropdown().Die(); });
            AddSlider("InactivityTime", 90f, 1f, 99999f, (float val) => { Mod.playerTweaks.SetOnlineInactivityTime(val, GetPlayerFromDropdown()); });
            Toggle invincibleToggle; Text text;
            UIFactory.CreateToggle(ContentRoot, "", out invincibleToggle, out text);
            invincibleToggle.isOn = false; text.text = "Invincible";
            invincibleToggle.onValueChanged.AddListener((bool value) => { Mod.playerTweaks.SetPlayerInvincible(value, GetPlayerFromDropdown()); });
            //AddButton("Teleport Forward", () => { Mod.playerTweaks.TeleportForward(); });
            //AddButton("Teleport Up", () => { Mod.playerTweaks.TeleportUp(); });
            //AddButton("Teleport Down", () => { Mod.playerTweaks.TeleportUp(-5); });
            AddButton("Teleport Me to Player", () => { Mod.playerTweaks.TeleportPlayerToPlayer(target: GetPlayerFromDropdown()); });
            AddButton("Teleport Player to Me", () => { Mod.playerTweaks.TeleportPlayerToPlayer(GetPlayerFromDropdown()); });
            foreach (var pos in Preferences.TeleportLocations.Entries) {
                AddButton($"Teleport to {pos.DisplayName}", () => { Mod.playerTweaks.TeleportToPos((Vector3)pos.BoxedValue, GetPlayerFromDropdown()); ; });
            }
            #endregion
            #region Stats
            AddSlider("Give Money Amount", 50, 0, 5000, (int val) => { Mod.Log($"Setting moneyAmountToGive to {val}"); PlayerTweaks.moneyAmountToGive = val; });
            AddButton("Get $1000", () => { GameController.Instance.earnMoney(1000); });
            AddButton("Get $10000", () => { GameController.Instance.earnMoney(100000); });
            AddButton("Set Level 0", () => { Mod.statsTweaks.SetXP(0); });
            AddButton("Set Level 50", () => { Mod.statsTweaks.SetLevel(50); });
            AddButton("Add 5000 xp", () => { Mod.statsTweaks.AddXP(5000); });
            AddSlider(Preferences.PhotoScoreMultiplier.DisplayName, Preferences.PhotoScoreMultiplier.Value, 0f, 15f, (float val) => { Mod.Log($"Setting {Preferences.PhotoScoreMultiplier.Identifier} to {val}"); Preferences.PhotoScoreMultiplier.Value = val; });
            AddSlider(Preferences.ProbeScoreMultiplier.DisplayName, Preferences.ProbeScoreMultiplier.Value, 0f, 15f, (float val) => { Mod.Log($"Setting {Preferences.PhotoScoreMultiplier.Identifier} to {val}"); Preferences.ProbeScoreMultiplier.Value = val; });
            #endregion
        }
    }
}
