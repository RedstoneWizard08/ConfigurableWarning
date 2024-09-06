using ContentSettings.API.Settings;
using UnityEngine;

namespace ContentSettings.Internal;

/// <summary>
/// Used to map input cells to game objects.
/// </summary>
internal static class SettingsMapper
{
    private static GameObject? _textSettingCell;

    private static GameObject? _intSettingCell;

    private static GameObject? _boolSettingCell;

    /// <summary>
    /// Gets the text setting cell.
    /// </summary>
    public static GameObject TextSettingCell
    {
        get
        {
            if (_textSettingCell == null)
            {
                _textSettingCell = SettingsAssets.SettingsTextInputPrefab;
            }

            if (_textSettingCell.GetComponent<TextSettingUI>() == null)
            {
                _textSettingCell.AddComponent<TextSettingUI>();
            }

            return _textSettingCell;
        }
    }

    /// <summary>
    /// Gets the int setting cell.
    /// </summary>
    public static GameObject IntSettingCell
    {
        get
        {
            if (_intSettingCell == null)
            {
                _intSettingCell = SettingsAssets.SettingsIntInputPrefab;
            }

            if (_intSettingCell.GetComponent<IntSettingUI>() == null)
            {
                _intSettingCell.AddComponent<IntSettingUI>();
            }

            return _intSettingCell;
        }
    }

    /// <summary>
    /// Gets the bool setting cell.
    /// </summary>
    public static GameObject BoolSettingCell
    {
        get
        {
            if (_boolSettingCell == null)
            {
                _boolSettingCell = SettingsAssets.SettingsBoolInputPrefab;
            }

            if (_boolSettingCell.GetComponent<BoolSettingUI>() == null)
            {
                _boolSettingCell.AddComponent<BoolSettingUI>();
            }

            return _boolSettingCell;
        }
    }
}
