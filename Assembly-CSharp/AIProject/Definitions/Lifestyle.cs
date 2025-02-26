using System;
using System.Collections.Generic;

namespace AIProject.Definitions
{
	// Token: 0x0200093C RID: 2364
	public static class Lifestyle
	{
		// Token: 0x06004276 RID: 17014 RVA: 0x001A265C File Offset: 0x001A0A5C
		// Note: this type is marked as 'beforefieldinit'.
		static Lifestyle()
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			dictionary[0] = "ゲッター";
			dictionary[1] = "ベイビー";
			dictionary[2] = "ドライバー";
			dictionary[3] = "コントローラー";
			dictionary[4] = "エキサイトメント・シーカー";
			dictionary[5] = "アームチェア";
			Lifestyle.LifestyleName = dictionary;
		}

		// Token: 0x04003EBB RID: 16059
		public static readonly Dictionary<int, string> LifestyleName;
	}
}
