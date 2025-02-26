using System;
using UnityEngine;

// Token: 0x02000338 RID: 824
[Serializable]
public class EnviroLightShaftsSettings
{
	// Token: 0x04000EBF RID: 3775
	[Header("Quality Settings")]
	[Tooltip("Lightshafts resolution quality setting.")]
	public EnviroLightShafts.SunShaftsResolution resolution = EnviroLightShafts.SunShaftsResolution.Normal;

	// Token: 0x04000EC0 RID: 3776
	[Tooltip("Lightshafts blur mode.")]
	public EnviroLightShafts.ShaftsScreenBlendMode screenBlendMode;

	// Token: 0x04000EC1 RID: 3777
	[Tooltip("Use cameras depth to hide lightshafts?")]
	public bool useDepthTexture = true;

	// Token: 0x04000EC2 RID: 3778
	[Header("Intensity Settings")]
	[Tooltip("Color gradient for lightshafts based on sun position.")]
	public Gradient lightShaftsColorSun;

	// Token: 0x04000EC3 RID: 3779
	[Tooltip("Color gradient for lightshafts based on moon position.")]
	public Gradient lightShaftsColorMoon;

	// Token: 0x04000EC4 RID: 3780
	[Tooltip("Treshhold gradient for lightshafts based on sun position. This will influence lightshafts intensity!")]
	public Gradient thresholdColorSun;

	// Token: 0x04000EC5 RID: 3781
	[Tooltip("Treshhold gradient for lightshafts based on moon position. This will influence lightshafts intensity!")]
	public Gradient thresholdColorMoon;

	// Token: 0x04000EC6 RID: 3782
	[Tooltip("Radius of blurring applied.")]
	public float blurRadius = 6f;

	// Token: 0x04000EC7 RID: 3783
	[Tooltip("Global Lightshafts intensity.")]
	public float intensity = 0.6f;

	// Token: 0x04000EC8 RID: 3784
	[Tooltip("Lightshafts maximum radius.")]
	public float maxRadius = 10f;
}
