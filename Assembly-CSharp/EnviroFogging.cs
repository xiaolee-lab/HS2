using System;
using UnityEngine;

// Token: 0x0200034A RID: 842
[Serializable]
public class EnviroFogging
{
	// Token: 0x04000F6D RID: 3949
	[HideInInspector]
	public float skyFogHeight = 1f;

	// Token: 0x04000F6E RID: 3950
	[HideInInspector]
	public float skyFogStrength = 0.1f;

	// Token: 0x04000F6F RID: 3951
	[HideInInspector]
	public float scatteringStrenght = 0.5f;

	// Token: 0x04000F70 RID: 3952
	[HideInInspector]
	public float sunBlocking = 0.5f;
}
