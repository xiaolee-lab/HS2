using System;
using UnityEngine;

// Token: 0x02000A5C RID: 2652
public class HitPSControl : MonoBehaviour
{
	// Token: 0x06004EAC RID: 20140 RVA: 0x001E2E94 File Offset: 0x001E1294
	private void Start()
	{
		ParticleSystem component = base.GetComponent<ParticleSystem>();
		UnityEngine.Object.Destroy(base.gameObject, component.main.duration);
	}

	// Token: 0x06004EAD RID: 20141 RVA: 0x001E2EC1 File Offset: 0x001E12C1
	private void Update()
	{
	}
}
