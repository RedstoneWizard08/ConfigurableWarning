using System.Collections.Generic;
using Unity.Mathematics;

namespace ConfigurableWarning.Settings {
    public class PrivateHost : CommonEnumSetting, IExposedSetting {
        public override void ApplyValue() => Plugin.Instance.PluginConfig.privateHost.Value = Value != 0;
        public string GetDisplayName() => "Host Privately (friends-only)";
        public SettingCategory GetSettingCategory() => SettingCategory.Controls;
        public override int GetDefaultValue() => Plugin.Instance.PluginConfig.privateHost.Value == true ? 1 : 0;
        public override List<string> GetChoices() => ["Off", "On"];
    }

    public class Oxygen : CommonFloatSetting, IExposedSetting {
        public override void ApplyValue() => Plugin.Instance.PluginConfig.maxOxygen.Value = Value;
        public string GetDisplayName() => "Maximum Oxygen (in seconds)";
        public SettingCategory GetSettingCategory() => SettingCategory.Controls;
        public override float GetDefaultValue() => Plugin.Instance.PluginConfig.maxOxygen.Value;
        public override float2 GetMinMaxValue() => new(0f, 2000f);
    }

    public class OxygenUsageMultiplier : CommonFloatSetting, IExposedSetting {
        public override void ApplyValue() => Plugin.Instance.PluginConfig.oxygenUsageMultiplier.Value = Value;
        public string GetDisplayName() => "Oxygen Usage Multiplier";
        public SettingCategory GetSettingCategory() => SettingCategory.Controls;
        public override float GetDefaultValue() => Plugin.Instance.PluginConfig.oxygenUsageMultiplier.Value;
        public override float2 GetMinMaxValue() => new(0f, 20f);
    }

    public class SprintMultiplier : CommonFloatSetting, IExposedSetting {
        public override void ApplyValue() => Plugin.Instance.PluginConfig.sprintMultiplier.Value = Value;
        public string GetDisplayName() => "Sprinting Oxygen Usage Multiplier";
        public SettingCategory GetSettingCategory() => SettingCategory.Controls;
        public override float GetDefaultValue() => Plugin.Instance.PluginConfig.sprintMultiplier.Value;
        public override float2 GetMinMaxValue() => new(1f, 10f);
    }

    public class UseOxygenInDiveBell : CommonEnumSetting, IExposedSetting {
        public override void ApplyValue() => Plugin.Instance.PluginConfig.useOxygenInDiveBell.Value = Value != 0;
        public string GetDisplayName() => "Use Oxygen in Dive Bell";
        public SettingCategory GetSettingCategory() => SettingCategory.Controls;
        public override int GetDefaultValue() => Plugin.Instance.PluginConfig.useOxygenInDiveBell.Value == true ? 1 : 0;
        public override List<string> GetChoices() => ["Off", "On"];
    }

    public class RefillOxygenInDiveBell : CommonEnumSetting, IExposedSetting {
        public override void ApplyValue() => Plugin.Instance.PluginConfig.refillOxygenInDiveBell.Value = Value != 0;
        public string GetDisplayName() => "Refill Oxygen in Dive Bell";
        public SettingCategory GetSettingCategory() => SettingCategory.Controls;
        public override int GetDefaultValue() => Plugin.Instance.PluginConfig.refillOxygenInDiveBell.Value == true ? 1 : 0;
        public override List<string> GetChoices() => ["Off", "On"];
    }

    public class UseOxygenOnSurface : CommonEnumSetting, IExposedSetting {
        public override void ApplyValue() => Plugin.Instance.PluginConfig.useOxygenOnSurface.Value = Value != 0;
        public string GetDisplayName() => "Use Oxygen on Surface (useful for debugging)";
        public SettingCategory GetSettingCategory() => SettingCategory.Controls;
        public override int GetDefaultValue() => Plugin.Instance.PluginConfig.useOxygenOnSurface.Value == true ? 1 : 0;
        public override List<string> GetChoices() => ["Off", "On"];
    }

    public class RefillOxygenOnSurface : CommonEnumSetting, IExposedSetting {
        public override void ApplyValue() => Plugin.Instance.PluginConfig.refillOxygenOnSurface.Value = Value != 0;
        public string GetDisplayName() => "Refill Oxygen on Surface (useful for debugging)";
        public SettingCategory GetSettingCategory() => SettingCategory.Controls;
        public override int GetDefaultValue() => Plugin.Instance.PluginConfig.refillOxygenOnSurface.Value == true ? 1 : 0;
        public override List<string> GetChoices() => ["Off", "On"];
    }

    public class OxygenRefillRate : CommonFloatSetting, IExposedSetting {
        public override void ApplyValue() => Plugin.Instance.PluginConfig.oxygenRefillRate.Value = Value;
        public string GetDisplayName() => "Oxygen Refill Rate";
        public SettingCategory GetSettingCategory() => SettingCategory.Controls;
        public override float GetDefaultValue() => Plugin.Instance.PluginConfig.oxygenRefillRate.Value;
        public override float2 GetMinMaxValue() => new(0f, 500f);
    }

    public class Health : CommonFloatSetting, IExposedSetting {
        public override void ApplyValue() => Plugin.Instance.PluginConfig.maxHealth.Value = Value;
        public string GetDisplayName() => "Maximum Health";
        public SettingCategory GetSettingCategory() => SettingCategory.Controls;
        public override float GetDefaultValue() => Plugin.Instance.PluginConfig.maxHealth.Value;
        public override float2 GetMinMaxValue() => new(0f, 1000f);
    }

    public class DaysPerQuota : CommonEnumSetting, IExposedSetting {
        public override void ApplyValue() => Plugin.Instance.PluginConfig.daysPerQuota.Value = int.Parse(GetChoices()[Value]);
        public string GetDisplayName() => "Days Per Quota";
        public SettingCategory GetSettingCategory() => SettingCategory.Controls;
        public override int GetDefaultValue() => GetChoices().IndexOf(Plugin.Instance.PluginConfig.daysPerQuota.Value.ToString());
        public override List<string> GetChoices() => ["3", "5", "7", "10", "14", "18", "20"];
        public void SetX(int value) => Set(GetChoices().IndexOf(value.ToString()));
    }
}
