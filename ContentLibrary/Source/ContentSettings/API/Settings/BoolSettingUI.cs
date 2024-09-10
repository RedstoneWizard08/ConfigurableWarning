// -----------------------------------------------------------------------
// <copyright file="BoolSettingUI.cs" company="ContentSettings">
// Copyright (c) ContentSettings. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ContentSettings.API.Settings;

using UnityEngine;
using UnityEngine.UI;
using Zorro.Settings;

/// <summary>
/// The UI for a boolean setting.
/// </summary>
public class BoolSettingUI : SettingInputUICell {
    [SerializeField]
    private Toggle? toggle;

    /// <summary>
    /// Sets up the setting UI with the provided setting and setting handler.
    /// </summary>
    /// <param name="setting">The setting to set up the UI with.</param>
    /// <param name="settingHandler">The setting handler to use for saving the setting.</param>
    public override void Setup(Setting setting, ISettingHandler settingHandler) {
        if (setting is not BoolSetting boolSetting) {
            return;
        }

        if (toggle == null) {
            return;
        }

        toggle.isOn = boolSetting.Value;
        toggle.onValueChanged.AddListener(OnToggleChanged);
        return;

        void OnToggleChanged(bool value) {
            boolSetting.SetValue(value, settingHandler);
        }
    }

    private void Awake() {
        toggle = GetComponentInChildren<Toggle>();
    }
}
