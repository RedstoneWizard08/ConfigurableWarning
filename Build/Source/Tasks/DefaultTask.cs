using Cake.Frosting;

namespace ConfigurableWarning.Build.Tasks;

[TaskName("Default")]
[IsDependentOn(typeof(BuildTask))]
[IsDependentOn(typeof(DocsTask))]
public class DefaultTask : FrostingTask {
}
