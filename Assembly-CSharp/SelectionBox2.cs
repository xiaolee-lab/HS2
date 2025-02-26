using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000678 RID: 1656
public class SelectionBox2 : MonoBehaviour
{
	// Token: 0x060026E5 RID: 9957 RVA: 0x000E0FE4 File Offset: 0x000DF3E4
	private void Start()
	{
		this.selectionLine = new VectorLine("Selection", new List<Vector2>(5), this.lineTexture, 4f, LineType.Continuous);
		this.selectionLine.textureScale = this.textureScale;
		this.selectionLine.alignOddWidthToPixels = true;
	}

	// Token: 0x060026E6 RID: 9958 RVA: 0x000E1030 File Offset: 0x000DF430
	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 10f, 300f, 25f), "Click & drag to make a selection box");
	}

	// Token: 0x060026E7 RID: 9959 RVA: 0x000E1058 File Offset: 0x000DF458
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.originalPos = Input.mousePosition;
		}
		if (Input.GetMouseButton(0))
		{
			this.selectionLine.MakeRect(this.originalPos, Input.mousePosition);
			this.selectionLine.Draw();
		}
		this.selectionLine.textureOffset = -Time.time * 2f % 1f;
	}

	// Token: 0x04002720 RID: 10016
	public Texture lineTexture;

	// Token: 0x04002721 RID: 10017
	public float textureScale = 4f;

	// Token: 0x04002722 RID: 10018
	private VectorLine selectionLine;

	// Token: 0x04002723 RID: 10019
	private Vector2 originalPos;
}
