using System;
using MyceliumNetworking;

namespace ConfigurableWarning.Settings {
    public class SyncRPC {
        public void Register() {
            MyceliumNetwork.RegisterNetworkObject(this, Plugin.MOD_ID);
        }

        [CustomRPC]
        public void SyncSettingsRecv(string data) {
            Console.WriteLine("Received: " + data);
            PackedSettings.unpack(data);
        }

        public void SyncSettings() {
            var settings = PackedSettings.collect().pack();
            Console.WriteLine("Sending: " + settings);

            MyceliumNetwork.RPC(Plugin.MOD_ID, nameof(SyncSettingsRecv), ReliableType.Reliable, settings);
        }
    }
}
