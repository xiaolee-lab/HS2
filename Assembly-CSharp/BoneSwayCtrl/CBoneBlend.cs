using System;

namespace BoneSwayCtrl
{
	// Token: 0x0200114A RID: 4426
	public class CBoneBlend
	{
		// Token: 0x04007686 RID: 30342
		public bool bBlend;

		// Token: 0x04007687 RID: 30343
		public float fLerp;

		// Token: 0x04007688 RID: 30344
		public float fBlendTime;

		// Token: 0x04007689 RID: 30345
		public float fBlendCnt;

		// Token: 0x0400768A RID: 30346
		public CSwayParam pNowParam = new CSwayParam();

		// Token: 0x0400768B RID: 30347
		public CSwayParam pNextParam = new CSwayParam();
	}
}
