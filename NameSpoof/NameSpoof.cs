using Exiled.API.Features;
using HarmonyLib;

namespace NameSpoof
{
	public class NameSpoof : Plugin<Config>
	{
		private Harmony hInstance;
		public EventHandler ev;

		public override void OnEnabled() 
		{
			base.OnEnabled();

			if (!Config.IsEnabled) return;

			hInstance = new Harmony("cyanox.namespoof");
			hInstance.PatchAll();
			ev = new EventHandler();
			Exiled.Events.Handlers.Server.SendingConsoleCommand += ev.OnConsoleCommand;
		}

		public override void OnDisabled()
		{
			base.OnDisabled();

			Exiled.Events.Handlers.Server.SendingConsoleCommand -= ev.OnConsoleCommand;
			hInstance.UnpatchAll();
			ev = null;
		}

		public override string Name => "NameSpoof";
	}
}
