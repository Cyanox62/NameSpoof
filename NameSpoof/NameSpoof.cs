using Exiled.API.Features;
using HarmonyLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace NameSpoof
{
	public class NameSpoof : Plugin<Config>
	{
		internal static string SavesDirectory = Path.Combine(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EXILED"), "Plugins"), "NameSpoof");
		internal static string SavesFilePath = Path.Combine(SavesDirectory, "Spoofs.json");

		private Harmony hInstance;

		internal static Dictionary<string, string> spoofs;

		private EventHandlers ev;

		public override void OnEnabled() 
		{
			base.OnEnabled();

			ev = new EventHandlers();
			Exiled.Events.Handlers.Server.RestartingRound += ev.OnRoundRestart;

			if (!Config.IsEnabled) return;

			if (!Directory.Exists(SavesDirectory)) Directory.CreateDirectory(SavesDirectory);
			if (!File.Exists(SavesFilePath)) File.WriteAllText(SavesFilePath, "{}");
			spoofs = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(SavesFilePath));

			hInstance = new Harmony("cyanox.namespoof");
			hInstance.PatchAll();
		}

		public override void OnDisabled()
		{
			base.OnDisabled();

			Exiled.Events.Handlers.Server.RestartingRound += ev.OnRoundRestart;
			ev = null;

			hInstance.UnpatchAll(hInstance.Id);
		}

		public override string Name => "NameSpoof";
	}

	internal class EventHandlers
	{
		internal void OnRoundRestart() => File.WriteAllText(NameSpoof.SavesFilePath, JsonConvert.SerializeObject(NameSpoof.spoofs, Formatting.Indented));
	}
}
