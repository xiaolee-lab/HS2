using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000402 RID: 1026
public class MeshTool : MonoBehaviour
{
	// Token: 0x0600123E RID: 4670 RVA: 0x0007251B File Offset: 0x0007091B
	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Token: 0x0600123F RID: 4671 RVA: 0x0007252C File Offset: 0x0007092C
	private void Update()
	{
		Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		if (Physics.Raycast(ray.origin, ray.direction, out this.m_HitInfo))
		{
			Vector3 a = (this.m_Method != MeshTool.ExtrudeMethod.Vertical) ? this.m_HitInfo.normal : Vector3.up;
			if (Input.GetMouseButton(0) || (Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift)))
			{
				this.ModifyMesh(this.m_Power * a, this.m_HitInfo.point);
			}
			if (Input.GetMouseButton(1) || (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift)))
			{
				this.ModifyMesh(-this.m_Power * a, this.m_HitInfo.point);
			}
		}
	}

	// Token: 0x06001240 RID: 4672 RVA: 0x00072624 File Offset: 0x00070A24
	private void ModifyMesh(Vector3 displacement, Vector3 center)
	{
		foreach (MeshFilter meshFilter in this.m_Filters)
		{
			Mesh mesh = meshFilter.mesh;
			Vector3[] vertices = mesh.vertices;
			for (int i = 0; i < vertices.Length; i++)
			{
				Vector3 pos = meshFilter.transform.TransformPoint(vertices[i]);
				vertices[i] += displacement * MeshTool.Gaussian(pos, center, this.m_Radius);
			}
			mesh.vertices = vertices;
			mesh.RecalculateBounds();
			MeshCollider component = meshFilter.GetComponent<MeshCollider>();
			if (component != null)
			{
				component.sharedMesh = new Mesh
				{
					vertices = mesh.vertices,
					triangles = mesh.triangles
				};
			}
		}
	}

	// Token: 0x06001241 RID: 4673 RVA: 0x00072738 File Offset: 0x00070B38
	private static float Gaussian(Vector3 pos, Vector3 mean, float dev)
	{
		float num = pos.x - mean.x;
		float num2 = pos.y - mean.y;
		float num3 = pos.z - mean.z;
		float num4 = 1f / (6.2831855f * dev * dev);
		return num4 * Mathf.Pow(2.7182817f, -(num * num + num2 * num2 + num3 * num3) / (2f * dev * dev));
	}

	// Token: 0x040014CE RID: 5326
	public List<MeshFilter> m_Filters = new List<MeshFilter>();

	// Token: 0x040014CF RID: 5327
	public float m_Radius = 1.5f;

	// Token: 0x040014D0 RID: 5328
	public float m_Power = 2f;

	// Token: 0x040014D1 RID: 5329
	public MeshTool.ExtrudeMethod m_Method;

	// Token: 0x040014D2 RID: 5330
	private RaycastHit m_HitInfo = default(RaycastHit);

	// Token: 0x02000403 RID: 1027
	public enum ExtrudeMethod
	{
		// Token: 0x040014D4 RID: 5332
		Vertical,
		// Token: 0x040014D5 RID: 5333
		MeshNormal
	}
}
