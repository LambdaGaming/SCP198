using EXILED;
using System.Collections.Generic;
using System.Linq;

namespace SCP198
{
	public class Plugin : EXILED.Plugin
	{
		public EventHandlers EventHandlers;
		public bool SCP198ShooterDeath;
		public bool SCP198MedicDeath;
		public bool SCP198UpgradeDeath;
		public bool SCP198KeycardDeath;
		public int SCP198PossessionChance;
		public int SCP198UpgradeDeathChance;
		public List<string> SCP198BlacklistedItems;

		public override void OnEnable()
		{
			if ( !Config.GetBool( "198_enabled", true ) ) return;
			SCP198ShooterDeath = Config.GetBool( "198_shooter_death", true );
			SCP198MedicDeath = Config.GetBool( "198_medic_death", true );
			SCP198UpgradeDeath = Config.GetBool( "198_upgrade_death", true );
			SCP198KeycardDeath = Config.GetBool( "198_keycard_death", true );
			SCP198PossessionChance = Config.GetInt( "198_possession_chance", 5 );
			SCP198UpgradeDeathChance = Config.GetInt( "198_upgrade_death_chance", 50 );
			SCP198BlacklistedItems = Config.GetString( "198_blacklist" ).Split( ',' ).ToList();

			EventHandlers = new EventHandlers( this );
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
		}

		public override void OnReload() { }

		public override string getName { get; } = "SCP198";
	}
}
