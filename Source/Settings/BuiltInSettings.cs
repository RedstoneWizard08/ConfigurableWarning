using System.Diagnostics.CodeAnalysis;
using ConfigurableWarning.Options;

namespace ConfigurableWarning.Settings {
    public static class BuiltInSettings {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        public static class Keys {
            public const string PrivateHost = "PrivateHost";
            public const string DaysPerQuota = "DaysPerQuota";
            public const string RequireAllPlayersInDiveBell = "RequireAllPlayersInDiveBell";
            public const string RequireDiveBellDoorClosed = "RequireDiveBellDoorClosed";

            public const string Health = "Health";

            public const string Oxygen = "Oxygen";
            public const string InfiniteOxygen = "InfiniteOxygen";
            public const string OxygenUsageMultiplier = "OxygenUsageMultiplier";
            public const string SprintMultiplier = "SprintMultiplier";
            public const string UseOxygenInDiveBell = "UseOxygenInDiveBell";
            public const string RefillOxygenInDiveBell = "RefillOxygenInDiveBell";
            public const string UseOxygenOnSurface = "UseOxygenOnSurface";
            public const string RefillOxygenOnSurface = "RefillOxygenOnSurface";
            public const string OxygenRefillRate = "OxygenRefillRate";
        }

        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        public static void Init() {
            // ================== General ==================

            new PrivateHost();
            new DaysPerQuota();
            new RequireAllPlayersInDiveBell();
            new RequireDiveBellDoorClosed();

            // ================== Player ==================

            new Health();

            // ================== Oxygen ==================

            new Oxygen();
            new InfiniteOxygen();
            new OxygenUsageMultiplier();
            new SprintMultiplier();
            new UseOxygenInDiveBell();
            new RefillOxygenInDiveBell();
            new UseOxygenOnSurface();
            new RefillOxygenOnSurface();
            new OxygenRefillRate();
        }

        private class PrivateHost()
            : BoolOption(Keys.PrivateHost, true, "Host Privately (friends-only)", "GAMEPLAY", "GENERAL", []);

        private class DaysPerQuota() : IntOption(Keys.DaysPerQuota, 3, "Days Per Quota", 0, 30, "GAMEPLAY", "GENERAL",
            [SettingsUtil.UpdateQuotaDays], false);

        private class RequireAllPlayersInDiveBell() : BoolOption(Keys.RequireAllPlayersInDiveBell, true,
            "Require All Players in Dive Bell", "GAMEPLAY", "GENERAL", []);

        private class RequireDiveBellDoorClosed() : BoolOption(Keys.RequireDiveBellDoorClosed, true,
            "Require Dive Bell Door Closed", "GAMEPLAY", "GENERAL", []);

        private class Health() : FloatOption(Keys.Health, 100.0f, "Player Maximum Health", 0f, 1000f, "GAMEPLAY", "PLAYER", [],
            false);

        private class Oxygen() : FloatOption(Keys.Oxygen, 500.0f, "Maximum Oxygen (in seconds)", 0f, 2000f, "GAMEPLAY",
            "OXYGEN", [], false);

        private class InfiniteOxygen()
            : BoolOption(Keys.InfiniteOxygen, false, "Enable Infinite Oxygen", "GAMEPLAY", "OXYGEN", []);

        private class OxygenUsageMultiplier() : FloatOption(Keys.OxygenUsageMultiplier, 1.0f, "Oxygen Usage Multiplier", 0f,
            20f, "GAMEPLAY", "OXYGEN", [], false);

        private class SprintMultiplier() : FloatOption(Keys.SprintMultiplier, 1.0f, "Sprinting Oxygen Usage Multiplier", 1f,
            10f, "GAMEPLAY", "OXYGEN", [], false);

        private class UseOxygenInDiveBell() : BoolOption(Keys.UseOxygenInDiveBell, false, "Use Oxygen in Dive Bell", "GAMEPLAY",
            "OXYGEN", []);

        private class RefillOxygenInDiveBell() : BoolOption(Keys.RefillOxygenInDiveBell, false, "Refill Oxygen in Dive Bell",
            "GAMEPLAY", "OXYGEN", []);

        private class UseOxygenOnSurface() : BoolOption(Keys.UseOxygenOnSurface, false,
            "Use Oxygen on Surface (useful for debugging)", "GAMEPLAY", "OXYGEN", []);

        private class RefillOxygenOnSurface() : BoolOption(Keys.RefillOxygenOnSurface, true,
            "Refill Oxygen on Surface (useful for debugging)", "GAMEPLAY", "OXYGEN", []);

        private class OxygenRefillRate() : FloatOption(Keys.OxygenRefillRate, 20.0f, "Oxygen Refill Rate", 0f, 500f, "GAMEPLAY",
            "OXYGEN", [], false);
    }
}
