// -----------------------------------------------------------------------
// <copyright file="TextSettingsUI.cs" company="ContentSettings">
// Copyright (c) ContentSettings. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ContentSettings.API.Settings.DebugUI;

using UnityEngine.UIElements;
using Zorro.Core;
using Zorro.Settings;
using Zorro.Settings.DebugUI;

/// <summary>
/// The debug UI for a text setting.
/// </summary>
public class TextSettingsUI : SettingUI {
    private TextSetting _setting;

    private ISettingHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextSettingsUI"/> class.
    /// </summary>
    /// <param name="setting">The text setting to create the UI for.</param>
    /// <param name="settingHandler">The setting handler to use.</param>
    public TextSettingsUI(TextSetting setting, ISettingHandler settingHandler) {
        _setting = setting;
        _handler = settingHandler;

        SingletonAsset<SettingUxmls>.Instance.FloatSettingUxml.CloneTree(this);
        var label = this.Q<Label>("SettingName");
        label.text = setting.GetType().Name;
    }
}