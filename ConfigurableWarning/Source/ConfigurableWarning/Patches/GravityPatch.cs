using ConfigurableWarning.API;
using ConfigurableWarning.Settings;
using HarmonyLib;

namespace ConfigurableWarning.Patches;

/// <summary>
///     Gravity Patches
/// </summary>
[HarmonyPatch]
public class GravityPatch {
    /// <summary>
    ///     Patches the player controller's update method to modify gravity.
    /// </summary>
    /// <param name="__instance">The current instance of the <see cref="PlayerController" />.</param>
    [HarmonyPatch(typeof(PlayerController), nameof(PlayerController.Update))]
    [HarmonyPostfix]
    public static void UpdateGravity(PlayerController __instance) {
        __instance.gravity = States.Floats[SettingKeys.Gravity];
    }
}
