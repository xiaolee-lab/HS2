using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200061B RID: 1563
public class bl_AllOptionsPro : MonoBehaviour
{
	// Token: 0x06002534 RID: 9524 RVA: 0x000D4970 File Offset: 0x000D2D70
	private void Awake()
	{
		if (UnityEngine.Object.FindObjectOfType<bl_BrightnessImage>() != null)
		{
			this.BrightnessImage = UnityEngine.Object.FindObjectOfType<bl_BrightnessImage>();
		}
		if (this.HUDCanvas)
		{
			this._hudScale = 1f - this.HUDCanvas.matchWidthOrHeight;
		}
	}

	// Token: 0x06002535 RID: 9525 RVA: 0x000D49BF File Offset: 0x000D2DBF
	private void Start()
	{
		if (this.ApplyOnStart)
		{
			this.LoadAndApply();
		}
		this.ChangeWindow(this.StartWindow, false);
		this.ChangeSelectionButton(this.PanelButtons[this.StartWindow]);
		this.SettingsPanel.SetActive(false);
	}

	// Token: 0x06002536 RID: 9526 RVA: 0x000D49FE File Offset: 0x000D2DFE
	private void OnDisable()
	{
		if (this.SaveOnDisable)
		{
			this.SaveOptions();
		}
	}

	// Token: 0x06002537 RID: 9527 RVA: 0x000D4A11 File Offset: 0x000D2E11
	private void OnApplicationQuit()
	{
		if (this.SaveOnDisable)
		{
			this.SaveOptions();
		}
	}

	// Token: 0x06002538 RID: 9528 RVA: 0x000D4A24 File Offset: 0x000D2E24
	public void ChangeWindow(int _id)
	{
		this.PanelAnimator.Play("Change", 0, 0f);
		base.StartCoroutine(this.WaitForSwichet(_id));
	}

	// Token: 0x06002539 RID: 9529 RVA: 0x000D4A4A File Offset: 0x000D2E4A
	public void ChangeWindow(int _id, bool anim)
	{
		if (anim)
		{
			this.PanelAnimator.Play("Change", 0, 0f);
		}
		base.StartCoroutine(this.WaitForSwichet(_id));
	}

	// Token: 0x0600253A RID: 9530 RVA: 0x000D4A78 File Offset: 0x000D2E78
	public void ChangeSelectionButton(Button b)
	{
		for (int i = 0; i < this.PanelButtons.Length; i++)
		{
			this.PanelButtons[i].interactable = true;
		}
		b.interactable = false;
	}

	// Token: 0x0600253B RID: 9531 RVA: 0x000D4AB4 File Offset: 0x000D2EB4
	public void ShowMenu()
	{
		this.Show = !this.Show;
		if (this.Show)
		{
			base.StopCoroutine("HideAnimate");
			this.SettingsPanel.SetActive(true);
			this.ContentAnim.SetBool("Show", true);
		}
		else if (this.AnimateHidePanel)
		{
			base.StartCoroutine("HideAnimate");
		}
		else
		{
			this.SettingsPanel.SetActive(false);
		}
	}

	// Token: 0x0600253C RID: 9532 RVA: 0x000D4B30 File Offset: 0x000D2F30
	public void GameQuality(bool mas)
	{
		if (mas)
		{
			this.CurrentQuality = (this.CurrentQuality + 1) % QualitySettings.names.Length;
		}
		else if (this.CurrentQuality != 0)
		{
			this.CurrentQuality = (this.CurrentQuality - 1) % QualitySettings.names.Length;
		}
		else
		{
			this.CurrentQuality = QualitySettings.names.Length - 1;
		}
		this.QualityText.text = QualitySettings.names[this.CurrentQuality].ToUpper();
		QualitySettings.SetQualityLevel(this.CurrentQuality);
	}

