using Exiled.API.Enums;
using Exiled.API.Features;
using events = Exiled.Events.Handlers;

namespace SCP198
{
	public class Plugin : Plugin<Config>
	{
		private EventHandlers EventHandlers;

		public override PluginPriority Priority { get; } = PluginPriority.Medium;

		public override void OnEnabled()
		{
			base.OnEnabled();
			EventHandlers = new EventHandlers( this );
			EventHandlers = new EventHandlers( this );
			events.Player.PickingUpItem += EventHandlers.OnItemPickup;
			events.Server.RoundEnded += EventHandlers.OnRoundEnd;
			events.Player.DroppingItem += EventHandlers.OnItemDrop;
			events.Player.Shot += EventHandlers.OnShoot;
			events.Player.ThrowingItem += EventHandlers.OnThrowGrenade;
			events.Player.ItemUsed += EventHandlers.OnItemUsed;
			events.Scp914.UpgradingInventoryItem += EventHandlers.OnItemUpgrade;
			events.Player.InteractingDoor += EventHandlers.OnDoorInteract;
			events.Server.RoundStarted += EventHandlers.OnRoundStart;
			events.Player.Died += EventHandlers.OnPlayerDeath;
			Log.Info( $"Successfully loaded." );
		}

		public override void OnDisabled()
		{
			base.OnDisabled();
			events.Player.PickingUpItem -= EventHandlers.OnItemPickup;
			events.Server.RoundEnded -= EventHandlers.OnRoundEnd;
			events.Player.DroppingItem -= EventHandlers.OnItemDrop;
			events.Player.Shot -= EventHandlers.OnShoot;
			events.Player.ThrowingItem -= EventHandlers.OnThrowGrenade;
			events.Player.UsingItem -= EventHandlers.OnItemUsed;
			events.Scp914.UpgradingInventoryItem -= EventHandlers.OnItemUpgrade;
			events.Player.InteractingDoor -= EventHandlers.OnDoorInteract;
			events.Server.RoundStarted -= EventHandlers.OnRoundStart;
			events.Player.Died -= EventHandlers.OnPlayerDeath;
			EventHandlers = null;
		}
	}
}
