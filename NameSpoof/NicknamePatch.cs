using Exiled.API.Features;
using HarmonyLib;
using Mirror;
using System;
using UnityEngine;

namespace NameSpoof
{
	[HarmonyPatch(typeof(NicknameSync), "SetNick")]
	class NicknamePatch
	{
		public static void Prefix(NicknameSync __instance, ref string nick)
		{
			string userid = __instance.gameObject.GetComponent<CharacterClassManager>().UserId;
			if (EventHandler.spoofs.ContainsKey(userid)) nick = EventHandler.spoofs[userid];
		}
	}
}
