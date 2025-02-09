using System;
using System.Linq;
using ConfigurableWarning.API.Attributes;
using ConfigurableWarning.API.Compat;
using ConfigurableWarning.API.Options;
using ConfigurableWarning.API.State;

namespace ContentLibrary.Settings.Compat;

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
        States.Floats[SpookdiversSettingKeys.ReadyTime] = Spookdivers.Plugin.Instance.ReadyTime.Value;
        States.Bools[SpookdiversSettingKeys.ForceScene] = Spookdivers.Plugin.Instance.ForceScene.Value;
        States.Enums.Set(SpookdiversSettingKeys.SceneName,
            Enum.Parse<SceneType>(Spookdivers.Plugin.Instance.SceneName.Value));
        States.Bools[SpookdiversSettingKeys.FakeBells] = Spookdivers.Plugin.Instance.FakeBellsConfig.Value;
        States.Floats[SpookdiversSettingKeys.LoweringSpeed] = Spookdivers.Plugin.Instance.LoweringSpeedConfig.Value;
    }

    private static void ApplySettings(IUntypedOption opt) {
        string[] all = [
            SpookdiversSettingKeys.ReadyTime,
            SpookdiversSettingKeys.ForceScene,
            SpookdiversSettingKeys.SceneName,
            SpookdiversSettingKeys.FakeBells,
            SpookdiversSettingKeys.LoweringSpeed
        ];

        if (!all.All(v => OptionsState.Instance.Has(v))) return;

        Spookdivers.Plugin.Instance.ReadyTime.Value = States.Floats[SpookdiversSettingKeys.ReadyTime];
        Spookdivers.Plugin.Instance.ForceScene.Value = States.Bools[SpookdiversSettingKeys.ForceScene];
        Spookdivers.Plugin.Instance.SceneName.Value = Enum.GetName(typeof(SceneType),
            States.Enums.Get<SceneType>(SpookdiversSettingKeys.SceneName));
        Spookdivers.Plugin.Instance.FakeBellsConfig.Value = States.Bools[SpookdiversSettingKeys.FakeBells];
        Spookdivers.Plugin.Instance.LoweringSpeedConfig.Value = States.Floats[SpookdiversSettingKeys.LoweringSpeed];
    }

    [CompatGroup("SPOOKDIVERS", "GENERAL")]
    private static class Settings {
        [Register]
        private class ReadyTime()
            : FloatOption(SpookdiversSettingKeys.ReadyTime, 30f, "Ready Time (Seconds)", 0f, 100f,
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