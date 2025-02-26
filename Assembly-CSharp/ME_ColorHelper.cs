using System;
using UnityEngine;

// Token: 0x0200042D RID: 1069
public static class ME_ColorHelper
{
	// Token: 0x0600137F RID: 4991 RVA: 0x000782F4 File Offset: 0x000766F4
	public static ME_ColorHelper.HSBColor ColorToHSV(Color color)
	{
		ME_ColorHelper.HSBColor result = new ME_ColorHelper.HSBColor(0f, 0f, 0f, color.a);
		float r = color.r;
		float g = color.g;
		float b = color.b;
		float num = Mathf.Max(r, Mathf.Max(g, b));
		if (num <= 0f)
		{
			return result;
		}
		float num2 = Mathf.Min(r, Mathf.Min(g, b));
		float num3 = num - num2;
		if (num > num2)
		{
			if (Math.Abs(g - num) < 0.0001f)
			{
				result.H = (b - r) / num3 * 60f + 120f;
			}
			else if (Math.Abs(b - num) < 0.0001f)
			{
				result.H = (r - g) / num3 * 60f + 240f;
			}
			else if (b > g)
			{
				result.H = (g - b) / num3 * 60f + 360f;
			}
			else
			{
				result.H = (g - b) / num3 * 60f;
			}
			if (result.H < 0f)
			{
				result.H += 360f;
			}
		}
		else
		{
			result.H = 0f;
		}
		result.H *= 0.0027777778f;
		result.S = num3 / num * 1f;
		result.B = num;
		return result;
	}

	// Token: 0x06001380 RID: 4992 RVA: 0x00078474 File Offset: 0x00076874
	public static Color HSVToColor(ME_ColorHelper.HSBColor hsbColor)
	{
		float value = hsbColor.B;
		float value2 = hsbColor.B;
		float value3 = hsbColor.B;
		if (Math.Abs(hsbColor.S) > 0.0001f)
		{
			float b = hsbColor.B;
			float num = hsbColor.B * hsbColor.S;
			float num2 = hsbColor.B - num;
			float num3 = hsbColor.H * 360f;
			if (num3 < 60f)
			{
				value = b;
				value2 = num3 * num / 60f + num2;
				value3 = num2;
			}
			else if (num3 < 120f)
			{
				value = -(num3 - 120f) * num / 60f + num2;
				value2 = b;
				value3 = num2;
			}
			else if (num3 < 180f)
			{
				value = num2;
				value2 = b;
				value3 = (num3 - 120f) * num / 60f + num2;
			}
			else if (num3 < 240f)
			{
				value = num2;
				value2 = -(num3 - 240f) * num / 60f + num2;
				value3 = b;
			}
			else if (num3 < 300f)
			{
				value = (num3 - 240f) * num / 60f + num2;
				value2 = num2;
				value3 = b;
			}
			else if (num3 <= 360f)
			{
				value = b;
				value2 = num2;
				value3 = -(num3 - 360f) * num / 60f + num2;
			}
			else
			{
				value = 0f;
				value2 = 0f;
				value3 = 0f;
			}
		}
		return new Color(Mathf.Clamp01(value), Mathf.Clamp01(value2), Mathf.Clamp01(value3), hsbColor.A);
	}

	// Token: 0x06001381 RID: 4993 RVA: 0x00078614 File Offset: 0x00076A14
	public static Color ConvertRGBColorByHUE(Color rgbColor, float hue)
	{
		float num = ME_ColorHelper.ColorToHSV(rgbColor).B;
		if (num < 0.0001f)
		{
			num = 0.0001f;
		}
		ME_ColorHelper.HSBColor hsbColor = ME_ColorHelper.ColorToHSV(rgbColor / num);
		hsbColor.H = hue;
		Color result = ME_ColorHelper.HSVToColor(hsbColor) * num;
		result.a = rgbColor.a;
		return result;
	}

