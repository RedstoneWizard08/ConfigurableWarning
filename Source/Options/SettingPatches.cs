using HarmonyLib;
using Zorro.Settings;

using IntSetting = ContentSettings.API.Settings.IntSetting;

namespace ConfigurableWarning.Options {
    [HarmonyPatch]
    internal class SettingPatches {
        private static float _originalFloatValue;
        private static int _originalIntValue;

        [HarmonyPrefix]
        [HarmonyPatch(typeof(FloatSetting), nameof(FloatSetting.SetValue))]
        internal static void SetValueFloatPre(ref FloatSetting __instance, float value, ISettingHandler handler) {
            _originalFloatValue = value;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(FloatSetting), nameof(FloatSetting.SetValue))]
        internal static void SetValueFloat(ref FloatSetting __instance, float value, ISettingHandler handler) {
            if (__instance is not FloatOption opt) return;
            
            __instance.Value = opt.ShouldClamp ? __instance.Clamp(_originalFloatValue) : _originalFloatValue;
            __instance.ApplyValue();
            handler.SaveSetting(__instance);
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(IntSetting), nameof(IntSetting.SetValue))]
        internal static void SetValueIntPre(ref IntSetting __instance, int newValue, ISettingHandler settingHandler) {
            _originalIntValue = newValue;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(IntSetting), nameof(IntSetting.SetValue))]
        internal static void SetValueInt(ref IntSetting __instance, int newValue, ISettingHandler settingHandler) {
            if (__instance is IntOption opt) {
                __instance.Value = opt.ShouldClamp ? __instance.Clamp(_originalIntValue) : _originalIntValue;
                __instance.ApplyValue();
                settingHandler.SaveSetting(__instance);
            }
        }
    }
}