using System;
using UnityEngine;

namespace LuxWater
{
	// Token: 0x020003EA RID: 1002
	public class LuxWater_SetMeshBounds : MonoBehaviour
	{
		// Token: 0x060011CF RID: 4559 RVA: 0x0006A2F4 File Offset: 0x000686F4
		private void OnEnable()
		{
			Mesh sharedMesh = base.GetComponent<MeshFilter>().sharedMesh;
			sharedMesh.RecalculateBounds();
			Bounds bounds = sharedMesh.bounds;
			bounds.Expand(new Vector3(this.Expand_XZ, this.Expand_Y, this.Expand_XZ));
			sharedMesh.bounds = bounds;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0006A340 File Offset: 0x00068740
		private void OnDisable()
		{
			Mesh sharedMesh = base.GetComponent<MeshFilter>().sharedMesh;
			sharedMesh.RecalculateBounds();
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0006A360 File Offset: 0x00068760
		private void OnValidate()
		{
			Mesh sharedMesh = base.GetComponent<MeshFilter>().sharedMesh;
			sharedMesh.RecalculateBounds();
			Bounds bounds = sharedMesh.bounds;
			bounds.Expand(new Vector3(this.Expand_XZ, this.Expand_Y, this.Expand_XZ));
			sharedMesh.bounds = bounds;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0006A3AC File Offset: 0x000687AC
		private void OnDrawGizmosSelected()
		{
			this.rend = base.GetComponent<Renderer>();
			Vector3 center = this.rend.bounds.center;
			Vector3 extents = this.rend.bounds.extents;
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(center, this.rend.bounds.size);
		}

		// Token: 0x04001401 RID: 5121
		[Space(6f)]
		[LuxWater_HelpBtn("h.s0d0kaaphhix")]
		public float Expand_XZ;

		// Token: 0x04001402 RID: 5122
		public float Expand_Y;

		// Token: 0x04001403 RID: 5123
		private Renderer rend;
	}
}
