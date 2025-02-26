using System;
using UnityEngine;

// Token: 0x02000460 RID: 1120
public class FadeInOutShaderFloat : MonoBehaviour
{
	// Token: 0x0600148D RID: 5261 RVA: 0x00080DA8 File Offset: 0x0007F1A8
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

	// Token: 0x0600148E RID: 5262 RVA: 0x00080DF1 File Offset: 0x0007F1F1
	private void Start()
	{
		this.GetEffectSettingsComponent(base.transform);
		if (this.effectSettings != null)
		{
			this.effectSettings.CollisionEnter += this.prefabSettings_CollisionEnter;
		}
		this.InitMaterial();
	}

	// Token: 0x0600148F RID: 5263 RVA: 0x00080E2D File Offset: 0x0007F22D
	public void UpdateMaterial(Material instanceMaterial)
	{
		this.mat = instanceMaterial;
		this.InitMaterial();
	}

	// Token: 0x06001490 RID: 5264 RVA: 0x00080E3C File Offset: 0x0007F23C
	private void InitMaterial()
	{
		if (this.isInitialized)
		{
			return;
		}
		if (base.GetComponent<Renderer>() != null)
		{
			this.mat = base.GetComponent<Renderer>().material;
		}
		else
		{
			LineRenderer component = base.GetComponent<LineRenderer>();
			if (component != null)
			{
				this.mat = component.material;
			}
			else
			{
				Projector component2 = base.GetComponent<Projector>();
				if (component2 != null)
				{
					if (!component2.material.name.EndsWith("(Instance)"))
					{
						component2.material = new Material(component2.material)
						{
							name = component2.material.name + " (Instance)"
						};
					}
					this.mat = component2.material;
				}
			}
		}
		if (this.mat == null)
		{
			return;
		}
		this.isStartDelay = (this.StartDelay > 0.001f);
		this.isIn = (this.FadeInSpeed > 0.001f);
		this.isOut = (this.FadeOutSpeed > 0.001f);
		this.InitDefaultVariables();
		this.isInitialized = true;
	}

