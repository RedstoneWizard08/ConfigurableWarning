#pragma warning disable IDE0060

using ConfigurableWarning.API.State;
using ConfigurableWarning.Settings;
using HarmonyLib;

namespace ConfigurableWarning.Patches;

/// <summary>
///     Patches the <see cref="MetaProgressionHandler" /> to allow for free
///     purchases based on config.
/// </summary>
[HarmonyPatch]
public class MetaProgressionPatch {
    [HarmonyPatch(typeof(MetaProgressionHandler), nameof(MetaProgressionHandler.CanAffordPurchase))]
    [HarmonyPrefix]
    private static bool PatchCanAfford(int amount, ref bool __result) {
        if (States.Bools[SettingKeys.FreeMetaCoins]) {
            __result = true;
            return false;
        }

        return true;
    }
}
