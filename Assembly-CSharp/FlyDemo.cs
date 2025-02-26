using System;
using UnityEngine;

// Token: 0x02000439 RID: 1081
public class FlyDemo : MonoBehaviour
{
	// Token: 0x060013CF RID: 5071 RVA: 0x0007AEEA File Offset: 0x000792EA
	private void Start()
	{
		this.t = base.transform;
	}

	// Token: 0x060013D0 RID: 5072 RVA: 0x0007AEF8 File Offset: 0x000792F8
	private void Update()
	{
		this.time += Time.deltaTime;
		float num = Mathf.Cos(this.time / this.Speed);
		this.t.localPosition = new Vector3(0f, 0f, num * this.Height);
	}

	// Token: 0x04001643 RID: 5699
	public float Speed = 1f;

	// Token: 0x04001644 RID: 5700
	public float Height = 1f;

	// Token: 0x04001645 RID: 5701
	private Transform t;

	// Token: 0x04001646 RID: 5702
	private float time;
}
