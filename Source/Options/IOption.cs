using ContentSettings.API.Settings;

namespace ConfigurableWarning.Options {
    public interface IOption<T> : ICustomSetting {
        string GetName();
        T GetValue();
        void SetValue(T value);
        void RegisterSetting(string tab, string category);
        IUntypedOption AsUntyped();

        public void Register(string tab, string category) {
            RegisterSetting(tab, category);
            OptionManager.Instance.Register(this);
            OptionsState.Instance.Register(this);
        }
    }
}
