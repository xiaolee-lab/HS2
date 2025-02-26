using System;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007C6 RID: 1990
	[DisallowMultipleComponent]
	public class CmpBody : CmpBase
	{
		// Token: 0x0600315F RID: 12639 RVA: 0x00124E39 File Offset: 0x00123239
		public CmpBody() : base(false)
		{
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x00124E58 File Offset: 0x00123258
		public override void SetReferenceObject()
		{
			FindAssist findAssist = new FindAssist();
			findAssist.Initialize(base.transform);
			this.targetEtc.objBody = findAssist.GetObjectFromName("o_body_cm");
			if (null == this.targetEtc.objBody)
			{
				this.targetEtc.objBody = findAssist.GetObjectFromName("o_body_cf");
			}
			if (null == this.targetEtc.objBody)
			{
				this.targetEtc.objBody = findAssist.GetObjectFromName("o_silhouette_cm");
			}
			if (null == this.targetEtc.objBody)
			{
				this.targetEtc.objBody = findAssist.GetObjectFromName("o_silhouette_cf");
			}
			if (null != this.targetEtc.objBody)
			{
				this.targetCustom.rendBody = this.targetEtc.objBody.GetComponent<Renderer>();
			}
			this.targetEtc.objDanTop = findAssist.GetObjectFromName("N_dan");
			this.targetEtc.objDanTama = findAssist.GetObjectFromName("cm_o_dan_f");
			this.targetEtc.objDanSao = findAssist.GetObjectFromName("cm_o_dan00");
			this.targetEtc.objTongue = findAssist.GetObjectFromName("N_tang");
			if (null != this.targetEtc.objTongue)
			{
				this.targetEtc.rendTongue = this.targetEtc.objTongue.GetComponentInChildren<Renderer>();
			}
			this.targetEtc.objMNPB = findAssist.GetObjectFromName("N_mnpb");
		}

		// Token: 0x04002F30 RID: 12080
		[Header("カスタムで使用")]
		public CmpBody.TargetCustom targetCustom = new CmpBody.TargetCustom();

		// Token: 0x04002F31 RID: 12081
		[Header("その他")]
		public CmpBody.TargetEtc targetEtc = new CmpBody.TargetEtc();

		// Token: 0x020007C7 RID: 1991
		[Serializable]
		public class TargetCustom
		{
			// Token: 0x04002F32 RID: 12082
			public Renderer rendBody;
		}

		// Token: 0x020007C8 RID: 1992
		[Serializable]
		public class TargetEtc
		{
			// Token: 0x04002F33 RID: 12083
			public GameObject objBody;

			// Token: 0x04002F34 RID: 12084
			public GameObject objDanTop;

			// Token: 0x04002F35 RID: 12085
			public GameObject objDanTama;

			// Token: 0x04002F36 RID: 12086
			public GameObject objDanSao;

			// Token: 0x04002F37 RID: 12087
			public GameObject objTongue;

			// Token: 0x04002F38 RID: 12088
			public GameObject objMNPB;

			// Token: 0x04002F39 RID: 12089
			public Renderer rendTongue;
		}
	}
}
