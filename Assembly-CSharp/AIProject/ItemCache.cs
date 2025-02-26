using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000E1F RID: 3615
	public class ItemCache
	{
		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x06007030 RID: 28720 RVA: 0x002FF451 File Offset: 0x002FD851
		// (set) Token: 0x06007031 RID: 28721 RVA: 0x002FF459 File Offset: 0x002FD859
		public int EventItemID { get; set; }

		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x06007032 RID: 28722 RVA: 0x002FF462 File Offset: 0x002FD862
		// (set) Token: 0x06007033 RID: 28723 RVA: 0x002FF46A File Offset: 0x002FD86A
		public ActionItemInfo KeyInfo { get; set; }

		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x06007034 RID: 28724 RVA: 0x002FF473 File Offset: 0x002FD873
		// (set) Token: 0x06007035 RID: 28725 RVA: 0x002FF47B File Offset: 0x002FD87B
		public GameObject AsGameObject { get; set; }
	}
}
