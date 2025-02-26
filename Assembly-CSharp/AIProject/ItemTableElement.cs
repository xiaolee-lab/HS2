using System;
using System.Collections.Generic;

namespace AIProject
{
	// Token: 0x02000F0B RID: 3851
	public class ItemTableElement
	{
		// Token: 0x170018E5 RID: 6373
		// (get) Token: 0x06007E1A RID: 32282 RVA: 0x0035BD00 File Offset: 0x0035A100
		// (set) Token: 0x06007E1B RID: 32283 RVA: 0x0035BD08 File Offset: 0x0035A108
		public float Rate { get; set; }

		// Token: 0x170018E6 RID: 6374
		// (get) Token: 0x06007E1C RID: 32284 RVA: 0x0035BD11 File Offset: 0x0035A111
		// (set) Token: 0x06007E1D RID: 32285 RVA: 0x0035BD19 File Offset: 0x0035A119
		public List<ItemTableElement.GatherElement> Elements { get; private set; } = new List<ItemTableElement.GatherElement>();

		// Token: 0x02000F0C RID: 3852
		public struct GatherElement
		{
			// Token: 0x040065C9 RID: 26057
			public string name;

			// Token: 0x040065CA RID: 26058
			public float prob;

			// Token: 0x040065CB RID: 26059
			public int categoryID;

			// Token: 0x040065CC RID: 26060
			public int itemID;

			// Token: 0x040065CD RID: 26061
			public int minCount;

			// Token: 0x040065CE RID: 26062
			public int maxCount;
		}
	}
}
