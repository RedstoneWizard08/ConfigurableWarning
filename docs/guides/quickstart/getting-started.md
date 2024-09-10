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
<PackageReference Include="RedstoneWizard08.ConfigurableWarning" Version="1.15.2" IncludeAssets="compile" ExcludeAssets="RUNTIME" />
```

---

## Your First Option

While the legacy `ContentSettings` API still works, we recommend using the newer API instead.
It's more powerful and easier to use!

Here's a basic option:

```cs
[Group("MY STUFF", "GENERAL")]
public static class MyOptions {
    [Register]
    private class MyOptionThing()
        : IntOption("MyOption", 50, "My Option!", 0, 100);
}
```

For more info, head to the [tutorials](../tutorials/index.md) section!

## Accessing its State

To access your option's state, you can use the <xref:ConfigurableWarning.API.States>
manager. To access it, you'll use the same string that you used in your option's name above.
In this case, it's `"MyOption"`.

```cs
int? state = States.Ints["MyOption"];
```

This code retrieves the optional state of the option. You can learn more about that
[here](../tutorials/state.md).
