using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;

namespace SCP198
{
	public sealed class Config : IConfig
	{
		[Description( "Indicates whether the plugin is enabled or not" )]
		public bool IsEnabled { get; set; } = true;

		[Description( "Whether or not debug messages should be shown in the console." )]
		public bool Debug { get; set; } = false;

		[Description( "Chance of an item getting possessed by SCP-198 when picked up by a player." )]
		public double PossessionChance { get; private set; } = 1;

		[Description( "Chance of player dying when upgrading a possessed item in SCP-914. (Set to -1 to disable)" )]
		public double UpgradeDeathChance { get; private set; } = 50;

		[Description( "List of items that won't be possessed by SCP-198." )]
		public List<string> BlacklistedItems { get; private set; } = new List<string>() {};

		[Description( "Whether or not notifications pushed by this plugin should be suppressed." )]
		public bool SuppressNotifications { get; private set; } = false;

		[Description( "Death reason that displays when a player is killed by SCP-198." )]
		public string DeathMessage { get; private set; } = "Attempting to forcefully remove SCP-198";

		[Description( "Broadcast message that displays when an item infected with SCP-198 is picked up. Use {0} for the name of the item." )]
		public string BroadcastPickup { get; private set; } = "<color=red>The {0} binds tightly to your hand. You can't seem to remove it...</color>";

		[Description( "Broadcast message that displays when a player tries to use an item that's infected with SCP-198." )]
		public string BroadcastItemUse { get; private set; } = "<color=red>You attempt to use the item but it just sticks to your hand...</color>";

		[Description( "Broadcast message that displays when a player attempts to drop an item infected with SCP-198. Use {0} for the name of the item." )]
		public string BroadcastDrop { get; private set; } = "<color=red>You attempt to remove the {0} from your hand but it won't budge.</color>";
	}
}
