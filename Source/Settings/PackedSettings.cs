namespace ConfigurableWarning.Settings {
    public class PackedSettings {
        private float maxOxygen;
        private float maxHealth;
        private int daysPerQuota;
        private float sprintMultiplier;
        private bool useOxygenInDiveBell;
        private bool refillOxygenInDiveBell;
        private bool useOxygenOnSurface;
        private bool refillOxygenOnSurface;
        private float oxygenRefillRate;

        public static PackedSettings pack() {
            var me = new PackedSettings {
                maxOxygen = Plugin.Instance.PluginConfig.maxOxygen.Value,
                maxHealth = Plugin.Instance.PluginConfig.maxHealth.Value,
                daysPerQuota = Plugin.Instance.PluginConfig.daysPerQuota.Value,
                sprintMultiplier = Plugin.Instance.PluginConfig.sprintUsage.Value,
                useOxygenInDiveBell = Plugin.Instance.PluginConfig.useOxygenInDiveBell.Value,
                refillOxygenInDiveBell = Plugin.Instance.PluginConfig.refillOxygenInDiveBell.Value,
                useOxygenOnSurface = Plugin.Instance.PluginConfig.useOxygenOnSurface.Value,
                refillOxygenOnSurface = Plugin.Instance.PluginConfig.refillOxygenOnSurface.Value,
                oxygenRefillRate = Plugin.Instance.PluginConfig.oxygenRefillRate.Value
            };

            return me;
        }

        public void unpack() {
            Plugin.Instance.PluginSettings.maxOxygen.Set(maxOxygen);
            Plugin.Instance.PluginSettings.maxHealth.Set(maxHealth);
            Plugin.Instance.PluginSettings.daysPerQuota.Set(daysPerQuota);
            Plugin.Instance.PluginSettings.sprintUsage.Set(sprintMultiplier);
            Plugin.Instance.PluginSettings.useOxygenInDiveBell.Set(useOxygenInDiveBell);
            Plugin.Instance.PluginSettings.refillOxygenInDiveBell.Set(refillOxygenInDiveBell);
            Plugin.Instance.PluginSettings.useOxygenOnSurface.Set(useOxygenOnSurface);
            Plugin.Instance.PluginSettings.refillOxygenOnSurface.Set(refillOxygenOnSurface);
            Plugin.Instance.PluginSettings.oxygenRefillRate.Set(oxygenRefillRate);
        }
    }
}