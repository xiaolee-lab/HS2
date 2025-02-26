using System;
using UnityEngine.EventSystems;

namespace Battlehub.UIControls
{
	// Token: 0x020000A0 RID: 160
	public class PointerEventArgs : EventArgs
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x00015139 File Offset: 0x00013539
		public PointerEventArgs(PointerEventData data)
		{
			this.Data = data;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00015148 File Offset: 0x00013548
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x00015150 File Offset: 0x00013550
		public PointerEventData Data { get; private set; }
	}
}
