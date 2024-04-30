using UnityEngine;
using Zorro.Settings;

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
                maxOxygen = Plugin.State.maxOxygen,
                maxHealth = Plugin.State.maxHealth,
                daysPerQuota = Plugin.State.daysPerQuota,
                sprintUsage = Plugin.State.sprintUsage,
                useOxygenInDiveBell = Plugin.State.useOxygenInDiveBell,
                refillOxygenInDiveBell = Plugin.State.refillOxygenInDiveBell,
                useOxygenOnSurface = Plugin.State.useOxygenOnSurface,
                refillOxygenOnSurface = Plugin.State.refillOxygenOnSurface,
                oxygenRefillRate = Plugin.State.oxygenRefillRate,
            };

            return me;
        }

        public string Pack() {
            return JsonUtility.ToJson(this);
        }

        public static PackedSettings Unpack(string data) {
            var me = JsonUtility.FromJson<PackedSettings>(data);

            SettingsUtil.SetValue(ref Plugin.PluginSettings.maxOxygen, me.maxOxygen);
            SettingsUtil.SetValue(ref Plugin.PluginSettings.maxHealth, me.maxHealth);
            SettingsUtil.SetValue(ref Plugin.PluginSettings.daysPerQuota, me.daysPerQuota);
            SettingsUtil.SetValue(ref Plugin.PluginSettings.sprintUsage, me.sprintUsage);
            SettingsUtil.SetValue(ref Plugin.PluginSettings.useOxygenInDiveBell, me.useOxygenInDiveBell);
            SettingsUtil.SetValue(ref Plugin.PluginSettings.refillOxygenInDiveBell, me.refillOxygenInDiveBell);
            SettingsUtil.SetValue(ref Plugin.PluginSettings.useOxygenOnSurface, me.useOxygenOnSurface);
            SettingsUtil.SetValue(ref Plugin.PluginSettings.refillOxygenOnSurface, me.refillOxygenOnSurface);
            SettingsUtil.SetValue(ref Plugin.PluginSettings.oxygenRefillRate, me.oxygenRefillRate);

            return me;
        }
    }
}