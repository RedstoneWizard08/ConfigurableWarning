using System.IO;
using Cake.Common;
using Cake.Common.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("Launch")]
[IsDependentOn(typeof(BuildTask))]
public sealed class LaunchTask : FrostingTask<BuildContext> {
    public override void Run(BuildContext context) {
        var config = context.MsBuildConfiguration;
        var gamePath = context.GamePath;
        var profilePath = context.ProfilePath;
        var outPath = $"../ConfigurableWarning/bin/{config}/netstandard2.1/RedstoneWizard08.ConfigurableWarning.dll";
        var pluginPath = Path.Join(profilePath, "BepInEx/plugins/RedstoneWizard08-ConfigurableWarning");
        var args = $"--doorstop-enable true --doorstop-target \"{profilePath}/BepInEx/core/BepInEx.Preloader.dll\"";

        context.CopyFile(outPath, $"{pluginPath}/RedstoneWizard08.ConfigurableWarning.dll");
        context.StartProcess(gamePath, args);
    }
}