namespace ConfigurableWarning.Settings {
    public class SettingsState {
        public bool privateHost;
        public float maxOxygen;
        public float maxHealth;
        public int daysPerQuota;
        public float sprintUsage;
        public float oxygenUsage;
        public bool useOxygenInDiveBell;
        public bool refillOxygenInDiveBell;
        public bool useOxygenOnSurface;
        public bool refillOxygenOnSurface;
        public float oxygenRefillRate;

        public SettingsState() {
            privateHost = true;
            maxOxygen = 500.0f;
            maxHealth = 100.0f;
            daysPerQuota = 3;
            sprintUsage = 1.0f;
            oxygenUsage = 1.0f;
            useOxygenInDiveBell = true;
            refillOxygenInDiveBell = true;
            useOxygenOnSurface = true;
            refillOxygenOnSurface = true;
            oxygenRefillRate = 1.0f;
        }
    }
}
