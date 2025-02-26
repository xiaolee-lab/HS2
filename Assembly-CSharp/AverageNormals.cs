using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020011B0 RID: 4528
public class AverageNormals : MonoBehaviour
{
	// Token: 0x060094B9 RID: 38073 RVA: 0x003D5280 File Offset: 0x003D3680
	public void Init()
	{
		if (null == this.objUpdate[0] || null == this.objUpdate[1] || null == this.ObjRange)
		{
			return;
		}
		Mesh[] array = new Mesh[2];
		List<int>[] array2 = new List<int>[2];
		for (int i = 0; i < 2; i++)
		{
			array2[i] = new List<int>();
		}
		List<int>[] array3 = new List<int>[2];
		for (int j = 0; j < 2; j++)
		{
			array3[j] = new List<int>();
			MeshFilter meshFilter = new MeshFilter();
			meshFilter = (this.objUpdate[j].GetComponent(typeof(MeshFilter)) as MeshFilter);
			if (meshFilter)
			{
				array[j] = meshFilter.sharedMesh;
			}
			else
			{
				SkinnedMeshRenderer skinnedMeshRenderer = new SkinnedMeshRenderer();
				skinnedMeshRenderer = (this.objUpdate[j].GetComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer);
				array[j] = skinnedMeshRenderer.sharedMesh;
			}
			if (null != array[j])
			{
				if (this.ObjRange)
				{
					for (int k = 0; k < array[j].vertexCount; k++)
					{
						Vector3 b = this.objUpdate[j].transform.TransformPoint(array[j].vertices[k]);
						float num = Vector3.Distance(this.ObjRange.transform.position, b);
						if (num < this.Range)
						{
							array3[j].Add(k);
						}
					}
				}
				else
				{
					for (int l = 0; l < array[j].vertexCount; l++)
					{
						array3[j].Add(l);
					}
				}
			}
		}
		if (null != array[0] && null != array[1])
		{
			for (int m = 0; m < array3[0].Count; m++)
			{
				for (int n = 0; n < array3[1].Count; n++)
				{
					int num2 = array3[0][m];
					int num3 = array3[1][n];
					Vector3 lhs = this.objUpdate[0].transform.TransformPoint(array[0].vertices[num2]);
					Vector3 rhs = this.objUpdate[1].transform.TransformPoint(array[1].vertices[num3]);
					if (lhs == rhs)
					{
						array2[0].Add(num2);
						array2[1].Add(num3);
						break;
					}
				}
			}
			this.calcIndexA = new int[array2[0].Count];
			this.calcIndexB = new int[array2[1].Count];
			for (int num4 = 0; num4 < array2[0].Count; num4++)
			{
				this.calcIndexA[num4] = array2[0][num4];
				this.calcIndexB[num4] = array2[1][num4];
			}
		}
	}

	// Token: 0x060094BA RID: 38074 RVA: 0x003D559A File Offset: 0x003D399A
	private void Awake()
	{
	}

	// Token: 0x060094BB RID: 38075 RVA: 0x003D559C File Offset: 0x003D399C
	private void Start()
	{
	}

	// Token: 0x060094BC RID: 38076 RVA: 0x003D559E File Offset: 0x003D399E
	private void Update()
	{
		if (this.meshInit)
		{
			this.GetUpdateMesh();
			this.meshInit = false;
		}
		this.Average();
	}

	// Token: 0x060094BD RID: 38077 RVA: 0x003D55C0 File Offset: 0x003D39C0
	public void GetUpdateMesh()
	{
		for (int i = 0; i < 2; i++)
		{
			if (!(null == this.objUpdate[i]))
			{
				MeshFilter meshFilter = new MeshFilter();
				meshFilter = (this.objUpdate[i].GetComponent(typeof(MeshFilter)) as MeshFilter);
				if (meshFilter)
				{
					this.meshUpdate[i] = meshFilter.sharedMesh;
				}
				else
				{
					SkinnedMeshRenderer skinnedMeshRenderer = new SkinnedMeshRenderer();
					skinnedMeshRenderer = (this.objUpdate[i].GetComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer);
					this.meshUpdate[i] = skinnedMeshRenderer.sharedMesh;
				}
			}
		}
	}

	// Token: 0x060094BE RID: 38078 RVA: 0x003D566C File Offset: 0x003D3A6C
	public void Average()
	{
		if (this.calcIndexA.Length == 0)
		{
			return;
		}
		if (this.calcIndexB.Length == 0)
		{
			return;
		}
		if (null == this.meshUpdate[0])
		{
			return;
		}
		if (null == this.meshUpdate[1])
		{
			return;
		}
		Vector3[] array = new Vector3[this.calcIndexA.Length];
		for (int i = 0; i < this.calcIndexA.Length; i++)
		{
			int num = this.calcIndexA[i];
			int num2 = this.calcIndexB[i];
			array[i] = Vector3.Lerp(this.meshUpdate[0].normals[num], this.meshUpdate[1].normals[num2], 0.5f);
		}
		Vector3[] array2 = new Vector3[this.meshUpdate[0].vertexCount];
		array2 = this.meshUpdate[0].normals;
		for (int j = 0; j < this.calcIndexA.Length; j++)
		{
			array2[this.calcIndexA[j]] = array[j];
		}
		this.meshUpdate[0].normals = array2;
		Vector3[] array3 = new Vector3[this.meshUpdate[1].vertexCount];
		array3 = this.meshUpdate[1].normals;
		for (int k = 0; k < this.calcIndexB.Length; k++)
		{
			array3[this.calcIndexB[k]] = array[k];
		}
		this.meshUpdate[1].normals = array3;
	}

	// Token: 0x060094BF RID: 38079 RVA: 0x003D581E File Offset: 0x003D3C1E
	private void OnDrawGizmos()
	{
		if (null == this.ObjRange)
		{
			return;
		}
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.ObjRange.transform.position, this.Range);
	}

	// Token: 0x04007799 RID: 30617
	public GameObject[] objUpdate = new GameObject[2];

	// Token: 0x0400779A RID: 30618
	public GameObject ObjRange;

	// Token: 0x0400779B RID: 30619
	public float Range = 1f;

	// Token: 0x0400779C RID: 30620
	private Mesh[] meshUpdate = new Mesh[2];

	// Token: 0x0400779D RID: 30621
	public int[] calcIndexA;

	// Token: 0x0400779E RID: 30622
	public int[] calcIndexB;

	// Token: 0x0400779F RID: 30623
	private bool meshInit = true;
}
