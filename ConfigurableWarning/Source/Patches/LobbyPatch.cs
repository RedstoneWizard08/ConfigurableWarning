using ConfigurableWarning.Options;
using ConfigurableWarning.Settings;
using HarmonyLib;
using Steamworks;

namespace ConfigurableWarning.Patches;

/// <summary>
///     Patches the lobby system to allow truly private games.
/// </summary>
[HarmonyPatch]
internal class LobbyPatch {
    private static bool IsFriendsWith(CSteamID cSteamID) {
        return SteamFriends.HasFriend(cSteamID, EFriendFlags.k_EFriendFlagNone);
    }

    private static bool IsPublic(SteamLobbyHandler __instance) {
        if (__instance.MasterClient) return !OptionsState.Instance.Get<bool>(BuiltInSettings.Keys.PrivateHost);

        return __instance.IsPlayingWithRandoms();
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(SteamLobbyHandler), nameof(SteamLobbyHandler.IsPlayingWithRandoms))]
    internal static void IsPlayingWithRandoms(SteamLobbyHandler __instance, ref bool __result) {
        if (__instance.MasterClient) __result = !OptionsState.Instance.Get<bool>(BuiltInSettings.Keys.PrivateHost);
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(SteamLobbyHandler), nameof(SteamLobbyHandler.AddSteamClient))]
    internal static bool AddSteamClient(SteamLobbyHandler __instance, CSteamID cSteamID) {
        if (!IsFriendsWith(cSteamID) && !IsPublic(__instance)) return false;

        return true;
    }

#pragma warning disable Harmony003

    [HarmonyPrefix]
    [HarmonyPatch(typeof(SteamLobbyHandler), nameof(SteamLobbyHandler.OnNetworkingSessionRequest))]
    internal static bool OnNetworkingSessionRequest(SteamLobbyHandler __instance,
        SteamNetworkingMessagesSessionRequest_t param) {
        return IsFriendsWith(param.m_identityRemote.GetSteamID()) || IsPublic(__instance);
    }

#pragma warning restore Harmony003

    [HarmonyPrefix]
    [HarmonyPatch(typeof(SteamLobbyHandler), nameof(SteamLobbyHandler.OpenLobby))]
    internal static bool OpenLobby(SteamLobbyHandler __instance) {
        if (!__instance.MasterClient) return false;

        SteamMatchmaking.SetLobbyType(__instance.m_CurrentLobby,
            IsPublic(__instance) ? ELobbyType.k_ELobbyTypePublic : ELobbyType.k_ELobbyTypeFriendsOnly);

        SteamMatchmaking.SetLobbyJoinable(__instance.m_CurrentLobby, true);

        return false;
    }
}