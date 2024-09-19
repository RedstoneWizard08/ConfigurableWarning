// -----------------------------------------------------------------------
// <copyright file="IntSettingsUI.cs" company="ContentSettings">
// Copyright (c) ContentSettings. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ContentSettings.API.Settings.DebugUI;

using UnityEngine.UIElements;
using Zorro.Core;
using Zorro.Settings;
using Zorro.Settings.DebugUI;
using IntSetting = IntSetting;

/// <summary>
/// The debug UI for an integer setting.
/// </summary>
public class IntSettingsUI : SettingUI {
    private readonly IntSetting _setting;

    private readonly ISettingHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="IntSettingsUI"/> class.
    /// </summary>
    /// <param name="setting">The integer setting to create the UI for.</param>
    /// <param name="settingHandler">The setting handler to use.</param>
    public IntSettingsUI(IntSetting setting, ISettingHandler settingHandler) {
        _setting = setting;
        _handler = settingHandler;

        SingletonAsset<SettingUxmls>.Instance.IntSettingUxml.CloneTree(this);
        var label = this.Q<Label>("SettingName");
        var control = this.Q<IntegerField>();
        label.text = setting.GetType().Name;
        control.SetValueWithoutNotify(setting.Value);
        control.RegisterValueChangedCallback(Callback);
    }

    private void Callback(ChangeEvent<int> changeEvent) {
        _setting.SetValue(changeEvent.newValue, _handler);
    }
}