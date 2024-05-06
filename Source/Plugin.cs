using BepInEx;
using HarmonyLib;
using BepInEx.Logging;
using ConfigurableWarning.Options;
using ConfigurableWarning.Settings;

namespace ConfigurableWarning {
    [ContentWarningPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_VERSION, false)]
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInDependency(MyceliumNetworking.MyPluginInfo.PLUGIN_GUID)]
    [BepInDependency(ContentSettings.MyPluginInfo.PLUGIN_GUID)]
    public class Plugin : BaseUnityPlugin {
        public static Plugin Instance { get; private set; } = null!;
        public static OptionSyncer Sync { get; private set; } = null!;

        // CF + G (in hex = 0x47) + W (in hex = 0x57), for ConFiGurableWarning
        public const uint ModID = 0xCF4757;
        private readonly Harmony _harmony = new(MyPluginInfo.PLUGIN_GUID);

        public void Awake() {
            Logger.LogInfo($"Loading plugin {MyPluginInfo.PLUGIN_GUID}...");

            Instance = this;
            Sync = new();
            BuiltInSettings.Init();
            Patch();

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded!");
        }

        public void Patch() {
            _harmony.PatchAll();
        }

        public void Unpatch() {
            Harmony.UnpatchAll();
        }

        public ManualLogSource GetLogger() {
            return Logger;
        }
    }
}
