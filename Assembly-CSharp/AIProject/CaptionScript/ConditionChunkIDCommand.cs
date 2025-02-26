using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Manager;

namespace AIProject.CaptionScript
{
	// Token: 0x02000E39 RID: 3641
	public class ConditionChunkIDCommand : IScriptCommand
	{
		// Token: 0x170015F6 RID: 5622
		// (get) Token: 0x060071F7 RID: 29175 RVA: 0x0030788F File Offset: 0x00305C8F
		public string Tag
		{
			[CompilerGenerated]
			get
			{
				return ConditionChunkIDCommand.DefaultTag;
			}
		}

		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x060071F8 RID: 29176 RVA: 0x00307896 File Offset: 0x00305C96
		public bool IsBefore
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x060071F9 RID: 29177 RVA: 0x0030789C File Offset: 0x00305C9C
		public bool Execute(Dictionary<string, string> table, int line)
		{
			string key = "0";
			string empty;
			if (!table.TryGetValue(key, out empty))
			{
				empty = string.Empty;
			}
			Singleton<ADV>.Instance.Captions.CommandSystem.GotoTag(empty);
			return true;
		}

		// Token: 0x060071FA RID: 29178 RVA: 0x003078DC File Offset: 0x00305CDC
		public Dictionary<string, string> Analysis(List<string> list)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			int i = 0;
			dictionary["Tag"] = list[i++].Replace(string.Empty, CommandSystem.ReplaceStrings);
			while (i < list.Count)
			{
				Match match = Regex.Match(list[i], "(\\S+\\d*)=(\\S*\\d*)");
				if (match.Success)
				{
					dictionary[match.Groups[1].ToString()] = match.Groups[2].ToString();
				}
				i++;
			}
			return dictionary;
		}

		// Token: 0x04005D3C RID: 23868
		public static string DefaultTag = "condition chunkid";
	}
}
