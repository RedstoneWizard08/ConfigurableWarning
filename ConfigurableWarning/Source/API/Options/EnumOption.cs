using System;
using System.Collections.Generic;
using System.Linq;
using ContentSettings.API;
using Zorro.Settings;

namespace ConfigurableWarning.API.Options;

/// <summary>
///     An enum option. This *must* be inherited from to use.
///     Its state is stored in the <see cref="OptionsState" /> class.
/// </summary>
public class EnumOption<T> : EnumSetting, IOption<T> where T : struct {
    private readonly List<Action<EnumOption<T>>> _applyActions;
    private readonly T _defaultValue;
    private readonly string _displayName;
    private readonly string _name;

    /// <summary>
    ///     Initialize a <see cref="IOption{T}" /> with the <see cref="T" /> type.
    /// </summary>
    /// <param name="name">The option's name.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="displayName">The option's displayed name.</param>
    /// <param name="tab">The tab to register to.</param>
    /// <param name="category">The category to register to.</param>
    protected EnumOption(string name, T defaultValue, string displayName, string tab, string category) : this(name,
        defaultValue, displayName, tab, category, []) {
    }

    /// <summary>
    ///     Initialize a <see cref="IOption{T}" /> with the <see cref="T" /> type.
    /// </summary>
    /// <param name="name">The option's name.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="displayName">The option's displayed name.</param>
    /// <param name="tab">The tab to register to.</param>
    /// <param name="category">The category to register to.</param>
    /// <param name="actions">Functions to run when the value is applied.</param>
    protected EnumOption(string name, T defaultValue, string displayName, string tab, string category,
        Action<EnumOption<T>>[] actions) {
        _name = name;
        _displayName = displayName;
        _defaultValue = defaultValue;
        _applyActions = [.. actions];

        AsOption().Register(tab, category);
    }

    /// <summary>
    ///     The wrapped (string) value of this option.
    /// </summary>
    public string WrappedValue {
        get => EnumUtil.GetOptions<T>()[Value];
        set => Value = EnumUtil.GetIndex(EnumUtil.Parse<T>(value));
    }

    /// <inheritdoc />
    public T State {
        get => AsOption().State;
        set => AsOption().State = value;
    }

    /// <inheritdoc />
    public void RegisterSetting(string tab, string category) {
        SettingsLoader.RegisterSetting(tab, category, this);
    }

    /// <inheritdoc />
    public void SetValue(T value) {
        WrappedValue = value.ToString();
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
    public T GetValue() {
        return Enum.Parse<T>(WrappedValue);
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

    /// <inheritdoc />
    T IOption<T>.GetDefaultValue() {
        return _defaultValue;
    }

    /// <inheritdoc />
    public IOption<T> AsOption() {
        return this;
    }

    object IUntypedOption.GetValue() {
        return GetValue();
    }

    /// <inheritdoc />
    public void SetValue(object value) {
        SetValue((T) value);
    }

    /// <inheritdoc />
    public override int GetDefaultValue() {
        return EnumUtil.GetIndex(_defaultValue);
    }

    /// <summary>
    ///     Get the choices for this option.
    /// </summary>
    /// <returns>A list of choices</returns>
    public override List<string> GetChoices() {
        return EnumUtil.GetOptions<T>().ToList();
    }

    /// <summary>
    ///     Get an instance of an option.
    /// </summary>
    /// <param name="name">The option's name.</param>
    /// <returns>The option.</returns>
    public static EnumOption<T>? Instance(string name) {
        return (EnumOption<T>?) IOption<T>.Instance(name);
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
}