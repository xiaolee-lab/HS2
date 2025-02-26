using System;

namespace SuperScrollView
{
	// Token: 0x02000607 RID: 1543
	public class StaggeredGridViewInitParam
	{
		// Token: 0x06002468 RID: 9320 RVA: 0x000CCBE6 File Offset: 0x000CAFE6
		public static StaggeredGridViewInitParam CopyDefaultInitParam()
		{
			return new StaggeredGridViewInitParam();
		}

		// Token: 0x040023AA RID: 9130
		public float mDistanceForRecycle0 = 300f;

		// Token: 0x040023AB RID: 9131
		public float mDistanceForNew0 = 200f;

		// Token: 0x040023AC RID: 9132
		public float mDistanceForRecycle1 = 300f;

		// Token: 0x040023AD RID: 9133
		public float mDistanceForNew1 = 200f;

		// Token: 0x040023AE RID: 9134
		public float mItemDefaultWithPaddingSize = 20f;
	}
}
