using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000664 RID: 1636
public class Ellipse2 : MonoBehaviour
{
	// Token: 0x0600269C RID: 9884 RVA: 0x000DE8B4 File Offset: 0x000DCCB4
	private void Start()
	{
		List<Vector2> points = new List<Vector2>(this.segments * 2 * this.numberOfEllipses);
		VectorLine vectorLine = new VectorLine("Line", points, this.lineTexture, 3f);
		for (int i = 0; i < this.numberOfEllipses; i++)
		{
			Vector2 v = new Vector2((float)UnityEngine.Random.Range(0, Screen.width), (float)UnityEngine.Random.Range(0, Screen.height));
			vectorLine.MakeEllipse(v, (float)UnityEngine.Random.Range(10, Screen.width / 2), (float)UnityEngine.Random.Range(10, Screen.height / 2), this.segments, i * (this.segments * 2));
		}
		vectorLine.Draw();
	}

	// Token: 0x040026B5 RID: 9909
	public Texture lineTexture;

	// Token: 0x040026B6 RID: 9910
	public int segments = 60;

	// Token: 0x040026B7 RID: 9911
	public int numberOfEllipses = 10;
}
