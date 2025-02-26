using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000841 RID: 2113
public class SkinnedCollisionHelper : MonoBehaviour
{
	// Token: 0x060035FB RID: 13819 RVA: 0x0013E1E6 File Offset: 0x0013C5E6
	private void Start()
	{
		this.Init();
	}

	// Token: 0x060035FC RID: 13820 RVA: 0x0013E1F0 File Offset: 0x0013C5F0
	public bool Init()
	{
		if (this.IsInit)
		{
			return true;
		}
		this.skinnedMeshRenderer = base.GetComponent<SkinnedMeshRenderer>();
		this.meshCollider = base.GetComponent<MeshCollider>();
		if (this.meshCollider != null && this.skinnedMeshRenderer != null)
		{
			this.meshCalc = UnityEngine.Object.Instantiate<Mesh>(this.skinnedMeshRenderer.sharedMesh);
			this.meshCalc.name = this.skinnedMeshRenderer.sharedMesh.name + "_calc";
			this.meshCollider.sharedMesh = this.meshCalc;
			this.meshCalc.MarkDynamic();
			Vector3[] vertices = this.skinnedMeshRenderer.sharedMesh.vertices;
			Vector3[] normals = this.skinnedMeshRenderer.sharedMesh.normals;
			Matrix4x4[] bindposes = this.skinnedMeshRenderer.sharedMesh.bindposes;
			BoneWeight[] boneWeights = this.skinnedMeshRenderer.sharedMesh.boneWeights;
			this.nodeWeights = new SkinnedCollisionHelper.CWeightList[this.skinnedMeshRenderer.bones.Length];
			for (int i = 0; i < this.skinnedMeshRenderer.bones.Length; i++)
			{
				this.nodeWeights[i] = new SkinnedCollisionHelper.CWeightList();
				this.nodeWeights[i].transform = this.skinnedMeshRenderer.bones[i];
			}
			for (int j = 0; j < vertices.Length; j++)
			{
				BoneWeight boneWeight = boneWeights[j];
				if (boneWeight.weight0 != 0f)
				{
					Vector3 p = bindposes[boneWeight.boneIndex0].MultiplyPoint3x4(vertices[j]);
					Vector3 n = bindposes[boneWeight.boneIndex0].MultiplyPoint3x4(normals[j]);
					this.nodeWeights[boneWeight.boneIndex0].weights.Add(new SkinnedCollisionHelper.CVertexWeight(j, p, n, boneWeight.weight0));
				}
				if (boneWeight.weight1 != 0f)
				{
					Vector3 p2 = bindposes[boneWeight.boneIndex1].MultiplyPoint3x4(vertices[j]);
					Vector3 n2 = bindposes[boneWeight.boneIndex1].MultiplyPoint3x4(normals[j]);
					this.nodeWeights[boneWeight.boneIndex1].weights.Add(new SkinnedCollisionHelper.CVertexWeight(j, p2, n2, boneWeight.weight1));
				}
				if (boneWeight.weight2 != 0f)
				{
					Vector3 p3 = bindposes[boneWeight.boneIndex2].MultiplyPoint3x4(vertices[j]);
					Vector3 n3 = bindposes[boneWeight.boneIndex2].MultiplyPoint3x4(normals[j]);
					this.nodeWeights[boneWeight.boneIndex2].weights.Add(new SkinnedCollisionHelper.CVertexWeight(j, p3, n3, boneWeight.weight2));
				}
				if (boneWeight.weight3 != 0f)
				{
					Vector3 p4 = bindposes[boneWeight.boneIndex3].MultiplyPoint3x4(vertices[j]);
					Vector3 n4 = bindposes[boneWeight.boneIndex3].MultiplyPoint3x4(normals[j]);
					this.nodeWeights[boneWeight.boneIndex3].weights.Add(new SkinnedCollisionHelper.CVertexWeight(j, p4, n4, boneWeight.weight3));
				}
			}
			this.UpdateCollisionMesh(false);
			this.IsInit = true;
			return true;
		}
		return false;
	}

