using System;
using System.Collections.Generic;
using ContentLibrary.API;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ContentSettings.Internal;

/// <summary>
///     Contains the assets used by the settings system.
/// </summary>
internal static class SettingsAssets {
    /// <summary>
    ///     The path to the asset bundle.
    /// </summary>
    private const string BundlePath = "contentsettings.bundle.gz";

    /// <summary>
    ///     Gets the settings navigation prefab.
    /// </summary>
    internal static GameObject SettingsNavigationPrefab { get; private set; } = null!;

    /// <summary>
    ///     Gets the settings tab prefab.
    /// </summary>
    internal static GameObject SettingsTabPrefab { get; private set; } = null!;

    /// <summary>
    ///     Gets the settings category prefab.
    /// </summary>
    internal static GameObject SettingsCategoryPrefab { get; private set; } = null!;

    /// <summary>
    ///     Gets the settings integer input prefab.
    /// </summary>
    internal static GameObject SettingsIntInputPrefab { get; private set; } = null!;

    /// <summary>
    ///     A cache of loaded asset bundles.
    /// </summary>
    private static Dictionary<string, AssetBundle> AssetBundles { get; } = [];

    /// <summary>
    ///     Loads the assets used by the settings system.
    /// </summary>
    internal static void LoadAssets() {
        SettingsNavigationPrefab = LoadAsset<GameObject>(
            BundlePath,
            "Assets/ContentSettings/SettingsNavigation.prefab");

        SettingsTabPrefab = LoadAsset<GameObject>(
            BundlePath,
            "Assets/ContentSettings/SettingsTab.prefab");

        SettingsCategoryPrefab = LoadAsset<GameObject>(
            BundlePath,
            "Assets/ContentSettings/SettingsCategory.prefab");

        SettingsIntInputPrefab = LoadAsset<GameObject>(
            BundlePath,
            "Assets/ContentSettings/SettingsIntInput.prefab");
    }

    /// <summary>
    ///     Loads an asset from an asset bundle that is embedded in the assembly.
    /// </summary>
    /// <param name="bundleName">The name of the asset bundle.</param>
    /// <param name="assetName">The name of the asset.</param>
    /// <typeparam name="T">The type of asset to load.</typeparam>
    /// <returns>The loaded asset.</returns>
    /// <exception cref="System.Exception">Thrown if the asset bundle or asset could not be loaded.</exception>
    private static T LoadAsset<T>(string bundleName, string assetName) where T : Object {
        ContentSettingsEntry.Logger.LogDebug($"Loading asset '{assetName}' from asset bundle '{bundleName}'.");

        if (AssetBundles.TryGetValue(bundleName, out var bundle)) return bundle.LoadAsset<T>(assetName);

        var assetBundle = BundleUtils.LoadEmbeddedAssetBundle(typeof(ContentSettingsEntry).Assembly,
            typeof(ContentLibrary.Plugin).Namespace + "." + bundleName, true);

        var asset = assetBundle?.LoadAsset<T>(assetName) ??
                    throw new Exception($"Failed to load asset '{assetName}' from asset bundle '{bundleName}'.");

        ContentSettingsEntry.Logger.LogDebug($"Loaded asset '{assetName}' from asset bundle '{bundleName}'.");
        AssetBundles.Add(bundleName, assetBundle);

        return asset;
    }
}