	// Token: 0x0600253D RID: 9533 RVA: 0x000D4BBC File Offset: 0x000D2FBC
	public void AntiStropic(bool b)
	{
		if (b)
		{
			this.CurrentAS = (this.CurrentAS + 1) % 3;
		}
		else if (this.CurrentAS != 0)
		{
			this.CurrentAS = (this.CurrentAS - 1) % 3;
		}
		else
		{
			this.CurrentAS = 2;
		}
		int currentAS = this.CurrentAS;
		if (currentAS != 0)
		{
			if (currentAS != 1)
			{
				if (currentAS == 2)
				{
					QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
					this.AnisotropicText.text = AnisotropicFiltering.ForceEnable.ToString().ToUpper();
				}
			}
			else
			{
				QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
				this.AnisotropicText.text = AnisotropicFiltering.Enable.ToString().ToUpper();
			}
		}
		else
		{
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
			this.AnisotropicText.text = AnisotropicFiltering.Disable.ToString().ToUpper();
		}
	}

	// Token: 0x0600253E RID: 9534 RVA: 0x000D4CA9 File Offset: 0x000D30A9
	public void FullScreenMode(bool use)
	{
		this.useFullScreen = use;
		this.FullScreenOnText.text = ((!this.useFullScreen) ? "OFF" : "ON");
	}

	// Token: 0x0600253F RID: 9535 RVA: 0x000D4CD8 File Offset: 0x000D30D8
	public void AntiAliasing(bool b)
	{
		this.CurrentAA = ((!b) ? ((this.CurrentAA == 0) ? (this.CurrentAA = 3) : ((this.CurrentAA - 1) % 4)) : ((this.CurrentAA + 1) % 4));
		this.AntiAliasingText.text = this.AntiAliasingNames[this.CurrentAA].ToUpper();
		switch (this.CurrentAA)
		{
		case 0:
			QualitySettings.antiAliasing = 0;
			break;
		case 1:
			QualitySettings.antiAliasing = 2;
			break;
		case 2:
			QualitySettings.antiAliasing = 4;
			break;
		case 3:
			QualitySettings.antiAliasing = 8;
			break;
		}
	}

	// Token: 0x06002540 RID: 9536 RVA: 0x000D4D90 File Offset: 0x000D3190
	public void ShowFPS()
	{
		this._showFPS = !this._showFPS;
		this.ShowFPSText.text = ((!this._showFPS) ? "OFF" : "ON");
		if (this.FPSObject != null)
		{
			foreach (GameObject gameObject in this.FPSObject)
			{
				gameObject.SetActive(this._showFPS);
			}
		}
	}

	// Token: 0x06002541 RID: 9537 RVA: 0x000D4E08 File Offset: 0x000D3208
	public void PauseSound(bool b)
	{
		this._isPauseSound = b;
		string text = (!this._isPauseSound) ? "OFF" : "ON";
		this.PauseText.text = text;
	}

	// Token: 0x06002542 RID: 9538 RVA: 0x000D4E44 File Offset: 0x000D3244
	public void VSyncCount(bool b)
	{
		this.CurrentVSC = ((!b) ? ((this.CurrentVSC == 0) ? (this.CurrentVSC = 2) : ((this.CurrentVSC - 1) % 3)) : ((this.CurrentVSC + 1) % 3));
		this.vSyncText.text = this.VSyncNames[this.CurrentVSC].ToUpper();
		int currentVSC = this.CurrentVSC;
		if (currentVSC != 0)
		{
			if (currentVSC != 1)
			{
				if (currentVSC == 2)
				{
					QualitySettings.vSyncCount = 2;
				}
			}
			else
			{
				QualitySettings.vSyncCount = 1;
			}
		}
		else
		{
			QualitySettings.vSyncCount = 0;
		}
	}

	// Token: 0x06002543 RID: 9539 RVA: 0x000D4EF0 File Offset: 0x000D32F0
	public void TextureQuality(bool b)
	{
		this.CurrentTL = ((!b) ? ((this.CurrentTL == 0) ? (this.CurrentTL = 3) : ((this.CurrentTL - 1) % 3)) : ((this.CurrentTL + 1) % 3));
		QualitySettings.masterTextureLimit = this.CurrentTL;
		this.TextureLimitText.text = this.TextureQualityNames[this.CurrentTL];
	}

