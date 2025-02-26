using System;
using UnityEngine;

// Token: 0x020003FC RID: 1020
public class DestroyOnTrigger : MonoBehaviour
{
	// Token: 0x0600122D RID: 4653 RVA: 0x00071CEA File Offset: 0x000700EA
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == this.m_Tag)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040014B3 RID: 5299
	public string m_Tag = "Player";
}
