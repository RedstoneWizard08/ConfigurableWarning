namespace ConfigurableWarning.API.State;

/// <summary>
///     A state holder for EnumOptions.
/// </summary>
public sealed class EnumStateHolder {
    /// <summary>
    ///     Get an option's state.
    /// </summary>
    /// <param name="name">The option.</param>
    /// <returns>Its state.</returns>
    // ReSharper disable once MemberCanBeMadeStatic.Global
    public T? Get<T>(string name) {
        return OptionsState.Instance.Get<T>(name);
    }

    /// <summary>
    ///     Set an option's state.
    /// </summary>
    /// <param name="name">The option.</param>
    /// <param name="value">The new state.</param>
    public void Set<T>(string name, T value) {
        OptionsState.Instance.Set(name, value!);
    }
}