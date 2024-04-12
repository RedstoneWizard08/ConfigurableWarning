using ConfigurableWarning.Settings;
using HarmonyLib;
using Zorro.Settings;

namespace ConfigurableWarning.Patches {
    [HarmonyPatch]
    internal class SettingsPatch {
        /// <summary>
        /// Patches the <see cref="DefaultSettingsSaveLoad.WriteToDisk"/> method to save our custom settings.
        /// </summary>
        [HarmonyPrefix]
        [HarmonyPatch(typeof(DefaultSettingsSaveLoad), nameof(DefaultSettingsSaveLoad.WriteToDisk))]
        internal static void WriteToDiskPatch() {
            SettingsLoader.SaveSettings();
        }

        /// <summary>
        /// Patches the <see cref="SettingCategoryTab.Select"/> method to load our custom settings when the modded settings tab is selected.
        /// </summary>
        /// <param name="__instance">The instance of the <see cref="SettingCategoryTab"/>.</param>
        /// <returns>Whether the original method should be called.</returns>
        [HarmonyPrefix]
        [HarmonyPatch(typeof(SettingCategoryTab), nameof(SettingCategoryTab.Select))]
        internal static bool SelectPatch(SettingCategoryTab __instance) {
            if (__instance.name == "GAMEPLAY") {
                SettingsLoader.LoadSettingsMenu(__instance.settingsMenu);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Patches the <see cref="SettingsMenu"/> to add our custom settings tab to the settings menu.
        /// </summary>
        /// <param name="__instance">The instance of the <see cref="SettingsMenu"/>.</param>
        /// <returns>Whether the original method should be called.</returns>
        [HarmonyPrefix]
        [HarmonyPatch(typeof(SettingsMenu), nameof(SettingsMenu.OnEnable))]
        internal static bool OnEnablePatch(SettingsMenu __instance) {
            SettingsLoader.CreateSettings(__instance);

            return true;
        }
    }
}