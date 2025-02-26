using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000EE1 RID: 3809
internal struct CraftSaveData
{
	// Token: 0x04006457 RID: 25687
	public int MaxFloorNum;

	// Token: 0x04006458 RID: 25688
	public int nPutPartsNum;

	// Token: 0x04006459 RID: 25689
	public List<Vector3> GridPos;

	// Token: 0x0400645A RID: 25690
	public List<bool> GridUseState;

	// Token: 0x0400645B RID: 25691
	public List<int> nFloorPartsHeight;

	// Token: 0x0400645C RID: 25692
	public List<List<List<int>>> SmallGridState;

	// Token: 0x0400645D RID: 25693
	public List<List<List<int[]>>> SmallGridOnParts;

	// Token: 0x0400645E RID: 25694
	public List<List<List<int[]>>> SmallGridOnStackWall;

	// Token: 0x0400645F RID: 25695
	public List<List<List<int>>> SmallGridCanRoofState;

	// Token: 0x04006460 RID: 25696
	public List<List<List<bool>>> SmallGridInRoomState;

	// Token: 0x04006461 RID: 25697
	public List<int> BuildPartsGridKind;

	// Token: 0x04006462 RID: 25698
	public List<int> BuildPartsKind;

	// Token: 0x04006463 RID: 25699
	public List<int> BuildPartsFloor;

	// Token: 0x04006464 RID: 25700
	public List<Vector3> BuildPartsPos;

	// Token: 0x04006465 RID: 25701
	public List<Quaternion> BuildPartsRot;

	// Token: 0x04006466 RID: 25702
	public List<List<int>> BuildPartsPutGridInfos;

	// Token: 0x04006467 RID: 25703
	public List<List<int>> BuildPartsPutSmallGridInfos;

	// Token: 0x04006468 RID: 25704
	public List<int> BuildPartsPutGridInfosNum;

	// Token: 0x04006469 RID: 25705
	public List<bool[]> tmpGridActiveList;

	// Token: 0x0400646A RID: 25706
	public List<bool> tmpGridActiveListUpdate;

	// Token: 0x0400646B RID: 25707
	public List<int> MaxPutHeight;
}
