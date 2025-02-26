using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200065F RID: 1631
public class SimpleCurve : MonoBehaviour
{
	// Token: 0x0600268A RID: 9866 RVA: 0x000DDDC0 File Offset: 0x000DC1C0
	private void Start()
	{
		if (this.curvePoints.Length != 4)
		{
			return;
		}
		List<Vector2> points = new List<Vector2>(this.segments + 1);
		VectorLine vectorLine = new VectorLine("Curve", points, 2f, LineType.Continuous, Joins.Weld);
		vectorLine.MakeCurve(this.curvePoints, this.segments);
		vectorLine.Draw();
	}

	// Token: 0x04002687 RID: 9863
	public Vector2[] curvePoints;

	// Token: 0x04002688 RID: 9864
	public int segments = 50;
}
