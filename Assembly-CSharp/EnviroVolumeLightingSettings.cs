using System;
using UnityEngine;

// Token: 0x02000337 RID: 823
[Serializable]
public class EnviroVolumeLightingSettings
{
	// Token: 0x04000EB3 RID: 3763
	[Tooltip("Downsampling of volume light rendering.")]
	public EnviroSkyRendering.VolumtericResolution Resolution = EnviroSkyRendering.VolumtericResolution.Quarter;

	// Token: 0x04000EB4 RID: 3764
	[Tooltip("Activate or deactivate directional volume light rendering.")]
	public bool dirVolumeLighting = true;

	// Token: 0x04000EB5 RID: 3765
	[Header("Quality")]
	[Range(1f, 64f)]
	public int SampleCount = 8;

	// Token: 0x04000EB6 RID: 3766
	[Header("Light Settings")]
	[Range(0f, 1f)]
	public float ScatteringCoef = 0.025f;

	// Token: 0x04000EB7 RID: 3767
	[Range(0f, 0.1f)]
	public float ExtinctionCoef = 0.05f;

	// Token: 0x04000EB8 RID: 3768
	[Range(0f, 0.999f)]
	public float Anistropy = 0.1f;

	// Token: 0x04000EB9 RID: 3769
	public float MaxRayLength = 10f;

	// Token: 0x04000EBA RID: 3770
	[Header("3D Noise")]
	[Tooltip("Use 3D noise for directional lighting. Attention: Expensive operation for directional lights with high sample count!")]
	public bool directLightNoise;

	// Token: 0x04000EBB RID: 3771
	[Range(0f, 1f)]
	[Tooltip("The noise intensity volume lighting.")]
	public float noiseIntensity = 1f;

	// Token: 0x04000EBC RID: 3772
	[Tooltip("The noise intensity offset of volume lighting.")]
	[Range(0f, 1f)]
	public float noiseIntensityOffset = 0.3f;

	// Token: 0x04000EBD RID: 3773
	[Range(0f, 0.1f)]
	[Tooltip("The noise scaling of volume lighting.")]
	public float noiseScale = 0.001f;

	// Token: 0x04000EBE RID: 3774
	[Tooltip("The speed and direction of volume lighting.")]
	public Vector2 noiseVelocity = new Vector2(3f, 1.5f);
}
