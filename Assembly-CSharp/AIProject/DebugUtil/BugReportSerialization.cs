using System;
using MessagePack;
using UnityEngine;

namespace AIProject.DebugUtil
{
	// Token: 0x02000E1B RID: 3611
	[MessagePackObject(false)]
	public class BugReportSerialization
	{
		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x06006FFF RID: 28671 RVA: 0x002FEC9A File Offset: 0x002FD09A
		// (set) Token: 0x06007000 RID: 28672 RVA: 0x002FECA2 File Offset: 0x002FD0A2
		[Key(0)]
		public Vector3 Position { get; set; }

		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x06007001 RID: 28673 RVA: 0x002FECAB File Offset: 0x002FD0AB
		// (set) Token: 0x06007002 RID: 28674 RVA: 0x002FECB3 File Offset: 0x002FD0B3
		[Key(1)]
		public bool IsEvent { get; set; }

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x06007003 RID: 28675 RVA: 0x002FECBC File Offset: 0x002FD0BC
		// (set) Token: 0x06007004 RID: 28676 RVA: 0x002FECC4 File Offset: 0x002FD0C4
		[Key(2)]
		public bool IsLoadingScene { get; set; }

		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x06007005 RID: 28677 RVA: 0x002FECCD File Offset: 0x002FD0CD
		// (set) Token: 0x06007006 RID: 28678 RVA: 0x002FECD5 File Offset: 0x002FD0D5
		[Key(3)]
		public int ChunkID { get; set; }

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x06007007 RID: 28679 RVA: 0x002FECDE File Offset: 0x002FD0DE
		// (set) Token: 0x06007008 RID: 28680 RVA: 0x002FECE6 File Offset: 0x002FD0E6
		[Key(4)]
		public int PrevEventID { get; set; }

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x06007009 RID: 28681 RVA: 0x002FECEF File Offset: 0x002FD0EF
		// (set) Token: 0x0600700A RID: 28682 RVA: 0x002FECF7 File Offset: 0x002FD0F7
		[Key(5)]
		public string Revision { get; set; }

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x0600700B RID: 28683 RVA: 0x002FED00 File Offset: 0x002FD100
		// (set) Token: 0x0600700C RID: 28684 RVA: 0x002FED08 File Offset: 0x002FD108
		[Key(6)]
		public RuntimePlatform Platform { get; set; }

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x0600700D RID: 28685 RVA: 0x002FED11 File Offset: 0x002FD111
		// (set) Token: 0x0600700E RID: 28686 RVA: 0x002FED19 File Offset: 0x002FD119
		[Key(7)]
		public TimeSpan RealTimeSinceStartup { get; set; }

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x0600700F RID: 28687 RVA: 0x002FED22 File Offset: 0x002FD122
		// (set) Token: 0x06007010 RID: 28688 RVA: 0x002FED2A File Offset: 0x002FD12A
		[Key(8)]
		public DateTime DateTimeInGame { get; set; }

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x06007011 RID: 28689 RVA: 0x002FED33 File Offset: 0x002FD133
		// (set) Token: 0x06007012 RID: 28690 RVA: 0x002FED3B File Offset: 0x002FD13B
		[Key(9)]
		public TimeZone TimeZone { get; set; }

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x06007013 RID: 28691 RVA: 0x002FED44 File Offset: 0x002FD144
		// (set) Token: 0x06007014 RID: 28692 RVA: 0x002FED4C File Offset: 0x002FD14C
		[Key(10)]
		public Weather Weather { get; set; }

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x06007015 RID: 28693 RVA: 0x002FED55 File Offset: 0x002FD155
		// (set) Token: 0x06007016 RID: 28694 RVA: 0x002FED5D File Offset: 0x002FD15D
		[Key(11)]
		public Temperature Tempareture { get; set; }

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x06007017 RID: 28695 RVA: 0x002FED66 File Offset: 0x002FD166
		// (set) Token: 0x06007018 RID: 28696 RVA: 0x002FED6E File Offset: 0x002FD16E
		[Key(12)]
		public float FrameRate { get; set; }

		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x06007019 RID: 28697 RVA: 0x002FED77 File Offset: 0x002FD177
		// (set) Token: 0x0600701A RID: 28698 RVA: 0x002FED7F File Offset: 0x002FD17F
		[Key(13)]
		public long MemoryUsage { get; set; }

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x0600701B RID: 28699 RVA: 0x002FED88 File Offset: 0x002FD188
		// (set) Token: 0x0600701C RID: 28700 RVA: 0x002FED90 File Offset: 0x002FD190
		[Key(14)]
		public float MemoryAvailable { get; set; }

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x0600701D RID: 28701 RVA: 0x002FED99 File Offset: 0x002FD199
		// (set) Token: 0x0600701E RID: 28702 RVA: 0x002FEDA1 File Offset: 0x002FD1A1
		[Key(15)]
		public string StackTrace { get; set; }
	}
}
