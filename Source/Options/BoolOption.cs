using System;
using System.Collections.Generic;
using ContentSettings.API;
using ContentSettings.API.Settings;

namespace ConfigurableWarning.Options {
    public class BoolOption : BoolSetting, IOption<bool>, IUntypedOption {
        private readonly string _name;
        private readonly string _displayName;
        private readonly bool _defaultValue;
        public string Tab;
        public string Category;
        private readonly List<Action<BoolOption>> _applyActions;

        protected BoolOption(string name, bool defaultValue, string displayName, string tab, string category, Action<BoolOption>[] actions) {
            _name = name;
            _displayName = displayName;
            _defaultValue = defaultValue;
            Tab = tab;
            Category = category;
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

        public void SetValue(bool value) {
            Value = value;
            GameHandler.Instance.SettingsHandler.SaveSetting(this);
        }

        public string GetName() => _name;
        public bool GetValue() => Value;
        public override bool GetDefaultValue() => _defaultValue;
        public string GetDisplayName() => _displayName;
        public IUntypedOption AsUntyped() => this;
        public IOption<bool> AsOption() => this;

        object IUntypedOption.GetValue() => GetValue();
        public void SetValue(object value) => SetValue((bool) value);
    }
}
