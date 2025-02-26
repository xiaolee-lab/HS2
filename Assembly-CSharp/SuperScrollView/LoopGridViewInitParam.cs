using System;

namespace SuperScrollView
{
	// Token: 0x020005FA RID: 1530
	public class LoopGridViewInitParam
	{
		// Token: 0x06002369 RID: 9065 RVA: 0x000C3F3A File Offset: 0x000C233A
		public static LoopGridViewInitParam CopyDefaultInitParam()
		{
			return new LoopGridViewInitParam();
		}

		// Token: 0x040022FA RID: 8954
		public float mSmoothDumpRate = 0.3f;

		// Token: 0x040022FB RID: 8955
		public float mSnapFinishThreshold = 0.01f;

		// Token: 0x040022FC RID: 8956
		public float mSnapVecThreshold = 145f;
	}
}
