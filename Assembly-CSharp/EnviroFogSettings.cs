using System;
using UnityEngine;

// Token: 0x02000336 RID: 822
[Serializable]
public class EnviroFogSettings
{
	// Token: 0x04000E9F RID: 3743
	[Header("Mode")]
	[Tooltip("Unity's fog mode.")]
	public FogMode Fogmode = FogMode.Exponential;

	// Token: 0x04000EA0 RID: 3744
	[Tooltip("Simple fog = just plain color without scattering.")]
	public bool useSimpleFog;

	// Token: 0x04000EA1 RID: 3745
	[Header("Distance Fog")]
	[Tooltip("Use distance fog?")]
	public bool distanceFog = true;

	// Token: 0x04000EA2 RID: 3746
	[Tooltip("Use radial distance fog?")]
	public bool useRadialDistance = true;

	// Token: 0x04000EA3 RID: 3747
	[Tooltip("The distance where fog starts.")]
	public float startDistance;

	// Token: 0x04000EA4 RID: 3748
	[Range(0f, 10f)]
	[Tooltip("The intensity of distance fog.")]
	public float distanceFogIntensity = 4f;

	// Token: 0x04000EA5 RID: 3749
	[Range(0f, 1f)]
	[Tooltip("The maximum density of fog.")]
	public float maximumFogDensity = 0.9f;

	// Token: 0x04000EA6 RID: 3750
	[Header("Height Fog")]
	[Tooltip("Use heightbased fog?")]
	public bool heightFog = true;

	// Token: 0x04000EA7 RID: 3751
	[Tooltip("The height of heightbased fog.")]
	public float height = 90f;

	// Token: 0x04000EA8 RID: 3752
	[Range(0f, 1f)]
	[Tooltip("The intensity of heightbased fog.")]
	public float heightFogIntensity = 1f;

	// Token: 0x04000EA9 RID: 3753
	[HideInInspector]
	public float heightDensity = 0.15f;

	// Token: 0x04000EAA RID: 3754
	[Header("Height Fog Noise")]
	[Range(0f, 1f)]
	[Tooltip("The noise intensity of height based fog.")]
	public float noiseIntensity = 1f;

	// Token: 0x04000EAB RID: 3755
	[Tooltip("The noise intensity offset of height based fog.")]
	[Range(0f, 1f)]
	public float noiseIntensityOffset = 0.3f;

	// Token: 0x04000EAC RID: 3756
	[Range(0f, 0.1f)]
	[Tooltip("The noise scaling of height based fog.")]
	public float noiseScale = 0.001f;

	// Token: 0x04000EAD RID: 3757
	[Tooltip("The speed and direction of height based fog.")]
	public Vector2 noiseVelocity = new Vector2(3f, 1.5f);

	// Token: 0x04000EAE RID: 3758
	[Header("Fog Scattering")]
	[Tooltip("Influence scattering near sun.")]
	public float mie = 5f;

	// Token: 0x04000EAF RID: 3759
	[Tooltip("Influence scattering near sun.")]
	public float g = 5f;

	// Token: 0x04000EB0 RID: 3760
	[Header("Fog Dithering")]
	[Tooltip("Fog dithering settings to reduce color banding.")]
	public float fogDitheringScale = 240f;

	// Token: 0x04000EB1 RID: 3761
	[Tooltip("Fog dithering settings to reduce color banding.")]
	public float fogDitheringIntensity = 0.5f;

	// Token: 0x04000EB2 RID: 3762
	[HideInInspector]
	public float skyFogIntensity = 1f;
}
