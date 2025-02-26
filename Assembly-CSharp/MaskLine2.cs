using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200066C RID: 1644
public class MaskLine2 : MonoBehaviour
{
	// Token: 0x060026B8 RID: 9912 RVA: 0x000DFAD0 File Offset: 0x000DDED0
	private void Start()
	{
		this.spikeLine = new VectorLine("SpikeLine", new List<Vector3>(this.numberOfPoints), 2f, LineType.Continuous);
		float num = this.lineHeight / 2f;
		for (int i = 0; i < this.numberOfPoints; i++)
		{
			this.spikeLine.points3[i] = new Vector2(UnityEngine.Random.Range(-this.lineWidth / 2f, this.lineWidth / 2f), num);
			num -= this.lineHeight / (float)this.numberOfPoints;
		}
		this.spikeLine.color = this.lineColor;
		this.spikeLine.drawTransform = base.transform;
		this.spikeLine.SetMask(this.mask);
		this.startPos = base.transform.position;
	}

	// Token: 0x060026B9 RID: 9913 RVA: 0x000DFBB8 File Offset: 0x000DDFB8
	private void Update()
	{
		this.t = Mathf.Repeat(this.t + Time.deltaTime, 360f);
		base.transform.position = new Vector2(this.startPos.x, this.startPos.y + Mathf.Cos(this.t) * 4f);
		this.spikeLine.Draw();
	}

	// Token: 0x040026E0 RID: 9952
	public int numberOfPoints = 100;

	// Token: 0x040026E1 RID: 9953
	public Color lineColor = Color.yellow;

	// Token: 0x040026E2 RID: 9954
	public GameObject mask;

	// Token: 0x040026E3 RID: 9955
	public float lineWidth = 9f;

	// Token: 0x040026E4 RID: 9956
	public float lineHeight = 17f;

	// Token: 0x040026E5 RID: 9957
	private VectorLine spikeLine;

	// Token: 0x040026E6 RID: 9958
	private float t;

	// Token: 0x040026E7 RID: 9959
	private Vector3 startPos;
}
