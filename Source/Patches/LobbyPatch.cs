using HarmonyLib;
using Steamworks;

namespace ConfigurableWarning.Patches {
    /// <summary>
    /// Patches the lobby system to allow truly private games.
    /// </summary>
    [HarmonyPatch]
    internal class LobbyPatch {
        internal static bool IsFriendsWith(CSteamID cSteamID) {
            return SteamFriends.HasFriend(cSteamID, EFriendFlags.k_EFriendFlagNone);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(SteamLobbyHandler), "IsPlayingWithRandoms")]
        internal static void IsPlayingWithRandoms(SteamLobbyHandler __instance, ref bool __result) {
            if (__instance.MasterClient) {
                __result = Plugin.Instance.PluginConfig.privateHost.Value;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(SteamLobbyHandler), "AddSteamClient")]
        internal static bool AddSteamClient(SteamLobbyHandler __instance, CSteamID cSteamID) {
            if (!IsFriendsWith(cSteamID) && !__instance.IsPlayingWithRandoms()) {
                return false;
            }

            return true;
        }

#pragma warning disable Harmony003

        [HarmonyPrefix]
        [HarmonyPatch(typeof(SteamLobbyHandler), "OnNetworkingSessionRequest")]
        internal static bool OnNetworkingSessionRequest(SteamLobbyHandler __instance, SteamNetworkingMessagesSessionRequest_t param) {
            if (!IsFriendsWith(param.m_identityRemote.GetSteamID()) && !__instance.IsPlayingWithRandoms()) {
                return false;
            }

            return true;
        }

#pragma warning restore Harmony003

        internal static bool OpenLobby(SteamLobbyHandler __instance) {
            if (__instance.MasterClient) {
                if (__instance.IsPlayingWithRandoms()) {
                    SteamMatchmaking.SetLobbyType(__instance.m_CurrentLobby, ELobbyType.k_ELobbyTypePublic);
                } else {
                    SteamMatchmaking.SetLobbyType(__instance.m_CurrentLobby, ELobbyType.k_ELobbyTypeFriendsOnly);
                }

                SteamMatchmaking.SetLobbyJoinable(__instance.m_CurrentLobby, true);
            }

            return false;
        }
    }
}
