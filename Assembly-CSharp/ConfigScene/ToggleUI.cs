using System;
using System.Diagnostics;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ConfigScene
{
	// Token: 0x0200086A RID: 2154
	public class ToggleUI : Toggle
	{
		// Token: 0x14000088 RID: 136
		// (add) Token: 0x060036EC RID: 14060 RVA: 0x00145BB0 File Offset: 0x00143FB0
		// (remove) Token: 0x060036ED RID: 14061 RVA: 0x00145BE8 File Offset: 0x00143FE8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event ToggleUI.OnClickDelegate onPointerClick;

		// Token: 0x060036EE RID: 14062 RVA: 0x00145C1E File Offset: 0x0014401E
		public override void OnPointerClick(PointerEventData eventData)
		{
			base.OnPointerClick(eventData);
			if (this.onPointerClick != null)
			{
				this.onPointerClick(base.isOn);
			}
		}

		// Token: 0x0200086B RID: 2155
		// (Invoke) Token: 0x060036F0 RID: 14064
		public delegate void OnClickDelegate(bool _value);
	}
}
