using System;
using JetBrains.Annotations;

namespace ConfigurableWarning.API.Attributes;

/// <summary>
///     Tells the ConfigurableWarning system to not sync this option.
///     This only works if the options was registered with the <see cref="Register"/> attribute.
/// </summary>
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class NoSync : Attribute;