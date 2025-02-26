using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200066B RID: 1643
public class MaskLine1 : MonoBehaviour
{
	// Token: 0x060026B5 RID: 9909 RVA: 0x000DF920 File Offset: 0x000DDD20
	private void Start()
	{
		this.rectLine = new VectorLine("Rects", new List<Vector3>(this.numberOfRects * 8), 2f);
		int num = 0;
		for (int i = 0; i < this.numberOfRects; i++)
		{
			this.rectLine.MakeRect(new Rect(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(0.25f, 3f), UnityEngine.Random.Range(0.25f, 2f)), num);
			num += 8;
		}
		this.rectLine.color = this.lineColor;
		this.rectLine.capLength = 1f;
		this.rectLine.drawTransform = base.transform;
		this.rectLine.SetMask(this.mask);
		this.startPos = base.transform.position;
	}

	// Token: 0x060026B6 RID: 9910 RVA: 0x000DFA14 File Offset: 0x000DDE14
	private void Update()
	{
		this.t = Mathf.Repeat(this.t + Time.deltaTime * this.moveSpeed, 360f);
		base.transform.position = new Vector2(this.startPos.x + Mathf.Sin(this.t) * 1.5f, this.startPos.y + Mathf.Cos(this.t) * 1.5f);
		this.rectLine.Draw();
	}

	// Token: 0x040026D9 RID: 9945
	public int numberOfRects = 30;

	// Token: 0x040026DA RID: 9946
	public Color lineColor = Color.green;

	// Token: 0x040026DB RID: 9947
	public GameObject mask;

	// Token: 0x040026DC RID: 9948
	public float moveSpeed = 2f;

	// Token: 0x040026DD RID: 9949
	private VectorLine rectLine;

	// Token: 0x040026DE RID: 9950
	private float t;

	// Token: 0x040026DF RID: 9951
	private Vector3 startPos;
}
