using System;
using UnityEngine;

// Token: 0x02000464 RID: 1124
public class OnStartSendCollision : MonoBehaviour
{
	// Token: 0x060014AA RID: 5290 RVA: 0x0008187C File Offset: 0x0007FC7C
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

	// Token: 0x060014AB RID: 5291 RVA: 0x000818C5 File Offset: 0x0007FCC5
	private void Start()
	{
		this.GetEffectSettingsComponent(base.transform);
		this.effectSettings.OnCollisionHandler(new CollisionInfo());
		this.isInitialized = true;
	}

	// Token: 0x060014AC RID: 5292 RVA: 0x000818EA File Offset: 0x0007FCEA
	private void OnEnable()
	{
		if (this.isInitialized)
		{
			this.effectSettings.OnCollisionHandler(new CollisionInfo());
		}
	}

	// Token: 0x040017C9 RID: 6089
	private EffectSettings effectSettings;

	// Token: 0x040017CA RID: 6090
	private bool isInitialized;
}
