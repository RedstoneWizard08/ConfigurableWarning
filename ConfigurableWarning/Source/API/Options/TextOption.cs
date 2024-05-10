using System;
using System.Collections.Generic;
using ContentSettings.API;
using ContentSettings.API.Settings;

namespace ConfigurableWarning.API.Options;

/// <summary>
///     A string option. This *must* be inherited from to use.
///     Its state is stored in the <see cref="OptionsState" /> class.
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class TextOption : TextSetting, IOption<string>, IUntypedOption {
    private readonly List<Action<TextOption>> _applyActions;
    private readonly string _defaultValue;
    private readonly string _displayName;
    private readonly string _name;

    /// <summary>
    ///     Initialize a <see cref="IOption{T}" /> with the <see cref="string" /> type.
    /// </summary>
    /// <param name="name">The option's name.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="displayName">The option's displayed name.</param>
    /// <param name="tab">The tab to register to.</param>
    /// <param name="category">The category to register to.</param>
    protected TextOption(string name, string defaultValue, string displayName, string tab, string category) : this(name,
        defaultValue, displayName, tab, category, []) {
    }

    /// <summary>
    ///     Initialize a <see cref="IOption{T}" /> with the <see cref="string" /> type.
    /// </summary>
    /// <param name="name">The option's name.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="displayName">The option's displayed name.</param>
    /// <param name="tab">The tab to register to.</param>
    /// <param name="category">The category to register to.</param>
    /// <param name="actions">Functions to run when the value is applied.</param>
    public TextOption(string name, string defaultValue, string displayName, string tab, string category,
        Action<TextOption>[] actions) {
        _name = name;
        _displayName = displayName;
        _defaultValue = defaultValue;
        _applyActions = [.. actions];

        AsOption().Register(tab, category);
    }

    /// <inheritdoc />
    public string? State {
        get => AsOption().State;
        set => AsOption().State = value;
    }

    /// <inheritdoc />
    public void RegisterSetting(string tab, string category) {
        SettingsLoader.RegisterSetting(tab, category, this);
    }

    /// <inheritdoc />
    public void SetValue(string value) {
        Value = value;
        GameHandler.Instance.SettingsHandler.SaveSetting(this);
    }

    /// <inheritdoc />
    public string GetName() {
        return _name;
    }

    /// <inheritdoc />
    public string GetValue() {
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
    public override string GetDefaultValue() {
        return _defaultValue;
    }

    /// <inheritdoc />
    public IOption<string> AsOption() {
        return this;
    }

    object IUntypedOption.GetValue() {
        return GetValue();
    }

    /// <inheritdoc />
    public void SetValue(object value) {
        SetValue((string) value);
    }

    /// <summary>
    ///     Get an instance of an option.
    /// </summary>
    /// <param name="name">The option's name.</param>
    /// <returns>The option.</returns>
    public static TextOption? Instance(string name) {
        return (TextOption?) IOption<string>.Instance(name);
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