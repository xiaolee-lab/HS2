using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000674 RID: 1652
public class CreateHills : MonoBehaviour
{
	// Token: 0x060026D3 RID: 9939 RVA: 0x000E07CC File Offset: 0x000DEBCC
	private void Start()
	{
		this.storedPosition = this.ball.transform.position;
		this.splinePoints = new Vector2[this.numberOfHills * 2 + 1];
		this.hills = new VectorLine("Hills", new List<Vector2>(this.numberOfPoints), this.hillTexture, 12f, LineType.Continuous, Joins.Weld);
		this.hills.useViewportCoords = true;
		this.hills.collider = true;
		this.hills.physicsMaterial = this.hillPhysicsMaterial;
		UnityEngine.Random.InitState(95);
		this.CreateHillLine();
	}

	// Token: 0x060026D4 RID: 9940 RVA: 0x000E0864 File Offset: 0x000DEC64
	private void OnGUI()
	{
		if (GUI.Button(new Rect(10f, 10f, 150f, 40f), "Make new hills"))
		{
			this.CreateHillLine();
			this.ball.transform.position = this.storedPosition;
			this.ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			this.ball.GetComponent<Rigidbody2D>().WakeUp();
		}
	}

	// Token: 0x060026D5 RID: 9941 RVA: 0x000E08DC File Offset: 0x000DECDC
	private void CreateHillLine()
	{
		this.splinePoints[0] = new Vector2(-0.02f, UnityEngine.Random.Range(0.1f, 0.6f));
		float num = 0f;
		float num2 = 1f / (float)(this.numberOfHills * 2);
		int i;
		for (i = 1; i < this.splinePoints.Length; i += 2)
		{
			num += num2;
			this.splinePoints[i] = new Vector2(num, UnityEngine.Random.Range(0.3f, 0.7f));
			num += num2;
			this.splinePoints[i + 1] = new Vector2(num, UnityEngine.Random.Range(0.1f, 0.6f));
		}
		this.splinePoints[i - 1] = new Vector2(1.02f, UnityEngine.Random.Range(0.1f, 0.6f));
		this.hills.MakeSpline(this.splinePoints);
		this.hills.Draw();
	}

	// Token: 0x0400270B RID: 9995
	public Texture hillTexture;

	// Token: 0x0400270C RID: 9996
	public PhysicsMaterial2D hillPhysicsMaterial;

	// Token: 0x0400270D RID: 9997
	public int numberOfPoints = 100;

	// Token: 0x0400270E RID: 9998
	public int numberOfHills = 4;

	// Token: 0x0400270F RID: 9999
	public GameObject ball;

	// Token: 0x04002710 RID: 10000
	private Vector3 storedPosition;

	// Token: 0x04002711 RID: 10001
	private VectorLine hills;

	// Token: 0x04002712 RID: 10002
	private Vector2[] splinePoints;
}
