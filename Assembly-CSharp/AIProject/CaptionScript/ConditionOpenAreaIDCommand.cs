using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Manager;

namespace AIProject.CaptionScript
{
	// Token: 0x02000E3A RID: 3642
	public class ConditionOpenAreaIDCommand : IScriptCommand
	{
		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x060071FD RID: 29181 RVA: 0x00307985 File Offset: 0x00305D85
		public string Tag
		{
			[CompilerGenerated]
			get
			{
				return ConditionOpenAreaIDCommand.DefaultTag;
			}
		}

		// Token: 0x170015F9 RID: 5625
		// (get) Token: 0x060071FE RID: 29182 RVA: 0x0030798C File Offset: 0x00305D8C
		public bool IsBefore
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x060071FF RID: 29183 RVA: 0x00307990 File Offset: 0x00305D90
		public bool Execute(Dictionary<string, string> table, int line)
		{
			string key = Singleton<ADV>.Instance.TargetMerchant.OpenAreaID.ToString();
			string empty;
			if (!table.TryGetValue(key, out empty))
			{
				empty = string.Empty;
			}
			Singleton<ADV>.Instance.Captions.CommandSystem.GotoTag(empty);
			return true;
		}

		// Token: 0x06007200 RID: 29184 RVA: 0x003079E8 File Offset: 0x00305DE8
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

		// Token: 0x04005D3D RID: 23869
		public static string DefaultTag = "condition openareaid";
	}
}
