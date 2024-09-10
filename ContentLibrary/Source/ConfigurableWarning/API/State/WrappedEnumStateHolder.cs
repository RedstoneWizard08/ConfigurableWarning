namespace ConfigurableWarning.API.State;

/// <summary>
///     A state holder for EnumOptions.
/// </summary>
public sealed class WrappedEnumStateHolder<T> : StateHolder<T> {
    /// <inheritdoc />
    public override T? this[string name] {
        get => Get(name);
        set => Set(name, value!);
    }

    /// <summary>
    ///     Get an option's state.
    /// </summary>
    /// <param name="name">The option.</param>
    /// <returns>Its state.</returns>
    // ReSharper disable once MemberCanBeMadeStatic.Global
    public T? Get(string name) {
        return OptionsState.Instance.Get<T>(name);
    }

    /// <summary>
    ///     Set an option's state.
    /// </summary>
    /// <param name="name">The option.</param>
    /// <param name="value">The new state.</param>
    public void Set(string name, T value) {
        OptionsState.Instance.Set(name, value!);
    }
}