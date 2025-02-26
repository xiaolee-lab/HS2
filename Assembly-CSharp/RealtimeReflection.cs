using System;
using UnityEngine;

// Token: 0x0200043F RID: 1087
public class RealtimeReflection : MonoBehaviour
{
	// Token: 0x060013E8 RID: 5096 RVA: 0x0007C5DB File Offset: 0x0007A9DB
	private void Awake()
	{
		this.probe = base.GetComponent<ReflectionProbe>();
		this.camT = Camera.main.transform;
	}

	// Token: 0x060013E9 RID: 5097 RVA: 0x0007C5FC File Offset: 0x0007A9FC
	private void Update()
	{
		Vector3 position = this.camT.position;
		this.probe.transform.position = new Vector3(position.x, position.y * -1f, position.z);
		this.probe.RenderProbe();
	}

	// Token: 0x0400167D RID: 5757
	private ReflectionProbe probe;

	// Token: 0x0400167E RID: 5758
	private Transform camT;
}
