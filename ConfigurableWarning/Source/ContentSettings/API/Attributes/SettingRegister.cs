// -----------------------------------------------------------------------
// <copyright file="SettingRegister.cs" company="ContentSettings">
// Copyright (c) ContentSettings. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ContentSettings.API.Attributes;

using System;
using JetBrains.Annotations;

/// <summary>
/// Register a setting to the settings manager.
/// </summary>
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public class SettingRegister : Attribute {
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingRegister"/> class.
    /// </summary>
    /// <param name="tab">The name of the tab to register the setting to.</param>
    /// <param name="category">The name of the tab category to register the setting to.</param>
    public SettingRegister(string tab, string? category = null) {
        Tab = tab;
        Category = category;
    }

    /// <summary>
    /// Gets the name of the tab to register the setting to.
    /// </summary>
    public string Tab { get; }

    /// <summary>
    /// Gets the name of the tab category to register the setting to.
    /// </summary>
    public string? Category { get; }
}