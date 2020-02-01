using EXILED;

namespace NameSpoof
{
	public class Plugin : EXILED.Plugin
	{
		public EventHandlers EventHandlers;

		public override void OnEnable() 
		{
			EventHandlers = new EventHandlers();
			Events.ConsoleCommandEvent += EventHandlers.OnConsoleCommand;
		}

		public override void OnDisable() { }

		public override void OnReload() { }

		public override string getName { get; } = "NameSpoof";
	}
}
