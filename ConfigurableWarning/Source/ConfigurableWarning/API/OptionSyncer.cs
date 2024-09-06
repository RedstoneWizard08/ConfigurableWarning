using MyceliumNetworking;

namespace ConfigurableWarning.API;

/// <summary>
///     Handles syncing all the options in <see cref="OptionsState" />
///     using the <see cref="MyceliumNetwork" /> API.
/// </summary>
public class OptionSyncer {
    private string _lastRecv = "";
    private string _lastSent = "";

    /// <summary>
    ///     Create a new instance of this.
    ///     This should only be called by ConfigurableWarning's base API.
    /// </summary>
    internal OptionSyncer() {
        MyceliumNetwork.RegisterNetworkObject(this, ConfigurableWarning.ModID);
    }

    /// <summary>
    ///     Resets cached values to force a re-sync.
    /// </summary>
    public void ResetCache() {
        _lastSent = "";
        _lastRecv = "";
    }

    /// <summary>
    ///     The receiving end of the sync RPC.
    ///     Applies new values when called.
    /// </summary>
    /// <param name="data">The JSON-encoded data string.</param>
    [CustomRPC]
    public void SyncSettingsRecv(string data) {
        if (data == _lastRecv) return;
        _lastRecv = data;

        OptionsState.Instance.Apply(data);
    }

    /// <summary>
    ///     Send the data to all connected clients
    ///     and sync current values in the <see cref="OptionsState" />.
    /// </summary>
    public void SyncSettings() {
        var settings = OptionsState.Instance.Collect();

        if (settings == _lastSent) return;
        _lastSent = settings;

        MyceliumNetwork.RPC(ConfigurableWarning.ModID, nameof(SyncSettingsRecv), ReliableType.Reliable, settings);
    }
}