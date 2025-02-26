using System;
using UnityEngine;

// Token: 0x0200066A RID: 1642
public class RotateAroundY : MonoBehaviour
{
	// Token: 0x060026B3 RID: 9907 RVA: 0x000DF8D3 File Offset: 0x000DDCD3
	private void Update()
	{
		base.transform.Rotate(Vector3.up * Time.deltaTime * this.rotateSpeed);
	}

	// Token: 0x040026D8 RID: 9944
	public float rotateSpeed = 10f;
}
