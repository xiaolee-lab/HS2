using System;
using UnityEngine;

// Token: 0x02000BAB RID: 2987
[Serializable]
public class NeckObjectVer3
{
	// Token: 0x040051E9 RID: 20969
	public string name;

	// Token: 0x040051EA RID: 20970
	[Tooltip("計算参照オブジェクト こいつから計算している")]
	public Transform m_referenceCalc;

	// Token: 0x040051EB RID: 20971
	[Tooltip("実際動かすオブジェクト")]
	public Transform neckBone;

	// Token: 0x040051EC RID: 20972
	[Tooltip("リングオブジェクト")]
	public Transform controlBone;

	// Token: 0x040051ED RID: 20973
	[SerializeField]
	[Tooltip("デバッグ用表示")]
	internal Quaternion fixAngle;

	// Token: 0x040051EE RID: 20974
	[SerializeField]
	[Tooltip("デバッグ用表示")]
	internal float angleHRate;

	// Token: 0x040051EF RID: 20975
	[SerializeField]
	[Tooltip("デバッグ用表示")]
	internal float angleVRate;

	// Token: 0x040051F0 RID: 20976
	[SerializeField]
	[Tooltip("デバッグ用表示")]
	internal float angleH;

	// Token: 0x040051F1 RID: 20977
	[SerializeField]
	[Tooltip("デバッグ用表示")]
	internal float angleV;

	// Token: 0x040051F2 RID: 20978
	internal Quaternion fixAngleBackup;

	// Token: 0x040051F3 RID: 20979
	internal Quaternion backupLocalRotationByTarget;

	// Token: 0x040051F4 RID: 20980
	internal Transform referenceCalc;

	// Token: 0x040051F5 RID: 20981
	internal Quaternion referenceCalcWorldRotation;

	// Token: 0x040051F6 RID: 20982
	internal Quaternion referenceCalcLocalRotation;

	// Token: 0x040051F7 RID: 20983
	internal Quaternion neckBoneWorldRotation;

	// Token: 0x040051F8 RID: 20984
	internal Quaternion neckBoneLocalRotation;
}
