using EXILED;

namespace SCP198
{
	public class Plugin : EXILED.Plugin
	{
		public static Plugin GetPlugin { private set; get; }
		public EventHandlers EventHandlers;
		public bool SCP198Enabled = Config.GetBool( "198_enabled", true );
		public bool SCP198ShooterDeath = Config.GetBool( "198_shooter_death", true );
		public bool SCP198MedicDeath = Config.GetBool( "198_medic_death", true );
		public bool SCP198UpgradeDeath = Config.GetBool( "198_upgrade_death", true );
		public bool SCP198KeycardDeath = Config.GetBool( "198_keycard_death", true );
		public int SCP198PossessionChance = Config.GetInt( "198_possession_chance", 5 );

		public override void OnEnable()
		{
			if ( !SCP198Enabled ) return;
			EventHandlers = new EventHandlers( this );
			GetPlugin = this;
			Events.PickupItemEvent += EventHandlers.OnItemPickup;
			Events.RoundEndEvent += EventHandlers.OnRoundEnd;
			Events.DropItemEvent += EventHandlers.OnItemDrop;
			Events.ShootEvent += EventHandlers.OnShoot;
			Events.UsedMedicalItemEvent += EventHandlers.OnMedicalItemUsed;
			Events.Scp914UpgradeEvent += EventHandlers.OnItemUpgrade;
			Events.DoorInteractEvent += EventHandlers.OnDoorInteract;
			Log.Info( "Successfully loaded." );
		}

		public override void OnDisable()
		{
			Events.PickupItemEvent -= EventHandlers.OnItemPickup;
			Events.RoundEndEvent -= EventHandlers.OnRoundEnd;
			Events.DropItemEvent -= EventHandlers.OnItemDrop;
			Events.ShootEvent -= EventHandlers.OnShoot;
			Events.UsedMedicalItemEvent -= EventHandlers.OnMedicalItemUsed;
			Events.Scp914UpgradeEvent -= EventHandlers.OnItemUpgrade;
			Events.DoorInteractEvent -= EventHandlers.OnDoorInteract;
			EventHandlers = null;
			GetPlugin = null;
		}

		public override void OnReload() { }

		public override string getName { get; } = "SCP198";
	}
}
