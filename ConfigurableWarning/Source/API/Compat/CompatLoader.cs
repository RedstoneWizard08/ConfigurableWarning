using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConfigurableWarning.API.Options;
using Zorro.Settings;

namespace ConfigurableWarning.API.Compat;

/// <summary>
///     A loader for compat modules.
/// </summary>
public static class CompatLoader {
    internal static Dictionary<Type, ICompatModule> RegisteredModules { get; } = new();

    internal static bool NamespaceExists(string ns) {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes().Select(t => t.Namespace))
            .Where(v => v != null)
            .Any(v => v!.StartsWith(ns + ".") || v == ns);
    }

    /// <summary>
    ///     Auto-loads all compat modules.
    /// </summary>
    public static void LoadModules() {
        foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())) {
            if (type.IsAbstract || type.IsInterface || !typeof(ICompatModule).IsAssignableFrom(type)) continue;
            if (RegisteredModules.ContainsKey(type)) continue;

            var module = type.GetCustomAttribute<CompatModule>(false);
            if (module == null) continue;

            Plugin.Logger.LogInfo($"Checking compat module: {type.Name} (from {type.Assembly.GetName().Name}.dll");

            var ok = true;

            foreach (var dep in module.Dependencies)
                if (NamespaceExists(dep)) {
                    Plugin.Logger.LogInfo($"Found namespace: {dep}");
                } else {
                    Plugin.Logger.LogInfo($"Could not find namespace: {dep}; Skipping...");
                    ok = false;
                    break;
                }

            if (!ok) continue;

            Plugin.Logger.LogInfo($"Initializing compat module: {type.Name}");

            foreach (var ty in type.GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Public)) {
                Plugin.Logger.LogInfo($"Checking compat setting: {ty.Name} (from {ty.Assembly.GetName().Name}.dll");

                if (ty.IsInterface || ty.IsAbstract || !ty.IsSubclassOf(typeof(Setting))) continue;

                var attr = ty.GetCustomAttribute<CompatSetting>(false);

                if (attr == null) continue;

                Plugin.Logger.LogInfo(
                    $"[COMPAT] Initializing option: {ty.Name} (from {ty.Assembly.GetName().Name}.dll");

                OptionLoader.RegisteredOptions[ty] = (IUntypedOption) Activator.CreateInstance(ty);
            }

            var it = (ICompatModule) Activator.CreateInstance(type);

            it.Init();
            RegisteredModules[type] = it;
        }
    }
}