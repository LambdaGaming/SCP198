using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;

namespace SCP198
{
	public sealed class Config : IConfig
	{
		[Description( "Indicates whether the plugin is enabled or not" )]
		public bool IsEnabled { get; set; } = true;

		[Description( "Whether or not players should die from shooting a gun infected by SCP-198." )]
		public bool ShooterDeath { get; private set; } = true;

		[Description( "Whether or not players should die from using a medical item infected by SCP-198." )]
		public bool MedicDeath { get; private set; } = true;

		[Description( "Whether or not players should die from upgrading an item infected by SCP-198 in SCP-914." )]
		public bool UpgradeDeath { get; private set; } = true;

		[Description( "Whether or not players should die from using a keycard infected by SCP-198." )]
		public bool KeycardDeath { get; private set; } = true;

		[Description( "Whether or not players should die from using a grenade infected by SCP-198." )]
		public bool GrenadeDeath { get; private set; } = true;

		[Description( "Chance of an item getting possessed by SCP-198 when picked up by a player." )]
		public double PossessionChance { get; private set; } = 5.0f;

		[Description( "Chance of player dying when upgrading a possessed item in SCP-914. (Must be a whole number)" )]
		public int UpgradeDeathChance { get; private set; } = 50;

		[Description( "List of items that won't be infected by SCP-198." )]
		public List<string> BlacklistedItems { get; private set; } = new List<string>() {};
	}
}
