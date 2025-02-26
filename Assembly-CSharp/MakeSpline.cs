using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200067A RID: 1658
public class MakeSpline : MonoBehaviour
{
	// Token: 0x060026EE RID: 9966 RVA: 0x000E12C8 File Offset: 0x000DF6C8
	private void Start()
	{
		List<Vector3> list = new List<Vector3>();
		int num = 1;
		GameObject gameObject = GameObject.Find("Sphere" + num++);
		while (gameObject != null)
		{
			list.Add(gameObject.transform.position);
			gameObject = GameObject.Find("Sphere" + num++);
		}
		if (this.usePoints)
		{
			VectorLine vectorLine = new VectorLine("Spline", new List<Vector3>(this.segments + 1), 2f, LineType.Points);
			vectorLine.MakeSpline(list.ToArray(), this.segments, this.loop);
			vectorLine.Draw();
		}
		else
		{
			VectorLine vectorLine2 = new VectorLine("Spline", new List<Vector3>(this.segments + 1), 2f, LineType.Continuous);
			vectorLine2.MakeSpline(list.ToArray(), this.segments, this.loop);
			vectorLine2.Draw3D();
		}
	}

	// Token: 0x04002729 RID: 10025
	public int segments = 250;

	// Token: 0x0400272A RID: 10026
	public bool loop = true;

	// Token: 0x0400272B RID: 10027
	public bool usePoints;
}
