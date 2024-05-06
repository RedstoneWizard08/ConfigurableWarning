using ConfigurableWarning.Options;
using ConfigurableWarning.Settings;
using HarmonyLib;
using UnityEngine;

namespace ConfigurableWarning.Patches {
    [HarmonyPatch]
    internal class StaminaPatch {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayerController), nameof(PlayerController.Start))]
        internal static void Start(ref PlayerController __instance) {
            Update(ref __instance);
            Player.localPlayer.data.currentStamina = OptionsState.Instance.Get<float>(BuiltInSettings.Keys.MaxStamina);
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(PlayerController), nameof(PlayerController.Update))]
        internal static void Update(ref PlayerController __instance) {
            __instance.maxStamina = OptionsState.Instance.Get<float>(BuiltInSettings.Keys.MaxStamina);
            __instance.sprintMultiplier = OptionsState.Instance.Get<float>(BuiltInSettings.Keys.SprintMultiplier);
            __instance.staminaRegRate = OptionsState.Instance.Get<float>(BuiltInSettings.Keys.StaminaRegenRate);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayerController), nameof(PlayerController.Update))]
        internal static void UpdatePost(ref PlayerController __instance) {
            var data = Player.localPlayer.data;
            var max = OptionsState.Instance.Get<float>(BuiltInSettings.Keys.MaxStamina);
            var regen = OptionsState.Instance.Get<float>(BuiltInSettings.Keys.StaminaRegenRate);

            if (data.sinceSprint > 1f) {
                if (regen > 1f) {
                    data.currentStamina = Mathf.MoveTowards(data.currentStamina, max, (regen - 1f) * Time.deltaTime);
                } else if (regen > 0f) {
                    data.currentStamina = Mathf.MoveTowards(data.currentStamina, 0f, (1f - regen) * Time.deltaTime);
                }
            }
        }
    }
}
