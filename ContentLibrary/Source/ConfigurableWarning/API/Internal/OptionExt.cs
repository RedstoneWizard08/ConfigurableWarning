using ConfigurableWarning.API.Options;

namespace ConfigurableWarning.API.Internal;

/// <summary>
///     Extension Methods.
/// </summary>
public static class OptionExt {
    /// <summary>
    ///     Convert this string into an option.
    /// </summary>
    /// <param name="name">The string.</param>
    /// <returns>The option.</returns>
    public static BoolOption? BoolOpt(this string name) {
        return BoolOption.Instance(name);
    }

    /// <summary>
    ///     Convert this string into an option.
    /// </summary>
    /// <param name="name">The string.</param>
    /// <returns>The option.</returns>
    public static FloatOption? FloatOpt(this string name) {
        return FloatOption.Instance(name);
    }

    /// <summary>
    ///     Convert this string into an option.
    /// </summary>
    /// <param name="name">The string.</param>
    /// <returns>The option.</returns>
    public static IntOption? IntOpt(this string name) {
        return IntOption.Instance(name);
    }

    /// <summary>
    ///     Convert this string into an option.
    /// </summary>
    /// <param name="name">The string.</param>
    /// <returns>The option.</returns>
    public static TextOption? TextOpt(this string name) {
        return TextOption.Instance(name);
    }

    /// <summary>
    ///     Convert this string into an option.
    /// </summary>
    /// <param name="name">The string.</param>
    /// <returns>The option.</returns>
    public static EnumOption<T>? EnumOpt<T>(this string name) where T : struct {
        return EnumOption<T>.Instance(name);
    }

    /// <summary>
    ///     Converts this string into an option and gets its state.
    /// </summary>
    /// <param name="opt">The string.</param>
    /// <returns>The state.</returns>
    public static bool BoolState(this string opt) {
        return opt.BoolOpt()!.State;
    }

    /// <summary>
    ///     Converts this string into an option and gets its state.
    /// </summary>
    /// <param name="opt">The string.</param>
    /// <returns>The state.</returns>
    public static float? FloatState(this string opt) {
        return opt.FloatOpt()?.State;
    }

    /// <summary>
    ///     Converts this string into an option and gets its state.
    /// </summary>
    /// <param name="opt">The string.</param>
    /// <returns>The state.</returns>
    public static int? IntState(this string opt) {
        return opt.IntOpt()?.State;
    }

    /// <summary>
    ///     Converts this string into an option and gets its state.
    /// </summary>
    /// <param name="opt">The string.</param>
    /// <returns>The state.</returns>
    public static string? TextState(this string opt) {
        return opt.TextOpt()?.State;
    }

    /// <summary>
    ///     Converts this string into an option and gets its state.
    /// </summary>
    /// <param name="opt">The string.</param>
    /// <returns>The state.</returns>
    public static T? EnumState<T>(this string opt) where T : struct {
        return opt.EnumOpt<T>()?.State;
    }
}