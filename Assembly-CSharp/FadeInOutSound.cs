using System;
using UnityEngine;

// Token: 0x02000461 RID: 1121
public class FadeInOutSound : MonoBehaviour
{
	// Token: 0x0600149A RID: 5274 RVA: 0x00081338 File Offset: 0x0007F738
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

	// Token: 0x0600149B RID: 5275 RVA: 0x00081381 File Offset: 0x0007F781
	private void Start()
	{
		this.GetEffectSettingsComponent(base.transform);
		if (this.effectSettings != null)
		{
			this.effectSettings.CollisionEnter += this.prefabSettings_CollisionEnter;
		}
		this.InitSource();
	}

	// Token: 0x0600149C RID: 5276 RVA: 0x000813C0 File Offset: 0x0007F7C0
	private void InitSource()
	{
		if (this.isInitialized)
		{
			return;
		}
		this.audioSource = base.GetComponent<AudioSource>();
		if (this.audioSource == null)
		{
			return;
		}
		this.isStartDelay = (this.StartDelay > 0.001f);
		this.isIn = (this.FadeInSpeed > 0.001f);
		this.isOut = (this.FadeOutSpeed > 0.001f);
		this.InitDefaultVariables();
		this.isInitialized = true;
	}

	// Token: 0x0600149D RID: 5277 RVA: 0x00081440 File Offset: 0x0007F840
	private void InitDefaultVariables()
	{
		this.fadeInComplited = false;
		this.fadeOutComplited = false;
		this.allComplited = false;
		this.canStartFadeOut = false;
		this.isCollisionEnter = false;
		this.oldVolume = 0f;
		this.currentVolume = this.MaxVolume;
		if (this.isIn)
		{
			this.currentVolume = 0f;
		}
		this.audioSource.volume = this.currentVolume;
		if (this.isStartDelay)
		{
			base.Invoke("SetupStartDelay", this.StartDelay);
		}
		else
		{
			this.canStart = true;
		}
		if (!this.isIn)
		{
			if (!this.FadeOutAfterCollision)
			{
				base.Invoke("SetupFadeOutDelay", this.FadeOutDelay);
			}
			this.oldVolume = this.MaxVolume;
		}
	}

	// Token: 0x0600149E RID: 5278 RVA: 0x00081509 File Offset: 0x0007F909
	private void prefabSettings_CollisionEnter(object sender, CollisionInfo e)
	{
		this.isCollisionEnter = true;
		if (!this.isIn && this.FadeOutAfterCollision)
		{
			base.Invoke("SetupFadeOutDelay", this.FadeOutDelay);
		}
	}

	// Token: 0x0600149F RID: 5279 RVA: 0x00081539 File Offset: 0x0007F939
	private void OnEnable()
	{
		if (this.isInitialized)
		{
			this.InitDefaultVariables();
		}
	}

	// Token: 0x060014A0 RID: 5280 RVA: 0x0008154C File Offset: 0x0007F94C
	private void SetupStartDelay()
	{
		this.canStart = true;
	}

	// Token: 0x060014A1 RID: 5281 RVA: 0x00081555 File Offset: 0x0007F955
	private void SetupFadeOutDelay()
	{
		this.canStartFadeOut = true;
	}

	// Token: 0x060014A2 RID: 5282 RVA: 0x00081560 File Offset: 0x0007F960
	private void Update()
	{
		if (!this.canStart || this.audioSource == null)
		{
			return;
		}
		if (this.effectSettings != null && this.UseHideStatus && this.allComplited && this.effectSettings.IsVisible)
		{
			this.allComplited = false;
			this.fadeInComplited = false;
			this.fadeOutComplited = false;
			this.InitDefaultVariables();
		}
		if (this.isIn && !this.fadeInComplited)
		{
			if (this.effectSettings == null)
			{
				this.FadeIn();
			}
			else if ((this.UseHideStatus && this.effectSettings.IsVisible) || !this.UseHideStatus)
			{
				this.FadeIn();
			}
		}
		if (!this.isOut || this.fadeOutComplited || !this.canStartFadeOut)
		{
			return;
		}
		if (this.effectSettings == null || (!this.UseHideStatus && !this.FadeOutAfterCollision))
		{
			this.FadeOut();
		}
		else if ((this.UseHideStatus && !this.effectSettings.IsVisible) || this.isCollisionEnter)
		{
			this.FadeOut();
		}
	}

	// Token: 0x060014A3 RID: 5283 RVA: 0x000816BC File Offset: 0x0007FABC
	private void FadeIn()
	{
		this.currentVolume = this.oldVolume + Time.deltaTime / this.FadeInSpeed * this.MaxVolume;
		if (this.currentVolume >= this.MaxVolume)
		{
			this.fadeInComplited = true;
			if (!this.isOut)
			{
				this.allComplited = true;
			}
			this.currentVolume = this.MaxVolume;
			base.Invoke("SetupFadeOutDelay", this.FadeOutDelay);
		}
		this.audioSource.volume = this.currentVolume;
		this.oldVolume = this.currentVolume;
	}

	// Token: 0x060014A4 RID: 5284 RVA: 0x00081750 File Offset: 0x0007FB50
	private void FadeOut()
	{
		this.currentVolume = this.oldVolume - Time.deltaTime / this.FadeOutSpeed * this.MaxVolume;
		if (this.currentVolume <= 0f)
		{
			this.currentVolume = 0f;
			this.fadeOutComplited = true;
			this.allComplited = true;
		}
		this.audioSource.volume = this.currentVolume;
		this.oldVolume = this.currentVolume;
	}

	// Token: 0x040017AA RID: 6058
	public float MaxVolume = 1f;

	// Token: 0x040017AB RID: 6059
	public float StartDelay;

	// Token: 0x040017AC RID: 6060
	public float FadeInSpeed;

	// Token: 0x040017AD RID: 6061
	public float FadeOutDelay;

	// Token: 0x040017AE RID: 6062
	public float FadeOutSpeed;

	// Token: 0x040017AF RID: 6063
	public bool FadeOutAfterCollision;

	// Token: 0x040017B0 RID: 6064
	public bool UseHideStatus;

	// Token: 0x040017B1 RID: 6065
	private AudioSource audioSource;

	// Token: 0x040017B2 RID: 6066
	private float oldVolume;

	// Token: 0x040017B3 RID: 6067
	private float currentVolume;

	// Token: 0x040017B4 RID: 6068
	private bool canStart;

	// Token: 0x040017B5 RID: 6069
	private bool canStartFadeOut;

	// Token: 0x040017B6 RID: 6070
	private bool fadeInComplited;

	// Token: 0x040017B7 RID: 6071
	private bool fadeOutComplited;

	// Token: 0x040017B8 RID: 6072
	private bool isCollisionEnter;

	// Token: 0x040017B9 RID: 6073
	private bool allComplited;

	// Token: 0x040017BA RID: 6074
	private bool isStartDelay;

	// Token: 0x040017BB RID: 6075
	private bool isIn;

	// Token: 0x040017BC RID: 6076
	private bool isOut;

	// Token: 0x040017BD RID: 6077
	private EffectSettings effectSettings;

	// Token: 0x040017BE RID: 6078
	private bool isInitialized;
}
