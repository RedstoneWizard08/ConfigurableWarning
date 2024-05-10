using Cake.Common.Tools.DotNet;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("Generate")]
public sealed class GenerateTask : FrostingTask<BuildContext> {
    public override void Run(BuildContext context) {
        context.DotNetTool("DocFxTocGenerator -d ../docs/guides/ -r");
    }
}
