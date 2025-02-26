using System;
using UnityEngine;

// Token: 0x0200032C RID: 812
[Serializable]
public class EnviroQualitySettings
{
	// Token: 0x04000E4D RID: 3661
	[Range(0f, 1f)]
	[Tooltip("Modifies the amount of particles used in weather effects.")]
	public float GlobalParticleEmissionRates = 1f;

	// Token: 0x04000E4E RID: 3662
	[Tooltip("How often Enviro Growth Instances should be updated. Lower value = smoother growth and more frequent updates but more perfomance hungry!")]
	public float UpdateInterval = 0.5f;
}
