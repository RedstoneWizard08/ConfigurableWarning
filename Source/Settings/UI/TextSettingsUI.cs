using ConfigurableWarning.Settings.Objects;
using UnityEngine.UIElements;
using Zorro.Core;
using Zorro.Settings.DebugUI;

namespace ConfigurableWarning.Settings.UI {
    public class TextSettingsUI : SettingUI {
        public TextSettingsUI(TextSetting setting) {
            SingletonAsset<SettingUxmls>.Instance.FloatSettingUxml.CloneTree(this);
            Label label = (Label)UQueryExtensions.Q(this, "SettingName", (string)null);

            label.text = setting.GetType().Name;
        }
    }
}
