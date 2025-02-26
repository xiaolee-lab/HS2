using System;
using System.Collections.Generic;

namespace AIProject.Definitions
{
	// Token: 0x02000937 RID: 2359
	public static class FlavorSkill
	{
		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06004274 RID: 17012 RVA: 0x001A25D9 File Offset: 0x001A09D9
		public static Dictionary<int, string> NameTable { get; }

		// Token: 0x06004275 RID: 17013 RVA: 0x001A25E0 File Offset: 0x001A09E0
		// Note: this type is marked as 'beforefieldinit'.
		static FlavorSkill()
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			dictionary[0] = "女子力";
			dictionary[1] = "信頼";
			dictionary[2] = "人間性";
			dictionary[3] = "本能";
			dictionary[4] = "変態";
			dictionary[5] = "危機管理";
			dictionary[6] = "闇";
			dictionary[7] = "社交性";
			FlavorSkill.NameTable = dictionary;
		}

		// Token: 0x02000938 RID: 2360
		public enum Type
		{
			// Token: 0x04003E67 RID: 15975
			Pheromone,
			// Token: 0x04003E68 RID: 15976
			Reliability,
			// Token: 0x04003E69 RID: 15977
			Reason,
			// Token: 0x04003E6A RID: 15978
			Instinct,
			// Token: 0x04003E6B RID: 15979
			Dirty,
			// Token: 0x04003E6C RID: 15980
			Wariness,
			// Token: 0x04003E6D RID: 15981
			Darkness,
			// Token: 0x04003E6E RID: 15982
			Sociability
		}
	}
}
