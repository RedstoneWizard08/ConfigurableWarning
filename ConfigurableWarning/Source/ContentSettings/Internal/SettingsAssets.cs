using System;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ContentSettings.Internal;

/// <summary>
/// Contains the assets used by the settings system.
/// </summary>
internal static class SettingsAssets {
    internal const string BundlePath = "contentsettings.bundle.gz";

    /// <summary>
    /// Gets the settings navigation prefab.
    /// </summary>
    internal static GameObject SettingsNavigationPrefab { get; private set; } = null!;

    /// <summary>
    /// Gets the settings tab prefab.
    /// </summary>
    internal static GameObject SettingsTabPrefab { get; private set; } = null!;

    /// <summary>
    /// Gets the settings category prefab.
    /// </summary>
    internal static GameObject SettingsCategoryPrefab { get; private set; } = null!;

    /// <summary>
    /// Gets the settings text input prefab.
    /// </summary>
    internal static GameObject SettingsTextInputPrefab { get; private set; } = null!;

    /// <summary>
    /// Gets the settings integer input prefab.
    /// </summary>
    internal static GameObject SettingsIntInputPrefab { get; private set; } = null!;

    /// <summary>
    /// Gets the settings bool input prefab.
    /// </summary>
    internal static GameObject SettingsBoolInputPrefab { get; private set; } = null!;

    private static Dictionary<string, AssetBundle> AssetBundles { get; } = new();

    /// <summary>
    /// Loads the assets used by the settings system.
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

        SettingsTextInputPrefab = LoadAsset<GameObject>(
            BundlePath,
            "Assets/ContentSettings/SettingsTextInput.prefab");

        SettingsIntInputPrefab = LoadAsset<GameObject>(
            BundlePath,
            "Assets/ContentSettings/SettingsIntInput.prefab");

        SettingsBoolInputPrefab = LoadAsset<GameObject>(
            BundlePath,
            "Assets/ContentSettings/SettingsBoolInput.prefab");
    }

    /// <summary>
    /// Loads an asset from an asset bundle that is embedded in the assembly.
    /// </summary>
    /// <param name="bundleName">The name of the asset bundle.</param>
    /// <param name="assetName">The name of the asset.</param>
    /// <typeparam name="T">The type of asset to load.</typeparam>
    /// <returns>The loaded asset.</returns>
    /// <exception cref="System.Exception">Thrown if the asset bundle or asset could not be loaded.</exception>
    private static T LoadAsset<T>(string bundleName, string assetName)
        where T : Object {
        ContentSettings.Logger.LogDebug($"Loading asset '{assetName}' from asset bundle '{bundleName}'.");

        if (AssetBundles.TryGetValue(bundleName, out var bundle)) {
            return bundle.LoadAsset<T>(assetName);
        }

        var assetBundleStream = typeof(ContentSettings)
            .Assembly
            .GetManifestResourceStream(typeof(ConfigurableWarning.ConfigurableWarning).Namespace + "." + bundleName);

        if (assetBundleStream == null) {
            throw new Exception($"Failed to load asset bundle '{bundleName}' from embedded resource.");
        }

        using (assetBundleStream)
        using (var gzStream = new GZipStream(assetBundleStream, CompressionMode.Decompress)) {
            var assetBundle = AssetBundle.LoadFromStream(gzStream);

            if (assetBundle == null) {
                throw new Exception($"Failed to load asset bundle '{bundleName}' from stream.");
            }

            var asset = assetBundle.LoadAsset<T>(assetName);

            if (asset == null) {
                throw new Exception($"Failed to load asset '{assetName}' from asset bundle '{bundleName}'.");
            }

            ContentSettings.Logger.LogDebug($"Loaded asset '{assetName}' from asset bundle '{bundleName}'.");

            AssetBundles.Add(bundleName, assetBundle);

            return asset;
        }
    }
}