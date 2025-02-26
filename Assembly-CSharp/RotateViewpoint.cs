using System;
using UnityEngine;

// Token: 0x0200066F RID: 1647
public class RotateViewpoint : MonoBehaviour
{
	// Token: 0x060026C1 RID: 9921 RVA: 0x000DFE82 File Offset: 0x000DE282
	private void Update()
	{
		base.transform.RotateAround(Vector3.zero, Vector3.right, this.rotateSpeed * Time.deltaTime);
	}

	// Token: 0x040026EE RID: 9966
	public float rotateSpeed = 5f;
}
