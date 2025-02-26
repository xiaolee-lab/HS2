using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000405 RID: 1029
[DefaultExecutionOrder(-200)]
public class NavMeshSourceTag : MonoBehaviour
{
	// Token: 0x06001253 RID: 4691 RVA: 0x00072A20 File Offset: 0x00070E20
	private void OnEnable()
	{
		MeshFilter component = base.GetComponent<MeshFilter>();
		if (component == null)
		{
			return;
		}
		if (!NavMeshSourceTag.m_Meshes.ContainsKey(base.gameObject))
		{
			NavMeshSourceTag.m_Meshes.Add(base.gameObject, new NavMeshSourceTag.Info
			{
				MeshFilter = component,
				NavMeshModifier = base.GetComponent<NavMeshModifier>()
			});
		}
		else
		{
			NavMeshSourceTag.Info info = NavMeshSourceTag.m_Meshes[base.gameObject];
			info.MeshFilter = component;
			info.NavMeshModifier = base.GetComponent<NavMeshModifier>();
		}
	}

	// Token: 0x06001254 RID: 4692 RVA: 0x00072AA9 File Offset: 0x00070EA9
	private void OnDisable()
	{
		NavMeshSourceTag.m_Meshes.Remove(base.gameObject);
	}

	// Token: 0x06001255 RID: 4693 RVA: 0x00072ABC File Offset: 0x00070EBC
	public static void Collect(ref List<NavMeshBuildSource> sources, int _defaultArea = 0)
	{
		sources.Clear();
		IEnumerable<NavMeshSourceTag.Info> enumerable = from v in NavMeshSourceTag.m_Meshes.Values
		where v.MeshFilter != null
		where v.MeshFilter.sharedMesh != null
		where !v.Ignore
		select v;
		foreach (NavMeshSourceTag.Info info in enumerable)
		{
			NavMeshBuildSource item = default(NavMeshBuildSource);
			item.shape = NavMeshBuildSourceShape.Mesh;
			item.sourceObject = info.MeshFilter.sharedMesh;
			item.transform = info.MeshFilter.transform.localToWorldMatrix;
			item.area = ((!info.OverrideArea) ? _defaultArea : info.Area);
			sources.Add(item);
		}
	}

	// Token: 0x040014DE RID: 5342
	public static Dictionary<GameObject, NavMeshSourceTag.Info> m_Meshes = new Dictionary<GameObject, NavMeshSourceTag.Info>();

	// Token: 0x02000406 RID: 1030
	public class Info
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x00072C20 File Offset: 0x00071020
		// (set) Token: 0x0600125C RID: 4700 RVA: 0x00072C28 File Offset: 0x00071028
		public MeshFilter MeshFilter { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x00072C31 File Offset: 0x00071031
		// (set) Token: 0x0600125E RID: 4702 RVA: 0x00072C39 File Offset: 0x00071039
		public NavMeshModifier NavMeshModifier { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00072C42 File Offset: 0x00071042
		public bool Ignore
		{
			[CompilerGenerated]
			get
			{
				return this.NavMeshModifier && (this.NavMeshModifier.isActiveAndEnabled & this.NavMeshModifier.ignoreFromBuild);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x00072C71 File Offset: 0x00071071
		public bool OverrideArea
		{
			[CompilerGenerated]
			get
			{
				return this.NavMeshModifier && (this.NavMeshModifier.isActiveAndEnabled & this.NavMeshModifier.overrideArea);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x00072CA0 File Offset: 0x000710A0
		public int Area
		{
			[CompilerGenerated]
			get
			{
				return (!this.NavMeshModifier) ? 0 : this.NavMeshModifier.area;
			}
		}
	}
}
