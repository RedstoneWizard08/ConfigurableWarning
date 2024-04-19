# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.9.0] - 2024-04-18

### Changed

- Refactored UI
- Moved to using ContentSettings and MyceliumNetworking

## [1.8.1] - 2024-04-12

### Changed

- Fixed a default config value
- Hopefully fix the lobby patch

## [1.8.0] - 2024-04-12

### Changed

- Fixed health & days left HUD to actually reflect the correct value (in real time)

## [1.7.1] - 2024-04-12

### Changed

- Fixed lobby stuff (I actually applied the patch this time!)
- Fixed patches not using `nameof`

## [1.7.0] - 2024-04-12

### Changed

- Reorganized settings to make it easier to work with
- Fixed a few issues
- Made the patches better
- We no longer rely on ContentSettings!

### Added

- Modified ContentSettings API is now built-in
- Settings patch
- `TextSetting` and `TextSettingsUI` for blank "settings" (basically a label/divider)
- `Common*Setting` classes for ease
- Basic register & setup function for settings (DX stuff)

## [1.6.0] - 2024-04-11

### Added

- Added oxygen refill-related settings
- Added private lobbies patch & config

### Changed

- Changed the sprint multiplier max from 4 to 10

## [1.5.0] - 2024-04-11

### Changed

- Moved to weird(-er) patching methods instead of overwriting methods for maximum compatibility (pretend that had echo in it)
- Removed redundant stuff
- Made config sync hopefully better
- Hopefully didn't break anything :pray:
