using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x0200096B RID: 2411
	public static class MathExtension
	{
		// Token: 0x060042EC RID: 17132 RVA: 0x001A58DC File Offset: 0x001A3CDC
		public static bool IsInsideRange(this int source, int min, int max)
		{
			return source >= min && source <= max;
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x001A58EF File Offset: 0x001A3CEF
		public static bool GetInsideRange(this float source, float min, float max)
		{
			return source >= min && source <= max;
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x001A5902 File Offset: 0x001A3D02
		public static Vector3 NearestVertexTo(this MeshFilter meshFilter, Vector3 point)
		{
			return MathExtension.NearestVertexTo(meshFilter.transform, meshFilter.mesh, point);
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x001A5916 File Offset: 0x001A3D16
		public static Vector3 NearestVertexTo(this MeshCollider collider, Vector3 point)
		{
			return MathExtension.NearestVertexTo(collider.transform, collider.sharedMesh, point);
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x001A592C File Offset: 0x001A3D2C
		public static Vector3 NearestVertexTo(Transform transform, Mesh mesh, Vector3 point)
		{
			point = transform.InverseTransformPoint(point);
			float num = float.PositiveInfinity;
			Vector3 position = Vector3.zero;
			foreach (Vector3 vector in mesh.vertices)
			{
				float sqrMagnitude = (point - vector).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					position = vector;
				}
			}
			return transform.TransformPoint(position);
		}
	}
}
