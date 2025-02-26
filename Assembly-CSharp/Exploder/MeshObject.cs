using System;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003A8 RID: 936
	internal struct MeshObject
	{
		// Token: 0x0400126C RID: 4716
		public ExploderMesh mesh;

		// Token: 0x0400126D RID: 4717
		public Material material;

		// Token: 0x0400126E RID: 4718
		public ExploderTransform transform;

		// Token: 0x0400126F RID: 4719
		public Transform parent;

		// Token: 0x04001270 RID: 4720
		public Vector3 position;

		// Token: 0x04001271 RID: 4721
		public Quaternion rotation;

		// Token: 0x04001272 RID: 4722
		public Vector3 localScale;

		// Token: 0x04001273 RID: 4723
		public GameObject original;

		// Token: 0x04001274 RID: 4724
		public ExploderOption option;

		// Token: 0x04001275 RID: 4725
		public GameObject skinnedOriginal;

		// Token: 0x04001276 RID: 4726
		public GameObject bakeObject;

		// Token: 0x04001277 RID: 4727
		public float distanceRatio;

		// Token: 0x04001278 RID: 4728
		public int id;
	}
}
