using System;
using System.Collections.Generic;

// Token: 0x02000EDB RID: 3803
public class CraftCommandListBaseObject : Singleton<CraftCommandListBaseObject>
{
	// Token: 0x04006402 RID: 25602
	public List<GridInfo> BaseGridInfo;

	// Token: 0x04006403 RID: 25603
	public List<BuildPartsPool>[] BaseParts;

	// Token: 0x04006404 RID: 25604
	public int nMaxFloorCnt;

	// Token: 0x04006405 RID: 25605
	public int nTargetFloorCnt;

	// Token: 0x04006406 RID: 25606
	public List<bool[]> tmpGridActiveList;

	// Token: 0x04006407 RID: 25607
	public List<bool> tmpGridActiveListUpdate;

	// Token: 0x04006408 RID: 25608
	public Dictionary<int, string> CategoryNames;

	// Token: 0x04006409 RID: 25609
	public List<int> MaxPutHeight = new List<int>();

	// Token: 0x0400640A RID: 25610
	public int nPutPartsNum;
}
