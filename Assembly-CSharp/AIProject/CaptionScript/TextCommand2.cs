using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Manager;

namespace AIProject.CaptionScript
{
	// Token: 0x02000E34 RID: 3636
	public class TextCommand2 : IScriptCommand
	{
		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x060071D8 RID: 29144 RVA: 0x003073A3 File Offset: 0x003057A3
		public string Tag
		{
			[CompilerGenerated]
			get
			{
				return TextCommand2.DefaultTag;
			}
		}

		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x060071D9 RID: 29145 RVA: 0x003073AA File Offset: 0x003057AA
		public bool IsBefore
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x060071DA RID: 29146 RVA: 0x003073B0 File Offset: 0x003057B0
		public bool Execute(Dictionary<string, string> table, int line)
		{
			string name;
			if (!table.TryGetValue("designation", out name))
			{
				string s;
				table.TryGetValue("name", out s);
				int num;
				if (int.TryParse(s, out num))
				{
					if (num != 0)
					{
						if (num != 1)
						{
							if (num == 2)
							{
								Singleton<ADV>.Instance.Captions.CaptionSystem.SetName(string.Empty);
							}
						}
						else
						{
							Singleton<ADV>.Instance.Captions.CaptionSystem.SetName(Singleton<ADV>.Instance.TargetMerchant.ChaControl.gameObject.name);
						}
					}
				}
				else
				{
					Singleton<ADV>.Instance.Captions.CaptionSystem.SetName(string.Empty);
				}
			}
			else
			{
				Singleton<ADV>.Instance.Captions.CaptionSystem.SetName(name);
			}
			bool noWait = false;
			string text;
			if (table.TryGetValue("wait", out text))
			{
				bool.TryParse(text, out noWait);
			}
			table.TryGetValue("content", out text);
			Singleton<ADV>.Instance.Captions.CaptionSystem.Action = new Action(this.OnComplete);
			Singleton<ADV>.Instance.Captions.CaptionSystem.SetText(text, noWait);
			return false;
		}

		// Token: 0x060071DB RID: 29147 RVA: 0x003074F4 File Offset: 0x003058F4
		private void OnComplete()
		{
			Singleton<ADV>.Instance.Captions.CommandSystem.CompletedCommand = true;
		}

		// Token: 0x060071DC RID: 29148 RVA: 0x0030750C File Offset: 0x0030590C
		public Dictionary<string, string> Analysis(List<string> list)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			int i = 0;
			dictionary["Tag"] = list[i++].Replace(string.Empty, CommandSystem.ReplaceStrings);
			dictionary["name"] = list[i++];
			dictionary["content"] = list[i++];
			for (i = 3; i < list.Count; i++)
			{
				Match match = Regex.Match(list[i], "(\\S+\\d*)=(\\S*\\d*)");
				if (match.Success)
				{
					dictionary[match.Groups[1].Value] = match.Groups[2].Value;
				}
			}
			return dictionary;
		}

		// Token: 0x04005D37 RID: 23863
		public static string DefaultTag = "text";
	}
}
