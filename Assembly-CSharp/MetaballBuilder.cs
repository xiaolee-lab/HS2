using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A4A RID: 2634
public class MetaballBuilder
{
	// Token: 0x06004E43 RID: 20035 RVA: 0x001DF4D4 File Offset: 0x001DD8D4
	static MetaballBuilder()
	{
		MetaballBuilder.__InitCubePatterns();
	}

	// Token: 0x06004E45 RID: 20037 RVA: 0x001DF91C File Offset: 0x001DDD1C
	private static void __InitCubePatterns()
	{
		for (int i = 0; i < 256; i++)
		{
			MetaballBuilder.__cubePatterns[i] = new MetaballBuilder.MB3DCubePattern();
			MetaballBuilder.__cubePatterns[i].Init((int)((sbyte)i));
		}
		MetaballBuilder.__bCubePatternsInitialized = true;
	}

	// Token: 0x06004E46 RID: 20038 RVA: 0x001DF960 File Offset: 0x001DDD60
	private static float CalcPower(Vector3 relativePos, float radius, float density)
	{
		float num = relativePos.magnitude / radius;
		if (num > 1f)
		{
			return 0f;
		}
		return density * Mathf.Max((1f - num) * (1f - num), 0f);
	}

	// Token: 0x17000E9D RID: 3741
	// (get) Token: 0x06004E47 RID: 20039 RVA: 0x001DF9A3 File Offset: 0x001DDDA3
	public static int MaxGridCellCount
	{
		get
		{
			return 1000000;
		}
	}

	// Token: 0x17000E9E RID: 3742
	// (get) Token: 0x06004E48 RID: 20040 RVA: 0x001DF9AA File Offset: 0x001DDDAA
	public static int MaxVertexCount
	{
		get
		{
			return 300000;
		}
	}

	// Token: 0x17000E9F RID: 3743
	// (get) Token: 0x06004E49 RID: 20041 RVA: 0x001DF9B1 File Offset: 0x001DDDB1
	public static MetaballBuilder Instance
	{
		get
		{
			if (MetaballBuilder._instance == null)
			{
				MetaballBuilder._instance = new MetaballBuilder();
			}
			return MetaballBuilder._instance;
		}
	}

	// Token: 0x06004E4A RID: 20042 RVA: 0x001DF9CC File Offset: 0x001DDDCC
	public string CreateMesh(MetaballCellClusterInterface rootCell, Transform root, float powerThreshold, float gridCellSize, Vector3 uDir, Vector3 vDir, Vector3 uvOffset, out Mesh out_mesh, MetaballCellObject cellObjPrefab = null, bool bReverse = false, Bounds? fixedBounds = null, bool bAutoGridSize = false, float autoGridQuarity = 0.2f)
	{
		Mesh mesh = new Mesh();
		Bounds value;
		MetaballBuilder.MetaballPointInfo[] points;
		this.AnalyzeCellCluster(rootCell, root, out value, out points, cellObjPrefab);
		if (fixedBounds != null)
		{
			value = fixedBounds.Value;
		}
		if (bAutoGridSize)
		{
			int num = (int)(1000000f * Mathf.Clamp01(autoGridQuarity));
			gridCellSize = Mathf.Pow(value.size.x * value.size.y * value.size.z / (float)num, 0.33333334f);
		}
		float num2 = (float)((int)(value.size.x / gridCellSize) * (int)(value.size.y / gridCellSize) * (int)(value.size.z / gridCellSize));
		if (num2 > 1000000f)
		{
			out_mesh = mesh;
			return string.Concat(new string[]
			{
				"Too many grid cells for building mesh (",
				num2.ToString(),
				" > ",
				1000000.ToString(),
				" ).",
				Environment.NewLine,
				"Make the area smaller or set larger (MetaballSeedBase.gridSize)"
			});
		}
		this.BuildMetaballMesh(mesh, value.center, value.extents, gridCellSize, points, powerThreshold, bReverse, uDir, vDir, uvOffset);
		out_mesh = mesh;
		return null;
	}

	// Token: 0x06004E4B RID: 20043 RVA: 0x001DFB2C File Offset: 0x001DDF2C
	public string CreateMeshWithSkeleton(SkinnedMetaballCell rootCell, Transform root, float powerThreshold, float gridCellSize, Vector3 uDir, Vector3 vDir, Vector3 uvOffset, out Mesh out_mesh, out Transform[] out_bones, MetaballCellObject cellObjPrefab = null, bool bReverse = false, Bounds? fixedBounds = null, bool bAutoGridSize = false, float autoGridQuarity = 0.2f)
	{
		Mesh mesh = new Mesh();
		Bounds value;
		Transform[] array;
		Matrix4x4[] bindposes;
		MetaballBuilder.MetaballPointInfo[] points;
		this.AnalyzeCellClusterWithSkeleton(rootCell, root, out value, out array, out bindposes, out points, cellObjPrefab);
		if (fixedBounds != null)
		{
			value = fixedBounds.Value;
		}
		if (bAutoGridSize)
		{
			int num = (int)(1000000f * Mathf.Clamp01(autoGridQuarity));
			gridCellSize = Mathf.Pow(value.size.x * value.size.y * value.size.z / (float)num, 0.33333334f);
		}
		mesh.bindposes = bindposes;
		float num2 = (float)((int)(value.size.x / gridCellSize) * (int)(value.size.y / gridCellSize) * (int)(value.size.z / gridCellSize));
		if (num2 > 1000000f)
		{
			out_mesh = mesh;
			out_bones = array;
			return string.Concat(new string[]
			{
				"Too many grid cells for building mesh (",
				num2.ToString(),
				" > ",
				1000000.ToString(),
				" ).",
				Environment.NewLine,
				"Make the area smaller or set larger (MetaballSeedBase.gridSize)"
			});
		}
		this.BuildMetaballMesh(mesh, value.center, value.extents, gridCellSize, points, powerThreshold, bReverse, uDir, vDir, uvOffset);
		out_mesh = mesh;
		out_bones = array;
		return null;
	}

