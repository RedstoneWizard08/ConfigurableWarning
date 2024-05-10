using ConfigurableWarning.API;
using ConfigurableWarning.Settings;
using HarmonyLib;
using UnityEngine;

namespace ConfigurableWarning.Patches;

/// <summary>
/// Stamina patches
/// </summary>
[HarmonyPatch]
public class StaminaPatch {
    /// <summary>
    /// Patches the player's stamina when they spawn.
    /// </summary>
    /// <param name="__instance">The current instance of the <see cref="PlayerController" />.</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(PlayerController), nameof(PlayerController.Start))]
    public static void Start(ref PlayerController __instance) {
        Update(ref __instance);
        Player.localPlayer.data.currentStamina = OptionsState.Instance.Get<float>(BuiltInSettings.Keys.MaxStamina);
    }

    /// <summary>
    /// Patches the player's stamina parameters.
    /// </summary>
    /// <param name="__instance">The current instance of the <see cref="PlayerController" />.</param>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(PlayerController), nameof(PlayerController.Update))]
    public static void Update(ref PlayerController __instance) {
        __instance.maxStamina = OptionsState.Instance.Get<float>(BuiltInSettings.Keys.MaxStamina);
        __instance.sprintMultiplier = OptionsState.Instance.Get<float>(BuiltInSettings.Keys.SprintMultiplier);
        __instance.staminaRegRate = OptionsState.Instance.Get<float>(BuiltInSettings.Keys.StaminaRegenRate);
    }

    /// <summary>
    /// Patches the player controller to reflect stamina settings.
    /// </summary>
    /// <param name="__instance">The current instance of the <see cref="PlayerController" />.</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(PlayerController), nameof(PlayerController.Update))]
    public static void UpdatePost(ref PlayerController __instance) {
        var data = Player.localPlayer.data;
        var max = OptionsState.Instance.Get<float>(BuiltInSettings.Keys.MaxStamina);
        var regen = OptionsState.Instance.Get<float>(BuiltInSettings.Keys.StaminaRegenRate);

        if (!(data.sinceSprint > 1f)) return;

        data.currentStamina = regen switch {
            > 1f => Mathf.MoveTowards(data.currentStamina, max, (regen - 1f) * Time.deltaTime),
            > 0f => Mathf.MoveTowards(data.currentStamina, 0f, (1f - regen) * Time.deltaTime),
            _ => data.currentStamina
        };
    }
}