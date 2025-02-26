using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200067D RID: 1661
public class TextDemo : MonoBehaviour
{
	// Token: 0x060026F4 RID: 9972 RVA: 0x000E1870 File Offset: 0x000DFC70
	private void Start()
	{
		this.textLine = new VectorLine("Text", new List<Vector2>(), 1f);
		this.textLine.color = Color.yellow;
		this.textLine.drawTransform = base.transform;
		this.textLine.MakeText(this.text, new Vector2((float)(Screen.width / 2 - this.text.Length * this.textSize / 2), (float)(Screen.height / 2 + this.textSize / 2)), (float)this.textSize);
	}

	// Token: 0x060026F5 RID: 9973 RVA: 0x000E1910 File Offset: 0x000DFD10
	private void Update()
	{
		base.transform.RotateAround(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), Vector3.forward, Time.deltaTime * 45f);
		Vector3 localScale = base.transform.localScale;
		localScale.x = 1f + Mathf.Sin(Time.time * 3f) * 0.3f;
		localScale.y = 1f + Mathf.Cos(Time.time * 3f) * 0.3f;
		base.transform.localScale = localScale;
		this.textLine.Draw();
	}

	// Token: 0x04002734 RID: 10036
	public string text = "Vectrosity!";

	// Token: 0x04002735 RID: 10037
	public int textSize = 40;

	// Token: 0x04002736 RID: 10038
	private VectorLine textLine;
}
