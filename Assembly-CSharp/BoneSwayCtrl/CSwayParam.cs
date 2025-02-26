using System;
using System.Collections.Generic;

namespace BoneSwayCtrl
{
	// Token: 0x02001149 RID: 4425
	[Serializable]
	public class CSwayParam
	{
		// Token: 0x0400767E RID: 30334
		public bool bEntry;

		// Token: 0x0400767F RID: 30335
		public int nPtn;

		// Token: 0x04007680 RID: 30336
		public string strName = string.Empty;

		// Token: 0x04007681 RID: 30337
		public float fBlendTime;

		// Token: 0x04007682 RID: 30338
		public bool bCalc;

		// Token: 0x04007683 RID: 30339
		public byte nCatch;

		// Token: 0x04007684 RID: 30340
		public float fMoveRate;

		// Token: 0x04007685 RID: 30341
		public List<CSwayParamDetail> listDetail = new List<CSwayParamDetail>();
	}
}
