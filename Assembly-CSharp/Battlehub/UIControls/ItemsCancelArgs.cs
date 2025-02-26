using System;
using System.Collections.Generic;

namespace Battlehub.UIControls
{
	// Token: 0x02000083 RID: 131
	public class ItemsCancelArgs : EventArgs
	{
		// Token: 0x06000161 RID: 353 RVA: 0x0000B506 File Offset: 0x00009906
		public ItemsCancelArgs(List<object> items)
		{
			this.Items = items;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000B515 File Offset: 0x00009915
		// (set) Token: 0x06000163 RID: 355 RVA: 0x0000B51D File Offset: 0x0000991D
		public List<object> Items { get; set; }
	}
}
