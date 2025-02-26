using System;
using UnityEngine.EventSystems;

namespace Battlehub.UIControls
{
	// Token: 0x02000086 RID: 134
	public class ItemArgs : EventArgs
	{
		// Token: 0x06000174 RID: 372 RVA: 0x0000B5C9 File Offset: 0x000099C9
		public ItemArgs(object[] item, PointerEventData pointerEventData)
		{
			this.Items = item;
			this.PointerEventData = pointerEventData;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000B5DF File Offset: 0x000099DF
		// (set) Token: 0x06000176 RID: 374 RVA: 0x0000B5E7 File Offset: 0x000099E7
		public object[] Items { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000B5F0 File Offset: 0x000099F0
		// (set) Token: 0x06000178 RID: 376 RVA: 0x0000B5F8 File Offset: 0x000099F8
		public PointerEventData PointerEventData { get; private set; }
	}
}
