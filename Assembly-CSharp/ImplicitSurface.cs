using System;
using UnityEngine;

// Token: 0x02000A3C RID: 2620
public class ImplicitSurface : ImplicitSurfaceMeshCreaterBase
{
	// Token: 0x17000E90 RID: 3728
	// (get) Token: 0x06004E02 RID: 19970 RVA: 0x001DDDAA File Offset: 0x001DC1AA
	public MeshFilter MeshFilter
	{
		get
		{
			if (this.meshFilter == null)
			{
				this.meshFilter = base.GetComponent<MeshFilter>();
			}
			return this.meshFilter;
		}
	}

	// Token: 0x17000E91 RID: 3729
	// (get) Token: 0x06004E03 RID: 19971 RVA: 0x001DDDCF File Offset: 0x001DC1CF
	// (set) Token: 0x06004E04 RID: 19972 RVA: 0x001DDDDC File Offset: 0x001DC1DC
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

	// Token: 0x06004E05 RID: 19973 RVA: 0x001DDDEC File Offset: 0x001DC1EC
	protected void ResetMaps()
	{
		int maxGridCellCount = MetaballBuilder.MaxGridCellCount;
		float num2;
		if (this.bAutoGridSize)
		{
			int num = (int)((float)maxGridCellCount * Mathf.Clamp01(this.autoGridQuarity));
			num2 = Mathf.Pow(this.fixedBounds.size.x * this.fixedBounds.size.y * this.fixedBounds.size.z / (float)num, 0.33333334f);
		}
		else
		{
			num2 = this.gridSize;
		}
		int num3 = Mathf.CeilToInt(this.fixedBounds.extents.x / num2) + 1;
		int num4 = Mathf.CeilToInt(this.fixedBounds.extents.y / num2) + 1;
		int num5 = Mathf.CeilToInt(this.fixedBounds.extents.z / num2) + 1;
		this._countX = num3 * 2;
		this._countY = num4 * 2;
		this._countZ = num5 * 2;
		Vector3 b = new Vector3((float)num3 * num2, (float)num4 * num2, (float)num5 * num2);
		Vector3 a = this.fixedBounds.center - b;
		int countX = this._countX;
		int num6 = this._countX * this._countY;
		int num7 = this._countX * this._countY * this._countZ;
		this._positionMap = new Vector3[num7];
		this._powerMap = new float[num7];
		this._powerMapMask = new float[num7];
		for (int i = 0; i < num7; i++)
		{
			this._powerMap[i] = 0f;
		}
		for (int j = 0; j < this._countZ; j++)
		{
			for (int k = 0; k < this._countY; k++)
			{
				for (int l = 0; l < this._countX; l++)
				{
					int num8 = l + k * countX + j * num6;
					this._positionMap[num8] = a + new Vector3(num2 * (float)l, num2 * (float)k, num2 * (float)j);
					if (j == 0 || j == this._countZ - 1 || k == 0 || k == this._countY - 1 || l == 0 || l == this._countX - 1)
					{
						this._powerMapMask[num8] = 0f;
					}
					else
					{
						this._powerMapMask[num8] = 1f;
					}
				}
			}
		}
		this.InitializePowerMap();
		this._bMapsDirty = false;
	}

	// Token: 0x06004E06 RID: 19974 RVA: 0x001DE094 File Offset: 0x001DC494
	protected virtual void InitializePowerMap()
	{
		int num = this._countX * this._countY * this._countZ;
		for (int i = 0; i < num; i++)
		{
			this._powerMap[i] = 0f;
		}
	}

	// Token: 0x06004E07 RID: 19975 RVA: 0x001DE0D8 File Offset: 0x001DC4D8
	public override void CreateMesh()
	{
		if (this._bMapsDirty)
		{
			this.ResetMaps();
		}
		Vector3 uDir;
		Vector3 vDir;
		Vector3 uvOffset;
		base.GetUVBaseVector(out uDir, out vDir, out uvOffset);
		Mesh mesh = MetaballBuilder.Instance.CreateImplicitSurfaceMesh(this._countX, this._countY, this._countZ, this._positionMap, this._powerMap, this.bReverse, this.powerThreshold, uDir, vDir, uvOffset);
		mesh.RecalculateBounds();
		this.Mesh = mesh;
		if (this.meshCollider != null)
		{
			this.meshCollider.sharedMesh = mesh;
		}
	}

	// Token: 0x06004E08 RID: 19976 RVA: 0x001DE164 File Offset: 0x001DC564
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireCube(this.fixedBounds.center + base.transform.position, this.fixedBounds.size);
	}

	// Token: 0x04004739 RID: 18233
	public MeshFilter meshFilter;

	// Token: 0x0400473A RID: 18234
	public MeshCollider meshCollider;

	// Token: 0x0400473B RID: 18235
	protected Vector3[] _positionMap;

	// Token: 0x0400473C RID: 18236
	protected float[] _powerMap;

	// Token: 0x0400473D RID: 18237
	protected float[] _powerMapMask;

	// Token: 0x0400473E RID: 18238
	protected int _countX;

	// Token: 0x0400473F RID: 18239
	protected int _countY;

	// Token: 0x04004740 RID: 18240
	protected int _countZ;

	// Token: 0x04004741 RID: 18241
	private bool _bMapsDirty = true;
}
