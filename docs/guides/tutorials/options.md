---
uid: Tutorials.Options
title: Options
---

# Options

## Creating an Option

To create an option, you'll need a few things.

You'll start with the parent class:

```csharp
[Group(/* Tab = */ "MY STUFF", /* Group = */ "GENERAL")]
public static class MyOptions { }
```

This class is used to group all of your options together. The `Group` attribute is used to
define the tab and category that the options will appear in.

<details>
<summary>Multiple groups in one tab</summary>

You can also have multiple groups in one tab! This is useful if you have a lot of options.

```csharp
[Tab("MY STUFF")]
public static class MyOptions {
    [Group("GENERAL")]
    public static class General { }

    [Group("ADVANCED")]
    public static class Advanced { }
}
```

</details>

Then, the option class, which is placed inside your group class:

```cs
[Register]
private class MyOptionThing()
    : IntOption(Keys.MyOption, 50, "My Option!", 0, 100);
```

> [!NOTE]
> For information about the `Keys` class, see [State](./state.md).

You'll notice a few things here. Let's break it down.

The `[RegisterOption]` comes from <xref:ConfigurableWarning.API.Attributes.Register>, and
tells ConfigurableWarning to automatically register your option to the game, meaning that
you don't have to do that!

The inherited class, <xref:ConfigurableWarning.API.Options.IntOption>, defines this as an
<xref:ConfigurableWarning.API.Options.IOption`1> with the <xref:System.Int32> type for its
value. This constructor takes a few arguments, namely:

1. The option's name - Here it's `"MyOption"`. This is used in the state and registry, and the
   end-user will likely never see it.
2. The default value - Here it's `50`.
3. The option's display name - Here it's `"My Option!"`. This is what the user will see in the
   settings menu.
4. The minimum value - Here it's `0`. This is the <u>minumum</u> value that the slider will go to.
5. The maximum value - Here it's `100`. This is the <u>maximum</u> value that the slider will go to.

> [!NOTE]
> Unfortunately, due to the nature of how Zorro's Settings API works, each option has
> to be its own class.

