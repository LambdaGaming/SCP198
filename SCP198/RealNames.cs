namespace SCP198;

internal static class RealNames
{
	internal static string Get( ItemType type )
	{
		foreach ( KeyValuePair<ItemType, string> kv in List )
		{
			if ( kv.Key == type )
				return kv.Value;
		}
		return "Error";
	}

	private static readonly Dictionary<ItemType, string> List = new() {
		{ ItemType.KeycardJanitor, "Janitor Keycard" },
		{ ItemType.KeycardScientist, "Scientist Keycard" },
		{ ItemType.KeycardResearchCoordinator, "Research Coordinator Keycard" },
		{ ItemType.KeycardZoneManager, "Zone Manager Keycard" },
		{ ItemType.KeycardGuard, "Security Guard Keycard" },
		{ ItemType.KeycardMTFPrivate, "MTF Private Keycard" },
		{ ItemType.KeycardContainmentEngineer, "Containment Engineer Keycard" },
		{ ItemType.KeycardMTFOperative, "MTF Operative Keycard" },
		{ ItemType.KeycardMTFCaptain, "MTF Captain Keycard" },
		{ ItemType.KeycardFacilityManager, "Facility Manager Keycard" },
		{ ItemType.KeycardChaosInsurgency, "Chaos Insurgency Keycard" },
		{ ItemType.KeycardO5, "O5 Keycard" },
		{ ItemType.Radio, "Radio" },
		{ ItemType.GunCOM15, "COM-15" },
		{ ItemType.Medkit, "Medkit" },
		{ ItemType.Flashlight, "Flashlight" },
		{ ItemType.MicroHID, "Micro HID" },
		{ ItemType.SCP500, "SCP-500" },
		{ ItemType.SCP207, "SCP-207" },
		{ ItemType.GunE11SR, "MTF-E11-SR" },
		{ ItemType.GunCrossvec, "Crossvec" },
		{ ItemType.GunFSP9, "FSP-9" },
		{ ItemType.GunLogicer, "Logicer" },
		{ ItemType.GunCOM18, "COM-18" },
		{ ItemType.SCP268, "SCP-268" },
		{ ItemType.Adrenaline, "Adrenaline" },
		{ ItemType.Painkillers, "Painkillers" },
		{ ItemType.Coin, "Coin" },
		{ ItemType.GunRevolver, ".44 Revolver" },
		{ ItemType.GunAK, "AK" },
		{ ItemType.GunShotgun, "Shotgun" },
		{ ItemType.SCP330, "SCP-330" },
		{ ItemType.SCP2176, "SCP-2176" },
		{ ItemType.SCP244a, "SCP-244-A" },
		{ ItemType.SCP244b, "SCP-244-B" },
		{ ItemType.SCP1853, "SCP-1853" },
		{ ItemType.ParticleDisruptor, "Particle Disruptor" },
		{ ItemType.GunCom45, "COM-45" },
		{ ItemType.SCP1576, "SCP-1576" },
		{ ItemType.Jailbird, "Jailbird" },
		{ ItemType.AntiSCP207, "Anti-Cola" },
		{ ItemType.GunFRMG0, "FR-MG-0" },
		{ ItemType.GunA7, "A7" },
		{ ItemType.Lantern, "Lantern" },
		{ ItemType.SCP1344, "SCP-1344" },
		{ ItemType.SCP1507Tape, "Tape Player" },
		{ ItemType.SurfaceAccessPass, "Surface Access Pass" },
		{ ItemType.GunSCP127, "SCP-127" },
		{ ItemType.SCP1509, "SCP-1509" },
		{ ItemType.Scp021J, "Evil Water Botllef" },
	};
}
