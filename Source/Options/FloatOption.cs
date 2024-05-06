using System;
using System.Collections.Generic;
using ContentSettings.API;
using JetBrains.Annotations;
using Unity.Mathematics;
using Zorro.Settings;

namespace ConfigurableWarning.Options;

/// <summary>
///     A float option. This *must* be inherited from to use.
///     Its state is stored in the <see cref="OptionsState" /> class.
/// </summary>
public class FloatOption : FloatSetting, IOption<float>, IUntypedOption {
    private readonly List<Action<FloatOption>> _applyActions;
    private readonly float _defaultValue;
    private readonly string _displayName;
    private readonly float2 _minMax;
    private readonly string _name;
    internal readonly bool _shouldClamp;

    protected FloatOption(string name, float defaultValue, string displayName, float min, float max, string tab,
        string category, Action<FloatOption>[] actions, bool doClamp = true) {
        _name = name;
        _displayName = displayName;
        _defaultValue = defaultValue;
        _minMax = new float2(min, max);
        _shouldClamp = doClamp;
        _applyActions = [.. actions];

        AsOption().Register(tab, category);
    }

    public void RegisterSetting(string tab, string category) {
        SettingsLoader.RegisterSetting(tab, category, this);
    }

    public void SetValue(float value) {
        Value = _shouldClamp ? Clamp(value) : value;
        GameHandler.Instance.SettingsHandler.SaveSetting(this);
    }

    public string GetName() {
        return _name;
    }

    public float GetValue() {
        return Value;
    }

    public string GetDisplayName() {
        return _displayName;
    }

    public IUntypedOption AsUntyped() {
        return this;
    }

    object IUntypedOption.GetValue() {
        return GetValue();
    }

    public void SetValue(object value) {
        SetValue((float) value);
    }

    /// <summary>
    ///     Applies the value. This is run when the user changes the value.
    ///     This will sync it, update the state, and run any apply actions.
    /// </summary>
    public override void ApplyValue() {
        OptionsState.Instance.Update(this);
        ConfigurableWarningAPI.Sync.SyncSettings();

        foreach (var action in _applyActions) action(this);
    }

    public override float GetDefaultValue() {
        return _defaultValue;
    }

    public override float2 GetMinMaxValue() {
        return _minMax;
    }

    [UsedImplicitly]
    public IOption<float> AsOption() {
        return this;
    }
}