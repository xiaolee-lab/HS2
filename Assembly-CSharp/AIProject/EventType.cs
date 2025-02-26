using System;

namespace AIProject
{
	// Token: 0x02000C16 RID: 3094
	[Flags]
	public enum EventType
	{
		// Token: 0x04005504 RID: 21764
		Sleep = 1,
		// Token: 0x04005505 RID: 21765
		Break = 2,
		// Token: 0x04005506 RID: 21766
		Eat = 4,
		// Token: 0x04005507 RID: 21767
		Toilet = 8,
		// Token: 0x04005508 RID: 21768
		Bath = 16,
		// Token: 0x04005509 RID: 21769
		Play = 32,
		// Token: 0x0400550A RID: 21770
		Search = 64,
		// Token: 0x0400550B RID: 21771
		StorageIn = 128,
		// Token: 0x0400550C RID: 21772
		StorageOut = 256,
		// Token: 0x0400550D RID: 21773
		Cook = 512,
		// Token: 0x0400550E RID: 21774
		DressIn = 1024,
		// Token: 0x0400550F RID: 21775
		DressOut = 2048,
		// Token: 0x04005510 RID: 21776
		Masturbation = 4096,
		// Token: 0x04005511 RID: 21777
		Lesbian = 8192,
		// Token: 0x04005512 RID: 21778
		Move = 16384,
		// Token: 0x04005513 RID: 21779
		PutItem = 32768,
		// Token: 0x04005514 RID: 21780
		ShallowSleep = 65536,
		// Token: 0x04005515 RID: 21781
		Wash = 131072,
		// Token: 0x04005516 RID: 21782
		Location = 262144,
		// Token: 0x04005517 RID: 21783
		DoorOpen = 524288,
		// Token: 0x04005518 RID: 21784
		DoorClose = 1048576,
		// Token: 0x04005519 RID: 21785
		Drink = 2097152,
		// Token: 0x0400551A RID: 21786
		ClothChange = 4194304,
		// Token: 0x0400551B RID: 21787
		Warp = 8388608
	}
}
