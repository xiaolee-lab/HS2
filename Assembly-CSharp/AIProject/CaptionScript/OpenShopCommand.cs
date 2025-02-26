using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;

namespace AIProject.CaptionScript
{
	// Token: 0x02000E36 RID: 3638
	public class OpenShopCommand : IScriptCommand
	{
		// Token: 0x170015F0 RID: 5616
		// (get) Token: 0x060071E5 RID: 29157 RVA: 0x0030764F File Offset: 0x00305A4F
		public string Tag
		{
			[CompilerGenerated]
			get
			{
				return OpenShopCommand.DefaultTag;
			}
		}

		// Token: 0x170015F1 RID: 5617
		// (get) Token: 0x060071E6 RID: 29158 RVA: 0x00307656 File Offset: 0x00305A56
		public bool IsBefore
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x060071E7 RID: 29159 RVA: 0x00307659 File Offset: 0x00305A59
		public bool Execute(Dictionary<string, string> table, int lien)
		{
			Singleton<ADV>.Instance.TargetMerchant.OpenShopUI();
			return false;
		}

		// Token: 0x060071E8 RID: 29160 RVA: 0x0030766C File Offset: 0x00305A6C
		public Dictionary<string, string> Analysis(List<string> list)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			int num = 0;
			dictionary["Tag"] = list[num++].Replace(string.Empty, CommandSystem.ReplaceStrings);
			return dictionary;
		}

		// Token: 0x04005D39 RID: 23865
		public static string DefaultTag = "open shop";
	}
}
