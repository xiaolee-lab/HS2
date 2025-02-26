using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AIProject.Definitions
{
	// Token: 0x0200093D RID: 2365
	public static class Sickness
	{
		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06004277 RID: 17015 RVA: 0x001A26BD File Offset: 0x001A0ABD
		public static ReadOnlyDictionary<int, string> NameTable { get; }

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06004278 RID: 17016 RVA: 0x001A26C4 File Offset: 0x001A0AC4
		public static Dictionary<string, int> TagTable { get; }

		// Token: 0x06004279 RID: 17017 RVA: 0x001A26CC File Offset: 0x001A0ACC
		// Note: this type is marked as 'beforefieldinit'.
		static Sickness()
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			dictionary[0] = "風邪";
			dictionary[1] = "腹痛";
			dictionary[2] = "過労";
			dictionary[3] = "熱中";
			dictionary[4] = "ケガ";
			Sickness.NameTable = new ReadOnlyDictionary<int, string>(dictionary);
			Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
			dictionary2["cold"] = 0;
			dictionary2["stomach"] = 1;
			dictionary2["overwork"] = 2;
			dictionary2["heatstroke"] = 3;
			dictionary2["drunkenness"] = 4;
			Sickness.TagTable = dictionary2;
		}

		// Token: 0x04003EBC RID: 16060
		public const int GoodHealthID = -1;

		// Token: 0x04003EBD RID: 16061
		public const int ColdID = 0;

		// Token: 0x04003EBE RID: 16062
		public const int StomachacheID = 1;

		// Token: 0x04003EBF RID: 16063
		public const int OverworkID = 2;

		// Token: 0x04003EC0 RID: 16064
		public const int HeatStrokeID = 3;

		// Token: 0x04003EC1 RID: 16065
		public const int HurtID = 4;
	}
}
