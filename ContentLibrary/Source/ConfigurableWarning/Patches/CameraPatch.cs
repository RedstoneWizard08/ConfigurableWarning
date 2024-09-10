using System;
using System.IO;
using ConfigurableWarning.API.State;
using ConfigurableWarning.Settings;
using HarmonyLib;
using Zorro.Core;

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

    /// <summary>
    ///     Patches the <see cref="CameraRecording.SaveToDesktop" /> method to save videos
    ///     to a custom location.
    /// </summary>
    /// <param name="__instance">The current instance of a <see cref="MainCamera" />.</param>
    /// <param name="videoFileName">The saved video file path.</param>
    /// <returns>Whether or not to continue executing the rest of the method.</returns>
    [HarmonyPatch(typeof(CameraRecording), nameof(CameraRecording.SaveToDesktop))]
    [HarmonyPrefix]
    public static bool SaveToDesktop(CameraRecording __instance, out string videoFileName) {
        if (!RecordingsHandler.TryGetRecordingPath(__instance.videoHandle, out string sourceFileName)) {
            videoFileName = string.Empty;
            return false;
        }

        videoFileName = "content_warning_" + GuidExtensions.ToShortString(__instance.videoHandle.id) + ".webm";

        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string value = States.Strings[SettingKeys.VideoSaveLocation] ?? "Desktop";

        if (value.ToLower() != "desktop" && Directory.Exists(value)) {
            File.Copy(sourceFileName, Path.Combine(value, videoFileName));
            ConfigurableWarning.Logger.LogInfo("Saved '" + videoFileName + "' to '" + Path.Combine(value, videoFileName) + "'");
        } else {
            File.Copy(sourceFileName, Path.Combine(folderPath, videoFileName));
            ConfigurableWarning.Logger.LogWarning("Could not find folder at specified custom path '" + value + "', so the video was instead  saved '" + videoFileName + "' to '" + Path.Combine(value, videoFileName) + "'");
        }

        return true;
    }
}