using System.Linq;
using UnityEngine;

namespace ContentLibrary.API.Enemies;

/// <summary>
/// Contains methods for loading enemies.
/// </summary>
public class EnemyLoader {
    /// <summary>
    /// Registers an enemy.
    /// </summary>
    /// <param name="bundle">The asset bundle to load the enemy from.</param>
    /// <param name="asset">The asset name to load the enemy from.</param>
    public static void LoadEnemyFromBundle(AssetBundle bundle, string asset) {
        var enemy = bundle.LoadAsset<GameObject>(asset);

        RegisterEnemy(enemy);
    }

    /// <summary>
    /// Registers an enemy.
    /// </summary>
    /// <param name="asset">The enemy asset.</param>
    public static void RegisterEnemy(GameObject asset) {
        EnemyManager.RegisterEnemy(asset);
    }

    /// <summary>
    /// Registers all enemies in an asset bundle.
    /// </summary>
    /// <param name="bundle">The asset bundle to load enemies from.</param>
    public static void LoadAllEnemiesFromBundle(AssetBundle bundle) {
        var assets = bundle.LoadAllAssets<GameObject>().Where(v => v.GetComponent<BudgetCost>() != null);

        foreach (var asset in assets) RegisterEnemy(asset);
    }
}