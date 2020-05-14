namespace SCP198
{
	public static class Extensions
	{
		public static void RAMessage( this CommandSender sender, string message, bool success = true ) =>
			sender.RaReply( "SCP198#" + message, success, true, string.Empty );

		public static void Broadcast( this ReferenceHub rh, uint time, string message ) => rh.GetComponent<Broadcast>().TargetAddElement( rh.scp079PlayerScript.connectionToClient, message, time, false );
	}
}