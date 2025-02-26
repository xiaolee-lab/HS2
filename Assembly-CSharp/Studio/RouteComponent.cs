using System;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200128D RID: 4749
	public class RouteComponent : MonoBehaviour
	{
		// Token: 0x06009D2E RID: 40238 RVA: 0x00403F65 File Offset: 0x00402365
		public void OnComplete()
		{
			if (this.onComplete != null)
			{
				this.onComplete();
			}
		}

		// Token: 0x04007D12 RID: 32018
		public RouteComponent.OnCompleteDel onComplete;

		// Token: 0x0200128E RID: 4750
		// (Invoke) Token: 0x06009D30 RID: 40240
		public delegate bool OnCompleteDel();
	}
}
