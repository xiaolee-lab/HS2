using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AIProject.CaptionScript
{
	// Token: 0x02000E32 RID: 3634
	public class TagCommand : IScriptCommand
	{
		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x060071CA RID: 29130 RVA: 0x003070E3 File Offset: 0x003054E3
		public string Tag
		{
			[CompilerGenerated]
			get
			{
				return TagCommand.DefaultTag;
			}
		}

		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x060071CB RID: 29131 RVA: 0x003070EA File Offset: 0x003054EA
		public bool IsBefore
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x060071CC RID: 29132 RVA: 0x003070ED File Offset: 0x003054ED
		public bool Execute(Dictionary<string, string> dic, int line)
		{
			return true;
		}

		// Token: 0x060071CD RID: 29133 RVA: 0x003070F0 File Offset: 0x003054F0
		public Dictionary<string, string> Analysis(List<string> list)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			int num = 0;
			dictionary["Tag"] = list[num++].Replace("'@", string.Empty).Replace("@", string.Empty);
			dictionary["name"] = list[num++];
			return dictionary;
		}

		// Token: 0x04005D35 RID: 23861
		public static string DefaultTag = "tag";
	}
}
