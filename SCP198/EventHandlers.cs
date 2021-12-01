using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using System;
using System.Collections.Generic;

namespace SCP198
{
	public class EventHandlers
	{
		private Plugin plugin;
		Random rand = new Random();
		static bool SCPActive = false;
		static ItemType SCPID = ItemType.None;
		static Player SCPPly = null;

		public EventHandlers( Plugin plugin ) => this.plugin = plugin;

		public bool IsBlacklisted( ItemType item )
		{
			List<ItemType> CustomBlacklist = ConvertToItems( plugin.Config.BlacklistedItems );
			ItemType[] blacklist = {
				ItemType.Ammo556x45, // Ammo is blacklisted since it's not part of the normal inventory
				ItemType.Ammo762x39,
				ItemType.Ammo9x19,
				ItemType.Ammo12gauge,
				ItemType.Ammo44cal
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

		public List<ItemType> ConvertToItems( List<string> blacklist )
		{
			if ( blacklist == null ) return null;
			List<ItemType> ItemList = new List<ItemType>();

			foreach ( string item in blacklist )
				ItemList.Add( ( ItemType ) Enum.Parse( typeof( ItemType ), item, true ) );

			return ItemList;
		}

		public void OnItemPickup( PickingUpItemEventArgs ev )
		{
			if ( SCPActive )
			{
				if ( ev.Player == SCPPly && ev.Pickup.Type == SCPID && !plugin.Config.SuppressNotifications )
					ev.Player.Broadcast( 6, "<color=red>The " + ev.Pickup.Type.ToString() + " binds tightly to your hand. You can't seem to remove it...</color>" );
			}
			else
			{
				int infectchance = rand.Next( 1, 101 );
				if ( !IsBlacklisted( ev.Pickup.Type ) && infectchance <= plugin.Config.PossessionChance )
				{
					SCPActive = true;
					SCPID = ev.Pickup.Type;
					SCPPly = ev.Player;
					if ( !plugin.Config.SuppressNotifications )
					{
						ev.Player.Broadcast( 6, "<color=red>The " + ev.Pickup.Type.ToString() + " binds tightly to your hand. You can't seem to remove it...</color>" );
					}
				}
			}
		}

		public IEnumerator<float> KillUser( Player ply )
		{
			yield return Timing.WaitForSeconds( 0.5f ); // Some events need this small timer or else it won't work
			ply.Kill( "Attempting to forcefully remove SCP-198" );
			if ( !plugin.Config.SuppressNotifications )
				ply.Broadcast( 6, "<color=red>You died attempting to forcefully remove SCP-198.</color>" );
		}

		public void OnShoot( ShotEventArgs ev )
		{
			if ( plugin.Config.ShooterDeath && SCPActive && ev.Shooter == SCPPly && ev.Shooter.CurrentItem.Type == SCPID )
				Timing.RunCoroutine( KillUser( ev.Shooter ) );
		}

		public void OnThrowGrenade( ThrowingItemEventArgs ev )
		{
			if ( plugin.Config.GrenadeDeath && SCPActive && ev.Player == SCPPly && ev.Player.CurrentItem.Type == SCPID )
				Timing.RunCoroutine( KillUser( ev.Player ) );
		}

		public void OnItemUsed( UsedItemEventArgs ev )
		{
			if ( plugin.Config.ItemDeath && SCPActive && ev.Player == SCPPly && ev.Player.CurrentItem.Type == SCPID )
				Timing.RunCoroutine( KillUser( ev.Player ) );
		}

		public void OnDoorInteract( InteractingDoorEventArgs ev )
		{
			if ( plugin.Config.KeycardDeath && SCPActive && ev.Player == SCPPly && ev.Player.CurrentItem.Type == SCPID )
				Timing.RunCoroutine( KillUser( ev.Player ) );
		}

		public void OnItemUpgrade( UpgradingInventoryItemEventArgs ev )
		{
			if ( plugin.Config.UpgradeDeath && SCPActive )
			{
				int chance = plugin.Config.UpgradeDeathChance;
				int randchance = rand.Next( 0, 101 );
				if ( randchance <= chance && ev.Player.CurrentItem.Type == SCPID && ev.Player == SCPPly )
				{
					Timing.RunCoroutine( KillUser( ev.Player ) );
				}
			}
		}

		public void OnItemDrop( DroppingItemEventArgs ev )
		{
			if ( ev.Item.Type == SCPID && ev.Player == SCPPly )
			{
				ev.IsAllowed = false;
				if ( !plugin.Config.SuppressNotifications )
					ev.Player.Broadcast( 6, "<color=red>You attempt to remove the " + ev.Item.Type.ToString() + " from your hand but it won't budge.</color>" );
			}
		}

		public void OnRoundEnd( RoundEndedEventArgs ev )
		{
			SCPActive = false;
			SCPID = ItemType.None;
			SCPPly = null;
		}

		public void OnRoundStart()
		{
			if ( SCPActive || SCPID != ItemType.None )
			{
				SCPActive = false;
				SCPID = ItemType.None;
				SCPPly = null;
				Log.Warn( "SCP-198 was not reset after the round ended. Resetting now..." );
			}
		}

		public void OnPlayerDeath( DiedEventArgs ev )
		{
			if ( ev.Target == SCPPly )
			{
				SCPActive = false;
				SCPID = ItemType.None;
				SCPPly = null;
			}
		}
	}
}
