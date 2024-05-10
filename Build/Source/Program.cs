using System;
using Cake.Frosting;

namespace Build;

public static class Program {
    public static int Main(string[] args) {
        return new CakeHost()
            .InstallTool(new Uri("nuget:?package=tcli&version=0.2.3"))
            .UseContext<BuildContext>()
            .Run(args);
    }
}