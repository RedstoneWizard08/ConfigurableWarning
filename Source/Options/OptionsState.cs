using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace ConfigurableWarning.Options {
    public class OptionsState {
        public static OptionsState Instance { get; } = new();
        private Dictionary<string, object> _states = new();

        public void Register<T>(IOption<T> opt) {
            if (!Has(opt)) {
                Debug.Log($"Registering option {opt.GetName()} with initial value {opt.GetValue()}");
                Set(opt, opt.GetValue());
            }
        }

        public void Set<T>(IOption<T> opt, T value) {
            _states[opt.GetName()] = value;
        }

        public void Set<T>(string name, T value) {
            _states[name] = value;
        }

        public T Get<T>(IOption<T> opt) {
            return (T)_states[opt.GetName()];
        }

        public T Get<T>(string name) {
            var v = _states[name];

            // Json.NET is dumb and doesn't deserialize numbers as the correct type.

            return v switch {
                double d when typeof(T) == typeof(float) =>
                    // This is dumb. Kill it with fire.
                    (T) (object) (float) d,
                long l when typeof(T) == typeof(int) =>
                    // This is dumb. Kill it with fire. (again)
                    (T) (object) (int) l,
                _ => (T) v
            };
        }

        public bool Has<T>(IOption<T> opt) {
            return _states.ContainsKey(opt.GetName());
        }

        public bool Has<T>(string name) {
            return _states.ContainsKey(name);
        }

        public void Remove<T>(IOption<T> opt) {
            _states.Remove(opt.GetName());
        }

        public void Remove<T>(string name) {
            _states.Remove(name);
        }

        public void Clear() {
            _states.Clear();
        }

        public void Update<T>(IOption<T> opt) {
            Set(opt, opt.GetValue());
        }

        public string Collect() {
            return JsonConvert.SerializeObject(_states);
        }

        public void Apply(string json) {
            _states = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }
    }
}