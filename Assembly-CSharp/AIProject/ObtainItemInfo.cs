using System;

namespace AIProject
{
	// Token: 0x0200090C RID: 2316
	public class ObtainItemInfo
	{
		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06003FFF RID: 16383 RVA: 0x0019E64F File Offset: 0x0019CA4F
		// (set) Token: 0x06004000 RID: 16384 RVA: 0x0019E657 File Offset: 0x0019CA57
		public string Name { get; set; }

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06004001 RID: 16385 RVA: 0x0019E660 File Offset: 0x0019CA60
		// (set) Token: 0x06004002 RID: 16386 RVA: 0x0019E668 File Offset: 0x0019CA68
		public int Rate { get; set; }

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06004003 RID: 16387 RVA: 0x0019E671 File Offset: 0x0019CA71
		// (set) Token: 0x06004004 RID: 16388 RVA: 0x0019E679 File Offset: 0x0019CA79
		public ItemInfo Info { get; set; }
	}
}
