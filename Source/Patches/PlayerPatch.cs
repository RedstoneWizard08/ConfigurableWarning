using ConfigurableWarning.Options;
using ConfigurableWarning.Settings;
using HarmonyLib;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ConfigurableWarning.Patches {
    [HarmonyPatch]
    internal class PlayerPatch {
        internal static float GetMaxOxygen() => OptionsState.Instance.Get<float>(BuiltInSettings.Keys.Oxygen);
        internal static float GetMaxHealth() => OptionsState.Instance.Get<float>(BuiltInSettings.Keys.Health);

        private static bool _tmpUsingOxygen;

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player), nameof(Player.CheckOxygen))]
        internal static bool CheckOxygen(Player __instance) {
            if (OptionsState.Instance.Get<bool>(BuiltInSettings.Keys.InfiniteOxygen)) {
                __instance.data.remainingOxygen = OptionsState.Instance.Get<float>(BuiltInSettings.Keys.Oxygen);
                return false;
            }

            var isSurface = SceneManager.GetActiveScene().name == "SurfaceScene";
            var flag = isSurface && !OptionsState.Instance.Get<bool>(BuiltInSettings.Keys.UseOxygenOnSurface);

            __instance.data.usingOxygen = !flag;

            if (isSurface && OptionsState.Instance.Get<bool>(BuiltInSettings.Keys.RefillOxygenOnSurface)) {
                __instance.data.remainingOxygen += Time.deltaTime * OptionsState.Instance.Get<float>(BuiltInSettings.Keys.OxygenRefillRate);
            }

            if (__instance.ai) {
                __instance.data.usingOxygen = false;
            }

            if (__instance.data.remainingOxygen > OptionsState.Instance.Get<float>(BuiltInSettings.Keys.Oxygen)) {
                __instance.data.remainingOxygen = OptionsState.Instance.Get<float>(BuiltInSettings.Keys.Oxygen);
            }

            if (__instance.data.remainingOxygen < 0) {
                __instance.data.remainingOxygen = 0;
            }

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player.PlayerData), nameof(Player.PlayerData.UpdateValues))]
        internal static bool UpdateValuesPre(Player.PlayerData __instance) {
            CheckOxygen(__instance.player);

            _tmpUsingOxygen = __instance.usingOxygen;
            Player.PlayerData.maxHealth = OptionsState.Instance.Get<float>(BuiltInSettings.Keys.Health);

            __instance.maxOxygen = OptionsState.Instance.Get<float>(BuiltInSettings.Keys.Oxygen);

            // We want to override this functionality with our own code, but
            // preserve the rest of the method.
            __instance.usingOxygen = false;

            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Player.PlayerData), nameof(Player.PlayerData.UpdateValues))]
        internal static void UpdateValuesPost(Player.PlayerData __instance) {
            __instance.usingOxygen = _tmpUsingOxygen;

            if (__instance.usingOxygen && !(__instance.isInDiveBell && !OptionsState.Instance.Get<bool>(BuiltInSettings.Keys.UseOxygenInDiveBell))) {
                var mul = (__instance.isSprinting ? OptionsState.Instance.Get<float>(BuiltInSettings.Keys.SprintMultiplier) : 1.0f) * OptionsState.Instance.Get<float>(BuiltInSettings.Keys.OxygenUsageMultiplier);

                __instance.remainingOxygen -= Time.deltaTime * mul;

                if (__instance.remainingOxygen < OptionsState.Instance.Get<float>(BuiltInSettings.Keys.Oxygen) * 0.5f && PhotonNetwork.IsMasterClient && PhotonGameLobbyHandler.CurrentObjective is FilmSomethingScaryObjective) {
                    PhotonGameLobbyHandler.Instance.SetCurrentObjective(new ReturnToTheDiveBellObjective());
                }
            }
        }
    }
}
