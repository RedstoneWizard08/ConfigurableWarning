using Cake.DocFx;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("Docs")]
public sealed class DocsTask : FrostingTask<BuildContext> {
    public override void Run(BuildContext context) {
        context.DocFxMetadata("../docs/docfx.json");
        context.DocFxBuild("../docs/docfx.json");
    }
}
