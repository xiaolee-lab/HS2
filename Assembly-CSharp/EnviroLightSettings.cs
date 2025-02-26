using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000335 RID: 821
[Serializable]
public class EnviroLightSettings
{
	// Token: 0x04000E91 RID: 3729
	[Header("Direct")]
	[Tooltip("Color gradient for sun and moon light based on sun position in sky.")]
	public Gradient LightColor;

	// Token: 0x04000E92 RID: 3730
	[Tooltip("Direct light sun intensity based on sun position in sky")]
	public AnimationCurve directLightSunIntensity = new AnimationCurve();

	// Token: 0x04000E93 RID: 3731
	[Tooltip("Direct light moon intensity based on moon position in sky")]
	public AnimationCurve directLightMoonIntensity = new AnimationCurve();

	// Token: 0x04000E94 RID: 3732
	[Tooltip("Realtime shadow strength of the directional light.")]
	public AnimationCurve shadowIntensity = new AnimationCurve();

	// Token: 0x04000E95 RID: 3733
	[Tooltip("Direct lighting y-offset.")]
	[Range(0f, 5000f)]
	public float directLightAngleOffset;

	// Token: 0x04000E96 RID: 3734
	[Header("Ambient")]
	[Tooltip("Ambient Rendering Mode.")]
	public AmbientMode ambientMode = AmbientMode.Flat;

	// Token: 0x04000E97 RID: 3735
	[Tooltip("Ambientlight intensity based on sun position in sky.")]
	public AnimationCurve ambientIntensity = new AnimationCurve();

	// Token: 0x04000E98 RID: 3736
	[Tooltip("Ambientlight sky color based on sun position in sky.")]
	public Gradient ambientSkyColor;

	// Token: 0x04000E99 RID: 3737
	[Tooltip("Ambientlight Equator color based on sun position in sky.")]
	public Gradient ambientEquatorColor;

	// Token: 0x04000E9A RID: 3738
	[Tooltip("Ambientlight Ground color based on sun position in sky.")]
	public Gradient ambientGroundColor;

	// Token: 0x04000E9B RID: 3739
	[Header("Global Reflections")]
	[Tooltip("Enable/disable enviro reflection probe..")]
	public bool globalReflections = true;

	// Token: 0x04000E9C RID: 3740
	[Tooltip("Reflection probe intensity.")]
	public float globalReflectionsIntensity = 0.5f;

	// Token: 0x04000E9D RID: 3741
	[Tooltip("Reflection probe update rate.")]
	public float globalReflectionsUpdate = 0.025f;

	// Token: 0x04000E9E RID: 3742
	[Tooltip("Reflection probe intensity.")]
	[Range(0.1f, 10f)]
	public float globalReflectionsScale = 1f;
}