	// Token: 0x06001382 RID: 4994 RVA: 0x00078674 File Offset: 0x00076A74
	public static void ChangeObjectColorByHUE(GameObject go, float hue)
	{
		Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>(true);
		foreach (Renderer renderer in componentsInChildren)
		{
			Material[] array2;
			if (!Application.isPlaying)
			{
				array2 = renderer.sharedMaterials;
			}
			else
			{
				array2 = renderer.materials;
			}
			if (array2.Length != 0)
			{
				foreach (string name in ME_ColorHelper.colorProperties)
				{
					foreach (Material material in array2)
					{
						if (material != null && material.HasProperty(name))
						{
							ME_ColorHelper.setMatHUEColor(material, name, hue);
						}
					}
				}
			}
		}
		ParticleSystemRenderer[] componentsInChildren2 = go.GetComponentsInChildren<ParticleSystemRenderer>(true);
		foreach (ParticleSystemRenderer particleSystemRenderer in componentsInChildren2)
		{
			Material material2 = particleSystemRenderer.trailMaterial;
			if (!(material2 == null))
			{
				material2 = new Material(material2)
				{
					name = material2.name + " (Instance)"
				};
				particleSystemRenderer.trailMaterial = material2;
				foreach (string name2 in ME_ColorHelper.colorProperties)
				{
					if (material2 != null && material2.HasProperty(name2))
					{
						ME_ColorHelper.setMatHUEColor(material2, name2, hue);
					}
				}
			}
		}
		SkinnedMeshRenderer[] componentsInChildren3 = go.GetComponentsInChildren<SkinnedMeshRenderer>(true);
		foreach (SkinnedMeshRenderer skinnedMeshRenderer in componentsInChildren3)
		{
			Material[] array8;
			if (!Application.isPlaying)
			{
				array8 = skinnedMeshRenderer.sharedMaterials;
			}
			else
			{
				array8 = skinnedMeshRenderer.materials;
			}
			if (array8.Length != 0)
			{
				foreach (string name3 in ME_ColorHelper.colorProperties)
				{
					foreach (Material material3 in array8)
					{
						if (material3 != null && material3.HasProperty(name3))
						{
							ME_ColorHelper.setMatHUEColor(material3, name3, hue);
						}
					}
				}
			}
		}
		Projector[] componentsInChildren4 = go.GetComponentsInChildren<Projector>(true);
		foreach (Projector projector in componentsInChildren4)
		{
			if (!projector.material.name.EndsWith("(Instance)"))
			{
				projector.material = new Material(projector.material)
				{
					name = projector.material.name + " (Instance)"
				};
			}
			Material material4 = projector.material;
			if (!(material4 == null))
			{
				foreach (string name4 in ME_ColorHelper.colorProperties)
				{
					if (material4 != null && material4.HasProperty(name4))
					{
						projector.material = ME_ColorHelper.setMatHUEColor(material4, name4, hue);
					}
				}
			}
		}
		Light[] componentsInChildren5 = go.GetComponentsInChildren<Light>(true);
		foreach (Light light in componentsInChildren5)
		{
			ME_ColorHelper.HSBColor hsbColor = ME_ColorHelper.ColorToHSV(light.color);
			hsbColor.H = hue;
			light.color = ME_ColorHelper.HSVToColor(hsbColor);
		}
		ParticleSystem[] componentsInChildren6 = go.GetComponentsInChildren<ParticleSystem>(true);
		foreach (ParticleSystem particleSystem in componentsInChildren6)
		{
			ParticleSystem.MainModule main = particleSystem.main;
			ME_ColorHelper.HSBColor hsbColor2 = ME_ColorHelper.ColorToHSV(particleSystem.main.startColor.color);
			hsbColor2.H = hue;
			main.startColor = ME_ColorHelper.HSVToColor(hsbColor2);
			ParticleSystem.ColorOverLifetimeModule colorOverLifetime = particleSystem.colorOverLifetime;
			ParticleSystem.MinMaxGradient color = colorOverLifetime.color;
			Gradient gradient = colorOverLifetime.color.gradient;
			GradientColorKey[] colorKeys = colorOverLifetime.color.gradient.colorKeys;
			hsbColor2 = ME_ColorHelper.ColorToHSV(colorKeys[0].color);
			float num7 = Math.Abs(ME_ColorHelper.ColorToHSV(colorKeys[1].color).H - hsbColor2.H);
			hsbColor2.H = hue;
			colorKeys[0].color = ME_ColorHelper.HSVToColor(hsbColor2);
			for (int num8 = 1; num8 < colorKeys.Length; num8++)
			{
				hsbColor2 = ME_ColorHelper.ColorToHSV(colorKeys[num8].color);
				hsbColor2.H = Mathf.Repeat(hsbColor2.H + num7, 1f);
				colorKeys[num8].color = ME_ColorHelper.HSVToColor(hsbColor2);
			}
			gradient.colorKeys = colorKeys;
			color.gradient = gradient;
			colorOverLifetime.color = color;
		}
	}

	// Token: 0x06001383 RID: 4995 RVA: 0x00078B94 File Offset: 0x00076F94
	private static Material setMatHUEColor(Material mat, string name, float hueColor)
	{
		Color color = mat.GetColor(name);
		Color value = ME_ColorHelper.ConvertRGBColorByHUE(color, hueColor);
		mat.SetColor(name, value);
		return mat;
	}

	// Token: 0x06001384 RID: 4996 RVA: 0x00078BBC File Offset: 0x00076FBC
	private static Material setMatAlphaColor(Material mat, string name, float alpha)
	{
		Color color = mat.GetColor(name);
		color.a = alpha;
		mat.SetColor(name, color);
		return mat;
	}

	// Token: 0x040015DF RID: 5599
	private const float TOLERANCE = 0.0001f;

	// Token: 0x040015E0 RID: 5600
	private static string[] colorProperties = new string[]
	{
		"_TintColor",
		"_Color",
		"_EmissionColor",
		"_BorderColor",
		"_ReflectColor",
		"_RimColor",
		"_MainColor",
		"_CoreColor",
		"_FresnelColor",
		"_CutoutColor"
	};

	// Token: 0x0200042E RID: 1070
	public struct HSBColor
	{
		// Token: 0x06001386 RID: 4998 RVA: 0x00078C4E File Offset: 0x0007704E
		public HSBColor(float h, float s, float b, float a)
		{
			this.H = h;
			this.S = s;
			this.B = b;
			this.A = a;
		}

		// Token: 0x040015E1 RID: 5601
		public float H;

		// Token: 0x040015E2 RID: 5602
		public float S;

		// Token: 0x040015E3 RID: 5603
		public float B;

		// Token: 0x040015E4 RID: 5604
		public float A;
	}
}
