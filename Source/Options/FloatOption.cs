using System;
using System.Collections.Generic;
using ConfigurableWarning.Options.Untyped;
using ContentSettings.API;
using HarmonyLib;
using Unity.Mathematics;
using Zorro.Settings;

namespace ConfigurableWarning.Options {
    public class FloatOption : FloatSetting, IOption<float> {
        private readonly string _name;
        private readonly string _displayName;
        private readonly float _defaultValue;
        private readonly float2 _minMax;
        public string Tab;
        public string Category;
        public readonly bool ShouldClamp;
        private readonly List<Action<FloatOption>> _applyActions;

        protected FloatOption(string name, float defaultValue, string displayName, float min, float max, string tab, string category, Action<FloatOption>[] actions, bool doClamp = true) {
            _name = name;
            _displayName = displayName;
            _defaultValue = defaultValue;
            _minMax = new float2(min, max);
            Tab = tab;
            Category = category;
            ShouldClamp = doClamp;
            _applyActions = [.. actions];

            AsOption().Register(tab, category);
        }

        public void RegisterSetting(string tab, string category) {
            SettingsLoader.RegisterSetting(tab, category, this);
        }

        public override void ApplyValue() {
            OptionsState.Instance.Update(this);
            Plugin.Sync.SyncSettings();

            foreach (var action in _applyActions) {
                action(this);
            }
        }

        public void SetValue(float value) {
            Value = ShouldClamp ? Clamp(value) : value;
            GameHandler.Instance.SettingsHandler.SaveSetting(this);
        }

        public string GetName() => _name;
        public float GetValue() => Value;
        public override float GetDefaultValue() => _defaultValue;
        public string GetDisplayName() => _displayName;
        public override float2 GetMinMaxValue() => _minMax;
        public IUntypedOption AsUntyped() => new UntypedFloatOption(this);
        public IOption<float> AsOption() => this;
    }
}
