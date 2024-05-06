using System.Collections.Generic;
using Zorro.Settings;

namespace ConfigurableWarning.Options;

/// <summary>
///     Manages options' loading, saving, registration, and accessing.
/// </summary>
public class OptionManager {
    private readonly Dictionary<string, IUntypedOption> _options = new();

    /// <summary>
    ///     The current instance of this <see cref="OptionManager" />.
    /// </summary>
    public static OptionManager Instance { get; } = new();

    /// <summary>
    ///     The internally used instance of the <see cref="DefaultSettingsSaveLoad" />.
    /// </summary>
    public static DefaultSettingsSaveLoad SaveLoader { get; } = new();

    /// <summary>
    ///     Registers a typed option, initializing and setting it up.
    ///     This will convert it to a <see cref="IUntypedOption" />.
    /// </summary>
    /// <typeparam name="T">The option's value type.</typeparam>
    /// <param name="opt">The option to register.</param>
    public void Register<T>(IOption<T> opt) {
        _options.Add(opt.GetName(), opt.AsUntyped());
    }

    /// <summary>
    ///     Registers an untyped option, initializing and setting it up.
    /// </summary>
    /// <param name="opt">The option to register.</param>
    public void Register(IUntypedOption opt) {
        _options.Add(opt.GetName(), opt);
    }

    /// <summary>
    ///     Gets an option by its name.
    /// </summary>
    /// <param name="name">The option's name.</param>
    /// <returns>The untyped form of the option.</returns>
    public IUntypedOption Get(string name) {
        return _options[name];
    }
}