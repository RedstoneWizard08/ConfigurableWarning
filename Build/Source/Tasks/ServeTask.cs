using Cake.DocFx;
using Cake.Frosting;

namespace ConfigurableWarning.Build.Tasks;

[TaskName("Serve")]
[IsDependentOn(typeof(DocsTask))]
public sealed class ServeTask : FrostingTask<BuildContext> {
    public override void Run(BuildContext context) {
        context.DocFxServe("../docs/_site");
    }
}
