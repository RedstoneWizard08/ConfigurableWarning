using HarmonyLib;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ConfigurableWarning.Patches {
    [HarmonyPatch]
    internal class PlayerPatch {
        internal static float GetMaxOxygen() => Plugin.Instance.PluginConfig.maxOxygen.Value;
        internal static float GetMaxHealth() => Plugin.Instance.PluginConfig.maxHealth.Value;

        internal static bool tmp_UsingOxygen;

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player), "CheckOxygen")]
        internal static bool CheckOxygen(Player __instance) {
            var flag = SceneManager.GetActiveScene().name == "SurfaceScene" && !Plugin.Instance.PluginConfig.useOxygenOnSurface.Value;

            __instance.data.usingOxygen = !flag;

            if (flag && Plugin.Instance.PluginConfig.refillOxygenOnSurface.Value) {
                // __instance.data.remainingOxygen = __instance.data.maxOxygen;
                __instance.data.remainingOxygen += Time.deltaTime * Plugin.Instance.PluginConfig.oxygenRefillRate.Value;
            }

            if (__instance.ai) {
                __instance.data.usingOxygen = false;
            }

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player.PlayerData), "UpdateValues")]
        internal static bool UpdateValuesPre(Player.PlayerData __instance) {
            CheckOxygen(__instance.player);

            tmp_UsingOxygen = __instance.usingOxygen;
            Player.PlayerData.maxHealth = GetMaxHealth();

            __instance.maxOxygen = GetMaxOxygen();
            __instance.usingOxygen = false;

            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Player.PlayerData), "UpdateValues")]
        internal static void UpdateValuesPost(Player.PlayerData __instance) {
            __instance.usingOxygen = tmp_UsingOxygen;

            if (__instance.usingOxygen && !(__instance.isInDiveBell && !Plugin.Instance.PluginConfig.useOxygenInDiveBell.Value)) {
                var mul = (__instance.isSprinting ? Plugin.Instance.PluginConfig.sprintMultiplier.Value : 1.0f) * Plugin.Instance.PluginConfig.oxygenUsageMultiplier.Value;

                __instance.remainingOxygen -= Time.deltaTime * mul;

                if (__instance.remainingOxygen < __instance.maxOxygen * 0.5f && PhotonNetwork.IsMasterClient && PhotonGameLobbyHandler.CurrentObjective is FilmSomethingScaryObjective) {
                    PhotonGameLobbyHandler.Instance.SetCurrentObjective(new ReturnToTheDiveBellObjective());
                }
            }
        }
    }
}
