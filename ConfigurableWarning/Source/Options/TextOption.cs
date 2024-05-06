using System;
using System.Collections.Generic;
using ContentSettings.API;
using ContentSettings.API.Settings;
using JetBrains.Annotations;

namespace ConfigurableWarning.Options;

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

    public TextOption(string name, string defaultValue, string displayName, string tab, string category,
        Action<TextOption>[] actions) {
        _name = name;
        _displayName = displayName;
        _defaultValue = defaultValue;
        _applyActions = [.. actions];

        AsOption().Register(tab, category);
    }

    public void RegisterSetting(string tab, string category) {
        SettingsLoader.RegisterSetting(tab, category, this);
    }

    public void SetValue(string value) {
        Value = value;
        GameHandler.Instance.SettingsHandler.SaveSetting(this);
    }

    public string GetName() {
        return _name;
    }

    public string GetValue() {
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
        SetValue((string) value);
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

    public override string GetDefaultValue() {
        return _defaultValue;
    }

    [UsedImplicitly]
    public IOption<string> AsOption() {
        return this;
    }
}