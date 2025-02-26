using System;
using UnityEngine;

// Token: 0x02000A65 RID: 2661
public class SinMovement : MonoBehaviour
{
	// Token: 0x06004ED4 RID: 20180 RVA: 0x001E411B File Offset: 0x001E251B
	private void Start()
	{
		this.startPosition = base.transform.position;
	}

	// Token: 0x06004ED5 RID: 20181 RVA: 0x001E412E File Offset: 0x001E252E
	private void FixedUpdate()
	{
		base.transform.position = Vector3.forward * Mathf.Sin(Time.time * this.speed) * this.magnitude + this.startPosition;
	}

	// Token: 0x040047D0 RID: 18384
	public float speed = 10f;

	// Token: 0x040047D1 RID: 18385
	public float magnitude = 5f;

	// Token: 0x040047D2 RID: 18386
	private Vector3 startPosition;
}
