using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Zorro.Settings;

namespace ConfigurableWarning {
    namespace Settings {
        public class SettingHandler {
            public static void OnChange(Action onChange) {
                onChange();

                // I was going to do netcode stuff, but Photon sucks
                // (or I just don't know how to use it properly)
            }
        }

        public class PrivateHost : EnumSetting, IExposedSetting {
            public override void ApplyValue() => SettingHandler.OnChange(() => Plugin.Instance.config.privateHost.Value = Value != 0);
            public string GetDisplayName() => "Host Privately (friends-only)";
            public SettingCategory GetSettingCategory() => SettingCategory.Controls;
            protected override int GetDefaultValue() => Plugin.Instance.config.privateHost.Value == true ? 1 : 0;
            public override List<string> GetChoices() => ["Off", "On"];
        }

        public class Oxygen : FloatSetting, IExposedSetting {
            public override void ApplyValue() => SettingHandler.OnChange(() => Plugin.Instance.config.maxOxygen.Value = Value);
            public string GetDisplayName() => "Maximum Oxygen (in seconds)";
            public SettingCategory GetSettingCategory() => SettingCategory.Controls;
            protected override float GetDefaultValue() => Plugin.Instance.config.maxOxygen.Value;
            protected override float2 GetMinMaxValue() => new(0f, 2000f);
        }

        public class OxygenUsageMultiplier : FloatSetting, IExposedSetting {
            public override void ApplyValue() => SettingHandler.OnChange(() => Plugin.Instance.config.oxygenUsageMultiplier.Value = Value);
            public string GetDisplayName() => "Oxygen Usage Multiplier";
            public SettingCategory GetSettingCategory() => SettingCategory.Controls;
            protected override float GetDefaultValue() => Plugin.Instance.config.oxygenUsageMultiplier.Value;
            protected override float2 GetMinMaxValue() => new(0f, 20f);
        }

        public class SprintMultiplier : FloatSetting, IExposedSetting {
            public override void ApplyValue() => SettingHandler.OnChange(() => Plugin.Instance.config.sprintMultiplier.Value = Value);
            public string GetDisplayName() => "Sprinting Oxygen Usage Multiplier";
            public SettingCategory GetSettingCategory() => SettingCategory.Controls;
            protected override float GetDefaultValue() => Plugin.Instance.config.sprintMultiplier.Value;
            protected override float2 GetMinMaxValue() => new(1f, 10f);
        }

        public class UseOxygenInDiveBell : EnumSetting, IExposedSetting {
            public override void ApplyValue() => SettingHandler.OnChange(() => Plugin.Instance.config.useOxygenInDiveBell.Value = Value != 0);
            public string GetDisplayName() => "Use Oxygen in Dive Bell";
            public SettingCategory GetSettingCategory() => SettingCategory.Controls;
            protected override int GetDefaultValue() => Plugin.Instance.config.useOxygenInDiveBell.Value == true ? 1 : 0;
            public override List<string> GetChoices() => ["Off", "On"];
        }

        public class RefillOxygenInDiveBell : EnumSetting, IExposedSetting {
            public override void ApplyValue() => SettingHandler.OnChange(() => Plugin.Instance.config.refillOxygenInDiveBell.Value = Value != 0);
            public string GetDisplayName() => "Refill Oxygen in Dive Bell";
            public SettingCategory GetSettingCategory() => SettingCategory.Controls;
            protected override int GetDefaultValue() => Plugin.Instance.config.refillOxygenInDiveBell.Value == true ? 1 : 0;
            public override List<string> GetChoices() => ["Off", "On"];
        }

        public class UseOxygenOnSurface : EnumSetting, IExposedSetting {
            public override void ApplyValue() => SettingHandler.OnChange(() => Plugin.Instance.config.useOxygenOnSurface.Value = Value != 0);
            public string GetDisplayName() => "Use Oxygen on Surface (useful for debugging)";
            public SettingCategory GetSettingCategory() => SettingCategory.Controls;
            protected override int GetDefaultValue() => Plugin.Instance.config.useOxygenOnSurface.Value == true ? 1 : 0;
            public override List<string> GetChoices() => ["Off", "On"];
        }

        public class RefillOxygenOnSurface : EnumSetting, IExposedSetting {
            public override void ApplyValue() => SettingHandler.OnChange(() => Plugin.Instance.config.refillOxygenOnSurface.Value = Value != 0);
            public string GetDisplayName() => "Refill Oxygen on Surface (useful for debugging)";
            public SettingCategory GetSettingCategory() => SettingCategory.Controls;
            protected override int GetDefaultValue() => Plugin.Instance.config.refillOxygenOnSurface.Value == true ? 1 : 0;
            public override List<string> GetChoices() => ["Off", "On"];
        }

        public class OxygenRefillRate : FloatSetting, IExposedSetting {
            public override void ApplyValue() => SettingHandler.OnChange(() => Plugin.Instance.config.oxygenRefillRate.Value = Value);
            public string GetDisplayName() => "Oxygen Refill Rate";
            public SettingCategory GetSettingCategory() => SettingCategory.Controls;
            protected override float GetDefaultValue() => Plugin.Instance.config.oxygenRefillRate.Value;
            protected override float2 GetMinMaxValue() => new(0f, 500f);
        }

        public class Health : FloatSetting, IExposedSetting {
            public override void ApplyValue() => SettingHandler.OnChange(() => Plugin.Instance.config.maxHealth.Value = Value);
            public string GetDisplayName() => "Maximum Health";
            public SettingCategory GetSettingCategory() => SettingCategory.Controls;
            protected override float GetDefaultValue() => Plugin.Instance.config.maxHealth.Value;
            protected override float2 GetMinMaxValue() => new(0f, 1000f);
        }

        public class DaysPerQuota : EnumSetting, IExposedSetting {
            public override void ApplyValue() => SettingHandler.OnChange(() => Plugin.Instance.config.daysPerQuota.Value = int.Parse(GetChoices()[Value]));
            public string GetDisplayName() => "Days Per Quota";
            public SettingCategory GetSettingCategory() => SettingCategory.Controls;
            protected override int GetDefaultValue() => GetChoices().IndexOf(Plugin.Instance.config.daysPerQuota.Value.ToString());
            public override List<string> GetChoices() => ["3", "5", "7", "10", "14", "18", "20"];
            public void SetValueX(int value, ISettingHandler handler) => SetValue(GetChoices().IndexOf(value.ToString()), handler);
        }
    }
}
