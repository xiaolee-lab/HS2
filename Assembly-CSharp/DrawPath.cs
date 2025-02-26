using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000671 RID: 1649
public class DrawPath : MonoBehaviour
{
	// Token: 0x060026C6 RID: 9926 RVA: 0x000E00A0 File Offset: 0x000DE4A0
	private void Start()
	{
		this.pathLine = new VectorLine("Path", new List<Vector3>(), this.lineTex, 12f, LineType.Continuous);
		this.pathLine.color = Color.green;
		this.pathLine.textureScale = 1f;
		this.MakeBall();
		base.StartCoroutine(this.SamplePoints(this.ball.transform));
	}

	// Token: 0x060026C7 RID: 9927 RVA: 0x000E0114 File Offset: 0x000DE514
	private void MakeBall()
	{
		if (this.ball)
		{
			UnityEngine.Object.Destroy(this.ball);
		}
		this.ball = UnityEngine.Object.Instantiate<GameObject>(this.ballPrefab, new Vector3(-2.25f, -4.4f, -1.9f), Quaternion.Euler(300f, 70f, 310f));
		this.ball.GetComponent<Rigidbody>().useGravity = true;
		this.ball.GetComponent<Rigidbody>().AddForce(this.ball.transform.forward * this.force, ForceMode.Impulse);
	}

	// Token: 0x060026C8 RID: 9928 RVA: 0x000E01B4 File Offset: 0x000DE5B4
	private IEnumerator SamplePoints(Transform thisTransform)
	{
		bool running = true;
		while (running)
		{
			this.pathLine.points3.Add(thisTransform.position);
			if (++this.pathIndex == this.maxPoints)
			{
				running = false;
			}
			yield return new WaitForSeconds(0.05f);
			if (this.continuousUpdate)
			{
				this.pathLine.Draw();
			}
		}
		yield break;
	}

	// Token: 0x060026C9 RID: 9929 RVA: 0x000E01D8 File Offset: 0x000DE5D8
	private void OnGUI()
	{
		if (GUI.Button(new Rect(10f, 10f, 100f, 30f), "Reset"))
		{
			this.Reset();
		}
		if (!this.continuousUpdate && GUI.Button(new Rect(10f, 45f, 100f, 30f), "Draw Path"))
		{
			this.pathLine.Draw();
		}
	}

	// Token: 0x060026CA RID: 9930 RVA: 0x000E0254 File Offset: 0x000DE654
	private void Reset()
	{
		base.StopAllCoroutines();
		this.MakeBall();
		this.pathLine.points3.Clear();
		this.pathLine.Draw();
		this.pathIndex = 0;
		base.StartCoroutine(this.SamplePoints(this.ball.transform));
	}

	// Token: 0x040026F6 RID: 9974
	public Texture lineTex;

	// Token: 0x040026F7 RID: 9975
	public Color lineColor = Color.green;

	// Token: 0x040026F8 RID: 9976
	public int maxPoints = 500;

	// Token: 0x040026F9 RID: 9977
	public bool continuousUpdate = true;

	// Token: 0x040026FA RID: 9978
	public GameObject ballPrefab;

	// Token: 0x040026FB RID: 9979
	public float force = 16f;

	// Token: 0x040026FC RID: 9980
	private VectorLine pathLine;

	// Token: 0x040026FD RID: 9981
	private int pathIndex;

	// Token: 0x040026FE RID: 9982
	private GameObject ball;
}
