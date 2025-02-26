using System;
using UnityEngine;

// Token: 0x02000060 RID: 96
[AddComponentMenu("AQUAS/Buoyancy")]
[RequireComponent(typeof(Rigidbody))]
public class AQUAS_Buoyancy : MonoBehaviour
{
	// Token: 0x06000079 RID: 121 RVA: 0x00006670 File Offset: 0x00004A70
	private void Start()
	{
		this.mesh = base.GetComponent<MeshFilter>().mesh;
		this.vertices = this.mesh.vertices;
		this.triangles = this.mesh.triangles;
		this.rb = base.GetComponent<Rigidbody>();
		this.regWaterDensity = this.waterDensity;
		this.maxWaterDensity = this.regWaterDensity + this.regWaterDensity * 0.5f * this.dynamicSurface;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x000066E8 File Offset: 0x00004AE8
	private void FixedUpdate()
	{
		if (this.balanceFactor.x < 0.001f)
		{
			this.balanceFactor.x = 0.001f;
		}
		if (this.balanceFactor.y < 0.001f)
		{
			this.balanceFactor.y = 0.001f;
		}
		if (this.balanceFactor.z < 0.001f)
		{
			this.balanceFactor.z = 0.001f;
		}
		this.AddForce();
	}

	// Token: 0x0600007B RID: 123 RVA: 0x0000676C File Offset: 0x00004B6C
	private void Update()
	{
		this.regWaterDensity = this.waterDensity;
		this.maxWaterDensity = this.regWaterDensity + this.regWaterDensity * 0.5f * this.dynamicSurface;
		this.effWaterDensity = (this.maxWaterDensity - this.regWaterDensity) / 2f + this.regWaterDensity + Mathf.Sin(Time.time * this.bounceFrequency) * (this.maxWaterDensity - this.regWaterDensity) / 2f;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x000067EC File Offset: 0x00004BEC
	private void AddForce()
	{
		for (int i = 0; i < this.triangles.Length; i += 3)
		{
			Vector3 p = this.vertices[this.triangles[i]];
			Vector3 p2 = this.vertices[this.triangles[i + 1]];
			Vector3 p3 = this.vertices[this.triangles[i + 2]];
			float num = this.waterLevel - this.Center(p, p2, p3).y;
			if (num > 0f && this.Center(p, p2, p3).y > (this.Center(p, p2, p3) + this.Normal(p, p2, p3)).y)
			{
				float y = this.effWaterDensity * Physics.gravity.y * num * this.Area(p, p2, p3) * this.Normal(p, p2, p3).normalized.y;
				if (this.useBalanceFactor)
				{
					this.rb.AddForceAtPosition(new Vector3(0f, y, 0f), base.transform.TransformPoint(new Vector3(base.transform.InverseTransformPoint(this.Center(p, p2, p3)).x / (this.balanceFactor.x * base.transform.localScale.x * 1000f), base.transform.InverseTransformPoint(this.Center(p, p2, p3)).y / (this.balanceFactor.y * base.transform.localScale.x * 1000f), base.transform.InverseTransformPoint(this.Center(p, p2, p3)).z / (this.balanceFactor.z * base.transform.localScale.x * 1000f))));
				}
				else
				{
					this.rb.AddForceAtPosition(new Vector3(0f, y, 0f), base.transform.TransformPoint(new Vector3(base.transform.InverseTransformPoint(this.Center(p, p2, p3)).x, base.transform.InverseTransformPoint(this.Center(p, p2, p3)).y, base.transform.InverseTransformPoint(this.Center(p, p2, p3)).z)));
				}
				if (this.debug == AQUAS_Buoyancy.debugModes.showAffectedFaces)
				{
				}
				if (this.debug == AQUAS_Buoyancy.debugModes.showForceRepresentation)
				{
				}
				if (this.debug == AQUAS_Buoyancy.debugModes.showReferenceVolume)
				{
				}
			}
		}
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00006AB0 File Offset: 0x00004EB0
	private Vector3 Center(Vector3 p1, Vector3 p2, Vector3 p3)
	{
		Vector3 position = (p1 + p2 + p3) / 3f;
		return base.transform.TransformPoint(position);
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00006AE4 File Offset: 0x00004EE4
	private Vector3 Normal(Vector3 p1, Vector3 p2, Vector3 p3)
	{
		return Vector3.Cross(base.transform.TransformPoint(p2) - base.transform.TransformPoint(p1), base.transform.TransformPoint(p3) - base.transform.TransformPoint(p1)).normalized;
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00006B3C File Offset: 0x00004F3C
	private float Area(Vector3 p1, Vector3 p2, Vector3 p3)
	{
		float num = Vector3.Distance(new Vector3(base.transform.TransformPoint(p1).x, this.Center(p1, p2, p3).y, base.transform.TransformPoint(p1).z), new Vector3(base.transform.TransformPoint(p2).x, this.Center(p1, p2, p3).y, base.transform.TransformPoint(p2).z));
		float num2 = Vector3.Distance(new Vector3(base.transform.TransformPoint(p3).x, this.Center(p1, p2, p3).y, base.transform.TransformPoint(p3).z), new Vector3(base.transform.TransformPoint(p1).x, this.Center(p1, p2, p3).y, base.transform.TransformPoint(p1).z));
		return num * num2 * Mathf.Sin(Vector3.Angle(new Vector3(base.transform.TransformPoint(p2).x, this.Center(p1, p2, p3).y, base.transform.TransformPoint(p2).z) - new Vector3(base.transform.TransformPoint(p1).x, this.Center(p1, p2, p3).y, base.transform.TransformPoint(p1).z), new Vector3(base.transform.TransformPoint(p3).x, this.Center(p1, p2, p3).y, base.transform.TransformPoint(p3).z) - new Vector3(base.transform.TransformPoint(p1).x, this.Center(p1, p2, p3).y, base.transform.TransformPoint(p1).z)) * 0.017453292f) / 2f;
	}

	// Token: 0x0400016A RID: 362
	public float waterLevel;

	// Token: 0x0400016B RID: 363
	public float waterDensity;

	// Token: 0x0400016C RID: 364
	[Space(5f)]
	public bool useBalanceFactor;

	// Token: 0x0400016D RID: 365
	public Vector3 balanceFactor;

	// Token: 0x0400016E RID: 366
	[Space(20f)]
	[Range(0f, 1f)]
	public float dynamicSurface = 0.3f;

	// Token: 0x0400016F RID: 367
	[Range(1f, 10f)]
	public float bounceFrequency = 3f;

	// Token: 0x04000170 RID: 368
	[Space(5f)]
	[Header("Debugging can be ver performance heavy!")]
	public AQUAS_Buoyancy.debugModes debug;

	// Token: 0x04000171 RID: 369
	private Vector3[] vertices;

	// Token: 0x04000172 RID: 370
	private int[] triangles;

	// Token: 0x04000173 RID: 371
	private Mesh mesh;

	// Token: 0x04000174 RID: 372
	private Rigidbody rb;

	// Token: 0x04000175 RID: 373
	private float effWaterDensity;

	// Token: 0x04000176 RID: 374
	private float regWaterDensity;

	// Token: 0x04000177 RID: 375
	private float maxWaterDensity;

	// Token: 0x02000061 RID: 97
	public enum debugModes
	{
		// Token: 0x04000179 RID: 377
		none,
		// Token: 0x0400017A RID: 378
		showAffectedFaces,
		// Token: 0x0400017B RID: 379
		showForceRepresentation,
		// Token: 0x0400017C RID: 380
		showReferenceVolume
	}
}
