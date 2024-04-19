using MyceliumNetworking;

namespace ConfigurableWarning.Settings {
    public class SettingsSync {
        public void Register() {
            MyceliumNetwork.RegisterNetworkObject(this, Plugin.MOD_ID);
        }

        [CustomRPC]
        public void SyncSettingsRecv(PackedSettings settings) {
            settings.unpack();
        }

        public void SyncSettings() {
            var settings = PackedSettings.pack();

            MyceliumNetwork.RPC(Plugin.MOD_ID, nameof(SyncSettingsRecv), ReliableType.Reliable, settings);
        }
    }
}
