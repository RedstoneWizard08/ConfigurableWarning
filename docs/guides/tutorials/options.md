---
uid: Tutorials.Options
title: Options
---

# Options

## Creating an Option

To create an option, you'll need a few things.

You'll start with the class:

```cs
[RegisterOption]
private class MyOptionThing() // Class name is different here to show that it doesn't matter in state
    : IntOption(Keys.MyOption, 50, "My Option!", 0, 100, "MY STUFF", "GENERAL");
```

> [!NOTE]
> For information about the `Keys` class, see [State](./state.md).

You'll notice a few things here. Let's break it down.

The `[RegisterOption]` comes from <xref:ConfigurableWarning.API.RegisterOption>, and
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
6. The tab - Here it's `"MY STUFF"`. This will be the tab that the setting shows up in.
7. The category - Here it's `"GENERAL"`. This will be the category that the setting appears under.

> [!NOTE]
> Unfortunately, due to the nature of how Zorro's Settings API works, each option has
> to be its own class.

