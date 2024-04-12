# ConfigurableWarning

Makes the game configurable!
This is a quality-of-life mod to help you make the game as hard (or as easy) as you want!

## Caveats

- I have not had a chance to test this much in multiplayer, so beware of bugs!
- Hopefully configs will sync but I'm not sure.
- Configs should be host-only! Changes by anyone but the host should not apply!

## Current Settings

- Max Oxygen
    - Type: Float
    - Default: 500.0
    - The maximum oxygen (in seconds) for a player.
- Max Health
    - Type: Float
    - Default: 100.0
    - The maximum health for a player.
- Oxygen Usage Multiplier
    - Type: Float
    - Default: 1.0
    - How fast a player's oxygen will drain.
- Use Oxygen in Dive Bell
    - Type: Bool
    - Default: False
    - Whether or not the player's oxygen decreases in the dive bell.
- Days Per Quota
    - Type: Enum - [3, 5, 7, 10, 14, 18, 20]
    - Default: 3
    - How many days per quota period.

## Information

This project contains code based on [ContentSettings](https://github.com/Commander-Cat101/ContentSettings), which
is licensed under GPL v3. Go check them out!
