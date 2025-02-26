using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CharaCustom
{
	// Token: 0x020009C7 RID: 2503
	public class CustomCanvasSort : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
	{
		// Token: 0x06004939 RID: 18745 RVA: 0x001BD754 File Offset: 0x001BBB54
		public virtual void OnPointerDown(PointerEventData ped)
		{
			if (!Input.GetMouseButton(0))
			{
				return;
			}
			if (null != this.ccsCtrl)
			{
				this.ccsCtrl.SortCanvas(this.canvas);
			}
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x001BD784 File Offset: 0x001BBB84
		public virtual void OnBeginDrag(PointerEventData ped)
		{
		}

		// Token: 0x0600493B RID: 18747 RVA: 0x001BD786 File Offset: 0x001BBB86
		public virtual void OnDrag(PointerEventData ped)
		{
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x001BD788 File Offset: 0x001BBB88
		public virtual void OnEndDrag(PointerEventData ped)
		{
		}

		// Token: 0x040043D7 RID: 17367
		[SerializeField]
		private CustomCanvasSortControl ccsCtrl;

		// Token: 0x040043D8 RID: 17368
		[SerializeField]
		private Canvas canvas;
	}
}
