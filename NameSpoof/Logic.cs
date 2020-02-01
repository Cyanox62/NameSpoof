namespace NameSpoof
{
	partial class EventHandlers
	{
		class PlayerSpoof
		{
			public string pNormalName;
			public string pSpoofedName;
		}

		private void SpoofName(ReferenceHub player, PlayerSpoof playerSpoof)
		{
			if (!spoofs.ContainsKey(player.characterClassManager.UserId))
			{
				spoofs.Add(player.characterClassManager.UserId, playerSpoof);
			}
			else
			{
				spoofs[player.characterClassManager.UserId].pSpoofedName = playerSpoof.pSpoofedName;
			}
			SetNickname(player, playerSpoof.pSpoofedName);
		}

		private void UnSpoofName(ReferenceHub player)
		{
			if (spoofs.ContainsKey(player.characterClassManager.UserId))
			{
				SetNickname(player, spoofs[player.characterClassManager.UserId].pNormalName);
				spoofs.Remove(player.characterClassManager.UserId);
			}
		}

		private void SetNickname(ReferenceHub player, string name)
		{
			Plugin.Info("spoofing to " + name);
			//player.serverRoles.NetworkGlobalBadge = name;
			player.nicknameSync.MyNick = name;
			player.nicknameSync.Network_myNickSync = name;
		}
	}
}
