using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003A6 RID: 934
	public class MeshCutter
	{
		// Token: 0x06001082 RID: 4226 RVA: 0x0005C94C File Offset: 0x0005AD4C
		public void Init(int trianglesNum, int verticesNum)
		{
			this.AllocateBuffers(trianglesNum, verticesNum, false, false);
			this.AllocateContours(trianglesNum / 2);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0005C964 File Offset: 0x0005AD64
		private void AllocateBuffers(int trianglesNum, int verticesNum, bool useMeshTangents, bool useVertexColors)
		{
			if (this.triangles == null || this.triangles[0].Capacity < trianglesNum)
			{
				this.triangles = new List<int>[]
				{
					new List<int>(trianglesNum),
					new List<int>(trianglesNum)
				};
			}
			else
			{
				this.triangles[0].Clear();
				this.triangles[1].Clear();
			}
			if (this.vertices == null || this.vertices[0].Capacity < verticesNum || this.triCache.Length < verticesNum || (useMeshTangents && (this.tangents == null || this.tangents[0].Capacity < verticesNum)) || (useVertexColors && (this.vertexColors == null || this.vertexColors[0].Capacity < verticesNum)))
			{
				this.vertices = new List<Vector3>[]
				{
					new List<Vector3>(verticesNum),
					new List<Vector3>(verticesNum)
				};
				this.normals = new List<Vector3>[]
				{
					new List<Vector3>(verticesNum),
					new List<Vector3>(verticesNum)
				};
				this.uvs = new List<Vector2>[]
				{
					new List<Vector2>(verticesNum),
					new List<Vector2>(verticesNum)
				};
				if (useMeshTangents)
				{
					this.tangents = new List<Vector4>[]
					{
						new List<Vector4>(verticesNum),
						new List<Vector4>(verticesNum)
					};
				}
				else
				{
					this.tangents = new List<Vector4>[]
					{
						new List<Vector4>(0),
						new List<Vector4>(0)
					};
				}
				if (useVertexColors)
				{
					this.vertexColors = new List<Color32>[]
					{
						new List<Color32>(verticesNum),
						new List<Color32>(verticesNum)
					};
				}
				else
				{
					this.vertexColors = new List<Color32>[]
					{
						new List<Color32>(0),
						new List<Color32>(0)
					};
				}
				this.centroid = new Vector3[2];
				this.triCache = new int[verticesNum + 1];
				this.triCounter = new int[2];
				this.cutTris = new List<int>(verticesNum / 3);
			}
			else
			{
				for (int i = 0; i < 2; i++)
				{
					this.vertices[i].Clear();
					this.normals[i].Clear();
					this.uvs[i].Clear();
					this.tangents[i].Clear();
					this.vertexColors[i].Clear();
					this.centroid[i] = Vector3.zero;
					this.triCounter[i] = 0;
				}
				this.cutTris.Clear();
				for (int j = 0; j < this.triCache.Length; j++)
				{
					this.triCache[j] = 0;
				}
			}
			if (this.polygonIndicesArray == null)
			{
				this.polygonIndicesArray = new Array<int>(1024);
			}
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0005CC20 File Offset: 0x0005B020
		private void AllocateContours(int cutTrianglesNum)
		{
			if (this.contour == null)
			{
				this.contour = new Contour(cutTrianglesNum);
				this.cutVertCache = new Dictionary<long, int>[]
				{
					new Dictionary<long, int>(cutTrianglesNum * 2),
					new Dictionary<long, int>(cutTrianglesNum * 2)
				};
				this.cornerVertCache = new Dictionary<int, int>[]
				{
					new Dictionary<int, int>(cutTrianglesNum),
					new Dictionary<int, int>(cutTrianglesNum)
				};
				this.contourBufferSize = cutTrianglesNum;
			}
			else
			{
				if (this.contourBufferSize < cutTrianglesNum)
				{
					this.cutVertCache = new Dictionary<long, int>[]
					{
						new Dictionary<long, int>(cutTrianglesNum * 2),
						new Dictionary<long, int>(cutTrianglesNum * 2)
					};
					this.cornerVertCache = new Dictionary<int, int>[]
					{
						new Dictionary<int, int>(cutTrianglesNum),
						new Dictionary<int, int>(cutTrianglesNum)
					};
					this.contourBufferSize = cutTrianglesNum;
				}
				else
				{
					for (int i = 0; i < 2; i++)
					{
						this.cutVertCache[i].Clear();
						this.cornerVertCache[i].Clear();
					}
				}
				this.contour.AllocateBuffers(cutTrianglesNum);
			}
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0005CD20 File Offset: 0x0005B120
		public float Cut(ExploderMesh mesh, ExploderTransform meshTransform, Plane plane, bool triangulateHoles, bool allowOpenMesh, ref List<ExploderMesh> meshes, Color crossSectionVertexColor, Vector4 crossUV)
		{
			this.crossSectionVertexColour = crossSectionVertexColor;
			this.crossSectionUV = crossUV;
			return this.Cut(mesh, meshTransform, plane, triangulateHoles, allowOpenMesh, ref meshes);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0005CD44 File Offset: 0x0005B144
		private float Cut(ExploderMesh mesh, ExploderTransform meshTransform, Plane plane, bool triangulateHoles, bool allowOpenMesh, ref List<ExploderMesh> meshes)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			int num = mesh.triangles.Length;
			int verticesNum = mesh.vertices.Length;
			int[] array = mesh.triangles;
			Vector4[] array2 = mesh.tangents;
			Color32[] colors = mesh.colors32;
			Vector3[] array3 = mesh.vertices;
			Vector3[] array4 = mesh.normals;
			Vector2[] uv = mesh.uv;
			bool flag = array2 != null && array2.Length > 0;
			bool flag2 = colors != null && colors.Length > 0;
			bool flag3 = array4 != null && array4.Length > 0;
			this.AllocateBuffers(num, verticesNum, flag, flag2);
			for (int i = 0; i < num; i += 3)
			{
				Vector3 vector = array3[array[i]];
				Vector3 vector2 = array3[array[i + 1]];
				Vector3 vector3 = array3[array[i + 2]];
				bool sideFix = plane.GetSideFix(ref vector);
				bool sideFix2 = plane.GetSideFix(ref vector2);
				bool sideFix3 = plane.GetSideFix(ref vector3);
				array3[array[i]] = vector;
				array3[array[i + 1]] = vector2;
				array3[array[i + 2]] = vector3;
				if (sideFix == sideFix2 && sideFix2 == sideFix3)
				{
					int num2 = (!sideFix) ? 1 : 0;
					if (array[i] >= this.triCache.Length)
					{
					}
					if (this.triCache[array[i]] == 0)
					{
						this.triangles[num2].Add(this.triCounter[num2]);
						this.vertices[num2].Add(array3[array[i]]);
						this.uvs[num2].Add(uv[array[i]]);
						if (flag3)
						{
							this.normals[num2].Add(array4[array[i]]);
						}
						if (flag)
						{
							this.tangents[num2].Add(array2[array[i]]);
						}
						if (flag2)
						{
							this.vertexColors[num2].Add(colors[array[i]]);
						}
						this.centroid[num2] += array3[array[i]];
						this.triCache[array[i]] = this.triCounter[num2] + 1;
						this.triCounter[num2]++;
					}
					else
					{
						this.triangles[num2].Add(this.triCache[array[i]] - 1);
					}
					if (this.triCache[array[i + 1]] == 0)
					{
						this.triangles[num2].Add(this.triCounter[num2]);
						this.vertices[num2].Add(array3[array[i + 1]]);
						this.uvs[num2].Add(uv[array[i + 1]]);
						if (flag3)
						{
							this.normals[num2].Add(array4[array[i + 1]]);
						}
						if (flag)
						{
							this.tangents[num2].Add(array2[array[i + 1]]);
						}
						if (flag2)
						{
							this.vertexColors[num2].Add(colors[array[i + 1]]);
						}
						this.centroid[num2] += array3[array[i + 1]];
						this.triCache[array[i + 1]] = this.triCounter[num2] + 1;
						this.triCounter[num2]++;
					}
					else
					{
						this.triangles[num2].Add(this.triCache[array[i + 1]] - 1);
					}
					if (this.triCache[array[i + 2]] == 0)
					{
						this.triangles[num2].Add(this.triCounter[num2]);
						this.vertices[num2].Add(array3[array[i + 2]]);
						this.uvs[num2].Add(uv[array[i + 2]]);
						if (flag3)
						{
							this.normals[num2].Add(array4[array[i + 2]]);
						}
						if (flag)
						{
							this.tangents[num2].Add(array2[array[i + 2]]);
						}
						if (flag2)
						{
							this.vertexColors[num2].Add(colors[array[i + 2]]);
						}
						this.centroid[num2] += array3[array[i + 2]];
						this.triCache[array[i + 2]] = this.triCounter[num2] + 1;
						this.triCounter[num2]++;
					}
					else
					{
						this.triangles[num2].Add(this.triCache[array[i + 2]] - 1);
					}
				}
				else
				{
					this.cutTris.Add(i);
				}
			}
			if (this.vertices[0].Count == 0)
			{
				this.centroid[0] = array3[0];
			}
			else
			{
				this.centroid[0] /= (float)this.vertices[0].Count;
			}
			if (this.vertices[1].Count == 0)
			{
				this.centroid[1] = array3[1];
			}
			else
			{
				this.centroid[1] /= (float)this.vertices[1].Count;
			}
			if (this.cutTris.Count < 1)
			{
				stopwatch.Stop();
				return (float)stopwatch.ElapsedMilliseconds;
			}
			this.AllocateContours(this.cutTris.Count);
			foreach (int num3 in this.cutTris)
			{
				MeshCutter.Triangle triangle = default(MeshCutter.Triangle);
				triangle.ids = new int[]
				{
					array[num3],
					array[num3 + 1],
					array[num3 + 2]
				};
				triangle.pos = new Vector3[]
				{
					array3[array[num3]],
					array3[array[num3 + 1]],
					array3[array[num3 + 2]]
				};
				Vector3[] normal;
				if (flag3)
				{
					Vector3[] array5 = new Vector3[3];
					array5[0] = array4[array[num3]];
					array5[1] = array4[array[num3 + 1]];
					normal = array5;
					array5[2] = array4[array[num3 + 2]];
				}
				else
				{
					Vector3[] array6 = new Vector3[3];
					array6[0] = Vector3.zero;
					array6[1] = Vector3.zero;
					normal = array6;
					array6[2] = Vector3.zero;
				}
				triangle.normal = normal;
				triangle.uvs = new Vector2[]
				{
					uv[array[num3]],
					uv[array[num3 + 1]],
					uv[array[num3 + 2]]
				};
				Vector4[] array8;
				if (flag)
				{
					Vector4[] array7 = new Vector4[3];
					array7[0] = array2[array[num3]];
					array7[1] = array2[array[num3 + 1]];
					array8 = array7;
					array7[2] = array2[array[num3 + 2]];
				}
				else
				{
					Vector4[] array9 = new Vector4[3];
					array9[0] = Vector4.zero;
					array9[1] = Vector4.zero;
					array8 = array9;
					array9[2] = Vector4.zero;
				}
				triangle.tangents = array8;
				Color32[] colors2;
				if (flag2)
				{
					Color32[] array10 = new Color32[3];
					array10[0] = colors[array[num3]];
					array10[1] = colors[array[num3 + 1]];
					colors2 = array10;
					array10[2] = colors[array[num3 + 2]];
				}
				else
				{
					Color32[] array11 = new Color32[3];
					array11[0] = Color.white;
					array11[1] = Color.white;
					colors2 = array11;
					array11[2] = Color.white;
				}
				triangle.colors = colors2;
				MeshCutter.Triangle tri = triangle;
				bool side = plane.GetSide(tri.pos[0]);
				bool side2 = plane.GetSide(tri.pos[1]);
				bool side3 = plane.GetSide(tri.pos[2]);
				Vector3 zero = Vector3.zero;
				Vector3 zero2 = Vector3.zero;
				int num4 = (!side) ? 1 : 0;
				int num5 = 1 - num4;
				if (side == side2)
				{
					float num6;
					bool flag4 = plane.IntersectSegment(tri.pos[2], tri.pos[0], out num6, ref zero);
					float num7;
					bool flag5 = plane.IntersectSegment(tri.pos[2], tri.pos[1], out num7, ref zero2);
					int num8 = this.AddIntersectionPoint(zero, tri, tri.ids[2], tri.ids[0], this.cutVertCache[num4], this.vertices[num4], this.normals[num4], this.uvs[num4], this.tangents[num4], this.vertexColors[num4], flag, flag2, flag3);
					int num9 = this.AddIntersectionPoint(zero2, tri, tri.ids[2], tri.ids[1], this.cutVertCache[num4], this.vertices[num4], this.normals[num4], this.uvs[num4], this.tangents[num4], this.vertexColors[num4], flag, flag2, flag3);
					int item = this.AddTrianglePoint(tri.pos[0], tri.normal[0], tri.uvs[0], tri.tangents[0], tri.colors[0], tri.ids[0], this.triCache, this.cornerVertCache[num4], this.vertices[num4], this.normals[num4], this.uvs[num4], this.tangents[num4], this.vertexColors[num4], flag, flag2, flag3);
					int item2 = this.AddTrianglePoint(tri.pos[1], tri.normal[1], tri.uvs[1], tri.tangents[1], tri.colors[1], tri.ids[1], this.triCache, this.cornerVertCache[num4], this.vertices[num4], this.normals[num4], this.uvs[num4], this.tangents[num4], this.vertexColors[num4], flag, flag2, flag3);
					this.triangles[num4].Add(num8);
					this.triangles[num4].Add(item);
					this.triangles[num4].Add(num9);
					this.triangles[num4].Add(num9);
					this.triangles[num4].Add(item);
					this.triangles[num4].Add(item2);
					int num10 = this.AddIntersectionPoint(zero, tri, tri.ids[2], tri.ids[0], this.cutVertCache[num5], this.vertices[num5], this.normals[num5], this.uvs[num5], this.tangents[num5], this.vertexColors[num5], flag, flag2, flag3);
					int num11 = this.AddIntersectionPoint(zero2, tri, tri.ids[2], tri.ids[1], this.cutVertCache[num5], this.vertices[num5], this.normals[num5], this.uvs[num5], this.tangents[num5], this.vertexColors[num5], flag, flag2, flag3);
					int item3 = this.AddTrianglePoint(tri.pos[2], tri.normal[2], tri.uvs[2], tri.tangents[2], tri.colors[2], tri.ids[2], this.triCache, this.cornerVertCache[num5], this.vertices[num5], this.normals[num5], this.uvs[num5], this.tangents[num5], this.vertexColors[num5], flag, flag2, flag3);
					this.triangles[num5].Add(item3);
					this.triangles[num5].Add(num10);
					this.triangles[num5].Add(num11);
					if (triangulateHoles)
					{
						if (num4 == 0)
						{
							this.contour.AddTriangle(num3, num8, num9, zero, zero2);
						}
						else
						{
							this.contour.AddTriangle(num3, num10, num11, zero, zero2);
						}
					}
				}
				else if (side == side3)
				{
					float num6;
					bool flag6 = plane.IntersectSegment(tri.pos[1], tri.pos[0], out num6, ref zero2);
					float num7;
					bool flag7 = plane.IntersectSegment(tri.pos[1], tri.pos[2], out num7, ref zero);
					int num12 = this.AddIntersectionPoint(zero, tri, tri.ids[1], tri.ids[2], this.cutVertCache[num4], this.vertices[num4], this.normals[num4], this.uvs[num4], this.tangents[num4], this.vertexColors[num4], flag, flag2, flag3);
					int num13 = this.AddIntersectionPoint(zero2, tri, tri.ids[1], tri.ids[0], this.cutVertCache[num4], this.vertices[num4], this.normals[num4], this.uvs[num4], this.tangents[num4], this.vertexColors[num4], flag, flag2, flag3);
					int item4 = this.AddTrianglePoint(tri.pos[0], tri.normal[0], tri.uvs[0], tri.tangents[0], tri.colors[0], tri.ids[0], this.triCache, this.cornerVertCache[num4], this.vertices[num4], this.normals[num4], this.uvs[num4], this.tangents[num4], this.vertexColors[num4], flag, flag2, flag3);
					int item5 = this.AddTrianglePoint(tri.pos[2], tri.normal[2], tri.uvs[2], tri.tangents[2], tri.colors[2], tri.ids[2], this.triCache, this.cornerVertCache[num4], this.vertices[num4], this.normals[num4], this.uvs[num4], this.tangents[num4], this.vertexColors[num4], flag, flag2, flag3);
					this.triangles[num4].Add(item5);
					this.triangles[num4].Add(num13);
					this.triangles[num4].Add(num12);
					this.triangles[num4].Add(item5);
					this.triangles[num4].Add(item4);
					this.triangles[num4].Add(num13);
					int num14 = this.AddIntersectionPoint(zero, tri, tri.ids[1], tri.ids[2], this.cutVertCache[num5], this.vertices[num5], this.normals[num5], this.uvs[num5], this.tangents[num5], this.vertexColors[num5], flag, flag2, flag3);
					int num15 = this.AddIntersectionPoint(zero2, tri, tri.ids[1], tri.ids[0], this.cutVertCache[num5], this.vertices[num5], this.normals[num5], this.uvs[num5], this.tangents[num5], this.vertexColors[num5], flag, flag2, flag3);
					int item6 = this.AddTrianglePoint(tri.pos[1], tri.normal[1], tri.uvs[1], tri.tangents[1], tri.colors[1], tri.ids[1], this.triCache, this.cornerVertCache[num5], this.vertices[num5], this.normals[num5], this.uvs[num5], this.tangents[num5], this.vertexColors[num5], flag, flag2, flag3);
					this.triangles[num5].Add(num14);
					this.triangles[num5].Add(num15);
					this.triangles[num5].Add(item6);
					if (triangulateHoles)
					{
						if (num4 == 0)
						{
							this.contour.AddTriangle(num3, num12, num13, zero, zero2);
						}
						else
						{
							this.contour.AddTriangle(num3, num14, num15, zero, zero2);
						}
					}
				}
				else
				{
					float num6;
					bool flag8 = plane.IntersectSegment(tri.pos[0], tri.pos[1], out num6, ref zero);
					float num7;
					bool flag9 = plane.IntersectSegment(tri.pos[0], tri.pos[2], out num7, ref zero2);
					int num16 = this.AddIntersectionPoint(zero, tri, tri.ids[0], tri.ids[1], this.cutVertCache[num5], this.vertices[num5], this.normals[num5], this.uvs[num5], this.tangents[num5], this.vertexColors[num5], flag, flag2, flag3);
					int num17 = this.AddIntersectionPoint(zero2, tri, tri.ids[0], tri.ids[2], this.cutVertCache[num5], this.vertices[num5], this.normals[num5], this.uvs[num5], this.tangents[num5], this.vertexColors[num5], flag, flag2, flag3);
					int item7 = this.AddTrianglePoint(tri.pos[1], tri.normal[1], tri.uvs[1], tri.tangents[1], tri.colors[1], tri.ids[1], this.triCache, this.cornerVertCache[num5], this.vertices[num5], this.normals[num5], this.uvs[num5], this.tangents[num5], this.vertexColors[num5], flag, flag2, flag3);
					int item8 = this.AddTrianglePoint(tri.pos[2], tri.normal[2], tri.uvs[2], tri.tangents[2], tri.colors[2], tri.ids[2], this.triCache, this.cornerVertCache[num5], this.vertices[num5], this.normals[num5], this.uvs[num5], this.tangents[num5], this.vertexColors[num5], flag, flag2, flag3);
					this.triangles[num5].Add(item8);
					this.triangles[num5].Add(num17);
					this.triangles[num5].Add(item7);
					this.triangles[num5].Add(num17);
					this.triangles[num5].Add(num16);
					this.triangles[num5].Add(item7);
					int num18 = this.AddIntersectionPoint(zero, tri, tri.ids[0], tri.ids[1], this.cutVertCache[num4], this.vertices[num4], this.normals[num4], this.uvs[num4], this.tangents[num4], this.vertexColors[num4], flag, flag2, flag3);
					int num19 = this.AddIntersectionPoint(zero2, tri, tri.ids[0], tri.ids[2], this.cutVertCache[num4], this.vertices[num4], this.normals[num4], this.uvs[num4], this.tangents[num4], this.vertexColors[num4], flag, flag2, flag3);
					int item9 = this.AddTrianglePoint(tri.pos[0], tri.normal[0], tri.uvs[0], tri.tangents[0], tri.colors[0], tri.ids[0], this.triCache, this.cornerVertCache[num4], this.vertices[num4], this.normals[num4], this.uvs[num4], this.tangents[num4], this.vertexColors[num4], flag, flag2, flag3);
					this.triangles[num4].Add(num19);
					this.triangles[num4].Add(item9);
					this.triangles[num4].Add(num18);
					if (triangulateHoles)
					{
						if (num4 == 0)
						{
							this.contour.AddTriangle(num3, num18, num19, zero, zero2);
						}
						else
						{
							this.contour.AddTriangle(num3, num16, num17, zero, zero2);
						}
					}
				}
			}
			if (triangulateHoles)
			{
				this.contour.FindContours();
				if (this.contour.contour.Count == 0 || this.contour.contour[0].Count < 3)
				{
					if (!allowOpenMesh)
					{
						stopwatch.Stop();
						return (float)stopwatch.ElapsedMilliseconds;
					}
					triangulateHoles = false;
				}
			}
			List<int>[] array12 = null;
			Vector3 zero3 = Vector3.zero;
			Vector3 zero4 = Vector3.zero;
			Vector3 zero5 = Vector3.zero;
			Vector3 zero6 = Vector3.zero;
			Vector3 zero7 = Vector3.zero;
			Vector3 zero8 = Vector3.zero;
			ExploderMesh.CalculateCentroid(this.vertices[0], ref zero3, ref zero5, ref zero6);
			ExploderMesh.CalculateCentroid(this.vertices[1], ref zero4, ref zero7, ref zero8);
			if (triangulateHoles)
			{
				array12 = new List<int>[]
				{
					new List<int>(this.contour.MidPointsCount),
					new List<int>(this.contour.MidPointsCount)
				};
				this.Triangulate(this.contour.contour, plane, this.vertices, this.normals, this.uvs, this.tangents, this.vertexColors, array12, true, flag, flag2, flag3);
			}
			if (this.vertices[0].Count > 3 && this.vertices[1].Count > 3)
			{
				ExploderMesh exploderMesh = new ExploderMesh();
				ExploderMesh exploderMesh2 = new ExploderMesh();
				Vector3[] array13 = this.vertices[0].ToArray();
				Vector3[] array14 = this.vertices[1].ToArray();
				exploderMesh.vertices = array13;
				exploderMesh.uv = this.uvs[0].ToArray();
				exploderMesh2.vertices = array14;
				exploderMesh2.uv = this.uvs[1].ToArray();
				if (flag3)
				{
					exploderMesh.normals = this.normals[0].ToArray();
					exploderMesh2.normals = this.normals[1].ToArray();
				}
				if (flag)
				{
					exploderMesh.tangents = this.tangents[0].ToArray();
					exploderMesh2.tangents = this.tangents[1].ToArray();
				}
				if (flag2)
				{
					exploderMesh.colors32 = this.vertexColors[0].ToArray();
					exploderMesh2.colors32 = this.vertexColors[1].ToArray();
				}
				if (array12 != null && array12[0].Count > 3)
				{
					this.triangles[0].AddRange(array12[0]);
					this.triangles[1].AddRange(array12[1]);
				}
				exploderMesh.triangles = this.triangles[0].ToArray();
				exploderMesh2.triangles = this.triangles[1].ToArray();
				exploderMesh.centroid = zero3;
				exploderMesh.min = zero5;
				exploderMesh.max = zero6;
				exploderMesh2.centroid = zero4;
				exploderMesh2.min = zero7;
				exploderMesh2.max = zero8;
				meshes = new List<ExploderMesh>
				{
					exploderMesh,
					exploderMesh2
				};
				stopwatch.Stop();
				return (float)stopwatch.ElapsedMilliseconds;
			}
			stopwatch.Stop();
			return (float)stopwatch.ElapsedMilliseconds;
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0005E884 File Offset: 0x0005CC84
		private int AddIntersectionPoint(Vector3 pos, MeshCutter.Triangle tri, int edge0, int edge1, Dictionary<long, int> cache, List<Vector3> vertices, List<Vector3> normals, List<Vector2> uvs, List<Vector4> tangents, List<Color32> colors32, bool useTangents, bool useColors, bool useNormals)
		{
			int num = (edge0 >= edge1) ? ((edge1 << 16) + edge0) : ((edge0 << 16) + edge1);
			int result;
			if (cache.TryGetValue((long)num, out result))
			{
				return result;
			}
			Vector3 vector = MeshUtils.ComputeBarycentricCoordinates(tri.pos[0], tri.pos[1], tri.pos[2], pos);
			vertices.Add(pos);
			if (useNormals)
			{
				normals.Add(new Vector3(vector.x * tri.normal[0].x + vector.y * tri.normal[1].x + vector.z * tri.normal[2].x, vector.x * tri.normal[0].y + vector.y * tri.normal[1].y + vector.z * tri.normal[2].y, vector.x * tri.normal[0].z + vector.y * tri.normal[1].z + vector.z * tri.normal[2].z));
			}
			uvs.Add(new Vector2(vector.x * tri.uvs[0].x + vector.y * tri.uvs[1].x + vector.z * tri.uvs[2].x, vector.x * tri.uvs[0].y + vector.y * tri.uvs[1].y + vector.z * tri.uvs[2].y));
			if (useTangents)
			{
				tangents.Add(new Vector4(vector.x * tri.tangents[0].x + vector.y * tri.tangents[1].x + vector.z * tri.tangents[2].x, vector.x * tri.tangents[0].y + vector.y * tri.tangents[1].y + vector.z * tri.tangents[2].y, vector.x * tri.tangents[0].z + vector.y * tri.tangents[1].z + vector.z * tri.tangents[2].z, vector.x * tri.tangents[0].w + vector.y * tri.tangents[1].w + vector.z * tri.tangents[2].z));
			}
			if (useColors)
			{
				colors32.Add(tri.colors[0]);
			}
			int num2 = vertices.Count - 1;
			cache.Add((long)num, num2);
			return num2;
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0005EC3C File Offset: 0x0005D03C
		private int AddTrianglePoint(Vector3 pos, Vector3 normal, Vector2 uv, Vector4 tangent, Color32 color, int idx, int[] triCache, Dictionary<int, int> cache, List<Vector3> vertices, List<Vector3> normals, List<Vector2> uvs, List<Vector4> tangents, List<Color32> colors, bool useTangents, bool useColors, bool useNormals)
		{
			if (triCache[idx] != 0)
			{
				return triCache[idx] - 1;
			}
			int result;
			if (cache.TryGetValue(idx, out result))
			{
				return result;
			}
			vertices.Add(pos);
			if (useNormals)
			{
				normals.Add(normal);
			}
			uvs.Add(uv);
			if (useTangents)
			{
				tangents.Add(tangent);
			}
			if (useColors)
			{
				colors.Add(color);
			}
			int num = vertices.Count - 1;
			cache.Add(idx, num);
			return num;
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0005ECC4 File Offset: 0x0005D0C4
		private void Triangulate(List<Dictionary<int, int>> contours, Plane plane, List<Vector3>[] vertices, List<Vector3>[] normals, List<Vector2>[] uvs, List<Vector4>[] tangents, List<Color32>[] colors, List<int>[] triangles, bool uvCutMesh, bool useTangents, bool useColors, bool useNormals)
		{
			if (contours.Count == 0 || contours[0].Count < 3)
			{
				return;
			}
			Matrix4x4 planeMatrix = plane.GetPlaneMatrix();
			Matrix4x4 inverse = planeMatrix.inverse;
			float z = 0f;
			List<Polygon> list = new List<Polygon>(contours.Count);
			Polygon polygon = null;
			foreach (Dictionary<int, int> dictionary in contours)
			{
				Vector2[] array = new Vector2[dictionary.Count];
				int num = 0;
				foreach (int index in dictionary.Values)
				{
					Vector4 v = inverse * vertices[0][index];
					array[num++] = v;
					z = v.z;
				}
				Polygon polygon2 = new Polygon(array);
				list.Add(polygon2);
				if (polygon == null || Mathf.Abs(polygon.Area) < Mathf.Abs(polygon2.Area))
				{
					polygon = polygon2;
				}
			}
			if (list.Count > 0)
			{
				List<Polygon> list2 = new List<Polygon>();
				foreach (Polygon polygon3 in list)
				{
					if (polygon3 != polygon && polygon.IsPointInside(polygon3.Points[0]))
					{
						polygon.AddHole(polygon3);
						list2.Add(polygon3);
					}
				}
				foreach (Polygon item in list2)
				{
					list.Remove(item);
				}
			}
			int num2 = vertices[0].Count;
			int num3 = vertices[1].Count;
			foreach (Polygon polygon4 in list)
			{
				if (polygon4.Triangulate(this.polygonIndicesArray))
				{
					float num4 = Mathf.Min(polygon4.Min.x, polygon4.Min.y);
					float num5 = Mathf.Max(polygon4.Max.x, polygon4.Max.y);
					float num6 = num5 - num4;
					foreach (Vector2 vector in polygon4.Points)
					{
						Vector4 v2 = planeMatrix * new Vector3(vector.x, vector.y, z);
						vertices[0].Add(v2);
						vertices[1].Add(v2);
						if (useNormals)
						{
							normals[0].Add(-plane.Normal);
							normals[1].Add(plane.Normal);
						}
						if (uvCutMesh)
						{
							Vector2 item2 = new Vector2((vector.x - num4) / num6, (vector.y - num4) / num6);
							Vector2 item3 = new Vector2((vector.x - num4) / num6, (vector.y - num4) / num6);
							float num7 = this.crossSectionUV.z - this.crossSectionUV.x;
							float num8 = this.crossSectionUV.w - this.crossSectionUV.y;
							item2.x = this.crossSectionUV.x + item2.x * num7;
							item2.y = this.crossSectionUV.y + item2.y * num8;
							item3.x = this.crossSectionUV.x + item3.x * num7;
							item3.y = this.crossSectionUV.y + item3.y * num8;
							uvs[0].Add(item2);
							uvs[1].Add(item3);
						}
						else
						{
							uvs[0].Add(Vector2.zero);
							uvs[1].Add(Vector2.zero);
						}
						if (useTangents)
						{
							Vector3 normal = plane.Normal;
							MeshUtils.Swap<float>(ref normal.x, ref normal.y);
							MeshUtils.Swap<float>(ref normal.y, ref normal.z);
							Vector4 item4 = Vector3.Cross(plane.Normal, normal);
							item4.w = 1f;
							tangents[0].Add(item4);
							item4.w = -1f;
							tangents[1].Add(item4);
						}
						if (useColors)
						{
							colors[0].Add(this.crossSectionVertexColour);
							colors[1].Add(this.crossSectionVertexColour);
						}
					}
					int count = this.polygonIndicesArray.Count;
					int num9 = count - 1;
					for (int j = 0; j < count; j++)
					{
						triangles[0].Add(num2 + this.polygonIndicesArray[j]);
						triangles[1].Add(num3 + this.polygonIndicesArray[num9]);
						num9--;
					}
					num2 += polygon4.Points.Length;
					num3 += polygon4.Points.Length;
				}
			}
		}

		// Token: 0x04001255 RID: 4693
		private List<int>[] triangles;

		// Token: 0x04001256 RID: 4694
		private List<Vector3>[] vertices;

		// Token: 0x04001257 RID: 4695
		private List<Vector3>[] normals;

		// Token: 0x04001258 RID: 4696
		private List<Vector2>[] uvs;

		// Token: 0x04001259 RID: 4697
		private List<Vector4>[] tangents;

		// Token: 0x0400125A RID: 4698
		private List<Color32>[] vertexColors;

		// Token: 0x0400125B RID: 4699
		private List<int> cutTris;

		// Token: 0x0400125C RID: 4700
		private int[] triCache;

		// Token: 0x0400125D RID: 4701
		private Vector3[] centroid;

		// Token: 0x0400125E RID: 4702
		private int[] triCounter;

		// Token: 0x0400125F RID: 4703
		private Array<int> polygonIndicesArray;

		// Token: 0x04001260 RID: 4704
		private Contour contour;

		// Token: 0x04001261 RID: 4705
		private Dictionary<long, int>[] cutVertCache;

		// Token: 0x04001262 RID: 4706
		private Dictionary<int, int>[] cornerVertCache;

		// Token: 0x04001263 RID: 4707
		private int contourBufferSize;

		// Token: 0x04001264 RID: 4708
		private Color crossSectionVertexColour;

		// Token: 0x04001265 RID: 4709
		private Vector4 crossSectionUV;

		// Token: 0x020003A7 RID: 935
		private struct Triangle
		{
			// Token: 0x04001266 RID: 4710
			public int[] ids;

			// Token: 0x04001267 RID: 4711
			public Vector3[] pos;

			// Token: 0x04001268 RID: 4712
			public Vector3[] normal;

			// Token: 0x04001269 RID: 4713
			public Vector2[] uvs;

			// Token: 0x0400126A RID: 4714
			public Vector4[] tangents;

			// Token: 0x0400126B RID: 4715
			public Color32[] colors;
		}
	}
}
