// -----------------------------------------------------------------------
// <copyright file="SettingsNavigation.cs" company="ContentSettings">
// Copyright (c) ContentSettings. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ContentSettings.API.Settings.UI;

using System.Collections.Generic;
using Internal;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zorro.Core;

/// <summary>
/// A component containing settings tabs for a settings menu.
/// </summary>
[UsedImplicitly]
public class SettingsNavigation : MonoBehaviour {
    /// <summary>
    /// The settings menu this tab belongs to.
    /// </summary>
    [SerializeField]
    private SettingsMenu? settingsMenu;

    /// <summary>
    /// The currently selected tab.
    /// </summary>
    [SerializeField]
    private SettingsTab? selectedTab;

    /// <summary>
    /// The list of settings tabs available.
    /// </summary>
    [SerializeField]
    private List<SettingsTab> settingsTabs = new();

    /// <summary>
    /// The number of settings to display per page.
    /// </summary>
    [SerializeField]
    private int pageSize = 3;

    /// <summary>
    /// Gets a value indicating the current page of settings tabs.
    /// </summary>
    [UsedImplicitly]
    public int Page { get; private set; }

    /// <summary>
    /// Gets a value indicating the total number of pages of settings tabs.
    /// </summary>
    [UsedImplicitly]
    public int PageCount => Mathf.CeilToInt((float)settingsTabs.Count / pageSize);

    /// <summary>
    /// Add a settings tab to the available tabs.
    /// </summary>
    /// <param name="tab">The tab to add.</param>
    public void Add(SettingsTab tab) {
        settingsTabs.Add(tab);
    }

    /// <summary>
    /// Create a new settings tab with the specified name.
    /// </summary>
    /// <param name="tabName">The name of the tab. This will be the display name.</param>
    /// <returns>The created settings tab.</returns>
    public SettingsTab Create(string tabName) {
        var settingsTabObject = Instantiate(SettingsAssets.SettingsTabPrefab, transform);
        var settingsTab = settingsTabObject.AddComponent<SettingsTab>();
        settingsTab.Name = tabName;

        Add(settingsTab);

        return settingsTab;
    }

    /// <summary>
    /// Select a settings tab.
    /// </summary>
    /// <param name="tab">The tab to select.</param>
    public void Select(SettingsTab tab) {
        if (selectedTab != null) {
            Deselect(selectedTab);
        }

        selectedTab = tab;
        tab.Select();
        OnSelected(tab);
    }

    /// <summary>
    /// De-select a settings tab.
    /// </summary>
    /// <param name="tab">The tab to de-select.</param>
    public void Deselect(SettingsTab tab) {
        selectedTab = null;
        tab.Deselect();
    }

    /// <summary>
    /// Show the next page of settings tabs.
    /// </summary>
    public void NextPage() {
        SetPage((Page + 1) % PageCount);
    }

    /// <summary>
    /// Show the previous page of settings tabs.
    /// </summary>
    public void PreviousPage() {
        SetPage((Page - 1 + PageCount) % PageCount);
    }

    /// <summary>
    /// Select a specific page of settings tabs.
    /// </summary>
    /// <param name="page">The page to select, indexed from zero.</param>
    public void SelectPage(int page) {
        SetPage(Mathf.Clamp(page, 0, PageCount - 1));
    }

    /// <summary>
    /// Called when a tab is selected.
    /// </summary>
    /// <param name="tab">The selected tab.</param>
    public virtual void OnSelected(SettingsTab tab) {
        tab.Show();

        // Hide all other tabs except the current page
        var page = settingsTabs.IndexOf(tab) / pageSize;
        for (var i = 0; i < settingsTabs.Count; i++) {
            settingsTabs[i].gameObject.SetActive(i / pageSize == page);
        }
    }

    private void SetPage(int page) {
        Page = page;
        for (var i = 0; i < settingsTabs.Count; i++) {
            settingsTabs[i].gameObject.SetActive(i / pageSize == page);
        }
    }

    private void Awake() {
        settingsMenu = GetComponentInParent<SettingsMenu>();

        var nextTabs = settingsMenu.transform
            .FindChildRecursive("Next Tabs");
        nextTabs?.gameObject.AddComponent<SettingsButton>();
        nextTabs?.GetComponent<Button>()?
            .onClick.AddListener(NextPage);

        var previousTabs = settingsMenu.transform
            .FindChildRecursive("Previous Tabs");
        previousTabs?.gameObject.AddComponent<SettingsButton>();
        previousTabs?.GetComponent<Button>()?
            .onClick.AddListener(PreviousPage);
    }

    private void Start() {
        if (selectedTab != null) {
            Select(selectedTab);
        } else if (settingsTabs.Count > 0) {
            Select(settingsTabs[0]);
        }
    }

    private void OnEnable() {
        if (selectedTab != null) {
            Select(selectedTab);
        }
    }
}
