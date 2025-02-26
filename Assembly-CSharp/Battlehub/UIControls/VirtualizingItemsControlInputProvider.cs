using System;
using UnityEngine;

namespace Battlehub.UIControls
{
	// Token: 0x020000A3 RID: 163
	public class VirtualizingItemsControlInputProvider : InputProvider
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00015187 File Offset: 0x00013587
		public override bool IsFunctionalButtonPressed
		{
			get
			{
				return Input.GetKey(this.MultiselectKey);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00015194 File Offset: 0x00013594
		public override bool IsFunctional2ButtonPressed
		{
			get
			{
				return Input.GetKey(this.RangeselectKey);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000378 RID: 888 RVA: 0x000151A1 File Offset: 0x000135A1
		public override bool IsDeleteButtonDown
		{
			get
			{
				return Input.GetKeyDown(this.DeleteKey);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000379 RID: 889 RVA: 0x000151AE File Offset: 0x000135AE
		public override bool IsSelectAllButtonDown
		{
			get
			{
				return Input.GetKeyDown(this.SelectAllKey);
			}
		}

		// Token: 0x04000313 RID: 787
		public KeyCode MultiselectKey = KeyCode.LeftControl;

		// Token: 0x04000314 RID: 788
		public KeyCode RangeselectKey = KeyCode.LeftShift;

		// Token: 0x04000315 RID: 789
		public KeyCode SelectAllKey = KeyCode.A;

		// Token: 0x04000316 RID: 790
		public KeyCode DeleteKey = KeyCode.Delete;
	}
}
