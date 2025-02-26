using System;
using UnityEngine;

namespace PicoGames.QuickRopes
{
	// Token: 0x02000A68 RID: 2664
	[Serializable]
	public struct JointSettings
	{
		// Token: 0x040047DE RID: 18398
		[SerializeField]
		[Min(0.001f)]
		public float breakForce;

		// Token: 0x040047DF RID: 18399
		[SerializeField]
		[Min(0.001f)]
		public float breakTorque;

		// Token: 0x040047E0 RID: 18400
		[SerializeField]
		[Range(0f, 180f)]
		public float twistLimit;

		// Token: 0x040047E1 RID: 18401
		[SerializeField]
		[Range(0f, 180f)]
		public float swingLimit;

		// Token: 0x040047E2 RID: 18402
		[SerializeField]
		[Min(0f)]
		public float spring;

		// Token: 0x040047E3 RID: 18403
		[SerializeField]
		[Min(0f)]
		public float damper;
	}
}
