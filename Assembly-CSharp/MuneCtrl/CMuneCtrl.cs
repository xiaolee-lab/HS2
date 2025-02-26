using System;
using System.Collections.Generic;
using BoneSwayCtrl;
using UnityEngine;

namespace MuneCtrl
{
	// Token: 0x02001153 RID: 4435
	public class CMuneCtrl : MonoBehaviour
	{
		// Token: 0x060092AF RID: 37551 RVA: 0x003CD2F7 File Offset: 0x003CB6F7
		public bool Load(long nPtn, string strAsset, string strResource)
		{
			return true;
		}

		// Token: 0x040076A2 RID: 30370
		public BoneSway[] m_aMune = new BoneSway[2];

		// Token: 0x040076A3 RID: 30371
		private int m_nNumBone;

		// Token: 0x040076A4 RID: 30372
		private List<int> m_listNumLocater = new List<int>();

		// Token: 0x040076A5 RID: 30373
		private bool m_bSucceed;

		// Token: 0x040076A6 RID: 30374
		private CMuneParamCtrl m_ParamCtrl = new CMuneParamCtrl();

		// Token: 0x040076A7 RID: 30375
		private bool m_bAllocParam;

		// Token: 0x040076A8 RID: 30376
		private Vector2 m_Mouse = new Vector2(0f, 0f);

		// Token: 0x040076A9 RID: 30377
		private float m_ftime;

		// Token: 0x040076AA RID: 30378
		private int m_nCatchMode;

		// Token: 0x040076AB RID: 30379
		private Transform m_transRef;
	}
}
