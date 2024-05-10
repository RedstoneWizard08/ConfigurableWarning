using ConfigurableWarning.API.Options;
using ContentSettings.API;
using HarmonyLib;
using Zorro.Settings;
using IntSetting = ContentSettings.API.Settings.IntSetting;

namespace ConfigurableWarning.API;

/// <summary>
/// Settings patches
/// </summary>
[HarmonyPatch]
public class SettingPatches {
    /// <summary>
    /// The held value for a <see cref="FloatSetting" />.
    /// </summary>
    private static float _originalFloatValue;

    /// <summary>
    /// The held value for an <see cref="IntSetting" />.
    /// </summary>
    private static int _originalIntValue;

    /// <summary>
    /// Patches the <see cref="FloatSetting.SetValue(float, ISettingHandler)" /> method
    /// to capture the value.
    /// </summary>
    /// <param name="__instance">The current instance of a <see cref="FloatSetting" />.</param>
    /// <param name="value">The value</param>
    /// <param name="handler">The settings handler</param>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(FloatSetting), nameof(FloatSetting.SetValue))]
    public static void SetValueFloatPre(ref FloatSetting __instance, float value, ISettingHandler handler) {
        _originalFloatValue = value;
    }

    /// <summary>
    /// Patches the <see cref="FloatSetting.SetValue(float, ISettingHandler)" /> method
    /// to not clamp if it's disabled.
    /// </summary>
    /// <param name="__instance">The current instance of a <see cref="FloatSetting" />.</param>
    /// <param name="value">The value</param>
    /// <param name="handler">The settings handler</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(FloatSetting), nameof(FloatSetting.SetValue))]
    public static void SetValueFloat(ref FloatSetting __instance, float value, ISettingHandler handler) {
        if (__instance is not FloatOption opt) return;

        __instance.Value = opt._shouldClamp ? __instance.Clamp(_originalFloatValue) : _originalFloatValue;
        __instance.ApplyValue();
        handler.SaveSetting(__instance);
    }

    /// <summary>
    /// Patches the <see cref="IntSetting.SetValue(int, ISettingHandler)" /> method
    /// to capture the value.
    /// </summary>
    /// <param name="__instance">The current instance of an <see cref="IntSetting" />.</param>
    /// <param name="newValue">The value</param>
    /// <param name="settingHandler">The settings handler</param>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(IntSetting), nameof(IntSetting.SetValue))]
    public static void SetValueIntPre(ref IntSetting __instance, int newValue, ISettingHandler settingHandler) {
        _originalIntValue = newValue;
    }

    /// <summary>
    /// Patches the <see cref="IntSetting.SetValue(int, ISettingHandler)" /> method
    /// to not clamp if it's disabled.
    /// </summary>
    /// <param name="__instance">The current instance of an <see cref="IntSetting" />.</param>
    /// <param name="newValue">The value</param>
    /// <param name="settingHandler">The settings handler</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(IntSetting), nameof(IntSetting.SetValue))]
    public static void SetValueInt(ref IntSetting __instance, int newValue, ISettingHandler settingHandler) {
        if (__instance is not IntOption opt) return;

        __instance.Value = opt._shouldClamp ? __instance.Clamp(_originalIntValue) : _originalIntValue;
        __instance.ApplyValue();
        settingHandler.SaveSetting(__instance);
    }

    /// <summary>
    /// Patches <see cref="SettingsLoader.RegisterSettings" /> to register our settings
    /// too.
    /// </summary>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(SettingsLoader), nameof(SettingsLoader.RegisterSettings))]
    public static void RegisterOptions() {
        OptionLoader.RegisterOptions();
    }
}