# NoTCBlockEntity
**NoTCBlockEntity** is an [Oxide](https://umod.org/) plugin that blocks players from placing certain entities unless they are in the vicinity of a Tool Cupboard. Its primary purpose is to help address some of the limitations of the [EntityLimit](https://umod.org/plugins/entity-limit) plugin.
### Table of Contents  
- [Requirements](#requirements)  
- [Installation](#installation)  
- [Permissions](#permissions)  
- [Commands](#commands)  
- [Configuration](#configuration)  
- [Localization](#localization)  
- [Credits](#credits)
## Requirements
| Depends On | Works With |
| --- | --- |
| None | [EntityLimit](https://umod.org/plugins/entity-limit) |
## Installation
Download the plugin:
```bash
git clone https://github.com/rustireland/notcblockentity.git
```
Copy it to the Oxide plugins directory:
```bash
cp notcblockentity/NoTCBlockEntity.cs oxide/plugins
```
Oxide will compile and load the plugin automatically.
## Permissions
This plugin uses the Oxide permission system. To assign a permission, use `oxide.grant <user or group> <name or steam id> <permission>`. To remove a permission, use `oxide.revoke <user or group> <name or steam id> <permission>`.
- `notcblockentity.bypass` - Allows a user or group to bypass all blocks on entity deployment
## Commands
This plugin doesn't provide any console or chat commands.
## Configuration
The settings and options can be configured in the `NoTCBlockEntity.json` file under the `oxide/config` directory. The use of an editor and validator is recommended to avoid formatting issues and syntax errors.

When run for the first time, the plugin will create a default configuration file with *Auto Turrets*, *BBQ's*, and *Mixing Tables* blocked.
```json
{
  "Chat Icon (SteamID)": 0,
  "Entities to Block (use shortnames)": [
    "autoturret",
    "bbq",
    "mixingtable"
  ]
}
```
## Localization
The default messages are in the `NoTCBlockEntity.json` file under the `oxide/lang/en` directory. To add support for another language, create a new language folder (e.g. **de** for German) if not already created, copy the default language file to the new folder and then customize the messages.
```json
{
  "BlockedMessage": "<color=#ff6969>WARNING:</color> You must be within range of a <color=#ffa500>Tool Cupboard</color> to place a <color=#ffa500>{entity}</color>!"
}
```
# Credits
- [Agamemnon](https://github.com/agamemnon23) - Code, testing.
- [Black_demon6](https://github.com/TheBlackdemon6) - Initial concept, testing, and French translations.
