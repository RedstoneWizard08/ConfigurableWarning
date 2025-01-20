using BepInEx.Logging;
using ConfigurableWarning.API;
using ContentLibrary;

namespace ConfigurableWarning;

/// <summary>
///     ConfigurableWarning's entrypoint.
/// </summary>
public class ConfigurableWarningEntry {
    /// <summary>
    ///     Our <see cref="ManualLogSource" />.
    /// </summary>
    internal static readonly ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(CWPluginInfo.PLUGIN_GUID);

    /// <summary>
    ///     Initializes the plugin
    /// </summary>
    public static void Init() {
        Logger.LogInfo($"Loading plugin {CWPluginInfo.PLUGIN_GUID} (bundled with {MyPluginInfo.PLUGIN_GUID})...");

        ConfigurableWarningAPI.Init();
        ConfigurableWarningAPI.Register();

        Logger.LogInfo($"Plugin {CWPluginInfo.PLUGIN_GUID} loaded!");
    }
}