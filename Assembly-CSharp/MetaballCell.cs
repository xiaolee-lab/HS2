using System;
using UnityEngine;

// Token: 0x02000A51 RID: 2641
public class MetaballCell
{
	// Token: 0x04004782 RID: 18306
	public Vector3 baseColor;

	// Token: 0x04004783 RID: 18307
	public string tag;

	// Token: 0x04004784 RID: 18308
	public float radius;

	// Token: 0x04004785 RID: 18309
	public float density = 1f;

	// Token: 0x04004786 RID: 18310
	public Vector3 modelPosition = Vector3.zero;

	// Token: 0x04004787 RID: 18311
	public Quaternion modelRotation = Quaternion.identity;
}
