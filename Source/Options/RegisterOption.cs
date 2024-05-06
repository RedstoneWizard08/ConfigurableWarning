using System;
using JetBrains.Annotations;

namespace ConfigurableWarning.Options {
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class RegisterOption : Attribute;
}
