using System;
using UnityEngine;

// Token: 0x02000425 RID: 1061
public class ME_RealtimeReflection : MonoBehaviour
{
	// Token: 0x06001359 RID: 4953 RVA: 0x0007713E File Offset: 0x0007553E
	private void Awake()
	{
		this.probe = base.GetComponent<ReflectionProbe>();
		this.camT = Camera.main.transform;
	}

	// Token: 0x0600135A RID: 4954 RVA: 0x0007715C File Offset: 0x0007555C
	private void Update()
	{
		Vector3 position = this.camT.position;
		this.probe.transform.position = new Vector3(position.x, position.y * -1f, position.z);
		if (this.CanUpdate)
		{
			this.probe.RenderProbe();
		}
	}

	// Token: 0x040015A4 RID: 5540
	private ReflectionProbe probe;

	// Token: 0x040015A5 RID: 5541
	private Transform camT;

	// Token: 0x040015A6 RID: 5542
	public bool CanUpdate = true;
}
