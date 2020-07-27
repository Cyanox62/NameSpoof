using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Mirror;

namespace NameSpoof
{
	public class EventHandler
	{
		internal static Dictionary<string, string> spoofs = new Dictionary<string, string>();

		public void TargetRpcRedirect(Player player, ushort port)
		{
			NetworkBehaviour behaviour = player.ReferenceHub.playerStats;
			NetworkWriter writer = NetworkWriterPool.GetWriter();
			writer.WriteSingle(1f);
			writer.WriteUInt16(port);
			behaviour.SendTargetRPCInternal(behaviour.connectionToClient, typeof(PlayerStats), "RpcRoundrestartRedirect", writer, 0);
			NetworkWriterPool.Recycle(writer);
		}

		public void OnConsoleCommand(SendingConsoleCommandEventArgs ev)
		{
			string cmd = ev.Name.ToLower();
			if (cmd == "spoof" && ev.Player.ReferenceHub.serverRoles.RemoteAdmin)
			{
				if (ev.Arguments.Count > 0)
				{
					string name = string.Empty;
					foreach (string s in ev.Arguments) name += $"{s} ";
					name = name.Trim();
					if (!spoofs.ContainsKey(ev.Player.UserId)) spoofs.Add(ev.Player.UserId, name);
					else spoofs[ev.Player.UserId] = name;
					ev.ReturnMessage = $"Your name has been spoofed to '{name}'. You must reconnect for the name change to take effect. Type '.relog' to reconnect now.";
				}
				else
				{
					spoofs.Remove(ev.Player.UserId);
					ev.ReturnMessage = "Your name has been unspoofed. You must reconnect for the name change to take effect. Type '.relog' to reconnect now.";
				}
			}
			else if (cmd == "relog")
			{
				TargetRpcRedirect(ev.Player, (ushort)ServerConsole.Port);
			}
		}
	}
}
