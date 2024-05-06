using System;
using System.Collections.Generic;
using ContentSettings.API;
using Unity.Mathematics;
using Zorro.Settings;

namespace ConfigurableWarning.Options {
    /// <summary>
    /// A float option. This *must* be inherited from to use.
    /// Its state is stored in the <see cref="OptionsState" /> class.
    /// </summary>
    public class FloatOption : FloatSetting, IOption<float>, IUntypedOption {
        private readonly string _name;
        private readonly string _displayName;
        private readonly float _defaultValue;
        private readonly float2 _minMax;
        private readonly string _tab;
        private readonly string _category;
        internal readonly bool _shouldClamp;
        private readonly List<Action<FloatOption>> _applyActions;

        protected FloatOption(string name, float defaultValue, string displayName, float min, float max, string tab, string category, Action<FloatOption>[] actions, bool doClamp = true) {
            _name = name;
            _displayName = displayName;
            _defaultValue = defaultValue;
            _minMax = new float2(min, max);
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

        public void SetValue(float value) {
            Value = _shouldClamp ? Clamp(value) : value;
            GameHandler.Instance.SettingsHandler.SaveSetting(this);
        }

        public string GetName() => _name;
        public float GetValue() => Value;
        public override float GetDefaultValue() => _defaultValue;
        public string GetDisplayName() => _displayName;
        public override float2 GetMinMaxValue() => _minMax;
        public IUntypedOption AsUntyped() => this;
        public IOption<float> AsOption() => this;

        object IUntypedOption.GetValue() => GetValue();
        public void SetValue(object value) => SetValue((float) value);
    }
}
