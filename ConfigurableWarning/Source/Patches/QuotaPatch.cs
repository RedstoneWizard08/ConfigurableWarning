using ConfigurableWarning.API;
using ConfigurableWarning.Settings;
using HarmonyLib;

namespace ConfigurableWarning.Patches;

internal static class QuotaPatch {
    [HarmonyPostfix]
    [HarmonyPatch(typeof(RoomStatsHolder), MethodType.Constructor,
        [typeof(SurfaceNetworkHandler), typeof(int), typeof(int), typeof(int)])]
    internal static void Constructor(RoomStatsHolder __instance, SurfaceNetworkHandler handler, int startMoney,
        int startQuotaToReachToReach, int daysPerQuota) {
        __instance.DaysPerQutoa = OptionsState.Instance.Get<int>(BuiltInSettings.Keys.DaysPerQuota);
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(RoomStatsHolder), nameof(RoomStatsHolder.DaysPerQutoa), MethodType.Getter)]
    internal static void GetDaysPerQuota(RoomStatsHolder __instance, ref int __result) {
        __result = OptionsState.Instance.Get<int>(BuiltInSettings.Keys.DaysPerQuota);
    }
}