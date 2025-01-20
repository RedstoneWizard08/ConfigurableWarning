using ContentSettings.API.Settings;
using UnityEngine;

namespace ContentSettings.Internal;

/// <summary>
///     Used to map input cells to game objects.
/// </summary>
internal static class SettingsMapper {
    private static GameObject? _intSettingCell;

    /// <summary>
    ///     Gets the int setting cell.
    /// </summary>
    public static GameObject IntSettingCell {
        get {
            if (_intSettingCell == null) _intSettingCell = SettingsAssets.SettingsIntInputPrefab;

            if (_intSettingCell.GetComponent<IntSettingUI>() == null) _intSettingCell.AddComponent<IntSettingUI>();

            return _intSettingCell;
        }
    }
}