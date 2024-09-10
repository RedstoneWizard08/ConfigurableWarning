using System;
using JetBrains.Annotations;

namespace ConfigurableWarning.API.Attributes;

/// <summary>
///     Registers any contained options as a part of this category.
/// </summary>
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class Group : Attribute {
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
    /// </summary>
    /// <param name="category">The category to register to.</param>
    public Group(string category) {
        Category = category;
        Tab = null;
    }

    /// <summary>
    ///     Registers any contained options as a part of this category,
    ///     overriding the parent's tab.
    /// </summary>
    /// <param name="tab">The tab</param>
    /// <param name="category">The category</param>
    public Group(string tab, string category) {
        Tab = tab;
        Category = category;
    }
}