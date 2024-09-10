using BepInEx;
using BepInEx.Logging;
using ConfigurableWarning.API;
using ContentLibrary;

namespace ConfigurableWarning;

/// <summary>
///     ConfigurableWarning's BepInEx entrypoint.
/// </summary>
[ContentWarningPlugin(CWPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_VERSION, false)]
[BepInPlugin(CWPluginInfo.PLUGIN_GUID, CWPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(MyceliumNetworking.MyPluginInfo.PLUGIN_GUID)]
public class ConfigurableWarning : BaseUnityPlugin {
    /// <summary>
    ///     Our <see cref="ManualLogSource" />.
    /// </summary>
    internal new static readonly ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(CWPluginInfo.PLUGIN_GUID);

    /// <summary>
    ///     Initializes the plugin
    /// </summary>
    public void Awake() {
        Logger.LogInfo($"Loading plugin {CWPluginInfo.PLUGIN_GUID} (bundled with {MyPluginInfo.PLUGIN_GUID})...");

        ConfigurableWarningAPI.Init();
        ConfigurableWarningAPI.Register();

        Logger.LogInfo($"Plugin {CWPluginInfo.PLUGIN_GUID} loaded!");
    }
}
