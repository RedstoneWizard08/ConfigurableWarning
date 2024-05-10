using System;
using JetBrains.Annotations;

namespace ConfigurableWarning.API.Compat;

/// <summary>
///     Declare a compat module.
/// </summary>
/// <param name="dependencies">A list of namespaces this module depends on.</param>
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class CompatModule(string[] dependencies) : Attribute {
    /// <summary>
    ///     A list of namespaces this module depends on.
    /// </summary>
    public string[] Dependencies = dependencies;
}
