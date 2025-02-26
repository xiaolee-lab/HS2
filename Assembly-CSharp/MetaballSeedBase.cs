using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A46 RID: 2630
public abstract class MetaballSeedBase : ImplicitSurfaceMeshCreaterBase
{
	// Token: 0x17000E98 RID: 3736
	// (get) Token: 0x06004E2C RID: 20012
	public abstract bool IsTreeShape { get; }

	// Token: 0x06004E2D RID: 20013 RVA: 0x001DECA0 File Offset: 0x001DD0A0
	private void OnDrawGizmos()
	{
		if (this.bUseFixedBounds)
		{
			Gizmos.color = Color.white;
			Gizmos.DrawWireCube(this.fixedBounds.center + base.transform.position, this.fixedBounds.size);
		}
	}

	// Token: 0x06004E2E RID: 20014 RVA: 0x001DECF0 File Offset: 0x001DD0F0
	protected void EnumBoneNodes()
	{
		List<GameObject> list = new List<GameObject>();
		this.EnumerateGameObjects(this.boneRoot.gameObject, list);
		this._boneNodes = list.ToArray();
	}

	// Token: 0x06004E2F RID: 20015 RVA: 0x001DED24 File Offset: 0x001DD124
	private void EnumerateGameObjects(GameObject parent, List<GameObject> list)
	{
		for (int i = 0; i < parent.transform.childCount; i++)
		{
			GameObject gameObject = parent.transform.GetChild(i).gameObject;
			list.Add(gameObject);
			this.EnumerateGameObjects(gameObject, list);
		}
	}

	// Token: 0x06004E30 RID: 20016 RVA: 0x001DED70 File Offset: 0x001DD170
	protected void CleanupBoneRoot()
	{
		if (this._boneNodes == null)
		{
			this._boneNodes = new GameObject[0];
		}
		int num = this._boneNodes.Length;
		for (int i = 0; i < num; i++)
		{
			if (!(this._boneNodes[i] == null))
			{
				this._boneNodes[i].transform.DetachChildren();
				UnityEngine.Object.Destroy(this._boneNodes[i]);
			}
		}
	}

	// Token: 0x0400475D RID: 18269
	public Transform boneRoot;

	// Token: 0x0400475E RID: 18270
	public MetaballNode sourceRoot;

	// Token: 0x0400475F RID: 18271
	public MetaballCellObject cellObjPrefab;

	// Token: 0x04004760 RID: 18272
	public float baseRadius = 1f;

	// Token: 0x04004761 RID: 18273
	public bool bUseFixedBounds;

	// Token: 0x04004762 RID: 18274
	protected string _errorMsg;

	// Token: 0x04004763 RID: 18275
	[SerializeField]
	private GameObject[] _boneNodes = new GameObject[0];
}
