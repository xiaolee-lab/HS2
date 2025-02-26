using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020011B2 RID: 4530
public class NormalControl
{
	// Token: 0x060094C3 RID: 38083 RVA: 0x003D58EC File Offset: 0x003D3CEC
	public static void GetNormalData(GameObject obj, List<Vector3> normal)
	{
		if (null == obj)
		{
			return;
		}
		MeshFilter meshFilter = new MeshFilter();
		meshFilter = (obj.GetComponent(typeof(MeshFilter)) as MeshFilter);
		if (null != meshFilter)
		{
			foreach (Vector3 item in meshFilter.sharedMesh.normals)
			{
				normal.Add(item);
			}
		}
		else
		{
			SkinnedMeshRenderer skinnedMeshRenderer = new SkinnedMeshRenderer();
			skinnedMeshRenderer = (obj.GetComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer);
			if (skinnedMeshRenderer)
			{
				foreach (Vector3 item2 in skinnedMeshRenderer.sharedMesh.normals)
				{
					normal.Add(item2);
				}
			}
		}
	}
}
