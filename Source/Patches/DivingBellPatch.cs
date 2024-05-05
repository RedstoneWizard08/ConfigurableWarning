using System.Collections.Generic;
using HarmonyLib;

namespace ConfigurableWarning.Patches {
    [HarmonyPatch]
    internal class DivingBellPatch {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(DivingBellDoor), nameof(DivingBellDoor.IsFullyClosed))]
        internal static void IsFullyClosed(DivingBellDoor __instance, ref bool __result) {
            __result = __result || !Plugin.State.requireDiveBellDoorClosed;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(DiveBellPlayerDetector), nameof(DiveBellPlayerDetector.CheckForPlayers))]
        internal static void CheckForPlayers(DiveBellPlayerDetector __instance, ref ICollection<Player> __result) {
            if (!Plugin.State.requireAllPlayersInDiveBell) {
                __result = PlayerHandler.instance.players;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(DivingBell), nameof(DivingBell.Update))]
        internal static void Update(DivingBell __instance) {
            var recharging = __instance.onSurface && TimeOfDayHandler.TimeOfDay == TimeOfDay.Evening;

            if (recharging) {
                __instance.StateMachine.SwitchState<DivingBellRechargingState>();
                return;
            }

            var allInside = true;
            var playersFoundInBell = __instance.playerDetector.CheckForPlayers();
            var players = PlayerHandler.instance.players;

            foreach (Player player in players) {
                if (!playersFoundInBell.Contains(player)) {
                    allInside = false;
                    break;
                }
            }

            var notClosed = !__instance.door.IsFullyClosed() && Plugin.State.requireDiveBellDoorClosed;
            var notAllInside = !allInside && Plugin.State.requireAllPlayersInDiveBell;

            if (!notClosed) {
                __instance.opened = false;
            }
            
            if (__instance.onSurface) {
                if (notAllInside) {
                    __instance.StateMachine.SwitchState<DivingBellNotReadyMissingPlayersState>();
                } else if (notClosed) {
                    __instance.StateMachine.SwitchState<DivingBellNotReadyDoorOpenState>();
                } else {
                    __instance.StateMachine.SwitchState<DivingBellReadyState>();
                }
            } else {
                if (notClosed) {
                    __instance.StateMachine.SwitchState<DivingBellNotReadyDoorOpenState>();
                } else {
                    __instance.StateMachine.SwitchState<DivingBellReadyState>();
                }
            }
        }
    }
}
