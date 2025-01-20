namespace ContentLibrary.API.State;

/// <summary>
///     The state of the diving bell.
/// </summary>
public enum DivingBellState {
    /// <summary>
    ///     The diving bell is ready to be used.
    /// </summary>
    Ready,

    /// <summary>
    ///     The diving bell is not ready to be used.
    /// </summary>
    NotReady,

    /// <summary>
    ///     The diving bell is missing players and cannot be used.
    /// </summary>
    MissingPlayers,

    /// <summary>
    ///     The diving bell is currently recharging.
    /// </summary>
    Recharging,

    /// <summary>
    ///     A custom state that can be set by mods.
    /// </summary>
    Custom
}