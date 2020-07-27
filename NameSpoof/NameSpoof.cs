using Exiled.API.Features;
using HarmonyLib;
using System;
using System.IO;

namespace NameSpoof
{
	public class NameSpoof : Plugin<Config>
	{
		internal static string SavesFilePath = Path.Combine(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EXILED"), "Plugins"), "NameSpoof");

		private Harmony hInstance;
		public EventHandler ev;

		public override void OnEnabled() 
		{
			base.OnEnabled();

			if (!Config.IsEnabled) return;

			if (!Directory.Exists(SavesFilePath)) Directory.CreateDirectory(SavesFilePath);

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
