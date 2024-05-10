namespace ConfigurableWarning.API;

/// <summary>
///     State helpers.
/// </summary>
public static class States {
    /// <summary>
    ///     Boolean states.
    /// </summary>
    public static readonly StateHolder<bool> Bools = new();

    /// <summary>
    ///     Float states.
    /// </summary>
    public static readonly StateHolder<float> Floats = new();

    /// <summary>
    ///     Int states.
    /// </summary>
    public static readonly StateHolder<int> Ints = new();

    /// <summary>
    ///     String states.
    /// </summary>
    public static readonly StateHolder<string> Strings = new();
}
