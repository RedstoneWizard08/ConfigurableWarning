using System;

namespace ConfigurableWarning.API;

public static class EnumUtil {
    public static string[] GetOptions<T>() where T : struct {
        return Enum.GetNames(typeof(T));
    }

    public static T Parse<T>(string value) where T : struct {
        return (T) Enum.Parse(typeof(T), value);
    }

    public static int GetIndex<T>(T value) where T : struct {
        return Array.IndexOf(GetOptions<T>(), value.ToString());
    }
}