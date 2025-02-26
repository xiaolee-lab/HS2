using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000361 RID: 865
[Serializable]
public class EnviroWeatherPrefab : MonoBehaviour
{
	// Token: 0x04001088 RID: 4232
	public EnviroWeatherPreset weatherPreset;

	// Token: 0x04001089 RID: 4233
	[HideInInspector]
	public List<ParticleSystem> effectSystems = new List<ParticleSystem>();

	// Token: 0x0400108A RID: 4234
	[HideInInspector]
	public List<float> effectEmmisionRates = new List<float>();
}
