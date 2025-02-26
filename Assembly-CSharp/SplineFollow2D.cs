using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200067B RID: 1659
public class SplineFollow2D : MonoBehaviour
{
	// Token: 0x060026F0 RID: 9968 RVA: 0x000E13E4 File Offset: 0x000DF7E4
	private IEnumerator Start()
	{
		List<Vector2> splinePoints = new List<Vector2>();
		int i = 1;
		object arg = "Sphere";
		int num;
		i = (num = i) + 1;
		GameObject obj = GameObject.Find(arg + num);
		while (obj != null)
		{
			splinePoints.Add(Camera.main.WorldToScreenPoint(obj.transform.position));
			object arg2 = "Sphere";
			i = (num = i) + 1;
			obj = GameObject.Find(arg2 + num);
		}
		VectorLine line = new VectorLine("Spline", new List<Vector2>(this.segments + 1), 2f, LineType.Continuous);
		line.MakeSpline(splinePoints.ToArray(), this.segments, this.loop);
		line.Draw();
		do
		{
			for (float dist = 0f; dist < 1f; dist += Time.deltaTime * this.speed)
			{
				Vector2 splinePoint = line.GetPoint01(dist);
				this.cube.position = Camera.main.ScreenToWorldPoint(new Vector3(splinePoint.x, splinePoint.y, 10f));
				yield return null;
			}
		}
		while (this.loop);
		yield break;
	}

	// Token: 0x0400272C RID: 10028
	public int segments = 250;

	// Token: 0x0400272D RID: 10029
	public bool loop = true;

	// Token: 0x0400272E RID: 10030
	public Transform cube;

	// Token: 0x0400272F RID: 10031
	public float speed = 0.05f;
}
