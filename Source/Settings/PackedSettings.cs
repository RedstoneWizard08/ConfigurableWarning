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

        public static PackedSettings collect() {
            var me = new PackedSettings {
                maxOxygen = Plugin.Instance.PluginConfig.maxOxygen.Value,
                maxHealth = Plugin.Instance.PluginConfig.maxHealth.Value,
                daysPerQuota = Plugin.Instance.PluginConfig.daysPerQuota.Value,
                sprintUsage = Plugin.Instance.PluginConfig.sprintUsage.Value,
                useOxygenInDiveBell = Plugin.Instance.PluginConfig.useOxygenInDiveBell.Value,
                refillOxygenInDiveBell = Plugin.Instance.PluginConfig.refillOxygenInDiveBell.Value,
                useOxygenOnSurface = Plugin.Instance.PluginConfig.useOxygenOnSurface.Value,
                refillOxygenOnSurface = Plugin.Instance.PluginConfig.refillOxygenOnSurface.Value,
                oxygenRefillRate = Plugin.Instance.PluginConfig.oxygenRefillRate.Value
            };

            return me;
        }

        public string pack() {
            return JsonUtility.ToJson(this);
        }

        public static PackedSettings unpack(string data) {
            var me = JsonUtility.FromJson<PackedSettings>(data);

            Plugin.Instance.PluginSettings.maxOxygen.Set(me.maxOxygen);
            Plugin.Instance.PluginSettings.maxHealth.Set(me.maxHealth);
            Plugin.Instance.PluginSettings.daysPerQuota.Set(me.daysPerQuota);
            Plugin.Instance.PluginSettings.sprintUsage.Set(me.sprintUsage);
            Plugin.Instance.PluginSettings.useOxygenInDiveBell.Set(me.useOxygenInDiveBell);
            Plugin.Instance.PluginSettings.refillOxygenInDiveBell.Set(me.refillOxygenInDiveBell);
            Plugin.Instance.PluginSettings.useOxygenOnSurface.Set(me.useOxygenOnSurface);
            Plugin.Instance.PluginSettings.refillOxygenOnSurface.Set(me.refillOxygenOnSurface);
            Plugin.Instance.PluginSettings.oxygenRefillRate.Set(me.oxygenRefillRate);

            return me;
        }
    }
}