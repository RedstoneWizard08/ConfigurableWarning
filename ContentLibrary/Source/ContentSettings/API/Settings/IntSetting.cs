// -----------------------------------------------------------------------
// <copyright file="IntSetting.cs" company="ContentSettings">
// Copyright (c) ContentSettings. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ContentSettings.API.Settings;

using ConfigurableWarning.API.Options;
using DebugUI;
using Internal;
using JetBrains.Annotations;
using UnityEngine;
using Zorro.Settings;
using Zorro.Settings.DebugUI;

/// <summary>
/// A setting that contains an integer value.
/// </summary>
public abstract class IntSetting : Setting {
    /// <summary>
    /// Gets the value of the setting.
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    /// Gets the minimum value of the setting.
    /// </summary>
    public int MinValue { get; private set; }

    /// <summary>
    /// Gets the maximum value of the setting.
    /// </summary>
    public int MaxValue { get; private set; }

    /// <summary>
    /// Loads the setting using the provided loader.
    /// </summary>
    /// <param name="loader">The loader to load the setting with.</param>
    public override void Load(ISettingsSaveLoad loader) {
        var (min, max) = GetMinMaxValue();
        MinValue = min;
        MaxValue = max;

        Value = Clamp(loader.TryLoadInt(GetType(), out var value) ? value : GetDefaultValue());
    }

    /// <summary>
    /// Save the setting using the provided loader.
    /// </summary>
    /// <param name="saver">The loader to save the setting with.</param>
    public override void Save(ISettingsSaveLoad saver) {
        saver.SaveInt(GetType(), Value);
    }

    /// <summary>
    /// Sets the value of the setting and saves it.
    /// </summary>
    /// <param name="newValue">The new value of the setting.</param>
    /// <param name="settingHandler">The setting handler to save the setting with.</param>
    public void SetValue(int newValue, ISettingHandler settingHandler) {
        if (this is IntOption opt)
            Value = opt._shouldClamp ? Clamp(newValue) : newValue;
        else
            Value = Clamp(newValue);

        ApplyValue();
        settingHandler.SaveSetting(this);
    }

    /// <summary>
    /// Gets the setting UI for the setting.
    /// </summary>
    /// <param name="settingHandler">The setting handler to get the setting UI for.</param>
    /// <returns>The setting UI for the setting.</returns>
    public override SettingUI GetDebugUI(ISettingHandler settingHandler) {
        return new IntSettingsUI(this, settingHandler);
    }

    /// <summary>
    /// Gets the setting UI for the setting.
    /// </summary>
    /// <returns>The setting UI for the setting.</returns>
    public override GameObject GetSettingUICell() {
        return SettingsMapper.IntSettingCell;
    }

    /// <summary>
    /// Exposes the value of the setting as a string, for display purposes.
    /// </summary>
    /// <param name="value">The value to expose.</param>
    /// <returns>The exposed value.</returns>
    [UsedImplicitly]
    public virtual string Expose(int value) {
        return value.ToString();
    }

    /// <summary>
    /// Clamps the value to the minimum and maximum value of the setting.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <returns>The clamped value.</returns>
    [UsedImplicitly]
    public virtual int Clamp(int value) {
        return Mathf.Clamp(value, MinValue, MaxValue);
    }

    /// <summary>
    /// Gets the value of the setting as an integer.
    /// </summary>
    /// <returns>The default value for the setting.</returns>
    public abstract int GetDefaultValue();

    /// <summary>
    /// Gets the minimum and maximum value of the setting.
    /// </summary>
    /// <returns>A tuple containing the minimum and maximum value of the setting.</returns>
    protected abstract (int, int) GetMinMaxValue();
}