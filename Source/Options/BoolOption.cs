using System;
using System.Collections.Generic;
using ContentSettings.API;
using ContentSettings.API.Settings;

namespace ConfigurableWarning.Options {
    /// <summary>
    /// A boolean option. This *must* be inherited from to use.
    /// Its state is stored in the <see cref="OptionsState" /> class.
    /// </summary>
    public class BoolOption : BoolSetting, IOption<bool>, IUntypedOption {
        private readonly string _name;
        private readonly string _displayName;
        private readonly bool _defaultValue;
        private readonly string _tab;
        private readonly string _category;
        private readonly List<Action<BoolOption>> _applyActions;

        protected BoolOption(string name, bool defaultValue, string displayName, string tab, string category, Action<BoolOption>[] actions) {
            _name = name;
            _displayName = displayName;
            _defaultValue = defaultValue;
            _tab = tab;
            _category = category;
            _applyActions = [.. actions];

            AsOption().Register(tab, category);
        }

        public void RegisterSetting(string tab, string category) {
            SettingsLoader.RegisterSetting(tab, category, this);
        }

        /// <summary>
        /// Applies the value. This is run when the user changes the value.
        /// This will sync it, update the state, and run any apply actions.
        /// </summary>
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
