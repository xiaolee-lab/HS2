using System;
using UnityEngine;

namespace PicoGames.QuickRopes
{
	// Token: 0x02000A66 RID: 2662
	[Serializable]
	public struct ColliderSettings
	{
		// Token: 0x040047D3 RID: 18387
		[SerializeField]
		public QuickRope.ColliderType type;

		// Token: 0x040047D4 RID: 18388
		[SerializeField]
		public ColliderSettings.Direction direction;

		// Token: 0x040047D5 RID: 18389
		[SerializeField]
		public Vector3 center;

		// Token: 0x040047D6 RID: 18390
		[SerializeField]
		public Vector3 size;

		// Token: 0x040047D7 RID: 18391
		[SerializeField]
		[Min(0f)]
		public float radius;

		// Token: 0x040047D8 RID: 18392
		[SerializeField]
		[Min(0f)]
		public float height;

		// Token: 0x040047D9 RID: 18393
		[SerializeField]
		public PhysicMaterial physicsMaterial;

		// Token: 0x02000A67 RID: 2663
		public enum Direction
		{
			// Token: 0x040047DB RID: 18395
			X_Axis,
			// Token: 0x040047DC RID: 18396
			Y_Axis,
			// Token: 0x040047DD RID: 18397
			Z_Axis
		}
	}
}
