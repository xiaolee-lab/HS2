using System;
using UnityEngine.EventSystems;

namespace Battlehub.UIControls
{
	// Token: 0x02000088 RID: 136
	public class ItemDropArgs : EventArgs
	{
		// Token: 0x0600017C RID: 380 RVA: 0x0000B601 File Offset: 0x00009A01
		public ItemDropArgs(object[] dragItems, object dropTarget, ItemDropAction action, bool isExternal, PointerEventData pointerEventData)
		{
			this.DragItems = dragItems;
			this.DropTarget = dropTarget;
			this.Action = action;
			this.IsExternal = isExternal;
			this.PointerEventData = pointerEventData;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000B62E File Offset: 0x00009A2E
		// (set) Token: 0x0600017E RID: 382 RVA: 0x0000B636 File Offset: 0x00009A36
		public object[] DragItems { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000B63F File Offset: 0x00009A3F
		// (set) Token: 0x06000180 RID: 384 RVA: 0x0000B647 File Offset: 0x00009A47
		public object DropTarget { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000B650 File Offset: 0x00009A50
		// (set) Token: 0x06000182 RID: 386 RVA: 0x0000B658 File Offset: 0x00009A58
		public ItemDropAction Action { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000B661 File Offset: 0x00009A61
		// (set) Token: 0x06000184 RID: 388 RVA: 0x0000B669 File Offset: 0x00009A69
		public bool IsExternal { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000B672 File Offset: 0x00009A72
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0000B67A File Offset: 0x00009A7A
		public PointerEventData PointerEventData { get; private set; }
	}
}
