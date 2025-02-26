using System;
using UnityEngine;

// Token: 0x020010E4 RID: 4324
[Serializable]
public class NeckObject
{
	// Token: 0x04007446 RID: 29766
	public Transform neckTransform;

	// Token: 0x04007447 RID: 29767
	internal float angleH;

	// Token: 0x04007448 RID: 29768
	internal float angleV;

	// Token: 0x04007449 RID: 29769
	internal Vector3 dirUp;

	// Token: 0x0400744A RID: 29770
	internal Vector3 referenceLookDir;

	// Token: 0x0400744B RID: 29771
	internal Vector3 referenceUpDir;

	// Token: 0x0400744C RID: 29772
	internal Quaternion origRotation;
}
