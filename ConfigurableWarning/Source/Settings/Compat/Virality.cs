namespace ConfigurableWarning.Settings.Compat;

using ConfigurableWarning.API;
using ConfigurableWarning.API.Compat;
using ConfigurableWarning.API.Options;
using Virality;

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
public class ViralityCompat: ICompatModule {
    [CompatSetting]
    private class MaxPlayers()
        : IntOption(ViralitySettingKeys.MaxPlayers, 12, "Max Players", 4, 100, "VIRALITY", "GENERAL", [ApplySettings], false);

    [CompatSetting]
    private class AllowLateJoin()
        : BoolOption(ViralitySettingKeys.AllowLateJoin, true, "Allow Late Joining", "VIRALITY", "GENERAL", [ApplySettings]);

    [CompatSetting]
    private class EnableVoiceFix()
        : BoolOption(ViralitySettingKeys.EnableVoiceFix, true, "Enable Voice Fix", "VIRALITY", "GENERAL", [ApplySettings]);

    /// <inheritdoc />
    public void Init() {
        States.Ints[ViralitySettingKeys.MaxPlayers] = Virality.MaxPlayers!.Value;
        States.Bools[ViralitySettingKeys.AllowLateJoin] = Virality.AllowLateJoin!.Value;
        States.Bools[ViralitySettingKeys.EnableVoiceFix] = Virality.EnableVoiceFix!.Value;
    }

    internal static void ApplySettings(IUntypedOption _opt) {
        Virality.MaxPlayers!.Value = States.Ints[ViralitySettingKeys.MaxPlayers];
        Virality.AllowLateJoin!.Value = States.Bools[ViralitySettingKeys.AllowLateJoin];
        Virality.EnableVoiceFix!.Value = States.Bools[ViralitySettingKeys.EnableVoiceFix];
    }
}
