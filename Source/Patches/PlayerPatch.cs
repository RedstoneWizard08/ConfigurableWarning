﻿using HarmonyLib;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ConfigurableWarning.Patches {
    [HarmonyPatch]
    internal class PlayerPatch {
        internal static float GetMaxOxygen() => Plugin.State.maxOxygen;
        internal static float GetMaxHealth() => Plugin.State.maxHealth;

        internal static bool tmp_UsingOxygen;

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player), nameof(Player.CheckOxygen))]
        internal static bool CheckOxygen(Player __instance) {
            var flag = SceneManager.GetActiveScene().name == "SurfaceScene" && !Plugin.State.useOxygenOnSurface;

            __instance.data.usingOxygen = !flag;

            if (flag && Plugin.State.refillOxygenOnSurface) {
                // __instance.data.remainingOxygen = __instance.data.maxOxygen;
                __instance.data.remainingOxygen += Time.deltaTime * Plugin.State.oxygenRefillRate;
            }

            if (__instance.ai) {
                __instance.data.usingOxygen = false;
            }

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player.PlayerData), nameof(Player.PlayerData.UpdateValues))]
        internal static bool UpdateValuesPre(Player.PlayerData __instance) {
            CheckOxygen(__instance.player);

            tmp_UsingOxygen = __instance.usingOxygen;
            Player.PlayerData.maxHealth = Plugin.State.maxHealth;

            __instance.maxOxygen = Plugin.State.maxOxygen;
            __instance.usingOxygen = false;

            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Player.PlayerData), nameof(Player.PlayerData.UpdateValues))]
        internal static void UpdateValuesPost(Player.PlayerData __instance) {
            __instance.usingOxygen = tmp_UsingOxygen;

            if (__instance.usingOxygen && !(__instance.isInDiveBell && !Plugin.State.useOxygenInDiveBell)) {
                var mul = (__instance.isSprinting ? Plugin.State.sprintUsage : 1.0f) * Plugin.State.oxygenUsage;

                __instance.remainingOxygen -= Time.deltaTime * mul;

                if (__instance.remainingOxygen < __instance.maxOxygen * 0.5f && PhotonNetwork.IsMasterClient && PhotonGameLobbyHandler.CurrentObjective is FilmSomethingScaryObjective) {
                    PhotonGameLobbyHandler.Instance.SetCurrentObjective(new ReturnToTheDiveBellObjective());
                }
            }
        }
    }
}
