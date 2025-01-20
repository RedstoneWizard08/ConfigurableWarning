namespace ConfigurableWarning.API.State;

/// <summary>
///     A state holder.
/// </summary>
/// <typeparam name="T">The option's type.</typeparam>
public class StateHolder<T> {
    /// <summary>
    ///     Get or set an option's state.
    /// </summary>
    /// <param name="name">The option.</param>
    /// <returns>Its state.</returns>
    public virtual T? this[string name] {
        get => OptionsState.Instance.Get<T>(name);
        set => OptionsState.Instance.Set(name, value!);
    }

    /// <summary>
    ///     Get an option's state or a default value.
    /// </summary>
    /// <param name="name">The option.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The value.</returns>
    public T GetOrDefault(string name, T defaultValue) {
        return OptionsState.Instance.Has(name) ? OptionsState.Instance.Get<T>(name)! : defaultValue;
    }
}