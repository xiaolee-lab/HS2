using System;
using UnityEngine;

namespace Illusion.Extensions
{
	// Token: 0x02001077 RID: 4215
	public static class ColorExtensions
	{
		// Token: 0x06008D42 RID: 36162 RVA: 0x003B0C14 File Offset: 0x003AF014
		private static string[] FormatRemoveSplit(string str)
		{
			return ColorExtensions.FormatRemove(str).Split(new char[]
			{
				','
			});
		}

		// Token: 0x06008D43 RID: 36163 RVA: 0x003B0C2C File Offset: 0x003AF02C
		private static string FormatRemove(string str)
		{
			return str.Replace("RGBA", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ", string.Empty);
		}

		// Token: 0x06008D44 RID: 36164 RVA: 0x003B0C6C File Offset: 0x003AF06C
		public static string Convert(this Color self, bool isDefault = true)
		{
			int num = 0;
			return string.Format((!isDefault) ? "{0},{1},{2},{3}" : "RGBA({0}, {1}, {2}, {3})", new object[]
			{
				self[num++],
				self[num++],
				self[num++],
				self[num++]
			});
		}

		// Token: 0x06008D45 RID: 36165 RVA: 0x003B0CEC File Offset: 0x003AF0EC
		public static Color Convert(this Color _, string str)
		{
			string[] array = ColorExtensions.FormatRemoveSplit(str);
			Color clear = Color.clear;
			int num = 0;
			while (num < array.Length && num < 4)
			{
				float value;
				if (float.TryParse(array[num], out value))
				{
					clear[num] = value;
				}
				num++;
			}
			return clear;
		}

		// Token: 0x06008D46 RID: 36166 RVA: 0x003B0D3C File Offset: 0x003AF13C
		public static float[] ToArray(this Color self)
		{
			int num = 0;
			return new float[]
			{
				self[num++],
				self[num++],
				self[num++],
				self[num++]
			};
		}

		// Token: 0x06008D47 RID: 36167 RVA: 0x003B0D90 File Offset: 0x003AF190
		public static Color RGBToHSV(this Color self)
		{
			float r;
			float g;
			float b;
			Color.RGBToHSV(self, out r, out g, out b);
			return new Color(r, g, b, self.a);
		}

		// Token: 0x06008D48 RID: 36168 RVA: 0x003B0DB8 File Offset: 0x003AF1B8
		public static Color HSVToRGB(this Color self)
		{
			int index = 0;
			Color result = Color.HSVToRGB(self[index++], self[index++], self[index++]);
			result[index] = self[index];
			return result;
		}

		// Token: 0x06008D49 RID: 36169 RVA: 0x003B0E04 File Offset: 0x003AF204
		public static Color HSVToRGB(this Color self, bool hdr)
		{
			int index = 0;
			Color result = Color.HSVToRGB(self[index++], self[index++], self[index++], hdr);
			result[index] = self[index];
			return result;
		}

		// Token: 0x06008D4A RID: 36170 RVA: 0x003B0E50 File Offset: 0x003AF250
		public static Color Get(this Color self, Color set, bool a = true, bool r = true, bool g = true, bool b = true)
		{
			return new Color((!r) ? self.r : set.r, (!g) ? self.g : set.g, (!b) ? self.b : set.b, (!a) ? self.a : set.a);
		}
	}
}
