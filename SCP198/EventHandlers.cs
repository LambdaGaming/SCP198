using EXILED;
using EXILED.Extensions;
using System;

namespace SCP198
{
	public class EventHandlers
	{
		public Plugin plugin;
		Random rand = new Random();
		bool SCPActive = false;
		ItemType SCPID;

		public EventHandlers( Plugin plugin ) => this.plugin = plugin;

		public bool IsBlacklisted( ItemType item )
		{
			ItemType[] blacklist = {
				ItemType.Ammo556,
				ItemType.Ammo762,
				ItemType.Ammo9mm,
				ItemType.GrenadeFlash,
				ItemType.GrenadeFrag
			};
			foreach ( ItemType blacklisted in blacklist )
			{
				if ( blacklisted == item ) return true;
			}
			return false;
		}

		public void OnItemPickup( ref PickupItemEvent ev )
		{
			if ( !SCPActive && !IsBlacklisted( ev.Item.ItemId ) && rand.Next( 1, 101 ) <= 5 ) // 5% chance of the item being possessed
			{
				SCPActive = true;
				SCPID = ev.Item.ItemId;
				ev.Player.Broadcast( 6, "<color=red>Items of this type have been possessed by SCP-198 and can no longer be dropped!</color>" );
				foreach ( ReferenceHub hub in Player.GetHubs() )
				{
					if ( hub != ev.Player )
					{
						try
						{
							Item item = hub.inventory.GetItemByID( SCPID );
							hub.Broadcast( 6, "<color=red>Items of the type " + item.label + " have been possessed by SCP-198 and can no longer be dropped!</color>" );
						}
						catch
						{
							Log.Error( "Error getting possessed item name." );
						}
					}
				}
			}
			if ( SCPActive && ev.Item.ItemId == SCPID )
				ev.Player.Broadcast( 6, "<color=red>Items of this type have been possessed by SCP-198 and can no longer be dropped!</color>" );
		}

		public void OnShoot( ref ShootEvent ev )
		{
			if ( SCPActive && ev.Shooter.inventory.GetItemInHand().id == SCPID )
			{
				ev.Shooter.Kill();
				ev.Shooter.Broadcast( 6, "<color=red>You died attempting to forcefully remove SCP-198.</color>" );
			}
		}

		public void OnMedicalItemUsed( UsedMedicalItemEvent ev )
		{
			if ( SCPActive && ev.ItemType == SCPID )
			{
				ev.Player.Kill();
				ev.Player.Broadcast( 6, "<color=red>You died attempting to forcefully remove SCP-198.</color>" );
			}
		}

		public void OnItemDrop( ref DropItemEvent ev )
		{
			if ( ev.Item.id == SCPID ) // Unforunately I have to make it so all of one item type is posessed since the pickup item event doesn't support the item's unique ID
			{
				ev.Allow = false;
				ev.Player.Broadcast( 6, "<color=red>This item is possessed by SCP-198 and cannot be dropped.</color>" );
			}
		}

		public void OnRoundEnd()
		{
			SCPActive = false;
			SCPID = 0;
		}
	}
}