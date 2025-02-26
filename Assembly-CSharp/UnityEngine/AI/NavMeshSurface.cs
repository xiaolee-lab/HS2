using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Serialization;

namespace UnityEngine.AI
{
	// Token: 0x0200040F RID: 1039
	[ExecuteInEditMode]
	[DefaultExecutionOrder(-102)]
	[AddComponentMenu("Navigation/NavMeshSurface", 30)]
	[HelpURL("https://github.com/Unity-Technologies/NavMeshComponents#documentation-draft")]
	public class NavMeshSurface : MonoBehaviour
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x00073842 File Offset: 0x00071C42
		// (set) Token: 0x060012A9 RID: 4777 RVA: 0x0007384A File Offset: 0x00071C4A
		public int agentTypeID
		{
			get
			{
				return this.m_AgentTypeID;
			}
			set
			{
				this.m_AgentTypeID = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x00073853 File Offset: 0x00071C53
		// (set) Token: 0x060012AB RID: 4779 RVA: 0x0007385B File Offset: 0x00071C5B
		public CollectObjects collectObjects
		{
			get
			{
				return this.m_CollectObjects;
			}
			set
			{
				this.m_CollectObjects = value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x00073864 File Offset: 0x00071C64
		// (set) Token: 0x060012AD RID: 4781 RVA: 0x0007386C File Offset: 0x00071C6C
		public Vector3 size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x00073875 File Offset: 0x00071C75
		// (set) Token: 0x060012AF RID: 4783 RVA: 0x0007387D File Offset: 0x00071C7D
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x00073886 File Offset: 0x00071C86
		// (set) Token: 0x060012B1 RID: 4785 RVA: 0x0007388E File Offset: 0x00071C8E
		public LayerMask layerMask
		{
			get
			{
				return this.m_LayerMask;
			}
			set
			{
				this.m_LayerMask = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x00073897 File Offset: 0x00071C97
		// (set) Token: 0x060012B3 RID: 4787 RVA: 0x0007389F File Offset: 0x00071C9F
		public NavMeshCollectGeometry useGeometry
		{
			get
			{
				return this.m_UseGeometry;
			}
			set
			{
				this.m_UseGeometry = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x000738A8 File Offset: 0x00071CA8
		// (set) Token: 0x060012B5 RID: 4789 RVA: 0x000738B0 File Offset: 0x00071CB0
		public int defaultArea
		{
			get
			{
				return this.m_DefaultArea;
			}
			set
			{
				this.m_DefaultArea = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x000738B9 File Offset: 0x00071CB9
		// (set) Token: 0x060012B7 RID: 4791 RVA: 0x000738C1 File Offset: 0x00071CC1
		public bool ignoreNavMeshAgent
		{
			get
			{
				return this.m_IgnoreNavMeshAgent;
			}
			set
			{
				this.m_IgnoreNavMeshAgent = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x000738CA File Offset: 0x00071CCA
		// (set) Token: 0x060012B9 RID: 4793 RVA: 0x000738D2 File Offset: 0x00071CD2
		public bool ignoreNavMeshObstacle
		{
			get
			{
				return this.m_IgnoreNavMeshObstacle;
			}
			set
			{
				this.m_IgnoreNavMeshObstacle = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x000738DB File Offset: 0x00071CDB
		// (set) Token: 0x060012BB RID: 4795 RVA: 0x000738E3 File Offset: 0x00071CE3
		public bool overrideTileSize
		{
			get
			{
				return this.m_OverrideTileSize;
			}
			set
			{
				this.m_OverrideTileSize = value;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x000738EC File Offset: 0x00071CEC
		// (set) Token: 0x060012BD RID: 4797 RVA: 0x000738F4 File Offset: 0x00071CF4
		public int tileSize
		{
			get
			{
				return this.m_TileSize;
			}
			set
			{
				this.m_TileSize = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x000738FD File Offset: 0x00071CFD
		// (set) Token: 0x060012BF RID: 4799 RVA: 0x00073905 File Offset: 0x00071D05
		public bool overrideVoxelSize
		{
			get
			{
				return this.m_OverrideVoxelSize;
			}
			set
			{
				this.m_OverrideVoxelSize = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x0007390E File Offset: 0x00071D0E
		// (set) Token: 0x060012C1 RID: 4801 RVA: 0x00073916 File Offset: 0x00071D16
		public float voxelSize
		{
			get
			{
				return this.m_VoxelSize;
			}
			set
			{
				this.m_VoxelSize = value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x0007391F File Offset: 0x00071D1F
		// (set) Token: 0x060012C3 RID: 4803 RVA: 0x00073927 File Offset: 0x00071D27
		public bool buildHeightMesh
		{
			get
			{
				return this.m_BuildHeightMesh;
			}
			set
			{
				this.m_BuildHeightMesh = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00073930 File Offset: 0x00071D30
		// (set) Token: 0x060012C5 RID: 4805 RVA: 0x00073938 File Offset: 0x00071D38
		public NavMeshData navMeshData
		{
			get
			{
				return this.m_NavMeshData;
			}
			set
			{
				this.m_NavMeshData = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x00073941 File Offset: 0x00071D41
		public static List<NavMeshSurface> activeSurfaces
		{
			get
			{
				return NavMeshSurface.s_NavMeshSurfaces;
			}
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00073948 File Offset: 0x00071D48
		private void OnEnable()
		{
			NavMeshSurface.Register(this);
			this.AddData();
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00073956 File Offset: 0x00071D56
		private void OnDisable()
		{
			this.RemoveData();
			NavMeshSurface.Unregister(this);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00073964 File Offset: 0x00071D64
		public void AddData()
		{
			if (this.m_NavMeshDataInstance.valid)
			{
				return;
			}
			if (this.m_NavMeshData != null)
			{
				this.m_NavMeshDataInstance = NavMesh.AddNavMeshData(this.m_NavMeshData, base.transform.position, base.transform.rotation);
				this.m_NavMeshDataInstance.owner = this;
			}
			this.m_LastPosition = base.transform.position;
			this.m_LastRotation = base.transform.rotation;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x000739E8 File Offset: 0x00071DE8
		public void RemoveData()
		{
			this.m_NavMeshDataInstance.Remove();
			this.m_NavMeshDataInstance = default(NavMeshDataInstance);
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00073A10 File Offset: 0x00071E10
		public NavMeshBuildSettings GetBuildSettings()
		{
			NavMeshBuildSettings settingsByID = NavMesh.GetSettingsByID(this.m_AgentTypeID);
			if (settingsByID.agentTypeID == -1)
			{
				Debug.LogWarning("No build settings for agent type ID " + this.agentTypeID, this);
				settingsByID.agentTypeID = this.m_AgentTypeID;
			}
			if (this.overrideTileSize)
			{
				settingsByID.overrideTileSize = true;
				settingsByID.tileSize = this.tileSize;
			}
			if (this.overrideVoxelSize)
			{
				settingsByID.overrideVoxelSize = true;
				settingsByID.voxelSize = this.voxelSize;
			}
			return settingsByID;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00073AA0 File Offset: 0x00071EA0
		public void BuildNavMesh()
		{
			List<NavMeshBuildSource> sources = this.CollectSources();
			Bounds localBounds = new Bounds(this.m_Center, NavMeshSurface.Abs(this.m_Size));
			if (this.m_CollectObjects == CollectObjects.All || this.m_CollectObjects == CollectObjects.Children)
			{
				localBounds = this.CalculateWorldBounds(sources);
			}
			NavMeshData navMeshData = NavMeshBuilder.BuildNavMeshData(this.GetBuildSettings(), sources, localBounds, base.transform.position, base.transform.rotation);
			if (navMeshData != null)
			{
				navMeshData.name = base.gameObject.name;
				this.RemoveData();
				this.m_NavMeshData = navMeshData;
				if (base.isActiveAndEnabled)
				{
					this.AddData();
				}
			}
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00073B4C File Offset: 0x00071F4C
		public AsyncOperation UpdateNavMesh(NavMeshData data)
		{
			List<NavMeshBuildSource> sources = this.CollectSources();
			Bounds localBounds = new Bounds(this.m_Center, NavMeshSurface.Abs(this.m_Size));
			if (this.m_CollectObjects == CollectObjects.All || this.m_CollectObjects == CollectObjects.Children)
			{
				localBounds = this.CalculateWorldBounds(sources);
			}
			return NavMeshBuilder.UpdateNavMeshDataAsync(data, this.GetBuildSettings(), sources, localBounds);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00073BA8 File Offset: 0x00071FA8
		private static void Register(NavMeshSurface surface)
		{
			if (NavMeshSurface.s_NavMeshSurfaces.Count == 0)
			{
				Delegate onPreUpdate = NavMesh.onPreUpdate;
				if (NavMeshSurface.<>f__mg$cache0 == null)
				{
					NavMeshSurface.<>f__mg$cache0 = new NavMesh.OnNavMeshPreUpdate(NavMeshSurface.UpdateActive);
				}
				NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)Delegate.Combine(onPreUpdate, NavMeshSurface.<>f__mg$cache0);
			}
			if (!NavMeshSurface.s_NavMeshSurfaces.Contains(surface))
			{
				NavMeshSurface.s_NavMeshSurfaces.Add(surface);
			}
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00073C10 File Offset: 0x00072010
		private static void Unregister(NavMeshSurface surface)
		{
			NavMeshSurface.s_NavMeshSurfaces.Remove(surface);
			if (NavMeshSurface.s_NavMeshSurfaces.Count == 0)
			{
				Delegate onPreUpdate = NavMesh.onPreUpdate;
				if (NavMeshSurface.<>f__mg$cache1 == null)
				{
					NavMeshSurface.<>f__mg$cache1 = new NavMesh.OnNavMeshPreUpdate(NavMeshSurface.UpdateActive);
				}
				NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)Delegate.Remove(onPreUpdate, NavMeshSurface.<>f__mg$cache1);
			}
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00073C6C File Offset: 0x0007206C
		private static void UpdateActive()
		{
			for (int i = 0; i < NavMeshSurface.s_NavMeshSurfaces.Count; i++)
			{
				NavMeshSurface.s_NavMeshSurfaces[i].UpdateDataIfTransformChanged();
			}
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00073CA4 File Offset: 0x000720A4
		private void AppendModifierVolumes(ref List<NavMeshBuildSource> sources)
		{
			List<NavMeshModifierVolume> list;
			if (this.m_CollectObjects == CollectObjects.Children)
			{
				list = new List<NavMeshModifierVolume>(base.GetComponentsInChildren<NavMeshModifierVolume>());
				list.RemoveAll((NavMeshModifierVolume x) => !x.isActiveAndEnabled);
			}
			else
			{
				list = NavMeshModifierVolume.activeModifiers;
			}
			foreach (NavMeshModifierVolume navMeshModifierVolume in list)
			{
				if ((this.m_LayerMask & 1 << navMeshModifierVolume.gameObject.layer) != 0)
				{
					if (navMeshModifierVolume.AffectsAgentType(this.m_AgentTypeID))
					{
						Vector3 pos = navMeshModifierVolume.transform.TransformPoint(navMeshModifierVolume.center);
						Vector3 lossyScale = navMeshModifierVolume.transform.lossyScale;
						Vector3 size = new Vector3(navMeshModifierVolume.size.x * Mathf.Abs(lossyScale.x), navMeshModifierVolume.size.y * Mathf.Abs(lossyScale.y), navMeshModifierVolume.size.z * Mathf.Abs(lossyScale.z));
						NavMeshBuildSource item = default(NavMeshBuildSource);
						item.shape = NavMeshBuildSourceShape.ModifierBox;
						item.transform = Matrix4x4.TRS(pos, navMeshModifierVolume.transform.rotation, Vector3.one);
						item.size = size;
						item.area = navMeshModifierVolume.area;
						sources.Add(item);
					}
				}
			}
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00073E4C File Offset: 0x0007224C
		private List<NavMeshBuildSource> CollectSources()
		{
			List<NavMeshBuildSource> list = new List<NavMeshBuildSource>();
			NavMeshSourceTag.Collect(ref list, this.m_DefaultArea);
			if (this.m_IgnoreNavMeshAgent)
			{
				list.RemoveAll((NavMeshBuildSource x) => x.component != null && x.component.gameObject.GetComponent<NavMeshAgent>() != null);
			}
			if (this.m_IgnoreNavMeshObstacle)
			{
				list.RemoveAll((NavMeshBuildSource x) => x.component != null && x.component.gameObject.GetComponent<NavMeshObstacle>() != null);
			}
			this.AppendModifierVolumes(ref list);
			return list;
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00073ED3 File Offset: 0x000722D3
		private static Vector3 Abs(Vector3 v)
		{
			return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00073F00 File Offset: 0x00072300
		private static Bounds GetWorldBounds(Matrix4x4 mat, Bounds bounds)
		{
			Vector3 a = NavMeshSurface.Abs(mat.MultiplyVector(Vector3.right));
			Vector3 a2 = NavMeshSurface.Abs(mat.MultiplyVector(Vector3.up));
			Vector3 a3 = NavMeshSurface.Abs(mat.MultiplyVector(Vector3.forward));
			Vector3 center = mat.MultiplyPoint(bounds.center);
			Vector3 size = a * bounds.size.x + a2 * bounds.size.y + a3 * bounds.size.z;
			return new Bounds(center, size);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00073FA8 File Offset: 0x000723A8
		private Bounds CalculateWorldBounds(List<NavMeshBuildSource> sources)
		{
			Matrix4x4 lhs = Matrix4x4.TRS(base.transform.position, base.transform.rotation, Vector3.one);
			lhs = lhs.inverse;
			Bounds result = default(Bounds);
			foreach (NavMeshBuildSource navMeshBuildSource in sources)
			{
				switch (navMeshBuildSource.shape)
				{
				case NavMeshBuildSourceShape.Mesh:
				{
					Mesh mesh = navMeshBuildSource.sourceObject as Mesh;
					result.Encapsulate(NavMeshSurface.GetWorldBounds(lhs * navMeshBuildSource.transform, mesh.bounds));
					break;
				}
				case NavMeshBuildSourceShape.Terrain:
				{
					TerrainData terrainData = navMeshBuildSource.sourceObject as TerrainData;
					result.Encapsulate(NavMeshSurface.GetWorldBounds(lhs * navMeshBuildSource.transform, new Bounds(0.5f * terrainData.size, terrainData.size)));
					break;
				}
				case NavMeshBuildSourceShape.Box:
				case NavMeshBuildSourceShape.Sphere:
				case NavMeshBuildSourceShape.Capsule:
				case NavMeshBuildSourceShape.ModifierBox:
					result.Encapsulate(NavMeshSurface.GetWorldBounds(lhs * navMeshBuildSource.transform, new Bounds(Vector3.zero, navMeshBuildSource.size)));
					break;
				}
			}
			result.Expand(0.1f);
			return result;
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00074110 File Offset: 0x00072510
		private bool HasTransformChanged()
		{
			return this.m_LastPosition != base.transform.position || this.m_LastRotation != base.transform.rotation;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0007414D File Offset: 0x0007254D
		private void UpdateDataIfTransformChanged()
		{
			if (this.HasTransformChanged())
			{
				this.RemoveData();
				this.AddData();
			}
		}

		// Token: 0x04001514 RID: 5396
		[SerializeField]
		private int m_AgentTypeID;

		// Token: 0x04001515 RID: 5397
		[SerializeField]
		private CollectObjects m_CollectObjects;

		// Token: 0x04001516 RID: 5398
		[SerializeField]
		private Vector3 m_Size = new Vector3(10f, 10f, 10f);

		// Token: 0x04001517 RID: 5399
		[SerializeField]
		private Vector3 m_Center = new Vector3(0f, 2f, 0f);

		// Token: 0x04001518 RID: 5400
		[SerializeField]
		private LayerMask m_LayerMask = -1;

		// Token: 0x04001519 RID: 5401
		[SerializeField]
		private NavMeshCollectGeometry m_UseGeometry;

		// Token: 0x0400151A RID: 5402
		[SerializeField]
		private int m_DefaultArea;

		// Token: 0x0400151B RID: 5403
		[SerializeField]
		private bool m_IgnoreNavMeshAgent = true;

		// Token: 0x0400151C RID: 5404
		[SerializeField]
		private bool m_IgnoreNavMeshObstacle = true;

		// Token: 0x0400151D RID: 5405
		[SerializeField]
		private bool m_OverrideTileSize;

		// Token: 0x0400151E RID: 5406
		[SerializeField]
		private int m_TileSize = 256;

		// Token: 0x0400151F RID: 5407
		[SerializeField]
		private bool m_OverrideVoxelSize;

		// Token: 0x04001520 RID: 5408
		[SerializeField]
		private float m_VoxelSize;

		// Token: 0x04001521 RID: 5409
		[SerializeField]
		private bool m_BuildHeightMesh;

		// Token: 0x04001522 RID: 5410
		[FormerlySerializedAs("m_BakedNavMeshData")]
		[SerializeField]
		private NavMeshData m_NavMeshData;

		// Token: 0x04001523 RID: 5411
		private NavMeshDataInstance m_NavMeshDataInstance;

		// Token: 0x04001524 RID: 5412
		private Vector3 m_LastPosition = Vector3.zero;

		// Token: 0x04001525 RID: 5413
		private Quaternion m_LastRotation = Quaternion.identity;

		// Token: 0x04001526 RID: 5414
		private static readonly List<NavMeshSurface> s_NavMeshSurfaces = new List<NavMeshSurface>();

		// Token: 0x04001527 RID: 5415
		[CompilerGenerated]
		private static NavMesh.OnNavMeshPreUpdate <>f__mg$cache0;

		// Token: 0x04001528 RID: 5416
		[CompilerGenerated]
		private static NavMesh.OnNavMeshPreUpdate <>f__mg$cache1;
	}
}
