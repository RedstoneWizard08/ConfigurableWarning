using ConfigurableWarning.API.State;
using ConfigurableWarning.Settings;
using HarmonyLib;
using Steamworks;

namespace ConfigurableWarning.Patches;

/// <summary>
///     Patches the lobby system to allow truly private games.
/// </summary>
[HarmonyPatch]
public class LobbyPatch {
    /// <summary>
    ///     Check if the player is friends with another person.
    /// </summary>
    /// <param name="cSteamID">The other's Steam ID.</param>
    /// <returns>If they are friends or not</returns>
    private static bool IsFriendsWith(CSteamID cSteamID) {
        return SteamFriends.HasFriend(cSteamID, EFriendFlags.k_EFriendFlagNone);
    }

    /// <summary>
    ///     Checks if the game is public
    /// </summary>
    /// <param name="__instance">The current instance of the <see cref="SteamLobbyHandler" />.</param>
    /// <returns>If the game is public</returns>
    private static bool IsPublic(SteamLobbyHandler __instance) {
        if (__instance.MasterClient) return !States.Bools[SettingKeys.PrivateHost];

        return __instance.IsPlayingWithRandoms();
    }

    /// <summary>
    ///     Patches the random player allower thingamajig to apply the privacy setting
    /// </summary>
    /// <param name="__instance">The current instance of the <see cref="SteamLobbyHandler" />.</param>
    /// <param name="__result">The bool result</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(SteamLobbyHandler), nameof(SteamLobbyHandler.IsPlayingWithRandoms))]
    public static void IsPlayingWithRandoms(SteamLobbyHandler __instance, ref bool __result) {
        if (__instance.MasterClient) __result = !States.Bools[SettingKeys.PrivateHost];
    }

    /// <summary>
    ///     Patches the steam client adder to kick if they are not friends (and the
    ///     private host setting is true)
    /// </summary>
    /// <param name="__instance">The current instance of the <see cref="SteamLobbyHandler" />.</param>
    /// <param name="cSteamID">The new player's Steam ID</param>
    /// <returns><see cref="HarmonyPrefix" /> continue-or-not flag (this is a prefix)</returns>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(SteamLobbyHandler), nameof(SteamLobbyHandler.AddSteamClient))]
    public static bool AddSteamClient(SteamLobbyHandler __instance, CSteamID cSteamID) {
        if (!IsFriendsWith(cSteamID) && !IsPublic(__instance)) return false;

        return true;
    }

#pragma warning disable Harmony003

    /// <summary>
    ///     Patches the networking session responder to apply our settings.
    /// </summary>
    /// <param name="__instance">The current instance of the <see cref="SteamLobbyHandler" />.</param>
    /// <param name="param">The session request</param>
    /// <returns><see cref="HarmonyPrefix" /> continue-or-not flag (this is a prefix)</returns>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(SteamLobbyHandler), nameof(SteamLobbyHandler.OnNetworkingSessionRequest))]
    public static bool OnNetworkingSessionRequest(SteamLobbyHandler __instance,
        SteamNetworkingMessagesSessionRequest_t param) {
        return IsFriendsWith(param.m_identityRemote.GetSteamID()) || IsPublic(__instance);
    }

#pragma warning restore Harmony003

    /// <summary>
    ///     Patches the lobby opener to allow private games.
    /// </summary>
    /// <param name="__instance">The current instance of the <see cref="SteamLobbyHandler" />.</param>
    /// <returns><see cref="HarmonyPrefix" /> continue-or-not flag (this is a prefix)</returns>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(SteamLobbyHandler), nameof(SteamLobbyHandler.OpenLobby))]
    public static bool OpenLobby(SteamLobbyHandler __instance) {
        if (!__instance.MasterClient) return false;

        SteamMatchmaking.SetLobbyType(__instance.m_CurrentLobby,
            IsPublic(__instance) ? ELobbyType.k_ELobbyTypePublic : ELobbyType.k_ELobbyTypeFriendsOnly);

        SteamMatchmaking.SetLobbyJoinable(__instance.m_CurrentLobby, true);

        return false;
    }
}