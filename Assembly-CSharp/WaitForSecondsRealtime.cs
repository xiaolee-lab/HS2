using System;
using UnityEngine;

// Token: 0x020011AC RID: 4524
public sealed class WaitForSecondsRealtime : CustomYieldInstruction
{
	// Token: 0x060094A9 RID: 38057 RVA: 0x003D4F1B File Offset: 0x003D331B
	public WaitForSecondsRealtime(float time)
	{
		this.waitTime = Time.realtimeSinceStartup + time;
	}

	// Token: 0x17001F8C RID: 8076
	// (get) Token: 0x060094AA RID: 38058 RVA: 0x003D4F30 File Offset: 0x003D3330
	public override bool keepWaiting
	{
		get
		{
			return Time.realtimeSinceStartup < this.waitTime;
		}
	}

	// Token: 0x04007793 RID: 30611
	private float waitTime;
}
