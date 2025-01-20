using ConfigurableWarning.API.State;
using ContentLibrary.Settings;
using HarmonyLib;

namespace ContentLibrary.Patches;

/// <summary>
///     Quota patches
/// </summary>
[HarmonyPatch]
public class QuotaPatch {
    /// <summary>
    ///     Patches the days per quota to reflect the set value.
    ///     This patches the constructor of <see cref="RoomStatsHolder" />.
    /// </summary>
    /// <param name="__instance">The current instance of the <see cref="RoomStatsHolder" />.</param>
    /// <param name="handler">The current <see cref="SurfaceNetworkHandler" /></param>
    /// <param name="startMoney">The starting money</param>
    /// <param name="startQuotaToReachToReach">The starting quota to reach</param>
    /// <param name="daysPerQuota">The original days per quota (constructor arg)</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(RoomStatsHolder), MethodType.Constructor,
        [typeof(SurfaceNetworkHandler), typeof(int), typeof(int), typeof(int)])]
    public static void Constructor(RoomStatsHolder __instance, SurfaceNetworkHandler handler, int startMoney,
        int startQuotaToReachToReach, int daysPerQuota) {
        __instance.DaysPerQutoa = States.Ints[SettingKeys.DaysPerQuota];
    }

    /// <summary>
    ///     Patches the getter for days per quota.
    /// </summary>
    /// <param name="__instance">The current instance of the <see cref="RoomStatsHolder" />.</param>
    /// <param name="__result">The resulting value</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(RoomStatsHolder), nameof(RoomStatsHolder.DaysPerQutoa), MethodType.Getter)]
    public static void GetDaysPerQuota(RoomStatsHolder __instance, ref int __result) {
        __result = States.Ints[SettingKeys.DaysPerQuota];
    }
}