using Smod2.API;
using UnityEngine;

namespace NameSpoof
{
	partial class EventHandler
	{
		public static void SpoofName(Player player, PlayerSpoof playerSpoof)
		{
			if (!spoofs.ContainsKey(player.SteamId))
			{
				spoofs.Add(player.SteamId, playerSpoof);
			}
			else
			{
				spoofs[player.SteamId].pSpoofedName = playerSpoof.pSpoofedName;
			}
			SetNickname(player, playerSpoof.pSpoofedName);
		}

		public static void UnSpoofName(Player player)
		{
			if (spoofs.ContainsKey(player.SteamId))
			{
				SetNickname(player, spoofs[player.SteamId].pNormalName);
				spoofs.Remove(player.SteamId);
			}
		}

		private static void SetNickname(Player player, string name)
		{
			GameObject obj = (GameObject)player.GetGameObject();
			obj.GetComponent<ServerRoles>().SetBadgeUpdate(name);
			obj.GetComponent<NicknameSync>().UpdateNickname(name);
		}
	}
}