	// Token: 0x06002544 RID: 9540 RVA: 0x000D4F60 File Offset: 0x000D3360
	public void ShadowCascades(bool b)
	{
		this.CurrentSC = ((!b) ? ((this.CurrentSC == 0) ? (this.CurrentSC = 3) : ((this.CurrentSC - 1) % 3)) : ((this.CurrentSC + 1) % 3));
		QualitySettings.shadowCascades = this.ShadowCascadeOptions[this.CurrentSC];
		this.ShadowCascadeText.text = this.ShadowCascadeNames[this.CurrentSC];
	}

	// Token: 0x06002545 RID: 9541 RVA: 0x000D4FD8 File Offset: 0x000D33D8
	public void blendWeights(bool b)
	{
		this.CurrentBW = ((!b) ? ((this.CurrentBW == 0) ? (this.CurrentBW = 2) : ((this.CurrentBW - 1) % 3)) : ((this.CurrentBW + 1) % 3));
		int currentBW = this.CurrentBW;
		if (currentBW != 0)
		{
			if (currentBW != 1)
			{
				if (currentBW == 2)
				{
					QualitySettings.blendWeights = BlendWeights.FourBones;
					this.blendWeightsText.text = BlendWeights.FourBones.ToString().ToUpper();
				}
			}
			else
			{
				QualitySettings.blendWeights = BlendWeights.TwoBones;
				this.blendWeightsText.text = BlendWeights.TwoBones.ToString().ToUpper();
			}
		}
		else
		{
			QualitySettings.blendWeights = BlendWeights.OneBone;
			this.blendWeightsText.text = BlendWeights.OneBone.ToString().ToUpper();
		}
	}

	// Token: 0x06002546 RID: 9542 RVA: 0x000D50C4 File Offset: 0x000D34C4
	public void SetBrightness(float v)
	{
		if (this.BrightnessImage == null)
		{
			return;
		}
		this._brightness = v;
		this.BrightnessImage.SetValue(v);
		this.BrightnessSlider.value = v;
		this.BrightnessText.text = string.Format("{0}%", (v * 100f).ToString("F0"));
	}

	// Token: 0x06002547 RID: 9543 RVA: 0x000D512B File Offset: 0x000D352B
	public void SetLodBias(float value)
	{
		QualitySettings.lodBias = value;
		this._lodBias = value;
		this.LoadBiasText.text = string.Format("{0}", value.ToString("F2"));
	}

	// Token: 0x06002548 RID: 9544 RVA: 0x000D515B File Offset: 0x000D355B
	public void ShadowDistance(float value)
	{
		if (this._shadowEnable)
		{
			QualitySettings.shadowDistance = value;
		}
		this.ShadowDistanceText.text = string.Format("{0}m", value.ToString("F0"));
		this.cacheShadowDistance = value;
	}

	// Token: 0x06002549 RID: 9545 RVA: 0x000D5198 File Offset: 0x000D3598
	public void SetShadowEnable(bool enable)
	{
		QualitySettings.shadowDistance = ((!enable) ? 0f : this.cacheShadowDistance);
		this._shadowEnable = enable;
		this.ShadowEnebleText.text = ((!enable) ? "DISABLE" : "ENABLE");
	}

	// Token: 0x0600254A RID: 9546 RVA: 0x000D51E7 File Offset: 0x000D35E7
	public void SetRealTimeReflection(bool b)
	{
		QualitySettings.realtimeReflectionProbes = b;
		this._realtimeReflection = b;
		this.RealtimeReflectionText.text = ((!this._realtimeReflection) ? "DISABLE" : "ENABLE");
	}

	// Token: 0x0600254B RID: 9547 RVA: 0x000D521C File Offset: 0x000D361C
	public void SetHUDScale(float value)
	{
		if (this.HUDCanvas == null)
		{
			return;
		}
		this.HUDCanvas.matchWidthOrHeight = 1f - value;
		this._hudScale = value;
		this.HudScaleText.text = string.Format("{0}", value.ToString("F2"));
	}

