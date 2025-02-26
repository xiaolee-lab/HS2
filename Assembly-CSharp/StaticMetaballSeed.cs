using System;
using UnityEngine;

// Token: 0x02000A49 RID: 2633
public class StaticMetaballSeed : MetaballSeedBase
{
	// Token: 0x06004E3C RID: 20028 RVA: 0x001DF104 File Offset: 0x001DD504
	private void ConstructCellCluster(MetaballCellCluster cluster, Transform parentNode, Matrix4x4 toLocalMtx, Transform meshTrans)
	{
		for (int i = 0; i < parentNode.childCount; i++)
		{
			Transform child = parentNode.GetChild(i);
			MetaballNode component = child.GetComponent<MetaballNode>();
			if (component != null)
			{
				MetaballCell metaballCell = this._cellCluster.AddCell(meshTrans.InverseTransformPoint(child.position), 0f, new float?(component.Radius), child.gameObject.name);
				metaballCell.density = component.Density;
			}
			this.ConstructCellCluster(cluster, child, toLocalMtx, meshTrans);
		}
	}

	// Token: 0x06004E3D RID: 20029 RVA: 0x001DF190 File Offset: 0x001DD590
	private void WorldPositionBounds(Transform parentNode, ref Bounds bounds)
	{
		for (int i = 0; i < parentNode.childCount; i++)
		{
			Transform child = parentNode.GetChild(i);
			MetaballNode component = child.GetComponent<MetaballNode>();
			if (component != null)
			{
				for (int j = 0; j < 3; j++)
				{
					if (child.transform.position[j] - component.Radius < bounds.min[j])
					{
						Vector3 min = bounds.min;
						min[j] = child.transform.position[j] - component.Radius;
						bounds.min = min;
					}
					if (child.transform.position[j] + component.Radius > bounds.max[j])
					{
						Vector3 max = bounds.max;
						max[j] = child.transform.position[j] + component.Radius;
						bounds.max = max;
					}
				}
			}
			this.WorldPositionBounds(child, ref bounds);
		}
	}

	// Token: 0x06004E3E RID: 20030 RVA: 0x001DF2B4 File Offset: 0x001DD6B4
	private bool WorldPositionBoundsFirst(Transform parentNode, ref Bounds bounds)
	{
		for (int i = 0; i < parentNode.childCount; i++)
		{
			Transform child = parentNode.GetChild(i);
			MetaballNode component = child.GetComponent<MetaballNode>();
			if (component != null)
			{
				for (int j = 0; j < 3; j++)
				{
					float value = child.transform.position[j] - component.Radius;
					Vector3 vector = bounds.min;
					vector[j] = value;
					bounds.min = vector;
					vector = bounds.max;
					vector[j] = value;
					bounds.max = vector;
				}
				return true;
			}
			if (this.WorldPositionBoundsFirst(child, ref bounds))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004E3F RID: 20031 RVA: 0x001DF36C File Offset: 0x001DD76C
	[ContextMenu("CreateMesh")]
	public override void CreateMesh()
	{
		base.CleanupBoneRoot();
		this._cellCluster = new MetaballCellCluster();
		Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
		this.WorldPositionBoundsFirst(this.sourceRoot.transform, ref bounds);
		this.WorldPositionBounds(this.sourceRoot.transform, ref bounds);
		this.meshFilter.transform.position = bounds.center;
		Matrix4x4 worldToLocalMatrix = this.meshFilter.transform.worldToLocalMatrix;
		this.ConstructCellCluster(this._cellCluster, this.sourceRoot.transform, worldToLocalMatrix, this.meshFilter.transform);
		Vector3 uDir;
		Vector3 vDir;
		Vector3 uvOffset;
		base.GetUVBaseVector(out uDir, out vDir, out uvOffset);
		Bounds? fixedBounds = null;
		if (this.bUseFixedBounds)
		{
			fixedBounds = new Bounds?(this.fixedBounds);
		}
		Mesh mesh;
		this._errorMsg = MetaballBuilder.Instance.CreateMesh(this._cellCluster, this.boneRoot.transform, this.powerThreshold, base.GridSize, uDir, vDir, uvOffset, out mesh, this.cellObjPrefab, this.bReverse, fixedBounds, this.bAutoGridSize, this.autoGridQuarity);
		if (!string.IsNullOrEmpty(this._errorMsg))
		{
			return;
		}
		mesh.RecalculateBounds();
		this.meshFilter.sharedMesh = mesh;
		base.EnumBoneNodes();
	}

	// Token: 0x17000E9B RID: 3739
	// (get) Token: 0x06004E40 RID: 20032 RVA: 0x001DF4B4 File Offset: 0x001DD8B4
	// (set) Token: 0x06004E41 RID: 20033 RVA: 0x001DF4C1 File Offset: 0x001DD8C1
	public override Mesh Mesh
	{
		get
		{
			return this.meshFilter.sharedMesh;
		}
		set
		{
			this.meshFilter.sharedMesh = value;
		}
	}

	// Token: 0x17000E9C RID: 3740
	// (get) Token: 0x06004E42 RID: 20034 RVA: 0x001DF4CF File Offset: 0x001DD8CF
	public override bool IsTreeShape
	{
		get
		{
			return false;
		}
	}

	// Token: 0x04004768 RID: 18280
	public MeshFilter meshFilter;

	// Token: 0x04004769 RID: 18281
	private MetaballCellCluster _cellCluster;
}
