using ConfigurableWarning.Options;

namespace ConfigurableWarning.Settings;

public static class SettingsUtil {
    public static void UpdateQuotaDays(IntOption opt) {
        if (SurfaceNetworkHandler.RoomStats != null)
            SurfaceNetworkHandler.RoomStats.DaysPerQutoa =
                OptionsState.Instance.Get<int>(BuiltInSettings.Keys.DaysPerQuota);
    }
}