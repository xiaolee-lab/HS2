using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000EE7 RID: 3815
public class BuildPartsInfo : MonoBehaviour
{
	// Token: 0x06007C95 RID: 31893 RVA: 0x003557AD File Offset: 0x00353BAD
	public void Init(int id, int formkind, int itemkind, int catkind, int Dire, int poolId, int height = 1)
	{
		this.nID = id;
		this.nPoolID = poolId;
		this.nFormKind = formkind;
		this.nItemKind = itemkind;
		this.nCatKind = catkind;
		this.nDirection = Dire;
		this.nHeight = height;
	}

	// Token: 0x06007C96 RID: 31894 RVA: 0x003557E4 File Offset: 0x00353BE4
	public void SetDirection(int Dire)
	{
		this.nDirection = Dire;
	}

	// Token: 0x06007C97 RID: 31895 RVA: 0x003557F0 File Offset: 0x00353BF0
	public int GetInfo(int kind)
	{
		switch (kind)
		{
		case 0:
			return this.nID;
		case 1:
			return this.nPoolID;
		case 2:
			return this.nFormKind;
		case 3:
			return this.nItemKind;
		case 4:
			return this.nDirection;
		case 5:
			return this.nCatKind;
		default:
			return -1;
		}
	}

	// Token: 0x04006482 RID: 25730
	public int nHeight;

	// Token: 0x04006483 RID: 25731
	public int nPutFloor;

	// Token: 0x04006484 RID: 25732
	public List<GridInfo> putGridInfos;

	// Token: 0x04006485 RID: 25733
	public List<int> putSmallGridInfos;

	// Token: 0x04006486 RID: 25734
	private int nID;

	// Token: 0x04006487 RID: 25735
	private int nPoolID;

	// Token: 0x04006488 RID: 25736
	private int nFormKind;

	// Token: 0x04006489 RID: 25737
	private int nItemKind;

	// Token: 0x0400648A RID: 25738
	private int nCatKind;

	// Token: 0x0400648B RID: 25739
	private int nDirection;
}
