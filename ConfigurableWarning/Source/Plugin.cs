using BepInEx;
using ConfigurableWarning.API;
using HarmonyLib;

namespace ConfigurableWarning;

#pragma warning disable 1591

[ContentWarningPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_VERSION, false)]
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(MyceliumNetworking.MyPluginInfo.PLUGIN_GUID)]
[BepInDependency(ContentSettings.MyPluginInfo.PLUGIN_GUID)]
public class Plugin : BaseUnityPlugin {
    // CF + G (in hex = 0x47) + W (in hex = 0x57), for ConFiGurableWarning
    public const uint ModID = 0xCF4757;
    private readonly Harmony _harmony = new(MyPluginInfo.PLUGIN_GUID);

    public void Awake() {
        Logger.LogInfo($"Loading plugin {MyPluginInfo.PLUGIN_GUID}...");

        ConfigurableWarningAPI.Init();
        Patch();

        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded!");
    }

    public void Patch() {
        _harmony.PatchAll();
    }

    public void Unpatch() {
        Harmony.UnpatchAll();
    }
}

#pragma warning restore 1591
