using ConfigurableWarning.API.Attributes;
using ConfigurableWarning.API.Options;

namespace ConfigurableWarning.Settings;

/// <summary>
///     ConfigurableWarning's built-in settings.
/// </summary>
[Tab("GAMEPLAY")]
public static class BuiltInSettings {
    // TODO: Implement more settings:
    // - Spawning Mechanics
    // - Damage Taken Multiplier
    // - Customization Limits (revive https://thunderstore.io/c/content-warning/p/DiabolicalStudios/MoreCustomization/)
    // - Shop Prices
    // - Battery Settings
    // - MetaCoin Prices
    // - Save Slots (based on https://thunderstore.io/c/content-warning/p/Isbjorn52/More_Saves/)
    // - Gravity (based on https://thunderstore.io/c/content-warning/p/Marcus/Better_Gravity/)
    // - Intro Screen Skip (based on https://thunderstore.io/c/content-warning/p/CTWOriginals/DevelopmentStartup/)
    // - Video Save Location (based on https://thunderstore.io/c/content-warning/p/RamuneNeptune/CustomVideoSaveLocation/)
    // - Inventory Slots (based on https://thunderstore.io/c/content-warning/p/nickklmao/ExtraInventorySlot/)
    // - More Graphics Settings (basically revive https://thunderstore.io/c/content-warning/p/Jasson9/MoreSettings/)
    // - Revive https://thunderstore.io/c/content-warning/p/Computery/Toggle_Mute/

    [Group("GENERAL")]
    private static class General {
        [Register]
        private class PrivateHost()
            : BoolOption(SettingKeys.PrivateHost, true, "Host Privately (friends-only)");

        [Register]
        private class DaysPerQuota() : IntOption(SettingKeys.DaysPerQuota, 3, "Days Per Quota", 0, 30,
            [SettingsUtil.UpdateQuotaDays], false);

        [Register]
        private class RequireAllPlayersInDiveBell() : BoolOption(SettingKeys.RequireAllPlayersInDiveBell, true,
            "Require All Players in Dive Bell");

        [Register]
        private class RequireDiveBellDoorClosed() : BoolOption(SettingKeys.RequireDiveBellDoorClosed, true,
            "Require Dive Bell Door Closed");
        
        [Register]
        private class Fov() : FloatOption(SettingKeys.Fov, 70.0f, "Field of View", 20f, 120f, false);
    }

    [Group("PLAYER")]
    private static class Player {
        [Register]
        private class MaxHealth() : FloatOption(SettingKeys.Health, 100.0f, "Maximum Health", 0f, 1000f,
            false);

        [Register]
        private class MaxStamina() : FloatOption(SettingKeys.MaxStamina, 10.0f, "Maximum Stamina", 0f, 100f,
            false);

        [Register]
        private class StaminaRegenRate() : FloatOption(SettingKeys.StaminaRegenRate, 1.0f, "Stamina Regen Rate", 0f,
            100f,
            false);

        [Register]
        private class SprintSpeed() : FloatOption(SettingKeys.SprintSpeed, 2.0f, "Sprint Speed", 0f, 100f,
            false);
    }

    [Group("OXYGEN")]
    private static class Oxygen {
        [Register]
        private class MaxOxygen()
            : FloatOption(SettingKeys.Oxygen, 500.0f, "Maximum Oxygen (in seconds)", 0f, 2000f, false);

        [Register]
        private class InfiniteOxygen()
            : BoolOption(SettingKeys.InfiniteOxygen, false, "Enable Infinite Oxygen");

        [Register]
        private class OxygenUsageMultiplier() : FloatOption(SettingKeys.OxygenUsageMultiplier, 1.0f,
            "Oxygen Usage Multiplier", 0f,
            20f, false);

        [Register]
        private class SprintMultiplier() : FloatOption(SettingKeys.SprintMultiplier, 1.0f,
            "Sprinting Oxygen Usage Multiplier", 1f,
            10f, false);

        [Register]
        private class UseOxygenInDiveBell()
            : BoolOption(SettingKeys.UseOxygenInDiveBell, false, "Use Oxygen in Dive Bell");

        [Register]
        private class RefillOxygenInDiveBell() : BoolOption(SettingKeys.RefillOxygenInDiveBell, false,
            "Refill Oxygen in Dive Bell");

        [Register]
        private class UseOxygenOnSurface() : BoolOption(SettingKeys.UseOxygenOnSurface, false,
            "Use Oxygen on Surface (useful for debugging)");

        [Register]
        private class RefillOxygenOnSurface() : BoolOption(SettingKeys.RefillOxygenOnSurface, true,
            "Refill Oxygen on Surface (useful for debugging)");

        [Register]
        private class OxygenRefillRate()
            : FloatOption(SettingKeys.OxygenRefillRate, 20.0f, "Oxygen Refill Rate", 0f, 500f, false);
    }
}