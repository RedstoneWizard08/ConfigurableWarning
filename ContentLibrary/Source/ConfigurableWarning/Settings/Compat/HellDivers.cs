using System.Linq;
using ConfigurableWarning.API.Attributes;
using ConfigurableWarning.API.Compat;
using ConfigurableWarning.API.Options;
using ConfigurableWarning.API.State;
using UnityEngine;

namespace ConfigurableWarning.Settings.Compat;

/// <summary>
///     Settings keys for HellDivers compat
/// </summary>
public static class HellDiversSettingKeys {
#pragma warning disable CS1591
    public const string DiveKey = "DiveKey";
#pragma warning restore CS1591
}

/// <summary>
///     HellDivers compat settings
/// </summary>
[CompatModule(["HellDivers"])]
public class HellDiversCompat : ICompatModule {
    /// <inheritdoc />
    public void Init() {
        States.Keys[HellDiversSettingKeys.DiveKey] = HellDivers.Main.instance.diveKeybind.Value;
    }

    internal static void ApplySettings(IUntypedOption _opt) {
        string[] all = [HellDiversSettingKeys.DiveKey];

        if (!all.All(v => OptionsState.Instance.Has(v))) return;

        HellDivers.Main.instance.diveKeybind.Value = States.Keys[HellDiversSettingKeys.DiveKey];
    }

    [CompatGroup("HELLDIVERS", "GENERAL")]
    private static class Settings {
        [Register]
        private class DiveKey()
            : KeyCodeOption(HellDiversSettingKeys.DiveKey, KeyCode.F, "Dive Keybind", [ApplySettings]);
    }
}
