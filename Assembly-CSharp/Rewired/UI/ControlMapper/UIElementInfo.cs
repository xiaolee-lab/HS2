using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000564 RID: 1380
	[AddComponentMenu("")]
	public abstract class UIElementInfo : MonoBehaviour, ISelectHandler, IEventSystemHandler
	{
		// Token: 0x14000076 RID: 118
		// (add) Token: 0x06001D0E RID: 7438 RVA: 0x0009C9A8 File Offset: 0x0009ADA8
		// (remove) Token: 0x06001D0F RID: 7439 RVA: 0x0009C9E0 File Offset: 0x0009ADE0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<GameObject> OnSelectedEvent;

		// Token: 0x06001D10 RID: 7440 RVA: 0x0009CA16 File Offset: 0x0009AE16
		public void OnSelect(BaseEventData eventData)
		{
			if (this.OnSelectedEvent != null)
			{
				this.OnSelectedEvent(base.gameObject);
			}
		}

		// Token: 0x04001E29 RID: 7721
		public string identifier;

		// Token: 0x04001E2A RID: 7722
		public int intData;

		// Token: 0x04001E2B RID: 7723
		public Text text;
	}
}
