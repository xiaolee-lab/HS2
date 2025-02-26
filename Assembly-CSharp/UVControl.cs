using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020011B5 RID: 4533
public class UVControl
{
	// Token: 0x060094C7 RID: 38087 RVA: 0x003D5A14 File Offset: 0x003D3E14
	public static void GetUVData(GameObject obj, List<Vector2> uv, int index)
	{
		if (null == obj)
		{
			return;
		}
		MeshFilter meshFilter = new MeshFilter();
		meshFilter = (obj.GetComponent(typeof(MeshFilter)) as MeshFilter);
		if (null != meshFilter)
		{
			if (index == 0)
			{
				foreach (Vector2 item in meshFilter.sharedMesh.uv)
				{
					uv.Add(item);
				}
			}
			else if (index == 1)
			{
				foreach (Vector2 item2 in meshFilter.sharedMesh.uv2)
				{
					uv.Add(item2);
				}
			}
			else if (index == 2)
			{
				foreach (Vector2 item3 in meshFilter.sharedMesh.uv3)
				{
					uv.Add(item3);
				}
			}
			else if (index == 3)
			{
				foreach (Vector2 item4 in meshFilter.sharedMesh.uv4)
				{
					uv.Add(item4);
				}
			}
		}
		else
		{
			SkinnedMeshRenderer skinnedMeshRenderer = new SkinnedMeshRenderer();
			skinnedMeshRenderer = (obj.GetComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer);
			if (skinnedMeshRenderer)
			{
				if (index == 0)
				{
					foreach (Vector2 item5 in skinnedMeshRenderer.sharedMesh.uv)
					{
						uv.Add(item5);
					}
				}
				else if (index == 1)
				{
					foreach (Vector2 item6 in skinnedMeshRenderer.sharedMesh.uv2)
					{
						uv.Add(item6);
					}
				}
				else if (index == 2)
				{
					foreach (Vector2 item7 in skinnedMeshRenderer.sharedMesh.uv3)
					{
						uv.Add(item7);
					}
				}
				else if (index == 3)
				{
					foreach (Vector2 item8 in skinnedMeshRenderer.sharedMesh.uv4)
					{
						uv.Add(item8);
					}
				}
			}
		}
	}

	// Token: 0x060094C8 RID: 38088 RVA: 0x003D5CC4 File Offset: 0x003D40C4
	public static void GetRangeIndex(GameObject obj, out int[] rangeIndex)
	{
		rangeIndex = null;
		if (null == obj)
		{
			return;
		}
		List<int> list = new List<int>();
		MeshFilter meshFilter = new MeshFilter();
		meshFilter = (obj.GetComponent(typeof(MeshFilter)) as MeshFilter);
		if (null != meshFilter)
		{
			for (int i = 0; i < meshFilter.sharedMesh.colors.Length; i++)
			{
				if (meshFilter.sharedMesh.colors[i].r == 1f)
				{
					list.Add(i);
				}
			}
		}
		else
		{
			SkinnedMeshRenderer skinnedMeshRenderer = new SkinnedMeshRenderer();
			skinnedMeshRenderer = (obj.GetComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer);
			if (skinnedMeshRenderer)
			{
				for (int j = 0; j < skinnedMeshRenderer.sharedMesh.colors.Length; j++)
				{
					if (skinnedMeshRenderer.sharedMesh.colors[j].r == 1f)
					{
						list.Add(j);
					}
				}
			}
		}
		if (list.Count != 0)
		{
			rangeIndex = new int[list.Count];
			for (int k = 0; k < list.Count; k++)
			{
				rangeIndex[k] = list[k];
			}
		}
		else
		{
			rangeIndex = null;
		}
	}
}
