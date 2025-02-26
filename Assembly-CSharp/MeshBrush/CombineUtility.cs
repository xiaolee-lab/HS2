using System;
using UnityEngine;

namespace MeshBrush
{
	// Token: 0x020003F2 RID: 1010
	public static class CombineUtility
	{
		// Token: 0x0600120D RID: 4621 RVA: 0x0006FA60 File Offset: 0x0006DE60
		public static Mesh Combine(CombineUtility.MeshInstance[] combines, bool generateStrips)
		{
			CombineUtility.vertexCount = 0;
			CombineUtility.triangleCount = 0;
			CombineUtility.stripCount = 0;
			foreach (CombineUtility.MeshInstance meshInstance in combines)
			{
				if (meshInstance.mesh != null)
				{
					CombineUtility.vertexCount += meshInstance.mesh.vertexCount;
					if (generateStrips)
					{
						CombineUtility.curStripCount = meshInstance.mesh.GetTriangles(meshInstance.subMeshIndex).Length;
						if (CombineUtility.curStripCount != 0)
						{
							if (CombineUtility.stripCount != 0)
							{
								if ((CombineUtility.stripCount & 1) == 1)
								{
									CombineUtility.stripCount += 3;
								}
								else
								{
									CombineUtility.stripCount += 2;
								}
							}
							CombineUtility.stripCount += CombineUtility.curStripCount;
						}
						else
						{
							generateStrips = false;
						}
					}
				}
			}
			if (!generateStrips)
			{
				foreach (CombineUtility.MeshInstance meshInstance2 in combines)
				{
					if (meshInstance2.mesh != null)
					{
						CombineUtility.triangleCount += meshInstance2.mesh.GetTriangles(meshInstance2.subMeshIndex).Length;
					}
				}
			}
			CombineUtility.vertices = new Vector3[CombineUtility.vertexCount];
			CombineUtility.normals = new Vector3[CombineUtility.vertexCount];
			CombineUtility.tangents = new Vector4[CombineUtility.vertexCount];
			CombineUtility.uv = new Vector2[CombineUtility.vertexCount];
			CombineUtility.uv1 = new Vector2[CombineUtility.vertexCount];
			CombineUtility.colors = new Color[CombineUtility.vertexCount];
			CombineUtility.triangles = new int[CombineUtility.triangleCount];
			CombineUtility.strip = new int[CombineUtility.stripCount];
			CombineUtility.offset = 0;
			foreach (CombineUtility.MeshInstance meshInstance3 in combines)
			{
				if (meshInstance3.mesh != null)
				{
					CombineUtility.Copy(meshInstance3.mesh.vertexCount, meshInstance3.mesh.vertices, CombineUtility.vertices, ref CombineUtility.offset, meshInstance3.transform);
				}
			}
			CombineUtility.offset = 0;
			foreach (CombineUtility.MeshInstance meshInstance4 in combines)
			{
				if (meshInstance4.mesh != null)
				{
					CombineUtility.invTranspose = meshInstance4.transform;
					CombineUtility.invTranspose = CombineUtility.invTranspose.inverse.transpose;
					CombineUtility.CopyNormal(meshInstance4.mesh.vertexCount, meshInstance4.mesh.normals, CombineUtility.normals, ref CombineUtility.offset, CombineUtility.invTranspose);
				}
			}
			CombineUtility.offset = 0;
			foreach (CombineUtility.MeshInstance meshInstance5 in combines)
			{
				if (meshInstance5.mesh != null)
				{
					CombineUtility.invTranspose = meshInstance5.transform;
					CombineUtility.invTranspose = CombineUtility.invTranspose.inverse.transpose;
					CombineUtility.CopyTangents(meshInstance5.mesh.vertexCount, meshInstance5.mesh.tangents, CombineUtility.tangents, ref CombineUtility.offset, CombineUtility.invTranspose);
				}
			}
			CombineUtility.offset = 0;
			foreach (CombineUtility.MeshInstance meshInstance6 in combines)
			{
				if (meshInstance6.mesh != null)
				{
					CombineUtility.Copy(meshInstance6.mesh.vertexCount, meshInstance6.mesh.uv, CombineUtility.uv, ref CombineUtility.offset);
				}
			}
			CombineUtility.offset = 0;
			foreach (CombineUtility.MeshInstance meshInstance7 in combines)
			{
				if (meshInstance7.mesh != null)
				{
					CombineUtility.Copy(meshInstance7.mesh.vertexCount, meshInstance7.mesh.uv2, CombineUtility.uv1, ref CombineUtility.offset);
				}
			}
			CombineUtility.offset = 0;
			foreach (CombineUtility.MeshInstance meshInstance8 in combines)
			{
				if (meshInstance8.mesh != null)
				{
					CombineUtility.CopyColors(meshInstance8.mesh.vertexCount, meshInstance8.mesh.colors, CombineUtility.colors, ref CombineUtility.offset);
				}
			}
			CombineUtility.triangleOffset = 0;
			CombineUtility.stripOffset = 0;
			CombineUtility.vertexOffset = 0;
			foreach (CombineUtility.MeshInstance meshInstance9 in combines)
			{
				if (meshInstance9.mesh != null)
				{
					if (generateStrips)
					{
						int[] array = meshInstance9.mesh.GetTriangles(meshInstance9.subMeshIndex);
						if (CombineUtility.stripOffset != 0)
						{
							if ((CombineUtility.stripOffset & 1) == 1)
							{
								CombineUtility.strip[CombineUtility.stripOffset] = CombineUtility.strip[CombineUtility.stripOffset - 1];
								CombineUtility.strip[CombineUtility.stripOffset + 1] = array[0] + CombineUtility.vertexOffset;
								CombineUtility.strip[CombineUtility.stripOffset + 2] = array[0] + CombineUtility.vertexOffset;
								CombineUtility.stripOffset += 3;
							}
							else
							{
								CombineUtility.strip[CombineUtility.stripOffset] = CombineUtility.strip[CombineUtility.stripOffset - 1];
								CombineUtility.strip[CombineUtility.stripOffset + 1] = array[0] + CombineUtility.vertexOffset;
								CombineUtility.stripOffset += 2;
							}
						}
						for (int num4 = 0; num4 < array.Length; num4++)
						{
							CombineUtility.strip[num4 + CombineUtility.stripOffset] = array[num4] + CombineUtility.vertexOffset;
						}
						CombineUtility.stripOffset += array.Length;
					}
					else
					{
						int[] array2 = meshInstance9.mesh.GetTriangles(meshInstance9.subMeshIndex);
						for (int num5 = 0; num5 < array2.Length; num5++)
						{
							CombineUtility.triangles[num5 + CombineUtility.triangleOffset] = array2[num5] + CombineUtility.vertexOffset;
						}
						CombineUtility.triangleOffset += array2.Length;
					}
					CombineUtility.vertexOffset += meshInstance9.mesh.vertexCount;
				}
			}
			Mesh mesh = new Mesh();
			mesh.name = "Combined Mesh";
			mesh.vertices = CombineUtility.vertices;
			mesh.normals = CombineUtility.normals;
			mesh.colors = CombineUtility.colors;
			mesh.uv = CombineUtility.uv;
			mesh.uv2 = CombineUtility.uv1;
			mesh.tangents = CombineUtility.tangents;
			if (generateStrips)
			{
				mesh.SetTriangles(CombineUtility.strip, 0);
			}
			else
			{
				mesh.triangles = CombineUtility.triangles;
			}
			return mesh;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00070140 File Offset: 0x0006E540
		private static void Copy(int vertexcount, Vector3[] src, Vector3[] dst, ref int offset, Matrix4x4 transform)
		{
			for (int i = 0; i < src.Length; i++)
			{
				dst[i + offset] = transform.MultiplyPoint(src[i]);
			}
			offset += vertexcount;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0007018C File Offset: 0x0006E58C
		private static void CopyNormal(int vertexcount, Vector3[] src, Vector3[] dst, ref int offset, Matrix4x4 transform)
		{
			for (int i = 0; i < src.Length; i++)
			{
				dst[i + offset] = transform.MultiplyVector(src[i]).normalized;
			}
			offset += vertexcount;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x000701E0 File Offset: 0x0006E5E0
		private static void Copy(int vertexcount, Vector2[] src, Vector2[] dst, ref int offset)
		{
			for (int i = 0; i < src.Length; i++)
			{
				dst[i + offset] = src[i];
			}
			offset += vertexcount;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00070224 File Offset: 0x0006E624
		private static void CopyColors(int vertexcount, Color[] src, Color[] dst, ref int offset)
		{
			for (int i = 0; i < src.Length; i++)
			{
				dst[i + offset] = src[i];
			}
			offset += vertexcount;
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00070268 File Offset: 0x0006E668
		private static void CopyTangents(int vertexcount, Vector4[] src, Vector4[] dst, ref int offset, Matrix4x4 transform)
		{
			for (int i = 0; i < src.Length; i++)
			{
				CombineUtility.p4 = src[i];
				CombineUtility.p = new Vector3(CombineUtility.p4.x, CombineUtility.p4.y, CombineUtility.p4.z);
				CombineUtility.p = transform.MultiplyVector(CombineUtility.p).normalized;
				dst[i + offset] = new Vector4(CombineUtility.p.x, CombineUtility.p.y, CombineUtility.p.z, CombineUtility.p4.w);
			}
			offset += vertexcount;
		}

		// Token: 0x0400148C RID: 5260
		private static int vertexCount;

		// Token: 0x0400148D RID: 5261
		private static int triangleCount;

		// Token: 0x0400148E RID: 5262
		private static int stripCount;

		// Token: 0x0400148F RID: 5263
		private static int curStripCount;

		// Token: 0x04001490 RID: 5264
		private static Vector3[] vertices;

		// Token: 0x04001491 RID: 5265
		private static Vector3[] normals;

		// Token: 0x04001492 RID: 5266
		private static Vector4[] tangents;

		// Token: 0x04001493 RID: 5267
		private static Vector2[] uv;

		// Token: 0x04001494 RID: 5268
		private static Vector2[] uv1;

		// Token: 0x04001495 RID: 5269
		private static Color[] colors;

		// Token: 0x04001496 RID: 5270
		private static int[] triangles;

		// Token: 0x04001497 RID: 5271
		private static int[] strip;

		// Token: 0x04001498 RID: 5272
		private static int offset;

		// Token: 0x04001499 RID: 5273
		private static int triangleOffset;

		// Token: 0x0400149A RID: 5274
		private static int stripOffset;

		// Token: 0x0400149B RID: 5275
		private static int vertexOffset;

		// Token: 0x0400149C RID: 5276
		private static Matrix4x4 invTranspose;

		// Token: 0x0400149D RID: 5277
		public const string combinedMeshName = "Combined Mesh";

		// Token: 0x0400149E RID: 5278
		private static Vector4 p4;

		// Token: 0x0400149F RID: 5279
		private static Vector3 p;

		// Token: 0x020003F3 RID: 1011
		public struct MeshInstance
		{
			// Token: 0x040014A0 RID: 5280
			public Mesh mesh;

			// Token: 0x040014A1 RID: 5281
			public int subMeshIndex;

			// Token: 0x040014A2 RID: 5282
			public Matrix4x4 transform;
		}
	}
}
