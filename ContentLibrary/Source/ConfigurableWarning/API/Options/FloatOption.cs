#pragma warning disable CS0612 // The obsolete message is only for other mods, we still need to use these methods.

using System;
using System.Collections.Generic;
using ConfigurableWarning.API.State;
using ContentSettings.API;
using Unity.Mathematics;
using Zorro.Settings;

namespace ConfigurableWarning.API.Options;

/// <summary>
///     A float option. This *must* be inherited from to use.
///     Its state is stored in the <see cref="OptionsState" /> class.
/// </summary>
public class FloatOption : FloatSetting, IOption<float> {
    private readonly List<Action<FloatOption>> _applyActions;
    private readonly float _defaultValue;
    private readonly string _displayName;
    private readonly float2 _minMax;
    private readonly string _name;

    /// <summary>
    ///     Whether to clamp the value when changed.
    /// </summary>
    internal readonly bool ShouldClamp;

    /// <summary>
    ///     Initialize a <see cref="IOption{T}" /> with the <see cref="float" /> type.
    /// </summary>
    /// <param name="name">The option's name.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="displayName">The option's displayed name.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <param name="doClamp">Whether to clamp the value when changed.</param>
    /// <param name="sync">Whether to sync this option with other clients.</param>
    protected FloatOption(
        string name,
        float defaultValue,
        string displayName,
        float min,
        float max,
        bool doClamp = true,
        bool sync = true
    ) : this(
        name,
        defaultValue,
        displayName,
        min,
        max,
        [],
        doClamp,
        sync
    ) {
    }

    /// <summary>
    ///     Initialize a <see cref="IOption{T}" /> with the <see cref="float" /> type.
    /// </summary>
    /// <param name="name">The option's name.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="displayName">The option's displayed name.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <param name="actions">Functions to run when the value is applied.</param>
    /// <param name="doClamp">Whether to clamp the value when changed.</param>
    /// <param name="sync">Whether to sync this option with other clients.</param>
    protected FloatOption(
        string name,
        float defaultValue,
        string displayName,
        float min,
        float max,
        Action<FloatOption>[] actions,
        bool doClamp = true,
        bool sync = true
    ) {
        _name = name;
        _displayName = displayName;
        _defaultValue = defaultValue;
        _minMax = new float2(min, max);
        ShouldClamp = doClamp;
        _applyActions = [.. actions];
        Sync = sync;
    }

    /// <inheritdoc />
    public float State {
        get => AsOption().State;
        set => AsOption().State = value;
    }

    /// <inheritdoc />
    public void RegisterSetting(string tab, string category) {
        SettingsLoader.RegisterSetting(tab, category, this);
    }

    /// <inheritdoc />
    public void SetValue(float value) {
        Value = ShouldClamp ? Clamp(value) : value;
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
    public float GetValue() {
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
    public override float GetDefaultValue() {
        return _defaultValue;
    }

    /// <inheritdoc />
    public IOption<float> AsOption() {
        return this;
    }

    object IUntypedOption.GetValue() {
        return GetValue();
    }

    /// <inheritdoc />
    public void SetValue(object value) {
        SetValue((float) value);
    }

    /// <inheritdoc />
    public bool Sync { get; set; } = true;

    /// <inheritdoc />
    public StateHolder<float> StateHolder => States.Floats;

    /// <summary>
    ///     Get an instance of an option.
    /// </summary>
    /// <param name="name">The option's name.</param>
    /// <returns>The option.</returns>
    public static FloatOption? Instance(string name) {
        return (FloatOption?) IOption<float>.Instance(name);
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
    public override float2 GetMinMaxValue() {
        return _minMax;
    }
}