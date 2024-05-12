using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("Build")]
public sealed class BuildTask : FrostingTask<BuildContext> {
    public override void Run(BuildContext context) {
        context.DotNetBuild("../ConfigurableWarning/ConfigurableWarning.csproj", new DotNetBuildSettings {
            Configuration = context.MsBuildConfiguration
        });

        context.DotNetTool("tcli build --config-path ../thunderstore.toml");
    }
}
