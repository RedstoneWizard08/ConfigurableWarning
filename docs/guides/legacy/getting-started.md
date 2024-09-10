---
uid: Legacy.GettingStarted
title: Getting Started
---

# Getting Started

With ContentSettings, getting started is easy!

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

## Your First Setting

Here's a basic setting:

```cs
public class MySetting : IntSetting, ICustomSetting {
    public override void ApplyValue() {
        // Do something with the value!
        // it's stored in the `Value` property
    }

    public override int GetDefaultValue() => 50;
    protected override (int, int) GetMinMaxValue() => (0, 100);
    public string GetDisplayName() => "My Setting";
}
```

## Registering the Setting

There are two ways to register a setting:

### [Using the `SettingRegister` attribute](#tab/attribute)

```cs
[SettingRegister("MY STUFF", "GENERAL")]
public class MySetting : IntSetting, ICustomSetting {
    // ...
}
```

### [Using the `SettingsLoader`](#tab/loader)

```cs
// Register MySetting to the tab "MY STUFF" and the group "GENERAL"
SettingsLoader.RegisterSetting("MY STUFF", "GENERAL", new MySetting());
```

---

### Accessing its State

To access your setting's state, you need to set it somewhere else
in the `ApplyValue` method.

Example:

```csharp
public static class MySettingValues {
    public static int MySettingValue = 50;
}

[SettingRegister("MY STUFF", "GENERAL")]
public class MySetting : IntSetting, ICustomSetting {
    public override void ApplyValue() {
        MySettingValues.MySettingValue = Value;
    }
}
```

In this example, you can access `MySetting`'s value from the `MySettingValues.MySettingValue` field.