	// Token: 0x06004E4C RID: 20044 RVA: 0x001DFCA0 File Offset: 0x001DE0A0
	private void AnalyzeCellCluster(MetaballCellClusterInterface cellCluster, Transform root, out Bounds bounds, out MetaballBuilder.MetaballPointInfo[] ballPoints, MetaballCellObject cellObjPrefab = null)
	{
		int cellCount = cellCluster.CellCount;
		Bounds tmpBounds = new Bounds(Vector3.zero, Vector3.zero);
		MetaballBuilder.MetaballPointInfo[] tmpBallPoints = new MetaballBuilder.MetaballPointInfo[cellCount];
		int cellIdx = 0;
		cellCluster.DoForeachCell(delegate(MetaballCell c)
		{
			for (int i = 0; i < 3; i++)
			{
				if (c.modelPosition[i] - c.radius < tmpBounds.min[i])
				{
					Vector3 min = tmpBounds.min;
					min[i] = c.modelPosition[i] - c.radius;
					tmpBounds.min = min;
				}
				if (c.modelPosition[i] + c.radius > tmpBounds.max[i])
				{
					Vector3 max = tmpBounds.max;
					max[i] = c.modelPosition[i] + c.radius;
					tmpBounds.max = max;
				}
			}
			GameObject gameObject;
			if (cellObjPrefab)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(cellObjPrefab.gameObject);
				gameObject.GetComponent<MetaballCellObject>().Setup(c);
			}
			else
			{
				gameObject = new GameObject();
			}
			if (!string.IsNullOrEmpty(c.tag))
			{
				gameObject.name = c.tag + "_Bone";
			}
			else
			{
				gameObject.name = "Bone";
			}
			Transform transform = gameObject.transform;
			transform.parent = root;
			transform.localPosition = c.modelPosition;
			transform.localRotation = c.modelRotation;
			transform.localScale = Vector3.one;
			MetaballBuilder.MetaballPointInfo metaballPointInfo = new MetaballBuilder.MetaballPointInfo();
			metaballPointInfo.center = c.modelPosition;
			metaballPointInfo.radius = c.radius;
			metaballPointInfo.density = c.density;
			tmpBallPoints[cellIdx] = metaballPointInfo;
			cellIdx++;
		});
		bounds = tmpBounds;
		ballPoints = tmpBallPoints;
	}

	// Token: 0x06004E4D RID: 20045 RVA: 0x001DFD18 File Offset: 0x001DE118
	private void AnalyzeCellClusterWithSkeleton(SkinnedMetaballCell rootCell, Transform root, out Bounds bounds, out Transform[] bones, out Matrix4x4[] bindPoses, out MetaballBuilder.MetaballPointInfo[] ballPoints, MetaballCellObject cellObjPrefab = null)
	{
		int cellCount = rootCell.CellCount;
		Transform[] tmpBones = new Transform[cellCount];
		Matrix4x4[] tmpBindPoses = new Matrix4x4[cellCount];
		Bounds tmpBounds = new Bounds(Vector3.zero, Vector3.zero);
		MetaballBuilder.MetaballPointInfo[] tmpBallPoints = new MetaballBuilder.MetaballPointInfo[cellCount];
		Dictionary<SkinnedMetaballCell, int> boneDictionary = new Dictionary<SkinnedMetaballCell, int>();
		int cellIdx = 0;
		rootCell.DoForeachSkinnedCell(delegate(SkinnedMetaballCell c)
		{
			for (int i = 0; i < 3; i++)
			{
				if (c.modelPosition[i] - c.radius < tmpBounds.min[i])
				{
					Vector3 min = tmpBounds.min;
					min[i] = c.modelPosition[i] - c.radius;
					tmpBounds.min = min;
				}
				if (c.modelPosition[i] + c.radius > tmpBounds.max[i])
				{
					Vector3 max = tmpBounds.max;
					max[i] = c.modelPosition[i] + c.radius;
					tmpBounds.max = max;
				}
			}
			GameObject gameObject;
			if (cellObjPrefab)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(cellObjPrefab.gameObject);
				gameObject.GetComponent<MetaballCellObject>().Setup(c);
			}
			else
			{
				gameObject = new GameObject();
			}
			if (!string.IsNullOrEmpty(c.tag))
			{
				gameObject.name = c.tag + "_Bone";
			}
			else
			{
				gameObject.name = "Bone";
			}
			Transform transform = gameObject.transform;
			if (c.IsRoot)
			{
				transform.parent = root;
				transform.localPosition = Vector3.zero;
				transform.localRotation = c.modelRotation;
				transform.localScale = Vector3.one;
			}
			else
			{
				Transform parent = tmpBones[boneDictionary[c.parent]];
				transform.parent = root;
				transform.localPosition = c.parent.modelPosition;
				transform.localRotation = c.modelRotation;
				transform.localScale = Vector3.one;
				transform.parent = parent;
			}
			tmpBones[cellIdx] = transform;
			tmpBindPoses[cellIdx] = tmpBones[cellIdx].worldToLocalMatrix * root.localToWorldMatrix;
			boneDictionary.Add(c, cellIdx);
			MetaballBuilder.MetaballPointInfo metaballPointInfo = new MetaballBuilder.MetaballPointInfo();
			metaballPointInfo.center = c.modelPosition;
			metaballPointInfo.radius = c.radius;
			metaballPointInfo.density = c.density;
			tmpBallPoints[cellIdx] = metaballPointInfo;
			cellIdx++;
		});
		bounds = tmpBounds;
		bones = tmpBones;
		bindPoses = tmpBindPoses;
		ballPoints = tmpBallPoints;
	}

	// Token: 0x06004E4E RID: 20046 RVA: 0x001DFDC8 File Offset: 0x001DE1C8
	public Mesh CreateImplicitSurfaceMesh(int countX, int countY, int countZ, Vector3[] positionMap, float[] powerMap, bool bReverse, float threshold, Vector3 uDir, Vector3 vDir, Vector3 uvOffset)
	{
		if (!MetaballBuilder.__bCubePatternsInitialized)
		{
			MetaballBuilder.__InitCubePatterns();
		}
		int num = countX * countY * countZ;
		Vector3[] array = new Vector3[num];
		int[] array2 = new int[num * 3];
		bool[] array3 = new bool[num];
		int num2 = countX * countY;
		int num3 = countX * countY * countZ;
		for (int i = 0; i < num * 3; i++)
		{
			array2[i] = -1;
		}
		for (int j = 0; j < num; j++)
		{
			float num4 = powerMap[j] - threshold;
			array3[j] = (num4 >= 0f);
			if (array3[j] && num4 < 0.001f)
			{
				powerMap[j] = threshold + 0.001f;
			}
		}
		for (int k = 1; k < countZ - 1; k++)
		{
			for (int l = 1; l < countY - 1; l++)
			{
				for (int m = 1; m < countX - 1; m++)
				{
					int num5 = m + l * countX + k * num2;
					Vector3 vector;
					vector.x = powerMap[num5 + 1] - powerMap[num5 - 1];
					vector.y = powerMap[num5 + countX] - powerMap[num5 - countX];
					vector.z = powerMap[num5 + num2] - powerMap[num5 - num2];
					if (vector.sqrMagnitude > 0.001f)
					{
						vector.Normalize();
					}
					array[num5] = vector;
				}
			}
		}
		int num6 = 0;
		List<Vector3> list = new List<Vector3>();
		List<Vector3> list2 = new List<Vector3>();
		List<Vector2> list3 = new List<Vector2>();
		int num7 = 0;
		while (num7 < countZ && num6 < 299999)
		{
			int num8 = 0;
			while (num8 < countY && num6 < 299999)
			{
				int num9 = 0;
				while (num9 < countX && num6 < 299999)
				{
					int num10 = 0;
					while (num10 < 3 && num6 < 299999)
					{
						int num11 = (num10 != 0) ? 0 : 1;
						int num12 = (num10 != 1) ? 0 : 1;
						int num13 = (num10 != 2) ? 0 : 1;
						if (num7 + num13 < countZ && num8 + num12 < countY && num9 + num11 < countX)
						{
							int num14 = num9 + num8 * countX + num7 * num2;
							int num15 = num9 + num11 + (num8 + num12) * countX + (num7 + num13) * num2;
							float num16 = powerMap[num14];
							float num17 = powerMap[num15];
							if ((num16 - threshold) * (num17 - threshold) < 0f)
							{
								float num18 = (threshold - num16) / (num17 - num16);
								Vector3 vector2 = positionMap[num15] * num18 + positionMap[num14] * (1f - num18);
								list.Add(vector2);
								Vector3 lhs = vector2 + uvOffset;
								list3.Add(new Vector2(Vector3.Dot(lhs, uDir), Vector3.Dot(lhs, vDir)));
								Vector3 vector3 = -(array[num15] * num18 + array[num14] * (1f - num18)).normalized;
								list2.Add((!bReverse) ? vector3 : (-vector3));
								array2[num10 * num3 + num14] = num6;
								num6++;
							}
						}
						num10++;
					}
					num9++;
				}
				num8++;
			}
			num7++;
		}
		int[] array4 = new int[15];
		int num19 = 0;
		List<int> list4 = new List<int>();
		if (num6 > 3)
		{
			for (int n = 0; n < countZ - 1; n++)
			{
				for (int num20 = 0; num20 < countY - 1; num20++)
				{
					for (int num21 = 0; num21 < countX - 1; num21++)
					{
						byte b = 0;
						for (int num22 = 0; num22 < 2; num22++)
						{
							for (int num23 = 0; num23 < 2; num23++)
							{
								for (int num24 = 0; num24 < 2; num24++)
								{
									if (array3[num21 + num24 + (num20 + num23) * countX + (n + num22) * num2])
									{
										b |= (byte)(1 << num22 * 4 + num23 * 2 + num24);
									}
								}
							}
						}
						int[] array5 = new int[12];
						for (int num25 = 0; num25 < 3; num25++)
						{
							for (int num26 = 0; num26 < 2; num26++)
							{
								for (int num27 = 0; num27 < 2; num27++)
								{
									int num28;
									int num29;
									int num30;
									switch (num25)
									{
									case 0:
										num28 = num21;
										num29 = num20 + num26;
										num30 = n + num27;
										break;
									case 1:
										num28 = num21 + num27;
										num29 = num20;
										num30 = n + num26;
										break;
									case 2:
										num28 = num21 + num26;
										num29 = num20 + num27;
										num30 = n;
										break;
									default:
										num29 = (num28 = (num30 = -1));
										break;
									}
									int num31 = num25 * 4 + num27 * 2 + num26;
									array5[num31] = array2[num25 * num3 + num28 + num29 * countX + num30 * num2];
								}
							}
						}
						int primaryPatternIndex = MetaballBuilder.__cubePatterns[(int)b].MatchingInfo.PrimaryPatternIndex;
						array4[primaryPatternIndex]++;
						if (!false)
						{
							for (int num32 = 0; num32 < MetaballBuilder.__cubePatterns[(int)b].MatchingInfo.GetTargetPrimitiveIndexBuffer().Length; num32++)
							{
								list4.Add(array5[MetaballBuilder.__cubePatterns[(int)b].IndexBuf[num32]]);
								num19++;
							}
						}
					}
				}
			}
		}
		Mesh mesh = new Mesh();
		mesh.vertices = list.ToArray();
		mesh.uv = list3.ToArray();
		mesh.normals = list2.ToArray();
		if (!bReverse)
		{
			mesh.triangles = list4.ToArray();
		}
		else
		{
			list4.Reverse();
			mesh.triangles = list4.ToArray();
		}
		return mesh;
	}

	// Token: 0x06004E4F RID: 20047 RVA: 0x001E0400 File Offset: 0x001DE800
	private void BuildMetaballMesh(Mesh mesh, Vector3 center, Vector3 extent, float cellSize, MetaballBuilder.MetaballPointInfo[] points, float powerThreshold, bool bReverse, Vector3 uDir, Vector3 vDir, Vector3 uvOffset)
	{
		if (!MetaballBuilder.__bCubePatternsInitialized)
		{
			MetaballBuilder.__InitCubePatterns();
		}
		int num = Mathf.CeilToInt(extent.x / cellSize) + 1;
		int num2 = Mathf.CeilToInt(extent.y / cellSize) + 1;
		int num3 = Mathf.CeilToInt(extent.z / cellSize) + 1;
		int num4 = num * 2;
		int num5 = num2 * 2;
		int num6 = num3 * 2;
		int num7 = num4;
		int num8 = num5;
		int num9 = num6;
		Vector3 b = new Vector3((float)num * cellSize, (float)num2 * cellSize, (float)num3 * cellSize);
		Vector3 a = center - b;
		float[] array = new float[num4 * num5 * num6];
		Vector3[] array2 = new Vector3[num4 * num5 * num6];
		Vector3[] array3 = new Vector3[num4 * num5 * num6];
		int[] array4 = new int[num4 * num5 * num6 * 3];
		bool[] array5 = new bool[num4 * num5 * num6];
		BoneWeight[] array6 = new BoneWeight[num4 * num5 * num6];
		int num10 = num7;
		int num11 = num7 * num8;
		int num12 = num7 * num8 * num9;
		for (int i = 0; i < num9; i++)
		{
			for (int j = 0; j < num8; j++)
			{
				for (int k = 0; k < num7; k++)
				{
					array2[k + j * num10 + i * num11] = a + new Vector3(cellSize * (float)k, cellSize * (float)j, cellSize * (float)i);
				}
			}
		}
		for (int l = 0; l < 3 * num9 * num8 * num7; l++)
		{
			array4[l] = -1;
		}
		int num13 = 0;
		foreach (MetaballBuilder.MetaballPointInfo metaballPointInfo in points)
		{
			int num14 = (int)Mathf.Floor((metaballPointInfo.center.x - center.x - metaballPointInfo.radius) / cellSize) + num;
			int num15 = (int)Mathf.Floor((metaballPointInfo.center.y - center.y - metaballPointInfo.radius) / cellSize) + num2;
			int num16 = (int)Mathf.Floor((metaballPointInfo.center.z - center.z - metaballPointInfo.radius) / cellSize) + num3;
			num14 = Mathf.Max(0, num14);
			num15 = Mathf.Max(0, num15);
			num16 = Mathf.Max(0, num16);
			int num17 = (int)Mathf.Ceil((metaballPointInfo.center.x - center.x + metaballPointInfo.radius) / cellSize) + num;
			int num18 = (int)Mathf.Ceil((metaballPointInfo.center.y - center.y + metaballPointInfo.radius) / cellSize) + num2;
			int num19 = (int)Mathf.Ceil((metaballPointInfo.center.z - center.z + metaballPointInfo.radius) / cellSize) + num3;
			num17 = Mathf.Min(num7 - 1, num17);
			num18 = Mathf.Min(num8 - 1, num18);
			num19 = Mathf.Min(num9 - 1, num19);
			for (int n = num16; n <= num19; n++)
			{
				for (int num20 = num15; num20 <= num18; num20++)
				{
					for (int num21 = num14; num21 <= num17; num21++)
					{
						int num22 = num21 + num20 * num10 + n * num11;
						Vector3 a2 = array2[num22];
						float num23 = MetaballBuilder.CalcPower(a2 - metaballPointInfo.center, metaballPointInfo.radius, metaballPointInfo.density);
						array[num22] += num23;
						if (num23 > 0f)
						{
							BoneWeight boneWeight = array6[num22];
							if (boneWeight.weight0 < num23 || boneWeight.weight1 < num23)
							{
								if (boneWeight.weight0 < boneWeight.weight1)
								{
									boneWeight.weight0 = num23;
									boneWeight.boneIndex0 = num13;
								}
								else
								{
									boneWeight.weight1 = num23;
									boneWeight.boneIndex1 = num13;
								}
							}
							array6[num22] = boneWeight;
						}
					}
				}
			}
			num13++;
		}
		for (int num24 = 0; num24 < num7 * num8 * num9; num24++)
		{
			array5[num24] = (array[num24] >= powerThreshold);
			if (array5[num24])
			{
				float num25 = 0.001f;
				if (Mathf.Abs(array[num24] - powerThreshold) < num25)
				{
					array[num24] = ((array[num24] - powerThreshold < 0f) ? (powerThreshold - num25) : (powerThreshold + num25));
				}
			}
		}
		for (int num26 = 1; num26 < num9 - 1; num26++)
		{
			for (int num27 = 1; num27 < num8 - 1; num27++)
			{
				for (int num28 = 1; num28 < num7 - 1; num28++)
				{
					array3[num28 + num27 * num10 + num26 * num11].x = array[num28 + 1 + num27 * num10 + num26 * num11] - array[num28 - 1 + num27 * num10 + num26 * num11];
					array3[num28 + num27 * num10 + num26 * num11].y = array[num28 + (num27 + 1) * num10 + num26 * num11] - array[num28 + (num27 - 1) * num10 + num26 * num11];
					array3[num28 + num27 * num10 + num26 * num11].z = array[num28 + num27 * num10 + (num26 + 1) * num11] - array[num28 + num27 * num10 + (num26 - 1) * num11];
					int num29 = num28 + num27 * num10 + num26 * num11;
					if (array3[num29].sqrMagnitude > 0.001f)
					{
						array3[num29].Normalize();
					}
				}
			}
		}
		int num30 = 0;
		List<Vector3> list = new List<Vector3>();
		List<Vector3> list2 = new List<Vector3>();
		List<Vector2> list3 = new List<Vector2>();
		int num31 = 0;
		while (num31 < num9 && num30 < 299999)
		{
			int num32 = 0;
			while (num32 < num8 && num30 < 299999)
			{
				int num33 = 0;
				while (num33 < num7 && num30 < 299999)
				{
					int num34 = 0;
					while (num34 < 3 && num30 < 299999)
					{
						int num35 = (num34 != 0) ? 0 : 1;
						int num36 = (num34 != 1) ? 0 : 1;
						int num37 = (num34 != 2) ? 0 : 1;
						if (num31 + num37 < num9 && num32 + num36 < num8 && num33 + num35 < num7)
						{
							int num38 = num33 + num32 * num10 + num31 * num11;
							int num39 = num33 + num35 + (num32 + num36) * num10 + (num31 + num37) * num11;
							float num40 = array[num38];
							float num41 = array[num39];
							if ((num40 - powerThreshold) * (num41 - powerThreshold) < 0f)
							{
								float num42 = (powerThreshold - num40) / (num41 - num40);
								Vector3 vector = array2[num39] * num42 + array2[num38] * (1f - num42);
								list.Add(vector);
								Vector3 lhs = vector + uvOffset;
								list3.Add(new Vector2(Vector3.Dot(lhs, uDir), Vector3.Dot(lhs, vDir)));
								Vector3 vector2 = -(array3[num39] * num42 + array3[num38] * (1f - num42)).normalized;
								list2.Add((!bReverse) ? vector2 : (-vector2));
								array4[num34 * num12 + num38] = num30;
								num30++;
							}
						}
						num34++;
					}
					num33++;
				}
				num32++;
			}
			num31++;
		}
		int[] array7 = new int[15];
		int[] array8 = new int[12];
		List<int> list4 = new List<int>();
		if (num30 > 3)
		{
			for (int num43 = 0; num43 < num9 - 1; num43++)
			{
				for (int num44 = 0; num44 < num8 - 1; num44++)
				{
					for (int num45 = 0; num45 < num7 - 1; num45++)
					{
						byte b2 = 0;
						for (int num46 = 0; num46 < 2; num46++)
						{
							for (int num47 = 0; num47 < 2; num47++)
							{
								for (int num48 = 0; num48 < 2; num48++)
								{
									if (array5[num45 + num48 + (num44 + num47) * num10 + (num43 + num46) * num11])
									{
										b2 |= (byte)(1 << num46 * 4 + num47 * 2 + num48);
									}
								}
							}
						}
						for (int num49 = 0; num49 < 3; num49++)
						{
							for (int num50 = 0; num50 < 2; num50++)
							{
								for (int num51 = 0; num51 < 2; num51++)
								{
									int num52;
									int num53;
									int num54;
									switch (num49)
									{
									case 0:
										num52 = num45;
										num53 = num44 + num50;
										num54 = num43 + num51;
										break;
									case 1:
										num52 = num45 + num51;
										num53 = num44;
										num54 = num43 + num50;
										break;
									case 2:
										num52 = num45 + num50;
										num53 = num44 + num51;
										num54 = num43;
										break;
									default:
										num53 = (num52 = (num54 = -1));
										break;
									}
									int num55 = num49 * 4 + num51 * 2 + num50;
									array8[num55] = array4[num49 * num12 + num52 + num53 * num10 + num54 * num11];
								}
							}
						}
						int primaryPatternIndex = MetaballBuilder.__cubePatterns[(int)b2].MatchingInfo.PrimaryPatternIndex;
						array7[primaryPatternIndex]++;
						for (int num56 = 0; num56 < MetaballBuilder.__cubePatterns[(int)b2].MatchingInfo.GetTargetPrimitiveIndexBuffer().Length; num56++)
						{
							if (array8[MetaballBuilder.__cubePatterns[(int)b2].IndexBuf[num56]] < 0)
							{
								string str = string.Empty;
								for (int num57 = 0; num57 < 12; num57++)
								{
									str = str + array8[num57].ToString() + ",";
								}
								string str2 = string.Empty;
								for (int num58 = 0; num58 < 2; num58++)
								{
									for (int num59 = 0; num59 < 2; num59++)
									{
										for (int num60 = 0; num60 < 2; num60++)
										{
											int num61 = num45 + num60 + (num44 + num59) * num10 + (num43 + num58) * num11;
											str2 = str2 + array[num61].ToString() + ",";
										}
									}
								}
								throw new UnityException("vertex error");
							}
							list4.Add(array8[MetaballBuilder.__cubePatterns[(int)b2].IndexBuf[num56]]);
						}
					}
				}
			}
		}
		mesh.vertices = list.ToArray();
		mesh.uv = list3.ToArray();
		mesh.normals = list2.ToArray();
		if (!bReverse)
		{
			mesh.triangles = list4.ToArray();
		}
		else
		{
			list4.Reverse();
			mesh.triangles = list4.ToArray();
		}
	}

	// Token: 0x0400476A RID: 18282
	private static bool __bCubePatternsInitialized = false;

	// Token: 0x0400476B RID: 18283
	private static MetaballBuilder.MB3DCubePattern[] __cubePatterns = new MetaballBuilder.MB3DCubePattern[256];

	// Token: 0x0400476C RID: 18284
	private const int MB3D_PATTERN_COUNT = 15;

	// Token: 0x0400476D RID: 18285
	private static MetaballBuilder.MB3DCubePrimitivePattern[] __primitivePatterns = new MetaballBuilder.MB3DCubePrimitivePattern[]
	{
		new MetaballBuilder.MB3DCubePrimitivePattern
		{
			InOut = new MetaballBuilder.MB3DCubeInOut(0, 0, 0, 0, 0, 0, 0, 0),
			IndexBuf = new int[0]
		},
		new MetaballBuilder.MB3DCubePrimitivePattern
		{
			InOut = new MetaballBuilder.MB3DCubeInOut(1, 0, 0, 0, 0, 0, 0, 0),
			IndexBuf = new int[]
			{
				0,
				4,
				8
			}
		},
		new MetaballBuilder.MB3DCubePrimitivePattern
		{
			InOut = new MetaballBuilder.MB3DCubeInOut(1, 0, 1, 0, 0, 0, 0, 0),
			IndexBuf = new int[]
			{
				1,
				10,
				0,
				8,
				0,
				10
			}
		},
		new MetaballBuilder.MB3DCubePrimitivePattern
		{
			InOut = new MetaballBuilder.MB3DCubeInOut(1, 0, 0, 0, 0, 0, 1, 0),
			IndexBuf = new int[]
			{
				0,
				4,
				8,
				3,
				5,
				10
			},
			IndexBufAlter = new int[]
			{
				0,
				3,
				8,
				5,
				8,
				3,
				0,
				4,
				3,
				10,
				3,
				4
			}
		},
		new MetaballBuilder.MB3DCubePrimitivePattern
		{
			InOut = new MetaballBuilder.MB3DCubeInOut(1, 0, 0, 0, 0, 0, 0, 1),
			IndexBuf = new int[]
			{
				0,
				4,
				8,
				3,
				11,
				7
			}
		},
		new MetaballBuilder.MB3DCubePrimitivePattern
		{
			InOut = new MetaballBuilder.MB3DCubeInOut(0, 1, 1, 1, 0, 0, 0, 0),
			IndexBuf = new int[]
			{
				10,
				4,
				0,
				10,
				0,
				9,
				10,
				9,
				11
			}
		},
		new MetaballBuilder.MB3DCubePrimitivePattern
		{
			InOut = new MetaballBuilder.MB3DCubeInOut(1, 0, 1, 0, 0, 0, 0, 1),
			IndexBuf = new int[]
			{
				1,
				10,
				0,
				8,
				0,
				10,
				3,
				11,
				7
			},
			IndexBufAlter = new int[]
			{
				3,
				10,
				7,
				10,
				8,
				7,
				8,
				0,
				7,
				0,
				1,
				7,
				1,
				11,
				7
			}
		},
		new MetaballBuilder.MB3DCubePrimitivePattern
		{
			InOut = new MetaballBuilder.MB3DCubeInOut(0, 0, 1, 0, 1, 0, 0, 1),
			IndexBuf = new int[]
			{
				10,
				4,
				1,
				2,
				8,
				5,
				3,
				11,
				7
			}
		},
		new MetaballBuilder.MB3DCubePrimitivePattern
		{
			InOut = new MetaballBuilder.MB3DCubeInOut(1, 1, 1, 1, 0, 0, 0, 0),
			IndexBuf = new int[]
			{
				10,
				8,
				11,
				9,
				11,
				8
			}
		},
		new MetaballBuilder.MB3DCubePrimitivePattern
		{
			InOut = new MetaballBuilder.MB3DCubeInOut(1, 1, 0, 1, 0, 1, 0, 0),
			IndexBuf = new int[]
			{
				2,
				7,
				8,
				8,
				7,
				4,
				4,
				7,
				11,
				4,
				11,
				1
			}
		},
		new MetaballBuilder.MB3DCubePrimitivePattern
		{
			InOut = new MetaballBuilder.MB3DCubeInOut(1, 0, 0, 1, 1, 0, 0, 1),
			IndexBuf = new int[]
			{
				2,
				0,
				5,
				4,
				5,
				0,
				3,
				1,
				7,
				6,
				7,
				1
			}
		},
		new MetaballBuilder.MB3DCubePrimitivePattern
		{
			InOut = new MetaballBuilder.MB3DCubeInOut(1, 1, 0, 1, 0, 0, 0, 1),
			IndexBuf = new int[]
			{
				8,
				9,
				4,
				4,
				9,
				3,
				7,
				3,
				9,
				1,
				4,
				3
			}
		},
		new MetaballBuilder.MB3DCubePrimitivePattern
		{
			InOut = new MetaballBuilder.MB3DCubeInOut(0, 1, 1, 1, 1, 0, 0, 0),
			IndexBuf = new int[]
			{
				2,
				8,
				5,
				10,
				4,
				0,
				10,
				0,
				9,
				10,
				9,
				11
			}
		},
		new MetaballBuilder.MB3DCubePrimitivePattern
		{
			InOut = new MetaballBuilder.MB3DCubeInOut(1, 0, 0, 1, 0, 1, 1, 0),
			IndexBuf = new int[]
			{
				0,
				4,
				8,
				3,
				5,
				10,
				2,
				7,
				9,
				11,
				1,
				6
			}
		},
		new MetaballBuilder.MB3DCubePrimitivePattern
		{
			InOut = new MetaballBuilder.MB3DCubeInOut(0, 1, 1, 1, 0, 1, 0, 0),
			IndexBuf = new int[]
			{
				2,
				4,
				0,
				2,
				11,
				4,
				7,
				11,
				2,
				10,
				4,
				11
			}
		}
	};

	// Token: 0x0400476E RID: 18286
	private const int _maxGridCellCount = 1000000;

	// Token: 0x0400476F RID: 18287
	private const int _maxVertexCount = 300000;

	// Token: 0x04004770 RID: 18288
	private static MetaballBuilder _instance;

	// Token: 0x02000A4B RID: 2635
	private class MB3DCubeVector
	{
		// Token: 0x06004E50 RID: 20048 RVA: 0x001E0F6A File Offset: 0x001DF36A
		public MB3DCubeVector()
		{
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x001E0F85 File Offset: 0x001DF385
		public MB3DCubeVector(sbyte x_, sbyte y_, sbyte z_)
		{
			this.x = x_;
			this.y = y_;
			this.z = z_;
			this.axisIdx = -1;
			this.CalcAxis();
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06004E52 RID: 20050 RVA: 0x001E0FC2 File Offset: 0x001DF3C2
		// (set) Token: 0x06004E53 RID: 20051 RVA: 0x001E0FCC File Offset: 0x001DF3CC
		public sbyte x
		{
			get
			{
				return this.e[0];
			}
			set
			{
				this.e[0] = value;
			}
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06004E54 RID: 20052 RVA: 0x001E0FD7 File Offset: 0x001DF3D7
		// (set) Token: 0x06004E55 RID: 20053 RVA: 0x001E0FE1 File Offset: 0x001DF3E1
		public sbyte y
		{
			get
			{
				return this.e[1];
			}
			set
			{
				this.e[1] = value;
			}
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06004E56 RID: 20054 RVA: 0x001E0FEC File Offset: 0x001DF3EC
		// (set) Token: 0x06004E57 RID: 20055 RVA: 0x001E0FF6 File Offset: 0x001DF3F6
		public sbyte z
		{
			get
			{
				return this.e[2];
			}
			set
			{
				this.e[2] = value;
			}
		}

		// Token: 0x06004E58 RID: 20056 RVA: 0x001E1001 File Offset: 0x001DF401
		public static MetaballBuilder.MB3DCubeVector operator +(MetaballBuilder.MB3DCubeVector lh, MetaballBuilder.MB3DCubeVector rh)
		{
			return new MetaballBuilder.MB3DCubeVector((sbyte)((int)lh.x + (int)rh.x), (sbyte)((int)lh.y + (int)rh.y), (sbyte)((int)lh.z + (int)rh.z));
		}

		// Token: 0x06004E59 RID: 20057 RVA: 0x001E1038 File Offset: 0x001DF438
		private void CalcAxis()
		{
			sbyte b = 0;
			while ((int)b < 3)
			{
				if ((int)this.e[(int)b] != 0)
				{
					if ((int)this.axisIdx != -1)
					{
						this.axisIdx = -1;
						break;
					}
					this.axisIdx = b;
				}
				b = (sbyte)((int)b + 1);
			}
		}

		// Token: 0x06004E5A RID: 20058 RVA: 0x001E1090 File Offset: 0x001DF490
		public void SetAxisVector(sbyte axisIdx_, sbyte value)
		{
			sbyte b = 0;
			this.z = b;
			b = b;
			this.y = b;
			this.x = b;
			if ((int)value == 0)
			{
				this.axisIdx = -1;
			}
			else
			{
				this.axisIdx = axisIdx_;
				this.e[(int)this.axisIdx] = value;
			}
		}

		// Token: 0x06004E5B RID: 20059 RVA: 0x001E10DF File Offset: 0x001DF4DF
		public static MetaballBuilder.MB3DCubeVector operator *(MetaballBuilder.MB3DCubeVector lh, sbyte rh)
		{
			return new MetaballBuilder.MB3DCubeVector((sbyte)((int)lh.x * (int)rh), (sbyte)((int)lh.y * (int)rh), (sbyte)((int)lh.z * (int)rh));
		}

		// Token: 0x04004771 RID: 18289
		public sbyte[] e = new sbyte[3];

		// Token: 0x04004772 RID: 18290
		public sbyte axisIdx = -1;
	}

	// Token: 0x02000A4C RID: 2636
	private class MB3DCubeInOut
	{
		// Token: 0x06004E5C RID: 20060 RVA: 0x001E1107 File Offset: 0x001DF507
		public MB3DCubeInOut()
		{
		}

		// Token: 0x06004E5D RID: 20061 RVA: 0x001E1120 File Offset: 0x001DF520
		public MB3DCubeInOut(sbyte patternIdx)
		{
			sbyte[] array = new sbyte[8];
			for (int i = 0; i < 8; i++)
			{
				array[i] = (sbyte)((int)patternIdx >> i & 1);
			}
			this.Init(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7]);
		}

		// Token: 0x06004E5E RID: 20062 RVA: 0x001E1188 File Offset: 0x001DF588
		public MB3DCubeInOut(sbyte a0, sbyte a1, sbyte a2, sbyte a3, sbyte a4, sbyte a5, sbyte a6, sbyte a7)
		{
			this.Init(a0, a1, a2, a3, a4, a5, a6, a7);
		}

		// Token: 0x06004E5F RID: 20063 RVA: 0x001E11BC File Offset: 0x001DF5BC
		private void Init(sbyte a0, sbyte a1, sbyte a2, sbyte a3, sbyte a4, sbyte a5, sbyte a6, sbyte a7)
		{
			this.bFill[0, 0, 0] = a0;
			this.bFill[0, 0, 1] = a1;
			this.bFill[0, 1, 0] = a2;
			this.bFill[0, 1, 1] = a3;
			this.bFill[1, 0, 0] = a4;
			this.bFill[1, 0, 1] = a5;
			this.bFill[1, 1, 0] = a6;
			this.bFill[1, 1, 1] = a7;
			this.inCount = (int)a0 + (int)a1 + (int)a2 + (int)a3 + (int)a4 + (int)a5 + (int)a6 + (int)a7;
		}

		// Token: 0x06004E60 RID: 20064 RVA: 0x001E1268 File Offset: 0x001DF668
		public sbyte At(MetaballBuilder.MB3DCubeVector point)
		{
			return this.bFill[(int)point.z, (int)point.y, (int)point.x];
		}

		// Token: 0x04004773 RID: 18291
		public sbyte[,,] bFill = new sbyte[2, 2, 2];

		// Token: 0x04004774 RID: 18292
		public int inCount;
	}

	// Token: 0x02000A4D RID: 2637
	private struct MB3DCubePrimitivePattern
	{
		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06004E61 RID: 20065 RVA: 0x001E128A File Offset: 0x001DF68A
		public int IndexCount
		{
			get
			{
				if (this.IndexBuf != null)
				{
					return this.IndexBuf.Length;
				}
				return 0;
			}
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06004E62 RID: 20066 RVA: 0x001E12A1 File Offset: 0x001DF6A1
		public int IndexCountAlter
		{
			get
			{
				if (this.IndexBufAlter != null)
				{
					return this.IndexBufAlter.Length;
				}
				return 0;
			}
		}

		// Token: 0x04004775 RID: 18293
		public MetaballBuilder.MB3DCubeInOut InOut;

		// Token: 0x04004776 RID: 18294
		public int[] IndexBuf;

		// Token: 0x04004777 RID: 18295
		public int[] IndexBufAlter;
	}

	// Token: 0x02000A4E RID: 2638
	private class MB3DPatternMatchingInfo
	{
		// Token: 0x06004E64 RID: 20068 RVA: 0x001E12D8 File Offset: 0x001DF6D8
		public void Match(MetaballBuilder.MB3DCubeInOut src)
		{
			this.PrimaryPatternIndex = -1;
			this.bFlipInOut = (src.inCount > 4);
			int i = 0;
			while (i < 15)
			{
				MetaballBuilder.MB3DCubeInOut inOut = MetaballBuilder.__primitivePatterns[i].InOut;
				if (this.bFlipInOut)
				{
					if (8 - src.inCount == inOut.inCount)
					{
						goto IL_6C;
					}
				}
				else if (src.inCount == inOut.inCount)
				{
					goto IL_6C;
				}
				IL_2E2:
				i++;
				continue;
				IL_6C:
				sbyte[] array = new sbyte[3];
				this.Origin.x = 0;
				while ((int)this.Origin.x < 2)
				{
					array[0] = (((int)this.Origin.x == 0) ? 1 : -1);
					this.Origin.y = 0;
					while ((int)this.Origin.y < 2)
					{
						array[1] = (((int)this.Origin.y == 0) ? 1 : -1);
						this.Origin.z = 0;
						while ((int)this.Origin.z < 2)
						{
							array[2] = (((int)this.Origin.z == 0) ? 1 : -1);
							sbyte b = (((int)this.Origin.x + (int)this.Origin.y + (int)this.Origin.z) % 2 == 0) ? 1 : 2;
							sbyte b2 = 0;
							while ((int)b2 < 3)
							{
								this.Axis[0] = new MetaballBuilder.MB3DCubeVector();
								this.Axis[1] = new MetaballBuilder.MB3DCubeVector();
								this.Axis[2] = new MetaballBuilder.MB3DCubeVector();
								this.Axis[0].SetAxisVector(b2, array[(int)b2]);
								this.Axis[1].SetAxisVector((sbyte)(((int)b2 + (int)b) % 3), array[((int)b2 + (int)b) % 3]);
								this.Axis[2].SetAxisVector((sbyte)(((int)b2 + (int)b + (int)b) % 3), array[((int)b2 + (int)b + (int)b) % 3]);
								bool flag = true;
								sbyte b3 = 0;
								while ((int)b3 < 2)
								{
									sbyte b4 = 0;
									while ((int)b4 < 2)
									{
										sbyte b5 = 0;
										while ((int)b5 < 2)
										{
											MetaballBuilder.MB3DCubeVector point = this.SampleVertex(new MetaballBuilder.MB3DCubeVector(b3, b4, b5));
											if (this.bFlipInOut == ((int)src.At(point) == (int)inOut.bFill[(int)b5, (int)b4, (int)b3]))
											{
												flag = false;
											}
											b5 = (sbyte)((int)b5 + 1);
										}
										b4 = (sbyte)((int)b4 + 1);
									}
									b3 = (sbyte)((int)b3 + 1);
								}
								if (flag)
								{
									this.PrimaryPatternIndex = i;
									return;
								}
								b2 = (sbyte)((int)b2 + 1);
							}
							MetaballBuilder.MB3DCubeVector origin = this.Origin;
							origin.z = (sbyte)((int)origin.z + 1);
						}
						MetaballBuilder.MB3DCubeVector origin2 = this.Origin;
						origin2.y = (sbyte)((int)origin2.y + 1);
					}
					MetaballBuilder.MB3DCubeVector origin3 = this.Origin;
					origin3.x = (sbyte)((int)origin3.x + 1);
				}
				goto IL_2E2;
			}
		}

		// Token: 0x06004E65 RID: 20069 RVA: 0x001E15D4 File Offset: 0x001DF9D4
		public int[] GetTargetPrimitiveIndexBuffer()
		{
			return (!this.bFlipInOut || MetaballBuilder.__primitivePatterns[this.PrimaryPatternIndex].IndexCountAlter <= 0) ? MetaballBuilder.__primitivePatterns[this.PrimaryPatternIndex].IndexBuf : MetaballBuilder.__primitivePatterns[this.PrimaryPatternIndex].IndexBufAlter;
		}

		// Token: 0x06004E66 RID: 20070 RVA: 0x001E1638 File Offset: 0x001DFA38
		public MetaballBuilder.MB3DCubeVector SampleVertex(MetaballBuilder.MB3DCubeVector primaryPos)
		{
			return this.Origin + this.Axis[0] * primaryPos.x + this.Axis[1] * primaryPos.y + this.Axis[2] * primaryPos.z;
		}

		// Token: 0x06004E67 RID: 20071 RVA: 0x001E1694 File Offset: 0x001DFA94
		public void SampleSegment(sbyte primarySegmentID, out sbyte out_axis, out sbyte out_a_1, out sbyte out_a_2)
		{
			sbyte b = (sbyte)((int)primarySegmentID / 4);
			sbyte rh = (sbyte)((int)primarySegmentID % 2);
			sbyte rh2 = (sbyte)((int)primarySegmentID / 2 % 2);
			out_axis = this.Axis[(int)b].axisIdx;
			MetaballBuilder.MB3DCubeVector mb3DCubeVector = this.Origin + this.Axis[((int)b + 1) % 3] * rh + this.Axis[((int)b + 2) % 3] * rh2;
			sbyte b2 = (sbyte)(((int)out_axis + 1) % 3);
			sbyte b3 = (sbyte)(((int)out_axis + 2) % 3);
			out_a_1 = mb3DCubeVector.e[(int)b2];
			out_a_2 = mb3DCubeVector.e[(int)b3];
		}

		// Token: 0x04004778 RID: 18296
		public int PrimaryPatternIndex;

		// Token: 0x04004779 RID: 18297
		public bool bFlipInOut;

		// Token: 0x0400477A RID: 18298
		public MetaballBuilder.MB3DCubeVector Origin = new MetaballBuilder.MB3DCubeVector();

		// Token: 0x0400477B RID: 18299
		public MetaballBuilder.MB3DCubeVector[] Axis = new MetaballBuilder.MB3DCubeVector[3];
	}

	// Token: 0x02000A4F RID: 2639
	private class MB3DCubePattern
	{
		// Token: 0x06004E69 RID: 20073 RVA: 0x001E174C File Offset: 0x001DFB4C
		public void Init(int patternIdx)
		{
			this.PatternIdx = patternIdx;
			MetaballBuilder.MB3DCubeInOut src = new MetaballBuilder.MB3DCubeInOut((sbyte)patternIdx);
			this.MatchingInfo.Match(src);
			int[] targetPrimitiveIndexBuffer = this.MatchingInfo.GetTargetPrimitiveIndexBuffer();
			for (int i = 0; i < targetPrimitiveIndexBuffer.Length; i++)
			{
				sbyte b;
				sbyte b2;
				sbyte b3;
				this.MatchingInfo.SampleSegment((sbyte)targetPrimitiveIndexBuffer[i], out b, out b2, out b3);
				int num = (!this.MatchingInfo.bFlipInOut) ? i : (targetPrimitiveIndexBuffer.Length - i - 1);
				this.IndexBuf[num] = (int)b * 4 + (int)b3 * 2 + (int)b2;
			}
		}

		// Token: 0x0400477C RID: 18300
		public int PatternIdx;

		// Token: 0x0400477D RID: 18301
		public MetaballBuilder.MB3DPatternMatchingInfo MatchingInfo = new MetaballBuilder.MB3DPatternMatchingInfo();

		// Token: 0x0400477E RID: 18302
		public int[] IndexBuf = new int[15];
	}

	// Token: 0x02000A50 RID: 2640
	public class MetaballPointInfo
	{
		// Token: 0x0400477F RID: 18303
		public Vector3 center;

		// Token: 0x04004780 RID: 18304
		public float radius;

		// Token: 0x04004781 RID: 18305
		public float density;
	}
}
