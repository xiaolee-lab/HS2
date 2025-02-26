using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003A5 RID: 933
	public class ExploderMesh
	{
		// Token: 0x0600107D RID: 4221 RVA: 0x0005C6B2 File Offset: 0x0005AAB2
		public ExploderMesh()
		{
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0005C6BC File Offset: 0x0005AABC
		public ExploderMesh(Mesh unityMesh)
		{
			this.triangles = unityMesh.triangles;
			this.vertices = unityMesh.vertices;
			this.normals = unityMesh.normals;
			this.uv = unityMesh.uv;
			this.tangents = unityMesh.tangents;
			this.colors32 = unityMesh.colors32;
			ExploderMesh.CalculateCentroid(new List<Vector3>(this.vertices), ref this.centroid, ref this.min, ref this.max);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0005C73C File Offset: 0x0005AB3C
		public static void CalculateCentroid(List<Vector3> vertices, ref Vector3 ctr, ref Vector3 min, ref Vector3 max)
		{
			ctr = Vector3.zero;
			int count = vertices.Count;
			min.Set(float.MaxValue, float.MaxValue, float.MaxValue);
			max.Set(float.MinValue, float.MinValue, float.MinValue);
			for (int i = 0; i < count; i++)
			{
				if (min.x > vertices[i].x)
				{
					min.x = vertices[i].x;
				}
				if (min.y > vertices[i].y)
				{
					min.y = vertices[i].y;
				}
				if (min.z > vertices[i].z)
				{
					min.z = vertices[i].z;
				}
				if (max.x < vertices[i].x)
				{
					max.x = vertices[i].x;
				}
				if (max.y < vertices[i].y)
				{
					max.y = vertices[i].y;
				}
				if (max.z < vertices[i].z)
				{
					max.z = vertices[i].z;
				}
				ctr += vertices[i];
			}
			ctr /= (float)count;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0005C8E8 File Offset: 0x0005ACE8
		public Mesh ToUnityMesh()
		{
			return new Mesh
			{
				vertices = this.vertices,
				normals = this.normals,
				uv = this.uv,
				tangents = this.tangents,
				colors32 = this.colors32,
				triangles = this.triangles
			};
		}

		// Token: 0x0400124C RID: 4684
		public int[] triangles;

		// Token: 0x0400124D RID: 4685
		public Vector3[] vertices;

		// Token: 0x0400124E RID: 4686
		public Vector3[] normals;

		// Token: 0x0400124F RID: 4687
		public Vector2[] uv;

		// Token: 0x04001250 RID: 4688
		public Vector4[] tangents;

		// Token: 0x04001251 RID: 4689
		public Color32[] colors32;

		// Token: 0x04001252 RID: 4690
		public Vector3 centroid;

		// Token: 0x04001253 RID: 4691
		public Vector3 min;

		// Token: 0x04001254 RID: 4692
		public Vector3 max;
	}
}
