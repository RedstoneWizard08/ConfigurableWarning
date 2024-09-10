// -----------------------------------------------------------------------
// <copyright file="BoolSetting.cs" company="ContentSettings">
// Copyright (c) ContentSettings. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ContentSettings.API.Settings;

using DebugUI;
using Internal;
using Zorro.Settings;
using Zorro.Settings.DebugUI;

/// <summary>
/// A setting that contains a boolean value.
/// </summary>
public abstract class BoolSetting : Setting {
    /// <summary>
    /// Gets a value indicating whether the setting is enabled.
    /// </summary>
    public bool Value { get; protected set; }

    /// <summary>
    /// Loads the setting using the provided loader.
    /// </summary>
    /// <param name="loader">The loader to load the setting with.</param>
    public override void Load(ISettingsSaveLoad loader) {
        Value = loader.TryLoadInt(GetType(), out var value) ? value != 0 : GetDefaultValue();
    }

    /// <summary>
    /// Save the setting using the provided loader.
    /// </summary>
    /// <param name="saver">The loader to save the setting with.</param>
    public override void Save(ISettingsSaveLoad saver) => saver.SaveInt(GetType(), Value ? 1 : 0);

    /// <summary>
    /// Sets the value of the setting and saves it.
    /// </summary>
    /// <param name="newValue">The new value of the setting.</param>
    /// <param name="settingHandler">The setting handler to save the setting with.</param>
    public void SetValue(bool newValue, ISettingHandler settingHandler) {
        Value = newValue;
        ApplyValue();
        settingHandler.SaveSetting(this);
    }

    /// <summary>
    /// Gets the setting UI for the setting.
    /// </summary>
    /// <returns>The setting UI for the setting.</returns>
    public override UnityEngine.GameObject GetSettingUICell() => SettingsMapper.BoolSettingCell;

    /// <summary>
    /// Gets the setting UI for the setting.
    /// </summary>
    /// <param name="settingHandler">The setting handler to get the setting UI for.</param>
    /// <returns>The setting UI for the setting.</returns>
    public override SettingUI GetDebugUI(ISettingHandler settingHandler)
        => new BoolSettingsUI(this, settingHandler);

    /// <summary>
    /// Exposes the value of the setting as a string, for display purposes.
    /// </summary>
    /// <param name="value">The value to expose.</param>
    /// <returns>The exposed value.</returns>
    public virtual string Expose(bool value) => value.ToString();

    /// <summary>
    /// Gets the default value for the setting.
    /// </summary>
    /// <returns>The default value for the setting.</returns>
    public abstract bool GetDefaultValue();
}
