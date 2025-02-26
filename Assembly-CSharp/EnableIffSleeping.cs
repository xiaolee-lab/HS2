using System;
using UnityEngine;

// Token: 0x020003FE RID: 1022
public class EnableIffSleeping : MonoBehaviour
{
	// Token: 0x06001231 RID: 4657 RVA: 0x00071EEE File Offset: 0x000702EE
	private void Start()
	{
		this.m_Rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06001232 RID: 4658 RVA: 0x00071EFC File Offset: 0x000702FC
	private void Update()
	{
		if (this.m_Rigidbody == null || this.m_Behaviour == null)
		{
			return;
		}
		if (this.m_Rigidbody.IsSleeping() && !this.m_Behaviour.enabled)
		{
			this.m_Behaviour.enabled = true;
		}
		if (!this.m_Rigidbody.IsSleeping() && this.m_Behaviour.enabled)
		{
			this.m_Behaviour.enabled = false;
		}
	}

	// Token: 0x040014B8 RID: 5304
	public Behaviour m_Behaviour;

	// Token: 0x040014B9 RID: 5305
	private Rigidbody m_Rigidbody;
}
