using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using ConfigurableWarning.API.State;
using ConfigurableWarning.Settings;
using HarmonyLib;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace ConfigurableWarning.Patches;

/// <summary>
///		Player customization patches.
/// </summary>
[HarmonyPatch]
public class CustomizationPatches {
    [HarmonyPatch(typeof(PlayerCustomizer), nameof(PlayerCustomizer.RunTerminal))]
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> RunTerminalTranspiler(IEnumerable<CodeInstruction> instructions) {
        List<CodeInstruction> list = [..instructions];
    
        foreach (var t in list.Where(t => t.opcode == OpCodes.Ldc_I4_3)) {
            t.opcode = OpCodes.Ldc_I4;
            t.operand = States.Ints[SettingKeys.FaceCharLimit];
    
            break;
        }
    
        return list.AsEnumerable();
    }
    
    [HarmonyPatch(typeof(PlayerVisor), nameof(PlayerVisor.SafetyCheckVisorText))]
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> RunSafetyCheckTranspiler(IEnumerable<CodeInstruction> instructions) {
        List<CodeInstruction> list = [..instructions];
    
        foreach (var t in list.Where(t => t.opcode == OpCodes.Ldc_I4_3)) {
            t.opcode = OpCodes.Ldc_I4;
            t.operand = States.Ints[SettingKeys.FaceCharLimit];
    
            break;
        }
    
        return list.AsEnumerable();
    }

    [HarmonyPatch(typeof(PlayerCustomizer), nameof(PlayerCustomizer.Update))]
    [HarmonyPostfix]
    private static void UpdatePostfix(PlayerCustomizer __instance, TextMeshProUGUI ___faceText, PhotonView ___view_g) {
        if (Input.GetKey(KeyCode.Delete)) {
            ___view_g.RPC("RCP_SetFaceText", RpcTarget.All, "");
        } else if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.V)) {
            var systemCopyBuffer = GUIUtility.systemCopyBuffer;
            ___view_g.RPC("RCP_SetFaceText", RpcTarget.All, systemCopyBuffer);
        } else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            var text = ___faceText.text;
            var text2 = text + "\n";
            ___view_g.RPC("RCP_SetFaceText", RpcTarget.All, text2);
        }
    }

    [HarmonyPatch(typeof(PlayerCustomizer), nameof(PlayerCustomizer.RunTerminal))]
    [HarmonyPostfix]
    private static void PostfixRunTerminal(PlayerCustomizer __instance) {
        if (__instance.faceText != null) {
            __instance.faceText.enableAutoSizing = States.Bools[SettingKeys.FaceAutoSizing];
            __instance.faceText.fontSizeMin = States.Floats[SettingKeys.FaceMinFont];
            __instance.faceText.fontSizeMax = States.Floats[SettingKeys.FaceMaxFont];
        }
    }

    [HarmonyPatch(typeof(PlayerCustomizer), nameof(PlayerCustomizer.RCP_SetFaceText))]
    [HarmonyPostfix]
    private static void PostfixSetFaceText(ref PlayerCustomizer __instance, string text) {
        if (!__instance.playerInTerminal) return;
        
        Debug.Log("Patching SetFaceText with full text: " + text);

        __instance.faceText.text = text;
        __instance.playerInTerminal.refs.visor.visorFaceText.text = text;

        if (__instance.faceText == null) return;
        
        __instance.faceText.enableAutoSizing = States.Bools[SettingKeys.FaceAutoSizing];
        __instance.faceText.fontSizeMin = States.Floats[SettingKeys.FaceMinFont];
        __instance.faceText.fontSizeMax = States.Floats[SettingKeys.FaceMaxFont];
    }

    [HarmonyPatch(typeof(PlayerVisor), "RPCA_SetVisorText")]
    [HarmonyPrefix]
    private static bool PrefixSetVisorText(PlayerVisor __instance, ref string text) {
        if ((Object) __instance.visorFaceText == null) return false;

        __instance.visorFaceText.text = text;
        __instance.visorFaceText.enableAutoSizing = true;
        __instance.visorFaceText.fontSizeMin = 10f;
        __instance.visorFaceText.fontSizeMax = 40f;

        return false;
    }

    [HarmonyPatch(typeof(PlayerCustomizer), "OnChangeFaceSize")]
    [HarmonyPrefix]
    private static bool PrefixOnChangeFaceSize(PlayerCustomizer __instance, bool smaller) {
        var x = __instance.faceText.transform.localScale.x;
        const float num = 0.05f;
        var num2 = smaller ? x - num : x + num;

        __instance.faceText.transform.localScale = new Vector3(num2, num2, 1f);

        Debug.Log($"Adjusted face size to: {num2}");

        return false;
    }
}