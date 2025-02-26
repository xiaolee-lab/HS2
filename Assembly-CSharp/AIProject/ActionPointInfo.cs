using System;

namespace AIProject
{
	// Token: 0x02000BCF RID: 3023
	[Serializable]
	public struct ActionPointInfo
	{
		// Token: 0x04005334 RID: 21300
		public string actionName;

		// Token: 0x04005335 RID: 21301
		public int pointID;

		// Token: 0x04005336 RID: 21302
		public int eventID;

		// Token: 0x04005337 RID: 21303
		public EventType eventTypeMask;

		// Token: 0x04005338 RID: 21304
		public int iconID;

		// Token: 0x04005339 RID: 21305
		public int poseID;

		// Token: 0x0400533A RID: 21306
		public int datePoseID;

		// Token: 0x0400533B RID: 21307
		public bool isTalkable;

		// Token: 0x0400533C RID: 21308
		public int cameraID;

		// Token: 0x0400533D RID: 21309
		public string baseNullName;

		// Token: 0x0400533E RID: 21310
		public string recoveryNullName;

		// Token: 0x0400533F RID: 21311
		public string labelNullName;

		// Token: 0x04005340 RID: 21312
		public int searchAreaID;

		// Token: 0x04005341 RID: 21313
		public int gradeValue;

		// Token: 0x04005342 RID: 21314
		public int doorOpenType;
	}
}
