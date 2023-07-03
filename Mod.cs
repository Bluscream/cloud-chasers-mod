using MelonLoader;
using System;
using UnityEngine;
using UniverseLib;
using UniverseLib.UI;
using UniverseLib.Input;
using StormChasers;

namespace StormTweakers {

    internal partial class Mod : MelonMod {
        MainPanel mainPanel;
        internal bool fullyLoaded = false;
        internal static MenuTweaks menuTweaks = new MenuTweaks();
        internal static PlayerTweaks playerTweaks = new PlayerTweaks();
        internal static TruckTweaks truckTweaks = new TruckTweaks();
        internal static DebugTweaks debugTweaks = new DebugTweaks();

        internal static void Log(object message, LogType type = LogType.Log) {
            var msg = message.ToString();
            MelonLogger.Msg(msg);
            try { GameController.Instance.sendChatInfoText(msg); } catch { }
        }

        public override void OnInitializeMelon() {
            Log("OnInitializeMelon");
        }

        public override void OnSceneWasInitialized(int buildindex, string sceneName)
        {
            Log($"OnSceneWasInitialized: {buildindex} \"{sceneName}\"");
            if (!fullyLoaded && sceneName == "Main Menu") {
                fullyLoaded = true;
                OnMainMenuLoaded();
            }
        }

        internal void OnMainMenuLoaded() {
            Log("OnMainMenuLoaded");
            menuTweaks.OnMainMenuLoaded();
            Universe.Init(1f, OnUniverseLibInitialized, Log, new UniverseLib.Config.UniverseLibConfig() { Force_Unlock_Mouse = false });
        }

        internal void OnUniverseLibInitialized() {
            Log("OnUniverseLibInitialized");
            UIBase myUIBase = UniversalUI.RegisterUI("bluscream.stormtweakers", () => { });
            mainPanel = new MainPanel(myUIBase) { Enabled = false};
            //mainPanel.SetActive(false);
        }

        public override void OnLateUpdate() {
            if (InputManager.GetKeyDown(KeyCode.F6)) {
                try { mainPanel.Toggle(); } catch { mainPanel.SetActive(!mainPanel.Enabled); }
            } else if (InputManager.GetKeyDown(KeyCode.Home)) {
                menuTweaks.JoinOnlineGame();
            }
            //else if (GetKeyDown("leftarrow")) {
            //    var myTruck = GameController.Instance.getLocalCar();
            //    var oldValue = (bool)leftSignalLightOn.GetValue(myTruck);
            //    leftSignalLightOn.SetValue(myTruck, !oldValue);
            //} else if (GetKeyDown("rightarrow")) {
            //    var myTruck = GameController.Instance.getLocalCar();
            //    var oldValue = (bool)rightSignalLightOn.GetValue(myTruck);
            //    rightSignalLightOn.SetValue(myTruck, !oldValue);
            //}
        }

        internal static bool isOnline() {
            return false;
            if (GlobalValues.Instance.gameType != GlobalValues.GameType.SINGLEPLAYER_FREE_ROAMING) {
                Mod.Log("Remote control in multiplayer will get you banned"); return true;
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

        //private Type GetUnityType(string typeName, string moduleName = "CoreModule") {
        //    return Type.GetType($"UnityEngine.{typeName}, UnityEngine.{moduleName}, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
        //}

        //private Type GetInputType() {
        //    return GetUnityType("Input") ?? GetUnityType("Input", "InputLegacyModule");
        //}

        //private object GetValue(Type type, string propertyName, object instance = null) {
        //    var property = type.GetProperty(propertyName);
        //    if (property != null) {
        //        return property.GetValue(instance, null);
        //    }
        //    return type.GetField(propertyName).GetValue(instance);
        //}

        //private void SetValue(Type type, string propertyName, object value, object instance = null) {
        //    var property = type.GetProperty(propertyName);
        //    type.GetProperty(propertyName).SetValue(instance, value, null);
        //}

        //private bool GetKeyDown(string key) {
        //    return (bool)GetKeyDownMethod.Invoke(null, new[] { key });
        //}
    }
}
