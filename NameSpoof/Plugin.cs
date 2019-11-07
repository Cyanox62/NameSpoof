using Smod2.Attributes;

namespace NameSpoof
{
	[PluginDetails(
	author = "Cyanox",
	name = "NameSpoof",
	description = "Spoofs a player's name.",
	id = "cyan.ns",
	version = "1.0.0",
	SmodMajor = 3,
	SmodMinor = 0,
	SmodRevision = 0
	)]
	public class Plugin : Smod2.Plugin
	{
		public override void OnDisable() { }

		public override void OnEnable() { }

		public override void Register()
		{
			AddEventHandlers(new EventHandler());
			AddCommands(new[] { "spoof" }, new CommandHandler());
		}
	}
}
