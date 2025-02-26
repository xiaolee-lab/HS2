using System;
using UnityEngine;

// Token: 0x02000A44 RID: 2628
public class MetaballNode : MonoBehaviour
{
	// Token: 0x17000E95 RID: 3733
	// (get) Token: 0x06004E26 RID: 20006 RVA: 0x001DE89D File Offset: 0x001DCC9D
	public virtual float Density
	{
		get
		{
			return (!this.bSubtract) ? 1f : -1f;
		}
	}

	// Token: 0x17000E96 RID: 3734
	// (get) Token: 0x06004E27 RID: 20007 RVA: 0x001DE8B9 File Offset: 0x001DCCB9
	public float Radius
	{
		get
		{
			return this.baseRadius;
		}
	}

	// Token: 0x06004E28 RID: 20008 RVA: 0x001DE8C4 File Offset: 0x001DCCC4
	private void OnDrawGizmosSelected()
	{
		if (this._seed == null)
		{
			this._seed = Utils.FindComponentInParents<MetaballSeedBase>(base.transform);
		}
		if (this.Density == 0f)
		{
			return;
		}
		if (this._seed != null && this._seed.sourceRoot != null && this._seed.sourceRoot.gameObject == base.gameObject)
		{
			return;
		}
		Gizmos.color = ((!this.bSubtract) ? Color.white : Color.red);
		float num = this.Radius;
		if (this._seed != null)
		{
			num *= 1f - Mathf.Sqrt(this._seed.powerThreshold);
		}
		Matrix4x4 matrix = Gizmos.matrix;
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.DrawWireSphere(Vector3.zero, num);
		MetaballNode component = base.transform.parent.GetComponent<MetaballNode>();
		if (component != null && component.Density != 0f && this._seed != null && this._seed.IsTreeShape)
		{
			if (this._boneMesh == null)
			{
				this._boneMesh = new Mesh();
				Vector3[] array = new Vector3[5];
				Vector3[] array2 = new Vector3[5];
				int[] array3 = new int[6];
				array[0] = new Vector3(0.1f, 0f, 0f);
				array[1] = new Vector3(-0.1f, 0f, 0f);
				array[2] = new Vector3(0f, 0.1f, 0f);
				array[3] = new Vector3(0f, -0.1f, 0f);
				array[4] = new Vector3(0f, 0f, 1f);
				array2[0] = new Vector3(0f, 0f, 1f);
				array2[1] = new Vector3(0f, 0f, 1f);
				array2[2] = new Vector3(0f, 0f, 1f);
				array2[3] = new Vector3(0f, 0f, 1f);
				array2[4] = new Vector3(0f, 0f, 1f);
				array3[0] = 0;
				array3[1] = 1;
				array3[2] = 4;
				array3[3] = 2;
				array3[4] = 3;
				array3[5] = 4;
				this._boneMesh.vertices = array;
				this._boneMesh.normals = array2;
				this._boneMesh.SetIndices(array3, MeshTopology.Triangles, 0);
			}
			Vector3 vector = Vector3.one;
			Vector3 position = base.transform.position;
			Vector3 position2 = base.transform.parent.position;
			if ((position2 - position).sqrMagnitude >= 1E-45f)
			{
				vector *= (position2 - position).magnitude;
				Matrix4x4 matrix2 = Matrix4x4.TRS(position2, Quaternion.LookRotation(position - position2), vector);
				Gizmos.color = Color.blue;
				Gizmos.matrix = matrix2;
				Gizmos.DrawWireMesh(this._boneMesh);
			}
		}
		Gizmos.color = Color.white;
		Gizmos.matrix = matrix;
	}

	// Token: 0x04004759 RID: 18265
	public float baseRadius = 1f;

	// Token: 0x0400475A RID: 18266
	public bool bSubtract;

	// Token: 0x0400475B RID: 18267
	private MetaballSeedBase _seed;

	// Token: 0x0400475C RID: 18268
	private Mesh _boneMesh;
}
