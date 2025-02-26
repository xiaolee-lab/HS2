using System;
using System.Collections.Generic;

namespace BoneSwayCtrl
{
	// Token: 0x02001146 RID: 4422
	public class CBoneData
	{
		// Token: 0x04007652 RID: 30290
		public int nNumLocater;

		// Token: 0x04007653 RID: 30291
		public List<CFrameInfo> listLocater = new List<CFrameInfo>();

		// Token: 0x04007654 RID: 30292
		public CFrameInfo Bone = new CFrameInfo();

		// Token: 0x04007655 RID: 30293
		public CFrameInfo Reference = new CFrameInfo();

		// Token: 0x04007656 RID: 30294
		public FakeTransform transResult;

		// Token: 0x04007657 RID: 30295
		public float fLerp;

		// Token: 0x04007658 RID: 30296
		public byte nCalcKind;

		// Token: 0x04007659 RID: 30297
		public byte[] anLocaterTIdx = new byte[2];

		// Token: 0x0400765A RID: 30298
		public byte[] anLocaterRIdx = new byte[2];

		// Token: 0x0400765B RID: 30299
		public byte nTransformKind;

		// Token: 0x0400765C RID: 30300
		public float fScaleT = 1f;

		// Token: 0x0400765D RID: 30301
		public float fScaleYT = 1f;

		// Token: 0x0400765E RID: 30302
		public float fScaleR = 1f;
	}
}
