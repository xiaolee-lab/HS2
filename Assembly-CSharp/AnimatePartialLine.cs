using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000670 RID: 1648
public class AnimatePartialLine : MonoBehaviour
{
	// Token: 0x060026C3 RID: 9923 RVA: 0x000DFEC8 File Offset: 0x000DE2C8
	private void Start()
	{
		this.startIndex = (float)(-(float)this.visibleLineSegments);
		this.endIndex = 0f;
		List<Vector2> points = new List<Vector2>(this.segments + 1);
		this.line = new VectorLine("Spline", points, this.lineTexture, 30f, LineType.Continuous, Joins.Weld);
		int num = Screen.width / 5;
		int num2 = Screen.height / 3;
		this.line.MakeSpline(new Vector2[]
		{
			new Vector2((float)num, (float)num2),
			new Vector2((float)(num * 2), (float)(num2 * 2)),
			new Vector2((float)(num * 3), (float)(num2 * 2)),
			new Vector2((float)(num * 4), (float)num2)
		});
	}

	// Token: 0x060026C4 RID: 9924 RVA: 0x000DFF9C File Offset: 0x000DE39C
	private void Update()
	{
		this.startIndex += Time.deltaTime * this.speed;
		this.endIndex += Time.deltaTime * this.speed;
		if (this.startIndex >= (float)(this.segments + 1))
		{
			this.startIndex = (float)(-(float)this.visibleLineSegments);
			this.endIndex = 0f;
		}
		else if (this.startIndex < (float)(-(float)this.visibleLineSegments))
		{
			this.startIndex = (float)this.segments;
			this.endIndex = (float)(this.segments + this.visibleLineSegments);
		}
		this.line.drawStart = (int)this.startIndex;
		this.line.drawEnd = (int)this.endIndex;
		this.line.Draw();
	}

	// Token: 0x040026EF RID: 9967
	public Texture lineTexture;

	// Token: 0x040026F0 RID: 9968
	public int segments = 60;

	// Token: 0x040026F1 RID: 9969
	public int visibleLineSegments = 20;

	// Token: 0x040026F2 RID: 9970
	public float speed = 60f;

	// Token: 0x040026F3 RID: 9971
	private float startIndex;

	// Token: 0x040026F4 RID: 9972
	private float endIndex;

	// Token: 0x040026F5 RID: 9973
	private VectorLine line;
}