	// Token: 0x0600254C RID: 9548 RVA: 0x000D5278 File Offset: 0x000D3678
	public void Resolution(bool b)
	{
		this.CurrentRS = ((!b) ? ((this.CurrentRS == 0) ? (this.CurrentRS = Screen.resolutions.Length - 1) : ((this.CurrentRS - 1) % Screen.resolutions.Length)) : ((this.CurrentRS + 1) % Screen.resolutions.Length));
		this.ResolutionText.text = Screen.resolutions[this.CurrentRS].width + " X " + Screen.resolutions[this.CurrentRS].height;
	}

	// Token: 0x0600254D RID: 9549 RVA: 0x000D5324 File Offset: 0x000D3724
	public void Volumen(float v)
	{
		AudioListener.volume = v;
		this._volumen = v;
		this.VolumenText.text = (this._volumen * 100f).ToString("00") + "%";
	}

	// Token: 0x0600254E RID: 9550 RVA: 0x000D536C File Offset: 0x000D376C
	public void ShadowProjectionType(bool b)
	{
		if (b)
		{
			QualitySettings.shadowProjection = ShadowProjection.StableFit;
			this.ShadowProjectionText.text = ShadowProjection.StableFit.ToString().ToUpper();
		}
		else
		{
			QualitySettings.shadowProjection = ShadowProjection.CloseFit;
			this.ShadowProjectionText.text = ShadowProjection.CloseFit.ToString().ToUpper();
		}
	}

	// Token: 0x0600254F RID: 9551 RVA: 0x000D53D0 File Offset: 0x000D37D0
	public void ApplyResolution()
	{
		bool fullscreen = this.AutoApplyResolution && this.useFullScreen;
		Screen.SetResolution(Screen.resolutions[this.CurrentRS].width, Screen.resolutions[this.CurrentRS].height, fullscreen);
	}

