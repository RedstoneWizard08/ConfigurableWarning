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

                binarySerializer.WriteFloat(Plugin.Instance.config.maxOxygen.Value);
                binarySerializer.WriteFloat(Plugin.Instance.config.oxygenUsageMultiplier.Value);
                binarySerializer.WriteFloat(Plugin.Instance.config.sprintMultiplier.Value);
                binarySerializer.WriteBool(Plugin.Instance.config.useOxygenInDiveBell.Value);
                binarySerializer.WriteBool(Plugin.Instance.config.refillOxygenInDiveBell.Value);
                binarySerializer.WriteBool(Plugin.Instance.config.useOxygenOnSurface.Value);
                binarySerializer.WriteBool(Plugin.Instance.config.refillOxygenOnSurface.Value);
                binarySerializer.WriteFloat(Plugin.Instance.config.oxygenRefillRate.Value);
                binarySerializer.WriteFloat(Plugin.Instance.config.maxHealth.Value);
                binarySerializer.WriteInt(Plugin.Instance.config.daysPerQuota.Value);

                var array = binarySerializer.buffer.ToByteArray();

                stream.SendNext(array);
                binarySerializer.Dispose();
            }
            else {
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
                }
                else {
                    ConfigContainers.maxOxygen.SetValue(binaryDeserializer.ReadFloat(), GameHandler.Instance.SettingsHandler);
                    ConfigContainers.oxygenUsage.SetValue(binaryDeserializer.ReadFloat(), GameHandler.Instance.SettingsHandler);
                    ConfigContainers.sprintUsage.SetValue(binaryDeserializer.ReadFloat(), GameHandler.Instance.SettingsHandler);
                    ConfigContainers.useOxygenInDiveBell.SetValue(binaryDeserializer.ReadBool() == true ? 1 : 0, GameHandler.Instance.SettingsHandler);
                    ConfigContainers.refillOxygenInDiveBell.SetValue(binaryDeserializer.ReadBool() == true ? 1 : 0, GameHandler.Instance.SettingsHandler);
                    ConfigContainers.useOxygenOnSurface.SetValue(binaryDeserializer.ReadBool() == true ? 1 : 0, GameHandler.Instance.SettingsHandler);
                    ConfigContainers.refillOxygenOnSurface.SetValue(binaryDeserializer.ReadBool() == true ? 1 : 0, GameHandler.Instance.SettingsHandler);
                    ConfigContainers.oxygenRefillRate.SetValue(binaryDeserializer.ReadFloat(), GameHandler.Instance.SettingsHandler);
                    ConfigContainers.maxHealth.SetValue(binaryDeserializer.ReadFloat(), GameHandler.Instance.SettingsHandler);
                    ConfigContainers.daysPerQuota.SetValueX(binaryDeserializer.ReadInt(), GameHandler.Instance.SettingsHandler);
                }

                binaryDeserializer.Dispose();
            }

            return true;
        }
    }
}
