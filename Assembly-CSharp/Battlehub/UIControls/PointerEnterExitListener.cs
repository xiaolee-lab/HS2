using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battlehub.UIControls
{
	// Token: 0x0200009A RID: 154
	public class PointerEnterExitListener : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IEventSystemHandler
	{
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000288 RID: 648 RVA: 0x00010A2C File Offset: 0x0000EE2C
		// (remove) Token: 0x06000289 RID: 649 RVA: 0x00010A64 File Offset: 0x0000EE64
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<PointerEventArgs> PointerEnter;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600028A RID: 650 RVA: 0x00010A9C File Offset: 0x0000EE9C
		// (remove) Token: 0x0600028B RID: 651 RVA: 0x00010AD4 File Offset: 0x0000EED4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<PointerEventArgs> PointerExit;

		// Token: 0x0600028C RID: 652 RVA: 0x00010B0A File Offset: 0x0000EF0A
		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			if (this.PointerEnter != null)
			{
				this.PointerEnter(this, new PointerEventArgs(eventData));
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00010B29 File Offset: 0x0000EF29
		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			if (this.PointerExit != null)
			{
				this.PointerExit(this, new PointerEventArgs(eventData));
			}
		}
	}
}