	// Token: 0x06002550 RID: 9552 RVA: 0x000D5428 File Offset: 0x000D3828
	private void LoadAndApply()
	{
		bl_Input.Instance.InitInput();
		this.CurrentAA = PlayerPrefs.GetInt("GameName.AntiAliasing", this.DefaultAntiAliasing);
		this.CurrentAS = PlayerPrefs.GetInt("GameName.AnisoTropic", this.DefaultAnisoTropic);
		this.CurrentBW = PlayerPrefs.GetInt("GameName.BlendWeight", this.DefaultBlendWeight);
		this.CurrentQuality = PlayerPrefs.GetInt("GameName.QualityLevel", this.DefaultQuality);
		this.CurrentRS = PlayerPrefs.GetInt("GameName.ResolutionScreen", this.DefaultResolution);
		this.CurrentVSC = PlayerPrefs.GetInt("GameName.VSyncCount", this.DefaultVSync);
		this.CurrentTL = PlayerPrefs.GetInt("GameName.TextureLimit", 0);
		this.CurrentSC = PlayerPrefs.GetInt("GameName.ShadowCascade", 0);
		this._showFPS = (PlayerPrefs.GetInt("GameName.ShowFPS", 0) == 1);
		this._volumen = PlayerPrefs.GetFloat("GameName.Volumen", 1f);
		float @float = PlayerPrefs.GetFloat("GameName.ShadowDistance", (float)this.DefaultShadowDistance);
		this.shadowProjection = (PlayerPrefs.GetInt("GameName.ShadowProjection", 0) == 1);
		this.PauseSound(PlayerPrefs.GetInt("GameName.PauseAudio", 0) == 1);
		this.useFullScreen = (PlayerPrefs.GetInt("GameName.ResolutionMode", 0) == 1);
		this._shadowEnable = AllOptionsKeyPro.IntToBool(PlayerPrefs.GetInt("GameName.ShadowEnable"));
		this._brightness = PlayerPrefs.GetFloat("GameName.Brightness", (float)this.DefaultBrightness);
		this._realtimeReflection = AllOptionsKeyPro.IntToBool(PlayerPrefs.GetInt("GameName.RealtimeReflection", 1));
		this._lodBias = PlayerPrefs.GetFloat("GameName.LoadBias", (float)this.DefaultLoadBias);
		this._hudScale = PlayerPrefs.GetFloat("GameName.HudScale", this._hudScale);
		this.SetBrightness(this._brightness);
		this.ShadowDistance(@float);
		this.ShadowDistanceSlider.value = @float;
		this.Volumen(this._volumen);
		this.VolumenSlider.value = this._volumen;
		this.ShadowProjectionType(this.shadowProjection);
		this.SetShadowEnable(this._shadowEnable);
		this.SetRealTimeReflection(this._realtimeReflection);
		this.SetLodBias(this._lodBias);
		this.SetHUDScale(this._hudScale);
		this.ApplyResolution();
		QualitySettings.shadowCascades = this.ShadowCascadeOptions[this.CurrentSC];
		this.ShadowCascadeText.text = this.ShadowCascadeNames[this.CurrentSC].ToUpper();
		this.QualityText.text = QualitySettings.names[this.CurrentQuality].ToUpper();
		QualitySettings.SetQualityLevel(this.CurrentQuality);
		this.FullScreenOnText.text = ((!this.useFullScreen) ? "OFF" : "ON");
		this.ShowFPSText.text = ((!this._showFPS) ? "OFF" : "ON");
		if (this.FPSObject != null)
		{
			foreach (GameObject gameObject in this.FPSObject)
			{
				gameObject.SetActive(this._showFPS);
			}
		}
		this.BrightnessSlider.value = this._brightness;
		this.LoadBiasSlider.value = this._lodBias;
		this.HUDScaleFactor.value = this._hudScale;
		int currentAS = this.CurrentAS;
		if (currentAS != 0)
		{
			if (currentAS != 1)
			{
				if (currentAS == 2)
				{
					QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
					this.AnisotropicText.text = AnisotropicFiltering.ForceEnable.ToString().ToUpper();
				}
			}
			else
			{
				QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
				this.AnisotropicText.text = AnisotropicFiltering.Enable.ToString().ToUpper();
			}
		}
		else
		{
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
			this.AnisotropicText.text = AnisotropicFiltering.Disable.ToString().ToUpper();
		}
		switch (this.CurrentAA)
		{
		case 0:
			QualitySettings.antiAliasing = 0;
			break;
		case 1:
			QualitySettings.antiAliasing = 2;
			break;
		case 2:
			QualitySettings.antiAliasing = 4;
			break;
		case 3:
			QualitySettings.antiAliasing = 8;
			break;
		}
		this.AntiAliasingText.text = this.AntiAliasingNames[this.CurrentAA].ToUpper();
		int currentVSC = this.CurrentVSC;
		if (currentVSC != 0)
		{
			if (currentVSC != 1)
			{
				if (currentVSC == 2)
				{
					QualitySettings.vSyncCount = 2;
				}
			}
			else
			{
				QualitySettings.vSyncCount = 1;
			}
		}
		else
		{
			QualitySettings.vSyncCount = 0;
		}
		this.vSyncText.text = this.VSyncNames[this.CurrentVSC].ToUpper();
		int currentBW = this.CurrentBW;
		if (currentBW != 0)
		{
			if (currentBW != 1)
			{
				if (currentBW == 2)
				{
					QualitySettings.blendWeights = BlendWeights.FourBones;
					this.blendWeightsText.text = BlendWeights.FourBones.ToString().ToUpper();
				}
			}
			else
			{
				QualitySettings.blendWeights = BlendWeights.TwoBones;
				this.blendWeightsText.text = BlendWeights.TwoBones.ToString().ToUpper();
			}
		}
		else
		{
			QualitySettings.blendWeights = BlendWeights.OneBone;
			this.blendWeightsText.text = BlendWeights.OneBone.ToString().ToUpper();
		}
		QualitySettings.masterTextureLimit = this.CurrentTL;
		this.TextureLimitText.text = this.TextureQualityNames[this.CurrentTL];
		this.ResolutionText.text = Screen.resolutions[this.CurrentRS].width + " X " + Screen.resolutions[this.CurrentRS].height;
		bool fullscreen = this.AutoApplyResolution && this.useFullScreen;
		Screen.SetResolution(Screen.resolutions[this.CurrentRS].width, Screen.resolutions[this.CurrentRS].height, fullscreen);
	}

