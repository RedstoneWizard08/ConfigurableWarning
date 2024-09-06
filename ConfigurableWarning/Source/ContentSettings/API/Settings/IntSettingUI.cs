// -----------------------------------------------------------------------
// <copyright file="IntSettingUI.cs" company="ContentSettings">
// Copyright (c) ContentSettings. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ContentSettings.API.Settings;

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zorro.Settings;

/// <summary>
/// The UI for an integer setting.
/// </summary>
public class IntSettingUI : SettingInputUICell
{
    [SerializeField]
    private TMP_InputField? inputField;

    [SerializeField]
    private Slider? slider;

    /// <summary>
    /// Sets up the setting UI with the provided setting and setting handler.
    /// </summary>
    /// <param name="setting">The setting to set up the UI with.</param>
    /// <param name="settingHandler">The setting handler to use for saving the setting.</param>
    public override void Setup(Setting setting, ISettingHandler settingHandler)
    {
        if (setting is not IntSetting intSetting)
        {
            return;
        }

        if (inputField == null || slider == null)
        {
            return;
        }

        slider.maxValue = intSetting.MaxValue;
        slider.minValue = intSetting.MinValue;

        inputField.SetTextWithoutNotify(intSetting.Expose(intSetting.Value));
        slider.SetValueWithoutNotify(intSetting.Value);

        inputField.onValueChanged.AddListener(OnInputChanged);
        inputField.onDeselect.AddListener(_ => OnInputChanged(inputField.text));
        slider.onValueChanged.AddListener(OnSliderChanged);

        return;

        void OnInputChanged(string value)
        {
            if (!int.TryParse(value, out var result))
            {
                return;
            }

            intSetting.SetValue(result, settingHandler);

            inputField.SetTextWithoutNotify(intSetting.Expose(result));
            slider.SetValueWithoutNotify(result);
        }

        void OnSliderChanged(float value)
        {
            intSetting.SetValue((int)value, settingHandler);

            inputField.SetTextWithoutNotify(intSetting.Expose(intSetting.Value));
            slider.SetValueWithoutNotify(intSetting.Value);
        }
    }

    private void Awake()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
        slider = GetComponentInChildren<Slider>();
    }
}
