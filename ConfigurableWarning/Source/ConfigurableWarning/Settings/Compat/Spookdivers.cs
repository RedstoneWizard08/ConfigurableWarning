using System;
using System.Linq;
using ConfigurableWarning.API;
using ConfigurableWarning.API.Attributes;
using ConfigurableWarning.API.Compat;
using ConfigurableWarning.API.Options;
using SPlugin = Spookdivers.Plugin;

namespace ConfigurableWarning.Settings.Compat;

/// <summary>
///     Settings keys for Spookdivers compat
/// </summary>
public static class SpookdiversSettingKeys {
#pragma warning disable CS1591
    public const string ReadyTime = "ReadyTime";
    public const string ForceScene = "ForceScene";
    public const string SceneName = "SceneName";
    public const string FakeBells = "FakeBells";
    public const string LoweringSpeed = "LoweringSpeed";
#pragma warning restore CS1591
}

/// <summary>
///     Scene types
/// </summary>
public enum SceneType {
#pragma warning disable CS1591
    FactoryScene,
    HarbourScene,
    MinesScene,
#pragma warning restore CS1591
}

/// <summary>
///     Spookdivers compat settings
/// </summary>
[CompatModule(["Spookdivers"])]
public class SpookdiversCompat : ICompatModule {
    /// <inheritdoc />
    public void Init() {
        States.Floats[SpookdiversSettingKeys.ReadyTime] = SPlugin.Instance.ReadyTime.Value;
        States.Bools[SpookdiversSettingKeys.ForceScene] = SPlugin.Instance.ForceScene.Value;
        States.Enums.Set(SpookdiversSettingKeys.SceneName, Enum.Parse<SceneType>(SPlugin.Instance.SceneName.Value));
        States.Bools[SpookdiversSettingKeys.FakeBells] = SPlugin.Instance.FakeBellsConfig.Value;
        States.Floats[SpookdiversSettingKeys.LoweringSpeed] = SPlugin.Instance.LoweringSpeedConfig.Value;
    }

    internal static void ApplySettings(IUntypedOption _opt) {
        string[] all = [
            SpookdiversSettingKeys.ReadyTime,
            SpookdiversSettingKeys.ForceScene,
            SpookdiversSettingKeys.SceneName,
            SpookdiversSettingKeys.FakeBells,
            SpookdiversSettingKeys.LoweringSpeed
        ];

        if (!all.All(v => OptionsState.Instance.Has(v))) return;

        SPlugin.Instance.ReadyTime.Value = States.Floats[SpookdiversSettingKeys.ReadyTime];
        SPlugin.Instance.ForceScene.Value = States.Bools[SpookdiversSettingKeys.ForceScene];
        SPlugin.Instance.SceneName.Value = Enum.GetName(typeof(SceneType),
            States.Enums.Get<SceneType>(SpookdiversSettingKeys.SceneName));
        SPlugin.Instance.FakeBellsConfig.Value = States.Bools[SpookdiversSettingKeys.FakeBells];
        SPlugin.Instance.LoweringSpeedConfig.Value = States.Floats[SpookdiversSettingKeys.LoweringSpeed];
    }

    [CompatGroup("SPOOKDIVERS", "GENERAL")]
    private static class Settings {
        [Register]
        private class ReadyTime()
            : FloatOption(SpookdiversSettingKeys.ReadyTime, 30f, "Max Players", 0f, 100f,
                [ApplySettings],
                false);

        [Register]
        private class ForceScene()
            : BoolOption(SpookdiversSettingKeys.ForceScene, false, "Force Scene Change",
                [ApplySettings]);

        [Register]
        private class SceneName()
            : EnumOption<SceneType>(SpookdiversSettingKeys.SceneName, SceneType.FactoryScene, "Scene Name",
                [ApplySettings]);

        [Register]
        private class FakeBells()
            : BoolOption(SpookdiversSettingKeys.FakeBells, false, "Enable Fake Bells",
                [ApplySettings]);

        [Register]
        private class LoweringSpeed()
            : FloatOption(SpookdiversSettingKeys.LoweringSpeed, 2f, "Bell Lowering Speed", 0f, 30f, [ApplySettings],
                false);
    }
}