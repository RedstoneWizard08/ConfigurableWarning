using MyceliumNetworking;
using UnityEngine;

namespace ConfigurableWarning.Options {
    public class OptionSyncer {
        private string _lastSent = "";
        private string _lastRecv = "";

        public OptionSyncer() {
            MyceliumNetwork.RegisterNetworkObject(this, Plugin.ModID);
        }

        public void ResetCache() {
            _lastSent = "";
            _lastRecv = "";
        }

        [CustomRPC]
        public void SyncSettingsRecv(string data) {
            if (data == _lastRecv) return;
            _lastRecv = data;

            OptionsState.Instance.Apply(data);
        }

        public void SyncSettings() {
            var settings = OptionsState.Instance.Collect();

            if (settings == _lastSent) return;
            _lastSent = settings;

            MyceliumNetwork.RPC(Plugin.ModID, nameof(SyncSettingsRecv), ReliableType.Reliable, settings);
        }
    }
}
