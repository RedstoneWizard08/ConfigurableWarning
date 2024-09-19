#pragma warning disable CS0612 // The obsolete message is only for other mods, we still need to use these methods.

using BepInEx.Logging;
using ContentLibrary;
using ContentSettings.API;
using ContentSettings.Internal;

namespace ContentSettings;

/// <summary>
///     The main Content Settings plugin class.
/// </summary>
public class ContentSettingsEntry {
    /// <summary>
    ///     Our <see cref="ManualLogSource" />.
    /// </summary>
    internal static readonly ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(CSPluginInfo.PLUGIN_GUID);

    /// <summary>
    ///     Initialize ContentSettings.
    /// </summary>
    public static void Init() {
        Logger.LogInfo($"Loading plugin {CSPluginInfo.PLUGIN_GUID} (bundled with {MyPluginInfo.PLUGIN_GUID})...");

        SettingsAssets.LoadAssets();

        Logger.LogInfo($"Plugin {CSPluginInfo.PLUGIN_GUID} loaded!");
    }

    /// <summary>
    ///     Called each frame.
    /// </summary>
    public static void Update() {
        SettingsLoader.Update();
    }
}