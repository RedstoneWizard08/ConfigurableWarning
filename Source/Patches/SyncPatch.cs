using HarmonyLib;
using Photon.Pun;
using Zorro.Core.Serizalization;
using Zorro.Core;
using Unity.Collections;

namespace ConfigurableWarning.Patches {
    [HarmonyPatch]
    internal class SyncPatch {
        internal static Player GetPlayer(PlayerSyncer __instance) => (Player)AccessTools.Field(typeof(PlayerSyncer), "player").GetValue(__instance);

        [HarmonyPrefix]
        [HarmonyPatch(typeof(PlayerSyncer), nameof(PlayerSyncer.OnPhotonSerializeView))]
        internal static bool OnPhotonSerializeView(PlayerSyncer __instance, PhotonStream stream, PhotonMessageInfo info) {
            if (stream.IsWriting) {
                // 5 floats, 4 bools, 1 int
                // 4 * 5 = 20, 4 * 1 = 4, 1 * 4 = 4
                // 20 + 4 + 4 = 28
                const int size = 28;

                var binarySerializer = new BinarySerializer(size, Allocator.Temp);

                binarySerializer.WriteFloat(Plugin.Instance.PluginConfig.maxOxygen.Value);
                binarySerializer.WriteFloat(Plugin.Instance.PluginConfig.oxygenUsageMultiplier.Value);
                binarySerializer.WriteFloat(Plugin.Instance.PluginConfig.sprintMultiplier.Value);
                binarySerializer.WriteBool(Plugin.Instance.PluginConfig.useOxygenInDiveBell.Value);
                binarySerializer.WriteBool(Plugin.Instance.PluginConfig.refillOxygenInDiveBell.Value);
                binarySerializer.WriteBool(Plugin.Instance.PluginConfig.useOxygenOnSurface.Value);
                binarySerializer.WriteBool(Plugin.Instance.PluginConfig.refillOxygenOnSurface.Value);
                binarySerializer.WriteFloat(Plugin.Instance.PluginConfig.oxygenRefillRate.Value);
                binarySerializer.WriteFloat(Plugin.Instance.PluginConfig.maxHealth.Value);
                binarySerializer.WriteInt(Plugin.Instance.PluginConfig.daysPerQuota.Value);

                var array = binarySerializer.buffer.ToByteArray();

                stream.SendNext(array);
                binarySerializer.Dispose();
            } else {
                if (GetPlayer(__instance) == null || GetPlayer(__instance).data == null) {
                    return true;
                }

                var binaryDeserializer = new BinaryDeserializer((byte[])stream.ReceiveNext(), Allocator.Temp);

                if (PhotonNetwork.IsMasterClient) {
                    // Host config shouldn't change here.
                    binaryDeserializer.ReadFloat();
                    binaryDeserializer.ReadFloat();
                    binaryDeserializer.ReadFloat();
                    binaryDeserializer.ReadBool();
                    binaryDeserializer.ReadBool();
                    binaryDeserializer.ReadBool();
                    binaryDeserializer.ReadBool();
                    binaryDeserializer.ReadFloat();
                    binaryDeserializer.ReadFloat();
                    binaryDeserializer.ReadInt();
                } else {
                    Plugin.Instance.PluginSettings.maxOxygen.Set(binaryDeserializer.ReadFloat());
                    Plugin.Instance.PluginSettings.oxygenUsage.Set(binaryDeserializer.ReadFloat());
                    Plugin.Instance.PluginSettings.sprintUsage.Set(binaryDeserializer.ReadFloat());
                    Plugin.Instance.PluginSettings.useOxygenInDiveBell.Set(binaryDeserializer.ReadBool() == true ? 1 : 0);
                    Plugin.Instance.PluginSettings.refillOxygenInDiveBell.Set(binaryDeserializer.ReadBool() == true ? 1 : 0);
                    Plugin.Instance.PluginSettings.useOxygenOnSurface.Set(binaryDeserializer.ReadBool() == true ? 1 : 0);
                    Plugin.Instance.PluginSettings.refillOxygenOnSurface.Set(binaryDeserializer.ReadBool() == true ? 1 : 0);
                    Plugin.Instance.PluginSettings.oxygenRefillRate.Set(binaryDeserializer.ReadFloat());
                    Plugin.Instance.PluginSettings.maxHealth.Set(binaryDeserializer.ReadFloat());
                    Plugin.Instance.PluginSettings.daysPerQuota.SetX(binaryDeserializer.ReadInt());
                }

                binaryDeserializer.Dispose();
            }

            return true;
        }
    }
}
