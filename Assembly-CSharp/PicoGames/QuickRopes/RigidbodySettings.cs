using System;
using UnityEngine;

namespace PicoGames.QuickRopes
{
	// Token: 0x02000A6B RID: 2667
	[Serializable]
	public struct RigidbodySettings
	{
		// Token: 0x04004808 RID: 18440
		[Min(0.001f)]
		public float mass;

		// Token: 0x04004809 RID: 18441
		[Min(0f)]
		public float drag;

		// Token: 0x0400480A RID: 18442
		[Min(0f)]
		public float angularDrag;

		// Token: 0x0400480B RID: 18443
		public bool useGravity;

		// Token: 0x0400480C RID: 18444
		public bool isKinematic;

		// Token: 0x0400480D RID: 18445
		public RigidbodyInterpolation interpolate;

		// Token: 0x0400480E RID: 18446
		public CollisionDetectionMode collisionDetection;

		// Token: 0x0400480F RID: 18447
		[SerializeField]
		public RigidbodyConstraints constraints;

		// Token: 0x04004810 RID: 18448
		[Range(6f, 100f)]
		public int solverCount;
	}
}
