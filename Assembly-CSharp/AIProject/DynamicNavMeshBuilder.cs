using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000F40 RID: 3904
	public class DynamicNavMeshBuilder : MonoBehaviour
	{
		// Token: 0x06008144 RID: 33092 RVA: 0x0036D5AC File Offset: 0x0036B9AC
		private void OnEnable()
		{
			this._navMesh = new NavMeshData();
			this._instance = NavMesh.AddNavMeshData(this._navMesh);
			if (this._tracked == null)
			{
				this._tracked = base.transform;
			}
			if (this._enumerator == null)
			{
				this._enumerator = this.LoadNavMesh();
			}
			base.StartCoroutine(this._enumerator);
		}

		// Token: 0x06008145 RID: 33093 RVA: 0x0036D616 File Offset: 0x0036BA16
		private void OnDisable()
		{
			base.StopCoroutine(this._enumerator);
			this._instance.Remove();
		}

		// Token: 0x06008146 RID: 33094 RVA: 0x0036D630 File Offset: 0x0036BA30
		private IEnumerator LoadNavMesh()
		{
			for (;;)
			{
				this.UpdateNavMesh(true);
				yield return this._operation;
			}
			yield break;
		}

		// Token: 0x06008147 RID: 33095 RVA: 0x0036D64C File Offset: 0x0036BA4C
		protected virtual void UpdateNavMesh(bool asyncUpdate = false)
		{
			NavMeshBuildTag.Collect(ref this.m_Sources);
			NavMeshBuildSettings settingsByID = NavMesh.GetSettingsByID(0);
			Bounds localBounds = this.QuantizedBounds();
			if (asyncUpdate)
			{
				this._operation = NavMeshBuilder.UpdateNavMeshDataAsync(this._navMesh, settingsByID, this.m_Sources, localBounds);
			}
			else
			{
				NavMeshBuilder.UpdateNavMeshData(this._navMesh, settingsByID, this.m_Sources, localBounds);
			}
		}

		// Token: 0x06008148 RID: 33096 RVA: 0x0036D6AC File Offset: 0x0036BAAC
		private static Vector3 Quantize(Vector3 v, Vector3 quant)
		{
			float x = quant.x * Mathf.Floor(v.x / quant.x);
			float y = quant.y * Mathf.Floor(v.y / quant.y);
			float z = quant.z * Mathf.Floor(v.z / quant.z);
			return new Vector3(x, y, z);
		}

		// Token: 0x06008149 RID: 33097 RVA: 0x0036D718 File Offset: 0x0036BB18
		protected Bounds QuantizedBounds()
		{
			Vector3 v = (!this._tracked) ? base.transform.position : this._tracked.position;
			return new Bounds(DynamicNavMeshBuilder.Quantize(v, 0.1f * this._size), this._size);
		}

		// Token: 0x0600814A RID: 33098 RVA: 0x0036D774 File Offset: 0x0036BB74
		protected virtual void OnDrawGizmosSelected()
		{
			if (this._navMesh)
			{
				Gizmos.color = Color.green;
				Gizmos.DrawWireCube(this._navMesh.sourceBounds.center, this._navMesh.sourceBounds.size);
			}
			Gizmos.color = Color.yellow;
			Bounds bounds = this.QuantizedBounds();
			Gizmos.DrawWireCube(bounds.center, bounds.size);
			Gizmos.color = Color.green;
			Vector3 center = (!this._tracked) ? base.transform.position : this._tracked.position;
			Gizmos.DrawWireCube(center, this._size);
		}

		// Token: 0x040067D4 RID: 26580
		[SerializeField]
		protected Transform _tracked;

		// Token: 0x040067D5 RID: 26581
		[SerializeField]
		protected Vector3 _size = new Vector3(80f, 20f, 80f);

		// Token: 0x040067D6 RID: 26582
		protected NavMeshData _navMesh;

		// Token: 0x040067D7 RID: 26583
		protected AsyncOperation _operation;

		// Token: 0x040067D8 RID: 26584
		protected NavMeshDataInstance _instance = default(NavMeshDataInstance);

		// Token: 0x040067D9 RID: 26585
		private List<NavMeshBuildSource> m_Sources = new List<NavMeshBuildSource>();

		// Token: 0x040067DA RID: 26586
		private IEnumerator _enumerator;
	}
}
