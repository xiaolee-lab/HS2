using System;
using UnityEngine;

// Token: 0x020010EA RID: 4330
[Serializable]
public class NeckTypeStateVer2
{
	// Token: 0x04007483 RID: 29827
	public string name;

	// Token: 0x04007484 RID: 29828
	[Tooltip("各骨のパラメーター")]
	public NeckLimitParameter[] aParam;

	// Token: 0x04007485 RID: 29829
	[Range(0f, 100f)]
	[Tooltip("補間速度")]
	public float leapSpeed = 2.5f;

	// Token: 0x04007486 RID: 29830
	[Range(0f, 180f)]
	[Tooltip("視野角(正面からどの範囲までターゲットを追うか)水色線")]
	public float hAngleLimit = 110f;

	// Token: 0x04007487 RID: 29831
	[Range(0f, 180f)]
	[Tooltip("視野角(正面からどの範囲までターゲットを追うか)水色線")]
	public float vAngleLimit = 80f;

	// Token: 0x04007488 RID: 29832
	[Range(0f, 180f)]
	[Tooltip("視野角から離脱するまでの補正値(黄色線 黄色線を超えると離脱する)")]
	public float limitBreakCorrectionValue;

	// Token: 0x04007489 RID: 29833
	[Range(0f, 180f)]
	[Tooltip("逸らすときに逆を向くための範囲\n[首が向ける最大角度からの]\n紫線")]
	public float limitAway = 20f;

	// Token: 0x0400748A RID: 29834
	[SerializeField]
	[Tooltip("デバッグ用表示")]
	internal bool isLimitBreakBackup;

	// Token: 0x0400748B RID: 29835
	public NECK_LOOK_TYPE_VER2 lookType = NECK_LOOK_TYPE_VER2.TARGET;
}
