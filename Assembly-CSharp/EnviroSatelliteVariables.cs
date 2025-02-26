using System;
using UnityEngine;

// Token: 0x0200032D RID: 813
[Serializable]
public class EnviroSatelliteVariables
{
	// Token: 0x04000E4F RID: 3663
	[Tooltip("Name of this satellite")]
	public string name;

	// Token: 0x04000E50 RID: 3664
	[Tooltip("Prefab with model that get instantiated.")]
	public GameObject prefab;

	// Token: 0x04000E51 RID: 3665
	[Tooltip("This value will influence the satellite orbitpositions.")]
	public float orbit_X;

	// Token: 0x04000E52 RID: 3666
	[Tooltip("This value will influence the satellite orbitpositions.")]
	public float orbit_Y;

	// Token: 0x04000E53 RID: 3667
	[Tooltip("The speed of the satellites orbit.")]
	public float speed;
}
