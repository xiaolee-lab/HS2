using System;

namespace AIProject
{
	// Token: 0x02000C1A RID: 3098
	[Flags]
	public enum PoseType
	{
		// Token: 0x0400552E RID: 21806
		Stand = 1,
		// Token: 0x0400552F RID: 21807
		Floor = 2,
		// Token: 0x04005530 RID: 21808
		Sit = 4,
		// Token: 0x04005531 RID: 21809
		Recline = 8,
		// Token: 0x04005532 RID: 21810
		PairF2F = 16,
		// Token: 0x04005533 RID: 21811
		PairSxS = 32
	}
}
