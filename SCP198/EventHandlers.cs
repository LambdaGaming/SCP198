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
		bool SCPActive = false;
		ItemType SCPID;

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
			if ( !SCPActive && !IsBlacklisted( ev.Pickup.ItemId ) && rand.Next( 1, 101 ) <= plugin.Config.PossessionChance )
			{
				SCPActive = true;
				SCPID = ev.Pickup.ItemId;
				ev.Player.Broadcast( 6, "<color=red>Items of this type have been possessed by SCP-198 and can no longer be dropped!</color>" );
				foreach ( Player ply in Player.List )
				{
					if ( ply != ev.Player )
					{
						try
						{
							Item item = ply.Inventory.GetItemByID( SCPID );
							ply.Broadcast( 6, "<color=red>Items of the type " + item.label + " have been possessed by SCP-198 and can no longer be dropped!</color>" );
						}
						catch
						{
							Log.Error( "Error getting possessed item name." );
						}
					}
				}
			}
			if ( SCPActive && ev.Pickup.ItemId == SCPID )
				ev.Player.Broadcast( 6, "<color=red>Items of this type have been possessed by SCP-198 and can no longer be dropped!</color>" );
		}

		public IEnumerator<float> KillShooter( Player shooter )
		{
			yield return Timing.WaitForSeconds( 0.5f );
			shooter.Kill();
			shooter.Broadcast( 6, "<color=red>You died attempting to forcefully remove SCP-198.</color>" );
		}

		public void OnShoot( ShotEventArgs ev )
		{
			Log.Warn( ev.Shooter.Inventory.GetItemInHand().id.ToString() );
			if ( plugin.Config.ShooterDeath && SCPActive && ev.Shooter.Inventory.GetItemInHand().id == SCPID )
				Timing.RunCoroutine( KillShooter( ev.Shooter ) );
		}

		public void OnThrowGrenade( ThrowingGrenadeEventArgs ev )
		{
			if ( plugin.Config.GrenadeDeath && SCPActive && ev.Player.Inventory.GetItemInHand().id == SCPID )
				Timing.RunCoroutine( KillShooter( ev.Player ) );
		}

		public void OnMedicalItemUsed( UsedMedicalItemEventArgs ev )
		{
			if ( plugin.Config.MedicDeath && SCPActive && ev.Player.Inventory.GetItemInHand().id == SCPID )
			{
				ev.Player.Kill();
				ev.Player.Broadcast( 6, "<color=red>You died attempting to forcefully remove SCP-198.</color>" );
			}
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
						if ( ply.Inventory.GetItemInHand().id == SCPID )
						{
							ply.Kill();
							ply.Broadcast( 6, "<color=red>You died attempting to forcefully remove SCP-198.</color>" );
						}
					}
				}
			}
		}

		public void OnDoorInteract( InteractingDoorEventArgs ev )
		{
			if ( plugin.Config.KeycardDeath && SCPActive && ev.Player.Inventory.GetItemInHand().id == SCPID )
			{
				ev.Player.Kill();
				ev.Player.Broadcast( 6, "<color=red>You died attempting to forcefully remove SCP-198.</color>" );
			}
		}

		public void OnItemDrop( DroppingItemEventArgs ev )
		{
			if ( ev.Item.id == SCPID )
			{
				ev.IsAllowed = false;
				ev.Player.Broadcast( 6, "<color=red>This item is possessed by SCP-198 and cannot be dropped.</color>" );
			}
		}

		public void OnRoundEnd( EndingRoundEventArgs ev )
		{
			SCPActive = false;
			SCPID = 0;
		}
	}
}
