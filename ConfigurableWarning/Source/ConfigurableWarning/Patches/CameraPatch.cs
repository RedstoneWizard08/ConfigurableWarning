using ConfigurableWarning.API;
using ConfigurableWarning.Settings;
using HarmonyLib;

namespace ConfigurableWarning.Patches;

/// <summary>
///     Patches for the camera.
/// </summary>
[HarmonyPatch]
public class CameraPatch {
    /// <summary>
    ///     Updates the camera's field of view.
    /// </summary>
    /// <param name="__instance">The current instance of a <see cref="MainCamera" />.</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(MainCamera), nameof(MainCamera.Update))]
    public static void Update(MainCamera __instance) {
        __instance.baseFOV = States.Floats[SettingKeys.Fov];
    }
}