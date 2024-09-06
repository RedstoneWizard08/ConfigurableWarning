using ConfigurableWarning.API;
using ConfigurableWarning.Settings;
using HarmonyLib;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ConfigurableWarning.Patches;

/// <summary>
///     Player &amp; PlayerData patch
/// </summary>
[HarmonyPatch]
public class PlayerPatch {
    private static bool _tmpUsingOxygen;

    /// <summary>
    ///     Internal max oxygen value.
    /// </summary>
    /// <returns>The custom max oxygen value.</returns>
    public static float GetMaxOxygen() {
        return States.Floats[SettingKeys.Oxygen];
    }

    /// <summary>
    ///     Internal max health value.
    /// </summary>
    /// <returns>The custom max health value.</returns>
    public static float GetMaxHealth() {
        return States.Floats[SettingKeys.Health];
    }

    /// <summary>
    ///     Patches the CheckOxygen value to add our custom stuff.
    /// </summary>
    /// <param name="__instance">The current instance of a <see cref="Player" />.</param>
    /// <returns><see cref="HarmonyPrefix" /> continue-or-not flag (this is a prefix)</returns>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Player), nameof(Player.CheckOxygen))]
    public static bool CheckOxygen(Player __instance) {
        if (States.Bools[SettingKeys.InfiniteOxygen]) {
            __instance.data.remainingOxygen = States.Floats[SettingKeys.Oxygen];
            return false;
        }

        var isSurface = SceneManager.GetActiveScene().name == "SurfaceScene";
        var flag = isSurface && !States.Bools[SettingKeys.UseOxygenOnSurface];

        __instance.data.usingOxygen = !flag;

        if (isSurface && States.Bools[SettingKeys.RefillOxygenOnSurface])
            __instance.data.remainingOxygen +=
                Time.deltaTime * States.Floats[SettingKeys.OxygenRefillRate];

        if (__instance.ai) __instance.data.usingOxygen = false;

        if (__instance.data.remainingOxygen > States.Floats[SettingKeys.Oxygen])
            __instance.data.remainingOxygen = States.Floats[SettingKeys.Oxygen];

        if (__instance.data.remainingOxygen < 0) __instance.data.remainingOxygen = 0;

        return false;
    }

    /// <summary>
    ///     Stores the current usingOxygen flag and modifies a few more things.
    /// </summary>
    /// <param name="__instance">The current instance of a <see cref="Player.PlayerData" />.</param>
    /// <returns><see cref="HarmonyPrefix" /> continue-or-not flag (this is a prefix)</returns>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Player.PlayerData), nameof(Player.PlayerData.UpdateValues))]
    public static bool UpdateValuesPre(Player.PlayerData __instance) {
        CheckOxygen(__instance.player);

        _tmpUsingOxygen = __instance.usingOxygen;
        Player.PlayerData.maxHealth = States.Floats[SettingKeys.Health];
        __instance.maxOxygen = States.Floats[SettingKeys.Oxygen];

        // We want to override this functionality with our own code, but
        // preserve the rest of the method.
        __instance.usingOxygen = false;

        return true;
    }

    /// <summary>
    ///     Patches the UpdateValues method to include our custom oxygen code
    /// </summary>
    /// <param name="__instance">The current instance of a <see cref="Player.PlayerData" />.</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(Player.PlayerData), nameof(Player.PlayerData.UpdateValues))]
    public static void UpdateValuesPost(Player.PlayerData __instance) {
        __instance.usingOxygen = _tmpUsingOxygen;

        if (!__instance.usingOxygen || (__instance.isInDiveBell && !States.Bools[SettingKeys.UseOxygenInDiveBell]))
            return;

        var mul = (__instance.isSprinting
            ? States.Floats[SettingKeys.SprintMultiplier]
            : 1.0f) * States.Floats[SettingKeys.OxygenUsageMultiplier];

        __instance.remainingOxygen -= Time.deltaTime * mul;

        if (__instance.remainingOxygen < States.Floats[SettingKeys.Oxygen] * 0.5f &&
            PhotonNetwork.IsMasterClient &&
            PhotonGameLobbyHandler.CurrentObjective is FilmSomethingScaryObjective)
            PhotonGameLobbyHandler.Instance.SetCurrentObjective(new ReturnToTheDiveBellObjective());
    }
}