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
}