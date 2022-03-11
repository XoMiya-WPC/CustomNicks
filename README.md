# CustomNicks

<h1>Intro</h1>

A plugin for SCP: SL that allows setting of nicknames for players that will be applied instantly and stay until removed. Ingame Commands to create and view nicknames. Common usage of this plugin is to combat inappropriate nicknames, or those whose names do not appear correctly ingame making moderation more difficult.

<h1>Requirements</h1>

This plugin requires [EXILED](https://github.com/Exiled-Team/EXILED/releases "Exiled Releases") `5.0.0`.
This plugin **WILL NOT WORK** on previous versions.
<h1>General Config</h1>

| Config  | Type | Def Value |
| ------------- | ------------- | ------------- |
| `is_enabled`  | Boolean  | true  |
| `enable_debug`  | Boolean  | false  |

* **is_enabled:** Defines if the plugin will be enabled or not. Only enter `true` or `false`.
* **enable_debug:** Defines if the plugin will print extra debug lines. Only enter `true` or `false`.


<h1>Commands</h1>

* **CNICK** - Parent command for the plugin which has 4 options for the subcommand, `add`, `update`, `remove`, `lookup`. The correct format for each is below.
> **cnick add (userid/playerid) (nickname)** *replace userid / playerid with either their ingame id (normally a one or two digit number or their userid@steam). Anything typed after this will be counted as their nickname. This will add a new player to the file.*

> **cnick update (userid/playerid) (nickname)** *replace userid / playerid with either their ingame id (normally a one or two digit number or their userid@steam). Anything typed after this will be counted as their nickname This will only work if the user already exists and overite their current nickname.*

> **cnick remove (userid/playerid)** *replace userid / playerid with either their ingame id (normally a one or two digit number or their userid@steam). This will remove them and delete their nickname from the file. Their nickname will be reverted automatically.*

> **cnick lookup (userid/playerid)** *replace userid / playerid with either their ingame id (normally a one or two digit number or their userid@steam). This will display the players current nickname set if they have one.*

* **CNLIST** - Will output a table of all the players with assigned nicknames in the order they were added.

* **CNRELOAD** - This will ask the plugin to request the latest version of the dictionary from the file.

<h1>Permissions</h1>

* **CNICK** requires the `customnicks.cnick` permission.
* **CNLIST** requires the `customnicks.list` permission.
* **CNRELOAD** requires the `customnicks.Reload` permission.

<h1>Info & Contact</h1>

This plugin was made originally to combat custom characters in steam names which make moderation difficult.

Special thanks to SirMeepington and Masonic for keeping me in the right direction whilst doing this.

For help or issues Contact me on Discord @ XoMiya#6113 or join my [discord](https://discord.gg/js4W9M5Csq "XoMiya's Kitchen")

![img](https://img.shields.io/github/downloads/XoMiya-WPC/CustomNicks/total?style=for-the-badge)

<h2>Default Config Generated</h2>

```yaml
CustomNicks:
# Is the plugin Enabled? - Accepts Bool (Def: true)
  is_enabled: true
  # Is debugging enabled? - Accepts Bool (Def: false)
  enable_debug: false
```
