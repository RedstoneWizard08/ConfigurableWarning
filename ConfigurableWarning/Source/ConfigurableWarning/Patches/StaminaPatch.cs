using ConfigurableWarning.API.State;
using ConfigurableWarning.Settings;
using HarmonyLib;
using UnityEngine;

namespace ConfigurableWarning.Patches;

/// <summary>
///     Stamina patches
/// </summary>
[HarmonyPatch]
public class StaminaPatch {
    /// <summary>
    ///     Patches the player's stamina when they spawn.
    /// </summary>
    /// <param name="__instance">The current instance of the <see cref="PlayerController" />.</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(PlayerController), nameof(PlayerController.Start))]
    public static void Start(ref PlayerController __instance) {
        Update(ref __instance);
        Player.localPlayer.data.currentStamina = States.Floats[SettingKeys.MaxStamina];
    }

    /// <summary>
    ///     Patches the player's stamina parameters.
    /// </summary>
    /// <param name="__instance">The current instance of the <see cref="PlayerController" />.</param>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(PlayerController), nameof(PlayerController.Update))]
    public static void Update(ref PlayerController __instance) {
        __instance.maxStamina = States.Floats[SettingKeys.MaxStamina];
        __instance.sprintMultiplier = States.Floats[SettingKeys.SprintSpeed];
        __instance.staminaRegRate = States.Floats[SettingKeys.StaminaRegenRate];
    }

    /// <summary>
    ///     Patches the player controller to reflect stamina settings.
    /// </summary>
    /// <param name="__instance">The current instance of the <see cref="PlayerController" />.</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(PlayerController), nameof(PlayerController.Update))]
    public static void UpdatePost(ref PlayerController __instance) {
        var data = Player.localPlayer.data;
        var max = States.Floats[SettingKeys.MaxStamina];
        var regen = States.Floats[SettingKeys.StaminaRegenRate];

        if (!(data.sinceSprint > 1f)) return;

        data.currentStamina = regen switch {
            > 1f => Mathf.MoveTowards(data.currentStamina, max, (regen - 1f) * Time.deltaTime),
            > 0f => Mathf.MoveTowards(data.currentStamina, 0f, (1f - regen) * Time.deltaTime),
            _ => data.currentStamina
        };
    }
}