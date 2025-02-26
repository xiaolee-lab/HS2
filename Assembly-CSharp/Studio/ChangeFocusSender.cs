using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Studio
{
	// Token: 0x020012D0 RID: 4816
	public class ChangeFocusSender : MonoBehaviour, ISelectHandler, IDeselectHandler, IEventSystemHandler
	{
		// Token: 0x0600A09E RID: 41118 RVA: 0x0041FE6F File Offset: 0x0041E26F
		public void OnDeselect(BaseEventData eventData)
		{
			if (this.changeFocus)
			{
				this.changeFocus.select = -1;
			}
		}

		// Token: 0x0600A09F RID: 41119 RVA: 0x0041FE8D File Offset: 0x0041E28D
		public void OnSelect(BaseEventData eventData)
		{
			if (this.changeFocus)
			{
				this.changeFocus.select = this.index;
			}
		}

		// Token: 0x04007EE0 RID: 32480
		[SerializeField]
		private ChangeFocus changeFocus;

		// Token: 0x04007EE1 RID: 32481
		[SerializeField]
		private int index;
	}
}
