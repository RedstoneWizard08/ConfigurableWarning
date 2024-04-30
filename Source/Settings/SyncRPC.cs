using UnityEngine;
using MyceliumNetworking;

namespace ConfigurableWarning.Settings {
    public class SyncRPC {
        private string lastSent = "";
        private string lastRecv = "";

        public SyncRPC() {
            MyceliumNetwork.RegisterNetworkObject(this, Plugin.MOD_ID);
        }

        public void ResetCache() {
            lastSent = "";
            lastRecv = "";
        }

        [CustomRPC]
        public void SyncSettingsRecv(string data) {
            if (data == lastRecv) return;
            lastRecv = data;

            PackedSettings.Unpack(data);
        }

        public void SyncSettings() {
            var settings = PackedSettings.Collect().Pack();

            if (settings == lastSent) return;
            lastSent = settings;

            MyceliumNetwork.RPC(Plugin.MOD_ID, nameof(SyncSettingsRecv), ReliableType.Reliable, settings);
        }
    }
}
