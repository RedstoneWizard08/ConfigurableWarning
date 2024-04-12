using BepInEx;
using HarmonyLib;
using ConfigurableWarning.Settings;
using BepInEx.Logging;

namespace ConfigurableWarning {
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin {
        public static Plugin Instance { get; private set; } = null!;
        public static bool IsBoot = true;

        public PluginConfig PluginConfig;
        public PluginSettings PluginSettings;
        public Harmony Harmony = new(MyPluginInfo.PLUGIN_GUID);

        public void Awake() {
            Instance = this;

            PluginConfig = new PluginConfig();
            PluginSettings = new PluginSettings();

            Logger.LogInfo($"Loading plugin {MyPluginInfo.PLUGIN_GUID}...");

            IsBoot = false;

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

            Harmony.PatchAll();
        }

        public ManualLogSource GetLogger() {
            return Logger;
        }
    }
}
