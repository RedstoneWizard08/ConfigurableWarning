using HarmonyLib;

namespace ConfigurableWarning.Patches {
    internal class QuotaPatch {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(RoomStatsHolder), MethodType.Constructor, [typeof(SurfaceNetworkHandler), typeof(int), typeof(int), typeof(int)])]
        internal static void Constructor(RoomStatsHolder __instance, SurfaceNetworkHandler handler, int startMoney, int startQuotaToReachToReach, int daysPerQuota) {
            AccessTools.Property(typeof(RoomStatsHolder), "DaysPerQutoa").SetValue(__instance, Plugin.Instance.PluginConfig.daysPerQuota.Value);
        }
    }
}
