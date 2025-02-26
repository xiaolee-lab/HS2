using System;
using UnityEngine;

// Token: 0x020010E9 RID: 4329
[Serializable]
public class NeckLimitParameter
{
	// Token: 0x0400747E RID: 29822
	public string name;

	// Token: 0x0400747F RID: 29823
	[Range(-100f, 0f)]
	[Tooltip("上を向く際の限界値(値が大きくなればなるほど上を向けられる)")]
	public float upBendingAngle = -1f;

	// Token: 0x04007480 RID: 29824
	[Range(0f, 100f)]
	[Tooltip("下を向く際の限界値(値が大きくなればなるほど下を向けられる)")]
	public float downBendingAngle = 6f;

	// Token: 0x04007481 RID: 29825
	[Range(-100f, 0f)]
	[Tooltip("左側を向く際の限界値")]
	public float minBendingAngle = -6f;

	// Token: 0x04007482 RID: 29826
	[Range(0f, 100f)]
	[Tooltip("右側を向く際の限界値")]
	public float maxBendingAngle = 6f;
}
