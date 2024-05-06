using Cake.DocFx;
using Cake.Frosting;

namespace ConfigurableWarning.Build.Tasks;

[TaskName("Docs")]
public sealed class DocsTask : FrostingTask<BuildContext> {
    public override void Run(BuildContext context) {
        context.DocFxBuild("../docs/docfx.json");
    }
}
