using System;
using System.Collections.Generic;
using ContentSettings.API;
using ContentSettings.API.Settings;
using Unity.Mathematics;

namespace ConfigurableWarning.Options {
    public class IntOption : IntSetting, IOption<int>, IUntypedOption {
        private readonly string _name;
        private readonly string _displayName;
        private readonly int _defaultValue;
        private readonly int2 _minMax;
        public string Tab;
        public string Category;
        public readonly bool ShouldClamp;
        private readonly List<Action<IntOption>> _applyActions;

        protected IntOption(string name, int defaultValue, string displayName, int min, int max, string tab, string category, Action<IntOption>[] actions, bool doClamp = true) {
            _name = name;
            _displayName = displayName;
            _defaultValue = defaultValue;
            _minMax = new int2(min, max);
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

        public void SetValue(int value) {
            Value = ShouldClamp ? Clamp(value) : value;
            GameHandler.Instance.SettingsHandler.SaveSetting(this);
        }

        public string GetName() => _name;
        public int GetValue() => Value;
        public override int GetDefaultValue() => _defaultValue;
        public string GetDisplayName() => _displayName;
        public override (int, int) GetMinMaxValue() => (_minMax.x, _minMax.y);
        public IUntypedOption AsUntyped() => this;
        public IOption<int> AsOption() => this;

        object IUntypedOption.GetValue() => GetValue();
        public void SetValue(object value) => SetValue((int) value);
    }
}
