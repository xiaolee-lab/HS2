using System;
using UnityEngine;

// Token: 0x0200045F RID: 1119
public class FadeInOutShaderColor : MonoBehaviour
{
	// Token: 0x06001480 RID: 5248 RVA: 0x000807C0 File Offset: 0x0007EBC0
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

	// Token: 0x06001481 RID: 5249 RVA: 0x00080809 File Offset: 0x0007EC09
	public void UpdateMaterial(Material instanceMaterial)
	{
		this.mat = instanceMaterial;
		this.InitMaterial();
	}

	// Token: 0x06001482 RID: 5250 RVA: 0x00080818 File Offset: 0x0007EC18
	private void Start()
	{
		this.GetEffectSettingsComponent(base.transform);
		if (this.effectSettings != null)
		{
			this.effectSettings.CollisionEnter += this.prefabSettings_CollisionEnter;
		}
		this.InitMaterial();
	}

	// Token: 0x06001483 RID: 5251 RVA: 0x00080854 File Offset: 0x0007EC54
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
		this.oldColor = this.mat.GetColor(this.ShaderColorName);
		this.isStartDelay = (this.StartDelay > 0.001f);
		this.isIn = (this.FadeInSpeed > 0.001f);
		this.isOut = (this.FadeOutSpeed > 0.001f);
		this.InitDefaultVariables();
		this.isInitialized = true;
	}

	// Token: 0x06001484 RID: 5252 RVA: 0x00080990 File Offset: 0x0007ED90
	private void InitDefaultVariables()
	{
		this.fadeInComplited = false;
		this.fadeOutComplited = false;
		this.canStartFadeOut = false;
		this.isCollisionEnter = false;
		this.oldAlpha = 0f;
		this.alpha = 0f;
		this.canStart = false;
		this.currentColor = this.oldColor;
		if (this.isIn)
		{
			this.currentColor.a = 0f;
		}
		this.mat.SetColor(this.ShaderColorName, this.currentColor);
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
			this.oldAlpha = this.oldColor.a;
		}
	}

	// Token: 0x06001485 RID: 5253 RVA: 0x00080A74 File Offset: 0x0007EE74
	private void prefabSettings_CollisionEnter(object sender, CollisionInfo e)
	{
		this.isCollisionEnter = true;
		if (!this.isIn && this.FadeOutAfterCollision)
		{
			base.Invoke("SetupFadeOutDelay", this.FadeOutDelay);
		}
	}

	// Token: 0x06001486 RID: 5254 RVA: 0x00080AA4 File Offset: 0x0007EEA4
	private void OnEnable()
	{
		if (this.isInitialized)
		{
			this.InitDefaultVariables();
		}
	}

	// Token: 0x06001487 RID: 5255 RVA: 0x00080AB7 File Offset: 0x0007EEB7
	private void SetupStartDelay()
	{
		this.canStart = true;
	}

	// Token: 0x06001488 RID: 5256 RVA: 0x00080AC0 File Offset: 0x0007EEC0
	private void SetupFadeOutDelay()
	{
		this.canStartFadeOut = true;
	}

	// Token: 0x06001489 RID: 5257 RVA: 0x00080ACC File Offset: 0x0007EECC
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

	// Token: 0x0600148A RID: 5258 RVA: 0x00080C70 File Offset: 0x0007F070
	private void FadeIn()
	{
		this.alpha = this.oldAlpha + Time.deltaTime / this.FadeInSpeed;
		if (this.alpha >= this.oldColor.a)
		{
			this.fadeInComplited = true;
			this.alpha = this.oldColor.a;
			base.Invoke("SetupFadeOutDelay", this.FadeOutDelay);
		}
		this.currentColor.a = this.alpha;
		this.mat.SetColor(this.ShaderColorName, this.currentColor);
		this.oldAlpha = this.alpha;
	}

	// Token: 0x0600148B RID: 5259 RVA: 0x00080D0C File Offset: 0x0007F10C
	private void FadeOut()
	{
		this.alpha = this.oldAlpha - Time.deltaTime / this.FadeOutSpeed;
		if (this.alpha <= 0f)
		{
			this.alpha = 0f;
			this.fadeOutComplited = true;
		}
		this.currentColor.a = this.alpha;
		this.mat.SetColor(this.ShaderColorName, this.currentColor);
		this.oldAlpha = this.alpha;
	}

	// Token: 0x0400177C RID: 6012
	public string ShaderColorName = "_Color";

	// Token: 0x0400177D RID: 6013
	public float StartDelay;

	// Token: 0x0400177E RID: 6014
	public float FadeInSpeed;

	// Token: 0x0400177F RID: 6015
	public float FadeOutDelay;

	// Token: 0x04001780 RID: 6016
	public float FadeOutSpeed;

	// Token: 0x04001781 RID: 6017
	public bool UseSharedMaterial;

	// Token: 0x04001782 RID: 6018
	public bool FadeOutAfterCollision;

	// Token: 0x04001783 RID: 6019
	public bool UseHideStatus;

	// Token: 0x04001784 RID: 6020
	private Material mat;

	// Token: 0x04001785 RID: 6021
	private Color oldColor;

	// Token: 0x04001786 RID: 6022
	private Color currentColor;

	// Token: 0x04001787 RID: 6023
	private float oldAlpha;

	// Token: 0x04001788 RID: 6024
	private float alpha;

	// Token: 0x04001789 RID: 6025
	private bool canStart;

	// Token: 0x0400178A RID: 6026
	private bool canStartFadeOut;

	// Token: 0x0400178B RID: 6027
	private bool fadeInComplited;

	// Token: 0x0400178C RID: 6028
	private bool fadeOutComplited;

	// Token: 0x0400178D RID: 6029
	private bool isCollisionEnter;

	// Token: 0x0400178E RID: 6030
	private bool isStartDelay;

	// Token: 0x0400178F RID: 6031
	private bool isIn;

	// Token: 0x04001790 RID: 6032
	private bool isOut;

	// Token: 0x04001791 RID: 6033
	private EffectSettings effectSettings;

	// Token: 0x04001792 RID: 6034
	private bool isInitialized;
}
