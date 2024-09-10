using System;
using Cake.Frosting;

namespace Build;

public static class Program {
    public static int Main(string[] args) {
        return new CakeHost()
            .InstallTool(new Uri("nuget:?package=tcli&version=0.2.3"))
            .InstallTool(new Uri("nuget:?package=docfx&version=2.77.0"))
            .UseContext<BuildContext>()
            .Run(args);
    }
}