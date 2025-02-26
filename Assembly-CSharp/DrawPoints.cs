using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000672 RID: 1650
public class DrawPoints : MonoBehaviour
{
	// Token: 0x060026CC RID: 9932 RVA: 0x000E03E4 File Offset: 0x000DE7E4
	private void Start()
	{
		int num = this.numberOfDots * this.numberOfRings;
		Vector2[] collection = new Vector2[num];
		Color32[] array = new Color32[num];
		float b = 1f - 0.75f / (float)num;
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = this.dotColor;
			this.dotColor *= b;
		}
		VectorLine vectorLine = new VectorLine("Dots", new List<Vector2>(collection), this.dotSize, LineType.Points);
		vectorLine.SetColors(new List<Color32>(array));
		for (int j = 0; j < this.numberOfRings; j++)
		{
			vectorLine.MakeCircle(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), (float)(Screen.height / (j + 2)), this.numberOfDots, this.numberOfDots * j);
		}
		vectorLine.Draw();
	}

	// Token: 0x040026FF RID: 9983
	public float dotSize = 2f;

	// Token: 0x04002700 RID: 9984
	public int numberOfDots = 100;

	// Token: 0x04002701 RID: 9985
	public int numberOfRings = 8;

	// Token: 0x04002702 RID: 9986
	public Color dotColor = Color.cyan;
}
