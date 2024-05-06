using System.Diagnostics.CodeAnalysis;
using ConfigurableWarning.Options;

namespace ConfigurableWarning.Settings;

public static class BuiltInSettings {
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public static class Keys {
        public const string PrivateHost = "PrivateHost";
        public const string DaysPerQuota = "DaysPerQuota";
        public const string RequireAllPlayersInDiveBell = "RequireAllPlayersInDiveBell";
        public const string RequireDiveBellDoorClosed = "RequireDiveBellDoorClosed";

        public const string Health = "Health";
        public const string MaxStamina = "MaxStamina";
        public const string StaminaRegenRate = "StaminaRegenRate";
        public const string SprintSpeed = "SprintSpeed";

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

    [RegisterOption]
    private class PrivateHost()
        : BoolOption(Keys.PrivateHost, true, "Host Privately (friends-only)", "GAMEPLAY", "GENERAL", []);

    [RegisterOption]
    private class DaysPerQuota() : IntOption(Keys.DaysPerQuota, 3, "Days Per Quota", 0, 30, "GAMEPLAY", "GENERAL",
        [SettingsUtil.UpdateQuotaDays], false);

    [RegisterOption]
    private class RequireAllPlayersInDiveBell() : BoolOption(Keys.RequireAllPlayersInDiveBell, true,
        "Require All Players in Dive Bell", "GAMEPLAY", "GENERAL", []);

    [RegisterOption]
    private class RequireDiveBellDoorClosed() : BoolOption(Keys.RequireDiveBellDoorClosed, true,
        "Require Dive Bell Door Closed", "GAMEPLAY", "GENERAL", []);

    [RegisterOption]
    private class Health() : FloatOption(Keys.Health, 100.0f, "Maximum Health", 0f, 1000f, "GAMEPLAY", "PLAYER", [],
        false);

    [RegisterOption]
    private class MaxStamina() : FloatOption(Keys.MaxStamina, 10.0f, "Maximum Stamina", 0f, 100f, "GAMEPLAY", "PLAYER",
        [],
        false);

    [RegisterOption]
    private class StaminaRegenRate() : FloatOption(Keys.StaminaRegenRate, 1.0f, "Stamina Regen Rate", 0f, 100f,
        "GAMEPLAY", "PLAYER", [],
        false);

    [RegisterOption]
    private class SprintSpeed() : FloatOption(Keys.SprintSpeed, 2.0f, "Sprint Speed", 0f, 100f, "GAMEPLAY", "PLAYER",
        [],
        false);

    [RegisterOption]
    private class Oxygen() : FloatOption(Keys.Oxygen, 500.0f, "Maximum Oxygen (in seconds)", 0f, 2000f, "GAMEPLAY",
        "OXYGEN", [], false);

    [RegisterOption]
    private class InfiniteOxygen()
        : BoolOption(Keys.InfiniteOxygen, false, "Enable Infinite Oxygen", "GAMEPLAY", "OXYGEN", []);

    [RegisterOption]
    private class OxygenUsageMultiplier() : FloatOption(Keys.OxygenUsageMultiplier, 1.0f, "Oxygen Usage Multiplier", 0f,
        20f, "GAMEPLAY", "OXYGEN", [], false);

    [RegisterOption]
    private class SprintMultiplier() : FloatOption(Keys.SprintMultiplier, 1.0f, "Sprinting Oxygen Usage Multiplier", 1f,
        10f, "GAMEPLAY", "OXYGEN", [], false);

    [RegisterOption]
    private class UseOxygenInDiveBell() : BoolOption(Keys.UseOxygenInDiveBell, false, "Use Oxygen in Dive Bell",
        "GAMEPLAY",
        "OXYGEN", []);

    [RegisterOption]
    private class RefillOxygenInDiveBell() : BoolOption(Keys.RefillOxygenInDiveBell, false,
        "Refill Oxygen in Dive Bell",
        "GAMEPLAY", "OXYGEN", []);

    [RegisterOption]
    private class UseOxygenOnSurface() : BoolOption(Keys.UseOxygenOnSurface, false,
        "Use Oxygen on Surface (useful for debugging)", "GAMEPLAY", "OXYGEN", []);

    [RegisterOption]
    private class RefillOxygenOnSurface() : BoolOption(Keys.RefillOxygenOnSurface, true,
        "Refill Oxygen on Surface (useful for debugging)", "GAMEPLAY", "OXYGEN", []);

    [RegisterOption]
    private class OxygenRefillRate() : FloatOption(Keys.OxygenRefillRate, 20.0f, "Oxygen Refill Rate", 0f, 500f,
        "GAMEPLAY",
        "OXYGEN", [], false);
}