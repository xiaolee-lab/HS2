using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Studio
{
	// Token: 0x0200124A RID: 4682
	public class GuideInputSender : MonoBehaviour, ISelectHandler, IDeselectHandler, IEventSystemHandler
	{
		// Token: 0x06009A5D RID: 39517 RVA: 0x003F6450 File Offset: 0x003F4850
		public void OnDeselect(BaseEventData eventData)
		{
			if (this.guideInput)
			{
				this.guideInput.selectIndex = -1;
			}
		}

		// Token: 0x06009A5E RID: 39518 RVA: 0x003F646E File Offset: 0x003F486E
		public void OnSelect(BaseEventData eventData)
		{
			if (this.guideInput)
			{
				this.guideInput.selectIndex = this.index;
			}
		}

		// Token: 0x04007B26 RID: 31526
		[SerializeField]
		private GuideInput guideInput;

		// Token: 0x04007B27 RID: 31527
		[SerializeField]
		private int index;
	}
}
