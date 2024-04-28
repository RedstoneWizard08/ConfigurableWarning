using ContentSettings.API.Attributes;
using ContentSettings.API.Settings;
using Unity.Mathematics;
using Zorro.Settings;

using IntSetting = ContentSettings.API.Settings.IntSetting;

namespace ConfigurableWarning.Settings {
    // -------------------- General -------------------- //

    [SettingRegister("GAMEPLAY", "GENERAL")]
    public class PrivateHost : BoolSetting, ICustomSetting {
        public override void ApplyValue() => Plugin.Sync.SyncSettings();
        public string GetDisplayName() => "Host Privately (friends-only)";
        public override bool GetDefaultValue() => true;
    }

    [SettingRegister("GAMEPLAY", "GENERAL")]
    public class DaysPerQuota : IntSetting, ICustomSetting {
        public override void ApplyValue() => Plugin.Sync.SyncSettings();
        public string GetDisplayName() => "Days Per Quota";
        public override int GetDefaultValue() => 3;
        public override (int, int) GetMinMaxValue() => (0, 30);
    }

    // -------------------- Player -------------------- //

    [SettingRegister("GAMEPLAY", "PLAYER")]
    public class Health : FloatSetting, ICustomSetting {
        public override void ApplyValue() => Plugin.Sync.SyncSettings();
        public string GetDisplayName() => "Player Maximum Health";
        public override float GetDefaultValue() => 100.0f;
        public override float2 GetMinMaxValue() => new(0f, 1000f);
    }

    // -------------------- Oxygen -------------------- //

    [SettingRegister("GAMEPLAY", "OXYGEN")]
    public class Oxygen : FloatSetting, ICustomSetting {
        public override void ApplyValue() => Plugin.Sync.SyncSettings();
        public string GetDisplayName() => "Maximum Oxygen (in seconds)";
        public override float GetDefaultValue() => 500.0f;
        public override float2 GetMinMaxValue() => new(0f, 2000f);
    }

    [SettingRegister("GAMEPLAY", "OXYGEN")]
    public class OxygenUsageMultiplier : FloatSetting, ICustomSetting {
        public override void ApplyValue() => Plugin.Sync.SyncSettings();
        public string GetDisplayName() => "Oxygen Usage Multiplier";
        public override float GetDefaultValue() => 1.0f;
        public override float2 GetMinMaxValue() => new(0f, 20f);
    }

    [SettingRegister("GAMEPLAY", "OXYGEN")]
    public class SprintMultiplier : FloatSetting, ICustomSetting {
        public override void ApplyValue() => Plugin.Sync.SyncSettings();
        public string GetDisplayName() => "Sprinting Oxygen Usage Multiplier";
        public override float GetDefaultValue() => 1.0f;
        public override float2 GetMinMaxValue() => new(1f, 10f);
    }

    [SettingRegister("GAMEPLAY", "OXYGEN")]
    public class UseOxygenInDiveBell : BoolSetting, ICustomSetting {
        public override void ApplyValue() => Plugin.Sync.SyncSettings();
        public string GetDisplayName() => "Use Oxygen in Dive Bell";
        public override bool GetDefaultValue() => false;
    }

    [SettingRegister("GAMEPLAY", "OXYGEN")]
    public class RefillOxygenInDiveBell : BoolSetting, ICustomSetting {
        public override void ApplyValue() => Plugin.Sync.SyncSettings();
        public string GetDisplayName() => "Refill Oxygen in Dive Bell";
        public override bool GetDefaultValue() => false;
    }

    [SettingRegister("GAMEPLAY", "OXYGEN")]
    public class UseOxygenOnSurface : BoolSetting, ICustomSetting {
        public override void ApplyValue() => Plugin.Sync.SyncSettings();
        public string GetDisplayName() => "Use Oxygen on Surface (useful for debugging)";
        public override bool GetDefaultValue() => false;
    }

    [SettingRegister("GAMEPLAY", "OXYGEN")]
    public class RefillOxygenOnSurface : BoolSetting, ICustomSetting {
        public override void ApplyValue() => Plugin.Sync.SyncSettings();
        public string GetDisplayName() => "Refill Oxygen on Surface (useful for debugging)";
        public override bool GetDefaultValue() => true;
    }

    [SettingRegister("GAMEPLAY", "OXYGEN")]
    public class OxygenRefillRate : FloatSetting, ICustomSetting {
        public override void ApplyValue() => Plugin.Sync.SyncSettings();
        public string GetDisplayName() => "Oxygen Refill Rate";
        public override float GetDefaultValue() => 20.0f;
        public override float2 GetMinMaxValue() => new(0f, 500f);
    }
}
