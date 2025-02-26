using System;

// Token: 0x02000B1E RID: 2846
public class StartWaitAnim
{
	// Token: 0x04004E0D RID: 19981
	public int ID;

	// Token: 0x04004E0E RID: 19982
	public StartWaitAnim.Info[] Player = new StartWaitAnim.Info[2];

	// Token: 0x04004E0F RID: 19983
	public StartWaitAnim.Info[] Agent = new StartWaitAnim.Info[2];

	// Token: 0x04004E10 RID: 19984
	public string CameraFile;

	// Token: 0x04004E11 RID: 19985
	public int VisibleMode;

	// Token: 0x02000B1F RID: 2847
	public struct Info
	{
		// Token: 0x04004E12 RID: 19986
		public string abName;

		// Token: 0x04004E13 RID: 19987
		public string assetName;

		// Token: 0x04004E14 RID: 19988
		public string State;
	}
}
