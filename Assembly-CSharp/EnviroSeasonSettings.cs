using System;
using UnityEngine;

// Token: 0x0200032E RID: 814
[Serializable]
public class EnviroSeasonSettings
{
	// Token: 0x04000E54 RID: 3668
	[Tooltip("How many days in spring?")]
	public float SpringInDays = 90f;

	// Token: 0x04000E55 RID: 3669
	[Tooltip("How many days in summer?")]
	public float SummerInDays = 93f;

	// Token: 0x04000E56 RID: 3670
	[Tooltip("How many days in autumn?")]
	public float AutumnInDays = 92f;

	// Token: 0x04000E57 RID: 3671
	[Tooltip("How many days in winter?")]
	public float WinterInDays = 90f;
}
