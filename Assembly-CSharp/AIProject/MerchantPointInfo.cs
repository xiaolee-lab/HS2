using System;
using AIProject.Definitions;

namespace AIProject
{
	// Token: 0x02000C23 RID: 3107
	[Serializable]
	public struct MerchantPointInfo
	{
		// Token: 0x0400555C RID: 21852
		public string actionName;

		// Token: 0x0400555D RID: 21853
		public int pointID;

		// Token: 0x0400555E RID: 21854
		public int eventID;

		// Token: 0x0400555F RID: 21855
		public Merchant.EventType eventTypeMask;

		// Token: 0x04005560 RID: 21856
		public int poseID;

		// Token: 0x04005561 RID: 21857
		public bool isTalkable;

		// Token: 0x04005562 RID: 21858
		public bool isLooking;

		// Token: 0x04005563 RID: 21859
		public string baseNullName;

		// Token: 0x04005564 RID: 21860
		public string recoveryNullName;
	}
}
