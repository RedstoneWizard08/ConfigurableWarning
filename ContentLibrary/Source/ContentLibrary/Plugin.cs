using BepInEx;
using BepInEx.Logging;
using ConfigurableWarning;
using ContentSettings;
using HarmonyLib;

namespace ContentLibrary;

/// <summary>
///     ContentLibrary's BepInEx entrypoint.
/// </summary>
[ContentWarningPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_VERSION, false)]
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(MyceliumNetworking.MyPluginInfo.PLUGIN_GUID)]
public class Plugin : BaseUnityPlugin {
    /// <summary>
    ///     The ModID for Mycelium.
    ///     This is just `CLIB` in hex.
    /// </summary>
    public const uint ModID = 0x434C4942;

    /// <summary>
    ///     Our <see cref="ManualLogSource" />.
    /// </summary>
    internal new static readonly ManualLogSource Logger =
        BepInEx.Logging.Logger.CreateLogSource(MyPluginInfo.PLUGIN_GUID);

    /// <summary>
    ///     Our <see cref="Harmony" /> instance
    /// </summary>
    private readonly Harmony _harmony = new(MyPluginInfo.PLUGIN_GUID);

    /// <summary>
    ///     Initializes the plugin
    /// </summary>
    public void Awake() {
        Logger.LogInfo($"Loading plugin {MyPluginInfo.PLUGIN_GUID}...");

        ContentSettingsEntry.Init();
        ConfigurableWarningEntry.Init();
        Patch();

        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded!");
    }

    /// <summary>
    ///     Called each frame.
    /// </summary>
    public void Update() {
        ContentSettingsEntry.Update();
    }

    /// <summary>
    ///     Applies patches
    /// </summary>
    public void Patch() {
        Logger.LogInfo("Patching...");

        _harmony.PatchAll();
    }

    /// <summary>
    ///     Un-applies patches
    /// </summary>
    public void Unpatch() {
        Logger.LogInfo("Unpatching...");

        Harmony.UnpatchAll();
    }
}