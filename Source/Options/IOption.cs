using ContentSettings.API.Settings;

namespace ConfigurableWarning.Options {
    /// <summary>
    /// Represents an option. This interface is implemented
    /// by all options.
    /// </summary>
    /// <typeparam name="T">The option's value type.</typeparam>
    public interface IOption<T> : ICustomSetting {
        /// <summary>
        /// Gets this option's name. This is its name in the registry
        /// and in the state holder.
        /// </summary>
        /// <returns>The option's name.</returns>
        string GetName();

        /// <summary>
        /// Gets the current value of the option.
        /// WARNING! THIS MAY NOT ALWAYS BE CORRECT! USE THE STATE HOLDER INSTEAD!
        /// </summary>
        /// <returns>The (potential) value of this option.</returns>
        T GetValue();

        /// <summary>
        /// Sets the option's value. This will NOT update it in the state!
        /// </summary>
        /// <param name="value">The new value.</param>
        void SetValue(T value);
        
        /// <summary>
        /// Registers this setting with Content Settings.
        /// You probably want to use <see cref="Register(string, string)" /> instead.
        /// </summary>
        /// <param name="tab">The tab to register to.</param>
        /// <param name="category">The category this option belongs to.</param>
        void RegisterSetting(string tab, string category);

        /// <summary>
        /// Get this option as an <see cref="IUntypedOption"/>
        /// </summary>
        /// <returns>The <see cref="IUntypedOption" /> form of this.</returns>
        IUntypedOption AsUntyped();

        /// <summary>
        /// Registers this option to Content Settings, sets up its state,
        /// and registers it to the <see cref="OptionManager" />.
        /// </summary>
        /// <param name="tab">The tab to register to.</param>
        /// <param name="category">The category this option belongs to.</param>
        public void Register(string tab, string category) {
            RegisterSetting(tab, category);
            OptionManager.Instance.Register(this);
            OptionsState.Instance.Register(this);
        }
    }
}
