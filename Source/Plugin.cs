using BepInEx;
using HarmonyLib;
using ConfigurableWarning.Settings;
using BepInEx.Logging;
using UnityEngine;

namespace ConfigurableWarning {
    [ContentWarningPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_VERSION, false)]
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInDependency(MyceliumNetworking.MyPluginInfo.PLUGIN_GUID)]
    [BepInDependency(ContentSettings.MyPluginInfo.PLUGIN_GUID)]
    public class Plugin : BaseUnityPlugin {
        public static Plugin Instance { get; private set; } = null!;
        public static SyncRPC Sync { get; private set; } = null!;
        public static bool IsBoot = true;
        
        // CF + G (in hex = 0x47) + W (in hex = 0x57), for ConFiGurableWarning
        public const uint MOD_ID = 0xCF4757;

        public PluginSettings PluginSettings;
        public Harmony Harmony = new(MyPluginInfo.PLUGIN_GUID);

        public void Awake() {
            Logger.LogInfo($"Loading plugin {MyPluginInfo.PLUGIN_GUID}...");

            Instance = this;
            Sync = new();

            PluginSettings = new();

            IsBoot = false;
            Harmony.PatchAll();

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded!");
        }

        public ManualLogSource GetLogger() {
            return Logger;
        }
    }
}
