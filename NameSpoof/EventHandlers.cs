using System.Collections.Generic;
using EXILED;

namespace NameSpoof
{
	public partial class EventHandlers
	{
		private Dictionary<string, PlayerSpoof> spoofs = new Dictionary<string, PlayerSpoof>();

		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			if (spoofs.ContainsKey(ev.Player.characterClassManager.UserId))
			{
				SetNickname(ev.Player, spoofs[ev.Player.characterClassManager.UserId].pSpoofedName);
			}
		}

		public void OnConsoleCommand(ConsoleCommandEvent ev)
		{
			string cmd = ev.Command.ToLower();
			if (!ev.Player.serverRoles.RemoteAdmin && cmd.StartsWith("spoof"))
			{
				string name = cmd.Replace("spoof", "").Trim();
				if (name.Length > 0)
				{
					PlayerSpoof ps = new PlayerSpoof();
					ps.pNormalName = ev.Player.nicknameSync.name;
					ps.pSpoofedName = name;
					SpoofName(ev.Player, ps);

					ev.ReturnMessage = $"Your name has been spoofed to '{name}'.";
				}
				else
				{
					UnSpoofName(ev.Player);
					ev.ReturnMessage = "Your name has been unspoofed.";
				}
			}
		}
	}
}
