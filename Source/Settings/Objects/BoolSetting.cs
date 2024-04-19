using System.Collections.Generic;

namespace ConfigurableWarning.Settings.Objects {
    public abstract class BoolSetting : CommonEnumSetting {
        public abstract bool GetDefault();
        public bool GetValue() => Value != 0;
        
        public override int GetDefaultValue() => GetDefault() == true ? 1 : 0;
        public override List<string> GetChoices() => ["Off", "On"];

        public void Set(bool value) {
            SetValue(value ? 1 : 0, GameHandler.Instance.SettingsHandler);
        }
    }
}
