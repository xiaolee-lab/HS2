using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000E1D RID: 3613
	public class MerchantMarker : MonoBehaviour
	{
		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x06007028 RID: 28712 RVA: 0x002FF330 File Offset: 0x002FD730
		public static Dictionary<int, MerchantActor> MerchantMarkerTable { get; } = new Dictionary<int, MerchantActor>();

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x06007029 RID: 28713 RVA: 0x002FF337 File Offset: 0x002FD737
		public static List<int> Keys { get; } = new List<int>();

		// Token: 0x0600702A RID: 28714 RVA: 0x002FF340 File Offset: 0x002FD740
		private void Awake()
		{
			if (this.merchant == null)
			{
				this.merchant = base.GetComponent<MerchantActor>();
			}
			if (this.merchant == null)
			{
				UnityEngine.Object.Destroy(this);
				return;
			}
			this.InstanceID = this.merchant.InstanceID;
		}

		// Token: 0x0600702B RID: 28715 RVA: 0x002FF394 File Offset: 0x002FD794
		private void OnEnable()
		{
			if (this.merchant != null)
			{
				MerchantMarker.MerchantMarkerTable[this.InstanceID] = this.merchant;
				if (!MerchantMarker.Keys.Contains(this.InstanceID))
				{
					MerchantMarker.Keys.Add(this.InstanceID);
				}
			}
		}

		// Token: 0x0600702C RID: 28716 RVA: 0x002FF3ED File Offset: 0x002FD7ED
		private void OnDisable()
		{
			if (this.merchant != null)
			{
				MerchantMarker.MerchantMarkerTable.Remove(this.InstanceID);
				MerchantMarker.Keys.RemoveAll((int x) => x == this.InstanceID);
			}
		}

		// Token: 0x04005C48 RID: 23624
		private int InstanceID;

		// Token: 0x04005C49 RID: 23625
		private MerchantActor merchant;
	}
}
