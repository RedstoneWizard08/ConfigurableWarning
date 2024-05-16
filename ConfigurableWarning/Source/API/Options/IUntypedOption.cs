namespace ConfigurableWarning.API.Options;

/// <summary>
///     A simplified version of <see cref="IOption{T}" /> that has no type
///     argument, and uses objects instead (with casts).
/// </summary>
public interface IUntypedOption {
    /// <summary>
    ///     Gets this option's name. This is its name in the registry
    ///     and in the state holder.
    /// </summary>
    /// <returns>The option's name.</returns>
    string GetName();

    /// <summary>
    ///     Gets the current value of the option.
    ///     WARNING! THIS MAY NOT ALWAYS BE CORRECT! USE THE STATE HOLDER INSTEAD!
    /// </summary>
    /// <returns>The (potential) value of this option.</returns>
    object GetValue();

    /// <summary>
    ///     Sets the option's value. This will NOT update it in the state!
    /// </summary>
    /// <param name="value">The new value.</param>
    void SetValue(object value);

    /// <summary>
    ///     Registers this option to Content Settings, sets up its state,
    ///     and registers it to the <see cref="OptionManager" />.
    /// </summary>
    /// <param name="tab">The tab to register to.</param>
    /// <param name="category">The category this option belongs to.</param>
    public void Register(string tab, string category);
}