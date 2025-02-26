using System;
using UnityEngine;

// Token: 0x02000A3D RID: 2621
public abstract class ImplicitSurfaceMeshCreaterBase : MonoBehaviour
{
	// Token: 0x17000E92 RID: 3730
	// (get) Token: 0x06004E0A RID: 19978 RVA: 0x001DDCC3 File Offset: 0x001DC0C3
	public float GridSize
	{
		get
		{
			return Mathf.Max(float.Epsilon, this.gridSize);
		}
	}

	// Token: 0x06004E0B RID: 19979
	public abstract void CreateMesh();

	// Token: 0x17000E93 RID: 3731
	// (get) Token: 0x06004E0C RID: 19980
	// (set) Token: 0x06004E0D RID: 19981
	public abstract Mesh Mesh { get; set; }

	// Token: 0x06004E0E RID: 19982 RVA: 0x001DDCD5 File Offset: 0x001DC0D5
	protected virtual void Update()
	{
	}

	// Token: 0x06004E0F RID: 19983 RVA: 0x001DDCD8 File Offset: 0x001DC0D8
	protected void GetUVBaseVector(out Vector3 uDir, out Vector3 vDir, out Vector3 offset)
	{
		if (this.uvProjectNode != null)
		{
			float d = Mathf.Max(this.uvProjectNode.uScale, 0.001f);
			float d2 = Mathf.Max(this.uvProjectNode.vScale, 0.001f);
			uDir = this.uvProjectNode.transform.right / d;
			vDir = this.uvProjectNode.transform.up / d2;
			offset = -this.uvProjectNode.transform.localPosition;
		}
		else
		{
			uDir = Vector3.right;
			vDir = Vector3.up;
			offset = Vector3.zero;
		}
	}

	// Token: 0x04004742 RID: 18242
	public float gridSize = 0.2f;

	// Token: 0x04004743 RID: 18243
	[Tooltip("Ignore gridSize and use automatically determined value by autoGridQuarity")]
	public bool bAutoGridSize;

	// Token: 0x04004744 RID: 18244
	[Range(0.005f, 1f)]
	public float autoGridQuarity = 0.2f;

	// Token: 0x04004745 RID: 18245
	public MetaballUVGuide uvProjectNode;

	// Token: 0x04004746 RID: 18246
	public float powerThreshold = 0.15f;

	// Token: 0x04004747 RID: 18247
	public bool bReverse;

	// Token: 0x04004748 RID: 18248
	public Bounds fixedBounds = new Bounds(Vector3.zero, Vector3.one * 10f);
}
