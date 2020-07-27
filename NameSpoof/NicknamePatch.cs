using HarmonyLib;
using System.IO;

namespace NameSpoof
{
	[HarmonyPatch(typeof(NicknameSync), "SetNick")]
	class NicknamePatch
	{
		public static void Prefix(NicknameSync __instance, ref string nick)
		{
			string userid = __instance.gameObject.GetComponent<CharacterClassManager>().UserId;
			string path = $"{Path.Combine(NameSpoof.SavesFilePath, userid)}.txt";
			if (File.Exists(path)) nick = File.ReadAllText(path);
		}
	}
}
