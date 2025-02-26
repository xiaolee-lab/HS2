using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Studio
{
	// Token: 0x02001297 RID: 4759
	public class PointerAction : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x06009D60 RID: 40288 RVA: 0x00404900 File Offset: 0x00402D00
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			foreach (UnityAction unityAction in this.listClickAction)
			{
				unityAction();
			}
		}

		// Token: 0x06009D61 RID: 40289 RVA: 0x0040495C File Offset: 0x00402D5C
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			foreach (UnityAction unityAction in this.listDownAction)
			{
				unityAction();
			}
		}

		// Token: 0x06009D62 RID: 40290 RVA: 0x004049B8 File Offset: 0x00402DB8
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			foreach (UnityAction unityAction in this.listEnterAction)
			{
				unityAction();
			}
		}

		// Token: 0x06009D63 RID: 40291 RVA: 0x00404A14 File Offset: 0x00402E14
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			foreach (UnityAction unityAction in this.listExitAction)
			{
				unityAction();
			}
		}

		// Token: 0x06009D64 RID: 40292 RVA: 0x00404A70 File Offset: 0x00402E70
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			foreach (UnityAction unityAction in this.listUpAction)
			{
				unityAction();
			}
		}

		// Token: 0x04007D39 RID: 32057
		public List<UnityAction> listClickAction = new List<UnityAction>();

		// Token: 0x04007D3A RID: 32058
		public List<UnityAction> listDownAction = new List<UnityAction>();

		// Token: 0x04007D3B RID: 32059
		public List<UnityAction> listEnterAction = new List<UnityAction>();

		// Token: 0x04007D3C RID: 32060
		public List<UnityAction> listExitAction = new List<UnityAction>();

		// Token: 0x04007D3D RID: 32061
		public List<UnityAction> listUpAction = new List<UnityAction>();
	}
}
