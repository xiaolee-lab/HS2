using System;
using System.Collections.Generic;

namespace AIProject.Definitions
{
	// Token: 0x0200092B RID: 2347
	public static class Status
	{
		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06004266 RID: 16998 RVA: 0x001A19F9 File Offset: 0x0019FDF9
		public static Dictionary<int, string> Names { get; }

		// Token: 0x06004267 RID: 16999 RVA: 0x001A1A00 File Offset: 0x0019FE00
		// Note: this type is marked as 'beforefieldinit'.
		static Status()
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			dictionary[0] = "体温";
			dictionary[1] = "機嫌";
			dictionary[2] = "満腹";
			dictionary[3] = "体調";
			dictionary[4] = "生命維持[存在しない]";
			dictionary[5] = "やる気";
			dictionary[6] = "Hな気分";
			dictionary[7] = "善悪";
			Status.Names = dictionary;
		}

		// Token: 0x0200092C RID: 2348
		public enum Type
		{
			// Token: 0x04003D85 RID: 15749
			Temperature,
			// Token: 0x04003D86 RID: 15750
			Mood,
			// Token: 0x04003D87 RID: 15751
			Hunger,
			// Token: 0x04003D88 RID: 15752
			Physical,
			// Token: 0x04003D89 RID: 15753
			Life,
			// Token: 0x04003D8A RID: 15754
			Motivation,
			// Token: 0x04003D8B RID: 15755
			Immoral,
			// Token: 0x04003D8C RID: 15756
			Morality
		}
	}
}
