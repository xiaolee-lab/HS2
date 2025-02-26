using System;
using UnityEngine;

// Token: 0x02000452 RID: 1106
public class CollisionActiveBehaviour : MonoBehaviour
{
	// Token: 0x06001441 RID: 5185 RVA: 0x0007EE44 File Offset: 0x0007D244
	private void Start()
	{
		this.GetEffectSettingsComponent(base.transform);
		if (this.IsReverse)
		{
			this.effectSettings.RegistreInactiveElement(base.gameObject, this.TimeDelay);
			base.gameObject.SetActive(false);
		}
		else
		{
			this.effectSettings.RegistreActiveElement(base.gameObject, this.TimeDelay);
		}
		if (this.IsLookAt)
		{
			this.effectSettings.CollisionEnter += this.effectSettings_CollisionEnter;
		}
	}

	// Token: 0x06001442 RID: 5186 RVA: 0x0007EEC9 File Offset: 0x0007D2C9
	private void effectSettings_CollisionEnter(object sender, CollisionInfo e)
	{
		base.transform.LookAt(this.effectSettings.transform.position + e.Hit.normal);
	}

	// Token: 0x06001443 RID: 5187 RVA: 0x0007EEF8 File Offset: 0x0007D2F8
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

	// Token: 0x04001712 RID: 5906
	public bool IsReverse;

	// Token: 0x04001713 RID: 5907
	public float TimeDelay;

	// Token: 0x04001714 RID: 5908
	public bool IsLookAt;

	// Token: 0x04001715 RID: 5909
	private EffectSettings effectSettings;
}
