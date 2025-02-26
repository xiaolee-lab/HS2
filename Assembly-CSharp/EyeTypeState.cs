using System;
using UnityEngine;

// Token: 0x020010DF RID: 4319
[Serializable]
public class EyeTypeState
{
	// Token: 0x0400740D RID: 29709
	[Range(-10f, 10f)]
	public float thresholdAngleDifference;

	// Token: 0x0400740E RID: 29710
	[Range(0f, 10f)]
	public float bendingMultiplier = 0.4f;

	// Token: 0x0400740F RID: 29711
	[Range(0f, 100f)]
	public float maxAngleDifference = 10f;

	// Token: 0x04007410 RID: 29712
	[Range(-100f, 0f)]
	public float upBendingAngle = -1f;

	// Token: 0x04007411 RID: 29713
	[Range(0f, 100f)]
	public float downBendingAngle = 6f;

	// Token: 0x04007412 RID: 29714
	[Range(-100f, 0f)]
	public float minBendingAngle = -6f;

	// Token: 0x04007413 RID: 29715
	[Range(0f, 100f)]
	public float maxBendingAngle = 6f;

	// Token: 0x04007414 RID: 29716
	[Range(0f, 100f)]
	public float leapSpeed = 2.5f;

	// Token: 0x04007415 RID: 29717
	[Range(0f, 100f)]
	public float forntTagDis = 50f;

	// Token: 0x04007416 RID: 29718
	[Range(0f, 100f)]
	public float nearDis = 2f;

	// Token: 0x04007417 RID: 29719
	[Range(0f, 180f)]
	public float hAngleLimit = 110f;

	// Token: 0x04007418 RID: 29720
	[Range(0f, 180f)]
	public float vAngleLimit = 80f;

	// Token: 0x04007419 RID: 29721
	public EYE_LOOK_TYPE lookType = EYE_LOOK_TYPE.TARGET;
}
