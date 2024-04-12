using ConfigurableWarning.Settings.UI;
using UnityEngine;
using Zorro.Core;
using Zorro.Settings;
using Zorro.Settings.DebugUI;

namespace ConfigurableWarning.Settings.Objects {
    public class TextSetting : Setting, IExposedSetting {
        public string Name;

        public TextSetting(string name) {
            Name = $"----- {name} -----";
        }

        public override void Load(ISettingsSaveLoad loader) { }

        public override void Save(ISettingsSaveLoad saver) { }

        public override void ApplyValue() { }

        public override SettingUI GetDebugUI(ISettingHandler settingHandler) {
            return new TextSettingsUI(this);
        }

        public override GameObject GetSettingUICell() {
            return SingletonAsset<InputCellMapper>.Instance.KeyCodeSettingCell;
        }

        public SettingCategory GetSettingCategory() => SettingCategory.Controls;

        public string GetDisplayName() => Name;
    }
}