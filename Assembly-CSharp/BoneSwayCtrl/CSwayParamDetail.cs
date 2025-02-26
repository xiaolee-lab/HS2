using System;
using UnityEngine;

namespace BoneSwayCtrl
{
	// Token: 0x02001148 RID: 4424
	[Serializable]
	public class CSwayParamDetail
	{
		// Token: 0x04007666 RID: 30310
		public Vector3 vLimitMaxT = Vector3.one;

		// Token: 0x04007667 RID: 30311
		public Vector3 vLimitMinT = Vector3.one;

		// Token: 0x04007668 RID: 30312
		public Vector3 vLimitMaxR = Vector3.one;

		// Token: 0x04007669 RID: 30313
		public Vector3 vLimitMinR = Vector3.one;

		// Token: 0x0400766A RID: 30314
		public Vector3 vAddR = Vector3.zero;

		// Token: 0x0400766B RID: 30315
		public Vector3 vAddT = Vector3.zero;

		// Token: 0x0400766C RID: 30316
		public Vector3 vAddS = Vector3.one;

		// Token: 0x0400766D RID: 30317
		public float fForceScale = 1f;

		// Token: 0x0400766E RID: 30318
		public float fForceLimit = 1f;

		// Token: 0x0400766F RID: 30319
		public float fInertiaScale;

		// Token: 0x04007670 RID: 30320
		public CSwayParamCalc Calc = new CSwayParamCalc();

		// Token: 0x04007671 RID: 30321
		public float fGravity;

		// Token: 0x04007672 RID: 30322
		public float fDrag;

		// Token: 0x04007673 RID: 30323
		public float fTension;

		// Token: 0x04007674 RID: 30324
		public float fShear;

		// Token: 0x04007675 RID: 30325
		public float fAttenuation;

		// Token: 0x04007676 RID: 30326
		public float fMass;

		// Token: 0x04007677 RID: 30327
		public float fCrushZMax;

		// Token: 0x04007678 RID: 30328
		public float fCrushZMin;

		// Token: 0x04007679 RID: 30329
		public float fCrushXYMax;

		// Token: 0x0400767A RID: 30330
		public float fCrushXYMin;

		// Token: 0x0400767B RID: 30331
		public bool bAutoRotProc;

		// Token: 0x0400767C RID: 30332
		public float fAutoRot;

		// Token: 0x0400767D RID: 30333
		public bool bAutoRotUp;
	}
}
