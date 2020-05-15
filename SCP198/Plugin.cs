using EXILED;

namespace SCP198
{
	public class Plugin : EXILED.Plugin
	{
		public EventHandlers EventHandlers;

		public override void OnEnable()
		{
			EventHandlers = new EventHandlers( this );
			Events.PickupItemEvent += EventHandlers.OnItemPickup;
			Events.RoundEndEvent += EventHandlers.OnRoundEnd;
			Events.DropItemEvent += EventHandlers.OnItemDrop;
			Events.ShootEvent += EventHandlers.OnShoot;
			Events.UsedMedicalItemEvent += EventHandlers.OnMedicalItemUsed;
			Log.Info( "Successfully loaded." );
		}

		public override void OnDisable()
		{
			Events.PickupItemEvent -= EventHandlers.OnItemPickup;
			Events.RoundEndEvent -= EventHandlers.OnRoundEnd;
			Events.DropItemEvent -= EventHandlers.OnItemDrop;
			Events.ShootEvent -= EventHandlers.OnShoot;
			Events.UsedMedicalItemEvent -= EventHandlers.OnMedicalItemUsed;
			EventHandlers = null;
		}

		public override void OnReload() { }

		public override string getName { get; } = "SCP198";
	}
}