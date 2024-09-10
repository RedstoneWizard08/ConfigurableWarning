using System;
using JetBrains.Annotations;

namespace ConfigurableWarning.API.Attributes;

/// <summary>
///     Registers any contained groups as a part of this tab.
///     This is specifically for compat modules.
/// </summary>
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class CompatTab(string tab) : Attribute {
    /// <summary>
    ///     The tab.
    /// </summary>
    public readonly string? Name = tab;
}