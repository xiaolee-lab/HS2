using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200061C RID: 1564
public class bl_ApplySettingsPro : MonoBehaviour
{
	// Token: 0x06002556 RID: 9558 RVA: 0x000D5F0E File Offset: 0x000D430E
	private void Start()
	{
		if (UnityEngine.Object.FindObjectOfType<bl_BrightnessImage>() != null)
		{
			this.BrightnessImage = UnityEngine.Object.FindObjectOfType<bl_BrightnessImage>();
		}
		this.LoadAndApply();
	}

	// Token: 0x06002557 RID: 9559 RVA: 0x000D5F31 File Offset: 0x000D4331
	public void ShadowProjectionType(bool b)
	{
		if (b)
		{
			QualitySettings.shadowProjection = ShadowProjection.StableFit;
		}
		else
		{
			QualitySettings.shadowProjection = ShadowProjection.CloseFit;
		}
	}

	// Token: 0x06002558 RID: 9560 RVA: 0x000D5F4C File Offset: 0x000D434C
	private void LoadAndApply()
	{
		int @int = PlayerPrefs.GetInt("GameName.AntiAliasing");
		int int2 = PlayerPrefs.GetInt("GameName.AnisoTropic");
		int int3 = PlayerPrefs.GetInt("GameName.BlendWeight");
		int int4 = PlayerPrefs.GetInt("GameName.QualityLevel");
		int int5 = PlayerPrefs.GetInt("GameName.ResolutionScreen");
		int int6 = PlayerPrefs.GetInt("GameName.VSyncCount");
		int int7 = PlayerPrefs.GetInt("GameName.TextureLimit", 0);
		int int8 = PlayerPrefs.GetInt("GameName.ShadowCascade", 0);
		bool active = PlayerPrefs.GetInt("GameName.ShowFPS", 0) == 1;
		float @float = PlayerPrefs.GetFloat("GameName.Volumen", 1f);
		float float2 = PlayerPrefs.GetFloat("GameName.ShadowDistance");
		bool b = PlayerPrefs.GetInt("GameName.ShadowProjection", 0) == 1;
		bool flag = AllOptionsKeyPro.IntToBool(PlayerPrefs.GetInt("GameName.ShadowEnable"));
		float float3 = PlayerPrefs.GetFloat("GameName.Brightness", 1f);
		bool realtimeReflectionProbes = AllOptionsKeyPro.IntToBool(PlayerPrefs.GetInt("GameName.RealtimeReflection", 1));
		float float4 = PlayerPrefs.GetFloat("GameName.LoadBias", 1f);
		float float5 = PlayerPrefs.GetFloat("GameName.HudScale", 0f);
		QualitySettings.shadowDistance = float2;
		AudioListener.volume = @float;
		AudioListener.pause = (PlayerPrefs.GetInt("GameName.PauseAudio", 0) == 1);
		this.ShadowProjectionType(b);
		QualitySettings.masterTextureLimit = int7;
		QualitySettings.shadowCascades = this.ShadowCascadeOptions[int8];
		QualitySettings.SetQualityLevel(int4);
		QualitySettings.realtimeReflectionProbes = realtimeReflectionProbes;
		QualitySettings.shadowDistance = ((!flag) ? 0f : float2);
		QualitySettings.lodBias = float4;
		if (this.BrightnessImage != null)
		{
			this.BrightnessImage.SetValue(float3);
		}
		if (this.HUDCanvas != null)
		{
			this.HUDCanvas.matchWidthOrHeight = 1f - float5;
		}
		if (this.FPSObject != null)
		{
			foreach (GameObject gameObject in this.FPSObject)
			{
				gameObject.SetActive(active);
			}
		}
		if (int2 != 0)
		{
			if (int2 != 1)
			{
				if (int2 == 2)
				{
					QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
				}
			}
			else
			{
				QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
			}
		}
		else
		{
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
		}
		switch (@int)
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
		if (int6 != 0)
		{
			if (int6 != 1)
			{
				if (int6 == 2)
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
		if (int3 != 0)
		{
			if (int3 != 1)
			{
				if (int3 == 2)
				{
					QualitySettings.blendWeights = BlendWeights.FourBones;
				}
			}
			else
			{
				QualitySettings.blendWeights = BlendWeights.TwoBones;
			}
		}
		else
		{
			QualitySettings.blendWeights = BlendWeights.OneBone;
		}
		Screen.SetResolution(Screen.resolutions[int5].width, Screen.resolutions[int5].height, false);
	}

	// Token: 0x04002521 RID: 9505
	[SerializeField]
	private CanvasScaler HUDCanvas;

	// Token: 0x04002522 RID: 9506
	[SerializeField]
	private GameObject[] FPSObject;

	// Token: 0x04002523 RID: 9507
	private int[] ShadowCascadeOptions = new int[]
	{
		0,
		2,
		4
	};

	// Token: 0x04002524 RID: 9508
	private bl_BrightnessImage BrightnessImage;
}
