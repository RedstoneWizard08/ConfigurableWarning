using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConfigurableWarning.API.Attributes;
using ConfigurableWarning.API.Internal;

namespace ConfigurableWarning.API.Compat;

/// <summary>
///     A loader for compat modules.
/// </summary>
public static class CompatLoader {
    /// <summary>
    ///     A list of all registered compat modules.
    /// </summary>
    public static readonly Dictionary<Type, ICompatModule> RegisteredModules = new();

    /// <summary>
    ///     A list of all registered compat tabs.
    /// </summary>
    public static readonly Dictionary<Type, CompatTab> RegisteredTabs = new();

    /// <summary>
    ///     A list of all registered compat groups.
    /// </summary>
    public static readonly Dictionary<Type, CompatGroup> RegisteredGroups = new();

    /// <summary>
    ///     Checks if a namespace exists.
    /// </summary>
    /// <param name="ns">The namespace.</param>
    /// <returns>If it exists.</returns>
    public static bool NamespaceExists(string ns) {
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

            ConfigurableWarningEntry.Logger.LogInfo(
                $"Checking compat module: {type.Name} (from {type.Assembly.GetName().Name}.dll");

            var ok = true;

            foreach (var dep in module.Dependencies)
                if (NamespaceExists(dep)) {
                    ConfigurableWarningEntry.Logger.LogInfo($"Found namespace: {dep}");
                } else {
                    ConfigurableWarningEntry.Logger.LogInfo($"Could not find namespace: {dep}; Skipping...");
                    ok = false;
                    break;
                }

            if (!ok) continue;

            ConfigurableWarningEntry.Logger.LogInfo($"Initializing compat module: {type.Name}");

            foreach (var ty in type.GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Public)) {
                TryRegisterCompatTab(ty);
                TryRegisterCompatGroup(null, ty);
            }

            var it = (ICompatModule) Activator.CreateInstance(type);

            it.Init();
            RegisteredModules[type] = it;
        }
    }

    /// <summary>
    ///     Try to register a compat group from a type.
    /// </summary>
    /// <param name="tab">The tab</param>
    /// <param name="type">The type</param>
    /// <returns>If it could be registered</returns>
    public static bool TryRegisterCompatGroup(string? tab, Type type) {
        if (RegisteredGroups.ContainsKey(type)) return false;

        var group = type.GetCustomAttribute<CompatGroup>(false);
        if (group == null) return false;

        ConfigurableWarningEntry.Logger.LogInfo($"Found compat group {group.Category} in {type.FullName}");

        if (tab == null && group.Tab == null) {
            ConfigurableWarningEntry.Logger.LogError("Cannot register a CompatGroup with no tab!");
            return false;
        }

        var settings = type.GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Public);

        foreach (var setting in settings) OptionLoader.TryRegisterSetting(tab ?? group.Tab!, group.Category, setting);

        RegisteredGroups.Add(type, group);

        return true;
    }

    /// <summary>
    ///     Try to register a compat tab from a type.
    /// </summary>
    /// <param name="type">The type</param>
    /// <returns>If it could be registered</returns>
    public static bool TryRegisterCompatTab(Type type) {
        if (RegisteredTabs.ContainsKey(type)) return false;

        var tab = type.GetCustomAttribute<CompatTab>(false);
        if (tab == null) return false;

        ConfigurableWarningEntry.Logger.LogInfo(
            $"Found compat tab {tab.Name} in {type.FullName} (from {type.Assembly.GetName().Name}.dll");

        var subClasses = type.GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Public);

        foreach (var subClass in subClasses) {
            OptionLoader.TryRegisterGroup(tab.Name, subClass);
            TryRegisterCompatGroup(tab.Name, subClass);
        }

        RegisteredTabs.Add(type, tab);

        return true;
    }
}