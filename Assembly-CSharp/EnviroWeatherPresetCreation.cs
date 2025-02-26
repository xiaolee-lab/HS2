using System;
using UnityEngine;

// Token: 0x02000365 RID: 869
public class EnviroWeatherPresetCreation
{
	// Token: 0x06000F5C RID: 3932 RVA: 0x000540D0 File Offset: 0x000524D0
	public static GameObject GetAssetPrefab(string name)
	{
		return null;
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x000540D3 File Offset: 0x000524D3
	public static Cubemap GetAssetCubemap(string name)
	{
		return null;
	}

	// Token: 0x06000F5E RID: 3934 RVA: 0x000540D6 File Offset: 0x000524D6
	public static Texture GetAssetTexture(string name)
	{
		return null;
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x000540DC File Offset: 0x000524DC
	public static Gradient CreateGradient()
	{
		Gradient gradient = new Gradient();
		GradientColorKey[] array = new GradientColorKey[2];
		GradientAlphaKey[] array2 = new GradientAlphaKey[2];
		array[0].color = Color.white;
		array[0].time = 0f;
		array[1].color = Color.white;
		array[1].time = 0f;
		array2[0].alpha = 0f;
		array2[0].time = 0f;
		array2[1].alpha = 0f;
		array2[1].time = 1f;
		gradient.SetKeys(array, array2);
		return gradient;
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x00054190 File Offset: 0x00052590
	public static Color GetColor(string hex)
	{
		Color result = default(Color);
		ColorUtility.TryParseHtmlString(hex, out result);
		return result;
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x000541B0 File Offset: 0x000525B0
	public static Keyframe CreateKey(float value, float time)
	{
		return new Keyframe
		{
			value = value,
			time = time
		};
	}

	// Token: 0x06000F62 RID: 3938 RVA: 0x000541D8 File Offset: 0x000525D8
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
