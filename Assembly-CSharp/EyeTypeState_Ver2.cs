using System;
using UnityEngine;

// Token: 0x02000BA0 RID: 2976
[Serializable]
public class EyeTypeState_Ver2
{
	// Token: 0x04005157 RID: 20823
	[Range(-10f, 10f)]
	public float thresholdAngleDifference;

	// Token: 0x04005158 RID: 20824
	[Range(0f, 10f)]
	public float bendingMultiplier = 0.4f;

	// Token: 0x04005159 RID: 20825
	[Range(0f, 100f)]
	public float maxAngleDifference = 10f;

	// Token: 0x0400515A RID: 20826
	[Range(-100f, 0f)]
	[Tooltip("上を向く際の限界値")]
	public float upBendingAngle = -1f;

	// Token: 0x0400515B RID: 20827
	[Range(0f, 100f)]
	[Tooltip("下を向く際の限界値")]
	public float downBendingAngle = 6f;

	// Token: 0x0400515C RID: 20828
	[Range(-100f, 0f)]
	[Tooltip("内側を向く際の限界値")]
	public float inBendingAngle = -6f;

	// Token: 0x0400515D RID: 20829
	[Range(0f, 100f)]
	[Tooltip("外側を向く際の限界値")]
	public float outBendingAngle = 6f;

	// Token: 0x0400515E RID: 20830
	[Range(0f, 100f)]
	[Tooltip("補間速度")]
	public float leapSpeed = 2.5f;

	// Token: 0x0400515F RID: 20831
	[Range(0f, 100f)]
	[Tooltip("正面時のターゲットとの距離")]
	public float frontTagDis = 50f;

	// Token: 0x04005160 RID: 20832
	[Range(0f, 100f)]
	[Tooltip("近距離原価地位(より目対策)")]
	public float nearDis = 2f;

	// Token: 0x04005161 RID: 20833
	[Range(0f, 180f)]
	[Tooltip("視野角(水平)")]
	public float hAngleLimit = 110f;

	// Token: 0x04005162 RID: 20834
	[Range(0f, 180f)]
	[Tooltip("視野角(垂直)")]
	public float vAngleLimit = 80f;

	// Token: 0x04005163 RID: 20835
	public EYE_LOOK_TYPE_VER2 lookType = EYE_LOOK_TYPE_VER2.TARGET;
}
