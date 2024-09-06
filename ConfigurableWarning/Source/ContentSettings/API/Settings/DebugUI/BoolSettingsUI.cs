// -----------------------------------------------------------------------
// <copyright file="BoolSettingsUI.cs" company="ContentSettings">
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
/// The debug UI for a boolean setting.
/// </summary>
public class BoolSettingsUI : SettingUI
{
    private readonly BoolSetting _setting;

    private readonly ISettingHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="BoolSettingsUI"/> class.
    /// </summary>
    /// <param name="setting">The boolean setting to create the UI for.</param>
    /// <param name="settingHandler">The setting handler to use.</param>
    public BoolSettingsUI(BoolSetting setting, ISettingHandler settingHandler)
    {
        _setting = setting;
        _handler = settingHandler;

        SingletonAsset<SettingUxmls>.Instance.IntSettingUxml.CloneTree(this);
        var label = this.Q<Label>("SettingName");
        var control = this.Q<IntegerField>();
        label.text = setting.GetType().Name;
        control.SetValueWithoutNotify(setting.Value ? 1 : 0);
        control.RegisterValueChangedCallback(Callback);
    }

    private void Callback(ChangeEvent<int> changeEvent)
    {
        _setting.SetValue(changeEvent.newValue != 0, _handler);
    }
}
