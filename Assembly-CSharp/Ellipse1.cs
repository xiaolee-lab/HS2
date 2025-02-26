using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000663 RID: 1635
public class Ellipse1 : MonoBehaviour
{
	// Token: 0x0600269A RID: 9882 RVA: 0x000DE828 File Offset: 0x000DCC28
	private void Start()
	{
		List<Vector2> points = new List<Vector2>(this.segments + 1);
		VectorLine vectorLine = new VectorLine("Line", points, this.lineTexture, 3f, LineType.Continuous);
		vectorLine.MakeEllipse(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), this.xRadius, this.yRadius, this.segments, this.pointRotation);
		vectorLine.Draw();
	}

	// Token: 0x040026B0 RID: 9904
	public Texture lineTexture;

	// Token: 0x040026B1 RID: 9905
	public float xRadius = 120f;

	// Token: 0x040026B2 RID: 9906
	public float yRadius = 120f;

	// Token: 0x040026B3 RID: 9907
	public int segments = 60;

	// Token: 0x040026B4 RID: 9908
	public float pointRotation;
}
