using UnityEngine;

namespace ConfigurableWarning.Settings {
    public class PackedSettings {
        [SerializeField] private float maxOxygen;
        [SerializeField] private float maxHealth;
        [SerializeField] private int daysPerQuota;
        [SerializeField] private float sprintUsage;
        [SerializeField] private bool useOxygenInDiveBell;
        [SerializeField] private bool refillOxygenInDiveBell;
        [SerializeField] private bool useOxygenOnSurface;
        [SerializeField] private bool refillOxygenOnSurface;
        [SerializeField] private float oxygenRefillRate;

        public static PackedSettings Collect() {
            var me = new PackedSettings {
                maxOxygen = Plugin.Instance.PluginSettings.maxOxygen.RealValue,
                maxHealth = Plugin.Instance.PluginSettings.maxHealth.RealValue,
                daysPerQuota = Plugin.Instance.PluginSettings.daysPerQuota.RealValue,
                sprintUsage = Plugin.Instance.PluginSettings.sprintUsage.RealValue,
                useOxygenInDiveBell = Plugin.Instance.PluginSettings.useOxygenInDiveBell.RealValue,
                refillOxygenInDiveBell = Plugin.Instance.PluginSettings.refillOxygenInDiveBell.RealValue,
                useOxygenOnSurface = Plugin.Instance.PluginSettings.useOxygenOnSurface.RealValue,
                refillOxygenOnSurface = Plugin.Instance.PluginSettings.refillOxygenOnSurface.RealValue,
                oxygenRefillRate = Plugin.Instance.PluginSettings.oxygenRefillRate.RealValue,
            };

            return me;
        }

        public string Pack() {
            return JsonUtility.ToJson(this);
        }

        public static PackedSettings Unpack(string data) {
            var me = JsonUtility.FromJson<PackedSettings>(data);

            Plugin.Instance.PluginSettings.maxOxygen.SetValue(me.maxOxygen, GameHandler.Instance.SettingsHandler);
            Plugin.Instance.PluginSettings.maxHealth.SetValue(me.maxHealth, GameHandler.Instance.SettingsHandler);
            Plugin.Instance.PluginSettings.daysPerQuota.SetValue(me.daysPerQuota, GameHandler.Instance.SettingsHandler);
            Plugin.Instance.PluginSettings.sprintUsage.SetValue(me.sprintUsage, GameHandler.Instance.SettingsHandler);
            Plugin.Instance.PluginSettings.useOxygenInDiveBell.SetValue(me.useOxygenInDiveBell, GameHandler.Instance.SettingsHandler);
            Plugin.Instance.PluginSettings.refillOxygenInDiveBell.SetValue(me.refillOxygenInDiveBell, GameHandler.Instance.SettingsHandler);
            Plugin.Instance.PluginSettings.useOxygenOnSurface.SetValue(me.useOxygenOnSurface, GameHandler.Instance.SettingsHandler);
            Plugin.Instance.PluginSettings.refillOxygenOnSurface.SetValue(me.refillOxygenOnSurface, GameHandler.Instance.SettingsHandler);
            Plugin.Instance.PluginSettings.oxygenRefillRate.SetValue(me.oxygenRefillRate, GameHandler.Instance.SettingsHandler);

            return me;
        }
    }
}