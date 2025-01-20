using UnityEngine;

namespace ContentLibrary.API.Util;

/// <summary>
///     Utilities for working with asset bundles.
/// </summary>
public class AssetBundleHelper {
    /// <summary>
    ///     Load an asset bundle from a path.
    /// </summary>
    /// <param name="path">The path to get the bundle from.</param>
    /// <returns>The loaded asset bundle.</returns>
    public static AssetBundle GetAssetBundle(string path) {
        return AssetBundle.LoadFromFile(path);
    }
}