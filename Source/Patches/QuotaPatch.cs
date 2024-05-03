using HarmonyLib;

namespace ConfigurableWarning.Patches {
    internal class QuotaPatch {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(RoomStatsHolder), MethodType.Constructor, [typeof(SurfaceNetworkHandler), typeof(int), typeof(int), typeof(int)])]
        internal static void Constructor(RoomStatsHolder __instance, SurfaceNetworkHandler handler, int startMoney, int startQuotaToReachToReach, int daysPerQuota) {
            __instance.DaysPerQutoa = Plugin.State.daysPerQuota;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(RoomStatsHolder), nameof(RoomStatsHolder.DaysPerQutoa), MethodType.Getter)]
        internal static void GetDaysPerQuota(RoomStatsHolder __instance, ref int __result) {
            __result = Plugin.State.daysPerQuota;
        }
    }
}
