using System;
using UnityEngine;

// Token: 0x0200045C RID: 1116
public class FadeInOutLight : MonoBehaviour
{
	// Token: 0x0600146A RID: 5226 RVA: 0x0007FE2C File Offset: 0x0007E22C
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

	// Token: 0x0600146B RID: 5227 RVA: 0x0007FE78 File Offset: 0x0007E278
	private void Start()
	{
		this.GetEffectSettingsComponent(base.transform);
		if (this.effectSettings != null)
		{
			this.effectSettings.CollisionEnter += this.prefabSettings_CollisionEnter;
		}
		this.goLight = base.GetComponent<Light>();
		this.startIntensity = this.goLight.intensity;
		this.isStartDelay = (this.StartDelay > 0.001f);
		this.isIn = (this.FadeInSpeed > 0.001f);
		this.isOut = (this.FadeOutSpeed > 0.001f);
		this.InitDefaultVariables();
		this.isInitialized = true;
	}

	// Token: 0x0600146C RID: 5228 RVA: 0x0007FF1C File Offset: 0x0007E31C
	private void InitDefaultVariables()
	{
		this.fadeInComplited = false;
		this.fadeOutComplited = false;
		this.allComplited = false;
		this.canStartFadeOut = false;
		this.isCollisionEnter = false;
		this.oldIntensity = 0f;
		this.currentIntensity = 0f;
		this.canStart = false;
		this.goLight.intensity = ((!this.isIn) ? this.startIntensity : 0f);
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
			this.oldIntensity = this.startIntensity;
		}
	}

	// Token: 0x0600146D RID: 5229 RVA: 0x0007FFEA File Offset: 0x0007E3EA
	private void prefabSettings_CollisionEnter(object sender, CollisionInfo e)
	{
		this.isCollisionEnter = true;
		if (!this.isIn && this.FadeOutAfterCollision)
		{
			base.Invoke("SetupFadeOutDelay", this.FadeOutDelay);
		}
	}

	// Token: 0x0600146E RID: 5230 RVA: 0x0008001A File Offset: 0x0007E41A
	private void OnEnable()
	{
		if (this.isInitialized)
		{
			this.InitDefaultVariables();
		}
	}

	// Token: 0x0600146F RID: 5231 RVA: 0x0008002D File Offset: 0x0007E42D
	private void SetupStartDelay()
	{
		this.canStart = true;
	}

	// Token: 0x06001470 RID: 5232 RVA: 0x00080036 File Offset: 0x0007E436
	private void SetupFadeOutDelay()
	{
		this.canStartFadeOut = true;
	}

	// Token: 0x06001471 RID: 5233 RVA: 0x00080040 File Offset: 0x0007E440
	private void Update()
	{
		if (!this.canStart)
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
		else if ((this.UseHideStatus && !this.effectSettings.IsVisible) || (this.FadeOutAfterCollision && this.isCollisionEnter))
		{
			this.FadeOut();
		}
	}

	// Token: 0x06001472 RID: 5234 RVA: 0x00080194 File Offset: 0x0007E594
	private void FadeIn()
	{
		this.currentIntensity = this.oldIntensity + Time.deltaTime / this.FadeInSpeed * this.startIntensity;
		if (this.currentIntensity >= this.startIntensity)
		{
			this.fadeInComplited = true;
			if (!this.isOut)
			{
				this.allComplited = true;
			}
			this.currentIntensity = this.startIntensity;
			base.Invoke("SetupFadeOutDelay", this.FadeOutDelay);
		}
		this.goLight.intensity = this.currentIntensity;
		this.oldIntensity = this.currentIntensity;
	}

	// Token: 0x06001473 RID: 5235 RVA: 0x00080228 File Offset: 0x0007E628
	private void FadeOut()
	{
		this.currentIntensity = this.oldIntensity - Time.deltaTime / this.FadeOutSpeed * this.startIntensity;
		if (this.currentIntensity <= 0f)
		{
			this.currentIntensity = 0f;
			this.fadeOutComplited = true;
			this.allComplited = true;
		}
		this.goLight.intensity = this.currentIntensity;
		this.oldIntensity = this.currentIntensity;
	}

	// Token: 0x04001758 RID: 5976
	public float StartDelay;

	// Token: 0x04001759 RID: 5977
	public float FadeInSpeed;

	// Token: 0x0400175A RID: 5978
	public float FadeOutDelay;

	// Token: 0x0400175B RID: 5979
	public float FadeOutSpeed;

	// Token: 0x0400175C RID: 5980
	public bool FadeOutAfterCollision;

	// Token: 0x0400175D RID: 5981
	public bool UseHideStatus;

	// Token: 0x0400175E RID: 5982
	private Light goLight;

	// Token: 0x0400175F RID: 5983
	private float oldIntensity;

	// Token: 0x04001760 RID: 5984
	private float currentIntensity;

	// Token: 0x04001761 RID: 5985
	private float startIntensity;

	// Token: 0x04001762 RID: 5986
	private bool canStart;

	// Token: 0x04001763 RID: 5987
	private bool canStartFadeOut;

	// Token: 0x04001764 RID: 5988
	private bool fadeInComplited;

	// Token: 0x04001765 RID: 5989
	private bool fadeOutComplited;

	// Token: 0x04001766 RID: 5990
	private bool isCollisionEnter;

	// Token: 0x04001767 RID: 5991
	private bool allComplited;

	// Token: 0x04001768 RID: 5992
	private bool isStartDelay;

	// Token: 0x04001769 RID: 5993
	private bool isIn;

	// Token: 0x0400176A RID: 5994
	private bool isOut;

	// Token: 0x0400176B RID: 5995
	private EffectSettings effectSettings;

	// Token: 0x0400176C RID: 5996
	private bool isInitialized;
}
