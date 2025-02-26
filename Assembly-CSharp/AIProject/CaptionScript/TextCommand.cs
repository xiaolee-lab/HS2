using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Manager;

namespace AIProject.CaptionScript
{
	// Token: 0x02000E33 RID: 3635
	public class TextCommand : IScriptCommand
	{
		// Token: 0x170015EA RID: 5610
		// (get) Token: 0x060071D0 RID: 29136 RVA: 0x00307164 File Offset: 0x00305564
		public string Tag
		{
			[CompilerGenerated]
			get
			{
				return TextCommand.DefaultTag;
			}
		}

		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x060071D1 RID: 29137 RVA: 0x0030716B File Offset: 0x0030556B
		public bool IsBefore
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x060071D2 RID: 29138 RVA: 0x00307170 File Offset: 0x00305570
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
							Singleton<ADV>.Instance.Captions.CaptionSystem.SetName(string.Empty);
						}
						else
						{
							Singleton<ADV>.Instance.Captions.CaptionSystem.SetName(Singleton<ADV>.Instance.TargetCharacter.ChaControl.gameObject.name);
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
			Singleton<ADV>.Instance.Captions.CaptionSystem.Action = delegate()
			{
				this.OnComplete();
			};
			Singleton<ADV>.Instance.Captions.CaptionSystem.SetText(text, noWait);
			return false;
		}

		// Token: 0x060071D3 RID: 29139 RVA: 0x003072AD File Offset: 0x003056AD
		private void OnComplete()
		{
			Singleton<ADV>.Instance.Captions.CommandSystem.CompletedCommand = true;
		}

		// Token: 0x060071D4 RID: 29140 RVA: 0x003072C4 File Offset: 0x003056C4
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

		// Token: 0x04005D36 RID: 23862
		public static string DefaultTag = "text";
	}
}
