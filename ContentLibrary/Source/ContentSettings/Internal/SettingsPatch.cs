#pragma warning disable CS0612 // The obsolete message is only for other mods, we still need to use these methods.
#pragma warning disable IDE0060

using System.Diagnostics.CodeAnalysis;
using ContentSettings.API;
using HarmonyLib;
using Zorro.Core;
using Zorro.Settings;

namespace ContentSettings.Internal;

/// <summary>
/// Patches for the settings system.
/// </summary>
[HarmonyPatch]
[SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Harmony patch.")]
[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter",
    Justification = "Harmony patch.")]
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
    /// Patches the <see cref="SettingsMenu"/> to add our custom settings tab to the settings menu.
    /// </summary>
    /// <param name="__instance">The instance of the <see cref="SettingsMenu"/>.</param>
    /// <returns>Whether the original method should be called.</returns>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(SettingsMenu), nameof(SettingsMenu.OnEnable))]
    internal static bool OnEnablePatch(SettingsMenu __instance) {
        GameBooter.Initialize();

        SettingsLoader.RegisterSettings();
        SettingsLoader.CreateSettingsMenu(__instance);

        return true;
    }

    /// <summary>
    /// Patch the <see cref="MainMenuHandler"/> to register the settings.
    /// </summary>
    /// <param name="__instance">The instance of the <see cref="MainMenuHandler"/>.</param>
    /// <returns>Whether the original method should be called.</returns>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(MainMenuHandler), nameof(MainMenuHandler.Start))]
    internal static bool MainMenuHandlerStartPatch(MainMenuHandler __instance) {
        SettingsLoader.RegisterSettings();

        return true;
    }

    /// <summary>
    /// Patches the <see cref="SFX_Instance.Play"/> method to ensure that the correct mixer group is used.
    /// </summary>
    /// <param name="__instance">The instance of the <see cref="SFX_Instance"/>.</param>
    /// <returns>Whether the original method should be called.</returns>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(SFX_Instance), nameof(SFX_Instance.Play))]
    internal static bool PlayPatch(SFX_Instance __instance) {
        if (__instance.settings.mixerGroup != SingletonAsset<MixerHolder>.Instance.sfxMixer)
            ContentSettingsEntry.Logger.LogDebug($"Changing mixer group for {__instance.name}");

        __instance.settings.mixerGroup = SingletonAsset<MixerHolder>.Instance.sfxMixer;

        return true;
    }
}