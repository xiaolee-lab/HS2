using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200066E RID: 1646
public class Orbit : MonoBehaviour
{
	// Token: 0x060026BE RID: 9918 RVA: 0x000DFDBC File Offset: 0x000DE1BC
	private void Start()
	{
		VectorLine vectorLine = new VectorLine("OrbitLine", new List<Vector3>(this.orbitLineResolution), 2f, LineType.Continuous);
		vectorLine.material = this.lineMaterial;
		vectorLine.MakeCircle(Vector3.zero, Vector3.up, Vector3.Distance(base.transform.position, Vector3.zero));
		vectorLine.Draw3DAuto();
	}

	// Token: 0x060026BF RID: 9919 RVA: 0x000DFE1C File Offset: 0x000DE21C
	private void Update()
	{
		base.transform.RotateAround(Vector3.zero, Vector3.up, this.orbitSpeed * Time.deltaTime);
		base.transform.Rotate(Vector3.up * this.rotateSpeed * Time.deltaTime);
	}

	// Token: 0x040026EA RID: 9962
	public float orbitSpeed = -45f;

	// Token: 0x040026EB RID: 9963
	public float rotateSpeed = 200f;

	// Token: 0x040026EC RID: 9964
	public int orbitLineResolution = 150;

	// Token: 0x040026ED RID: 9965
	public Material lineMaterial;
}
