using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;

namespace AIProject.CaptionScript
{
	// Token: 0x02000E3C RID: 3644
	public class EndADVCommand2 : IScriptCommand
	{
		// Token: 0x170015FC RID: 5628
		// (get) Token: 0x0600720A RID: 29194 RVA: 0x00307C88 File Offset: 0x00306088
		public string Tag
		{
			[CompilerGenerated]
			get
			{
				return EndADVCommand2.DefaultTag;
			}
		}

		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x0600720B RID: 29195 RVA: 0x00307C8F File Offset: 0x0030608F
		public bool IsBefore
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x0600720C RID: 29196 RVA: 0x00307C92 File Offset: 0x00306092
		public bool Execute(Dictionary<string, string> table, int line)
		{
			Singleton<ADV>.Instance.TargetMerchant.VanishCommands();
			return false;
		}

		// Token: 0x0600720D RID: 29197 RVA: 0x00307CA4 File Offset: 0x003060A4
		public Dictionary<string, string> Analysis(List<string> list)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			int num = 0;
			dictionary["Tag"] = list[num++].Replace(string.Empty, CommandSystem.ReplaceStrings);
			return dictionary;
		}

		// Token: 0x04005D40 RID: 23872
		public static string DefaultTag = "end adv";
	}
}
