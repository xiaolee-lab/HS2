using System;

namespace SuperScrollView
{
	// Token: 0x02000609 RID: 1545
	public class GridViewLayoutParam
	{
		// Token: 0x0600246B RID: 9323 RVA: 0x000CCC00 File Offset: 0x000CB000
		public bool CheckParam()
		{
			return this.mColumnOrRowCount > 0 && this.mItemWidthOrHeight > 0f && (this.mCustomColumnOrRowOffsetArray == null || this.mCustomColumnOrRowOffsetArray.Length == this.mColumnOrRowCount);
		}

		// Token: 0x040023B1 RID: 9137
		public int mColumnOrRowCount;

		// Token: 0x040023B2 RID: 9138
		public float mItemWidthOrHeight;

		// Token: 0x040023B3 RID: 9139
		public float mPadding1;

		// Token: 0x040023B4 RID: 9140
		public float mPadding2;

		// Token: 0x040023B5 RID: 9141
		public float[] mCustomColumnOrRowOffsetArray;
	}
}
