using System;
using System.Collections.Generic;
using ContentSettings.API;
using ContentSettings.API.Settings;

namespace ConfigurableWarning.Options {
    // ReSharper disable once ClassNeverInstantiated.Global
    public class TextOption : TextSetting, IOption<string>, IUntypedOption {
        private readonly string _name;
        private readonly string _displayName;
        private readonly string _defaultValue;
        public string Tab;
        public string Category;
        private readonly List<Action<TextOption>> _applyActions;

        public TextOption(string name, string defaultValue, string displayName, string tab, string category, Action<TextOption>[] actions) {
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

        public void SetValue(string value) {
            Value = value;
            GameHandler.Instance.SettingsHandler.SaveSetting(this);
        }

        public string GetName() => _name;
        public string GetValue() => Value;
        public override string GetDefaultValue() => _defaultValue;
        public string GetDisplayName() => _displayName;
        public IUntypedOption AsUntyped() => this;
        public IOption<string> AsOption() => this;

        object IUntypedOption.GetValue() => GetValue();
        public void SetValue(object value) => SetValue((string) value);
    }
}
