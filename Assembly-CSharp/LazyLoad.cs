using System;
using UnityEngine;

// Token: 0x02000443 RID: 1091
public class LazyLoad : MonoBehaviour
{
	// Token: 0x060013FB RID: 5115 RVA: 0x0007CC89 File Offset: 0x0007B089
	private void Awake()
	{
		this.GO.SetActive(false);
	}

	// Token: 0x060013FC RID: 5116 RVA: 0x0007CC97 File Offset: 0x0007B097
	private void LazyEnable()
	{
		this.GO.SetActive(true);
	}

	// Token: 0x060013FD RID: 5117 RVA: 0x0007CCA5 File Offset: 0x0007B0A5
	private void OnEnable()
	{
		base.Invoke("LazyEnable", this.TimeDelay);
	}

	// Token: 0x060013FE RID: 5118 RVA: 0x0007CCB8 File Offset: 0x0007B0B8
	private void OnDisable()
	{
		base.CancelInvoke("LazyEnable");
		this.GO.SetActive(false);
	}

	// Token: 0x04001691 RID: 5777
	public GameObject GO;

	// Token: 0x04001692 RID: 5778
	public float TimeDelay = 0.3f;
}
