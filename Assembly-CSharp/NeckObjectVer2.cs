using System;
using UnityEngine;

// Token: 0x020010E8 RID: 4328
[Serializable]
public class NeckObjectVer2
{
	// Token: 0x04007473 RID: 29811
	public string name;

	// Token: 0x04007474 RID: 29812
	[Tooltip("計算参照オブジェクト こいつから計算している")]
	public Transform referenceCalc;

	// Token: 0x04007475 RID: 29813
	[Tooltip("実際動かすオブジェクト")]
	public Transform neckBone;

	// Token: 0x04007476 RID: 29814
	[Tooltip("リングオブジェクト")]
	public Transform controlBone;

	// Token: 0x04007477 RID: 29815
	[SerializeField]
	[Tooltip("デバッグ用表示")]
	internal Quaternion fixAngle;

	// Token: 0x04007478 RID: 29816
	[SerializeField]
	[Tooltip("デバッグ用表示")]
	internal float angleHRate;

	// Token: 0x04007479 RID: 29817
	[SerializeField]
	[Tooltip("デバッグ用表示")]
	internal float angleVRate;

	// Token: 0x0400747A RID: 29818
	[SerializeField]
	[Tooltip("デバッグ用表示")]
	internal float angleH;

	// Token: 0x0400747B RID: 29819
	[SerializeField]
	[Tooltip("デバッグ用表示")]
	internal float angleV;

	// Token: 0x0400747C RID: 29820
	internal Quaternion fixAngleBackup;

	// Token: 0x0400747D RID: 29821
	internal Quaternion backupLocalRotaionByTarget;
}
