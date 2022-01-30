using HarmonyLib;

namespace NameSpoof
{
	[HarmonyPatch(typeof(NicknameSync), "SetNick")]
	class NicknamePatch
	{
		public static void Prefix(NicknameSync __instance, ref string nick)
		{
			if (__instance?._hub?.characterClassManager?.UserId != null &&
				NameSpoof.spoofs.ContainsKey(__instance._hub.characterClassManager.UserId))
			{
				nick = NameSpoof.spoofs[__instance._hub.characterClassManager.UserId];
			}
		}
	}
}
