using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Illusion.Extensions
{
	// Token: 0x02001081 RID: 4225
	public static class StringExtensions
	{
		// Token: 0x06008D86 RID: 36230 RVA: 0x003B16A9 File Offset: 0x003AFAA9
		public static string RemoveExtension(this string self)
		{
			return self.Substring(0, self.LastIndexOf("."));
		}

		// Token: 0x06008D87 RID: 36231 RVA: 0x003B16BD File Offset: 0x003AFABD
		public static string SetExtension(this string self, string extension)
		{
			return self.Substring(0, self.LastIndexOf(".")) + "." + extension;
		}

		// Token: 0x06008D88 RID: 36232 RVA: 0x003B16DC File Offset: 0x003AFADC
		public static string RemoveNewLine(this string self)
		{
			return self.Replace("\r", string.Empty).Replace("\n", string.Empty);
		}

		// Token: 0x06008D89 RID: 36233 RVA: 0x003B16FD File Offset: 0x003AFAFD
		public static bool Compare(this string self, string str, bool ignoreCase = false)
		{
			return string.Compare(self, str, ignoreCase) == 0;
		}

		// Token: 0x06008D8A RID: 36234 RVA: 0x003B170A File Offset: 0x003AFB0A
		public static bool Compare(this string self, string str, StringComparison comparison)
		{
			return string.Compare(self, str, comparison) == 0;
		}

		// Token: 0x06008D8B RID: 36235 RVA: 0x003B1717 File Offset: 0x003AFB17
		public static bool CompareParts(this string self, string str, bool ignoreCase = false)
		{
			return ignoreCase ? (self.IndexOf(str, StringComparison.OrdinalIgnoreCase) != -1) : (self.IndexOf(str) != -1);
		}

		// Token: 0x06008D8C RID: 36236 RVA: 0x003B173F File Offset: 0x003AFB3F
		public static bool CompareParts(this string self, string str, StringComparison comparison)
		{
			return self.IndexOf(str, comparison) != -1;
		}

		// Token: 0x06008D8D RID: 36237 RVA: 0x003B1750 File Offset: 0x003AFB50
		public static string[] LastStringEmptyRemove(this string[] self)
		{
			int num = self.Length;
			while (--num >= 0 && self[num].IsNullOrEmpty())
			{
			}
			string[] array = new string[++num];
			Array.Copy(self, 0, array, 0, num);
			return array;
		}

		// Token: 0x06008D8E RID: 36238 RVA: 0x003B1794 File Offset: 0x003AFB94
		public static List<string> LastStringEmptyRemove(this List<string> self)
		{
			int num = self.Count;
			while (--num >= 0 && self[num].IsNullOrEmpty())
			{
			}
			return self.GetRange(0, num + 1);
		}

		// Token: 0x06008D8F RID: 36239 RVA: 0x003B17D4 File Offset: 0x003AFBD4
		public static string[] LastStringEmptySpaceRemove(this string[] self)
		{
			int num = self.Length;
			while (--num >= 0 && self[num].IsNullOrWhiteSpace())
			{
			}
			string[] array = new string[++num];
			Array.Copy(self, 0, array, 0, num);
			return array;
		}

		// Token: 0x06008D90 RID: 36240 RVA: 0x003B1818 File Offset: 0x003AFC18
		public static List<string> LastStringEmptySpaceRemove(this List<string> self)
		{
			int num = self.Count;
			while (--num >= 0 && self[num].IsNullOrWhiteSpace())
			{
			}
			return self.GetRange(0, num + 1);
		}

		// Token: 0x06008D91 RID: 36241 RVA: 0x003B1857 File Offset: 0x003AFC57
		public static string Coloring(this string self, string color)
		{
			return string.Format("<color={0}>{1}</color>", color, self);
		}

		// Token: 0x06008D92 RID: 36242 RVA: 0x003B1865 File Offset: 0x003AFC65
		public static string Size(this string self, int size)
		{
			return string.Format("<size={0}>{1}</size>", size, self);
		}

		// Token: 0x06008D93 RID: 36243 RVA: 0x003B1878 File Offset: 0x003AFC78
		public static string Bold(this string self)
		{
			return string.Format("<b>{0}</b>", self);
		}

		// Token: 0x06008D94 RID: 36244 RVA: 0x003B1885 File Offset: 0x003AFC85
		public static string Italic(this string self)
		{
			return string.Format("<i>{0}</i>", self);
		}

		// Token: 0x06008D95 RID: 36245 RVA: 0x003B1894 File Offset: 0x003AFC94
		public static Color GetColor(this string self)
		{
			Color? colorCheck = self.GetColorCheck();
			return (colorCheck == null) ? Color.clear : colorCheck.Value;
		}

		// Token: 0x06008D96 RID: 36246 RVA: 0x003B18C8 File Offset: 0x003AFCC8
		public static Color? GetColorCheck(this string self)
		{
			if (self.IsNullOrEmpty())
			{
				return null;
			}
			string[] array = self.Split(new char[]
			{
				','
			});
			if (array.Length >= 3)
			{
				int num = 0;
				Color value;
				float.TryParse(array.SafeGet(num++), out value.r);
				float.TryParse(array.SafeGet(num++), out value.g);
				float.TryParse(array.SafeGet(num++), out value.b);
				if (!float.TryParse(array.SafeGet(num++), out value.a))
				{
					value.a = 1f;
				}
				for (int i = 0; i < num; i++)
				{
					if (value[i] > 1f)
					{
						value[i] = Mathf.InverseLerp(0f, 255f, value[i]);
					}
				}
				return new Color?(value);
			}
			Color value2;
			if (ColorUtility.TryParseHtmlString(self, out value2))
			{
				return new Color?(value2);
			}
			return null;
		}

		// Token: 0x06008D97 RID: 36247 RVA: 0x003B19E8 File Offset: 0x003AFDE8
		public static Vector2 GetVector2(this string self)
		{
			string[] array = StringExtensions.StringVectorReplace(self);
			return new Vector2(float.Parse(array[0]), float.Parse(array[1]));
		}

		// Token: 0x06008D98 RID: 36248 RVA: 0x003B1A14 File Offset: 0x003AFE14
		public static Vector3 GetVector3(this string self)
		{
			string[] array = StringExtensions.StringVectorReplace(self);
			return new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
		}

		// Token: 0x06008D99 RID: 36249 RVA: 0x003B1A45 File Offset: 0x003AFE45
		private static string[] StringVectorReplace(string str)
		{
			return str.Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ", string.Empty).Split(new char[]
			{
				','
			});
		}

		// Token: 0x06008D9A RID: 36250 RVA: 0x003B1A85 File Offset: 0x003AFE85
		public static int Check(this string self, bool ignoreCase, params string[] strs)
		{
			return self.Check(ignoreCase, (string s) => s, strs);
		}

		// Token: 0x06008D9B RID: 36251 RVA: 0x003B1AAC File Offset: 0x003AFEAC
		public static int Check(this string self, bool ignoreCase, Func<string, string> func, params string[] strs)
		{
			int num = -1;
			while (++num < strs.Length && !self.Compare(func(strs[num]), ignoreCase))
			{
			}
			return (num < strs.Length) ? num : -1;
		}

		// Token: 0x06008D9C RID: 36252 RVA: 0x003B1AF2 File Offset: 0x003AFEF2
		public static string ToTitleCase(this string self)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(self);
		}

		// Token: 0x06008D9D RID: 36253 RVA: 0x003B1B04 File Offset: 0x003AFF04
		public static string CopyNameReplace(this string self, int cnt)
		{
			return (cnt <= 0) ? self : string.Format("{0} {1}", self, cnt);
		}
	}
}
