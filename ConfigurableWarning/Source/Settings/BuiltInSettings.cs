using ConfigurableWarning.API;
using ConfigurableWarning.API.Options;

namespace ConfigurableWarning.Settings;

/// <summary>
///     ConfigurableWarning's built-in settings.
/// </summary>
public static class BuiltInSettings {
    // TODO: Implement more settings:
    // - Spawning Mechanics
    // - Customization Limits
    // - Camera Settings (Flashcard Support)
    // - Shop Prices
    // - Battery Settings
    // - MetaCoin Prices
    // - Gravity
    // - Intro Screen Skip
    // - Video Save Location
    // - More Graphics Settings?
    // - Player Limit? (Virality Config Support)

    [RegisterOption]
    private class PrivateHost()
        : BoolOption(SettingKeys.PrivateHost, true, "Host Privately (friends-only)", "GAMEPLAY", "GENERAL");

    [RegisterOption]
    private class DaysPerQuota() : IntOption(SettingKeys.DaysPerQuota, 3, "Days Per Quota", 0, 30, "GAMEPLAY", "GENERAL",
        [SettingsUtil.UpdateQuotaDays], false);

    [RegisterOption]
    private class RequireAllPlayersInDiveBell() : BoolOption(SettingKeys.RequireAllPlayersInDiveBell, true,
        "Require All Players in Dive Bell", "GAMEPLAY", "GENERAL");

    [RegisterOption]
    private class RequireDiveBellDoorClosed() : BoolOption(SettingKeys.RequireDiveBellDoorClosed, true,
        "Require Dive Bell Door Closed", "GAMEPLAY", "GENERAL");

    [RegisterOption]
    private class Health() : FloatOption(SettingKeys.Health, 100.0f, "Maximum Health", 0f, 1000f, "GAMEPLAY", "PLAYER",
        false);

    [RegisterOption]
    private class MaxStamina() : FloatOption(SettingKeys.MaxStamina, 10.0f, "Maximum Stamina", 0f, 100f, "GAMEPLAY", "PLAYER",
        false);

    [RegisterOption]
    private class StaminaRegenRate() : FloatOption(SettingKeys.StaminaRegenRate, 1.0f, "Stamina Regen Rate", 0f, 100f,
        "GAMEPLAY", "PLAYER", false);

    [RegisterOption]
    private class SprintSpeed() : FloatOption(SettingKeys.SprintSpeed, 2.0f, "Sprint Speed", 0f, 100f, "GAMEPLAY", "PLAYER",
        false);

    [RegisterOption]
    private class Oxygen() : FloatOption(SettingKeys.Oxygen, 500.0f, "Maximum Oxygen (in seconds)", 0f, 2000f, "GAMEPLAY",
        "OXYGEN", false);

    [RegisterOption]
    private class InfiniteOxygen()
        : BoolOption(SettingKeys.InfiniteOxygen, false, "Enable Infinite Oxygen", "GAMEPLAY", "OXYGEN");

    [RegisterOption]
    private class OxygenUsageMultiplier() : FloatOption(SettingKeys.OxygenUsageMultiplier, 1.0f, "Oxygen Usage Multiplier", 0f,
        20f, "GAMEPLAY", "OXYGEN", false);

    [RegisterOption]
    private class SprintMultiplier() : FloatOption(SettingKeys.SprintMultiplier, 1.0f, "Sprinting Oxygen Usage Multiplier", 1f,
        10f, "GAMEPLAY", "OXYGEN", false);

    [RegisterOption]
    private class UseOxygenInDiveBell() : BoolOption(SettingKeys.UseOxygenInDiveBell, false, "Use Oxygen in Dive Bell",
        "GAMEPLAY", "OXYGEN");

    [RegisterOption]
    private class RefillOxygenInDiveBell() : BoolOption(SettingKeys.RefillOxygenInDiveBell, false,
        "Refill Oxygen in Dive Bell", "GAMEPLAY", "OXYGEN");

    [RegisterOption]
    private class UseOxygenOnSurface() : BoolOption(SettingKeys.UseOxygenOnSurface, false,
        "Use Oxygen on Surface (useful for debugging)", "GAMEPLAY", "OXYGEN");

    [RegisterOption]
    private class RefillOxygenOnSurface() : BoolOption(SettingKeys.RefillOxygenOnSurface, true,
        "Refill Oxygen on Surface (useful for debugging)", "GAMEPLAY", "OXYGEN");

    [RegisterOption]
    private class OxygenRefillRate() : FloatOption(SettingKeys.OxygenRefillRate, 20.0f, "Oxygen Refill Rate", 0f, 500f,
        "GAMEPLAY", "OXYGEN", false);
}