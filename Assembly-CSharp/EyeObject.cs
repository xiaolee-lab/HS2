using System;
using UnityEngine;

// Token: 0x020010DE RID: 4318
[Serializable]
public class EyeObject
{
	// Token: 0x04007405 RID: 29701
	public Transform eyeTransform;

	// Token: 0x04007406 RID: 29702
	public EYE_LR eyeLR;

	// Token: 0x04007407 RID: 29703
	internal float angleH;

	// Token: 0x04007408 RID: 29704
	internal float angleV;

	// Token: 0x04007409 RID: 29705
	internal Vector3 dirUp;

	// Token: 0x0400740A RID: 29706
	internal Vector3 referenceLookDir;

	// Token: 0x0400740B RID: 29707
	internal Vector3 referenceUpDir;

	// Token: 0x0400740C RID: 29708
	internal Quaternion origRotation;
}
