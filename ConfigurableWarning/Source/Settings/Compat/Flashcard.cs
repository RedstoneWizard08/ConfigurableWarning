using System.Linq;
using ConfigurableWarning.API;
using ConfigurableWarning.API.Attributes;
using ConfigurableWarning.API.Compat;
using ConfigurableWarning.API.Options;
using Flashcard;

namespace ConfigurableWarning.Settings.Compat;

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
public class FlashcardCompat : ICompatModule {
    /// <inheritdoc />
    public void Init() {
        States.Bools[FlashcardSettingKeys.EnableExtraCamera] =
            FlashcardPlugin.config.UPGRADES_EXTRA_CAMERA.Value == "disabled";
        States.Floats[FlashcardSettingKeys.ClipLength] = FlashcardPlugin.config.RECORDING_CLIP_LENGTH.Value;
        States.Ints[FlashcardSettingKeys.ClipFramerate] = FlashcardPlugin.config.RECORDING_CLIP_FRAMERATE.Value;
        States.Strings[FlashcardSettingKeys.ClipQuality] = FlashcardPlugin.config.RECORDING_CLIP_QUALITY.Value;
        States.Floats[FlashcardSettingKeys.PacketDelay] = FlashcardPlugin.config.UPLOADING_DELAY_BETWEEN_PACKETS.Value;
        States.Bools[FlashcardSettingKeys.VerboseLogging] = FlashcardPlugin.config.DEBUGGING_VERBOSE_LOGGING.Value;
    }

    internal static void ApplySettings(IUntypedOption _opt) {
        string[] all = [
            FlashcardSettingKeys.EnableExtraCamera,
            FlashcardSettingKeys.ClipLength,
            FlashcardSettingKeys.ClipFramerate,
            FlashcardSettingKeys.ClipQuality,
            FlashcardSettingKeys.PacketDelay,
            FlashcardSettingKeys.VerboseLogging
        ];

        if (!all.All(v => OptionsState.Instance.Has(v))) return;

        FlashcardPlugin.config.UPGRADES_EXTRA_CAMERA.Value =
            States.Bools[FlashcardSettingKeys.EnableExtraCamera] ? "always" : "disabled";
        FlashcardPlugin.config.RECORDING_CLIP_LENGTH.Value = States.Floats[FlashcardSettingKeys.ClipLength];
        FlashcardPlugin.config.RECORDING_CLIP_FRAMERATE.Value = States.Ints[FlashcardSettingKeys.ClipFramerate];
        FlashcardPlugin.config.RECORDING_CLIP_QUALITY.Value = States.Strings[FlashcardSettingKeys.ClipQuality];
        FlashcardPlugin.config.UPLOADING_DELAY_BETWEEN_PACKETS.Value = States.Floats[FlashcardSettingKeys.PacketDelay];
        FlashcardPlugin.config.DEBUGGING_VERBOSE_LOGGING.Value = States.Bools[FlashcardSettingKeys.VerboseLogging];
    }

    [CompatTab("FLASHCARD")]
    private static class Settings {
        [Group("UPGRADES")]
        private static class Upgrades {
            [Register]
            private class EnableExtraCamera()
                : BoolOption(FlashcardSettingKeys.EnableExtraCamera, true, "Enable Extra Camera Upgrade",
                    [ApplySettings]);
        }

        [Group("RECORDING")]
        private static class Recording {
            [Register]
            private class ClipLength()
                : FloatOption(FlashcardSettingKeys.ClipLength, 120f, "Clip Length", 1f, 1000f,
                    [ApplySettings], false);

            [Register]
            private class ClipFramerate()
                : IntOption(FlashcardSettingKeys.ClipFramerate, 24, "Clip Framerate", 1, 30,
                    [ApplySettings], false);

            [Register]
            private class ClipQuality()
                : TextOption(FlashcardSettingKeys.ClipQuality, "512k", "Clip Quality (Bitrate)",
                    [ApplySettings]);
        }

        [Group("UPLOADING")]
        private static class Uploading {
            [Register]
            private class PacketDelay()
                : FloatOption(FlashcardSettingKeys.PacketDelay, 0.5f, "Delay Between Packets", 0f, 10f, [ApplySettings],
                    false);
        }

        [Group("DEBUGGING")]
        private static class Debugging {
            [Register]
            private class VerboseLogging()
                : BoolOption(FlashcardSettingKeys.VerboseLogging, false, "Enable Verbose Logging", [ApplySettings]);
        }
    }
}