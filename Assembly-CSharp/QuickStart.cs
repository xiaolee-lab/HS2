using System;
using Exploder.Utils;
using UnityEngine;

// Token: 0x02000394 RID: 916
public class QuickStart : MonoBehaviour
{
	// Token: 0x06001032 RID: 4146 RVA: 0x0005AB0A File Offset: 0x00058F0A
	private void Start()
	{
		ExploderSingleton.Instance.ExplodeObject(base.gameObject);
	}
}
