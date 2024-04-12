using Zorro.Settings;

namespace ConfigurableWarning.Settings {
    public interface SettingHelper {
        public static T Setup<T>(T obj) where T : Setting {
            SettingsLoader.RegisterSetting(obj);
            obj.ApplyValue();

            return obj;
        }
    }

    public abstract class CommonFloatSetting : FloatSetting {
        public void Set(float value) {
            SetValue(value, GameHandler.Instance.SettingsHandler);
        }
    }

    public abstract class CommonIntSetting : IntSetting {
        public void Set(int value) {
            SetValue(value, GameHandler.Instance.SettingsHandler);
        }
    }

    public abstract class CommonEnumSetting : EnumSetting {
        public void Set(int value) {
            SetValue(value, GameHandler.Instance.SettingsHandler);
        }
    }
}
