using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;
using UnityEngine;

namespace AIProject.CaptionScript
{
	// Token: 0x02000E38 RID: 3640
	public class RandomGotoTagCommand : IScriptCommand
	{
		// Token: 0x170015F4 RID: 5620
		// (get) Token: 0x060071F1 RID: 29169 RVA: 0x00307727 File Offset: 0x00305B27
		public string Tag
		{
			[CompilerGenerated]
			get
			{
				return RandomGotoTagCommand.DefaultTag;
			}
		}

		// Token: 0x170015F5 RID: 5621
		// (get) Token: 0x060071F2 RID: 29170 RVA: 0x0030772E File Offset: 0x00305B2E
		public bool IsBefore
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x060071F3 RID: 29171 RVA: 0x00307734 File Offset: 0x00305B34
		public bool Execute(Dictionary<string, string> table, int line)
		{
			List<string> list = ListPool<string>.Get();
			foreach (KeyValuePair<string, string> keyValuePair in table)
			{
				if (!(keyValuePair.Key == "Tag"))
				{
					if (!keyValuePair.Key.IsNullOrEmpty())
					{
						list.Add(keyValuePair.Key);
					}
				}
			}
			string tag = string.Empty;
			if (!list.IsNullOrEmpty<string>())
			{
				tag = list[UnityEngine.Random.Range(0, list.Count)];
			}
			ListPool<string>.Release(list);
			Singleton<ADV>.Instance.Captions.CommandSystem.GotoTag(tag);
			return true;
		}

		// Token: 0x060071F4 RID: 29172 RVA: 0x00307808 File Offset: 0x00305C08
		public Dictionary<string, string> Analysis(List<string> list)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			int i = 0;
			dictionary["Tag"] = list[i++].Replace(string.Empty, CommandSystem.ReplaceStrings);
			while (i < list.Count)
			{
				if (!list[i].IsNullOrEmpty())
				{
					dictionary[list[i]] = string.Empty;
				}
				i++;
			}
			return dictionary;
		}

		// Token: 0x04005D3B RID: 23867
		public static string DefaultTag = "random gototag";
	}
}
