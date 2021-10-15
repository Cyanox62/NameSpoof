using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
						File.WriteAllText($"{Path.Combine(NameSpoof.SavesFilePath, player.UserId)}.txt", name);
						response = $"Your name has been spoofed to '{name}'. You must reconnect for the name change to take effect. Type '.relog' to reconnect now.";
					}
					else
					{
						string path = $"{Path.Combine(NameSpoof.SavesFilePath, player.UserId)}.txt";
						if (File.Exists(path)) File.Delete(path);
						response = "Your name has been unspoofed. You must reconnect for the name change to take effect. Type '.relog' to reconnect now.";
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
