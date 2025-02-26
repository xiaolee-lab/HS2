using System;
using UnityEngine;

// Token: 0x02000455 RID: 1109
public class EffectsColorizer : MonoBehaviour
{
	// Token: 0x06001450 RID: 5200 RVA: 0x0007F4CC File Offset: 0x0007D8CC
	private void Start()
	{
	}

	// Token: 0x06001451 RID: 5201 RVA: 0x0007F4CE File Offset: 0x0007D8CE
	private void Update()
	{
		if (this.oldColor != this.TintColor)
		{
			this.ChangeColor(base.gameObject, this.TintColor);
		}
		this.oldColor = this.TintColor;
	}

	// Token: 0x06001452 RID: 5202 RVA: 0x0007F504 File Offset: 0x0007D904
	private void ChangeColor(GameObject effect, Color color)
	{
		Renderer[] componentsInChildren = effect.GetComponentsInChildren<Renderer>();
		foreach (Renderer renderer in componentsInChildren)
		{
			Material material;
			if (this.UseInstanceWhenNotEditorMode)
			{
				material = renderer.material;
			}
			else
			{
				material = renderer.sharedMaterial;
			}
			EffectsColorizer.HSBColor hsbcolor = this.ColorToHSV(this.TintColor);
			if (!(material == null))
			{
				if (material.HasProperty("_TintColor"))
				{
					Color color2 = material.GetColor("_TintColor");
					EffectsColorizer.HSBColor hsbColor = this.ColorToHSV(color2);
					hsbColor.h = hsbcolor.h / 360f;
					color = this.HSVToColor(hsbColor);
					material.SetColor("_TintColor", color);
				}
				if (material.HasProperty("_CoreColor"))
				{
					Color color3 = material.GetColor("_CoreColor");
					EffectsColorizer.HSBColor hsbColor2 = this.ColorToHSV(color3);
					hsbColor2.h = hsbcolor.h / 360f;
					color = this.HSVToColor(hsbColor2);
					material.SetColor("_CoreColor", color);
				}
				Projector[] componentsInChildren2 = effect.GetComponentsInChildren<Projector>();
				foreach (Projector projector in componentsInChildren2)
				{
					material = projector.material;
					if (!(material == null) && material.HasProperty("_TintColor"))
					{
						Color color4 = material.GetColor("_TintColor");
						EffectsColorizer.HSBColor hsbColor3 = this.ColorToHSV(color4);
						hsbColor3.h = hsbcolor.h / 360f;
						color = this.HSVToColor(hsbColor3);
						projector.material.SetColor("_TintColor", color);
					}
				}
			}
		}
		Light componentInChildren = effect.GetComponentInChildren<Light>();
		if (componentInChildren != null)
		{
			componentInChildren.color = color;
		}
	}

	// Token: 0x06001453 RID: 5203 RVA: 0x0007F6D8 File Offset: 0x0007DAD8
	public EffectsColorizer.HSBColor ColorToHSV(Color color)
	{
		EffectsColorizer.HSBColor result = new EffectsColorizer.HSBColor(0f, 0f, 0f, color.a);
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
			if (g == num)
			{
				result.h = (b - r) / num3 * 60f + 120f;
			}
			else if (b == num)
			{
				result.h = (r - g) / num3 * 60f + 240f;
			}
			else if (b > g)
			{
				result.h = (g - b) / num3 * 60f + 360f;
			}
			else
			{
				result.h = (g - b) / num3 * 60f;
			}
			if (result.h < 0f)
			{
				result.h += 360f;
			}
		}
		else
		{
			result.h = 0f;
		}
		result.h *= 0.0027777778f;
		result.s = num3 / num * 1f;
		result.b = num;
		return result;
	}

	// Token: 0x06001454 RID: 5204 RVA: 0x0007F840 File Offset: 0x0007DC40
	public Color HSVToColor(EffectsColorizer.HSBColor hsbColor)
	{
		float value = hsbColor.b;
		float value2 = hsbColor.b;
		float value3 = hsbColor.b;
		if (hsbColor.s != 0f)
		{
			float b = hsbColor.b;
			float num = hsbColor.b * hsbColor.s;
			float num2 = hsbColor.b - num;
			float num3 = hsbColor.h * 360f;
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
		return new Color(Mathf.Clamp01(value), Mathf.Clamp01(value2), Mathf.Clamp01(value3), hsbColor.a);
	}

	// Token: 0x0400172C RID: 5932
	public Color TintColor;

	// Token: 0x0400172D RID: 5933
	public bool UseInstanceWhenNotEditorMode = true;

	// Token: 0x0400172E RID: 5934
	private Color oldColor;

	// Token: 0x02000456 RID: 1110
	public struct HSBColor
	{
		// Token: 0x06001455 RID: 5205 RVA: 0x0007F9D9 File Offset: 0x0007DDD9
		public HSBColor(float h, float s, float b, float a)
		{
			this.h = h;
			this.s = s;
			this.b = b;
			this.a = a;
		}

		// Token: 0x0400172F RID: 5935
		public float h;

		// Token: 0x04001730 RID: 5936
		public float s;

		// Token: 0x04001731 RID: 5937
		public float b;

		// Token: 0x04001732 RID: 5938
		public float a;
	}
}
