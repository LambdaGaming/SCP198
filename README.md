# SCP-198
EXILED plugin for SCP:SL that has a chance of possessing a picked up item with SCP-198. Full features are below.
- Each time a player picks up an item, there is a 5% chance of that item getting possessed with SCP-198. (Excludes grenades and ammo.)
- When an item gets possessed, all items of that type are possessed as well.
- Only one type of item will be possessed each round.
- All players on the server will be notified when an item becomes possessed.
- Players who pick up a possessed item will be notified about it.
- If the possessed item is a medical item, gun, grenade, or keycard, attempting to use it will result in death.
- Attempting to upgrade a possessed item in SCP-914 while in a players hand will result in death. Works best with [KadeDev's Common-Utils plugin.](https://github.com/KadeDev/Common-Utils)

# Configs
Name | Type | Default Value | Description
:---: | :---: | :---: | :------
is_enabled | bool | true | Indicates whether the plugin is enabled or not.
shooter_death | bool | true | Whether or not players should die from shooting a gun possessed by SCP-198.
medic_death | bool | true | Whether or not players should die from using a medical item possessed by SCP-198.
upgrade_death | bool | true | Whether or not players should die from upgrading an item possessed by SCP-198 in SCP-914.
keycard_death | bool | true | Whether or not players should die from using a keycard possessed by SCP-198.
grenade_death | bool | true | Whether or not players should die from using a grenade possessed by SCP-198.
possession_chance | double | 5.0 | Chance of an item getting possessed by SCP-198 when picked up by a player.
upgrade_death_chance | int | 50 | Chance of player dying when upgrading a possessed item in SCP-914. (Must be a whole number)
blacklisted_items | string list | N/A | List of items that won't be possessed by SCP-198.
suppress_notifications | bool | false | Whether or not notifications pushed by this plugin should be suppressed.
