using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Zorro.Settings;

namespace ConfigurableWarning.Options {
    public static class OptionLoader {
        private static Dictionary<Type, IUntypedOption> RegisteredOptions { get; } = new();

        public static void RegisterOptions() {
            foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())) {
                if (type.IsAbstract || type.IsInterface || !type.IsSubclassOf(typeof(Setting))) {
                    continue;
                }

                if (RegisteredOptions.ContainsKey(type)) {
                    continue;
                }

                var register = type.GetCustomAttribute<RegisterOption>(false);

                if (register != null) {
                    var opt = (IOption<object>)Activator.CreateInstance(type);
                    var uopt = opt.AsUntyped();

                    RegisteredOptions[type] = uopt;
                }
            }
        }
    }
}
