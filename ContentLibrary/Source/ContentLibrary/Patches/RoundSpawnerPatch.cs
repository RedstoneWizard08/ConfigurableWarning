using ContentLibrary.API.Enemies;
using HarmonyLib;

namespace ContentLibrary.Patches;

/// <summary>
/// Contains Harmony patches for the round spawner.
/// </summary>
[HarmonyPatch]
public class RoundSpawnerPatch {
    /// <summary>
    /// Patches the RoundSpawner to inject custom enemies.
    /// </summary>
    /// <param name="__instance">The instance.</param>
    /// <returns>Whether to continue.</returns>
    [HarmonyPatch(typeof(RoundSpawner), nameof(RoundSpawner.Start))]
    [HarmonyPrefix]
    public static bool Start(RoundSpawner __instance) {
        EnemyManager.InjectEnemies(__instance);
        return true;
    }
}