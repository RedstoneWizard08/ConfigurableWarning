using Cake.Common;
using Cake.Core;
using Cake.Frosting;

namespace Build;

// ReSharper disable once ClassNeverInstantiated.Global
public class BuildContext(ICakeContext context) : FrostingContext(context) {
    public string GamePath { get; set; } = context.Argument("game-path",
        "F:/SteamLibrary/steamapps/common/Content Warning/Content Warning.exe");

    public string ProfilePath { get; set; } =
        context.Argument("profile-path", "F:/Thunderstore/DataFolder/ContentWarning/profiles/CW2");

    public string MsBuildConfiguration { get; set; } = context.Argument("configuration", "Release");
}