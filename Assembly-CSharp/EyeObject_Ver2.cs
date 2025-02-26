using System;
using UnityEngine;

// Token: 0x02000B9F RID: 2975
[Serializable]
public class EyeObject_Ver2
{
	// Token: 0x0400514D RID: 20813
	public Transform eyeTransform;

	// Token: 0x0400514E RID: 20814
	public EYE_LR_VER2 eyeLR;

	// Token: 0x0400514F RID: 20815
	internal float angleH;

	// Token: 0x04005150 RID: 20816
	internal float angleV;

	// Token: 0x04005151 RID: 20817
	internal Vector3 dirUp;

	// Token: 0x04005152 RID: 20818
	internal Vector3 referenceLookDir;

	// Token: 0x04005153 RID: 20819
	internal Vector3 referenceUpDir;

	// Token: 0x04005154 RID: 20820
	internal Quaternion origRotation;

	// Token: 0x04005155 RID: 20821
	internal Quaternion parentFirstWorldRotation;

	// Token: 0x04005156 RID: 20822
	internal Quaternion parentFirstLocalRotation;
}
