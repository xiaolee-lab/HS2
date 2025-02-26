using System;
using UnityEngine;

// Token: 0x02000453 RID: 1107
public class DeadTime : MonoBehaviour
{
	// Token: 0x06001445 RID: 5189 RVA: 0x0007EF54 File Offset: 0x0007D354
	private void Awake()
	{
		UnityEngine.Object.Destroy(this.destroyRoot ? base.transform.root.gameObject : base.gameObject, this.deadTime);
	}

	// Token: 0x04001716 RID: 5910
	public float deadTime = 1.5f;

	// Token: 0x04001717 RID: 5911
	public bool destroyRoot;
}
