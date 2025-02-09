using ConfigurableWarning.API.State;
using ContentLibrary.Settings;
using HarmonyLib;

namespace ContentLibrary.Patches;

/// <summary>
///     Intro Screen Patches
/// </summary>
[HarmonyPatch]
public class IntroScreenPatch {
    [HarmonyPatch(typeof(IntroScreenAnimator), nameof(IntroScreenAnimator.Start))]
    [HarmonyPrefix]
    private static void StartPatch(IntroScreenAnimator __instance) {
        if (!States.Bools[SettingKeys.SkipIntroScreen]) return;

        __instance.skipping = true;
        __instance.m_animator.enabled = false;
    }
}