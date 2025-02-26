using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SceneAssist
{
	// Token: 0x02001026 RID: 4134
	public class PointerAction : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x06008A91 RID: 35473 RVA: 0x003A4548 File Offset: 0x003A2948
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			for (int i = 0; i < this.listClickAction.Count; i++)
			{
				this.listClickAction[i]();
			}
		}

		// Token: 0x06008A92 RID: 35474 RVA: 0x003A4584 File Offset: 0x003A2984
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			for (int i = 0; i < this.listDownAction.Count; i++)
			{
				this.listDownAction[i]();
			}
		}

		// Token: 0x06008A93 RID: 35475 RVA: 0x003A45C0 File Offset: 0x003A29C0
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			for (int i = 0; i < this.listEnterAction.Count; i++)
			{
				this.listEnterAction[i]();
			}
		}

		// Token: 0x06008A94 RID: 35476 RVA: 0x003A45FC File Offset: 0x003A29FC
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			for (int i = 0; i < this.listExitAction.Count; i++)
			{
				this.listExitAction[i]();
			}
		}

		// Token: 0x06008A95 RID: 35477 RVA: 0x003A4638 File Offset: 0x003A2A38
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			for (int i = 0; i < this.listUpAction.Count; i++)
			{
				this.listUpAction[i]();
			}
		}

		// Token: 0x04007111 RID: 28945
		public List<UnityAction> listClickAction = new List<UnityAction>();

		// Token: 0x04007112 RID: 28946
		public List<UnityAction> listDownAction = new List<UnityAction>();

		// Token: 0x04007113 RID: 28947
		public List<UnityAction> listEnterAction = new List<UnityAction>();

		// Token: 0x04007114 RID: 28948
		public List<UnityAction> listExitAction = new List<UnityAction>();

		// Token: 0x04007115 RID: 28949
		public List<UnityAction> listUpAction = new List<UnityAction>();
	}
}
