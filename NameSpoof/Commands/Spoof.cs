using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;
using System;

namespace NameSpoof.Commands
{
	[CommandHandler(typeof(ClientCommandHandler))]
	class Spoof : ICommand
	{
		public string[] Aliases { get; set; } = Array.Empty<string>();

		public string Description { get; set; } = "Spoofs your name";

		string ICommand.Command { get; } = "spoof";

		public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
		{
			if (sender is PlayerCommandSender playerSender)
			{
				Player player = Player.Get(playerSender);
				if (player.RemoteAdminAccess)
				{
					if (arguments.Count > 0)
					{
						string name = string.Empty;
						foreach (string s in arguments) name += $"{s} ";
						name = name.Trim();
						if (!NameSpoof.spoofs.ContainsKey(player.UserId))
						{
							NameSpoof.spoofs.Add(player.UserId, name);
						}
						else
						{
							NameSpoof.spoofs[player.UserId] = name;
						}
						response = $"Your name has been spoofed to '{name}'. You must reconnect for the name change to take effect. Type '.relog' to reconnect now.";
					}
					else
					{
						if (NameSpoof.spoofs.ContainsKey(player.UserId))
						{
							NameSpoof.spoofs.Remove(player.UserId);
							response = "Your name has been unspoofed. You must reconnect for the name change to take effect. Type '.relog' to reconnect now.";
						}
						else
						{
							response = "Your name is not currently spoofed.";
						}
					}
					return true;
				}
				else
				{
					response = string.Empty;
					return false;
				}
			}
			else
			{
				response = "Only players may use this command";
				return false;
			}
		}
	}
}