	// Token: 0x06001491 RID: 5265 RVA: 0x00080F60 File Offset: 0x0007F360
	private void InitDefaultVariables()
	{
		this.fadeInComplited = false;
		this.fadeOutComplited = false;
		this.canStartFadeOut = false;
		this.canStart = false;
		this.isCollisionEnter = false;
		this.oldFloat = 0f;
		this.currentFloat = this.MaxFloat;
		if (this.isIn)
		{
			this.currentFloat = 0f;
		}
		this.mat.SetFloat(this.PropertyName, this.currentFloat);
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
			this.oldFloat = this.MaxFloat;
		}
	}

	// Token: 0x06001492 RID: 5266 RVA: 0x0008102F File Offset: 0x0007F42F
	private void prefabSettings_CollisionEnter(object sender, CollisionInfo e)
	{
		this.isCollisionEnter = true;
		if (!this.isIn && this.FadeOutAfterCollision)
		{
			base.Invoke("SetupFadeOutDelay", this.FadeOutDelay);
		}
	}

	// Token: 0x06001493 RID: 5267 RVA: 0x0008105F File Offset: 0x0007F45F
	private void OnEnable()
	{
		if (this.isInitialized)
		{
			this.InitDefaultVariables();
		}
	}

	// Token: 0x06001494 RID: 5268 RVA: 0x00081072 File Offset: 0x0007F472
	private void SetupStartDelay()
	{
		this.canStart = true;
	}

	// Token: 0x06001495 RID: 5269 RVA: 0x0008107B File Offset: 0x0007F47B
	private void SetupFadeOutDelay()
	{
		this.canStartFadeOut = true;
	}

	// Token: 0x06001496 RID: 5270 RVA: 0x00081084 File Offset: 0x0007F484
	private void Update()
	{
		if (!this.canStart)
		{
			return;
		}
		if (this.effectSettings != null && this.UseHideStatus)
		{
			if (!this.effectSettings.IsVisible && this.fadeInComplited)
			{
				this.fadeInComplited = false;
			}
			if (this.effectSettings.IsVisible && this.fadeOutComplited)
			{
				this.fadeOutComplited = false;
			}
		}
		if (this.UseHideStatus)
		{
			if (this.isIn && this.effectSettings != null && this.effectSettings.IsVisible && !this.fadeInComplited)
			{
				this.FadeIn();
			}
			if (this.isOut && this.effectSettings != null && !this.effectSettings.IsVisible && !this.fadeOutComplited)
			{
				this.FadeOut();
			}
		}
		else if (!this.FadeOutAfterCollision)
		{
			if (this.isIn && !this.fadeInComplited)
			{
				this.FadeIn();
			}
			if (this.isOut && this.canStartFadeOut && !this.fadeOutComplited)
			{
				this.FadeOut();
			}
		}
		else
		{
			if (this.isIn && !this.fadeInComplited)
			{
				this.FadeIn();
			}
			if (this.isOut && this.isCollisionEnter && this.canStartFadeOut && !this.fadeOutComplited)
			{
				this.FadeOut();
			}
		}
	}

	// Token: 0x06001497 RID: 5271 RVA: 0x00081228 File Offset: 0x0007F628
	private void FadeIn()
	{
		this.currentFloat = this.oldFloat + Time.deltaTime / this.FadeInSpeed * this.MaxFloat;
		if (this.currentFloat >= this.MaxFloat)
		{
			this.fadeInComplited = true;
			this.currentFloat = this.MaxFloat;
			base.Invoke("SetupFadeOutDelay", this.FadeOutDelay);
		}
		this.mat.SetFloat(this.PropertyName, this.currentFloat);
		this.oldFloat = this.currentFloat;
	}

	// Token: 0x06001498 RID: 5272 RVA: 0x000812B0 File Offset: 0x0007F6B0
	private void FadeOut()
	{
		this.currentFloat = this.oldFloat - Time.deltaTime / this.FadeOutSpeed * this.MaxFloat;
		if (this.currentFloat <= 0f)
		{
			this.currentFloat = 0f;
			this.fadeOutComplited = true;
		}
		this.mat.SetFloat(this.PropertyName, this.currentFloat);
		this.oldFloat = this.currentFloat;
	}

	// Token: 0x04001793 RID: 6035
	public string PropertyName = "_CutOut";

	// Token: 0x04001794 RID: 6036
	public float MaxFloat = 1f;

	// Token: 0x04001795 RID: 6037
	public float StartDelay;

	// Token: 0x04001796 RID: 6038
	public float FadeInSpeed;

	// Token: 0x04001797 RID: 6039
	public float FadeOutDelay;

	// Token: 0x04001798 RID: 6040
	public float FadeOutSpeed;

	// Token: 0x04001799 RID: 6041
	public bool FadeOutAfterCollision;

	// Token: 0x0400179A RID: 6042
	public bool UseHideStatus;

	// Token: 0x0400179B RID: 6043
	private Material OwnMaterial;

	// Token: 0x0400179C RID: 6044
	private Material mat;

	// Token: 0x0400179D RID: 6045
	private float oldFloat;

	// Token: 0x0400179E RID: 6046
	private float currentFloat;

	// Token: 0x0400179F RID: 6047
	private bool canStart;

	// Token: 0x040017A0 RID: 6048
	private bool canStartFadeOut;

	// Token: 0x040017A1 RID: 6049
	private bool fadeInComplited;

	// Token: 0x040017A2 RID: 6050
	private bool fadeOutComplited;

	// Token: 0x040017A3 RID: 6051
	private bool previousFrameVisibleStatus;

	// Token: 0x040017A4 RID: 6052
	private bool isCollisionEnter;

	// Token: 0x040017A5 RID: 6053
	private bool isStartDelay;

	// Token: 0x040017A6 RID: 6054
	private bool isIn;

	// Token: 0x040017A7 RID: 6055
	private bool isOut;

	// Token: 0x040017A8 RID: 6056
	private EffectSettings effectSettings;

	// Token: 0x040017A9 RID: 6057
	private bool isInitialized;
}
