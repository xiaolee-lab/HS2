using System;

namespace AIProject
{
	// Token: 0x02000C59 RID: 3161
	public struct ActorAnimInfo
	{
		// Token: 0x04005828 RID: 22568
		public bool inEnableBlend;

		// Token: 0x04005829 RID: 22569
		public float inBlendSec;

		// Token: 0x0400582A RID: 22570
		public float inFadeOutTime;

		// Token: 0x0400582B RID: 22571
		public bool outEnableBlend;

		// Token: 0x0400582C RID: 22572
		public float outBlendSec;

		// Token: 0x0400582D RID: 22573
		public int directionType;

		// Token: 0x0400582E RID: 22574
		public bool endEnableBlend;

		// Token: 0x0400582F RID: 22575
		public float endBlendSec;

		// Token: 0x04005830 RID: 22576
		public bool isLoop;

		// Token: 0x04005831 RID: 22577
		public int loopMinTime;

		// Token: 0x04005832 RID: 22578
		public int loopMaxTime;

		// Token: 0x04005833 RID: 22579
		public bool hasAction;

		// Token: 0x04005834 RID: 22580
		public string loopStateName;

		// Token: 0x04005835 RID: 22581
		public int randomCount;

		// Token: 0x04005836 RID: 22582
		public float oldNormalizedTime;

		// Token: 0x04005837 RID: 22583
		public int layer;
	}
}
