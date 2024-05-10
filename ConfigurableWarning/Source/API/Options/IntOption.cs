using System;
using System.Collections.Generic;
using ContentSettings.API;
using ContentSettings.API.Settings;
using Unity.Mathematics;

namespace ConfigurableWarning.API.Options;

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

    /// <summary>
    ///     Initialize a <see cref="IOption{T}" /> with the <see cref="int" /> type.
    /// </summary>
    /// <param name="name">The option's name.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="displayName">The option's displayed name.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <param name="tab">The tab to register to.</param>
    /// <param name="category">The category to register to.</param>
    /// <param name="doClamp">Whether or not to clamp the value when changed.</param>
    protected IntOption(string name, int defaultValue, string displayName, int min, int max, string tab,
        string category, bool doClamp = true) : this(name, defaultValue, displayName, min, max, tab, category, [],
        doClamp) {
    }

    /// <summary>
    ///     Initialize a <see cref="IOption{T}" /> with the <see cref="int" /> type.
    /// </summary>
    /// <param name="name">The option's name.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="displayName">The option's displayed name.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <param name="tab">The tab to register to.</param>
    /// <param name="category">The category to register to.</param>
    /// <param name="actions">Functions to run when the value is applied.</param>
    /// <param name="doClamp">Whether or not to clamp the value when changed.</param>
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

    /// <inheritdoc />
    public int State {
        get => AsOption().State;
        set => AsOption().State = value;
    }

    /// <inheritdoc />
    public void RegisterSetting(string tab, string category) {
        SettingsLoader.RegisterSetting(tab, category, this);
    }

    /// <inheritdoc />
    public void SetValue(int value) {
        Value = _shouldClamp ? Clamp(value) : value;
        GameHandler.Instance.SettingsHandler.SaveSetting(this);
    }

    /// <inheritdoc />
    public string GetName() {
        return _name;
    }

    /// <inheritdoc />
    public int GetValue() {
        return Value;
    }

    /// <inheritdoc />
    public string GetDisplayName() {
        return _displayName;
    }

    /// <inheritdoc />
    public IUntypedOption AsUntyped() {
        return this;
    }

    /// <inheritdoc />
    public override int GetDefaultValue() {
        return _defaultValue;
    }

    /// <inheritdoc />
    public IOption<int> AsOption() {
        return this;
    }

    object IUntypedOption.GetValue() {
        return GetValue();
    }

    /// <inheritdoc />
    public void SetValue(object value) {
        SetValue((int) value);
    }

    /// <summary>
    ///     Get an instance of an option.
    /// </summary>
    /// <param name="name">The option's name.</param>
    /// <returns>The option.</returns>
    public static IntOption? Instance(string name) {
        return (IntOption?) IOption<int>.Instance(name);
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

    /// <inheritdoc />
    public override (int, int) GetMinMaxValue() {
        return (_minMax.x, _minMax.y);
    }
}