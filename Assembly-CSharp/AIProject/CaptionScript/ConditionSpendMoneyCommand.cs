using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Manager;

namespace AIProject.CaptionScript
{
	// Token: 0x02000E3B RID: 3643
	public class ConditionSpendMoneyCommand : IScriptCommand
	{
		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x06007203 RID: 29187 RVA: 0x00307A91 File Offset: 0x00305E91
		public string Tag
		{
			[CompilerGenerated]
			get
			{
				return ConditionSpendMoneyCommand.DefaultTag;
			}
		}

		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x06007204 RID: 29188 RVA: 0x00307A98 File Offset: 0x00305E98
		public bool IsBefore
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x06007205 RID: 29189 RVA: 0x00307A9C File Offset: 0x00305E9C
		public bool Execute(Dictionary<string, string> table, int line)
		{
			List<Tuple<int, string>> list = ListPool<Tuple<int, string>>.Get();
			foreach (KeyValuePair<string, string> keyValuePair in table)
			{
				if (!(keyValuePair.Key == "Tag"))
				{
					int item;
					if (int.TryParse(keyValuePair.Key, out item))
					{
						list.Add(new Tuple<int, string>(item, keyValuePair.Value));
					}
				}
			}
			list.Sort((Tuple<int, string> a, Tuple<int, string> b) => b.Item1 - a.Item1);
			string tag = string.Empty;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Item1 <= Singleton<Map>.Instance.Player.PlayerData.SpendMoney)
				{
					tag = list[i].Item2;
					break;
				}
			}
			ListPool<Tuple<int, string>>.Release(list);
			Singleton<ADV>.Instance.Captions.CommandSystem.GotoTag(tag);
			return true;
		}

		// Token: 0x06007206 RID: 29190 RVA: 0x00307BD0 File Offset: 0x00305FD0
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

		// Token: 0x04005D3E RID: 23870
		public static string DefaultTag = "condition spendmoney";
	}
}
