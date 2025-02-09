// -----------------------------------------------------------------------
// <copyright file="SettingsTab.cs" company="ContentSettings">
// Copyright (c) ContentSettings. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using ContentSettings.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zorro.Core;
using Zorro.Settings;

#pragma warning disable CS0612 // The obsolete message is only for other mods, we still need to use these methods.

namespace ContentSettings.API.Settings.UI;

/// <summary>
///     A component representing a tab in a settings menu, which can be selected to show settings belonging to the tab.
/// </summary>
public class SettingsTab : SettingsButton {
    /// <summary>
    ///     The current settings menu instance.
    /// </summary>
    [SerializeField] private SettingsMenu? settingsMenu;

    /// <inheritdoc />
    protected override void Awake() {
        base.Awake();

        settingsMenu = GetComponentInParent<SettingsMenu>();
    }

    /// <summary>
    ///     Called by Unity when the script instance has been loaded.
    /// </summary>
    protected void Start() {
        GetComponent<Button>().onClick.AddListener(OnPointerClicked);
    }

    /// <summary>
    ///     Shows the settings for the tab.
    /// </summary>
    public void Show() {
        if (!SettingsLoader.TryGetTab(Name, out var settingsByCategory)) return;

        if (settingsMenu == null) return;

        settingsMenu.m_cells.ForEach(c => Destroy(c.gameObject));
        settingsMenu.m_cells.Clear();
        settingsMenu.m_settingsContainer?.ClearChildren();

        if (settingsMenu.m_settingsCell == null || settingsMenu.m_settingsContainer == null) return;

        if (settingsByCategory.ContainsKey(string.Empty)) settingsByCategory[string.Empty].ForEach(CreateSettingCell);

        foreach (var (category, settings) in settingsByCategory.Where(s => s.Key != string.Empty)) {
            var categoryCell = Instantiate(SettingsAssets.SettingsCategoryPrefab, settingsMenu.m_settingsContainer);
            categoryCell.GetComponentInChildren<TextMeshProUGUI>().text = category;

            settings.ForEach(CreateSettingCell);
        }
    }

    /// <summary>
    ///     Called when the tab is clicked, selecting the tab.
    /// </summary>
    public void OnPointerClicked() {
        transform.parent.GetComponent<SettingsNavigation>().Select(this);
    }

    private void CreateSettingCell(Setting setting) {
        if (settingsMenu == null) throw new Exception("Settings menu is null.");

        var component = Instantiate(settingsMenu.m_settingsCell, settingsMenu.m_settingsContainer)
            .GetComponent<SettingsCell>();
        component.Setup(setting, GameHandler.Instance.SettingsHandler);
        settingsMenu.m_cells.Add(component);
    }
}