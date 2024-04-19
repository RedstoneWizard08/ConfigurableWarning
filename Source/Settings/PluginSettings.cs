using ConfigurableWarning.Settings.Objects;

namespace ConfigurableWarning.Settings {
    public class PluginSettings {
        public PrivateHost privateHost;
        public Oxygen maxOxygen;
        public Health maxHealth;
        public DaysPerQuota daysPerQuota;
        public SprintMultiplier sprintUsage;
        public OxygenUsageMultiplier oxygenUsage;
        public UseOxygenInDiveBell useOxygenInDiveBell;
        public RefillOxygenInDiveBell refillOxygenInDiveBell;
        public UseOxygenOnSurface useOxygenOnSurface;
        public RefillOxygenOnSurface refillOxygenOnSurface;
        public OxygenRefillRate oxygenRefillRate;

        public PluginSettings() {
            // -------------------- General -------------------- //

            SettingHelper.Setup(new TextSetting("General"));

            privateHost = SettingHelper.Setup(new PrivateHost());
            daysPerQuota = SettingHelper.Setup(new DaysPerQuota());

            // -------------------- Player -------------------- //

            SettingHelper.Setup(new TextSetting("Player"));

            maxHealth = SettingHelper.Setup(new Health());

            // -------------------- Oxygen -------------------- //

            SettingHelper.Setup(new TextSetting("Oxygen"));

            maxOxygen = SettingHelper.Setup(new Oxygen());
            oxygenUsage = SettingHelper.Setup(new OxygenUsageMultiplier());
            sprintUsage = SettingHelper.Setup(new SprintMultiplier());
            oxygenRefillRate = SettingHelper.Setup(new OxygenRefillRate());

            useOxygenInDiveBell = SettingHelper.Setup(new UseOxygenInDiveBell());
            refillOxygenInDiveBell = SettingHelper.Setup(new RefillOxygenInDiveBell());
            useOxygenOnSurface = SettingHelper.Setup(new UseOxygenOnSurface());
            refillOxygenOnSurface = SettingHelper.Setup(new RefillOxygenOnSurface());
        }
    }
}
