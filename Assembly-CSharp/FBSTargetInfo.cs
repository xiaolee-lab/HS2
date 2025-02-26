using System;
using UnityEngine;

// Token: 0x020010F2 RID: 4338
[Serializable]
public class FBSTargetInfo
{
	// Token: 0x06008FE9 RID: 36841 RVA: 0x003BFA64 File Offset: 0x003BDE64
	public void SetSkinnedMeshRenderer()
	{
		if (this.ObjTarget)
		{
			this.smrTarget = this.ObjTarget.GetComponent<SkinnedMeshRenderer>();
		}
	}

	// Token: 0x06008FEA RID: 36842 RVA: 0x003BFA87 File Offset: 0x003BDE87
	public SkinnedMeshRenderer GetSkinnedMeshRenderer()
	{
		return this.smrTarget;
	}

	// Token: 0x06008FEB RID: 36843 RVA: 0x003BFA8F File Offset: 0x003BDE8F
	public void Clear()
	{
		this.ObjTarget = null;
		this.PtnSet = null;
		this.smrTarget = null;
	}

	// Token: 0x040074B5 RID: 29877
	public GameObject ObjTarget;

	// Token: 0x040074B6 RID: 29878
	public FBSTargetInfo.CloseOpen[] PtnSet;

	// Token: 0x040074B7 RID: 29879
	private SkinnedMeshRenderer smrTarget;

	// Token: 0x020010F3 RID: 4339
	[Serializable]
	public class CloseOpen
	{
		// Token: 0x040074B8 RID: 29880
		public int Close;

		// Token: 0x040074B9 RID: 29881
		public int Open;
	}
}
