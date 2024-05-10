using BepInEx;
using ConfigurableWarning.API;
using HarmonyLib;

namespace ConfigurableWarning;

/// <summary>
/// ConfigurableWarning's BepInEx entrypoint.
/// </summary>
[ContentWarningPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_VERSION, false)]
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(MyceliumNetworking.MyPluginInfo.PLUGIN_GUID)]
[BepInDependency(ContentSettings.MyPluginInfo.PLUGIN_GUID)]
public class Plugin : BaseUnityPlugin {
    /// <summary>
    /// The ModID for Mycelium.
    /// This is: CF + G (in hex = 0x47) + W (in hex = 0x57), for ConFiGurableWarning
    /// </summary>
    public const uint ModID = 0xCF4757;

    /// <summary>
    /// Our <see cref="Harmony" /> instance
    /// </summary>
    private readonly Harmony _harmony = new(MyPluginInfo.PLUGIN_GUID);

    /// <summary>
    /// Initializes the plugin
    /// </summary>
    public void Awake() {
        Logger.LogInfo($"Loading plugin {MyPluginInfo.PLUGIN_GUID}...");

        ConfigurableWarningAPI.Init();
        Patch();

        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded!");
    }

    /// <summary>
    /// Applies patches
    /// </summary>
    public void Patch() {
        _harmony.PatchAll();
    }

    /// <summary>
    /// Un-applies patches
    /// </summary>
    public void Unpatch() {
        Harmony.UnpatchAll();
    }
}
