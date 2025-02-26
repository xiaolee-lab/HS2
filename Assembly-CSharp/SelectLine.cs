using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000679 RID: 1657
public class SelectLine : MonoBehaviour
{
	// Token: 0x060026E9 RID: 9961 RVA: 0x000E10F0 File Offset: 0x000DF4F0
	private void Start()
	{
		this.lines = new VectorLine[this.numberOfLines];
		this.wasSelected = new bool[this.numberOfLines];
		for (int i = 0; i < this.numberOfLines; i++)
		{
			this.lines[i] = new VectorLine("SelectLine", new List<Vector2>(5), this.lineThickness, LineType.Continuous, Joins.Fill);
			this.SetPoints(i);
		}
	}

	// Token: 0x060026EA RID: 9962 RVA: 0x000E1160 File Offset: 0x000DF560
	private void SetPoints(int i)
	{
		for (int j = 0; j < this.lines[i].points2.Count; j++)
		{
			this.lines[i].points2[j] = new Vector2((float)UnityEngine.Random.Range(0, Screen.width), (float)UnityEngine.Random.Range(0, Screen.height - 20));
		}
		this.lines[i].Draw();
	}

	// Token: 0x060026EB RID: 9963 RVA: 0x000E11D0 File Offset: 0x000DF5D0
	private void Update()
	{
		for (int i = 0; i < this.numberOfLines; i++)
		{
			int num;
			if (this.lines[i].Selected(Input.mousePosition, this.extraThickness, out num))
			{
				if (!this.wasSelected[i])
				{
					this.lines[i].SetColor(Color.green);
					this.wasSelected[i] = true;
				}
				if (Input.GetMouseButtonDown(0))
				{
					this.SetPoints(i);
				}
			}
			else if (this.wasSelected[i])
			{
				this.wasSelected[i] = false;
				this.lines[i].SetColor(Color.white);
			}
		}
	}

	// Token: 0x060026EC RID: 9964 RVA: 0x000E1289 File Offset: 0x000DF689
	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 10f, 800f, 30f), "Click a line to make a new line");
	}

	// Token: 0x04002724 RID: 10020
	public float lineThickness = 10f;

	// Token: 0x04002725 RID: 10021
	public int extraThickness = 2;

	// Token: 0x04002726 RID: 10022
	public int numberOfLines = 2;

	// Token: 0x04002727 RID: 10023
	private VectorLine[] lines;

	// Token: 0x04002728 RID: 10024
	private bool[] wasSelected;
}
