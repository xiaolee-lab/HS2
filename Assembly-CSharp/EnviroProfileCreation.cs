using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000341 RID: 833
public static class EnviroProfileCreation
{
	// Token: 0x06000E5D RID: 3677 RVA: 0x00045F98 File Offset: 0x00044398
	public static void SetupDefaults(EnviroProfile profile)
	{
		List<Color> list = new List<Color>();
		List<float> list2 = new List<float>();
		list.Add(EnviroProfileCreation.GetColor("#4C5570"));
		list2.Add(0f);
		list.Add(EnviroProfileCreation.GetColor("#4C5570"));
		list2.Add(0.46f);
		list.Add(EnviroProfileCreation.GetColor("#C98842"));
		list2.Add(0.51f);
		list.Add(EnviroProfileCreation.GetColor("#EAC8A4"));
		list2.Add(0.56f);
		list.Add(EnviroProfileCreation.GetColor("#EADCCE"));
		list2.Add(1f);
		profile.lightSettings.LightColor = EnviroProfileCreation.CreateGradient(list, list2);
		list = new List<Color>();
		list2 = new List<float>();
		profile.lightSettings.directLightSunIntensity.AddKey(EnviroProfileCreation.CreateKey(0f, 0f));
		profile.lightSettings.directLightSunIntensity.AddKey(EnviroProfileCreation.CreateKey(0f, 0.42f));
		profile.lightSettings.directLightSunIntensity.AddKey(EnviroProfileCreation.CreateKey(0.75f, 0.5f, 5f, 5f));
		profile.lightSettings.directLightSunIntensity.AddKey(EnviroProfileCreation.CreateKey(1.5f, 1f));
		profile.lightSettings.directLightMoonIntensity.AddKey(EnviroProfileCreation.CreateKey(0.01f, 0f));
		profile.lightSettings.directLightMoonIntensity.AddKey(EnviroProfileCreation.CreateKey(0.01f, 0.42f));
		profile.lightSettings.directLightMoonIntensity.AddKey(EnviroProfileCreation.CreateKey(0.6f, 0.5f, 2f, 2f));
		profile.lightSettings.directLightMoonIntensity.AddKey(EnviroProfileCreation.CreateKey(1f, 1f));
		profile.lightSettings.shadowIntensity.AddKey(EnviroProfileCreation.CreateKey(1f, 0f));
		profile.lightSettings.shadowIntensity.AddKey(EnviroProfileCreation.CreateKey(1f, 1f));
		profile.lightSettings.ambientIntensity.AddKey(EnviroProfileCreation.CreateKey(0.75f, 0f));
		profile.lightSettings.ambientIntensity.AddKey(EnviroProfileCreation.CreateKey(0.75f, 1f));
		list.Add(EnviroProfileCreation.GetColor("#4C5570"));
		list2.Add(0f);
		list.Add(EnviroProfileCreation.GetColor("#4C5570"));
		list2.Add(0.46f);
		list.Add(EnviroProfileCreation.GetColor("#C98842"));
		list2.Add(0.51f);
		list.Add(EnviroProfileCreation.GetColor("#99B2C3"));
		list2.Add(0.57f);
		list.Add(EnviroProfileCreation.GetColor("#99B2C3"));
		list2.Add(1f);
		profile.lightSettings.ambientSkyColor = EnviroProfileCreation.CreateGradient(list, list2);
		list = new List<Color>();
		list2 = new List<float>();
		profile.lightSettings.ambientEquatorColor = EnviroProfileCreation.CreateGradient(EnviroProfileCreation.GetColor("#2E3344"), 0f, EnviroProfileCreation.GetColor("#414852"), 1f);
		profile.lightSettings.ambientGroundColor = EnviroProfileCreation.CreateGradient(EnviroProfileCreation.GetColor("#272B39"), 0f, EnviroProfileCreation.GetColor("#3E3631"), 1f);
		profile.skySettings.scatteringCurve.AddKey(EnviroProfileCreation.CreateKey(-25f, 0f));
		profile.skySettings.scatteringCurve.AddKey(EnviroProfileCreation.CreateKey(-10f, 0.5f, 55f, 55f));
		profile.skySettings.scatteringCurve.AddKey(EnviroProfileCreation.CreateKey(6.5f, 0.52f, 35f, 35f));
		profile.skySettings.scatteringCurve.AddKey(EnviroProfileCreation.CreateKey(11f, 1f));
		list.Add(EnviroProfileCreation.GetColor("#8492C8"));
		list2.Add(0f);
		list.Add(EnviroProfileCreation.GetColor("#8492C8"));
		list2.Add(0.45f);
		list.Add(EnviroProfileCreation.GetColor("#FFB69C"));
		list2.Add(0.527f);
		list.Add(EnviroProfileCreation.GetColor("#D2D2D2"));
		list2.Add(0.75f);
		list.Add(EnviroProfileCreation.GetColor("#D2D2D2"));
		list2.Add(1f);
		profile.skySettings.scatteringColor = EnviroProfileCreation.CreateGradient(list, list2);
		list = new List<Color>();
		list2 = new List<float>();
		list.Add(EnviroProfileCreation.GetColor("#0A0300"));
		list2.Add(0f);
		list.Add(EnviroProfileCreation.GetColor("#FF6211"));
		list2.Add(0.45f);
		list.Add(EnviroProfileCreation.GetColor("#FF6917"));
		list2.Add(0.55f);
		list.Add(EnviroProfileCreation.GetColor("#FFE2CB"));
		list2.Add(0.75f);
		list.Add(EnviroProfileCreation.GetColor("#FFFFFF"));
		list2.Add(1f);
		profile.skySettings.sunDiskColor = EnviroProfileCreation.CreateGradient(list, list2);
		list = new List<Color>();
		list2 = new List<float>();
		profile.skySettings.moonGlow.AddKey(EnviroProfileCreation.CreateKey(1f, 0f));
		profile.skySettings.moonGlow.AddKey(EnviroProfileCreation.CreateKey(0f, 0.65f));
		profile.skySettings.moonGlow.AddKey(EnviroProfileCreation.CreateKey(0f, 1f));
		profile.skySettings.skyLuminence.AddKey(EnviroProfileCreation.CreateKey(0f, 0f));
		profile.skySettings.skyLuminence.AddKey(EnviroProfileCreation.CreateKey(0.15f, 0.5f));
		profile.skySettings.skyLuminence.AddKey(EnviroProfileCreation.CreateKey(0.105f, 0.62f));
		profile.skySettings.skyLuminence.AddKey(EnviroProfileCreation.CreateKey(0.1f, 1f));
		profile.skySettings.skyColorPower.AddKey(EnviroProfileCreation.CreateKey(1.5f, 0f));
		profile.skySettings.skyColorPower.AddKey(EnviroProfileCreation.CreateKey(1.25f, 1f));
		profile.skySettings.starsIntensity.AddKey(EnviroProfileCreation.CreateKey(0.3f, 0f));
		profile.skySettings.starsIntensity.AddKey(EnviroProfileCreation.CreateKey(0.015f, 0.5f));
		profile.skySettings.starsIntensity.AddKey(EnviroProfileCreation.CreateKey(0f, 0.6f));
		profile.skySettings.starsIntensity.AddKey(EnviroProfileCreation.CreateKey(0f, 1f));
		profile.skySettings.moonTexture = EnviroProfileCreation.GetAssetTexture("tex_enviro_moon");
		profile.skySettings.starsCubeMap = EnviroProfileCreation.GetAssetCubemap("cube_enviro_stars");
		profile.skySettings.galaxyIntensity.AddKey(EnviroProfileCreation.CreateKey(1f, 0f));
		profile.skySettings.galaxyIntensity.AddKey(EnviroProfileCreation.CreateKey(0.015f, 0.5f));
		profile.skySettings.galaxyIntensity.AddKey(EnviroProfileCreation.CreateKey(0f, 0.6f));
		profile.skySettings.galaxyIntensity.AddKey(EnviroProfileCreation.CreateKey(0f, 1f));
		profile.skySettings.galaxyCubeMap = EnviroProfileCreation.GetAssetCubemap("cube_enviro_galaxy");
		Texture assetTexture = EnviroProfileCreation.GetAssetTexture("tex_enviro_noise");
		Texture assetTexture2 = EnviroProfileCreation.GetAssetTexture("tex_enviro_cirrus");
		if (assetTexture == null || assetTexture2 == null)
		{
		}
		profile.cloudsSettings.flatCloudsNoiseTexture = assetTexture;
		profile.cloudsSettings.cirrusCloudsTexture = assetTexture2;
		profile.cloudsSettings.volumeCloudsMoonColor = EnviroProfileCreation.CreateGradient(EnviroProfileCreation.GetColor("#232228"), 0f, EnviroProfileCreation.GetColor("#B6BCDC"), 1f);
		list.Add(EnviroProfileCreation.GetColor("#17171A"));
		list2.Add(0f);
		list.Add(EnviroProfileCreation.GetColor("#17171A"));
		list2.Add(0.455f);
		list.Add(EnviroProfileCreation.GetColor("#3D3D3B"));
		list2.Add(0.48f);
		list.Add(EnviroProfileCreation.GetColor("#EEB279"));
		list2.Add(0.53f);
		list.Add(EnviroProfileCreation.GetColor("#EEF0FF"));
		list2.Add(0.6f);
		list.Add(EnviroProfileCreation.GetColor("#ECEEFF"));
		list2.Add(1f);
		profile.cloudsSettings.cirrusCloudsColor = EnviroProfileCreation.CreateGradient(list, list2);
		list = new List<Color>();
		list2 = new List<float>();
		list.Add(EnviroProfileCreation.GetColor("#17171A"));
		list2.Add(0f);
		list.Add(EnviroProfileCreation.GetColor("#17171A"));
		list2.Add(0.455f);
		list.Add(EnviroProfileCreation.GetColor("#3D3D3B"));
		list2.Add(0.48f);
		list.Add(EnviroProfileCreation.GetColor("#EEB279"));
		list2.Add(0.53f);
		list.Add(EnviroProfileCreation.GetColor("#EEF0FF"));
		list2.Add(0.6f);
		list.Add(EnviroProfileCreation.GetColor("#ECEEFF"));
		list2.Add(1f);
		profile.cloudsSettings.flatCloudsColor = EnviroProfileCreation.CreateGradient(list, list2);
		list = new List<Color>();
		list2 = new List<float>();
		list.Add(EnviroProfileCreation.GetColor("#17171A"));
		list2.Add(0f);
		list.Add(EnviroProfileCreation.GetColor("#17171A"));
		list2.Add(0.455f);
		list.Add(EnviroProfileCreation.GetColor("#3D3D3B"));
		list2.Add(0.48f);
		list.Add(EnviroProfileCreation.GetColor("#EEB279"));
		list2.Add(0.53f);
		list.Add(EnviroProfileCreation.GetColor("#CECECE"));
		list2.Add(0.58f);
		list.Add(EnviroProfileCreation.GetColor("#CECECE"));
		list2.Add(1f);
		profile.cloudsSettings.volumeCloudsColor = EnviroProfileCreation.CreateGradient(list, list2);
		list = new List<Color>();
		list2 = new List<float>();
		profile.cloudsSettings.directLightIntensity = new AnimationCurve();
		profile.cloudsSettings.directLightIntensity.AddKey(EnviroProfileCreation.CreateKey(0.02f, 0f));
		profile.cloudsSettings.directLightIntensity.AddKey(EnviroProfileCreation.CreateKey(0.15f, 0.495f));
		profile.cloudsSettings.directLightIntensity.AddKey(EnviroProfileCreation.CreateKey(0.15f, 1f));
		profile.cloudsSettings.ambientLightIntensity = new AnimationCurve();
		profile.cloudsSettings.ambientLightIntensity.AddKey(EnviroProfileCreation.CreateKey(0.017f, 0f));
		profile.cloudsSettings.ambientLightIntensity.AddKey(EnviroProfileCreation.CreateKey(0f, 0.46f));
		profile.cloudsSettings.ambientLightIntensity.AddKey(EnviroProfileCreation.CreateKey(0.35f, 0.617f));
		profile.cloudsSettings.ambientLightIntensity.AddKey(EnviroProfileCreation.CreateKey(0.32f, 1f));
		list.Add(EnviroProfileCreation.GetColor("#FF703C"));
		list2.Add(0f);
		list.Add(EnviroProfileCreation.GetColor("#FF5D00"));
		list2.Add(0.47f);
		list.Add(EnviroProfileCreation.GetColor("#FFF4DF"));
		list2.Add(0.65f);
		list.Add(EnviroProfileCreation.GetColor("#FFFFFF"));
		list2.Add(1f);
		profile.lightshaftsSettings.lightShaftsColorSun = EnviroProfileCreation.CreateGradient(list, list2);
		list = new List<Color>();
		list2 = new List<float>();
		profile.lightshaftsSettings.lightShaftsColorMoon = EnviroProfileCreation.CreateGradient(EnviroProfileCreation.GetColor("#94A8E5"), 0f, EnviroProfileCreation.GetColor("#94A8E5"), 1f);
		list.Add(EnviroProfileCreation.GetColor("#1D1D1D"));
		list2.Add(0f);
		list.Add(EnviroProfileCreation.GetColor("#1D1D1D"));
		list2.Add(0.43f);
		list.Add(EnviroProfileCreation.GetColor("#A6A6A6"));
		list2.Add(0.54f);
		list.Add(EnviroProfileCreation.GetColor("#D0D0D0"));
		list2.Add(0.65f);
		list.Add(EnviroProfileCreation.GetColor("#C3C3C3"));
		list2.Add(1f);
		profile.lightshaftsSettings.thresholdColorSun = EnviroProfileCreation.CreateGradient(list, list2);
		list = new List<Color>();
		list2 = new List<float>();
		profile.lightshaftsSettings.thresholdColorMoon = EnviroProfileCreation.CreateGradient(EnviroProfileCreation.GetColor("#0B0B0B"), 0f, EnviroProfileCreation.GetColor("#000000"), 1f);
		profile.weatherSettings.lightningEffect = EnviroProfileCreation.GetAssetPrefab("Enviro_Lightning_Strike");
		for (int i = 0; i < 8; i++)
		{
			profile.audioSettings.ThunderSFX.Add(EnviroProfileCreation.GetAudioClip("SFX_Thunder_" + (i + 1)));
		}
		profile.weatherSettings.lightningEffect = EnviroProfileCreation.GetAssetPrefab("Enviro_Lightning_Strike");
	}

