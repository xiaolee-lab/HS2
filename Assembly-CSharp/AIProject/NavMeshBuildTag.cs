using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000F41 RID: 3905
	[DefaultExecutionOrder(-200)]
	public class NavMeshBuildTag : MonoBehaviour
	{
		// Token: 0x170019F6 RID: 6646
		// (get) Token: 0x0600814C RID: 33100 RVA: 0x0036D8D1 File Offset: 0x0036BCD1
		public int AreaID
		{
			[CompilerGenerated]
			get
			{
				return this._areaID;
			}
		}

		// Token: 0x0600814D RID: 33101 RVA: 0x0036D8DC File Offset: 0x0036BCDC
		private void OnEnable()
		{
			MeshFilter component = base.GetComponent<MeshFilter>();
			if (component != null)
			{
				NavMeshBuildTag.MeshInfo item = new NavMeshBuildTag.MeshInfo(component, this._areaID);
				NavMeshBuildTag._meshes.Add(item);
			}
			Terrain component2 = base.GetComponent<Terrain>();
			if (component2 != null)
			{
				NavMeshBuildTag.TerrainInfo item2 = new NavMeshBuildTag.TerrainInfo(component2, this._areaID);
				NavMeshBuildTag._terrains.Add(item2);
			}
		}

		// Token: 0x0600814E RID: 33102 RVA: 0x0036D940 File Offset: 0x0036BD40
		private void OnDisable()
		{
			MeshFilter m = base.GetComponent<MeshFilter>();
			if (m != null)
			{
				NavMeshBuildTag._meshes.RemoveAll((NavMeshBuildTag.MeshInfo x) => x.Filter == m);
			}
			Terrain t = base.GetComponent<Terrain>();
			if (t != null)
			{
				NavMeshBuildTag._terrains.RemoveAll((NavMeshBuildTag.TerrainInfo x) => x.Terrain == t);
			}
		}

		// Token: 0x0600814F RID: 33103 RVA: 0x0036D9BC File Offset: 0x0036BDBC
		public static void Collect(ref List<NavMeshBuildSource> sources)
		{
			sources.Clear();
			for (int i = 0; i < NavMeshBuildTag._meshes.Count; i++)
			{
				NavMeshBuildTag.MeshInfo meshInfo = NavMeshBuildTag._meshes[i];
				if (meshInfo != null)
				{
					Mesh sharedMesh = meshInfo.Filter.sharedMesh;
					if (!(sharedMesh == null))
					{
						NavMeshBuildSource item = default(NavMeshBuildSource);
						item.shape = NavMeshBuildSourceShape.Mesh;
						item.sourceObject = sharedMesh;
						item.transform = meshInfo.Filter.transform.localToWorldMatrix;
						item.area = meshInfo.Area;
						sources.Add(item);
					}
				}
			}
			for (int j = 0; j < NavMeshBuildTag._terrains.Count; j++)
			{
				NavMeshBuildTag.TerrainInfo terrainInfo = NavMeshBuildTag._terrains[j];
				if (terrainInfo != null)
				{
					NavMeshBuildSource item2 = default(NavMeshBuildSource);
					item2.shape = NavMeshBuildSourceShape.Terrain;
					item2.sourceObject = terrainInfo.Terrain.terrainData;
					item2.transform = Matrix4x4.TRS(terrainInfo.Terrain.transform.position, Quaternion.identity, Vector3.one);
					item2.area = terrainInfo.Area;
					sources.Add(item2);
				}
			}
		}

		// Token: 0x040067DB RID: 26587
		private static List<NavMeshBuildTag.MeshInfo> _meshes = new List<NavMeshBuildTag.MeshInfo>();

		// Token: 0x040067DC RID: 26588
		private static List<NavMeshBuildTag.TerrainInfo> _terrains = new List<NavMeshBuildTag.TerrainInfo>();

		// Token: 0x040067DD RID: 26589
		[SerializeField]
		[NavMeshAreaEnum]
		private int _areaID;

		// Token: 0x02000F42 RID: 3906
		public class MeshInfo
		{
			// Token: 0x06008151 RID: 33105 RVA: 0x0036DB13 File Offset: 0x0036BF13
			public MeshInfo(MeshFilter filter, int area)
			{
				this._filter = filter;
				this._area = area;
			}

			// Token: 0x170019F7 RID: 6647
			// (get) Token: 0x06008152 RID: 33106 RVA: 0x0036DB29 File Offset: 0x0036BF29
			// (set) Token: 0x06008153 RID: 33107 RVA: 0x0036DB31 File Offset: 0x0036BF31
			public MeshFilter Filter
			{
				get
				{
					return this._filter;
				}
				set
				{
					this._filter = value;
				}
			}

			// Token: 0x170019F8 RID: 6648
			// (get) Token: 0x06008154 RID: 33108 RVA: 0x0036DB3A File Offset: 0x0036BF3A
			// (set) Token: 0x06008155 RID: 33109 RVA: 0x0036DB42 File Offset: 0x0036BF42
			public int Area
			{
				get
				{
					return this._area;
				}
				set
				{
					this._area = value;
				}
			}

			// Token: 0x040067DE RID: 26590
			private MeshFilter _filter;

			// Token: 0x040067DF RID: 26591
			private int _area;
		}

		// Token: 0x02000F43 RID: 3907
		public class TerrainInfo
		{
			// Token: 0x06008156 RID: 33110 RVA: 0x0036DB4B File Offset: 0x0036BF4B
			public TerrainInfo(Terrain terrain, int area)
			{
				this._terrain = terrain;
				this._area = area;
			}

			// Token: 0x170019F9 RID: 6649
			// (get) Token: 0x06008157 RID: 33111 RVA: 0x0036DB61 File Offset: 0x0036BF61
			// (set) Token: 0x06008158 RID: 33112 RVA: 0x0036DB69 File Offset: 0x0036BF69
			public Terrain Terrain
			{
				get
				{
					return this._terrain;
				}
				set
				{
					this._terrain = value;
				}
			}

			// Token: 0x170019FA RID: 6650
			// (get) Token: 0x06008159 RID: 33113 RVA: 0x0036DB72 File Offset: 0x0036BF72
			// (set) Token: 0x0600815A RID: 33114 RVA: 0x0036DB7A File Offset: 0x0036BF7A
			public int Area
			{
				get
				{
					return this._area;
				}
				set
				{
					this._area = value;
				}
			}

			// Token: 0x040067E0 RID: 26592
			private Terrain _terrain;

			// Token: 0x040067E1 RID: 26593
			private int _area;
		}
	}
}
