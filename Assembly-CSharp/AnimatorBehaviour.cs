using System;
using UnityEngine;

// Token: 0x02000441 RID: 1089
public class AnimatorBehaviour : MonoBehaviour
{
	// Token: 0x060013F2 RID: 5106 RVA: 0x0007CACC File Offset: 0x0007AECC
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

	// Token: 0x060013F3 RID: 5107 RVA: 0x0007CB18 File Offset: 0x0007AF18
	private void Start()
	{
		this.oldSpeed = this.anim.speed;
		this.GetEffectSettingsComponent(base.transform);
		if (this.effectSettings != null)
		{
			this.effectSettings.CollisionEnter += this.prefabSettings_CollisionEnter;
		}
		this.isInitialized = true;
	}

	// Token: 0x060013F4 RID: 5108 RVA: 0x0007CB71 File Offset: 0x0007AF71
	private void OnEnable()
	{
		if (this.isInitialized)
		{
			this.anim.speed = this.oldSpeed;
		}
	}

	// Token: 0x060013F5 RID: 5109 RVA: 0x0007CB8F File Offset: 0x0007AF8F
	private void prefabSettings_CollisionEnter(object sender, CollisionInfo e)
	{
		this.anim.speed = 0f;
	}

	// Token: 0x060013F6 RID: 5110 RVA: 0x0007CBA1 File Offset: 0x0007AFA1
	private void Update()
	{
	}

	// Token: 0x0400168D RID: 5773
	public Animator anim;

	// Token: 0x0400168E RID: 5774
	private EffectSettings effectSettings;

	// Token: 0x0400168F RID: 5775
	private bool isInitialized;

	// Token: 0x04001690 RID: 5776
	private float oldSpeed;
}
