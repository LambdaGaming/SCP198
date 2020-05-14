using EXILED;
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

		public void OnItemPickup( ref PickupItemEvent ev )
		{
			if ( !SCPActive && rand.Next( 1, 101 ) <= 5 ) // 5% chance of the item being posessed
			{
				SCPActive = true;
				SCPID = ev.Item.ItemId;
				ev.Player.Broadcast( 6, "<color=red>Items of this type have been possessed by SCP-198 and can no longer be dropped!</color>" );
			}
			if ( SCPActive && ev.Item.ItemId == SCPID )
			{
				ev.Player.Broadcast( 6, "<color=red>Items of this type have been possessed by SCP-198 and can no longer be dropped!</color>" );
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