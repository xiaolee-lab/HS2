using System;
using UnityEngine;

// Token: 0x02000A48 RID: 2632
public class SkinnedMetaballSeed : MetaballSeedBase
{
	// Token: 0x06004E36 RID: 20022 RVA: 0x001DEE80 File Offset: 0x001DD280
	[ContextMenu("CreateMesh")]
	public override void CreateMesh()
	{
		base.CleanupBoneRoot();
		this._rootCell = new SkinnedMetaballCell();
		this._rootCell.radius = this.sourceRoot.Radius;
		this._rootCell.tag = this.sourceRoot.gameObject.name;
		this._rootCell.density = this.sourceRoot.Density;
		this._rootCell.modelPosition = this.sourceRoot.transform.position - base.transform.position;
		Matrix4x4 worldToLocalMatrix = this.skinnedMesh.transform.worldToLocalMatrix;
		this.ConstructTree(this.sourceRoot.transform, this._rootCell, worldToLocalMatrix);
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
		Transform[] bones;
		this._errorMsg = MetaballBuilder.Instance.CreateMeshWithSkeleton(this._rootCell, this.boneRoot.transform, this.powerThreshold, base.GridSize, uDir, vDir, uvOffset, out mesh, out bones, this.cellObjPrefab, this.bReverse, fixedBounds, this.bAutoGridSize, this.autoGridQuarity);
		if (!string.IsNullOrEmpty(this._errorMsg))
		{
			return;
		}
		mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 500f);
		this.skinnedMesh.bones = bones;
		this.skinnedMesh.sharedMesh = mesh;
		this.skinnedMesh.localBounds = new Bounds(Vector3.zero, Vector3.one * 500f);
		this.skinnedMesh.rootBone = this.boneRoot;
		base.EnumBoneNodes();
	}

	// Token: 0x06004E37 RID: 20023 RVA: 0x001DF03C File Offset: 0x001DD43C
	private void ConstructTree(Transform node, SkinnedMetaballCell cell, Matrix4x4 toLocalMtx)
	{
		for (int i = 0; i < node.childCount; i++)
		{
			Transform child = node.GetChild(i);
			MetaballNode component = child.GetComponent<MetaballNode>();
			if (component != null)
			{
				SkinnedMetaballCell skinnedMetaballCell = cell.AddChild(toLocalMtx * (child.transform.position - base.transform.position), component.Radius, 0f);
				skinnedMetaballCell.tag = child.gameObject.name;
				skinnedMetaballCell.density = component.Density;
				this.ConstructTree(child, skinnedMetaballCell, toLocalMtx);
			}
		}
	}

	// Token: 0x17000E99 RID: 3737
	// (get) Token: 0x06004E38 RID: 20024 RVA: 0x001DF0DE File Offset: 0x001DD4DE
	// (set) Token: 0x06004E39 RID: 20025 RVA: 0x001DF0EB File Offset: 0x001DD4EB
	public override Mesh Mesh
	{
		get
		{
			return this.skinnedMesh.sharedMesh;
		}
		set
		{
			this.skinnedMesh.sharedMesh = value;
		}
	}

	// Token: 0x17000E9A RID: 3738
	// (get) Token: 0x06004E3A RID: 20026 RVA: 0x001DF0F9 File Offset: 0x001DD4F9
	public override bool IsTreeShape
	{
		get
		{
			return true;
		}
	}

	// Token: 0x04004766 RID: 18278
	public SkinnedMeshRenderer skinnedMesh;

	// Token: 0x04004767 RID: 18279
	private SkinnedMetaballCell _rootCell;
}
