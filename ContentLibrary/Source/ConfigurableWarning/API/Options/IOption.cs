using ConfigurableWarning.API.Internal;
using ConfigurableWarning.API.State;
using ContentSettings.API.Settings;

namespace ConfigurableWarning.API.Options;

/// <summary>
///     Represents an option. This interface is implemented
///     by all options.
/// </summary>
/// <typeparam name="T">The option's value type.</typeparam>
public interface IOption<T> : ICustomSetting, IUntypedOption, IHasStateHolder<T> {
    /// <summary>
    ///     Get or set this option's state.
    /// </summary>
    public T? State {
        get => OptionsState.Instance.Get<T>(GetName());
        set => OptionsState.Instance.Set(this, value!);
    }

    string IExposedSetting.GetDisplayName() {
        return GetDisplayName();
    }

    void IUntypedOption.Register(string tab, string category) {
        Register(tab, category);
    }

    /// <summary>
    ///     Gets this option's name. This is its name in the registry
    ///     and in the state holder.
    /// </summary>
    /// <returns>The option's name.</returns>
    new string GetName();

    /// <summary>
    ///     Get the display name of this option.
    /// </summary>
    /// <returns>The option's display name.</returns>
    new string GetDisplayName();

    /// <summary>
    ///     Get this option's default value.
    /// </summary>
    /// <returns>The option's default value.</returns>
    T GetDefaultValue();

    /// <summary>
    ///     Gets the current value of the option.
    ///     WARNING! THIS MAY NOT ALWAYS BE CORRECT! USE THE STATE HOLDER INSTEAD!
    /// </summary>
    /// <returns>The (potential) value of this option.</returns>
    new T GetValue();

    /// <summary>
    ///     Sets the option's value. This will NOT update it in the state!
    /// </summary>
    /// <param name="value">The new value.</param>
    void SetValue(T value);

    /// <summary>
    ///     Registers this setting with Content Settings.
    ///     You probably want to use <see cref="Register(string, string)" /> instead.
    /// </summary>
    /// <param name="tab">The tab to register to.</param>
    /// <param name="category">The category this option belongs to.</param>
    void RegisterSetting(string tab, string category);

    /// <summary>
    ///     Get this option as an <see cref="IUntypedOption" />
    /// </summary>
    /// <returns>The <see cref="IUntypedOption" /> form of this.</returns>
    IUntypedOption AsUntyped();

    /// <summary>
    ///     Get this as an <see cref="IOption{T}" />.
    ///     This is used for accessing default methods.
    /// </summary>
    /// <returns>This as an <see cref="IOption{T}" />.</returns>
    IOption<T> AsOption();

    /// <summary>
    ///     Registers this option to Content Settings, sets up its state,
    ///     and registers it to the <see cref="OptionManager" />.
    /// </summary>
    /// <param name="tab">The tab to register to.</param>
    /// <param name="category">The category this option belongs to.</param>
    public new void Register(string tab, string category) {
        RegisterSetting(tab, category);
        OptionManager.Instance.Register(this);
        OptionsState.Instance.Register(this);
    }

    /// <summary>
    ///     Get an instance of an option.
    /// </summary>
    /// <param name="name">The option's name.</param>
    /// <returns>The option.</returns>
    public static IOption<T>? Instance(string name) {
        return OptionManager.Instance.Get<T>(name);
    }
}