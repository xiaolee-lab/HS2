using System;
using UnityEngine;

// Token: 0x0200081A RID: 2074
[Serializable]
public class HsvColor
{
	// Token: 0x060034D2 RID: 13522 RVA: 0x001377F4 File Offset: 0x00135BF4
	public HsvColor(float hue, float saturation, float brightness)
	{
		if (hue < 0f || 360f < hue)
		{
			throw new ArgumentException("hueは0~360の値です。", "hue");
		}
		if (saturation < 0f || 1f < saturation)
		{
			throw new ArgumentException("saturationは0以上1以下の値です。", "saturation");
		}
		if (brightness < 0f || 1f < brightness)
		{
			throw new ArgumentException("brightnessは0以上1以下の値です。", "brightness");
		}
		this._h = hue;
		this._s = saturation;
		this._v = brightness;
	}

	// Token: 0x060034D3 RID: 13523 RVA: 0x0013788E File Offset: 0x00135C8E
	public HsvColor(HsvColor src)
	{
		this._h = src._h;
		this._s = src._s;
		this._v = src._v;
	}

	// Token: 0x17000990 RID: 2448
	public float this[int index]
	{
		get
		{
			if (index == 0)
			{
				return this.H;
			}
			if (index == 1)
			{
				return this.S;
			}
			if (index != 2)
			{
				return float.MaxValue;
			}
			return this.V;
		}
		set
		{
			if (index != 0)
			{
				if (index != 1)
				{
					if (index == 2)
					{
						this.V = value;
					}
				}
				else
				{
					this.S = value;
				}
			}
			else
			{
				this.H = value;
			}
		}
	}

	// Token: 0x17000991 RID: 2449
	// (get) Token: 0x060034D6 RID: 13526 RVA: 0x0013792E File Offset: 0x00135D2E
	// (set) Token: 0x060034D7 RID: 13527 RVA: 0x00137936 File Offset: 0x00135D36
	public float H
	{
		get
		{
			return this._h;
		}
		set
		{
			this._h = value;
		}
	}

	// Token: 0x17000992 RID: 2450
	// (get) Token: 0x060034D8 RID: 13528 RVA: 0x0013793F File Offset: 0x00135D3F
	// (set) Token: 0x060034D9 RID: 13529 RVA: 0x00137947 File Offset: 0x00135D47
	public float S
	{
		get
		{
			return this._s;
		}
		set
		{
			this._s = value;
		}
	}

	// Token: 0x17000993 RID: 2451
	// (get) Token: 0x060034DA RID: 13530 RVA: 0x00137950 File Offset: 0x00135D50
	// (set) Token: 0x060034DB RID: 13531 RVA: 0x00137958 File Offset: 0x00135D58
	public float V
	{
		get
		{
			return this._v;
		}
		set
		{
			this._v = value;
		}
	}

	// Token: 0x060034DC RID: 13532 RVA: 0x00137961 File Offset: 0x00135D61
	public void Copy(HsvColor src)
	{
		this._h = src._h;
		this._s = src._s;
		this._v = src._v;
	}

	// Token: 0x060034DD RID: 13533 RVA: 0x00137988 File Offset: 0x00135D88
	public static HsvColor FromRgb(Color rgb)
	{
		float r = rgb.r;
		float g = rgb.g;
		float b = rgb.b;
		float num = Math.Max(r, Math.Max(g, b));
		float num2 = Math.Min(r, Math.Min(g, b));
		float hue = 0f;
		if (num == num2)
		{
			hue = 0f;
		}
		else if (num == r)
		{
			hue = (60f * (g - b) / (num - num2) + 360f) % 360f;
		}
		else if (num == g)
		{
			hue = 60f * (b - r) / (num - num2) + 120f;
		}
		else if (num == b)
		{
			hue = 60f * (r - g) / (num - num2) + 240f;
		}
		float saturation;
		if (num == 0f)
		{
			saturation = 0f;
		}
		else
		{
			saturation = (num - num2) / num;
		}
		float brightness = num;
		return new HsvColor(hue, saturation, brightness);
	}

	// Token: 0x060034DE RID: 13534 RVA: 0x00137A80 File Offset: 0x00135E80
	public static Color ToRgb(float h, float s, float v)
	{
		return HsvColor.ToRgb(new HsvColor(h, s, v));
	}

	// Token: 0x060034DF RID: 13535 RVA: 0x00137A90 File Offset: 0x00135E90
	public static Color ToRgb(HsvColor hsv)
	{
		float v = hsv.V;
		float s = hsv.S;
		float r;
		float g;
		float b;
		if (s == 0f)
		{
			r = v;
			g = v;
			b = v;
		}
		else
		{
			float num = hsv.H / 60f;
			int num2 = (int)Math.Floor((double)num) % 6;
			float num3 = num - (float)Math.Floor((double)num);
			float num4 = v * (1f - s);
			float num5 = v * (1f - s * num3);
			float num6 = v * (1f - s * (1f - num3));
			switch (num2)
			{
			case 0:
				r = v;
				g = num6;
				b = num4;
				break;
			case 1:
				r = num5;
				g = v;
				b = num4;
				break;
			case 2:
				r = num4;
				g = v;
				b = num6;
				break;
			case 3:
				r = num4;
				g = num5;
				b = v;
				break;
			case 4:
				r = num6;
				g = num4;
				b = v;
				break;
			case 5:
				r = v;
				g = num4;
				b = num5;
				break;
			default:
				throw new ArgumentException("色相の値が不正です。", "hsv");
			}
		}
		return new Color(r, g, b);
	}

	// Token: 0x060034E0 RID: 13536 RVA: 0x00137BAC File Offset: 0x00135FAC
	public static Color ToRgba(HsvColor hsv, float alpha)
	{
		Color result = HsvColor.ToRgb(hsv);
		result.a = alpha;
		return result;
	}

	// Token: 0x0400356D RID: 13677
	private float _h;

	// Token: 0x0400356E RID: 13678
	private float _s;

	// Token: 0x0400356F RID: 13679
	private float _v;
}
