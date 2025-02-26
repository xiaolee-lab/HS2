using System;

namespace AIProject.UI
{
	// Token: 0x02000E5A RID: 3674
	public class GameCoordinateFileInfo
	{
		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x0600743F RID: 29759 RVA: 0x00317594 File Offset: 0x00315994
		// (set) Token: 0x06007440 RID: 29760 RVA: 0x0031759C File Offset: 0x0031599C
		public int Index { get; set; }

		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x06007441 RID: 29761 RVA: 0x003175A5 File Offset: 0x003159A5
		// (set) Token: 0x06007442 RID: 29762 RVA: 0x003175AD File Offset: 0x003159AD
		public string FullPath { get; set; } = string.Empty;

		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x06007443 RID: 29763 RVA: 0x003175B6 File Offset: 0x003159B6
		// (set) Token: 0x06007444 RID: 29764 RVA: 0x003175BE File Offset: 0x003159BE
		public string FileName { get; set; } = string.Empty;

		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x06007445 RID: 29765 RVA: 0x003175C7 File Offset: 0x003159C7
		// (set) Token: 0x06007446 RID: 29766 RVA: 0x003175CF File Offset: 0x003159CF
		public DateTime Time { get; set; }

		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x06007447 RID: 29767 RVA: 0x003175D8 File Offset: 0x003159D8
		// (set) Token: 0x06007448 RID: 29768 RVA: 0x003175E0 File Offset: 0x003159E0
		public byte[] PngData { get; set; }

		// Token: 0x17001667 RID: 5735
		// (get) Token: 0x06007449 RID: 29769 RVA: 0x003175E9 File Offset: 0x003159E9
		// (set) Token: 0x0600744A RID: 29770 RVA: 0x003175F1 File Offset: 0x003159F1
		public bool IsInSaveData { get; set; }
	}
}
