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

		public override void OnEnabled() 
		{
			base.OnEnabled();

			if (!Config.IsEnabled) return;

			if (!Directory.Exists(SavesFilePath)) Directory.CreateDirectory(SavesFilePath);

			hInstance = new Harmony("cyanox.namespoof");
			hInstance.PatchAll();
		}

		public override void OnDisabled()
		{
			base.OnDisabled();

			hInstance.UnpatchAll();
		}

		public override string Name => "NameSpoof";
	}
}
