using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000404 RID: 1028
[ExecuteInEditMode]
[DefaultExecutionOrder(-102)]
public class NavMeshPrefabInstance : MonoBehaviour
{
	// Token: 0x17000100 RID: 256
	// (get) Token: 0x06001243 RID: 4675 RVA: 0x000727AF File Offset: 0x00070BAF
	// (set) Token: 0x06001244 RID: 4676 RVA: 0x000727B7 File Offset: 0x00070BB7
	public NavMeshData navMeshData
	{
		get
		{
			return this.m_NavMesh;
		}
		set
		{
			this.m_NavMesh = value;
		}
	}

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x06001245 RID: 4677 RVA: 0x000727C0 File Offset: 0x00070BC0
	// (set) Token: 0x06001246 RID: 4678 RVA: 0x000727C8 File Offset: 0x00070BC8
	public bool followTransform
	{
		get
		{
			return this.m_FollowTransform;
		}
		set
		{
			this.SetFollowTransform(value);
		}
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x06001247 RID: 4679 RVA: 0x000727D1 File Offset: 0x00070BD1
	public static List<NavMeshPrefabInstance> trackedInstances
	{
		get
		{
			return NavMeshPrefabInstance.s_TrackedInstances;
		}
	}

	// Token: 0x06001248 RID: 4680 RVA: 0x000727D8 File Offset: 0x00070BD8
	private void OnEnable()
	{
		this.AddInstance();
		if (this.m_Instance.valid && this.m_FollowTransform)
		{
			this.AddTracking();
		}
	}

	// Token: 0x06001249 RID: 4681 RVA: 0x00072801 File Offset: 0x00070C01
	private void OnDisable()
	{
		this.m_Instance.Remove();
		this.RemoveTracking();
	}

	// Token: 0x0600124A RID: 4682 RVA: 0x00072814 File Offset: 0x00070C14
	public void UpdateInstance()
	{
		this.m_Instance.Remove();
		this.AddInstance();
	}

	// Token: 0x0600124B RID: 4683 RVA: 0x00072828 File Offset: 0x00070C28
	private void AddInstance()
	{
		if (this.m_NavMesh)
		{
			this.m_Instance = NavMesh.AddNavMeshData(this.m_NavMesh, base.transform.position, base.transform.rotation);
		}
		this.m_Rotation = base.transform.rotation;
		this.m_Position = base.transform.position;
	}

	// Token: 0x0600124C RID: 4684 RVA: 0x00072890 File Offset: 0x00070C90
	private void AddTracking()
	{
		if (NavMeshPrefabInstance.s_TrackedInstances.Count == 0)
		{
			Delegate onPreUpdate = NavMesh.onPreUpdate;
			if (NavMeshPrefabInstance.<>f__mg$cache0 == null)
			{
				NavMeshPrefabInstance.<>f__mg$cache0 = new NavMesh.OnNavMeshPreUpdate(NavMeshPrefabInstance.UpdateTrackedInstances);
			}
			NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)Delegate.Combine(onPreUpdate, NavMeshPrefabInstance.<>f__mg$cache0);
		}
		NavMeshPrefabInstance.s_TrackedInstances.Add(this);
	}

	// Token: 0x0600124D RID: 4685 RVA: 0x000728E8 File Offset: 0x00070CE8
	private void RemoveTracking()
	{
		NavMeshPrefabInstance.s_TrackedInstances.Remove(this);
		if (NavMeshPrefabInstance.s_TrackedInstances.Count == 0)
		{
			Delegate onPreUpdate = NavMesh.onPreUpdate;
			if (NavMeshPrefabInstance.<>f__mg$cache1 == null)
			{
				NavMeshPrefabInstance.<>f__mg$cache1 = new NavMesh.OnNavMeshPreUpdate(NavMeshPrefabInstance.UpdateTrackedInstances);
			}
			NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)Delegate.Remove(onPreUpdate, NavMeshPrefabInstance.<>f__mg$cache1);
		}
	}

	// Token: 0x0600124E RID: 4686 RVA: 0x00072941 File Offset: 0x00070D41
	private void SetFollowTransform(bool value)
	{
		if (this.m_FollowTransform == value)
		{
			return;
		}
		this.m_FollowTransform = value;
		if (value)
		{
			this.AddTracking();
		}
		else
		{
			this.RemoveTracking();
		}
	}

	// Token: 0x0600124F RID: 4687 RVA: 0x0007296E File Offset: 0x00070D6E
	private bool HasMoved()
	{
		return this.m_Position != base.transform.position || this.m_Rotation != base.transform.rotation;
	}

	// Token: 0x06001250 RID: 4688 RVA: 0x000729A4 File Offset: 0x00070DA4
	private static void UpdateTrackedInstances()
	{
		foreach (NavMeshPrefabInstance navMeshPrefabInstance in NavMeshPrefabInstance.s_TrackedInstances)
		{
			if (navMeshPrefabInstance.HasMoved())
			{
				navMeshPrefabInstance.UpdateInstance();
			}
		}
	}

	// Token: 0x040014D6 RID: 5334
	[SerializeField]
	private NavMeshData m_NavMesh;

	// Token: 0x040014D7 RID: 5335
	[SerializeField]
	private bool m_FollowTransform;

	// Token: 0x040014D8 RID: 5336
	private NavMeshDataInstance m_Instance;

	// Token: 0x040014D9 RID: 5337
	private static readonly List<NavMeshPrefabInstance> s_TrackedInstances = new List<NavMeshPrefabInstance>();

	// Token: 0x040014DA RID: 5338
	private Vector3 m_Position;

	// Token: 0x040014DB RID: 5339
	private Quaternion m_Rotation;

	// Token: 0x040014DC RID: 5340
	[CompilerGenerated]
	private static NavMesh.OnNavMeshPreUpdate <>f__mg$cache0;

	// Token: 0x040014DD RID: 5341
	[CompilerGenerated]
	private static NavMesh.OnNavMeshPreUpdate <>f__mg$cache1;
}