	// Token: 0x06002551 RID: 9553 RVA: 0x000D5A4C File Offset: 0x000D3E4C
	private IEnumerator WaitForSwichet(int _id)
	{
		yield return base.StartCoroutine(bl_AllOptionsPro.WaitForRealSeconds(0.25f));
		for (int i = 0; i < this.Panels.Length; i++)
		{
			this.Panels[i].SetActive(false);
		}
		this.Panels[_id].SetActive(true);
		if (this.TitlePanelText != null)
		{
			this.TitlePanelText.text = this.Panels[_id].name.ToUpper();
		}
		yield break;
	}

	// Token: 0x06002552 RID: 9554 RVA: 0x000D5A70 File Offset: 0x000D3E70
	public static IEnumerator WaitForRealSeconds(float time)
	{
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + time)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06002553 RID: 9555 RVA: 0x000D5A8C File Offset: 0x000D3E8C
	private IEnumerator HideAnimate()
	{
		if (this.ContentAnim != null)
		{
			this.ContentAnim.SetBool("Show", false);
			yield return new WaitForSeconds(this.ContentAnim.GetCurrentAnimatorStateInfo(0).length);
			this.SettingsPanel.SetActive(false);
		}
		else
		{
			this.SettingsPanel.SetActive(false);
		}
		yield break;
	}

	// Token: 0x06002554 RID: 9556 RVA: 0x000D5AA8 File Offset: 0x000D3EA8
	public void SaveOptions()
	{
		PlayerPrefs.SetInt("GameName.AnisoTropic", this.CurrentAS);
		PlayerPrefs.SetInt("GameName.AntiAliasing", this.CurrentAA);
		PlayerPrefs.SetInt("GameName.BlendWeight", this.CurrentBW);
		PlayerPrefs.SetInt("GameName.QualityLevel", this.CurrentQuality);
		PlayerPrefs.SetInt("GameName.ResolutionScreen", this.CurrentRS);
		PlayerPrefs.SetInt("GameName.VSyncCount", this.CurrentVSC);
		PlayerPrefs.SetInt("GameName.AnisoTropic", this.CurrentAS);
		PlayerPrefs.SetInt("GameName.TextureLimit", this.CurrentTL);
		PlayerPrefs.SetInt("GameName.ShadowCascade", this.CurrentSC);
		PlayerPrefs.SetFloat("GameName.Volumen", this._volumen);
		PlayerPrefs.SetFloat("GameName.ShadowDistance", this.cacheShadowDistance);
		PlayerPrefs.SetInt("GameName.ShadowProjection", (!this.shadowProjection) ? 0 : 1);
		PlayerPrefs.SetInt("GameName.ShowFPS", (!this._showFPS) ? 0 : 1);
		PlayerPrefs.SetInt("GameName.PauseAudio", (!this._isPauseSound) ? 0 : 1);
		PlayerPrefs.SetInt("GameName.ResolutionMode", (!this.useFullScreen) ? 0 : 1);
		PlayerPrefs.SetInt("GameName.ShadowEnable", AllOptionsKeyPro.BoolToInt(this._shadowEnable));
		PlayerPrefs.SetFloat("GameName.Brightness", this._brightness);
		PlayerPrefs.SetInt("GameName.RealtimeReflection", AllOptionsKeyPro.BoolToInt(this._realtimeReflection));
		PlayerPrefs.SetFloat("GameName.LoadBias", this._lodBias);
		PlayerPrefs.SetFloat("GameName.HudScale", this._hudScale);
	}

