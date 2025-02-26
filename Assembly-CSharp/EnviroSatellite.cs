using System;
using UnityEngine;

// Token: 0x02000346 RID: 838
[Serializable]
public class EnviroSatellite
{
	// Token: 0x04000F44 RID: 3908
	[Tooltip("Name of this satellite")]
	public string name;

	// Token: 0x04000F45 RID: 3909
	[Tooltip("Prefab with model that get instantiated.")]
	public GameObject prefab;

	// Token: 0x04000F46 RID: 3910
	[Tooltip("Orbit distance.")]
	public float orbit;

	// Token: 0x04000F47 RID: 3911
	[Tooltip("Orbit modification on x axis.")]
	public float xRot;

	// Token: 0x04000F48 RID: 3912
	[Tooltip("Orbit modification on y axis.")]
	public float yRot;
}
