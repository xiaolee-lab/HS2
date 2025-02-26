using System;
using UnityEngine;

// Token: 0x02000BAD RID: 2989
[Serializable]
public class NeckTypeStateVer3
{
	// Token: 0x040051FE RID: 20990
	public string name;

	// Token: 0x040051FF RID: 20991
	[Tooltip("各骨のパラメーター")]
	public NeckLimitParameterVer3[] aParam;

	// Token: 0x04005200 RID: 20992
	[Range(0f, 100f)]
	[Tooltip("補間速度")]
	public float leapSpeed = 2.5f;

	// Token: 0x04005201 RID: 20993
	[Range(0f, 180f)]
	[Tooltip("視野角(正面からどの範囲までターゲットを追うか)水色線")]
	public float hAngleLimit = 110f;

	// Token: 0x04005202 RID: 20994
	[Range(0f, 180f)]
	[Tooltip("視野角(正面からどの範囲までターゲットを追うか)水色線")]
	public float vAngleLimit = 80f;

	// Token: 0x04005203 RID: 20995
	[Range(0f, 180f)]
	[Tooltip("視野角から離脱するまでの補間値(黄色線 黄色線を超えると離脱する)")]
	public float limitBreakCorrectionValue;

	// Token: 0x04005204 RID: 20996
	[Range(0f, 180f)]
	[Tooltip("逸らす時に逆を向くための範囲\n[首が向ける最大角度からの]\n紫線")]
	public float limitAway = 20f;

	// Token: 0x04005205 RID: 20997
	[SerializeField]
	[Tooltip("デバッグ用表示")]
	internal bool isLimitBreakBackup;

	// Token: 0x04005206 RID: 20998
	public NECK_LOOK_TYPE_VER3 lookType = NECK_LOOK_TYPE_VER3.TARGET;
}
