using System;
using UnityEngine;

// Token: 0x0200044E RID: 1102
public class OnRigidbodySendCollision : MonoBehaviour
{
	// Token: 0x06001432 RID: 5170 RVA: 0x0007E6A0 File Offset: 0x0007CAA0
	private void GetEffectSettingsComponent(Transform tr)
	{
		Transform parent = tr.parent;
		if (parent != null)
		{
			this.effectSettings = parent.GetComponentInChildren<EffectSettings>();
			if (this.effectSettings == null)
			{
				this.GetEffectSettingsComponent(parent.transform);
			}
		}
	}

	// Token: 0x06001433 RID: 5171 RVA: 0x0007E6E9 File Offset: 0x0007CAE9
	private void Start()
	{
		this.GetEffectSettingsComponent(base.transform);
	}

	// Token: 0x06001434 RID: 5172 RVA: 0x0007E6F7 File Offset: 0x0007CAF7
	private void OnCollisionEnter(Collision collision)
	{
		this.effectSettings.OnCollisionHandler(new CollisionInfo());
	}

	// Token: 0x040016F6 RID: 5878
	private EffectSettings effectSettings;
}
