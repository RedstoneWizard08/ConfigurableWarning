#pragma warning disable IDE0060

using ConfigurableWarning.API.Options;
using HarmonyLib;
using Zorro.Settings;

namespace ConfigurableWarning.Patches;

/// <summary>
///     Settings patches
/// </summary>
[HarmonyPatch]
public class SettingPatches {
    /// <summary>
    ///     The held value for a <see cref="FloatSetting" />.
    /// </summary>
    private static float _originalFloatValue;

    /// <summary>
    ///     Patches the <see cref="FloatSetting.SetValue(float, ISettingHandler)" /> method
    ///     to capture the value.
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
    ///     Patches the <see cref="FloatSetting.SetValue(float, ISettingHandler)" /> method
    ///     to not clamp if it's disabled.
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
}