	// Token: 0x040024D9 RID: 9433
	[Header("Panels")]
	[SerializeField]
	private GameObject[] Panels;

	// Token: 0x040024DA RID: 9434
	[SerializeField]
	private Button[] PanelButtons;

	// Token: 0x040024DB RID: 9435
	[SerializeField]
	private Animator PanelAnimator;

	// Token: 0x040024DC RID: 9436
	[Header("Settings")]
	public bool ApplyOnStart;

	// Token: 0x040024DD RID: 9437
	public bool AutoApplyResolution = true;

	// Token: 0x040024DE RID: 9438
	public bool SaveOnDisable = true;

	// Token: 0x040024DF RID: 9439
	public bool AnimateHidePanel = true;

	// Token: 0x040024E0 RID: 9440
	public int StartWindow;

	// Token: 0x040024E1 RID: 9441
	[SerializeField]
	[Range(0f, 8f)]
	private int DefaultQuality = 3;

	// Token: 0x040024E2 RID: 9442
	[SerializeField]
	[Range(0f, 15f)]
	private int DefaultResolution = 7;

	// Token: 0x040024E3 RID: 9443
	[SerializeField]
	[Range(0f, 3f)]
	private int DefaultAntiAliasing = 1;

	// Token: 0x040024E4 RID: 9444
	[SerializeField]
	[Range(0f, 2f)]
	private int DefaultAnisoTropic = 1;

	// Token: 0x040024E5 RID: 9445
	[SerializeField]
	[Range(0f, 2f)]
	private int DefaultVSync = 1;

	// Token: 0x040024E6 RID: 9446
	[SerializeField]
	[Range(0f, 2f)]
	private int DefaultBlendWeight = 1;

	// Token: 0x040024E7 RID: 9447
	[SerializeField]
	[Range(0f, 100f)]
	private int DefaultShadowDistance = 40;

	// Token: 0x040024E8 RID: 9448
	[SerializeField]
	[Range(0f, 1f)]
	private int DefaultBrightness = 1;

	// Token: 0x040024E9 RID: 9449
	[SerializeField]
	[Range(0.01f, 3f)]
	private int DefaultLoadBias = 1;

	// Token: 0x040024EA RID: 9450
	[Header("Options Name")]
	[SerializeField]
	private string[] AntiAliasingNames = new string[]
	{
		"X0",
		"X2",
		"X4",
		"X8"
	};

	// Token: 0x040024EB RID: 9451
	[SerializeField]
	private string[] VSyncNames = new string[]
	{
		"Don't Sync",
		"Every V Blank",
		"Every Second V Blank"
	};

	// Token: 0x040024EC RID: 9452
	[SerializeField]
	private string[] TextureQualityNames = new string[]
	{
		"FULL RES",
		"HALF RES",
		"QUARTER RES",
		"EIGHTH RES"
	};

	// Token: 0x040024ED RID: 9453
	[SerializeField]
	private string[] ShadowCascadeNames = new string[]
	{
		"NO CASCADES",
		"TWO CASCADES",
		"FOUR CASCADES"
	};

	// Token: 0x040024EE RID: 9454
	[Header("References")]
	[SerializeField]
	private GameObject SettingsPanel;

	// Token: 0x040024EF RID: 9455
	[SerializeField]
	private Animator ContentAnim;

	// Token: 0x040024F0 RID: 9456
	public Text QualityText;

	// Token: 0x040024F1 RID: 9457
	private int CurrentQuality;

	// Token: 0x040024F2 RID: 9458
	public Text AnisotropicText;

	// Token: 0x040024F3 RID: 9459
	private int CurrentAS;

	// Token: 0x040024F4 RID: 9460
	public Text AntiAliasingText;

	// Token: 0x040024F5 RID: 9461
	private int CurrentAA;

	// Token: 0x040024F6 RID: 9462
	public Text vSyncText;