	// Token: 0x06000E5E RID: 3678 RVA: 0x00046CD0 File Offset: 0x000450D0
	public static bool UpdateProfile(EnviroProfile profile, string fromV, string toV)
	{
		if (profile == null)
		{
			return false;
		}
		if ((fromV == "1.9.0" || fromV == "1.9.1") && toV == "2.0.5")
		{
			profile.lightSettings.directLightSunIntensity = new AnimationCurve();
			profile.lightSettings.directLightSunIntensity.AddKey(EnviroProfileCreation.CreateKey(0f, 0f));
			profile.lightSettings.directLightSunIntensity.AddKey(EnviroProfileCreation.CreateKey(0f, 0.42f));
			profile.lightSettings.directLightSunIntensity.AddKey(EnviroProfileCreation.CreateKey(0.75f, 0.5f, 5f, 5f));
			profile.lightSettings.directLightSunIntensity.AddKey(EnviroProfileCreation.CreateKey(1.5f, 1f));
			profile.lightSettings.directLightMoonIntensity = new AnimationCurve();
			profile.lightSettings.directLightMoonIntensity.AddKey(EnviroProfileCreation.CreateKey(0.01f, 0f));
			profile.lightSettings.directLightMoonIntensity.AddKey(EnviroProfileCreation.CreateKey(0.01f, 0.42f));
			profile.lightSettings.directLightMoonIntensity.AddKey(EnviroProfileCreation.CreateKey(0.6f, 0.5f, 2f, 2f));
			profile.lightSettings.directLightMoonIntensity.AddKey(EnviroProfileCreation.CreateKey(1f, 1f));
			profile.lightSettings.shadowIntensity = new AnimationCurve();
			profile.lightSettings.shadowIntensity.AddKey(EnviroProfileCreation.CreateKey(1f, 0f));
			profile.lightSettings.shadowIntensity.AddKey(EnviroProfileCreation.CreateKey(1f, 1f));
			profile.cloudsSettings.directLightIntensity = new AnimationCurve();
			profile.cloudsSettings.directLightIntensity.AddKey(EnviroProfileCreation.CreateKey(0.02f, 0f));
			profile.cloudsSettings.directLightIntensity.AddKey(EnviroProfileCreation.CreateKey(0.15f, 0.495f));
			profile.cloudsSettings.directLightIntensity.AddKey(EnviroProfileCreation.CreateKey(0.15f, 1f));
			profile.cloudsSettings.ambientLightIntensity = new AnimationCurve();
			profile.cloudsSettings.ambientLightIntensity.AddKey(EnviroProfileCreation.CreateKey(0.017f, 0f));
			profile.cloudsSettings.ambientLightIntensity.AddKey(EnviroProfileCreation.CreateKey(0f, 0.46f));
			profile.cloudsSettings.ambientLightIntensity.AddKey(EnviroProfileCreation.CreateKey(0.35f, 0.617f));
			profile.cloudsSettings.ambientLightIntensity.AddKey(EnviroProfileCreation.CreateKey(0.32f, 1f));
			profile.skySettings.moonColor = EnviroProfileCreation.GetColor("#9C9D9EFF");
			profile.skySettings.moonGlowColor = EnviroProfileCreation.GetColor("#4D4D4DFF");
			List<Color> list = new List<Color>();
			List<float> list2 = new List<float>();
			list.Add(EnviroProfileCreation.GetColor("#17171A"));
			list2.Add(0f);
			list.Add(EnviroProfileCreation.GetColor("#17171A"));
			list2.Add(0.455f);
			list.Add(EnviroProfileCreation.GetColor("#3D3D3B"));
			list2.Add(0.48f);
			list.Add(EnviroProfileCreation.GetColor("#EEB279"));
			list2.Add(0.53f);
			list.Add(EnviroProfileCreation.GetColor("#EEF0FF"));
			list2.Add(0.6f);
			list.Add(EnviroProfileCreation.GetColor("#ECEEFF"));
			list2.Add(1f);
			profile.cloudsSettings.cirrusCloudsColor = EnviroProfileCreation.CreateGradient(list, list2);
			list = new List<Color>();
			list2 = new List<float>();
			list.Add(EnviroProfileCreation.GetColor("#17171A"));
			list2.Add(0f);
			list.Add(EnviroProfileCreation.GetColor("#17171A"));
			list2.Add(0.455f);
			list.Add(EnviroProfileCreation.GetColor("#3D3D3B"));
			list2.Add(0.48f);
			list.Add(EnviroProfileCreation.GetColor("#EEB279"));
			list2.Add(0.53f);
			list.Add(EnviroProfileCreation.GetColor("#EEF0FF"));
			list2.Add(0.6f);
			list.Add(EnviroProfileCreation.GetColor("#ECEEFF"));
			list2.Add(1f);
			profile.cloudsSettings.flatCloudsColor = EnviroProfileCreation.CreateGradient(list, list2);
			list = new List<Color>();
			list2 = new List<float>();
			list.Add(EnviroProfileCreation.GetColor("#17171A"));
			list2.Add(0f);
			list.Add(EnviroProfileCreation.GetColor("#17171A"));
			list2.Add(0.455f);
			list.Add(EnviroProfileCreation.GetColor("#3D3D3B"));
			list2.Add(0.48f);
			list.Add(EnviroProfileCreation.GetColor("#EEB279"));
			list2.Add(0.53f);
			list.Add(EnviroProfileCreation.GetColor("#CECECE"));
			list2.Add(0.58f);
			list.Add(EnviroProfileCreation.GetColor("#CECECE"));
			list2.Add(1f);
			profile.cloudsSettings.volumeCloudsColor = EnviroProfileCreation.CreateGradient(list, list2);
			profile.cloudsSettings.volumeCloudsMoonColor = EnviroProfileCreation.CreateGradient(EnviroProfileCreation.GetColor("#232228"), 0f, EnviroProfileCreation.GetColor("#B6BCDC"), 1f);
			Texture assetTexture = EnviroProfileCreation.GetAssetTexture("tex_enviro_noise");
			Texture assetTexture2 = EnviroProfileCreation.GetAssetTexture("tex_enviro_cirrus");
			if (assetTexture == null || assetTexture2 == null)
			{
			}
			profile.cloudsSettings.flatCloudsNoiseTexture = assetTexture;
			profile.cloudsSettings.cirrusCloudsTexture = assetTexture2;
			profile.skySettings.moonTexture = EnviroProfileCreation.GetAssetTexture("tex_enviro_moon");
			profile.skySettings.moonBrightness = 1f;
			profile.skySettings.galaxyIntensity = new AnimationCurve();
			profile.skySettings.galaxyIntensity.AddKey(EnviroProfileCreation.CreateKey(0.4f, 0f));
			profile.skySettings.galaxyIntensity.AddKey(EnviroProfileCreation.CreateKey(0.015f, 0.5f));
			profile.skySettings.galaxyIntensity.AddKey(EnviroProfileCreation.CreateKey(0f, 0.6f));
			profile.skySettings.galaxyIntensity.AddKey(EnviroProfileCreation.CreateKey(0f, 1f));
			profile.skySettings.galaxyCubeMap = EnviroProfileCreation.GetAssetCubemap("cube_enviro_galaxy");
			profile.weatherSettings.lightningEffect = EnviroProfileCreation.GetAssetPrefab("Enviro_Lightning_Strike");
			profile.version = toV;
			return true;
		}
		if ((fromV == "2.0.0" || fromV == "2.0.1" || fromV == "2.0.2") && toV == "2.0.5")
		{
			profile.skySettings.galaxyIntensity = new AnimationCurve();
			profile.skySettings.galaxyIntensity.AddKey(EnviroProfileCreation.CreateKey(0.4f, 0f));
			profile.skySettings.galaxyIntensity.AddKey(EnviroProfileCreation.CreateKey(0.015f, 0.5f));
			profile.skySettings.galaxyIntensity.AddKey(EnviroProfileCreation.CreateKey(0f, 0.6f));
			profile.skySettings.galaxyIntensity.AddKey(EnviroProfileCreation.CreateKey(0f, 1f));
			profile.skySettings.galaxyCubeMap = EnviroProfileCreation.GetAssetCubemap("cube_enviro_galaxy");
			profile.weatherSettings.lightningEffect = EnviroProfileCreation.GetAssetPrefab("Enviro_Lightning_Strike");
			profile.version = toV;
			return true;
		}
		if (fromV == "2.0.3" && toV == "2.0.5")
		{
			profile.weatherSettings.lightningEffect = EnviroProfileCreation.GetAssetPrefab("Enviro_Lightning_Strike");
			profile.version = toV;
			return true;
		}
		if (fromV == "2.0.4" && toV == "2.0.5")
		{
			profile.version = toV;
			return true;
		}
		return false;
	}

