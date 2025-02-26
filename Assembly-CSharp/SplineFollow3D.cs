using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200067C RID: 1660
public class SplineFollow3D : MonoBehaviour
{
	// Token: 0x060026F2 RID: 9970 RVA: 0x000E1650 File Offset: 0x000DFA50
	private IEnumerator Start()
	{
		List<Vector3> splinePoints = new List<Vector3>();
		int i = 1;
		object arg = "Sphere";
		int num;
		i = (num = i) + 1;
		GameObject obj = GameObject.Find(arg + num);
		while (obj != null)
		{
			splinePoints.Add(obj.transform.position);
			object arg2 = "Sphere";
			i = (num = i) + 1;
			obj = GameObject.Find(arg2 + num);
		}
		VectorLine line = new VectorLine("Spline", new List<Vector3>(this.segments + 1), 2f, LineType.Continuous);
		line.MakeSpline(splinePoints.ToArray(), this.segments, this.doLoop);
		line.Draw3D();
		do
		{
			for (float dist = 0f; dist < 1f; dist += Time.deltaTime * this.speed)
			{
				this.cube.position = line.GetPoint3D01(dist);
				yield return null;
			}
		}
		while (this.doLoop);
		yield break;
	}

	// Token: 0x04002730 RID: 10032
	public int segments = 250;

	// Token: 0x04002731 RID: 10033
	public bool doLoop = true;

	// Token: 0x04002732 RID: 10034
	public Transform cube;

	// Token: 0x04002733 RID: 10035
	public float speed = 0.05f;
}
