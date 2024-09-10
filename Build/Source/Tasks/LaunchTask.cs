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
        var outPath = $"../ContentLibrary/bin/{config}/netstandard2.1/RedstoneWizard08.ContentLibrary.dll";
        var pluginPath = Path.Join(profilePath, "BepInEx/plugins/RedstoneWizard08-ContentLibrary");
        var args = $"--doorstop-enable true --doorstop-target \"{profilePath}/BepInEx/core/BepInEx.Preloader.dll\"";

        if (!Path.Exists(Path.Join(outPath, ".."))) {
            context.CreateDirectory(Path.Join(outPath, ".."));
        }

        context.CopyFile(outPath, $"{pluginPath}/RedstoneWizard08.ContentLibrary.dll");
        context.StartProcess(gamePath, args);
    }
}
