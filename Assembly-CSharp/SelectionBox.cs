using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000677 RID: 1655
public class SelectionBox : MonoBehaviour
{
	// Token: 0x060026E0 RID: 9952 RVA: 0x000E0DC8 File Offset: 0x000DF1C8
	private void Start()
	{
		this.lineColors = new List<Color32>(new Color32[4]);
		this.selectionLine = new VectorLine("Selection", new List<Vector2>(5), 3f, LineType.Continuous);
		this.selectionLine.capLength = 1.5f;
	}

	// Token: 0x060026E1 RID: 9953 RVA: 0x000E0E07 File Offset: 0x000DF207
	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 10f, 300f, 25f), "Click & drag to make a selection box");
	}

	// Token: 0x060026E2 RID: 9954 RVA: 0x000E0E2C File Offset: 0x000DF22C
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			base.StopCoroutine("CycleColor");
			this.selectionLine.SetColor(Color.white);
			this.originalPos = Input.mousePosition;
		}
		if (Input.GetMouseButton(0))
		{
			this.selectionLine.MakeRect(this.originalPos, Input.mousePosition);
			this.selectionLine.Draw();
		}
		if (Input.GetMouseButtonUp(0))
		{
			base.StartCoroutine("CycleColor");
		}
	}

	// Token: 0x060026E3 RID: 9955 RVA: 0x000E0EBC File Offset: 0x000DF2BC
	private IEnumerator CycleColor()
	{
		for (;;)
		{
			for (int i = 0; i < 4; i++)
			{
				this.lineColors[i] = Color.Lerp(Color.yellow, Color.red, Mathf.PingPong((Time.time + (float)i * 0.25f) * 3f, 1f));
			}
			this.selectionLine.SetColors(this.lineColors);
			yield return null;
		}
		yield break;
	}

	// Token: 0x0400271D RID: 10013
	private VectorLine selectionLine;

	// Token: 0x0400271E RID: 10014
	private Vector2 originalPos;

	// Token: 0x0400271F RID: 10015
	private List<Color32> lineColors;
}
