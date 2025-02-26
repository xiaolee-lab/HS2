using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;

namespace AIProject.CaptionScript
{
	// Token: 0x02000E37 RID: 3639
	public class FuckCommand2 : IScriptCommand
	{
		// Token: 0x170015F2 RID: 5618
		// (get) Token: 0x060071EB RID: 29163 RVA: 0x003076BB File Offset: 0x00305ABB
		public string Tag
		{
			[CompilerGenerated]
			get
			{
				return FuckCommand2.DefaultTag;
			}
		}

		// Token: 0x170015F3 RID: 5619
		// (get) Token: 0x060071EC RID: 29164 RVA: 0x003076C2 File Offset: 0x00305AC2
		public bool IsBefore
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x060071ED RID: 29165 RVA: 0x003076C5 File Offset: 0x00305AC5
		public bool Execute(Dictionary<string, string> table, int line)
		{
			Singleton<ADV>.Instance.TargetMerchant.InitiateHScene();
			return false;
		}

		// Token: 0x060071EE RID: 29166 RVA: 0x003076D8 File Offset: 0x00305AD8
		public Dictionary<string, string> Analysis(List<string> list)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			int num = 0;
			dictionary["Tag"] = list[num++].Replace(string.Empty, CommandSystem.ReplaceStrings);
			return dictionary;
		}

		// Token: 0x04005D3A RID: 23866
		public static string DefaultTag = "fuck";
	}
}
