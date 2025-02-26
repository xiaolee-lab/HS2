using System;
using UnityEngine;

// Token: 0x02000967 RID: 2407
public class SetProjectioinQueue : MonoBehaviour
{
	// Token: 0x060042CB RID: 17099 RVA: 0x001A4F1C File Offset: 0x001A331C
	private void Awake()
	{
		Material material = base.GetComponent<Projector>().material;
		material.renderQueue = this._queue;
	}

	// Token: 0x04003FB4 RID: 16308
	[SerializeField]
	private int _queue = 3000;
}