	// Token: 0x06000E5F RID: 3679 RVA: 0x000474D0 File Offset: 0x000458D0
	public static GameObject GetAssetPrefab(string name)
	{
		return null;
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x000474D3 File Offset: 0x000458D3
	public static AudioClip GetAudioClip(string name)
	{
		return null;
	}

	// Token: 0x06000E61 RID: 3681 RVA: 0x000474D6 File Offset: 0x000458D6
	public static Cubemap GetAssetCubemap(string name)
	{
		return null;
	}

	// Token: 0x06000E62 RID: 3682 RVA: 0x000474D9 File Offset: 0x000458D9
	public static Texture GetAssetTexture(string name)
	{
		return null;
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x000474DC File Offset: 0x000458DC
	public static Gradient CreateGradient(Color clr1, float time1, Color clr2, float time2)
	{
		Gradient gradient = new Gradient();
		GradientColorKey[] array = new GradientColorKey[2];
		GradientAlphaKey[] array2 = new GradientAlphaKey[2];
		array[0].color = clr1;
		array[0].time = time1;
		array[1].color = clr2;
		array[1].time = time2;
		array2[0].alpha = 1f;
		array2[0].time = 0f;
		array2[1].alpha = 1f;
		array2[1].time = 1f;
		gradient.SetKeys(array, array2);
		return gradient;
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x00047580 File Offset: 0x00045980
	public static Gradient CreateGradient(List<Color> clrs, List<float> times)
	{
		Gradient gradient = new Gradient();
		GradientColorKey[] array = new GradientColorKey[clrs.Count];
		GradientAlphaKey[] array2 = new GradientAlphaKey[2];
		for (int i = 0; i < clrs.Count; i++)
		{
			array[i].color = clrs[i];
			array[i].time = times[i];
		}
		array2[0].alpha = 1f;
		array2[0].time = 0f;
		array2[1].alpha = 1f;
		array2[1].time = 1f;
		gradient.SetKeys(array, array2);
		return gradient;
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x00047630 File Offset: 0x00045A30
	public static Color GetColor(string hex)
	{
		Color result = default(Color);
		ColorUtility.TryParseHtmlString(hex, out result);
		return result;
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x00047650 File Offset: 0x00045A50
	public static Keyframe CreateKey(float value, float time)
	{
		return new Keyframe
		{
			value = value,
			time = time
		};
	}

	// Token: 0x06000E67 RID: 3687 RVA: 0x00047678 File Offset: 0x00045A78
	public static Keyframe CreateKey(float value, float time, float inTangent, float outTangent)
	{
		return new Keyframe
		{
			value = value,
			time = time,
			inTangent = inTangent,
			outTangent = outTangent
		};
	}
}