	// Token: 0x060035FD RID: 13821 RVA: 0x0013E578 File Offset: 0x0013C978
	public bool Release()
	{
		UnityEngine.Object.Destroy(this.meshCalc);
		return true;
	}

	// Token: 0x060035FE RID: 13822 RVA: 0x0013E588 File Offset: 0x0013C988
	public void UpdateCollisionMesh(bool _bRelease = true)
	{
		Vector3[] vertices = this.meshCalc.vertices;
		for (int i = 0; i < vertices.Length; i++)
		{
			vertices[i] = Vector3.zero;
		}
		foreach (SkinnedCollisionHelper.CWeightList cweightList in this.nodeWeights)
		{
			Matrix4x4 localToWorldMatrix = cweightList.transform.localToWorldMatrix;
			IEnumerator enumerator = cweightList.weights.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					SkinnedCollisionHelper.CVertexWeight cvertexWeight = (SkinnedCollisionHelper.CVertexWeight)obj;
					vertices[cvertexWeight.index] += localToWorldMatrix.MultiplyPoint3x4(cvertexWeight.localPosition) * cvertexWeight.weight;
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
		for (int k = 0; k < vertices.Length; k++)
		{
			vertices[k] = base.transform.InverseTransformPoint(vertices[k]);
		}
		this.meshCalc.vertices = vertices;
		this.meshCollider.enabled = false;
		this.meshCollider.enabled = true;
	}

	// Token: 0x060035FF RID: 13823 RVA: 0x0013E6E8 File Offset: 0x0013CAE8
	private void Update()
	{
	}

	// Token: 0x06003600 RID: 13824 RVA: 0x0013E6EA File Offset: 0x0013CAEA
	private void LateUpdate()
	{
		if (!this.IsInit)
		{
			return;
		}
		if (this.forceUpdate)
		{
			if (this.updateOncePerFrame)
			{
				this.forceUpdate = false;
			}
			this.UpdateCollisionMesh(true);
		}
	}

	// Token: 0x04003641 RID: 13889
	public bool forceUpdate;

	// Token: 0x04003642 RID: 13890
	public bool updateOncePerFrame = true;

	// Token: 0x04003643 RID: 13891
	public bool calcNormal = true;

	// Token: 0x04003644 RID: 13892
	private bool IsInit;

	// Token: 0x04003645 RID: 13893
	private SkinnedCollisionHelper.CWeightList[] nodeWeights;

	// Token: 0x04003646 RID: 13894
	private SkinnedMeshRenderer skinnedMeshRenderer;

	// Token: 0x04003647 RID: 13895
	private MeshCollider meshCollider;

	// Token: 0x04003648 RID: 13896
	private Mesh meshCalc;

	// Token: 0x02000842 RID: 2114
	private class CVertexWeight
	{
		// Token: 0x06003601 RID: 13825 RVA: 0x0013E71C File Offset: 0x0013CB1C
		public CVertexWeight(int i, Vector3 p, Vector3 n, float w)
		{
			this.index = i;
			this.localPosition = p;
			this.localNormal = n;
			this.weight = w;
		}

		// Token: 0x04003649 RID: 13897
		public int index;

		// Token: 0x0400364A RID: 13898
		public Vector3 localPosition;

		// Token: 0x0400364B RID: 13899
		public Vector3 localNormal;

		// Token: 0x0400364C RID: 13900
		public float weight;
	}

	// Token: 0x02000843 RID: 2115
	private class CWeightList
	{
		// Token: 0x06003602 RID: 13826 RVA: 0x0013E741 File Offset: 0x0013CB41
		public CWeightList()
		{
			this.weights = new ArrayList();
		}

		// Token: 0x0400364D RID: 13901
		public Transform transform;

		// Token: 0x0400364E RID: 13902
		public ArrayList weights;
	}
}
