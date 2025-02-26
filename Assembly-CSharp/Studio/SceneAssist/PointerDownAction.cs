using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Studio.SceneAssist
{
	// Token: 0x02001298 RID: 4760
	public class PointerDownAction : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
	{
		// Token: 0x06009D66 RID: 40294 RVA: 0x00404AE0 File Offset: 0x00402EE0
		public void OnPointerDown(PointerEventData eventData)
		{
			foreach (UnityAction unityAction in this.listAction)
			{
				unityAction();
			}
		}

		// Token: 0x04007D3E RID: 32062
		public List<UnityAction> listAction = new List<UnityAction>();
	}
}
