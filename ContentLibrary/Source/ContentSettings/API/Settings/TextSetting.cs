// -----------------------------------------------------------------------
// <copyright file="TextSetting.cs" company="ContentSettings">
// Copyright (c) ContentSettings. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ContentSettings.API.Settings;

using DebugUI;
using Internal;
using UnityEngine;
using Zorro.Settings;
using Zorro.Settings.DebugUI;

/// <summary>
/// A setting that contains a text value.
/// </summary>
public abstract class TextSetting : Setting {
    /// <summary>
    /// Gets the value of the setting.
    /// </summary>
    public string Value { get; protected set; } = string.Empty;

    /// <summary>
    /// Loads the setting using the provided loader.
    /// </summary>
    /// <param name="loader">The loader to load the setting with.</param>
    public override void Load(ISettingsSaveLoad loader) {
        Value = loader.TryGetString(GetType(), out var value) ? value : GetDefaultValue();
    }

    /// <summary>
    /// Save the setting using the provided loader.
    /// </summary>
    /// <param name="saver">The loader to save the setting with.</param>
    public override void Save(ISettingsSaveLoad saver) {
        saver.SaveString(GetType(), Value);
    }

    /// <summary>
    /// Sets the value of the setting and saves it.
    /// </summary>
    /// <param name="newValue">The new value of the setting.</param>
    /// <param name="settingHandler">The setting handler to save the setting with.</param>
    public void SetValue(string newValue, ISettingHandler settingHandler) {
        Value = newValue;
        ApplyValue();
        settingHandler.SaveSetting(this);
    }

    /// <summary>
    /// Gets the setting UI for the setting.
    /// </summary>
    /// <returns>The setting UI for the setting.</returns>
    public override GameObject GetSettingUICell() {
        return SettingsMapper.TextSettingCell;
    }

    /// <summary>
    /// Gets the debug UI for the setting.
    /// </summary>
    /// <param name="settingHandler">The setting handler to get the debug UI for.</param>
    /// <returns>The debug UI for the setting.</returns>
    public override SettingUI GetDebugUI(ISettingHandler settingHandler) {
        return new TextSettingsUI(this, settingHandler);
    }

    /// <summary>
    /// Expose the setting value, you should apply any formatting here.
    /// </summary>
    /// <param name="value">The value to expose.</param>
    /// <returns>The exposed value.</returns>
    public virtual string Expose(string value) {
        return value;
    }

    /// <summary>
    /// Get the default value for the setting.
    /// </summary>
    /// <returns>The default value for the setting.</returns>
    public abstract string GetDefaultValue();
}