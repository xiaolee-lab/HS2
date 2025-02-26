using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SceneAssist
{
	// Token: 0x02001029 RID: 4137
	public class PointerEnterExitAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
	{
		// Token: 0x06008A9B RID: 35483 RVA: 0x003A4730 File Offset: 0x003A2B30
		public void OnPointerEnter(PointerEventData eventData)
		{
			for (int i = 0; i < this.listActionEnter.Count; i++)
			{
				this.listActionEnter[i]();
			}
		}

		// Token: 0x06008A9C RID: 35484 RVA: 0x003A476C File Offset: 0x003A2B6C
		public void OnPointerExit(PointerEventData eventData)
		{
			for (int i = 0; i < this.listActionExit.Count; i++)
			{
				this.listActionExit[i]();
			}
		}

		// Token: 0x04007118 RID: 28952
		public List<UnityAction> listActionEnter = new List<UnityAction>();

		// Token: 0x04007119 RID: 28953
		public List<UnityAction> listActionExit = new List<UnityAction>();
	}
}
