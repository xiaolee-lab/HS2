using System;
using System.Diagnostics;
using UnityEngine.EventSystems;

namespace Battlehub.UIControls
{
	// Token: 0x02000077 RID: 119
	public class RectTransformChangeListener : UIBehaviour
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000ED RID: 237 RVA: 0x000099E8 File Offset: 0x00007DE8
		// (remove) Token: 0x060000EE RID: 238 RVA: 0x00009A20 File Offset: 0x00007E20
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event RectTransformChanged RectTransformChanged;

		// Token: 0x060000EF RID: 239 RVA: 0x00009A56 File Offset: 0x00007E56
		protected override void OnRectTransformDimensionsChange()
		{
			if (this.RectTransformChanged != null)
			{
				this.RectTransformChanged();
			}
		}
	}
}
