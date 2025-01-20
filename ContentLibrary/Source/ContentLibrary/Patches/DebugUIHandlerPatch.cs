using ConfigurableWarning.API.State;
using ContentLibrary.Settings;
using HarmonyLib;
using UnityEngine;
using Zorro.Core.CLI;

namespace ContentLibrary.Patches;

/// <summary>
///     Patches for the DebugUIHandler class.
/// </summary>
[HarmonyPatch]
public class DebugUIHandlerPatch {
    /// <summary>
    ///     Postfix for the Update method in DebugUIHandler.
    /// </summary>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(DebugUIHandler), nameof(DebugUIHandler.Update))]
    public static void UpdatePostfix(DebugUIHandler __instance) {
        if (!Input.GetKeyDown(States.Keys[SettingKeys.DebugUIButton])) return;

        if (__instance.IsOpen) __instance.Hide();
        else __instance.Show();
    }
}