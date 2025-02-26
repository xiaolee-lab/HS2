using System;

namespace AIProject.Animal
{
	// Token: 0x02000B63 RID: 2915
	[Flags]
	public enum AnimalTypes
	{
		// Token: 0x0400500A RID: 20490
		Cat = 1,
		// Token: 0x0400500B RID: 20491
		Chicken = 2,
		// Token: 0x0400500C RID: 20492
		Fish = 4,
		// Token: 0x0400500D RID: 20493
		Butterfly = 8,
		// Token: 0x0400500E RID: 20494
		Mecha = 16,
		// Token: 0x0400500F RID: 20495
		Frog = 32,
		// Token: 0x04005010 RID: 20496
		BirdFlock = 64,
		// Token: 0x04005011 RID: 20497
		CatWithFish = 128,
		// Token: 0x04005012 RID: 20498
		CatTank = 256,
		// Token: 0x04005013 RID: 20499
		Chick = 512,
		// Token: 0x04005014 RID: 20500
		Fairy = 1024,
		// Token: 0x04005015 RID: 20501
		DarkSpirit = 2048,
		// Token: 0x04005016 RID: 20502
		Ground = 787,
		// Token: 0x04005017 RID: 20503
		Flying = 3144,
		// Token: 0x04005018 RID: 20504
		Insect = 32,
		// Token: 0x04005019 RID: 20505
		Viewing = 3084,
		// Token: 0x0400501A RID: 20506
		All = 3967
	}
}
