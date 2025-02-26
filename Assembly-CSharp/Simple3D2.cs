using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200065B RID: 1627
public class Simple3D2 : MonoBehaviour
{
	// Token: 0x0600267A RID: 9850 RVA: 0x000DCCA8 File Offset: 0x000DB0A8
	private void Start()
	{
		List<Vector3> points = VectorLine.BytesToVector3List(this.vectorCube.bytes);
		VectorLine line = new VectorLine(base.gameObject.name, points, 2f);
		VectorManager.ObjectSetup(base.gameObject, line, Visibility.Dynamic, Brightness.None);
	}

	// Token: 0x04002670 RID: 9840
	public TextAsset vectorCube;
}
