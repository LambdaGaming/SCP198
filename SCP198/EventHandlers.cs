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
				ItemType.Ammo556, // Ammo is blacklisted since you can't drop it by default
				ItemType.Ammo762,
				ItemType.Ammo9mm
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
				if ( ev.Player == SCPPly && ev.Pickup.ItemId == SCPID && !plugin.Config.SuppressNotifications )
					ev.Player.Broadcast( 6, "<color=red>The " + ev.Player.Inventory.GetItemByID( SCPID ).label + " binds tightly to your hand. You can't seem to remove it...</color>" );
			}
			else
			{
				int infectchance = rand.Next( 1, 101 );
				if ( !IsBlacklisted( ev.Pickup.ItemId ) && infectchance <= plugin.Config.PossessionChance )
				{
					SCPActive = true;
					SCPID = ev.Pickup.ItemId;
					SCPPly = ev.Player;
					if ( !plugin.Config.SuppressNotifications )
					{
						ev.Player.Broadcast( 6, "<color=red>The " + ev.Player.Inventory.GetItemByID( SCPID ).label + " binds tightly to your hand. You can't seem to remove it...</color>" );
					}
				}
			}
		}

		public IEnumerator<float> KillUser( Player ply )
		{
			yield return Timing.WaitForSeconds( 0.5f ); // Some events need this small timer or else it won't work
			ply.Kill();
			if ( !plugin.Config.SuppressNotifications )
				ply.Broadcast( 6, "<color=red>You died attempting to forcefully remove SCP-198.</color>" );
		}

		public void OnShoot( ShotEventArgs ev )
		{
			if ( plugin.Config.ShooterDeath && SCPActive && ev.Shooter == SCPPly && ev.Shooter.Inventory.curItem == SCPID )
				Timing.RunCoroutine( KillUser( ev.Shooter ) );
		}

		public void OnThrowGrenade( ThrowingGrenadeEventArgs ev )
		{
			if ( plugin.Config.GrenadeDeath && SCPActive && ev.Player == SCPPly && ev.Player.Inventory.curItem == SCPID )
				Timing.RunCoroutine( KillUser( ev.Player ) );
		}

		public void OnMedicalItemUsed( UsedMedicalItemEventArgs ev )
		{
			if ( plugin.Config.MedicDeath && SCPActive && ev.Player == SCPPly && ev.Player.Inventory.curItem == SCPID )
				Timing.RunCoroutine( KillUser( ev.Player ) );
		}

		public void OnDoorInteract( InteractingDoorEventArgs ev )
		{
			if ( plugin.Config.KeycardDeath && SCPActive && ev.Player == SCPPly && ev.Player.Inventory.curItem == SCPID )
				Timing.RunCoroutine( KillUser( ev.Player ) );
		}

		public void OnItemUpgrade( UpgradingItemsEventArgs ev )
		{
			if ( plugin.Config.UpgradeDeath && SCPActive )
			{
				int chance = plugin.Config.UpgradeDeathChance;
				int randchance = rand.Next( 0, 101 );
				if ( randchance <= chance )
				{
					foreach ( Player ply in ev.Players )
					{
						if ( ply.Inventory.curItem == SCPID && ply == SCPPly )
						{
							Timing.RunCoroutine( KillUser( ply ) );
						}
					}
				}
			}
		}

		public void OnItemDrop( DroppingItemEventArgs ev )
		{
			if ( ev.Item.id == SCPID && ev.Player == SCPPly )
			{
				ev.IsAllowed = false;
				if ( !plugin.Config.SuppressNotifications )
					ev.Player.Broadcast( 6, "<color=red>You attempt to remove the " + ev.Player.Inventory.GetItemByID( SCPID ).label + " from your hand but it won't budge.</color>" );
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
