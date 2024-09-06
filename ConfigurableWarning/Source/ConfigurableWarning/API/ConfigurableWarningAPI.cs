namespace ConfigurableWarning.API;

/// <summary>
///     ConfigurableWarning's main API entrypoint.
/// </summary>
public static class ConfigurableWarningAPI {
    /// <summary>
    ///     The global instance of <see cref="OptionSyncer" />.
    /// </summary>
    public static OptionSyncer Sync { get; private set; } = null!;

    internal static void Init() {
        ConfigurableWarning.Logger.LogInfo("Initializing ConfigurableWarning API...");

        Sync = new OptionSyncer();
    }
}