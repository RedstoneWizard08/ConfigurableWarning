using System;
using System.Collections.Generic;
using ContentSettings.API;
using ContentSettings.API.Settings;
using Unity.Mathematics;

namespace ConfigurableWarning.Options {
    /// <summary>
    /// An int option. This *must* be inherited from to use.
    /// Its state is stored in the <see cref="OptionsState" /> class.
    /// </summary>
    public class IntOption : IntSetting, IOption<int>, IUntypedOption {
        private readonly string _name;
        private readonly string _displayName;
        private readonly int _defaultValue;
        private readonly int2 _minMax;
        private readonly string _tab;
        private readonly string _category;
        internal readonly bool _shouldClamp;
        private readonly List<Action<IntOption>> _applyActions;

        protected IntOption(string name, int defaultValue, string displayName, int min, int max, string tab, string category, Action<IntOption>[] actions, bool doClamp = true) {
            _name = name;
            _displayName = displayName;
            _defaultValue = defaultValue;
            _minMax = new int2(min, max);
            _tab = tab;
            _category = category;
            _shouldClamp = doClamp;
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

        public void SetValue(int value) {
            Value = _shouldClamp ? Clamp(value) : value;
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
