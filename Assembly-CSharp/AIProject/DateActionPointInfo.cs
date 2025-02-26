using System;

namespace AIProject
{
	// Token: 0x02000BD0 RID: 3024
	[Serializable]
	public struct DateActionPointInfo
	{
		// Token: 0x04005343 RID: 21315
		public string actionName;

		// Token: 0x04005344 RID: 21316
		public int pointID;

		// Token: 0x04005345 RID: 21317
		public int eventID;

		// Token: 0x04005346 RID: 21318
		public EventType eventTypeMask;

		// Token: 0x04005347 RID: 21319
		public int iconID;

		// Token: 0x04005348 RID: 21320
		public int poseIDA;

		// Token: 0x04005349 RID: 21321
		public int poseIDB;

		// Token: 0x0400534A RID: 21322
		public bool isTalkable;

		// Token: 0x0400534B RID: 21323
		public int cameraID;

		// Token: 0x0400534C RID: 21324
		public string baseNullNameA;

		// Token: 0x0400534D RID: 21325
		public string baseNullNameB;

		// Token: 0x0400534E RID: 21326
		public string recoveryNullNameA;

		// Token: 0x0400534F RID: 21327
		public string recoveryNullNameB;

		// Token: 0x04005350 RID: 21328
		public string labelNullName;

		// Token: 0x04005351 RID: 21329
		public int searchAreaID;

		// Token: 0x04005352 RID: 21330
		public int gradeValue;

		// Token: 0x04005353 RID: 21331
		public int doorOpenType;
	}
}
