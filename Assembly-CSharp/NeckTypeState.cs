using System;
using UnityEngine;

// Token: 0x020010E5 RID: 4325
[Serializable]
public class NeckTypeState
{
	// Token: 0x0400744D RID: 29773
	[Range(-10f, 10f)]
	public float thresholdAngleDifference;

	// Token: 0x0400744E RID: 29774
	[Range(0f, 10f)]
	public float bendingMultiplier = 0.4f;

	// Token: 0x0400744F RID: 29775
	[Range(0f, 100f)]
	public float maxAngleDifference = 10f;

	// Token: 0x04007450 RID: 29776
	[Range(-100f, 0f)]
	public float upBendingAngle = -1f;

	// Token: 0x04007451 RID: 29777
	[Range(0f, 100f)]
	public float downBendingAngle = 6f;

	// Token: 0x04007452 RID: 29778
	[Range(-100f, 0f)]
	public float minBendingAngle = -6f;

	// Token: 0x04007453 RID: 29779
	[Range(0f, 100f)]
	public float maxBendingAngle = 6f;

	// Token: 0x04007454 RID: 29780
	[Range(0f, 100f)]
	public float leapSpeed = 2.5f;

	// Token: 0x04007455 RID: 29781
	[Range(0f, 100f)]
	public float forntTagDis = 50f;

	// Token: 0x04007456 RID: 29782
	[Range(0f, 100f)]
	public float nearDis = 2f;

	// Token: 0x04007457 RID: 29783
	[Range(0f, 180f)]
	public float hAngleLimit = 110f;

	// Token: 0x04007458 RID: 29784
	[Range(0f, 180f)]
	public float vAngleLimit = 80f;

	// Token: 0x04007459 RID: 29785
	public NECK_LOOK_TYPE lookType = NECK_LOOK_TYPE.TARGET;
}
