using System;
using UnityEngine;

// Token: 0x02001113 RID: 4371
[Serializable]
public class MorphCalcInfo
{
	// Token: 0x0400757C RID: 30076
	public GameObject TargetObj;

	// Token: 0x0400757D RID: 30077
	public Mesh OriginalMesh;

	// Token: 0x0400757E RID: 30078
	public Mesh TargetMesh;

	// Token: 0x0400757F RID: 30079
	public Vector3[] OriginalPos;

	// Token: 0x04007580 RID: 30080
	public Vector3[] OriginalNormal;

	// Token: 0x04007581 RID: 30081
	public bool WeightFlags;

	// Token: 0x04007582 RID: 30082
	public int[] UpdateIndex;

	// Token: 0x04007583 RID: 30083
	public MorphUpdateInfo[] UpdateInfo;
}
