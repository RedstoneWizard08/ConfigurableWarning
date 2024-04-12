using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.PropertyVariants;
using Zorro.Settings;

namespace ConfigurableWarning.Settings {
    public static class SettingsLoader {
        private static readonly DefaultSettingsSaveLoad SaveLoader = new();

        private static readonly List<Setting> Settings = new();

        /// <summary>
        /// Register a custom setting.
        /// </summary>
        /// <remarks>This will apply the loaded value of the setting immediately. See <see cref="Setting.ApplyValue"/>.</remarks>
        /// <param name="setting">The setting to register.</param>
        public static void RegisterSetting(Setting setting) {
            Settings.Add(setting);
            setting.Load(SaveLoader);
            setting.ApplyValue();
        }

        /// <summary>
        /// Loads the settings into the settings menu.
        /// </summary>
        /// <param name="menu">The settings menu to load the settings into.</param>
        internal static void LoadSettingsMenu(SettingsMenu menu) {
            foreach (var settingsCell in menu.m_cells) {
                Object.Destroy(settingsCell.gameObject);
            }

            menu.m_cells.Clear();

            var settingsHandler = GameHandler.Instance.SettingsHandler;
            var settings = Settings;

            foreach (var setting in settings) {
                var component = Object.Instantiate(menu.m_settingsCell, menu.m_settingsContainer).GetComponent<SettingsCell>();

                component.Setup(setting, settingsHandler);
                menu.m_cells.Add(component);
            }
        }

        /// <summary>
        /// Saves all registered settings.
        /// </summary>
        internal static void SaveSettings() {
            foreach (var setting in Settings) {
                setting.Save(SaveLoader);
            }
        }

        /// <summary>
        /// Creates the settings tab for the modded settings.
        /// </summary>
        /// <param name="menu">The settings menu to create the tab in.</param>
        /// <exception cref="System.Exception">Thrown when the existing tab to create the modded settings tab from is not found.</exception>
        internal static void CreateSettings(SettingsMenu menu) {
            var settingsTabs = menu.transform.Find("Content")?.Find("TABS");

            if (settingsTabs == null) {
                throw new System.Exception("Failed to find settings tab.");
            }

            if (settingsTabs.Find("GAMEPLAY") != null) {
                return;
            }

            var existingTab = settingsTabs.GetChild(0)?.gameObject;

            if (existingTab == null) {
                throw new System.Exception("Failed to find existing tab.");
            }

            var customSettingsTab = Object.Instantiate(existingTab, settingsTabs, true);
            customSettingsTab.name = "GAMEPLAY";

            var customSettingsTabText = customSettingsTab.transform.GetChild(1);

            Object.Destroy(customSettingsTabText.GetComponent<GameObjectLocalizer>());
            customSettingsTabText.GetComponent<TextMeshProUGUI>().SetText("GAMEPLAY");

            LoadSettingsMenu(menu);
        }
    }
}
