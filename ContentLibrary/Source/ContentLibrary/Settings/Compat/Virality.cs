using System.Linq;
using ConfigurableWarning.API.Attributes;
using ConfigurableWarning.API.Compat;
using ConfigurableWarning.API.Options;
using ConfigurableWarning.API.State;

namespace ContentLibrary.Settings.Compat;

/// <summary>
///     Settings keys for Virality compat
/// </summary>
public static class ViralitySettingKeys {
#pragma warning disable CS1591
    public const string MaxPlayers = "MaxPlayers";
    public const string AllowLateJoin = "AllowLateJoin";
    public const string EnableVoiceFix = "EnableVoiceFix";
#pragma warning restore CS1591
}

/// <summary>
///     Virality compat settings
/// </summary>
[CompatModule(["Virality"])]
public class ViralityCompat : ICompatModule {
    /// <inheritdoc />
    public void Init() {
        States.Ints[ViralitySettingKeys.MaxPlayers] = Virality.Virality.MaxPlayers!.Value;
        States.Bools[ViralitySettingKeys.AllowLateJoin] = Virality.Virality.AllowLateJoin!.Value;
        States.Bools[ViralitySettingKeys.EnableVoiceFix] = Virality.Virality.EnableVoiceFix!.Value;
    }

    private static void ApplySettings(IUntypedOption opt) {
        string[] all = [
            ViralitySettingKeys.MaxPlayers,
            ViralitySettingKeys.AllowLateJoin,
            ViralitySettingKeys.EnableVoiceFix
        ];

        if (!all.All(v => OptionsState.Instance.Has(v))) return;

        Virality.Virality.MaxPlayers!.Value = States.Ints[ViralitySettingKeys.MaxPlayers];
        Virality.Virality.AllowLateJoin!.Value = States.Bools[ViralitySettingKeys.AllowLateJoin];
        Virality.Virality.EnableVoiceFix!.Value = States.Bools[ViralitySettingKeys.EnableVoiceFix];
    }

    [CompatGroup("VIRALITY", "GENERAL")]
    private static class Settings {
        [Register]
        private class MaxPlayers()
            : IntOption(ViralitySettingKeys.MaxPlayers, 12, "Max Players", 4, 100, [ApplySettings],
                false);

        [Register]
        private class AllowLateJoin()
            : BoolOption(ViralitySettingKeys.AllowLateJoin, true, "Allow Late Joining",
                [ApplySettings]);

        [Register]
        private class EnableVoiceFix()
            : BoolOption(ViralitySettingKeys.EnableVoiceFix, true, "Enable Voice Fix",
                [ApplySettings]);
    }
}