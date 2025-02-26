using System;

namespace SuperScrollView
{
	// Token: 0x02000602 RID: 1538
	public class LoopListViewInitParam
	{
		// Token: 0x060023F0 RID: 9200 RVA: 0x000C72BF File Offset: 0x000C56BF
		public static LoopListViewInitParam CopyDefaultInitParam()
		{
			return new LoopListViewInitParam();
		}

		// Token: 0x04002358 RID: 9048
		public float mDistanceForRecycle0 = 300f;

		// Token: 0x04002359 RID: 9049
		public float mDistanceForNew0 = 200f;

		// Token: 0x0400235A RID: 9050
		public float mDistanceForRecycle1 = 300f;

		// Token: 0x0400235B RID: 9051
		public float mDistanceForNew1 = 200f;

		// Token: 0x0400235C RID: 9052
		public float mSmoothDumpRate = 0.3f;

		// Token: 0x0400235D RID: 9053
		public float mSnapFinishThreshold = 0.01f;

		// Token: 0x0400235E RID: 9054
		public float mSnapVecThreshold = 145f;

		// Token: 0x0400235F RID: 9055
		public float mItemDefaultWithPaddingSize = 20f;
	}
}
