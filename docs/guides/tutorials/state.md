---
uid: Tutorials.State
title: State
---

# State

State in ConfigurableWarning will always be held in the
<xref:ConfigurableWarning.API.Options.OptionsState> class. You can check its
documentation for a list of fields and methods.

## Keys

It is recommended to have a class of keys to store common values.

Example:

```cs
public static class Keys {
    public const string MyOption = "MyOption";
}

// Then access the state

var state = OptionsState.Instance.Get<...>(Keys.MyOption);
```

If you use the key instead, it eliminates the risk of a typo somewhere in the code.

## Modifying State

The <xref:ConfigurableWarning.API.Options.OptionsState> also allows you to modify state
manually! Just call other methods.

Examples:

```cs
OptionsState.Instance.Set(Keys.MyOption, ...); // Here, `...` is the value to set it to.
```

```cs
OptionsState.Instance.Remove<...>(Keys.MyOption); // Here, `...` is the option's type.
```

> [!NOTE]
> Yes, I do know that this type parameter doesn't need to exist. For the string
> overload, I'll be removing the type parameter soon.

## Checking State

You can also check state! The <xref:ConfigurableWarning.API.Options.OptionsState.Has> method
will allow you to check if the option is registered to the state.

```cs
bool exists = OptionsState.Instance.Has<...>(Keys.MyOption);
```

> [!NOTE]
> Yes, I do know that this type parameter doesn't need to exist. For the string
> overload, I'll be removing the type parameter soon.

## Updating State

You can also have the state automatically update based on the option's contained value!
Just use the <xref:ConfigurableWarning.API.Options.OptionsState.Update> method.

```cs
IOption<...> myOption = ...;

OptionsState.Instance.Update<...>(myOption);
```

Note that you *do* need an instance of the option to use this method.
