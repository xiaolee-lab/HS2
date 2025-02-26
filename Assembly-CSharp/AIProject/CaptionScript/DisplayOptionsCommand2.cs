using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;

namespace AIProject.CaptionScript
{
	// Token: 0x02000E35 RID: 3637
	public class DisplayOptionsCommand2 : IScriptCommand
	{
		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x060071DF RID: 29151 RVA: 0x003075E3 File Offset: 0x003059E3
		public string Tag
		{
			[CompilerGenerated]
			get
			{
				return DisplayOptionsCommand2.DefaultTag;
			}
		}

		// Token: 0x170015EF RID: 5615
		// (get) Token: 0x060071E0 RID: 29152 RVA: 0x003075EA File Offset: 0x003059EA
		public bool IsBefore
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x060071E1 RID: 29153 RVA: 0x003075ED File Offset: 0x003059ED
		public bool Execute(Dictionary<string, string> table, int line)
		{
			Singleton<ADV>.Instance.TargetMerchant.PopupCommands();
			return false;
		}

		// Token: 0x060071E2 RID: 29154 RVA: 0x00307600 File Offset: 0x00305A00
		public Dictionary<string, string> Analysis(List<string> list)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			int num = 0;
			dictionary["Tag"] = list[num++].Replace(string.Empty, CommandSystem.ReplaceStrings);
			return dictionary;
		}

		// Token: 0x04005D38 RID: 23864
		public static string DefaultTag = "display option";
	}
}
