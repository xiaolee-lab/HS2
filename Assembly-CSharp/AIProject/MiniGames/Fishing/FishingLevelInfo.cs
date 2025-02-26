using System;

namespace AIProject.MiniGames.Fishing
{
	// Token: 0x02000F2A RID: 3882
	public struct FishingLevelInfo
	{
		// Token: 0x06007FEB RID: 32747 RVA: 0x00363F78 File Offset: 0x00362378
		public FishingLevelInfo(int _level, float _timeScale)
		{
			this.Level = _level;
			this.TimeScale = _timeScale;
		}

		// Token: 0x170019A9 RID: 6569
		// (get) Token: 0x06007FEC RID: 32748 RVA: 0x00363F88 File Offset: 0x00362388
		// (set) Token: 0x06007FED RID: 32749 RVA: 0x00363F90 File Offset: 0x00362390
		public int Level { get; private set; }

		// Token: 0x170019AA RID: 6570
		// (get) Token: 0x06007FEE RID: 32750 RVA: 0x00363F99 File Offset: 0x00362399
		// (set) Token: 0x06007FEF RID: 32751 RVA: 0x00363FA1 File Offset: 0x003623A1
		public float TimeScale { get; private set; }
	}
}
