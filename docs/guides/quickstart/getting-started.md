---
uid: Guides.GettingStarted
title: Getting Started
---

# Getting Started

With ConfigurableWarning, getting started is easy!

## Installing

After setting up a new BepInEx mod,
you can add the package to your project using the following:

# [CLI](#tab/cli)

```pwsh
dotnet add package RedstoneWizard08.ConfigurableWarning
```

# [CSProj](#tab/csproj)

```xml
<PackageReference Include="RedstoneWizard08.ConfigurableWarning" Version="1.13.2" IncludeAssets="compile" ExcludeAssets="RUNTIME" />
```

---

> [!NOTE]
> ConfigurableWarning is not yet actually published on NuGet.
> This is what it will be like in the future!

## Your First Option

Here's a basic option:

```cs
[RegisterOption]
private class MyOptionThing() // Class name is different here to show that it doesn't matter in state
    : IntOption("MyOption", 50, "My Option!", 0, 100, "MY STUFF", "GENERAL");
```

For more info, head to the [tutorials](./tutorials/) section!

## Accessing it's state

To access your option's state, you can use the <xref:ConfigurableWarning.API.Options.OptionsState>
manager. To access it, you'll use the same string that you used in your option's name above.
In this case, it's `"MyOption"`.

```cs
int? state = OptionsState.Instance.Get<int>("MyOption");
```

This code retrieves the optional state of the option. You can learn more about that
[here](../tutorials/state.md).
