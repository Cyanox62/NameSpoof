using Smod2.API;
using Smod2.Commands;

namespace NameSpoof
{
	class CommandHandler : ICommandHandler
	{
		public string GetCommandDescription()
		{
			return "Spoofs your name.";
		}

		public string GetUsage()
		{
			return "SPOOF (SPOOFED NAME)";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (sender is Player player)
			{
				if (args.Length > 0)
				{
					string name = string.Empty;
					for (int i = 0; i < args.Length; i++)
					{
						name += $"{args[i]}{(i != args.Length - 1 ? " " : "")}";
					}

					PlayerSpoof ps = new PlayerSpoof();
					ps.pNormalName = player.Name;
					ps.pSpoofedName = name;
					EventHandler.SpoofName(player, ps);

					return new[] { $"Your name has been spoofed to '{name}'." };
				}
				else
				{
					EventHandler.UnSpoofName(player);
					return new[] { "Your name has been unspoofed." };
				}
			}
			else
			{
				return new[] { "You must be a player to run this command." };
			}
		}
	}
}
