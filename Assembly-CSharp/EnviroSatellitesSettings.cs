using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000334 RID: 820
[Serializable]
public class EnviroSatellitesSettings
{
	// Token: 0x04000E90 RID: 3728
	[Tooltip("List of satellites.")]
	public List<EnviroSatellite> additionalSatellites = new List<EnviroSatellite>();
}
