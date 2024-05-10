using System;
using System.Linq;
using System.Reflection;
using ConfigurableWarning.API.Options;
using Zorro.Settings;

namespace ConfigurableWarning.API.Compat;

/// <summary>
///     A loader for compat modules.
/// </summary>
public static class CompatLoader {
    internal static bool NamespaceExists(string ns) {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes().Select(t => t.Namespace))
            .Any(v => v.StartsWith(ns + ".") || v == ns);
    }

    /// <summary>
    ///     Auto-loads all compat modules.
    /// </summary>
    public static void LoadModules() {
        foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())) {
            if (!type.IsClass || !type.IsSubclassOf(typeof(ICompatModule))) continue;

            var module = type.GetCustomAttribute<CompatModule>(false);

            if (module == null) continue;

            Plugin.Logger.LogInfo($"Checking compat module: {type.Name} (from {type.Assembly.GetName().Name}.dll");

            foreach (var dep in module.Dependencies) {
                if (NamespaceExists(dep)) {
                    Plugin.Logger.LogInfo($"Found namespace: {dep}");
                } else {
                    Plugin.Logger.LogInfo($"Could not find namespace: {dep}; Skipping...");
                    continue;
                }
            }

            Plugin.Logger.LogInfo($"Initializing compat module: {type.Name}");

            foreach (var ty in type.GetNestedTypes()) {
                if (ty.IsInterface || ty.IsAbstract || !ty.IsSubclassOf(typeof(Setting))) continue;

                var attr = ty.GetCustomAttribute<CompatSetting>(false);

                if (attr == null) continue;

                OptionLoader.RegisteredOptions[type] = (IUntypedOption) Activator.CreateInstance(ty);
            }

            var it = (ICompatModule) Activator.CreateInstance(type);

            it.Init();
        }
    }
}
