using ConfigurableWarning.Options;
using ConfigurableWarning.Settings;
using HarmonyLib;

namespace ConfigurableWarning.Patches {
    [HarmonyPatch]
    internal class UIPatches {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(UI_Health), nameof(UI_Health.Update))]
        internal static void UpdateHealth(UI_Health __instance) {
            __instance.fill.fillAmount = Player.localPlayer.data.health / OptionsState.Instance.Get<float>(BuiltInSettings.Keys.Health);
        }
    }
}
