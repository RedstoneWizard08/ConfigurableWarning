using System.Collections.Generic;
using ConfigurableWarning.Settings.Objects;
using ContentSettings.API.Attributes;
using ContentSettings.API.Settings;
using Unity.Mathematics;

namespace ConfigurableWarning.Settings {
    // -------------------- General -------------------- //

    [SettingRegister("GAMEPLAY", "GENERAL")]
    public class PrivateHost : BoolSetting, ICustomSetting {
        public override void Apply() => Plugin.Instance.PluginConfig.privateHost.Value = GetValue();
        public string GetDisplayName() => "Host Privately (friends-only)";
        public override bool GetDefault() => Plugin.Instance.PluginConfig.privateHost.Value;
    }

    [SettingRegister("GAMEPLAY", "GENERAL")]
    public class DaysPerQuota : CommonEnumSetting, ICustomSetting {
        public override void Apply() => Plugin.Instance.PluginConfig.daysPerQuota.Value = int.Parse(GetChoices()[Value]);
        public string GetDisplayName() => "Days Per Quota";
        public override int GetDefaultValue() => GetChoices().IndexOf(Plugin.Instance.PluginConfig.daysPerQuota.Value.ToString());
        public override List<string> GetChoices() => ["3", "5", "7", "10", "14", "18", "20"];
        public new void Set(int value) => Set(GetChoices().IndexOf(value.ToString()));
    }

    // -------------------- Player -------------------- //

    [SettingRegister("GAMEPLAY", "PLAYER")]
    public class Health : CommonFloatSetting, ICustomSetting {
        public override void Apply() => Plugin.Instance.PluginConfig.maxHealth.Value = Value;
        public string GetDisplayName() => "Player Maximum Health";
        public override float GetDefaultValue() => Plugin.Instance.PluginConfig.maxHealth.Value;
        public override float2 GetMinMaxValue() => new(0f, 1000f);
    }

    // -------------------- Oxygen -------------------- //

    [SettingRegister("GAMEPLAY", "OXYGEN")]
    public class Oxygen : CommonFloatSetting, ICustomSetting {
        public override void Apply() => Plugin.Instance.PluginConfig.maxOxygen.Value = Value;
        public string GetDisplayName() => "Maximum Oxygen (in seconds)";
        public override float GetDefaultValue() => Plugin.Instance.PluginConfig.maxOxygen.Value;
        public override float2 GetMinMaxValue() => new(0f, 2000f);
    }

    [SettingRegister("GAMEPLAY", "OXYGEN")]
    public class OxygenUsageMultiplier : CommonFloatSetting, ICustomSetting {
        public override void Apply() => Plugin.Instance.PluginConfig.oxygenUsage.Value = Value;
        public string GetDisplayName() => "Oxygen Usage Multiplier";
        public override float GetDefaultValue() => Plugin.Instance.PluginConfig.oxygenUsage.Value;
        public override float2 GetMinMaxValue() => new(0f, 20f);
    }

    [SettingRegister("GAMEPLAY", "OXYGEN")]
    public class SprintMultiplier : CommonFloatSetting, ICustomSetting {
        public override void Apply() => Plugin.Instance.PluginConfig.sprintUsage.Value = Value;
        public string GetDisplayName() => "Sprinting Oxygen Usage Multiplier";
        public override float GetDefaultValue() => Plugin.Instance.PluginConfig.sprintUsage.Value;
        public override float2 GetMinMaxValue() => new(1f, 10f);
    }

    [SettingRegister("GAMEPLAY", "OXYGEN")]
    public class UseOxygenInDiveBell : BoolSetting, ICustomSetting {
        public override void Apply() => Plugin.Instance.PluginConfig.useOxygenInDiveBell.Value = GetValue();
        public string GetDisplayName() => "Use Oxygen in Dive Bell";
        public override bool GetDefault() => Plugin.Instance.PluginConfig.useOxygenInDiveBell.Value;
    }

    [SettingRegister("GAMEPLAY", "OXYGEN")]
    public class RefillOxygenInDiveBell : BoolSetting, ICustomSetting {
        public override void Apply() => Plugin.Instance.PluginConfig.refillOxygenInDiveBell.Value = GetValue();
        public string GetDisplayName() => "Refill Oxygen in Dive Bell";
        public override bool GetDefault() => Plugin.Instance.PluginConfig.refillOxygenInDiveBell.Value;
    }

    [SettingRegister("GAMEPLAY", "OXYGEN")]
    public class UseOxygenOnSurface : BoolSetting, ICustomSetting {
        public override void Apply() => Plugin.Instance.PluginConfig.useOxygenOnSurface.Value = GetValue();
        public string GetDisplayName() => "Use Oxygen on Surface (useful for debugging)";
        public override bool GetDefault() => Plugin.Instance.PluginConfig.useOxygenOnSurface.Value;
    }

    [SettingRegister("GAMEPLAY", "OXYGEN")]
    public class RefillOxygenOnSurface : BoolSetting, ICustomSetting {
        public override void Apply() => Plugin.Instance.PluginConfig.refillOxygenOnSurface.Value = GetValue();
        public string GetDisplayName() => "Refill Oxygen on Surface (useful for debugging)";
        public override bool GetDefault() => Plugin.Instance.PluginConfig.refillOxygenOnSurface.Value;
    }

    [SettingRegister("GAMEPLAY", "OXYGEN")]
    public class OxygenRefillRate : CommonFloatSetting, ICustomSetting {
        public override void Apply() => Plugin.Instance.PluginConfig.oxygenRefillRate.Value = Value;
        public string GetDisplayName() => "Oxygen Refill Rate";
        public override float GetDefaultValue() => Plugin.Instance.PluginConfig.oxygenRefillRate.Value;
        public override float2 GetMinMaxValue() => new(0f, 500f);
    }
}