	// Token: 0x040024F7 RID: 9463
	private int CurrentVSC;

	// Token: 0x040024F8 RID: 9464
	public Text blendWeightsText;

	// Token: 0x040024F9 RID: 9465
	private int CurrentBW;

	// Token: 0x040024FA RID: 9466
	public Text ResolutionText;

	// Token: 0x040024FB RID: 9467
	private int CurrentRS;

	// Token: 0x040024FC RID: 9468
	[SerializeField]
	private Text FullScreenOnText;

	// Token: 0x040024FD RID: 9469
	private bool useFullScreen;

	// Token: 0x040024FE RID: 9470
	[SerializeField]
	private Text TextureLimitText;

	// Token: 0x040024FF RID: 9471
	private int CurrentTL;

	// Token: 0x04002500 RID: 9472
	[SerializeField]
	private Text RealtimeReflectionText;

	// Token: 0x04002501 RID: 9473
	private bool _realtimeReflection;

	// Token: 0x04002502 RID: 9474
	[SerializeField]
	private Text LoadBiasText;

	// Token: 0x04002503 RID: 9475
	private float _lodBias;

	// Token: 0x04002504 RID: 9476
	[SerializeField]
	private Text ShadowCascadeText;

	// Token: 0x04002505 RID: 9477
	private int CurrentSC = 2;

	// Token: 0x04002506 RID: 9478
	private int[] ShadowCascadeOptions = new int[]
	{
		0,
		2,
		4
	};

	// Token: 0x04002507 RID: 9479
	[SerializeField]
	private Text ShowFPSText;

	// Token: 0x04002508 RID: 9480
	private bool _showFPS;

	// Token: 0x04002509 RID: 9481
	[SerializeField]
	private Text ShadowDistanceText;

	// Token: 0x0400250A RID: 9482
	[SerializeField]
	private Slider ShadowDistanceSlider;

	// Token: 0x0400250B RID: 9483
	private float cacheShadowDistance;

	// Token: 0x0400250C RID: 9484
	[SerializeField]
	private Slider BrightnessSlider;

	// Token: 0x0400250D RID: 9485
	[SerializeField]
	private Slider LoadBiasSlider;

	// Token: 0x0400250E RID: 9486
	[SerializeField]
	private Slider HUDScaleFactor;

	// Token: 0x0400250F RID: 9487
	[SerializeField]
	private Text HudScaleText;

	// Token: 0x04002510 RID: 9488
	[SerializeField]
	private Text BrightnessText;

	// Token: 0x04002511 RID: 9489
	private float _brightness;

	// Token: 0x04002512 RID: 9490
	[SerializeField]
	private Text ShadowProjectionText;

	// Token: 0x04002513 RID: 9491
	private bool shadowProjection;

	// Token: 0x04002514 RID: 9492
	[SerializeField]
	private Text ShadowEnebleText;

	// Token: 0x04002515 RID: 9493
	private bool _shadowEnable;

	// Token: 0x04002516 RID: 9494
	[SerializeField]
	private Text PauseText;

	// Token: 0x04002517 RID: 9495
	private bool _isPauseSound;

	// Token: 0x04002518 RID: 9496
	[SerializeField]
	private Text VolumenText;

	// Token: 0x04002519 RID: 9497
	[SerializeField]
	private Slider VolumenSlider;

	// Token: 0x0400251A RID: 9498
	[SerializeField]
	private Text TitlePanelText;

	// Token: 0x0400251B RID: 9499
	[SerializeField]
	private CanvasScaler HUDCanvas;

	// Token: 0x0400251C RID: 9500
	[SerializeField]
	private GameObject[] FPSObject;

	// Token: 0x0400251D RID: 9501
	private bl_BrightnessImage BrightnessImage;

	// Token: 0x0400251E RID: 9502
	private float _hudScale;

	// Token: 0x0400251F RID: 9503
	private bool Show;

	// Token: 0x04002520 RID: 9504
	private float _volumen;
}
