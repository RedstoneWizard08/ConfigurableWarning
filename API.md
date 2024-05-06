# API

ConfigurableWarning now has an API! Here's an example:

```cs
// A setting called "MySetting", that is a toggle (bool), defaulting to
// true, with the description "My Setting!", in the tab "MY STUFF" with
// a category of "THINGS" and no apply actions.
[RegisterOption]
private class MySetting() : BoolOption("MySetting", true, "My Setting!", "MY STUFF", "THINGS", []);
```

More documentation to come!
