using ConfigurableWarning.Settings.Objects;
using HarmonyLib;
using Zorro.Settings;

namespace ConfigurableWarning.Patches {
    [HarmonyPatch]
    internal class TextSettingPatch {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(KeyCodeSettingUI), nameof(KeyCodeSettingUI.Setup))]
        internal static void Setup(KeyCodeSettingUI __instance, Setting setting, ISettingHandler settingHandler) {
            if (setting is TextSetting) {
                __instance.gameObject.SetActive(false);
                __instance.label.alignment = TMPro.TextAlignmentOptions.Center;
            }
        }
    }
}