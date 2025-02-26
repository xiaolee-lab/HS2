using System;
using MessagePack;

namespace AIProject.SaveData
{
	// Token: 0x02000970 RID: 2416
	[MessagePackObject(false)]
	public class SickLockInfo
	{
		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x060043DB RID: 17371 RVA: 0x001A81D2 File Offset: 0x001A65D2
		// (set) Token: 0x060043DC RID: 17372 RVA: 0x001A81DA File Offset: 0x001A65DA
		[Key(0)]
		public bool Lock { get; set; }

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x060043DD RID: 17373 RVA: 0x001A81E3 File Offset: 0x001A65E3
		// (set) Token: 0x060043DE RID: 17374 RVA: 0x001A81EB File Offset: 0x001A65EB
		[Key(1)]
		public float ElapsedTime { get; set; }
	}
}
