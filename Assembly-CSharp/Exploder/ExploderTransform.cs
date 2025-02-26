using System;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003A2 RID: 930
	public struct ExploderTransform
	{
		// Token: 0x06001072 RID: 4210 RVA: 0x0005C471 File Offset: 0x0005A871
		public ExploderTransform(Transform unityTransform)
		{
			this.position = unityTransform.position;
			this.rotation = unityTransform.rotation;
			this.localScale = unityTransform.localScale;
			this.parent = unityTransform.parent;
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0005C4A3 File Offset: 0x0005A8A3
		public Vector3 InverseTransformDirection(Vector3 dir)
		{
			return Quaternion.Inverse(this.rotation) * dir;
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0005C4B8 File Offset: 0x0005A8B8
		public Vector3 InverseTransformPoint(Vector3 pnt)
		{
			Vector3 a = new Vector3(1f / this.localScale.x, 1f / this.localScale.y, 1f / this.localScale.z);
			return Vector3.Scale(a, Quaternion.Inverse(this.rotation) * (pnt - this.position));
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0005C524 File Offset: 0x0005A924
		public Vector3 TransformPoint(Vector3 pnt)
		{
			return Matrix4x4.TRS(this.position, this.rotation, this.localScale).MultiplyPoint3x4(pnt);
		}

		// Token: 0x04001245 RID: 4677
		public Vector3 position;

		// Token: 0x04001246 RID: 4678
		public Quaternion rotation;

		// Token: 0x04001247 RID: 4679
		public Vector3 localScale;

		// Token: 0x04001248 RID: 4680
		public Transform parent;
	}
}
