using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000666 RID: 1638
public class DrawGrid : MonoBehaviour
{
	// Token: 0x060026A0 RID: 9888 RVA: 0x000DEBEB File Offset: 0x000DCFEB
	private void Start()
	{
		this.gridLine = new VectorLine("Grid", new List<Vector2>(), 1f);
		this.gridLine.alignOddWidthToPixels = true;
		this.MakeGrid();
	}

	// Token: 0x060026A1 RID: 9889 RVA: 0x000DEC1C File Offset: 0x000DD01C
	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 10f, 30f, 20f), this.gridPixels.ToString());
		this.gridPixels = (int)GUI.HorizontalSlider(new Rect(40f, 15f, 590f, 20f), (float)this.gridPixels, 5f, 200f);
		if (GUI.changed)
		{
			this.MakeGrid();
		}
	}

	// Token: 0x060026A2 RID: 9890 RVA: 0x000DECA0 File Offset: 0x000DD0A0
	private void MakeGrid()
	{
		int newCount = (Screen.width / this.gridPixels + 1 + (Screen.height / this.gridPixels + 1)) * 2;
		this.gridLine.Resize(newCount);
		int num = 0;
		for (int i = 0; i < Screen.width; i += this.gridPixels)
		{
			this.gridLine.points2[num++] = new Vector2((float)i, 0f);
			this.gridLine.points2[num++] = new Vector2((float)i, (float)(Screen.height - 1));
		}
		for (int j = 0; j < Screen.height; j += this.gridPixels)
		{
			this.gridLine.points2[num++] = new Vector2(0f, (float)j);
			this.gridLine.points2[num++] = new Vector2((float)(Screen.width - 1), (float)j);
		}
		this.gridLine.Draw();
	}

	// Token: 0x040026BE RID: 9918
	public int gridPixels = 50;

	// Token: 0x040026BF RID: 9919
	private VectorLine gridLine;
}
