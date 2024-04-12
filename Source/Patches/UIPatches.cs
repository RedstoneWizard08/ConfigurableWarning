using HarmonyLib;

namespace ConfigurableWarning.Patches {
    [HarmonyPatch]
    internal class UIPatches {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(UI_Health), nameof(UI_Health.Update))]
        internal static void UpdateHealth(UI_Health __instance) {
            __instance.fill.fillAmount = Player.localPlayer.data.health / Plugin.Instance.PluginConfig.maxHealth.Value;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(UI_DaysLeft), nameof(UI_DaysLeft.Update))]
        internal static void UpdateDaysLeft(UI_DaysLeft __instance) {
            int num = Plugin.Instance.PluginConfig.daysPerQuota.Value - SurfaceNetworkHandler.RoomStats.CurrentQuotaDay + 1;

            __instance.text.text = (num == 1) ? __instance.m_LastDayText : __instance.m_DaysLeftText.Replace("{0}", num.ToString());
        }
    }
}
