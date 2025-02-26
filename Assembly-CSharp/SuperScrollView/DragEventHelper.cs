using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SuperScrollView
{
	// Token: 0x020005B0 RID: 1456
	public class DragEventHelper : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
	{
		// Token: 0x060021BB RID: 8635 RVA: 0x000BA108 File Offset: 0x000B8508
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (this.mOnBeginDragHandler != null)
			{
				this.mOnBeginDragHandler(eventData);
			}
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x000BA121 File Offset: 0x000B8521
		public void OnDrag(PointerEventData eventData)
		{
			if (this.mOnDragHandler != null)
			{
				this.mOnDragHandler(eventData);
			}
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x000BA13A File Offset: 0x000B853A
		public void OnEndDrag(PointerEventData eventData)
		{
			if (this.mOnEndDragHandler != null)
			{
				this.mOnEndDragHandler(eventData);
			}
		}

		// Token: 0x0400214E RID: 8526
		public DragEventHelper.OnDragEventHandler mOnBeginDragHandler;

		// Token: 0x0400214F RID: 8527
		public DragEventHelper.OnDragEventHandler mOnDragHandler;

		// Token: 0x04002150 RID: 8528
		public DragEventHelper.OnDragEventHandler mOnEndDragHandler;

		// Token: 0x020005B1 RID: 1457
		// (Invoke) Token: 0x060021BF RID: 8639
		public delegate void OnDragEventHandler(PointerEventData eventData);
	}
}
