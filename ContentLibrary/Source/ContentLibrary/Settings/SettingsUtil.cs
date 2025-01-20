#pragma warning disable IDE0060

using ConfigurableWarning.API.Options;
using ConfigurableWarning.API.State;

namespace ContentLibrary.Settings;

/// <summary>
///     ConfigurableWarning's settings utilities.
/// </summary>
public static class SettingsUtil {
    /// <summary>
    ///     Updates remaining days in the game's quota days counter.
    /// </summary>
    /// <param name="opt">The option (see <see cref="SettingKeys.DaysPerQuota" />).</param>
    public static void UpdateQuotaDays(IntOption opt) {
        if (SurfaceNetworkHandler.RoomStats != null)
            SurfaceNetworkHandler.RoomStats.DaysPerQutoa = States.Ints[SettingKeys.DaysPerQuota];
    }
}