using System;
using UnityEngine;

// Token: 0x020004A0 RID: 1184
public class CoroutineActivity
{
	// Token: 0x060015E4 RID: 5604 RVA: 0x00087181 File Offset: 0x00085581
	public CoroutineActivity(int id)
	{
		this.seqID = id;
		this.timestamp = Time.realtimeSinceStartup;
		this.typeName = base.GetType().Name.Substring("Coroutine".Length);
	}

	// Token: 0x040018C5 RID: 6341
	public int seqID;

	// Token: 0x040018C6 RID: 6342
	public float timestamp;

	// Token: 0x040018C7 RID: 6343
	public int curFrame;

	// Token: 0x040018C8 RID: 6344
	public string typeName;
}
