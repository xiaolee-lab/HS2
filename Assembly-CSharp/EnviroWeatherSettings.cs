using System;
using UnityEngine;

// Token: 0x0200032F RID: 815
[Serializable]
public class EnviroWeatherSettings
{
	// Token: 0x04000E58 RID: 3672
	[Header("Zones Setup:")]
	[Tooltip("Tag for zone triggers. Create and assign a tag to this gameObject")]
	public bool useTag;

	// Token: 0x04000E59 RID: 3673
	[Header("Weather Transition Settings:")]
	[Tooltip("Defines the speed of wetness will raise when it is raining.")]
	public float wetnessAccumulationSpeed = 0.05f;

	// Token: 0x04000E5A RID: 3674
	[Tooltip("Defines the speed of wetness will dry when it is not raining.")]
	public float wetnessDryingSpeed = 0.05f;

	// Token: 0x04000E5B RID: 3675
	[Tooltip("Defines the speed of snow will raise when it is snowing.")]
	public float snowAccumulationSpeed = 0.05f;

	// Token: 0x04000E5C RID: 3676
	[Tooltip("Defines the speed of snow will meld when it is not snowing.")]
	public float snowMeltingSpeed = 0.05f;

	// Token: 0x04000E5D RID: 3677
	[Tooltip("Defines the speed of clouds will change when weather conditions changed.")]
	public float cloudTransitionSpeed = 1f;

	// Token: 0x04000E5E RID: 3678
	[Tooltip("Defines the speed of fog will change when weather conditions changed.")]
	public float fogTransitionSpeed = 1f;

	// Token: 0x04000E5F RID: 3679
	[Tooltip("Defines the speed of particle effects will change when weather conditions changed.")]
	public float effectTransitionSpeed = 1f;

	// Token: 0x04000E60 RID: 3680
	[Tooltip("Defines the speed of sfx will fade in and out when weather conditions changed.")]
	public float audioTransitionSpeed = 0.1f;

	// Token: 0x04000E61 RID: 3681
	[Header("Lightning Effect:")]
	public GameObject lightningEffect;

	// Token: 0x04000E62 RID: 3682
	[Range(500f, 10000f)]
	public float lightningRange = 500f;

	// Token: 0x04000E63 RID: 3683
	[Range(500f, 5000f)]
	public float lightningHeight = 750f;
}
