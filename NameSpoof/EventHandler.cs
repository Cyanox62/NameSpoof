using Smod2.Events;
using Smod2.EventHandlers;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpoof
{
	class PlayerSpoof
	{
		public string pNormalName;
		public string pSpoofedName;
	}

	partial class EventHandler : IEventHandlerPlayerJoin, IEventHandlerCallCommand
	{
		public static Dictionary<string, PlayerSpoof> spoofs = new Dictionary<string, PlayerSpoof>();

		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			if (spoofs.ContainsKey(ev.Player.SteamId))
			{
				SetNickname(ev.Player, spoofs[ev.Player.SteamId].pSpoofedName);
			}
		}

		public void OnCallCommand(PlayerCallCommandEvent ev)
		{
			if (((GameObject)ev.Player.GetGameObject()).GetComponent<ServerRoles>().RemoteAdmin && ev.Command.StartsWith("spoof"))
			{
				string name = ev.Command.Replace("spoof", "").Trim();
				if (name.Length > 0)
				{
					PlayerSpoof ps = new PlayerSpoof();
					ps.pNormalName = ev.Player.Name;
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
