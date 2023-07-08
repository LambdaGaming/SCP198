using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp914;
using Exiled.Events.EventArgs.Server;
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
				ItemType.GrenadeFlash, // Grenades cannot be blocked from being thrown due to a base game bug, so blacklist them for now
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
			if ( ev.Player == null || ev.Item == null ) return; // Prevents errors caused by picking up items from christmas trees and maybe other stuff too
			if ( SCPID > 0 )
			{
				if ( ev.Item.Serial == SCPID && !plugin.Config.SuppressNotifications )
					ev.Player.Broadcast( 6, "<color=red>The " + ev.Item.Type.ToString() + " binds tightly to your hand. You can't seem to remove it...</color>" );
			}
			else
			{
				int infectchance = rand.Next( 1, 101 );
				if ( !IsBlacklisted( ev.Item.Type ) && infectchance <= plugin.Config.PossessionChance )
				{
					SCPID = ev.Item.Serial;
					if ( !plugin.Config.SuppressNotifications )
						ev.Player.Broadcast( 6, "<color=red>The " + ev.Item.Type.ToString() + " binds tightly to your hand. You can't seem to remove it...</color>" );
				}
			}
		}

		public IEnumerator<float> KillUser( Player ply )
		{
			yield return Timing.WaitForSeconds( 0.5f ); // Some events need this small timer or else it won't work
			ply.Kill( "Attempting to forcefully remove SCP-198" );
		}

		public void OnThrowGrenade( ThrowingRequestEventArgs ev )
		{
			// Temporarily disabled due to a bug with the base game
			/*if ( ev.Player.CurrentItem.Serial == SCPID )
			{
				ev.IsAllowed= false;
				if ( !plugin.Config.SuppressNotifications )
					ev.Player.Broadcast( 6, "<color=red>You attempt to throw the item but it just sticks to your hand...</color>", shouldClearPrevious: true );
			}*/
		}

		public void OnItemUse( UsingItemEventArgs ev )
		{
			if ( ev.Item.Serial == SCPID && ev.Item.IsConsumable )
			{
				ev.IsAllowed = false;
				if ( !plugin.Config.SuppressNotifications )
					ev.Player.Broadcast( 6, "<color=red>You attempt to use the item but it just sticks to your hand...</color>", shouldClearPrevious: true );
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
					ev.Player.Broadcast( 6, "<color=red>You attempt to remove the " + ev.Item.Type.ToString() + " from your hand but it won't budge.</color>", shouldClearPrevious: true );
			}
		}

		public void OnRoundEnd( RoundEndedEventArgs ev )
		{
			SCPID = 0;
		}

		public void OnRoundStart()
		{
			if ( SCPID > 0 )
			{
				SCPID = 0;
				Log.Warn( "SCP-198 was not reset after the round ended. Resetting now..." );
			}
		}
	}
}
