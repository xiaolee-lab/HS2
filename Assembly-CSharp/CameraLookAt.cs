using System;
using UnityEngine;

// Token: 0x020005A5 RID: 1445
public class CameraLookAt : MonoBehaviour
{
	// Token: 0x0600217E RID: 8574 RVA: 0x000B900E File Offset: 0x000B740E
	private void Start()
	{
	}

	// Token: 0x0600217F RID: 8575 RVA: 0x000B9010 File Offset: 0x000B7410
	private void Update()
	{
		float x = this.radius * Mathf.Cos(Time.time * this.vel);
		float y = this.radius * Mathf.Sin(Time.time * this.vel);
		base.transform.position = new Vector3(x, y, -10f);
		if (base.transform && this.target != null)
		{
			base.transform.LookAt(this.target.transform);
		}
	}

	// Token: 0x04002108 RID: 8456
	public GameObject target;

	// Token: 0x04002109 RID: 8457
	public float radius = 10f;

	// Token: 0x0400210A RID: 8458
	public float vel = 5f;
}
