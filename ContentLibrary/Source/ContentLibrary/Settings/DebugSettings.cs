using ConfigurableWarning.API.Attributes;
using ConfigurableWarning.API.Options;
using UnityEngine;

namespace ContentLibrary.Settings;

/// <summary>
///     Debug settings.
/// </summary>
[Tab("DEBUG")]
public class DebugSettings {
    [Group("CONTROLS")]
    private static class Controls {
        [Register]
        [NoSync]
        private class DebugUIButton() : KeyCodeOption(SettingKeys.DebugUIButton, KeyCode.F1, "Debug UI Button");
    }
}