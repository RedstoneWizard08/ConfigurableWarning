using System;
using JetBrains.Annotations;

namespace ConfigurableWarning.API.Attributes;

/// <summary>
///     Registers any contained groups as a part of this tab.
/// </summary>
/// <param name="tab">The tab.</param>
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class Tab(string tab) : Attribute {
    /// <summary>
    ///     The tab.
    /// </summary>
    public readonly string? Name = tab;
}
