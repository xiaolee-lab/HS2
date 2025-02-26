using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Studio.SceneAssist
{
	// Token: 0x02001299 RID: 4761
	public class PointerEnterAction : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler
	{
		// Token: 0x06009D68 RID: 40296 RVA: 0x00404B50 File Offset: 0x00402F50
		public void OnPointerEnter(PointerEventData eventData)
		{
			foreach (UnityAction unityAction in this.listAction)
			{
				unityAction();
			}
		}

		// Token: 0x04007D3F RID: 32063
		public List<UnityAction> listAction = new List<UnityAction>();
	}
}
