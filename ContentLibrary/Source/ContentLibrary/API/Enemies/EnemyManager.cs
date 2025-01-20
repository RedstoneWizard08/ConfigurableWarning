using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace ContentLibrary.API.Enemies;

/// <summary>
///     Contains methods for managing enemies.
/// </summary>
public static class EnemyManager {
    private static List<GameObject> Enemies { get; } = [];
    private static List<GameObject> AddedEnemies { get; } = [];

    /// <summary>
    ///     Injects all registered enemies into a round spawner.
    /// </summary>
    /// <param name="spawner">The round spawner to inject enemies into.</param>
    public static void InjectEnemies(RoundSpawner spawner) {
        foreach (var enemy in Enemies.Where(enemy => !AddedEnemies.Contains(enemy))) {
            spawner.possibleSpawns.AddItem(enemy);
            spawner.spawnBudgetCosts.AddItem(enemy.GetComponent<BudgetCost>());

            AddedEnemies.Add(enemy);
        }
    }

    /// <summary>
    ///     Registers an enemy.
    /// </summary>
    /// <param name="enemy">The enemy to register.</param>
    public static void RegisterEnemy(GameObject enemy) {
        Enemies.Add(enemy);
    }
}