using System;
using JetBrains.Annotations;

namespace ConfigurableWarning.API.Attributes;

/// <summary>
///     Registers any contained options as a part of this category.
///     This is specifically for compat modules.
/// </summary>
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class CompatGroup : Attribute {
    /// <summary>
    ///     The category.
    /// </summary>
    public readonly string Category;

    /// <summary>
    ///     The tab. If this is null, it will default to the parent's
    ///     tab.
    /// </summary>
    public readonly string? Tab;
    
    /// <summary>
    ///     Registers any contained options as a part of this category.
    ///     This is specifically for compat modules.
    /// </summary>
    /// <param name="category">The category to register to.</param>
    public CompatGroup(string category) {
        Category = category;
        Tab = null;
    }

    /// <summary>
    ///     Registers any contained options as a part of this category,
    ///     overriding the parent's tab.
    ///     This is specifically for compat modules.
    /// </summary>
    /// <param name="tab">The tab</param>
    /// <param name="category">The category</param>
    public CompatGroup(string tab, string category) {
        Tab = tab;
        Category = category;
    }
}