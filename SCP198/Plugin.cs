using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using events = Exiled.Events.Handlers;

namespace SCP198
{
	public class Plugin : Plugin<Config>
	{
		private EventHandlers EventHandlers;
		public override Version Version { get; } = new Version( 2, 1, 2 );
		public override Version RequiredExiledVersion { get; } = new Version( 8, 0, 0 );
		public override string Author { get; } = "OPGman";
		public override PluginPriority Priority { get; } = PluginPriority.Medium;

		public override void OnEnabled()
		{
			base.OnEnabled();
			EventHandlers = new EventHandlers( this );
			events.Player.ItemAdded += EventHandlers.OnItemPickup;
			events.Server.RoundEnded += EventHandlers.OnRoundEnd;
			events.Player.DroppingItem += EventHandlers.OnItemDrop;
			events.Player.ThrowingRequest += EventHandlers.OnThrowGrenade;
			events.Scp914.UpgradingInventoryItem += EventHandlers.OnItemUpgrade;
			events.Server.RoundStarted += EventHandlers.OnRoundStart;
			events.Player.UsingItem += EventHandlers.OnItemUse;
		}

		public override void OnDisabled()
		{
			base.OnDisabled();
			events.Player.ItemAdded -= EventHandlers.OnItemPickup;
			events.Server.RoundEnded -= EventHandlers.OnRoundEnd;
			events.Player.DroppingItem -= EventHandlers.OnItemDrop;
			events.Player.ThrowingRequest -= EventHandlers.OnThrowGrenade;
			events.Scp914.UpgradingInventoryItem -= EventHandlers.OnItemUpgrade;
			events.Server.RoundStarted -= EventHandlers.OnRoundStart;
			events.Player.UsingItem -= EventHandlers.OnItemUse;
			EventHandlers = null;
		}
	}
}
