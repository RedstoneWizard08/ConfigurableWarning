using System.Linq;
using ConfigurableWarning.API.Attributes;
using ConfigurableWarning.API.Compat;
using ConfigurableWarning.API.Options;
using ConfigurableWarning.API.State;
using HellDivers;
using UnityEngine;

namespace ContentLibrary.Settings.Compat;

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
        States.Keys[HellDiversSettingKeys.DiveKey] = Main.instance.diveKeybind.Value;
    }

    private static void ApplySettings(IUntypedOption opt) {
        string[] all = [HellDiversSettingKeys.DiveKey];

        if (!all.All(v => OptionsState.Instance.Has(v))) return;

        Main.instance.diveKeybind.Value = States.Keys[HellDiversSettingKeys.DiveKey];
    }

    [CompatGroup("HELLDIVERS", "GENERAL")]
    private static class Settings {
        [Register]
        private class DiveKey()
            : KeyCodeOption(HellDiversSettingKeys.DiveKey, KeyCode.F, "Dive Keybind", [ApplySettings]);
    }
}