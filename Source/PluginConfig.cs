using BepInEx.Configuration;

namespace ConfigurableWarning {
    public class PluginConfig {
        public ConfigEntry<float> maxOxygen;
        public ConfigEntry<float> oxygenUsageMultiplier;
        public ConfigEntry<float> sprintMultiplier;
        public ConfigEntry<bool> useOxygenInDiveBell;
        public ConfigEntry<bool> refillOxygenInDiveBell;
        public ConfigEntry<bool> useOxygenOnSurface;
        public ConfigEntry<bool> refillOxygenOnSurface;
        public ConfigEntry<float> oxygenRefillRate;
        public ConfigEntry<float> maxHealth;
        public ConfigEntry<int> daysPerQuota;
        public ConfigEntry<bool> privateHost;

        public PluginConfig() {
            // Synced settings

            maxHealth = Plugin.Instance.Config.Bind("General", "MaxHealth", 100f, "The maximum amount of health a player has.");
            daysPerQuota = Plugin.Instance.Config.Bind("General", "DaysPerQuota", 3, "Days per quota. Valid values: 3, 5, 7, 10, 14, 18, 20");
            maxOxygen = Plugin.Instance.Config.Bind("Oxygen", "MaxOxygen", 500f, "The maximum amount of oxygen a player has, in seconds.");
            oxygenUsageMultiplier = Plugin.Instance.Config.Bind("Oxygen", "OxygenUsageMultiplier", 1f, "The oxygen usage multiplier.");
            sprintMultiplier = Plugin.Instance.Config.Bind("Oxygen", "SprintOxygenUsageMultiplier", 1f, "The oxygen usage multiplier while sprinting.");
            useOxygenInDiveBell = Plugin.Instance.Config.Bind("Oxygen", "UseOxygenInDiveBell", false, "Whether or not to use oxygen while inside of the dive bell.");
            refillOxygenInDiveBell = Plugin.Instance.Config.Bind("Oxygen", "RefillOxygenInDiveBell", false, "Whether or not to refill oxygen while inside of the dive bell.");
            useOxygenOnSurface = Plugin.Instance.Config.Bind("Oxygen", "UseOxygenOnSurface", false, "Whether or not to use oxygen on the surface. Useful for debugging.");
            refillOxygenOnSurface = Plugin.Instance.Config.Bind("Oxygen", "RefillOxygenOnSurface", true, "Whether or not to refill oxygen on the surface. Useful for debugging.");
            oxygenRefillRate = Plugin.Instance.Config.Bind("Oxygen", "OxygenRefillRate", 10f, "The rate at which oxygen refills.");

            // Non-synced settings

            privateHost = Plugin.Instance.Config.Bind("General", "PrivateHost", false, "Whether or not to host games privately (friends-only).");
        }
    }
}
