using CommandSystem;
using Exiled.API.Features;
using Mirror;
using RemoteAdmin;
using System;

namespace NameSpoof.Commands
{
	[CommandHandler(typeof(ClientCommandHandler))]
	class Relog : ICommand
	{
		public string[] Aliases { get; set; } = Array.Empty<string>();

		public string Description { get; set; } = "Relogs your client";

		string ICommand.Command { get; } = "relog";

		private void TargetRpcRedirect(Player player, ushort port)
		{
			NetworkBehaviour behaviour = player.ReferenceHub.playerStats;
			PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
			writer.WriteSingle(1f);
			writer.WriteUInt16(port);
			behaviour.SendTargetRPCInternal(behaviour.connectionToClient, typeof(PlayerStats), "RpcRoundrestartRedirect", writer, 0);
			NetworkWriterPool.Recycle(writer);
		}

		public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
		{
			if (sender is PlayerCommandSender playerSender)
			{
				TargetRpcRedirect(Player.Get(playerSender), (ushort)ServerConsole.Port);
				response = "Relogging...";
				return true;
			}
			else
			{
				response = "Only players may use this command";
				return false;
			}
		}
	}
}
