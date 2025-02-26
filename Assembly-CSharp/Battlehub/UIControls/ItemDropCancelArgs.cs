using System;
using UnityEngine.EventSystems;

namespace Battlehub.UIControls
{
	// Token: 0x02000087 RID: 135
	public class ItemDropCancelArgs : ItemDropArgs
	{
		// Token: 0x06000179 RID: 377 RVA: 0x0000B683 File Offset: 0x00009A83
		public ItemDropCancelArgs(object[] dragItems, object dropTarget, ItemDropAction action, bool isExternal, PointerEventData pointerEventData) : base(dragItems, dropTarget, action, isExternal, pointerEventData)
		{
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000B692 File Offset: 0x00009A92
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000B69A File Offset: 0x00009A9A
		public bool Cancel { get; set; }
	}
}
