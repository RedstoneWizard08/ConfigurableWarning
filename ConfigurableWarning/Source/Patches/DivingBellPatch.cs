using System.Collections.Generic;
using System.Linq;
using ConfigurableWarning.API;
using ConfigurableWarning.Settings;
using HarmonyLib;

namespace ConfigurableWarning.Patches;

/// <summary>
///     Diving Bell patches
/// </summary>
[HarmonyPatch]
public class DivingBellPatch {
    /// <summary>
    ///     Patches the diving bell to allow you to leave without your friends.
    /// </summary>
    /// <param name="__instance">The current instance of the <see cref="DivingBellDoor" />.</param>
    /// <param name="__result"></param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(DivingBellDoor), nameof(DivingBellDoor.IsFullyClosed))]
    public static void IsFullyClosed(DivingBellDoor __instance, ref bool __result) {
        __result = __result || !States.Bools[SettingKeys.RequireDiveBellDoorClosed];
    }

    /// <summary>
    ///     Patches the player checker to allow for all players to not need to be
    ///     inside.
    /// </summary>
    /// <param name="__instance">The current instance of the <see cref="DiveBellPlayerDetector" />.</param>
    /// <param name="__result">The players "inside" the diving bell</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(DiveBellPlayerDetector), nameof(DiveBellPlayerDetector.CheckForPlayers))]
    public static void CheckForPlayers(DiveBellPlayerDetector __instance, ref ICollection<Player> __result) {
        if (!States.Bools[SettingKeys.RequireAllPlayersInDiveBell])
            __result = PlayerHandler.instance.players;
    }

    /// <summary>
    ///     Patches the diving bell's update function to apply our settings
    /// </summary>
    /// <param name="__instance">The current instance of the <see cref="DivingBell" />.</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(DivingBell), nameof(DivingBell.Update))]
    public static void Update(DivingBell __instance) {
        var recharging = __instance.onSurface && TimeOfDayHandler.TimeOfDay == TimeOfDay.Evening;

        if (recharging) {
            __instance.StateMachine.SwitchState<DivingBellRechargingState>();
            return;
        }

        var playersFoundInBell = __instance.playerDetector.CheckForPlayers();
        var players = PlayerHandler.instance.players;
        var allInside = players.All(player => playersFoundInBell.Contains(player));
        var notClosed = !__instance.door.IsFullyClosed() && States.Bools[SettingKeys.RequireDiveBellDoorClosed];
        var notAllInside = !allInside && States.Bools[SettingKeys.RequireAllPlayersInDiveBell];

        if (!notClosed) __instance.opened = false;

        if (__instance.onSurface) {
            if (notAllInside)
                __instance.StateMachine.SwitchState<DivingBellNotReadyMissingPlayersState>();
            else if (notClosed)
                __instance.StateMachine.SwitchState<DivingBellNotReadyDoorOpenState>();
            else
                __instance.StateMachine.SwitchState<DivingBellReadyState>();
        } else {
            if (notClosed)
                __instance.StateMachine.SwitchState<DivingBellNotReadyDoorOpenState>();
            else
                __instance.StateMachine.SwitchState<DivingBellReadyState>();
        }
    }
}