using System;
using UnityEngine;

// Token: 0x02000345 RID: 837
[Serializable]
public class EnviroComponents
{
	// Token: 0x04000F3B RID: 3899
	[Tooltip("The Enviro sun object.")]
	public GameObject Sun;

	// Token: 0x04000F3C RID: 3900
	[Tooltip("The Enviro moon object.")]
	public GameObject Moon;

	// Token: 0x04000F3D RID: 3901
	[Tooltip("The directional light for direct sun and moon lighting.")]
	public Transform DirectLight;

	// Token: 0x04000F3E RID: 3902
	[Tooltip("The Enviro global reflection probe for dynamic reflections.")]
	public ReflectionProbe GlobalReflectionProbe;

	// Token: 0x04000F3F RID: 3903
	[Tooltip("Your WindZone that reflect our weather wind settings.")]
	public WindZone windZone;

	// Token: 0x04000F40 RID: 3904
	[Tooltip("The Enviro Lighting Flash Component.")]
	public EnviroLightning LightningGenerator;

	// Token: 0x04000F41 RID: 3905
	[Tooltip("Link to the object that hold all additional satellites as childs.")]
	public Transform satellites;

	// Token: 0x04000F42 RID: 3906
	[Tooltip("Just a transform for stars rotation calculations. ")]
	public Transform starsRotation;

	// Token: 0x04000F43 RID: 3907
	[Tooltip("Plane to cast cloud shadows.")]
	public GameObject cloudsShadowPlane;
}
