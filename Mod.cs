﻿using MelonLoader;
using System;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UniverseLib;
using UniverseLib.Input;
using UniverseLib.UI;

namespace StormChasers {

    internal partial class Mod : MelonMod {
        internal static AssemblyName FileInfo = typeof(Mod).Assembly.GetName();
        internal static MelonInfoAttribute ModInfo = typeof(Mod).Assembly.GetCustomAttribute<MelonInfoAttribute>();
        internal static MainPanel mainPanel;
        internal static ChatPanel chatPanel;
        internal static RoomPanel roomPanel;
        internal bool fullyLoaded = false;
        internal static MenuTweaks menuTweaks = new MenuTweaks();
        internal static PlayerTweaks playerTweaks = new PlayerTweaks();
        internal static TruckTweaks truckTweaks = new TruckTweaks();
        internal static DebugTweaks debugTweaks = new DebugTweaks();
        internal static MapTweaks mapTweaks = new MapTweaks();
        internal static StatsTweaks statsTweaks = new StatsTweaks();
        internal static NetworkTweaks networkTweaks = new NetworkTweaks();

        internal static void Log(object message, LogType type = LogType.Log) {
            var msg = message.ToString();
            MelonLogger.Msg(msg);
            // try { GameController.Instance.getLocalPlayer().think(msg); } catch { }
        }

        public override void OnInitializeMelon() {
            Preferences.Init();
        }

        public override void OnSceneWasInitialized(int buildindex, string sceneName) {
            if (!fullyLoaded && sceneName == "Main Menu") {
                fullyLoaded = true;
                OnMainMenuLoaded();
            } else if (sceneName == "Game") {
                MainPanel.PopulatePlayers();
            }
        }

        internal void OnMainMenuLoaded() {
            menuTweaks.OnMainMenuLoaded();
            Universe.Init(Preferences.StartupDelay.Value, OnUniverseLibInitialized, Log, new UniverseLib.Config.UniverseLibConfig() { Force_Unlock_Mouse = Preferences.ForceUnlockMouse.Value, Disable_EventSystem_Override = Preferences.DisableEventSystemOverride.Value });
        }

        internal void OnUniverseLibInitialized() {
            Log("OnUniverseLibInitialized");
            UIBase panel = UniversalUI.RegisterUI("bluscream.stormtweakers", () => { });
            mainPanel = new MainPanel(panel) { Enabled = false };
            chatPanel = new ChatPanel(panel) { Enabled = false };
            roomPanel = new RoomPanel(panel) { Enabled = false };
        }

        public override void OnLateUpdate() {
            if (Preferences.ToggleModPanelKey is null || InputManager.CurrentType == InputType.None) return;
            if (InputManager.GetKeyDown(Preferences.ToggleModPanelKey.Value) || InputManager.GetKeyDown(Preferences.ToggleModPanelAltKey.Value)) {
                try { mainPanel.Toggle(); } catch { mainPanel.SetActive(!mainPanel.Enabled); }
            } else if (InputManager.GetKeyDown(Preferences.QuickJoinKey.Value)) {
                menuTweaks.JoinOnlineGame();
            } else if (InputManager.GetKeyDown(Preferences.UnlockMouseKey.Value)) {
                menuTweaks.UnlockMouse();
            } else if (InputManager.GetKeyDown(Preferences.ToggleLaptopKey.Value)) {
                GameController.Instance.toggleLaptopMenu();
            } else if (InputManager.GetKeyDown(Preferences.TeleportForwardKey.Value)) {
                playerTweaks.TeleportForward(Preferences.TeleportForwardDistance.Value);
            } else if (InputManager.GetKeyDown(Preferences.TeleportUpKey.Value)) {
                playerTweaks.TeleportUp(Preferences.TeleportUpDistance.Value);
            } else if (InputManager.GetKeyDown(Preferences.TeleportDownKey.Value)) {
                playerTweaks.TeleportUp(-Preferences.TeleportDownDistance.Value);
            } else if (InputManager.GetKeyDown(Preferences.LeftIndicatorKey.Value)) {
                TruckTweaks.leftIndicatorOn = !TruckTweaks.leftIndicatorOn;
            } else if (InputManager.GetKeyDown(Preferences.RightIndicatorKey.Value)) {
                TruckTweaks.rightIndicatorOn = !TruckTweaks.rightIndicatorOn;
            } else if (InputManager.GetKeyDown(Preferences.ToggleChatPanelKey.Value)) {
                chatPanel.Toggle();
            } else if (InputManager.GetKeyDown(Preferences.ToggleRoomPanelKey.Value)) {
                roomPanel.Toggle();
            } else if (InputManager.GetKeyDown(Preferences.ExitToMainMenuKey.Value)) {
                Log("Exiting to main menu ...");
                PhotonNetwork.LeaveRoom(true);
                Fader.Fade("Main Menu", Color.black, .1f);
            } else if (InputManager.GetKeyDown(Preferences.ExitGameKey.Value)) {
                Log("Exiting game ...");
                try { Application.Quit(); } catch { try { Environment.Exit(0); } catch { Process.GetCurrentProcess().Kill(); } }
            } else if (InputManager.GetKeyDown(Preferences.RestartGameKey.Value)) {
                Log("Restarting game ...");
                Process.Start("steam://run/862800"); // , Environment.CommandLine.ToString());
                try { Application.Quit(); } catch { try { Environment.Exit(0); } catch { Process.GetCurrentProcess().Kill(); } }
            } /* else if (InputManager.GetKeyDown(Preferences.TogglePlayerPanelKey.Value)) {
            } else if (InputManager.GetKeyDown(Preferences.ToggleTruckPanelKey.Value)) {
            } else if (InputManager.GetKeyDown(Preferences.ToggleStormPanelKey.Value)) {
            } else if (InputManager.GetKeyDown(Preferences.ToggleTeleportPanelKey.Value)) {
            }*/
        }

        internal static bool isOnline() {
            return false;
            if (GlobalValues.Instance.gameType != GlobalValues.GameType.SINGLEPLAYER_FREE_ROAMING &&
            GlobalValues.Instance.gameType != GlobalValues.GameType.SINGLEPLAYER_MISSION) {
                Log("Disabled in multiplayer ಠ_ಠ");
                return true;
            }
            return false;
        }

        private float GetCommandLineArgument(string name) {
            string[] args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++) {
                if (args[i] == $"-{name}") {
                    return float.Parse(args[i + 1]);
                }
            }
            return 1;
        }
    }
}