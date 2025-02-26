using System;
using UnityEngine;

// Token: 0x02000362 RID: 866
[Serializable]
public class EnviroWeatherCloudsConfig
{
	// Token: 0x0400108B RID: 4235
	[Tooltip("Top color of clouds.")]
	public Color topColor = Color.white;

	// Token: 0x0400108C RID: 4236
	[Tooltip("Bottom color of clouds.")]
	public Color bottomColor = Color.grey;

	// Token: 0x0400108D RID: 4237
	[Tooltip("Sky blend factor.")]
	[Range(0f, 1f)]
	public float skyBlending = 1f;

	// Token: 0x0400108E RID: 4238
	[Tooltip("Light inscattering factor.")]
	[Range(0f, 2f)]
	public float alphaCoef = 1f;

	// Token: 0x0400108F RID: 4239
	[Tooltip("Light extinction factor.")]
	[Range(0f, 2f)]
	public float scatteringCoef = 1f;

	// Token: 0x04001090 RID: 4240
	[Tooltip("Density factor of clouds.")]
	[Range(0f, 1f)]
	public float density = 1f;

	// Token: 0x04001091 RID: 4241
	[Tooltip("Global coverage multiplicator of clouds.")]
	[Range(0f, 1f)]
	public float coverage = 1f;

	// Token: 0x04001092 RID: 4242
	[Tooltip("Global coverage height multiplicator of clouds.")]
	[Range(0f, 1f)]
	public float coverageHeight = 1f;

	// Token: 0x04001093 RID: 4243
	[Tooltip("Clouds raynarching step modifier.")]
	[Range(0.25f, 1f)]
	public float raymarchingScale = 1f;

	// Token: 0x04001094 RID: 4244
	[Tooltip("Clouds modelling type.")]
	[Range(0f, 1f)]
	public float cloudType = 1f;

	// Token: 0x04001095 RID: 4245
	[Tooltip("Cirrus Clouds Alpha")]
	[Range(0f, 1f)]
	public float cirrusAlpha;

	// Token: 0x04001096 RID: 4246
	[Tooltip("Cirrus Clouds Coverage")]
	[Range(0f, 1f)]
	public float cirrusCoverage;

	// Token: 0x04001097 RID: 4247
	[Tooltip("Cirrus Clouds Color Power")]
	[Range(0f, 1f)]
	public float cirrusColorPow = 2f;

	// Token: 0x04001098 RID: 4248
	[Tooltip("Flat Clouds Alpha")]
	[Range(0f, 1f)]
	public float flatAlpha;

	// Token: 0x04001099 RID: 4249
	[Tooltip("Flat Clouds Coverage")]
	[Range(0f, 1f)]
	public float flatCoverage;

	// Token: 0x0400109A RID: 4250
	[Tooltip("Flat Clouds Softness")]
	[Range(0f, 1f)]
	public float flatSoftness = 0.75f;

	// Token: 0x0400109B RID: 4251
	[Tooltip("Flat Clouds Brightness")]
	[Range(0f, 1f)]
	public float flatBrightness = 0.75f;

	// Token: 0x0400109C RID: 4252
	[Tooltip("Flat Clouds Color Power")]
	[Range(0f, 1f)]
	public float flatColorPow = 2f;
}
