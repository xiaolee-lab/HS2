using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SceneAssist
{
	// Token: 0x02001027 RID: 4135
	public class PointerDownAction : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
	{
		// Token: 0x06008A97 RID: 35479 RVA: 0x003A4688 File Offset: 0x003A2A88
		public void OnPointerDown(PointerEventData eventData)
		{
			for (int i = 0; i < this.listAction.Count; i++)
			{
				this.listAction[i]();
			}
		}

		// Token: 0x04007116 RID: 28950
		public List<UnityAction> listAction = new List<UnityAction>();
	}
}
