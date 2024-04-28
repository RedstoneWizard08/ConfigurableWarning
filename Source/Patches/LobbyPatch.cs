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

        internal static bool IsPublic(SteamLobbyHandler __instance) {
            if (__instance.MasterClient) {
                return !Plugin.Instance.PluginSettings.privateHost.Value;
            }

            return __instance.IsPlayingWithRandoms();
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(SteamLobbyHandler), nameof(SteamLobbyHandler.IsPlayingWithRandoms))]
        internal static void IsPlayingWithRandoms(SteamLobbyHandler __instance, ref bool __result) {
            if (__instance.MasterClient) {
                __result = !Plugin.Instance.PluginSettings.privateHost.Value;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(SteamLobbyHandler), nameof(SteamLobbyHandler.AddSteamClient))]
        internal static bool AddSteamClient(SteamLobbyHandler __instance, CSteamID cSteamID) {
            if (!IsFriendsWith(cSteamID) && !IsPublic(__instance)) {
                return false;
            }

            return true;
        }

#pragma warning disable Harmony003

        [HarmonyPrefix]
        [HarmonyPatch(typeof(SteamLobbyHandler), nameof(SteamLobbyHandler.OnNetworkingSessionRequest))]
        internal static bool OnNetworkingSessionRequest(SteamLobbyHandler __instance, SteamNetworkingMessagesSessionRequest_t param) {
            if (!IsFriendsWith(param.m_identityRemote.GetSteamID()) && !IsPublic(__instance)) {
                return false;
            }

            return true;
        }

#pragma warning restore Harmony003

        [HarmonyPrefix]
        [HarmonyPatch(typeof(SteamLobbyHandler), nameof(SteamLobbyHandler.OpenLobby))]
        internal static bool OpenLobby(SteamLobbyHandler __instance) {
            if (__instance.MasterClient) {
                if (IsPublic(__instance)) {
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
