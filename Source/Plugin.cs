using BepInEx;
using ContentSettings.API;
using HarmonyLib;
using ConfigurableWarning.Settings;
using System.Reflection;
using BepInEx.Logging;

namespace ConfigurableWarning {
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin {
        public static Plugin Instance { get; private set; } = null!;
        public static bool IsBoot = true;

        public PluginConfig config;
        public ConfigContainers configs;

        public void Awake() {
            Instance = this;
            config = new PluginConfig();
            configs = new ConfigContainers();

            Logger.LogInfo($"Loading plugin {PluginInfo.PLUGIN_GUID}...");

            ConfigContainers.maxOxygen = new Oxygen();
            ConfigContainers.maxHealth = new Health();
            ConfigContainers.oxygenUsage = new OxygenUsageMultiplier();
            ConfigContainers.sprintUsage = new SprintMultiplier();
            ConfigContainers.daysPerQuota = new DaysPerQuota();
            ConfigContainers.useOxygenInDiveBell = new UseOxygenInDiveBell();
            ConfigContainers.useOxygenOnSurface = new UseOxygenOnSurface();
            ConfigContainers.refillOxygenOnSurface = new RefillOxygenOnSurface();

            SettingsLoader.RegisterSetting(ConfigContainers.maxOxygen);
            SettingsLoader.RegisterSetting(ConfigContainers.maxHealth);
            SettingsLoader.RegisterSetting(ConfigContainers.oxygenUsage);
            SettingsLoader.RegisterSetting(ConfigContainers.sprintUsage);
            SettingsLoader.RegisterSetting(ConfigContainers.daysPerQuota);
            SettingsLoader.RegisterSetting(ConfigContainers.useOxygenInDiveBell);
            SettingsLoader.RegisterSetting(ConfigContainers.useOxygenOnSurface);
            SettingsLoader.RegisterSetting(ConfigContainers.refillOxygenOnSurface);

            ConfigContainers.maxOxygen.ApplyValue();
            ConfigContainers.maxHealth.ApplyValue();
            ConfigContainers.oxygenUsage.ApplyValue();
            ConfigContainers.sprintUsage.ApplyValue();
            ConfigContainers.daysPerQuota.ApplyValue();
            ConfigContainers.useOxygenInDiveBell.ApplyValue();
            ConfigContainers.useOxygenOnSurface.ApplyValue();
            ConfigContainers.refillOxygenOnSurface.ApplyValue();

            IsBoot = false;

            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        public ManualLogSource GetLogger() {
            return Logger;
        }
    }

    public class ConfigContainers {
        public static Oxygen maxOxygen;
        public static Health maxHealth;
        public static DaysPerQuota daysPerQuota;
        public static SprintMultiplier sprintUsage;
        public static OxygenUsageMultiplier oxygenUsage;
        public static UseOxygenInDiveBell useOxygenInDiveBell;
        public static UseOxygenOnSurface useOxygenOnSurface;
        public static RefillOxygenOnSurface refillOxygenOnSurface;
    }
}
