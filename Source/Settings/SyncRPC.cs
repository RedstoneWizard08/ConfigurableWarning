using UnityEngine;
using MyceliumNetworking;

namespace ConfigurableWarning.Settings {
    public class SyncRPC {
        private string lastSent = "";
        private string lastRecv = "";

        public SyncRPC() {
            MyceliumNetwork.RegisterNetworkObject(this, Plugin.MOD_ID);
        }

        [CustomRPC]
        public void SyncSettingsRecv(string data) {
            if (data == lastRecv) return;
            lastRecv = data;

            Debug.Log("R: " + data);

            PackedSettings.Unpack(data);
        }

        public void SyncSettings() {
            var settings = PackedSettings.Collect().Pack();

            if (settings == lastSent) return;
            lastSent = settings;

            Debug.Log("S: " + settings);

            MyceliumNetwork.RPC(Plugin.MOD_ID, nameof(SyncSettingsRecv), ReliableType.Reliable, settings);
        }
    }
}
