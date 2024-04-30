using ContentSettings.API.Settings;
using Zorro.Settings;

using IntSetting = ContentSettings.API.Settings.IntSetting;

namespace ConfigurableWarning.Settings {
    public class SettingsUtil {
        public static void SetValue<T>(ref T setting, float value) where T: FloatSetting {
            setting.Value = setting.Clamp(value);
            GameHandler.Instance.SettingsHandler.SaveSetting(setting);
        }

        public static void SetValue<T>(ref T setting, int value) where T: IntSetting {
            setting.Value = setting.Clamp(value);
            GameHandler.Instance.SettingsHandler.SaveSetting(setting);
        }

        public static void SetValue<T>(ref T setting, bool value) where T: BoolSetting {
            setting.Value = value;
            GameHandler.Instance.SettingsHandler.SaveSetting(setting);
        }
    }
}