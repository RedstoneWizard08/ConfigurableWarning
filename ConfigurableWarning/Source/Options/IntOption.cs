using System;
using System.Collections.Generic;
using ContentSettings.API;
using ContentSettings.API.Settings;
using JetBrains.Annotations;
using Unity.Mathematics;

namespace ConfigurableWarning.Options;

/// <summary>
///     An int option. This *must* be inherited from to use.
///     Its state is stored in the <see cref="OptionsState" /> class.
/// </summary>
public class IntOption : IntSetting, IOption<int>, IUntypedOption {
    private readonly List<Action<IntOption>> _applyActions;
    private readonly int _defaultValue;
    private readonly string _displayName;
    private readonly int2 _minMax;
    private readonly string _name;
    internal readonly bool _shouldClamp;

    protected IntOption(string name, int defaultValue, string displayName, int min, int max, string tab,
        string category, Action<IntOption>[] actions, bool doClamp = true) {
        _name = name;
        _displayName = displayName;
        _defaultValue = defaultValue;
        _minMax = new int2(min, max);
        _shouldClamp = doClamp;
        _applyActions = [.. actions];

        AsOption().Register(tab, category);
    }

    public void RegisterSetting(string tab, string category) {
        SettingsLoader.RegisterSetting(tab, category, this);
    }

    public void SetValue(int value) {
        Value = _shouldClamp ? Clamp(value) : value;
        GameHandler.Instance.SettingsHandler.SaveSetting(this);
    }

    public string GetName() {
        return _name;
    }

    public int GetValue() {
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
        SetValue((int) value);
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

    public override int GetDefaultValue() {
        return _defaultValue;
    }

    public override (int, int) GetMinMaxValue() {
        return (_minMax.x, _minMax.y);
    }

    [UsedImplicitly]
    public IOption<int> AsOption() {
        return this;
    }
}