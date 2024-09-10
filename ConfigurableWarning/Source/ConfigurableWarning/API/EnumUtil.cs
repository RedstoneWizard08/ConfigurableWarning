using System;

namespace ConfigurableWarning.API;

/// <summary>
///     Utilities for enums.
/// </summary>
public static class EnumUtil {
    /// <summary>
    ///     Get the options for an enum.
    /// </summary>
    /// <returns>A list of names for the enum.</returns>
    public static string[] GetOptions<T>() where T : struct {
        return Enum.GetNames(typeof(T));
    }

    /// <summary>
    ///     Get an enum value by it's name.
    /// </summary>
    /// <param name="value">The name of the enum value.</param>
    /// <returns>The enum value.</returns>
    public static T Parse<T>(string value) where T : struct {
        return (T)Enum.Parse(typeof(T), value);
    }

    /// <summary>
    ///     Get an enum value's index/ordinal.
    /// </summary>
    /// <param name="value">The enum value.</param>
    /// <returns>The index of the value.</returns>
    public static int GetIndex<T>(T value) where T : struct {
        return Array.IndexOf(GetOptions<T>(), value.ToString());
    }
}