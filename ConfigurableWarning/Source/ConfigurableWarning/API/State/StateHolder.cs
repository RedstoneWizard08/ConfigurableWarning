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
}