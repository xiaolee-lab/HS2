using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Studio
{
	// Token: 0x020012CE RID: 4814
	public class CanvasSortOrder : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
	{
		// Token: 0x0600A093 RID: 41107 RVA: 0x0041FCF6 File Offset: 0x0041E0F6
		public void OnPointerDown(PointerEventData eventData)
		{
			SortCanvas.select = this.m_Canvas;
		}

		// Token: 0x04007EDC RID: 32476
		[SerializeField]
		private Canvas m_Canvas;
	}
}
