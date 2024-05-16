using System;
using JetBrains.Annotations;

namespace ConfigurableWarning.API.Attributes;

/// <summary>
///     Registers any contained groups as a part of this tab.
/// </summary>
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class Tab : Attribute {
    /// <summary>
    ///     The tab.
    /// </summary>
    public readonly string? Name;

    /// <summary>
    ///     Registers any contained groups as a part of this tab.
    /// </summary>
    /// <param name="tab">The tab.</param>
    public Tab(string tab) {
        Name = tab;
    }
}