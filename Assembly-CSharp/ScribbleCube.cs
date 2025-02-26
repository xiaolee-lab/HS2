using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000676 RID: 1654
public class ScribbleCube : MonoBehaviour
{
	// Token: 0x060026D9 RID: 9945 RVA: 0x000E0A9C File Offset: 0x000DEE9C
	private void Start()
	{
		this.line = new VectorLine("Line", new List<Vector3>(this.numberOfPoints), this.lineTexture, (float)this.lineWidth, LineType.Continuous);
		this.line.material = this.lineMaterial;
		this.line.drawTransform = base.transform;
		this.LineSetup(false);
	}

	// Token: 0x060026DA RID: 9946 RVA: 0x000E0AFC File Offset: 0x000DEEFC
	private void LineSetup(bool resize)
	{
		if (resize)
		{
			this.lineColors = null;
			this.line.Resize(this.numberOfPoints);
		}
		for (int i = 0; i < this.line.points3.Count; i++)
		{
			this.line.points3[i] = new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f));
		}
		this.SetLineColors();
	}

	// Token: 0x060026DB RID: 9947 RVA: 0x000E0B94 File Offset: 0x000DEF94
	private void SetLineColors()
	{
		if (this.lineColors == null)
		{
			this.lineColors = new List<Color32>(new Color32[this.numberOfPoints - 1]);
		}
		for (int i = 0; i < this.lineColors.Count; i++)
		{
			this.lineColors[i] = Color.Lerp(this.color1, this.color2, (float)i / (float)this.lineColors.Count);
		}
		this.line.SetColors(this.lineColors);
	}

	// Token: 0x060026DC RID: 9948 RVA: 0x000E0C22 File Offset: 0x000DF022
	private void LateUpdate()
	{
		this.line.Draw();
	}

	// Token: 0x060026DD RID: 9949 RVA: 0x000E0C30 File Offset: 0x000DF030
	private void OnGUI()
	{
		GUI.Label(new Rect(20f, 10f, 250f, 30f), "Zoom with scrollwheel or arrow keys");
		if (GUI.Button(new Rect(20f, 50f, 100f, 30f), "Change colors"))
		{
			int num = UnityEngine.Random.Range(0, 3);
			int num2;
			do
			{
				num2 = UnityEngine.Random.Range(0, 3);
			}
			while (num2 == num);
			this.color1 = this.RandomColor(this.color1, num);
			this.color2 = this.RandomColor(this.color2, num2);
			this.SetLineColors();
		}
		GUI.Label(new Rect(20f, 100f, 150f, 30f), "Number of points: " + this.numberOfPoints);
		this.numberOfPoints = (int)GUI.HorizontalSlider(new Rect(20f, 130f, 120f, 30f), (float)this.numberOfPoints, 50f, 1000f);
		if (GUI.Button(new Rect(160f, 120f, 40f, 30f), "Set"))
		{
			this.LineSetup(true);
		}
	}

	// Token: 0x060026DE RID: 9950 RVA: 0x000E0D68 File Offset: 0x000DF168
	private Color RandomColor(Color color, int component)
	{
		for (int i = 0; i < 3; i++)
		{
			if (i == component)
			{
				color[i] = UnityEngine.Random.value * 0.25f;
			}
			else
			{
				color[i] = UnityEngine.Random.value * 0.5f + 0.5f;
			}
		}
		return color;
	}

	// Token: 0x04002715 RID: 10005
	public Texture lineTexture;

	// Token: 0x04002716 RID: 10006
	public Material lineMaterial;

	// Token: 0x04002717 RID: 10007
	public int lineWidth = 14;

	// Token: 0x04002718 RID: 10008
	private Color color1 = Color.green;

	// Token: 0x04002719 RID: 10009
	private Color color2 = Color.blue;

	// Token: 0x0400271A RID: 10010
	private VectorLine line;

	// Token: 0x0400271B RID: 10011
	private List<Color32> lineColors;

	// Token: 0x0400271C RID: 10012
	private int numberOfPoints = 350;
}
