// -----------------------------------------------------------------------
// <copyright file="ICustomSetting.cs" company="ContentSettings">
// Copyright (c) ContentSettings. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ContentSettings.API.Settings;

using JetBrains.Annotations;

/// <summary>
/// Interface for all custom settings to implement.
/// </summary>
[UsedImplicitly]
public interface ICustomSetting : IExposedSetting {
    /// <summary>
    /// This is called when the setting is loaded but currently does nothing.
    /// </summary>
    /// <remarks>
    /// Unfortunately, due to the SettingCategory being an enum, it is not possible for us to implement this method.
    /// </remarks>
    /// <returns>The setting category of the setting.</returns>
    SettingCategory IExposedSetting.GetSettingCategory() {
        return SettingCategory.Controls;
    }
}