# SCP198
EXILED plugin for SCP:SL that has a chance of possessing a picked up item with SCP-198. Full features are below.
- Each time a player picks up an item, there is a 5% chance of that item getting possessed with SCP-198. (Excludes grenades and ammo.)
- When an item gets possessed, all items of that type are possessed as well.
- Only one type of item will be possessed each round.
- All players on the server will be notified when an item becomes possessed.
- Players who pick up a possessed item will be notified about it.
- If the possessed item is a medical item, gun, or keycard, attempting to use it will result in death.
- Attempting to upgrade a possessed item in SCP-914 while in a players hand will result in death. Works best with [KadeDev's Common-Utils plugin.](https://github.com/KadeDev/Common-Utils)

# Configs
Name | Type | Default Value | Description
:---: | :---: | :---: | :------
198_enabled | bool | true | Whether or not the plugin should load when the server starts.
198_shooter_death | bool | true | Whether or not players should die when shooting a possessed gun.
198_medic_death | bool | true | Whether or not players should die when using a possessed medical item.
198_upgrade_death | bool | true | Whether or not players should die when upgrading a possessed item in SCP-914.
198_keycard_death | bool | true | Whether or not players should die when opening a door with a possessed keycard.
198_possession_chance | int | 5 | Chance out of 100 of an item getting possessed when a player picks it up.
