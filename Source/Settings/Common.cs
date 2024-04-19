using Zorro.Settings;

namespace ConfigurableWarning.Settings {
    public interface SettingHelper {
        public static T Setup<T>(T obj) where T : Setting {
            obj.ApplyValue();

            return obj;
        }

        public static void PostApply() {
            Plugin.Sync.SyncSettings();
        }
    }

    public abstract class CommonFloatSetting : FloatSetting {
        public abstract void Apply();
        
        public override void ApplyValue() {
            Apply();
            SettingHelper.PostApply();
        }

        public void Set(float value) {
            SetValue(value, GameHandler.Instance.SettingsHandler);
        }
    }

    public abstract class CommonIntSetting : IntSetting {
        public abstract void Apply();
        
        public override void ApplyValue() {
            Apply();
            SettingHelper.PostApply();
        }

        public void Set(int value) {
            SetValue(value, GameHandler.Instance.SettingsHandler);
        }
    }

    public abstract class CommonEnumSetting : EnumSetting {
        public abstract void Apply();
        
        public override void ApplyValue() {
            Apply();
            SettingHelper.PostApply();
        }

        public void Set(int value) {
            SetValue(value, GameHandler.Instance.SettingsHandler);
        }
    }
}
