using System;

namespace Battlehub.UIControls
{
	// Token: 0x02000084 RID: 132
	public class ItemsRemovedArgs : EventArgs
	{
		// Token: 0x06000164 RID: 356 RVA: 0x0000B526 File Offset: 0x00009926
		public ItemsRemovedArgs(object[] items)
		{
			this.Items = items;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000B535 File Offset: 0x00009935
		// (set) Token: 0x06000166 RID: 358 RVA: 0x0000B53D File Offset: 0x0000993D
		public object[] Items { get; private set; }
	}
}
