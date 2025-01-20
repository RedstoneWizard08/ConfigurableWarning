using System.Collections.Generic;
using System.Linq;
using ConfigurableWarning.API.Internal;
using ConfigurableWarning.API.Options;
using Newtonsoft.Json;
using UnityEngine;

namespace ConfigurableWarning.API.State;

/// <summary>
///     The class responsible for storing options' values.
/// </summary>
public class OptionsState {
    private readonly Dictionary<string, object> _states = [];

    /// <summary>
    ///     The current instance of this <see cref="OptionsState" />.
    /// </summary>
    public static OptionsState Instance { get; } = new();

    /// <summary>
    ///     Registers a typed option to the state.
    /// </summary>
    /// <typeparam name="T">The option's value type.</typeparam>
    /// <param name="opt">The option to register.</param>
    public void Register<T>(IOption<T> opt) {
        if (Has(opt)) return;

        Set(opt, opt.GetValue());
    }

    /// <summary>
    ///     Sets the provided option's state.
    /// </summary>
    /// <typeparam name="T">The option's value type.</typeparam>
    /// <param name="opt">The option to set for.</param>
    /// <param name="value">The new value.</param>
    public void Set<T>(IOption<T> opt, T value) {
        _states[opt.GetName()] = value!;
    }

    /// <summary>
    ///     Sets the state based on the option's name.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="name">The option's name.</param>
    /// <param name="value">The new value.</param>
    public void Set<T>(string name, T value) {
        _states[name] = value!;
    }

    /// <summary>
    ///     Gets the value of an option.
    ///     This will forward to <see cref="Get{T}(string)" />
    ///     based on <see cref="IOption{T}.GetName" />.
    /// </summary>
    /// <typeparam name="T">The option's value type.</typeparam>
    /// <param name="opt">The option to reference.</param>
    /// <returns>The option's value.</returns>
    public T? Get<T>(IOption<T> opt) {
        return Get<T>(opt.GetName());
    }

    /// <summary>
    ///     Gets an option's value from the internal storage.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="name">The option's name.</param>
    /// <returns>The option's value.</returns>
    public T? Get<T>(string name) {
        var v = _states[name];

        // Json.NET is dumb and doesn't deserialize numbers as the correct type.

        return v switch {
            double d when typeof(T) == typeof(float) =>
                // This is dumb. Kill it with fire.
                (T) (object) (float) d,
            long l when typeof(T) == typeof(int) =>
                // This is dumb. Kill it with fire. (again)
                (T) (object) (int) l,
            long i when typeof(T) == typeof(KeyCode) =>
                // This is dumb. Kill it with fire. (again x2)
                (T) (object) (int) i,
            _ => (T) v
        };
    }

    /// <summary>
    ///     Returns true if the option is registered in the state.
    /// </summary>
    /// <typeparam name="T">The option's value type.</typeparam>
    /// <param name="opt">The option to reference.</param>
    /// <returns>True if the option has a value in the state.</returns>
    public bool Has<T>(IOption<T> opt) {
        return _states.ContainsKey(opt.GetName());
    }

    /// <summary>
    ///     Returns true if the option is registered in the state.
    /// </summary>
    /// <param name="name">The option's name.</param>
    /// <returns>True if the option has a value in the state.</returns>
    public bool Has(string name) {
        return _states.ContainsKey(name);
    }

    /// <summary>
    ///     Removes the option's value from the state, unregistering it.
    ///     This does NOT unregister it from the <see cref="OptionManager" />
    ///     *or* ContentSettings' <see cref="ContentSettings.API.SettingsLoader" />.
    /// </summary>
    /// <typeparam name="T">The option's value type.</typeparam>
    /// <param name="opt">The option to remove.</param>
    public void Remove<T>(IOption<T> opt) {
        _states.Remove(opt.GetName());
    }

    /// <summary>
    ///     Removes the option's value from the state, unregistering it.
    ///     This does NOT unregister it from the <see cref="OptionManager" />
    ///     *or* ContentSettings' <see cref="ContentSettings.API.SettingsLoader" />.
    /// </summary>
    /// <param name="name">The option's name.</param>
    public void Remove(string name) {
        _states.Remove(name);
    }

    /// <summary>
    ///     Clears all states from the internal dictionary,
    ///     unregistering all of its options.
    ///     This does NOT unregister ANYTHING from the <see cref="OptionManager" />
    ///     *or* ContentSettings' <see cref="ContentSettings.API.SettingsLoader" />.
    /// </summary>
    public void Clear() {
        _states.Clear();
    }

    /// <summary>
    ///     Updates an option's value in the state.
    ///     This will set the value in the state to its contained value.
    /// </summary>
    /// <typeparam name="T">The option's value type.</typeparam>
    /// <param name="opt">The option.</param>
    public void Update<T>(IOption<T> opt) {
        Set(opt, opt.GetValue());
    }

    /// <summary>
    ///     Collect and serialize (with <see cref="JsonConvert" />) all
    ///     registered states to a JSON string.
    /// </summary>
    /// <returns>The JSON-formatted states.</returns>
    public string Collect() {
        return JsonConvert.SerializeObject(_states.Where(it => OptionManager.Instance.Get(it.Key)?.Sync ?? false));
    }

    /// <summary>
    ///     Replaces all states with the JSON-encoded map provided,
    ///     removing any not present in the provided string, and
    ///     replacing all that are already present.
    /// </summary>
    /// <param name="json">The JSON-encoded map.</param>
    public void Apply(string json) {
        foreach (var item in JsonConvert.DeserializeObject<Dictionary<string, object>>(json)!) {
            // We don't want to overwrite non-synced options, so we just set the values instead of the whole dictionary.
            _states[item.Key] = item.Value;
        }
    }
}