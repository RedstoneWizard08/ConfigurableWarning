---
uid: Tutorials.State
title: State
---

# State

State in ConfigurableWarning will always be held in the
<xref:ConfigurableWarning.API.States> class. You can check its
documentation for a list of fields and methods.

## Keys

It is recommended to have a class of keys to store common values.

Example:

```cs
public static class Keys {
    public const string MyOption = "MyOption";
}

// Then access the state

int? state = States.Ints[Keys.MyOption];
```

If you use the key instead, it eliminates the risk of a typo somewhere in the code.

## Modifying State

The <xref:ConfigurableWarning.API.States> also allows you to modify state
manually! Just call other methods.

Example:

```cs
States.Ints[Keys.MyOption] = ...; // Here, `...` is the value to set it to.
```

## Syncing

Whenever an option is changed, the state is automatically synced to every
client in the game. This is done through @RugbugRedfern's awesome Mycelium Networking system,
as to not put any strain on the internal Pun-based system (which also costs the devs money!).

In the future, there will be a way to disable the syncing behavior, but for now, it's always on.
