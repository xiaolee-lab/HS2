using System;
using System.Collections.Generic;

namespace AIProject
{
	// Token: 0x02000913 RID: 2323
	public struct RecyclingItemInfo : IComparable<RecyclingItemInfo>
	{
		// Token: 0x060040CE RID: 16590 RVA: 0x0019E682 File Offset: 0x0019CA82
		public int CompareTo(RecyclingItemInfo x)
		{
			if (this.CategoryID == x.CategoryID)
			{
				return this.ItemID - x.ItemID;
			}
			return this.CategoryID - x.CategoryID;
		}

		// Token: 0x04003CBA RID: 15546
		public int CategoryID;

		// Token: 0x04003CBB RID: 15547
		public int ItemID;

		// Token: 0x04003CBC RID: 15548
		public int IconID;

		// Token: 0x04003CBD RID: 15549
		public bool Adult;

		// Token: 0x04003CBE RID: 15550
		public List<string> ItemNameList;

		// Token: 0x04003CBF RID: 15551
		public StuffItemInfo ItemInfo;
	}
}
