using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConfigurableWarning.API.Attributes;
using ConfigurableWarning.API.Compat;
using ConfigurableWarning.API.Options;
using Zorro.Settings;

namespace ConfigurableWarning.API.Internal;

/// <summary>
///     Responsible for loading, holding, and registering options.
/// </summary>
public static class OptionLoader {
    /// <summary>
    ///     A list of all registered options.
    /// </summary>
    public static readonly Dictionary<Type, IUntypedOption> RegisteredOptions = new();

    /// <summary>
    ///     A list of all registered tabs.
    /// </summary>
    public static readonly Dictionary<Type, Tab> RegisteredTabs = new();

    /// <summary>
    ///     A list of all registered groups.
    /// </summary>
    public static readonly Dictionary<Type, Group> RegisteredGroups = new();

    /// <summary>
    ///     Automatically collect and register all options annotated with
    ///     <see cref="Register" />.
    /// </summary>
    public static void RegisterOptions() {
        foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())) {
            TryRegisterTab(type);
            TryRegisterGroup(null, type);
        }

        CompatLoader.LoadModules();
    }

    /// <summary>
    ///     Try to register a setting from a type.
    /// </summary>
    /// <param name="group">The group</param>
    /// <param name="type">The type</param>
    /// <param name="tab">The tab</param>
    /// <returns>If it could be registered</returns>
    public static bool TryRegisterSetting(string tab, string group, Type type) {
        if (type.IsInterface || type.IsAbstract || !type.IsSubclassOf(typeof(Setting))) return false;
        if (RegisteredOptions.ContainsKey(type)) return false;

        var register = type.GetCustomAttribute<Register>(false);
        if (register == null) return false;

        ConfigurableWarning.Logger.LogInfo($"Found setting {type.FullName}");

        var instance = (IUntypedOption)Activator.CreateInstance(type);

        instance.Register(tab, group);
        RegisteredOptions.Add(type, instance);

        return true;
    }

    /// <summary>
    ///     Try to register a group from a type.
    /// </summary>
    /// <param name="tab">The tab</param>
    /// <param name="type">The type</param>
    /// <returns>If it could be registered</returns>
    public static bool TryRegisterGroup(string? tab, Type type) {
        if (RegisteredGroups.ContainsKey(type)) return false;

        var group = type.GetCustomAttribute<Group>(false);
        if (group == null) return false;

        ConfigurableWarning.Logger.LogInfo($"Found group {group.Category} in {type.FullName}");

        if (tab == null && group.Tab == null) {
            ConfigurableWarning.Logger.LogError("Cannot register a Group with no tab!");
            return false;
        }

        var settings = type.GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Public);

        foreach (var setting in settings) TryRegisterSetting(tab ?? group.Tab!, group.Category, setting);

        RegisteredGroups.Add(type, group);

        return true;
    }

    /// <summary>
    ///     Try to register a tab from a type.
    /// </summary>
    /// <param name="type">The type</param>
    /// <returns>If it could be registered</returns>
    public static bool TryRegisterTab(Type type) {
        if (RegisteredTabs.ContainsKey(type)) return false;

        var tab = type.GetCustomAttribute<Tab>(false);
        if (tab == null) return false;

        ConfigurableWarning.Logger.LogInfo($"Found tab {tab.Name} in {type.FullName} (from {type.Assembly.GetName().Name}.dll");

        var subClasses = type.GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Public);

        foreach (var subClass in subClasses) TryRegisterGroup(tab.Name, subClass);

        RegisteredTabs.Add(type, tab);

        return true;
    }
}