using System;
using System.Collections.Generic;
using Exploder.Utils;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003A9 RID: 937
	public static class MeshUtils
	{
		// Token: 0x0600108A RID: 4234 RVA: 0x0005F2D4 File Offset: 0x0005D6D4
		public static Vector3 ComputeBarycentricCoordinates(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			float num = b.x - a.x;
			float num2 = b.y - a.y;
			float num3 = b.z - a.z;
			float num4 = c.x - a.x;
			float num5 = c.y - a.y;
			float num6 = c.z - a.z;
			float num7 = p.x - a.x;
			float num8 = p.y - a.y;
			float num9 = p.z - a.z;
			float num10 = num * num + num2 * num2 + num3 * num3;
			float num11 = num * num4 + num2 * num5 + num3 * num6;
			float num12 = num4 * num4 + num5 * num5 + num6 * num6;
			float num13 = num7 * num + num8 * num2 + num9 * num3;
			float num14 = num7 * num4 + num8 * num5 + num9 * num6;
			float num15 = num10 * num12 - num11 * num11;
			float num16 = (num12 * num13 - num11 * num14) / num15;
			float num17 = (num10 * num14 - num11 * num13) / num15;
			float x = 1f - num16 - num17;
			return new Vector3(x, num16, num17);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0005F40C File Offset: 0x0005D80C
		public static void Swap<T>(ref T a, ref T b)
		{
			T t = a;
			a = b;
			b = t;
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x0005F434 File Offset: 0x0005D834
		public static void CenterPivot(Vector3[] vertices, Vector3 centroid)
		{
			int num = vertices.Length;
			for (int i = 0; i < num; i++)
			{
				Vector3 vector = vertices[i];
				vector.x -= centroid.x;
				vector.y -= centroid.y;
				vector.z -= centroid.z;
				vertices[i] = vector;
			}
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0005F4B0 File Offset: 0x0005D8B0
		public static List<ExploderMesh> IsolateMeshIslands(ExploderMesh mesh)
		{
			int[] triangles = mesh.triangles;
			int allocSize = mesh.vertices.Length;
			int num = mesh.triangles.Length;
			Vector4[] tangents = mesh.tangents;
			Color32[] colors = mesh.colors32;
			Vector3[] vertices = mesh.vertices;
			Vector3[] normals = mesh.normals;
			Vector2[] uv = mesh.uv;
			bool flag = tangents != null && tangents.Length > 0;
			bool flag2 = colors != null && colors.Length > 0;
			bool flag3 = normals != null && normals.Length > 0;
			if (num <= 3)
			{
				return null;
			}
			LSHash lshash = new LSHash(0.1f, allocSize);
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = lshash.Hash(vertices[triangles[i]]);
			}
			List<HashSet<int>> list = new List<HashSet<int>>
			{
				new HashSet<int>
				{
					array[0],
					array[1],
					array[2]
				}
			};
			List<List<int>> list2 = new List<List<int>>
			{
				new List<int>(num)
				{
					0,
					1,
					2
				}
			};
			bool[] array2 = new bool[num];
			array2[0] = true;
			array2[1] = true;
			array2[2] = true;
			HashSet<int> hashSet = list[0];
			List<int> list3 = list2[0];
			int num2 = 3;
			int num3 = -1;
			int num4 = 0;
			do
			{
				bool flag4 = false;
				for (int j = 3; j < num; j += 3)
				{
					if (!array2[j])
					{
						if (hashSet.Contains(array[j]) || hashSet.Contains(array[j + 1]) || hashSet.Contains(array[j + 2]))
						{
							hashSet.Add(array[j]);
							hashSet.Add(array[j + 1]);
							hashSet.Add(array[j + 2]);
							list3.Add(j);
							list3.Add(j + 1);
							list3.Add(j + 2);
							array2[j] = true;
							array2[j + 1] = true;
							array2[j + 2] = true;
							num2 += 3;
							flag4 = true;
						}
						else
						{
							num3 = j;
						}
					}
				}
				if (num2 == num)
				{
					break;
				}
				if (!flag4)
				{
					hashSet = new HashSet<int>
					{
						array[num3],
						array[num3 + 1],
						array[num3 + 2]
					};
					list3 = new List<int>(num / 2)
					{
						num3,
						num3 + 1,
						num3 + 2
					};
					list.Add(hashSet);
					list2.Add(list3);
				}
				num4++;
			}
			while (num4 <= 100);
			int count = list.Count;
			if (count == 1)
			{
				return null;
			}
			List<ExploderMesh> list4 = new List<ExploderMesh>(list.Count);
			foreach (List<int> list5 in list2)
			{
				ExploderMesh exploderMesh = new ExploderMesh();
				int count2 = list5.Count;
				ExploderMesh exploderMesh2 = exploderMesh;
				List<int> list6 = new List<int>(count2);
				List<Vector3> list7 = new List<Vector3>(count2);
				List<Vector3> list8 = new List<Vector3>(count2);
				List<Vector2> list9 = new List<Vector2>(count2);
				List<Color32> list10 = new List<Color32>(count2);
				List<Vector4> list11 = new List<Vector4>(count2);
				Dictionary<int, int> dictionary = new Dictionary<int, int>(num);
				Vector3 a = Vector3.zero;
				int num5 = 0;
				int num6 = 0;
				foreach (int num7 in list5)
				{
					int num8 = triangles[num7];
					int item = 0;
					if (dictionary.TryGetValue(num8, out item))
					{
						list6.Add(item);
					}
					else
					{
						list6.Add(num6);
						dictionary.Add(num8, num6);
						num6++;
						a += vertices[num8];
						num5++;
						list7.Add(vertices[num8]);
						list9.Add(uv[num8]);
						if (flag3)
						{
							list8.Add(normals[num8]);
						}
						if (flag2)
						{
							list10.Add(colors[num8]);
						}
						if (flag)
						{
							list11.Add(tangents[num8]);
						}
					}
				}
				exploderMesh2.vertices = list7.ToArray();
				exploderMesh2.uv = list9.ToArray();
				if (flag3)
				{
					exploderMesh2.normals = list8.ToArray();
				}
				if (flag2)
				{
					exploderMesh2.colors32 = list10.ToArray();
				}
				if (flag)
				{
					exploderMesh2.tangents = list11.ToArray();
				}
				exploderMesh2.triangles = list6.ToArray();
				exploderMesh.centroid = a / (float)num5;
				list4.Add(exploderMesh);
			}
			return list4;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0005FA20 File Offset: 0x0005DE20
		public static void GeneratePolygonCollider(PolygonCollider2D collider, Mesh mesh)
		{
			if (mesh && collider)
			{
				Vector3[] vertices = mesh.vertices;
				Vector2[] array = new Vector2[vertices.Length];
				for (int i = 0; i < vertices.Length; i++)
				{
					array[i] = vertices[i];
				}
				Vector2[] points = Hull2D.ChainHull2D(array);
				collider.SetPath(0, points);
			}
		}
	}
}
