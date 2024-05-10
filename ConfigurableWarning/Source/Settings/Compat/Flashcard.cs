namespace ConfigurableWarning.Settings.Compat;

using ConfigurableWarning.API;
using ConfigurableWarning.API.Compat;
using ConfigurableWarning.API.Options;
using Flashcard;

/// <summary>
///     Settings keys for Flashcard compat
/// </summary>
public static class FlashcardSettingKeys {
#pragma warning disable CS1591
    public const string EnableExtraCamera = "EnableExtraCamera";
    public const string ClipLength = "ClipLength";
    public const string ClipFramerate = "ClipFramerate";
    public const string ClipQuality = "ClipQuality";
    public const string PacketDelay = "PacketDelay";
    public const string VerboseLogging = "VerboseLogging";
#pragma warning restore CS1591
}

/// <summary>
///     Flashcard compat settings
/// </summary>
[CompatModule(["Flashcard"])]
public class FlashcardCompat: ICompatModule {
    [CompatSetting]
    private class EnableExtraCamera()
        : BoolOption(FlashcardSettingKeys.EnableExtraCamera, true, "Enable Extra Camera Upgrade", "FLASHCARD", "UPGRADES", [ApplySettings]);

    [CompatSetting]
    private class ClipLength()
        : FloatOption(FlashcardSettingKeys.ClipLength, 120f, "Clip Length", 1f, 1000f, "FLASHCARD", "RECORDING", [ApplySettings], false);
    
    [CompatSetting]
    private class ClipFramerate()
        : IntOption(FlashcardSettingKeys.ClipFramerate, 24, "Clip Framerate", 1, 30, "FLASHCARD", "RECORDING", [ApplySettings], false);
    
    [CompatSetting]
    private class ClipQuality()
        : TextOption(FlashcardSettingKeys.ClipQuality, "512k", "Clip Quality (Bitrate)", "FLASHCARD", "RECORDING", [ApplySettings]);

    [CompatSetting]
    private class PacketDelay()
        : FloatOption(FlashcardSettingKeys.PacketDelay, 0.5f, "Delay Between Packets", 0f, 10f, "FLASHCARD", "UPLOADING", [ApplySettings], false);

    [CompatSetting]
    private class VerboseLogging()
        : BoolOption(FlashcardSettingKeys.VerboseLogging, false, "Enable Verbose Logging", "FLASHCARD", "DEBUGGING", [ApplySettings]);

    /// <inheritdoc />
    public void Init() {
        States.Bools[FlashcardSettingKeys.EnableExtraCamera] = FlashcardPlugin.config.UPGRADES_EXTRA_CAMERA.Value == "disabled";
        States.Floats[FlashcardSettingKeys.ClipLength] = FlashcardPlugin.config.RECORDING_CLIP_LENGTH.Value;
        States.Ints[FlashcardSettingKeys.ClipFramerate] = FlashcardPlugin.config.RECORDING_CLIP_FRAMERATE.Value;
        States.Strings[FlashcardSettingKeys.ClipQuality] = FlashcardPlugin.config.RECORDING_CLIP_QUALITY.Value;
        States.Floats[FlashcardSettingKeys.PacketDelay] = FlashcardPlugin.config.UPLOADING_DELAY_BETWEEN_PACKETS.Value;
        States.Bools[FlashcardSettingKeys.VerboseLogging] = FlashcardPlugin.config.DEBUGGING_VERBOSE_LOGGING.Value;
    }

    internal static void ApplySettings(IUntypedOption _opt) {
        FlashcardPlugin.config.UPGRADES_EXTRA_CAMERA.Value = States.Bools[FlashcardSettingKeys.EnableExtraCamera] ? "always" : "disabled";
        FlashcardPlugin.config.RECORDING_CLIP_LENGTH.Value = States.Floats[FlashcardSettingKeys.ClipLength];
        FlashcardPlugin.config.RECORDING_CLIP_FRAMERATE.Value = States.Ints[FlashcardSettingKeys.ClipFramerate];
        FlashcardPlugin.config.RECORDING_CLIP_QUALITY.Value = States.Strings[FlashcardSettingKeys.ClipQuality];
        FlashcardPlugin.config.UPLOADING_DELAY_BETWEEN_PACKETS.Value = States.Floats[FlashcardSettingKeys.PacketDelay];
        FlashcardPlugin.config.DEBUGGING_VERBOSE_LOGGING.Value = States.Bools[FlashcardSettingKeys.VerboseLogging];
    }
}
