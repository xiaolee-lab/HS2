using System;
using UnityEngine;

// Token: 0x0200034C RID: 844
[Serializable]
public class EnviroCloudsLayer
{
	// Token: 0x04000F73 RID: 3955
	[HideInInspector]
	public GameObject myObj;

	// Token: 0x04000F74 RID: 3956
	[HideInInspector]
	public Material myMaterial;

	// Token: 0x04000F75 RID: 3957
	[HideInInspector]
	public Material myShadowMaterial;

	// Token: 0x04000F76 RID: 3958
	[HideInInspector]
	public float DirectLightIntensity = 10f;

	// Token: 0x04000F77 RID: 3959
	[HideInInspector]
	[Tooltip("Base color of clouds.")]
	public Color FirstColor = Color.white;

	// Token: 0x04000F78 RID: 3960
	[HideInInspector]
	[Tooltip("Coverage rate of clouds generated.")]
	public float Coverage;

	// Token: 0x04000F79 RID: 3961
	[HideInInspector]
	[Tooltip("Density of clouds generated.")]
	public float Density;

	// Token: 0x04000F7A RID: 3962
	[HideInInspector]
	[Tooltip("Clouds detail normal power modificator.")]
	public float DetailPower = 2f;

	// Token: 0x04000F7B RID: 3963
	[HideInInspector]
	[Tooltip("Clouds alpha modificator.")]
	public float Alpha;
}
