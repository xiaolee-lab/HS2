using System;

namespace CharaCustom
{
	// Token: 0x020009AE RID: 2478
	public class CustomClothesFileInfo
	{
		// Token: 0x04004285 RID: 17029
		public CustomClothesScrollViewInfo cssvi;

		// Token: 0x04004286 RID: 17030
		public int index;

		// Token: 0x04004287 RID: 17031
		public string FullPath = string.Empty;

		// Token: 0x04004288 RID: 17032
		public string FileName = string.Empty;

		// Token: 0x04004289 RID: 17033
		public DateTime time;

		// Token: 0x0400428A RID: 17034
		public string name = string.Empty;

		// Token: 0x0400428B RID: 17035
		public byte[] pngData;

		// Token: 0x0400428C RID: 17036
		public CoordinateCategoryKind cateKind = CoordinateCategoryKind.Female;
	}
}
