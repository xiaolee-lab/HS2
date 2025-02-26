using System;
using UnityEngine;

// Token: 0x0200045D RID: 1117
public class FadeInOutParticles : MonoBehaviour
{
	// Token: 0x06001475 RID: 5237 RVA: 0x000802A4 File Offset: 0x0007E6A4
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

	// Token: 0x06001476 RID: 5238 RVA: 0x000802ED File Offset: 0x0007E6ED
	private void Start()
	{
		this.GetEffectSettingsComponent(base.transform);
		this.particles = this.effectSettings.GetComponentsInChildren<ParticleSystem>();
		this.oldVisibleStat = this.effectSettings.IsVisible;
	}

	// Token: 0x06001477 RID: 5239 RVA: 0x00080320 File Offset: 0x0007E720
	private void Update()
	{
		if (this.effectSettings.IsVisible != this.oldVisibleStat)
		{
			if (this.effectSettings.IsVisible)
			{
				foreach (ParticleSystem particleSystem in this.particles)
				{
					if (this.effectSettings.IsVisible)
					{
						particleSystem.Play();
					}
					particleSystem.emission.enabled = true;
				}
			}
			else
			{
				foreach (ParticleSystem particleSystem2 in this.particles)
				{
					if (!this.effectSettings.IsVisible)
					{
						particleSystem2.Stop();
					}
					particleSystem2.emission.enabled = false;
				}
			}
		}
		this.oldVisibleStat = this.effectSettings.IsVisible;
	}

	// Token: 0x0400176D RID: 5997
	private EffectSettings effectSettings;

	// Token: 0x0400176E RID: 5998
	private ParticleSystem[] particles;

	// Token: 0x0400176F RID: 5999
	private bool oldVisibleStat;
}
