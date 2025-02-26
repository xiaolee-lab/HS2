using System;
using UnityEngine;

// Token: 0x020005A3 RID: 1443
public class RadialFillCursor : MonoBehaviour
{
	// Token: 0x06002178 RID: 8568 RVA: 0x000B8BAC File Offset: 0x000B6FAC
	private void Update()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = 10f;
		base.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
		if (Input.GetKey(KeyCode.W))
		{
			this.radius += Time.deltaTime * this.radiusSpeed;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			this.radius -= Time.deltaTime * this.radiusSpeed;
		}
		if (this.radius < 0f)
		{
			this.radius = 0f;
		}
		if (Input.GetKey(KeyCode.A))
		{
			this.rotationAngle += Time.deltaTime * this.rotationSpeed;
		}
		else if (Input.GetKey(KeyCode.D))
		{
			this.rotationAngle -= Time.deltaTime * this.rotationSpeed;
		}
		if (Input.GetKey(KeyCode.Q))
		{
			this.angle += Time.deltaTime * this.angleSpeed;
		}
		else if (Input.GetKey(KeyCode.E))
		{
			this.angle -= Time.deltaTime * this.angleSpeed;
		}
		this.angle = Mathf.Clamp(this.angle, 0f, 360f);
		if (Input.GetKey(KeyCode.X))
		{
			this.strenght += Time.deltaTime * this.strenghtSpeed;
		}
		else if (Input.GetKey(KeyCode.Z))
		{
			this.strenght -= Time.deltaTime * this.strenghtSpeed;
		}
		this.strenght = Mathf.Clamp(this.strenght, 0.1f, this.strenghtMax);
		Color color = this.centerColor;
		color.a = 0.34f + this.strenght / this.strenghtMax * 0.66f;
		this.centerColor = color;
		if (this.vertices == null || this.vertices.Length != this.meshAngleSeparation + 1)
		{
			this.vertices = new Vector3[this.meshAngleSeparation + 2];
			this.colors = new Color[this.meshAngleSeparation + 2];
			this.triangles = new int[this.meshAngleSeparation * 3 + 3];
		}
		Mesh mesh = this.meshFilter.mesh;
		if (mesh == null)
		{
			mesh = new Mesh();
			this.meshFilter.mesh = mesh;
		}
		this.vertices[0] = Vector3.zero;
		this.colors[0] = this.centerColor;
		for (int i = 1; i < this.meshAngleSeparation + 2; i++)
		{
			float num = this.angle / (float)this.meshAngleSeparation * (float)(i - 1) + this.rotationAngle;
			float num2 = Mathf.Cos(0.017453292f * num);
			float num3 = Mathf.Sin(0.017453292f * num);
			float x = this.radius * num2;
			float y = this.radius * num3;
			this.tempV3.x = x;
			this.tempV3.y = y;
			this.tempV3.z = 0f;
			this.vertices[i] = this.tempV3;
			this.colors[i] = this.externalColor;
		}
		int num4 = 0;
		for (int j = 0; j < this.meshAngleSeparation * 3; j += 3)
		{
			this.triangles[j] = 0;
			this.triangles[j + 1] = num4 + 2;
			this.triangles[j + 2] = num4 + 1;
			num4++;
		}
		mesh.Clear();
		mesh.vertices = this.vertices;
		mesh.triangles = this.triangles;
		mesh.colors = this.colors;
	}

	// Token: 0x06002179 RID: 8569 RVA: 0x000B8F88 File Offset: 0x000B7388
	public void Show(bool b)
	{
		this.meshFilter.gameObject.SetActive(b);
	}

	// Token: 0x040020F7 RID: 8439
	[Header("Radial Data")]
	public float radius = 8f;

	// Token: 0x040020F8 RID: 8440
	public float strenght = 1f;

	// Token: 0x040020F9 RID: 8441
	public float strenghtMax = 10f;

	// Token: 0x040020FA RID: 8442
	public float angle = 90f;

	// Token: 0x040020FB RID: 8443
	public float rotationAngle = -45f;

	// Token: 0x040020FC RID: 8444
	[Header("Input Speed")]
	public float radiusSpeed = 8f;

	// Token: 0x040020FD RID: 8445
	public float strenghtSpeed = 8f;

	// Token: 0x040020FE RID: 8446
	public float rotationSpeed = 80f;

	// Token: 0x040020FF RID: 8447
	public float angleSpeed = 80f;

	// Token: 0x04002100 RID: 8448
	[Header("Mesh Data")]
	public int meshAngleSeparation = 50;

	// Token: 0x04002101 RID: 8449
	public MeshFilter meshFilter;

	// Token: 0x04002102 RID: 8450
	public Color centerColor;

	// Token: 0x04002103 RID: 8451
	public Color externalColor;

	// Token: 0x04002104 RID: 8452
	private Vector3[] vertices;

	// Token: 0x04002105 RID: 8453
	private Color[] colors;

	// Token: 0x04002106 RID: 8454
	private int[] triangles;

	// Token: 0x04002107 RID: 8455
	private Vector3 tempV3 = Vector3.right;
}
