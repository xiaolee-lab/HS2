using System;
using System.Collections.Generic;
using UnityEngine;
using usmooth;

// Token: 0x0200048D RID: 1165
public class MeshLut
{
	// Token: 0x0600157A RID: 5498 RVA: 0x00084CDC File Offset: 0x000830DC
	public bool AddMesh(GameObject go)
	{
		if (this._lut.ContainsKey(go.GetInstanceID()))
		{
			return true;
		}
		if (go.GetComponent<Renderer>() == null)
		{
			return false;
		}
		MeshFilter meshFilter = (MeshFilter)go.GetComponent(typeof(MeshFilter));
		if (meshFilter == null)
		{
			return false;
		}
		MeshData meshData = new MeshData();
		meshData._instID = go.GetInstanceID();
		meshData._vertCount = meshFilter.mesh.vertexCount;
		meshData._materialCount = go.GetComponent<Renderer>().sharedMaterials.Length;
		meshData._boundSize = go.GetComponent<Renderer>().bounds.size.magnitude;
		meshData._camDist = ((!(DataCollector.MainCamera != null)) ? 0f : Vector3.Distance(go.transform.position, DataCollector.MainCamera.transform.position));
		this._lut.Add(meshData._instID, meshData);
		return true;
	}

	// Token: 0x0600157B RID: 5499 RVA: 0x00084DE4 File Offset: 0x000831E4
	public void WriteMesh(int instID, UsCmd cmd)
	{
		MeshData meshData;
		if (this._lut.TryGetValue(instID, out meshData))
		{
			meshData.Write(cmd);
		}
	}

	// Token: 0x0600157C RID: 5500 RVA: 0x00084E0B File Offset: 0x0008320B
	public void ClearLut()
	{
		this._lut.Clear();
	}

	// Token: 0x04001895 RID: 6293
	private Dictionary<int, MeshData> _lut = new Dictionary<int, MeshData>();
}
