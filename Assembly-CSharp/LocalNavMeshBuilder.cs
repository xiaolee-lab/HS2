using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000401 RID: 1025
[DefaultExecutionOrder(-102)]
public class LocalNavMeshBuilder : MonoBehaviour
{
	// Token: 0x06001236 RID: 4662 RVA: 0x000721E0 File Offset: 0x000705E0
	private IEnumerator Start()
	{
		for (;;)
		{
			this.UpdateNavMesh(true);
			yield return this.m_Operation;
		}
		yield break;
	}

	// Token: 0x06001237 RID: 4663 RVA: 0x000721FC File Offset: 0x000705FC
	private void OnEnable()
	{
		this.m_NavMesh = new NavMeshData();
		this.m_Instance = NavMesh.AddNavMeshData(this.m_NavMesh);
		if (this.m_Tracked == null)
		{
			this.m_Tracked = base.transform;
		}
		this.UpdateNavMesh(false);
	}

	// Token: 0x06001238 RID: 4664 RVA: 0x00072249 File Offset: 0x00070649
	private void OnDisable()
	{
		this.m_Instance.Remove();
	}

	// Token: 0x06001239 RID: 4665 RVA: 0x00072258 File Offset: 0x00070658
	private void UpdateNavMesh(bool asyncUpdate = false)
	{
		NavMeshSourceTag.Collect(ref this.m_Sources, 0);
		NavMeshBuildSettings settingsByID = NavMesh.GetSettingsByID(0);
		Bounds localBounds = this.QuantizedBounds();
		if (asyncUpdate)
		{
			this.m_Operation = NavMeshBuilder.UpdateNavMeshDataAsync(this.m_NavMesh, settingsByID, this.m_Sources, localBounds);
		}
		else
		{
			NavMeshBuilder.UpdateNavMeshData(this.m_NavMesh, settingsByID, this.m_Sources, localBounds);
		}
	}

	// Token: 0x0600123A RID: 4666 RVA: 0x000722B8 File Offset: 0x000706B8
	private static Vector3 Quantize(Vector3 v, Vector3 quant)
	{
		float x = quant.x * Mathf.Floor(v.x / quant.x);
		float y = quant.y * Mathf.Floor(v.y / quant.y);
		float z = quant.z * Mathf.Floor(v.z / quant.z);
		return new Vector3(x, y, z);
	}

	// Token: 0x0600123B RID: 4667 RVA: 0x00072324 File Offset: 0x00070724
	private Bounds QuantizedBounds()
	{
		Vector3 v = (!this.m_Tracked) ? base.transform.position : this.m_Tracked.position;
		return new Bounds(LocalNavMeshBuilder.Quantize(v, 0.1f * this.m_Size), this.m_Size);
	}

	// Token: 0x0600123C RID: 4668 RVA: 0x00072380 File Offset: 0x00070780
	private void OnDrawGizmosSelected()
	{
		if (this.m_NavMesh)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(this.m_NavMesh.sourceBounds.center, this.m_NavMesh.sourceBounds.size);
		}
		Gizmos.color = Color.yellow;
		Bounds bounds = this.QuantizedBounds();
		Gizmos.DrawWireCube(bounds.center, bounds.size);
		Gizmos.color = Color.green;
		Vector3 center = (!this.m_Tracked) ? base.transform.position : this.m_Tracked.position;
		Gizmos.DrawWireCube(center, this.m_Size);
	}

	// Token: 0x040014C8 RID: 5320
	public Transform m_Tracked;

	// Token: 0x040014C9 RID: 5321
	public Vector3 m_Size = new Vector3(80f, 20f, 80f);

	// Token: 0x040014CA RID: 5322
	private NavMeshData m_NavMesh;

	// Token: 0x040014CB RID: 5323
	private AsyncOperation m_Operation;

	// Token: 0x040014CC RID: 5324
	private NavMeshDataInstance m_Instance;

	// Token: 0x040014CD RID: 5325
	private List<NavMeshBuildSource> m_Sources = new List<NavMeshBuildSource>();
}
