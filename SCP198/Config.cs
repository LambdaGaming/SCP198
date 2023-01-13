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
		public double PossessionChance { get; private set; } = 0.1;

		[Description( "Chance of player dying when upgrading a possessed item in SCP-914. (Set to -1 to disable)" )]
		public double UpgradeDeathChance { get; private set; } = 50;

		[Description( "List of items that won't be possessed by SCP-198." )]
		public List<string> BlacklistedItems { get; private set; } = new List<string>() {};

		[Description( "Whether or not notifications pushed by this plugin should be suppressed." )]
		public bool SuppressNotifications { get; private set; } = false;
	}
}
