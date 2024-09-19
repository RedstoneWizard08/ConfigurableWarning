// -----------------------------------------------------------------------
// <copyright file="SettingsLoader.cs" company="ContentSettings">
// Copyright (c) ContentSettings. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ContentSettings.API;

using System.Collections.Generic;
using Zorro.Settings;
using JetBrains.Annotations;
using System;
using System.Linq;
using System.Reflection;
using Internal;
using Settings.UI;
using Zorro.Core;
using Object = UnityEngine.Object;

/// <summary>
/// Settings loader for custom settings belonging to mods.
/// </summary>
[Obsolete]
public static class SettingsLoader {
    /// <summary>
    /// Gets all registered settings.
    /// </summary>
    [UsedImplicitly]
    public static IEnumerable<Setting> Settings => RegisteredSettings.Values;

    private static DefaultSettingsSaveLoad SaveLoader { get; } = new();

    private static Dictionary<string, Dictionary<string, List<Setting>>> SettingsByCategoryByTab { get; set; } = [];

    private static Dictionary<Type, Setting> RegisteredSettings { get; } = [];

    private static bool IsInitialized { get; set; }

    private static bool IsDirty { get; set; }

    private static SettingsNavigation? SettingsNavigation { get; set; }

    /// <summary>
    /// Returns whether the settings manager has a tab.
    /// </summary>
    /// <param name="tab">The tab to check for.</param>
    /// <returns>True if the settings manager has the tab; otherwise, false.</returns>
    [UsedImplicitly]
    public static bool HasTab(string tab) {
        return SettingsByCategoryByTab.ContainsKey(tab);
    }

    /// <summary>
    /// Gets the settings for the specified tab.
    /// </summary>
    /// <param name="tab">The tab to get the settings for.</param>
    /// <param name="settingsByCategory">The settings by category for the tab, if found.</param>
    /// <returns>True if the tab was found; otherwise, false.</returns>
    public static bool TryGetTab(string tab, out Dictionary<string, List<Setting>> settingsByCategory) {
        return SettingsByCategoryByTab.TryGetValue(tab, out settingsByCategory);
    }

    /// <summary>
    /// Get the instance of the specified setting.
    /// </summary>
    /// <typeparam name="T">The type of the setting to get.</typeparam>
    /// <returns>The instance of the setting.</returns>
    public static T? GetSetting<T>()
        where T : Setting {
        if (RegisteredSettings.TryGetValue(typeof(T), out var setting)) return (T) setting;

        return null;
    }

    /// <summary>
    /// Register a custom setting.
    /// </summary>
    /// <remarks>This will apply the value of the setting immediately. See <see cref="Setting.ApplyValue"/>.</remarks>
    /// <param name="tab">The tab to register the setting to.</param>
    /// <param name="category">The category of the setting.</param>
    /// <param name="setting">The setting to register.</param>
    [Obsolete]
    [UsedImplicitly]
    public static void RegisterSetting(string tab, string? category, Setting setting) {
        category ??= string.Empty;

        var settingType = setting.GetType();
        ContentSettingsEntry.Logger.LogDebug(
            $"Registering setting {settingType.Name}({settingType.BaseType?.Name}) to tab {tab} and category {category}.");

        var settingsByCategory = SettingsByCategoryByTab.GetValueOrDefault(tab, []);
        var settings = settingsByCategory.GetValueOrDefault(category, []);

        settingsByCategory[category] = settings;
        SettingsByCategoryByTab[tab] = settingsByCategory;

        settings.Add(setting);

        if (!RegisteredSettings.ContainsKey(settingType)) {
            setting.Load(SaveLoader);
            setting.ApplyValue();

            RegisteredSettings.Add(settingType, setting);
        }

        if (IsInitialized) IsDirty = true;
    }

    /// <summary>
    /// Register a custom setting to the provided tab without a category.
    /// </summary>
    /// <param name="tab">The tab to register the setting to.</param>
    /// <param name="setting">The setting to register.</param>
    [Obsolete]
    [UsedImplicitly]
    public static void RegisterSetting(string tab, Setting setting) {
        RegisterSetting(tab, string.Empty, setting);
    }

