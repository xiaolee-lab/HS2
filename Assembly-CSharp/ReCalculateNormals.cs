using System;
using UnityEngine;

// Token: 0x020011B1 RID: 4529
public class ReCalculateNormals : MonoBehaviour
{
	// Token: 0x060094C1 RID: 38081 RVA: 0x003D5860 File Offset: 0x003D3C60
	public void Update()
	{
		MeshFilter meshFilter = new MeshFilter();
		meshFilter = (base.gameObject.GetComponent(typeof(MeshFilter)) as MeshFilter);
		Mesh sharedMesh;
		if (meshFilter)
		{
			sharedMesh = meshFilter.sharedMesh;
		}
		else
		{
			SkinnedMeshRenderer skinnedMeshRenderer = new SkinnedMeshRenderer();
			skinnedMeshRenderer = (base.gameObject.GetComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer);
			sharedMesh = skinnedMeshRenderer.sharedMesh;
		}
		if (null != sharedMesh)
		{
			sharedMesh.RecalculateNormals();
		}
	}
}
