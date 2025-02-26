using System;
using UnityEngine;

// Token: 0x02000BAC RID: 2988
[Serializable]
public class NeckLimitParameterVer3
{
	// Token: 0x040051F9 RID: 20985
	public string name;

	// Token: 0x040051FA RID: 20986
	[Range(-100f, 0f)]
	[Tooltip("上を向く際の限界値(値が大きくなればなるほど上を向けられる)")]
	public float upBendingAngle = -1f;

	// Token: 0x040051FB RID: 20987
	[Range(0f, 100f)]
	[Tooltip("下を向く際の限界値(値が大きくなればなるほど下を向けられる)")]
	public float downBendingAngle = 6f;

	// Token: 0x040051FC RID: 20988
	[Range(-100f, 0f)]
	[Tooltip("左側を向く際の限界値")]
	public float leftBendingAngle = -6f;

	// Token: 0x040051FD RID: 20989
	[Range(0f, 100f)]
	[Tooltip("右側を向く際の限界値")]
	public float rightBendingAngle = 6f;
}
