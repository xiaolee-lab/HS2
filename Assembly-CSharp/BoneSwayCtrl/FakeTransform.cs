using System;
using UnityEngine;

namespace BoneSwayCtrl
{
	// Token: 0x02001144 RID: 4420
	public class FakeTransform
	{
		// Token: 0x06009235 RID: 37429 RVA: 0x003CA4C7 File Offset: 0x003C88C7
		public FakeTransform()
		{
			this.Pos = Vector3.zero;
			this.Rot = Quaternion.identity;
			this.Scale = Vector3.one;
		}

		// Token: 0x06009236 RID: 37430 RVA: 0x003CA4F0 File Offset: 0x003C88F0
		public FakeTransform(FakeTransform _In)
		{
			this.Pos = _In.Pos;
			this.Rot = _In.Rot;
			this.Scale = _In.Scale;
		}

		// Token: 0x0400764D RID: 30285
		public Vector3 Pos;

		// Token: 0x0400764E RID: 30286
		public Quaternion Rot;

		// Token: 0x0400764F RID: 30287
		public Vector3 Scale;
	}
}
