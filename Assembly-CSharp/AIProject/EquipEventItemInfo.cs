using System;

namespace AIProject
{
	// Token: 0x02000906 RID: 2310
	public class EquipEventItemInfo
	{
		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06003FE6 RID: 16358 RVA: 0x0019E57C File Offset: 0x0019C97C
		// (set) Token: 0x06003FE7 RID: 16359 RVA: 0x0019E584 File Offset: 0x0019C984
		public int EventItemID { get; set; }

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06003FE8 RID: 16360 RVA: 0x0019E58D File Offset: 0x0019C98D
		// (set) Token: 0x06003FE9 RID: 16361 RVA: 0x0019E595 File Offset: 0x0019C995
		public ActionItemInfo ActionItemInfo { get; set; }

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06003FEA RID: 16362 RVA: 0x0019E59E File Offset: 0x0019C99E
		// (set) Token: 0x06003FEB RID: 16363 RVA: 0x0019E5A6 File Offset: 0x0019C9A6
		public string ParentName { get; set; }
	}
}
