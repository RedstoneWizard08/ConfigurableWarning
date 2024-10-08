using System;
using Cake.Common;
using Cake.Common.IO;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;
using Path = System.IO.Path;

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
        
        context.Log.Information("Copying files...");

        context.CopyFile(outPath, $"{pluginPath}/RedstoneWizard08.ContentLibrary.dll");
        
        context.Log.Information("Launching game...");
        
        context.StartAndReturnProcess(gamePath, new ProcessSettings() {
            Arguments = args,
            RedirectStandardOutput = true,
        });
    }
}
