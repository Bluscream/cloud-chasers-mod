using System;
using UniverseLib.UI.Models;
using UniverseLib.UI;
using UniverseLib.UI.Panels;
using UnityEngine.UI;
using System.Linq;

namespace StormChasers {
    public static class PanelExtensions {
        public static ButtonRef AddButton(this PanelBase panel, string text, Action action) {
            ButtonRef btn = UIFactory.CreateButton(panel.ContentRoot, null, text);
            UIFactory.SetLayoutElement(btn.Component.gameObject, minWidth: 200, minHeight: 25);
            btn.OnClick += action;
            return btn;
        }
        public static Slider AddSlider(this PanelBase panel, string text, int value = 50, int minValue = 0, int maxValue = 100, UnityEngine.Events.UnityAction<int> action = null) => AddSlider(panel, text, (float)value, (float)minValue, (float)maxValue, (float val) => action?.Invoke((int)val));
        public static Slider AddSlider(this PanelBase panel, string text, float value = 50f, float minValue = 0f, float maxValue = 100f, UnityEngine.Events.UnityAction<float> action = null) {
            Text sliderTxt = UIFactory.CreateLabel(panel.ContentRoot, "", text);
            UIFactory.SetLayoutElement(sliderTxt.gameObject, minWidth: 200, minHeight: 25);
            var sliderObj = UIFactory.CreateSlider(panel.ContentRoot, "", out Slider outSlider);
            outSlider.minValue = minValue;
            outSlider.maxValue = maxValue;
            outSlider.value = value;
            outSlider.onValueChanged.AddListener(action);
            UIFactory.SetLayoutElement(sliderObj, minWidth: 200, minHeight: 25);
            return outSlider;
        }
        public static InputFieldRef AddTextInput(this PanelBase panel, string placeholder = "", Action<string> onValueChanged = null) {
            InputFieldRef input = UIFactory.CreateInputField(panel.ContentRoot, null, placeholder);
            UIFactory.SetLayoutElement(input.Component.gameObject, minWidth: 200, minHeight: 25);
            if (onValueChanged != null) {
                input.OnValueChanged += onValueChanged;
            }
            return input;
        }

        public static void PopulateDropdown(this Dropdown dropdown, string[] options, int defaultIndex = 0) {
            dropdown.ClearOptions();
            dropdown.AddOptions(options.ToList());
            dropdown.value = defaultIndex;
        }

        public static void AddToDropdown(this Dropdown dropdown, string option) {
            dropdown.options.Add(new Dropdown.OptionData(option));
        }
    }
}
