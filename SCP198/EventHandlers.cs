using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp914;
using MEC;
using System;
using System.Collections.Generic;

namespace SCP198
{
	public class EventHandlers
	{
		private Plugin plugin;
		private Random rand = new Random();
		private static ushort SCPID = 0;

		public EventHandlers( Plugin plugin ) => this.plugin = plugin;

		public bool IsBlacklisted( ItemType item )
		{
			List<ItemType> CustomBlacklist = new List<ItemType>();

			foreach ( string i in plugin.Config.BlacklistedItems )
				CustomBlacklist.Add( ( ItemType ) Enum.Parse( typeof( ItemType ), i, true ) );

			ItemType[] blacklist = {
				ItemType.Ammo556x45, // Ammo and armor blacklisted since they're not part of the normal inventory
				ItemType.Ammo762x39,
				ItemType.Ammo9x19,
				ItemType.Ammo12gauge,
				ItemType.Ammo44cal,
				ItemType.ArmorCombat,
				ItemType.ArmorHeavy,
				ItemType.ArmorLight,
				ItemType.GrenadeFlash, // Throwables cannot be blocked from being used
				ItemType.GrenadeHE,
				ItemType.SCP018,
				ItemType.SCP2176
			};

			foreach ( ItemType blacklisted in blacklist )
				if ( blacklisted == item ) return true;

			if ( CustomBlacklist != null && !CustomBlacklist.IsEmpty() )
			{
				foreach ( ItemType blacklisted in CustomBlacklist )
					if ( blacklisted == item ) return true;
			}
			return false;
		}

		public void OnItemPickup( ItemAddedEventArgs ev )
		{
			// Prevents errors caused by picking up invalid items, and prevents starting items from being infected
			if ( ev.Player == null || ev.Item == null || ev.Pickup == null )
				return;

			if ( SCPID > 0 )
			{
				if ( ev.Item.Serial == SCPID && !plugin.Config.SuppressNotifications )
					ev.Player.Broadcast( 6, string.Format( plugin.Config.BroadcastPickup, ev.Item.Type.ToString() ) );
			}
			else
			{
				double infectchance = rand.NextDouble() * 100;
				if ( !IsBlacklisted( ev.Item.Type ) && infectchance <= plugin.Config.PossessionChance )
				{
					SCPID = ev.Item.Serial;
					if ( !plugin.Config.SuppressNotifications )
						ev.Player.Broadcast( 6, string.Format( plugin.Config.BroadcastPickup, ev.Item.Type.ToString() ) );
				}
			}
		}

		public IEnumerator<float> KillUser( Player ply )
		{
			yield return Timing.WaitForSeconds( 0.5f ); // Some events need this small timer or else it won't work
			ply.Kill( plugin.Config.DeathMessage );
		}

		public void OnItemUse( UsingItemEventArgs ev )
		{
			if ( ev.Item.Serial == SCPID && ev.Item.IsConsumable )
			{
				ev.IsAllowed = false;
				if ( !plugin.Config.SuppressNotifications )
					ev.Player.Broadcast( 6, plugin.Config.BroadcastItemUse, shouldClearPrevious: true );
			}
		}

		public void OnItemUpgrade( UpgradingInventoryItemEventArgs ev )
		{
			if ( ev.Player.CurrentItem.Serial == SCPID )
			{
				double chance = plugin.Config.UpgradeDeathChance;
				int randchance = rand.Next( 0, 101 );
				if ( randchance <= chance )
				{
					Timing.RunCoroutine( KillUser( ev.Player ) );
				}
			}
		}

		public void OnItemDrop( DroppingItemEventArgs ev )
		{
			if ( ev.Item.Serial == SCPID )
			{
				ev.IsAllowed = false;
				if ( !plugin.Config.SuppressNotifications )
					ev.Player.Broadcast( 6, string.Format( plugin.Config.BroadcastDrop, ev.Item.Type.ToString() ), shouldClearPrevious: true );
			}
		}

		public void OnRoundStart()
		{
			SCPID = 0;
		}
	}
}
