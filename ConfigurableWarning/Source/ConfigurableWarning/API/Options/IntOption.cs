#pragma warning disable CS0612 // The obsolete message is only for other mods, we still need to use these methods.

using System;
using System.Collections.Generic;
using ConfigurableWarning.API.State;
using ContentSettings.API;
using ContentSettings.API.Settings;
using Unity.Mathematics;

namespace ConfigurableWarning.API.Options;

/// <summary>
///     An int option. This *must* be inherited from to use.
///     Its state is stored in the <see cref="OptionsState" /> class.
/// </summary>
public class IntOption : IntSetting, IOption<int> {
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
    /// <param name="doClamp">Whether to clamp the value when changed.</param>
    protected IntOption(string name, int defaultValue, string displayName, int min, int max, bool doClamp = true) :
        this(name, defaultValue, displayName, min, max, [],
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
    /// <param name="actions">Functions to run when the value is applied.</param>
    /// <param name="doClamp">Whether to clamp the value when changed.</param>
    protected IntOption(string name, int defaultValue, string displayName, int min, int max,
        Action<IntOption>[] actions, bool doClamp = true) {
        _name = name;
        _displayName = displayName;
        _defaultValue = defaultValue;
        _minMax = new int2(min, max);
        _shouldClamp = doClamp;
        _applyActions = [.. actions];
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

    /// <summary>
    ///     Gets this option's name. This is its name in the registry
    ///     and in the state holder.
    /// </summary>
    /// <returns>The option's name.</returns>
    public string GetName() {
        return _name;
    }

    /// <inheritdoc />
    public int GetValue() {
        return Value;
    }

    /// <summary>
    ///     Get the display name of this option.
    /// </summary>
    /// <returns>The option's display name.</returns>
    public string GetDisplayName() {
        return _displayName;
    }

    /// <inheritdoc />
    public IUntypedOption AsUntyped() {
        return this;
    }

    /// <summary>
    ///     Get this option's default value.
    /// </summary>
    /// <returns>The option's default value.</returns>
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
        SetValue((int)value);
    }

    /// <summary>
    ///     Get an instance of an option.
    /// </summary>
    /// <param name="name">The option's name.</param>
    /// <returns>The option.</returns>
    public static IntOption? Instance(string name) {
        return (IntOption?)IOption<int>.Instance(name);
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
    protected override (int, int) GetMinMaxValue() {
        return (_minMax.x, _minMax.y);
    }

    /// <inheritdoc />
    public StateHolder<int> StateHolder => States.Ints;
}