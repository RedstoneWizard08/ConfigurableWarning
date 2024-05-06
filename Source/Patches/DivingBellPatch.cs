using System.Collections.Generic;
using System.Linq;
using ConfigurableWarning.Options;
using ConfigurableWarning.Settings;
using HarmonyLib;

namespace ConfigurableWarning.Patches {
    [HarmonyPatch]
    internal class DivingBellPatch {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(DivingBellDoor), nameof(DivingBellDoor.IsFullyClosed))]
        internal static void IsFullyClosed(DivingBellDoor __instance, ref bool __result) {
            __result = __result || !OptionsState.Instance.Get<bool>(BuiltInSettings.Keys.RequireDiveBellDoorClosed);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(DiveBellPlayerDetector), nameof(DiveBellPlayerDetector.CheckForPlayers))]
        internal static void CheckForPlayers(DiveBellPlayerDetector __instance, ref ICollection<Player> __result) {
            if (!OptionsState.Instance.Get<bool>(BuiltInSettings.Keys.RequireAllPlayersInDiveBell)) {
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

            var playersFoundInBell = __instance.playerDetector.CheckForPlayers();
            var players = PlayerHandler.instance.players;
            var allInside = players.All(player => playersFoundInBell.Contains(player));
            var notClosed = !__instance.door.IsFullyClosed() && OptionsState.Instance.Get<bool>(BuiltInSettings.Keys.RequireDiveBellDoorClosed);
            var notAllInside = !allInside && OptionsState.Instance.Get<bool>(BuiltInSettings.Keys.RequireAllPlayersInDiveBell);

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
