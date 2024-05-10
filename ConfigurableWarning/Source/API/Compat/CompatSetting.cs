using System;
using JetBrains.Annotations;

namespace ConfigurableWarning.API.Compat;

/// <summary>
///     Automatically registers the option. This will initialize it in the state,
///     register it with Content Settings, and load its default value if the value
///     is not present.
/// </summary>
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class CompatSetting : Attribute;
