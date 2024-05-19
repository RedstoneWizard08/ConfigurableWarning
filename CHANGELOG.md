# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [v1.14.0] - 2024-05-18

### Added

- New settings DSL!
- Other mods compat!
- Better organization!
- Annotations!
- Much more that I forgor!

## [v1.13.2] - 2024-05-09

### Added

- New Developer API (`ConfigurableWarning.API`)
- Added documentation built with [DocFX](https://dotnet.github.io/docfx/)
- Added CI for docs
- Added new days-per-quota patch
- Added new state cache
- Added new option loader
- Added `[RegisterOption]` annotation
- Added clamp toggles (via a patch to `Zorro.Settings` and `ContentSettings`)
- Added `BoolOption`, `FloatOption`, `IntOption`, and `TextOption`
- Added documentation comments to all API classes

### Changed

- Updated README
- Migrated existing settings to the new API
- Migrated patches to the new API
- Reorganized project
- Switched to [Cake](https://cakebuild.net/) (Frosting) for builds
- Switched to [Thunderstore CLI](https://github.com/thunderstore-io/thunderstore-cli)/[Cake](https://cakebuild.net/) for packaging
- Switched mostly to Rider (for development)
- Moved the syncer from the `Plugin` class to an instance of `OptionSyncer`
- Improved code structure
- Moved namespaces to the top instead of a block

### Removed

- Removed old (legacy) package script
- Removed old days-per-quota patch

## [v1.12.0] - 2024-05-05

### Added

- Added diving bell settings

### Changed

- Updated sync

## [v1.11.0] - 2024-05-02

### Added

- Added depdendencies

### Changed

- Fixed settings sync (again)
- Made UI elements not borked
- Redid settings packing
- Fixed for CW's new May 2nd release

## [v1.10.2] - 2024-05-01

### Changed

- Hopefully fix settings sync

## [v1.10.1] - 2024-05-01

### Changed

- Actually fixed the build

## [v1.10.0] - 2024-04-29

### Changed

- Updated settings UI to be cleaner
- Switched fully to Mycelium Networking
- Cached settings values
- Building in release mode! (Yes, I wasn't before.)
- Removed BepInEx config because it kinda broke stuff

## [v1.9.2] - 2024-04-19

### Changed

- Fixed settings sync

## [v1.9.1] - 2024-04-18

### Changed

- Nothing, just updated the README. :)

## [v1.9.0] - 2024-04-18

### Changed

- Refactored UI
- Moved to using ContentSettings and MyceliumNetworking

## [v1.8.1] - 2024-04-12

### Changed

- Fixed a default config value
- Hopefully fix the lobby patch

## [v1.8.0] - 2024-04-12

### Changed

- Fixed health & days left HUD to actually reflect the correct value (in real time)

## [v1.7.1] - 2024-04-12

### Changed

- Fixed lobby stuff (I actually applied the patch this time!)
- Fixed patches not using `nameof`

## [v1.7.0] - 2024-04-12

### Added

- Modified ContentSettings API is now built-in
- Settings patch
- `TextSetting` and `TextSettingsUI` for blank "settings" (basically a label/divider)
- `Common*Setting` classes for ease
- Basic register & setup function for settings (DX stuff)

### Changed

- Reorganized settings to make it easier to work with
- Fixed a few issues
- Made the patches better
- We no longer rely on ContentSettings!

## [v1.6.0] - 2024-04-11

### Added

- Added oxygen refill-related settings
- Added private lobbies patch & config

### Changed

- Changed the sprint multiplier max from 4 to 10

## [v1.5.0] - 2024-04-11

### Changed

- Moved to weird(-er) patching methods instead of overwriting methods for maximum compatibility (pretend that had echo
  in it)
- Removed redundant stuff
- Made config sync hopefully better
- Hopefully didn't break anything :pray:
