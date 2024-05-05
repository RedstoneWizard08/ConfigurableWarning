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
        public RequireAllPlayersInDiveBell requireAllPlayersInDiveBell;
        public RequireDiveBellDoorClosed requireDiveBellDoorClosed;
        public InfiniteOxygen infiniteOxygen;

        public PluginSettings() {
            // -------------------- General -------------------- //

            privateHost = new PrivateHost();
            daysPerQuota = new DaysPerQuota();
            requireAllPlayersInDiveBell = new RequireAllPlayersInDiveBell();
            requireDiveBellDoorClosed = new RequireDiveBellDoorClosed();

            // -------------------- Player -------------------- //

            maxHealth = new Health();

            // -------------------- Oxygen -------------------- //

            maxOxygen = new Oxygen();
            oxygenUsage = new OxygenUsageMultiplier();
            sprintUsage = new SprintMultiplier();
            oxygenRefillRate = new OxygenRefillRate();

            useOxygenInDiveBell = new UseOxygenInDiveBell();
            refillOxygenInDiveBell = new RefillOxygenInDiveBell();
            useOxygenOnSurface = new UseOxygenOnSurface();
            refillOxygenOnSurface = new RefillOxygenOnSurface();
            infiniteOxygen = new InfiniteOxygen();
        }
    }
}
