using System;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007D0 RID: 2000
	[DisallowMultipleComponent]
	public class CmpFace : CmpBase
	{
		// Token: 0x06003178 RID: 12664 RVA: 0x00125DFD File Offset: 0x001241FD
		public CmpFace() : base(false)
		{
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x00125E1C File Offset: 0x0012421C
		public override void SetReferenceObject()
		{
			FindAssist findAssist = new FindAssist();
			findAssist.Initialize(base.transform);
			this.targetCustom.rendEyes = new Renderer[2];
			GameObject objectFromName = findAssist.GetObjectFromName("o_eyebase_L");
			if (null != objectFromName)
			{
				this.targetCustom.rendEyes[0] = objectFromName.GetComponent<Renderer>();
			}
			objectFromName = findAssist.GetObjectFromName("o_eyebase_R");
			if (null != objectFromName)
			{
				this.targetCustom.rendEyes[1] = objectFromName.GetComponent<Renderer>();
			}
			objectFromName = findAssist.GetObjectFromName("o_eyelashes");
			if (null != objectFromName)
			{
				this.targetCustom.rendEyelashes = objectFromName.GetComponent<Renderer>();
			}
			objectFromName = findAssist.GetObjectFromName("o_eyeshadow");
			if (null != objectFromName)
			{
				this.targetCustom.rendShadow = objectFromName.GetComponent<Renderer>();
			}
			objectFromName = findAssist.GetObjectFromName("o_head");
			if (null != objectFromName)
			{
				this.targetCustom.rendHead = objectFromName.GetComponent<Renderer>();
			}
			objectFromName = findAssist.GetObjectFromName("o_namida");
			if (null != objectFromName)
			{
				this.targetEtc.rendTears = objectFromName.GetComponent<Renderer>();
			}
			this.targetEtc.objTongue = findAssist.GetObjectFromName("o_tang");
		}

		// Token: 0x04002FAE RID: 12206
		[Header("カスタムで使用")]
		public CmpFace.TargetCustom targetCustom = new CmpFace.TargetCustom();

		// Token: 0x04002FAF RID: 12207
		[Header("その他")]
		public CmpFace.TargetEtc targetEtc = new CmpFace.TargetEtc();

		// Token: 0x020007D1 RID: 2001
		[Serializable]
		public class TargetCustom
		{
			// Token: 0x04002FB0 RID: 12208
			public Renderer[] rendEyes;

			// Token: 0x04002FB1 RID: 12209
			public Renderer rendEyelashes;

			// Token: 0x04002FB2 RID: 12210
			public Renderer rendShadow;

			// Token: 0x04002FB3 RID: 12211
			public Renderer rendHead;
		}

		// Token: 0x020007D2 RID: 2002
		[Serializable]
		public class TargetEtc
		{
			// Token: 0x04002FB4 RID: 12212
			public Renderer rendTears;

			// Token: 0x04002FB5 RID: 12213
			public GameObject objTongue;
		}
	}
}
