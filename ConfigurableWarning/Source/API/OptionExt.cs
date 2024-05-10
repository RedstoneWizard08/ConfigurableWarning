using ConfigurableWarning.API.Options;

namespace ConfigurableWarning.API;

/// <summary>
///     Extension Methods.
/// </summary>
public static class OptionExt {
    /// <summary>
    ///     Convert this string into an option.
    /// </summary>
    /// <param name="name">The string.</param>
    /// <returns>The option.</returns>
    public static BoolOption? BoolOpt(this string name) => BoolOption.Instance(name);

    /// <summary>
    ///     Convert this string into an option.
    /// </summary>
    /// <param name="name">The string.</param>
    /// <returns>The option.</returns>
    public static FloatOption? FloatOpt(this string name) => FloatOption.Instance(name);

    /// <summary>
    ///     Convert this string into an option.
    /// </summary>
    /// <param name="name">The string.</param>
    /// <returns>The option.</returns>
    public static IntOption? IntOpt(this string name) => IntOption.Instance(name);

    /// <summary>
    ///     Convert this string into an option.
    /// </summary>
    /// <param name="name">The string.</param>
    /// <returns>The option.</returns>
    public static TextOption? TextOpt(this string name) => TextOption.Instance(name);

    /// <summary>
    ///     Converts this string into an option and gets its state.
    /// </summary>
    /// <param name="opt">The string.</param>
    /// <returns>The state.</returns>
    public static bool BoolState(this string opt) => opt.BoolOpt()!.State;

    /// <summary>
    ///     Converts this string into an option and gets its state.
    /// </summary>
    /// <param name="opt">The string.</param>
    /// <returns>The state.</returns>
    public static float? FloatState(this string opt) => opt.FloatOpt()?.State;

    /// <summary>
    ///     Converts this string into an option and gets its state.
    /// </summary>
    /// <param name="opt">The string.</param>
    /// <returns>The state.</returns>
    public static int? IntState(this string opt) => opt.IntOpt()?.State;

    /// <summary>
    ///     Converts this string into an option and gets its state.
    /// </summary>
    /// <param name="opt">The string.</param>
    /// <returns>The state.</returns>
    public static string? TextState(this string opt) => opt.TextOpt()?.State;
}
