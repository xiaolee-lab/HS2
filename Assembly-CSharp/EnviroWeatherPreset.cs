using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000364 RID: 868
[Serializable]
public class EnviroWeatherPreset : ScriptableObject
{
	// Token: 0x040010A0 RID: 4256
	public string version;

	// Token: 0x040010A1 RID: 4257
	public string Name;

	// Token: 0x040010A2 RID: 4258
	[Header("Season Settings")]
	public bool Spring = true;

	// Token: 0x040010A3 RID: 4259
	[Range(1f, 100f)]
	public float possibiltyInSpring = 50f;

	// Token: 0x040010A4 RID: 4260
	public bool Summer = true;

	// Token: 0x040010A5 RID: 4261
	[Range(1f, 100f)]
	public float possibiltyInSummer = 50f;

	// Token: 0x040010A6 RID: 4262
	public bool Autumn = true;

	// Token: 0x040010A7 RID: 4263
	[Range(1f, 100f)]
	public float possibiltyInAutumn = 50f;

	// Token: 0x040010A8 RID: 4264
	public bool winter = true;

	// Token: 0x040010A9 RID: 4265
	[Range(1f, 100f)]
	public float possibiltyInWinter = 50f;

	// Token: 0x040010AA RID: 4266
	[Header("Cloud Settings")]
	public EnviroWeatherCloudsConfig cloudsConfig;

	// Token: 0x040010AB RID: 4267
	[Header("Linear Fog")]
	public float fogStartDistance;

	// Token: 0x040010AC RID: 4268
	public float fogDistance = 1000f;

	// Token: 0x040010AD RID: 4269
	[Header("Exp Fog")]
	public float fogDensity = 0.0001f;

	// Token: 0x040010AE RID: 4270
	[Tooltip("Used to modify sky, direct, ambient light and fog color. The color alpha value defines the intensity")]
	public Gradient weatherSkyMod;

	// Token: 0x040010AF RID: 4271
	public Gradient weatherLightMod;

	// Token: 0x040010B0 RID: 4272
	public Gradient weatherFogMod;

	// Token: 0x040010B1 RID: 4273
	[Range(0f, 2f)]
	public float volumeLightIntensity = 1f;

	// Token: 0x040010B2 RID: 4274
	[Range(0f, 100f)]
	[Tooltip("The density of height based fog for this weather.")]
	public float heightFogDensity = 1f;

	// Token: 0x040010B3 RID: 4275
	[Range(0f, 2f)]
	[Tooltip("Define the height of fog rendered in sky.")]
	public float SkyFogHeight = 0.5f;

	// Token: 0x040010B4 RID: 4276
	[Tooltip("Define the intensity of fog rendered in sky.")]
	[Range(0f, 2f)]
	public float SkyFogIntensity = 1f;

	// Token: 0x040010B5 RID: 4277
	[Range(1f, 10f)]
	[Tooltip("Define the scattering intensity of fog.")]
	public float FogScatteringIntensity = 1f;

	// Token: 0x040010B6 RID: 4278
	[Range(0f, 1f)]
	[Tooltip("Block the sundisk with fog.")]
	public float fogSunBlocking = 0.25f;

	// Token: 0x040010B7 RID: 4279
	[Header("Weather Settings")]
	public List<EnviroWeatherEffects> effectSystems = new List<EnviroWeatherEffects>();

	// Token: 0x040010B8 RID: 4280
	[Range(0f, 1f)]
	[Tooltip("Wind intensity that will applied to wind zone.")]
	public float WindStrenght = 0.5f;

	// Token: 0x040010B9 RID: 4281
	[Range(0f, 1f)]
	[Tooltip("The maximum wetness level that can be reached.")]
	public float wetnessLevel;

	// Token: 0x040010BA RID: 4282
	[Range(0f, 1f)]
	[Tooltip("The maximum snow level that can be reached.")]
	public float snowLevel;

	// Token: 0x040010BB RID: 4283
	[Tooltip("Activate this to enable thunder and lightning.")]
	public bool isLightningStorm;

	// Token: 0x040010BC RID: 4284
	[Range(0f, 2f)]
	[Tooltip("The Intervall of lightning in seconds. Random(lightningInterval,lightningInterval * 2). ")]
	public float lightningInterval = 10f;

	// Token: 0x040010BD RID: 4285
	[Header("Audio Settings - SFX")]
	[Tooltip("Define an sound effect for this weather preset.")]
	public AudioClip weatherSFX;

	// Token: 0x040010BE RID: 4286
	[Header("Audio Settings - Ambient")]
	[Tooltip("This sound wil be played in spring at day.(looped)")]
	public AudioClip SpringDayAmbient;

	// Token: 0x040010BF RID: 4287
	[Tooltip("This sound wil be played in spring at night.(looped)")]
	public AudioClip SpringNightAmbient;

	// Token: 0x040010C0 RID: 4288
	[Tooltip("This sound wil be played in summer at day.(looped)")]
	public AudioClip SummerDayAmbient;

	// Token: 0x040010C1 RID: 4289
	[Tooltip("This sound wil be played in summer at night.(looped)")]
	public AudioClip SummerNightAmbient;

	// Token: 0x040010C2 RID: 4290
	[Tooltip("This sound wil be played in autumn at day.(looped)")]
	public AudioClip AutumnDayAmbient;

	// Token: 0x040010C3 RID: 4291
	[Tooltip("This sound wil be played in autumn at night.(looped)")]
	public AudioClip AutumnNightAmbient;

	// Token: 0x040010C4 RID: 4292
	[Tooltip("This sound wil be played in winter at day.(looped)")]
	public AudioClip WinterDayAmbient;

	// Token: 0x040010C5 RID: 4293
	[Tooltip("This sound wil be played in winter at night.(looped)")]
	public AudioClip WinterNightAmbient;
}