    /// <summary>
    /// Register a custom setting to the default MODDED tab and empty category.
    /// </summary>
    /// <param name="setting">The setting to register.</param>
    [Obsolete]
    [UsedImplicitly]
    public static void RegisterSetting(Setting setting) {
        RegisterSetting("MODDED", string.Empty, setting);
    }

    /// <summary>
    /// Saves all registered settings.
    /// </summary>
    internal static void SaveSettings() {
        ContentSettingsEntry.Logger.LogDebug("Saving settings to disk.");
        foreach (var setting in Settings) setting.Save(SaveLoader);
    }

    /// <summary>
    /// Creates the settings tab for the modded settings.
    /// </summary>
    /// <param name="menu">The settings menu to create the tab in.</param>
    /// <exception cref="System.Exception">Thrown when the existing tab to create the modded settings tab from is not found.</exception>
    internal static void CreateSettingsMenu(SettingsMenu menu) {
        ContentSettingsEntry.Logger.LogDebug("Initializing settings.");

        var tabs = menu.transform.Find("Content")?.Find("TABS") ?? throw new Exception("Failed to find settings tab.");

        if (SettingsNavigation == null) {
            var settingsMenuObject = Object.Instantiate(SettingsAssets.SettingsNavigationPrefab, tabs.parent, false);
            var settingsTabsTransform = settingsMenuObject.transform.FindChildRecursive("Content");
            var settingsNavigation = settingsTabsTransform.gameObject.AddComponent<SettingsNavigation>();

            foreach (var tab in SettingsByCategoryByTab.Keys) settingsNavigation.Create(tab);

            SettingsNavigation = settingsNavigation;
        }

        tabs.gameObject.SetActive(false);
    }

    /// <summary>
    /// Register all settings in the current domain.
    /// </summary>
    internal static void RegisterSettings() {
        if (IsInitialized) return;

        RegisterDefaultSettings();

        ContentSettingsEntry.Logger.LogDebug("Registering settings.");

        foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())) {
            if (type.IsAbstract || type.IsInterface || !type.IsSubclassOf(typeof(Setting))) continue;

            if (RegisteredSettings.ContainsKey(type)) continue;

            var settingDefinitions = type.GetCustomAttributes<Attributes.SettingRegister>(false);
            Setting? setting = null;

            foreach (var settingDefinition in settingDefinitions) {
                setting ??= (Setting) Activator.CreateInstance(type);

                RegisterSetting(settingDefinition.Tab, settingDefinition.Category, setting);
            }
        }

        ContentSettingsEntry.Logger.LogDebug($"Registered {RegisteredSettings.Count} settings.");

        IsInitialized = true;
    }

    /// <summary>
    /// Called every frame to update the settings.
    /// </summary>
    internal static void Update() {
        foreach (var setting in Settings) setting.Update();
    }

    /// <summary>
    /// Register the default settings from the original settings system.
    /// </summary>
    private static void RegisterDefaultSettings() {
        ContentSettingsEntry.Logger.LogDebug("Registering default settings.");

        var settingsByCategoryByTab = SettingsByCategoryByTab;

        SettingsByCategoryByTab = [];

        // if (GameHandler.Instance == null) {
        //     ContentSettings.Logger.LogWarning("GameHandler is null, we've been called too early!");
        //     return;
        // }

        var settingsHandler = GameHandler.Instance.SettingsHandler;

        ContentSettingsEntry.Logger.LogInfo($"SettingsHandler is null: {settingsHandler == null}");

        foreach (var category in Enum.GetValues(typeof(SettingCategory))) {
            var settingCategory = (SettingCategory) category;
            var categorySettings = settingsHandler?.GetSettings(settingCategory) ?? [];

            foreach (var setting in categorySettings) RegisterSetting(settingCategory.ToString().ToUpper(), setting);
        }

        foreach (var tab in settingsByCategoryByTab.Keys.Where(tab => !SettingsByCategoryByTab.ContainsKey(tab)))
            SettingsByCategoryByTab.Add(tab, settingsByCategoryByTab[tab]);
    }
}