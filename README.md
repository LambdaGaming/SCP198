# SCP-198
EXILED plugin for SCP:SL that has a chance of possessing a picked up item with SCP-198.

# Features
- Each time a player picks up an item, there is a small chance (default 0.5%) of that item getting possessed with SCP-198. (Excludes ammo, armor, and throwables)
- When an item gets possessed, the player who picked it up will not be able to remove it from their inventory unless they die.
- If the possessed item is a consumable, that item cannot be used.
- Only a single item can be possessed each round. Possessed items will keep their status for the remainder of the round, even if they are dropped when a player dies.
- Attempting to upgrade a possessed item in SCP-914 while in a players hand has a chance (default 50%) of killing them. Only works if the '914-mode' setting in the server config is set to 'Held' or 'DroppedAndHeld'.

# Building
 The project files are intended to be built using either the command line or VSCode with the C# Dev Tools extension, but Visual Studio should work too. You will need to have the SCP:SL dedicated server and .NET SDK 8 or above installed. You will also need to change the reference paths in the .csproj file to the location of your dedicated server.

# Contributing
 Contributions are welcome! Please read through the [guidelines](https://lambdagaming.github.io/guides/contributing) before submitting an issue or pull request.
