using BepInEx;
using BepInEx.Logging;
using ConfigurableWarning;
using ContentSettings.API;
using ContentSettings.Internal;

namespace ContentSettings;

/// <summary>
/// The main Content Settings plugin class.
/// </summary>
[ContentWarningPlugin(CSPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_VERSION, true)]
[BepInPlugin(CSPluginInfo.PLUGIN_GUID, CSPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public partial class ContentSettings : BaseUnityPlugin {
    /// <summary>
    /// Gets the logger of the plugin.
    /// </summary>
    internal new static ManualLogSource Logger { get; private set; } = null!;

    /// <summary>
    /// Initialize ContentSettings.
    /// </summary>
    public void Awake() {
        // DON'T PATCH HARMONY HERE!!! ConfigurableWarning already does it!

        Logger = base.Logger;

        SettingsAssets.LoadAssets();
    }

    /// <summary>
    /// Update ContentSettings.
    /// </summary>
    public void Update() {
        SettingsLoader.Update();
    }
}
