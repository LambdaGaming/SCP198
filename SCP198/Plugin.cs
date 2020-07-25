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
			events.Server.EndingRound += EventHandlers.OnRoundEnd;
			events.Player.DroppingItem += EventHandlers.OnItemDrop;
			events.Player.Shot += EventHandlers.OnShoot;
			events.Player.ThrowingGrenade += EventHandlers.OnThrowGrenade;
			events.Player.MedicalItemUsed += EventHandlers.OnMedicalItemUsed;
			events.Scp914.UpgradingItems += EventHandlers.OnItemUpgrade;
			events.Player.InteractingDoor += EventHandlers.OnDoorInteract;
			events.Server.RoundStarted += EventHandlers.OnRoundStart;
			Log.Info( $"Successfully loaded." );
		}

		public override void OnDisabled()
		{
			base.OnDisabled();
			events.Player.PickingUpItem -= EventHandlers.OnItemPickup;
			events.Server.EndingRound -= EventHandlers.OnRoundEnd;
			events.Player.DroppingItem -= EventHandlers.OnItemDrop;
			events.Player.Shot -= EventHandlers.OnShoot;
			events.Player.ThrowingGrenade -= EventHandlers.OnThrowGrenade;
			events.Player.UsingMedicalItem -= EventHandlers.OnMedicalItemUsed;
			events.Scp914.UpgradingItems -= EventHandlers.OnItemUpgrade;
			events.Player.InteractingDoor -= EventHandlers.OnDoorInteract;
			events.Server.RoundStarted -= EventHandlers.OnRoundStart;
			EventHandlers = null;
		}
	}
}
