using ConfigurableWarning.API;
using ConfigurableWarning.API.Options;

namespace ConfigurableWarning.Settings;

/// <summary>
///     ConfigurableWarning's settings utilities.
/// </summary>
public static class SettingsUtil {
    /// <summary>
    ///     Updates remaining days in the game's quota days counter.
    /// </summary>
    /// <param name="_opt">The option (see <see cref="SettingKeys.DaysPerQuota" />).</param>
    public static void UpdateQuotaDays(IntOption _opt) {
        if (SurfaceNetworkHandler.RoomStats != null)
            SurfaceNetworkHandler.RoomStats.DaysPerQutoa = States.Ints[SettingKeys.DaysPerQuota];
    }
}