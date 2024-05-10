using ConfigurableWarning.API;
using ConfigurableWarning.Settings;
using HarmonyLib;

namespace ConfigurableWarning.Patches;

/// <summary>
///     HUD/UI patches
/// </summary>
[HarmonyPatch]
public class UIPatches {
    /// <summary>
    ///     Patches the health bar to accurately reflect the player's max health,
    ///     as it's normally constant.
    /// </summary>
    /// <param name="__instance">The current instance of the <see cref="UI_Health" />.</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(UI_Health), nameof(UI_Health.Update))]
    public static void UpdateHealth(UI_Health __instance) {
        __instance.fill.fillAmount = Player.localPlayer.data.health / States.Floats[SettingKeys.Health];
    }
}