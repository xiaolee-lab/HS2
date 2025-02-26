using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SceneAssist
{
	// Token: 0x02001028 RID: 4136
	public class PointerEnterAction : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler
	{
		// Token: 0x06008A99 RID: 35481 RVA: 0x003A46D8 File Offset: 0x003A2AD8
		public void OnPointerEnter(PointerEventData eventData)
		{
			for (int i = 0; i < this.listAction.Count; i++)
			{
				this.listAction[i]();
			}
		}

		// Token: 0x04007117 RID: 28951
		public List<UnityAction> listAction = new List<UnityAction>();
	}
}
