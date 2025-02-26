using System;
using UnityEngine;

namespace Illusion.Extensions
{
	// Token: 0x02001084 RID: 4228
	public static class VectorExtensions
	{
		// Token: 0x06008DA7 RID: 36263 RVA: 0x003B1E14 File Offset: 0x003B0214
		private static string[] FormatRemoveSplit(string str)
		{
			return VectorExtensions.FormatRemove(str).Split(new char[]
			{
				','
			});
		}

		// Token: 0x06008DA8 RID: 36264 RVA: 0x003B1E2C File Offset: 0x003B022C
		private static string FormatRemove(string str)
		{
			return str.Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ", string.Empty);
		}

		// Token: 0x06008DA9 RID: 36265 RVA: 0x003B1E5C File Offset: 0x003B025C
		public static string Convert(this Vector2 self, bool isDefault = true)
		{
			int num = 0;
			return string.Format((!isDefault) ? "{0},{1}" : "({0}, {1})", self[num++], self[num++]);
		}

		// Token: 0x06008DAA RID: 36266 RVA: 0x003B1EA8 File Offset: 0x003B02A8
		public static string Convert(this Vector3 self, bool isDefault = true)
		{
			int num = 0;
			return string.Format((!isDefault) ? "{0},{1},{2}" : "({0}, {1}, {2})", self[num++], self[num++], self[num++]);
		}

		// Token: 0x06008DAB RID: 36267 RVA: 0x003B1F04 File Offset: 0x003B0304
		public static string Convert(this Vector4 self, bool isDefault = true)
		{
			int num = 0;
			return string.Format((!isDefault) ? "{0},{1},{2},{3}" : "({0}, {1}, {2}, {3})", new object[]
			{
				self[num++],
				self[num++],
				self[num++],
				self[num++]
			});
		}

		// Token: 0x06008DAC RID: 36268 RVA: 0x003B1F84 File Offset: 0x003B0384
		public static Vector2 Convert(this Vector2 _, string str)
		{
			string[] array = VectorExtensions.FormatRemoveSplit(str);
			Vector2 zero = Vector2.zero;
			int num = 0;
			while (num < array.Length && num < 2)
			{
				float value;
				if (float.TryParse(array[num], out value))
				{
					zero[num] = value;
				}
				num++;
			}
			return zero;
		}

		// Token: 0x06008DAD RID: 36269 RVA: 0x003B1FD4 File Offset: 0x003B03D4
		public static Vector3 Convert(this Vector3 _, string str)
		{
			string[] array = VectorExtensions.FormatRemoveSplit(str);
			Vector3 zero = Vector3.zero;
			int num = 0;
			while (num < array.Length && num < 3)
			{
				float value;
				if (float.TryParse(array[num], out value))
				{
					zero[num] = value;
				}
				num++;
			}
			return zero;
		}

		// Token: 0x06008DAE RID: 36270 RVA: 0x003B2024 File Offset: 0x003B0424
		public static Vector4 Convert(this Vector4 _, string str)
		{
			string[] array = VectorExtensions.FormatRemoveSplit(str);
			Vector4 zero = Vector4.zero;
			int num = 0;
			while (num < array.Length && num < 4)
			{
				float value;
				if (float.TryParse(array[num], out value))
				{
					zero[num] = value;
				}
				num++;
			}
			return zero;
		}

		// Token: 0x06008DAF RID: 36271 RVA: 0x003B2074 File Offset: 0x003B0474
		public static Vector2 Convert(this Vector2 self, float[] fArray)
		{
			int num = 0;
			return new Vector2(fArray[num++], fArray[num++]);
		}

		// Token: 0x06008DB0 RID: 36272 RVA: 0x003B2098 File Offset: 0x003B0498
		public static Vector3 Convert(this Vector3 self, float[] fArray)
		{
			int num = 0;
			return new Vector3(fArray[num++], fArray[num++], fArray[num++]);
		}

		// Token: 0x06008DB1 RID: 36273 RVA: 0x003B20C4 File Offset: 0x003B04C4
		public static Vector4 Convert(this Vector4 self, float[] fArray)
		{
			int num = 0;
			return new Vector4(fArray[num++], fArray[num++], fArray[num++], fArray[num++]);
		}

		// Token: 0x06008DB2 RID: 36274 RVA: 0x003B20F4 File Offset: 0x003B04F4
		public static float[] ToArray(this Vector2 self)
		{
			int num = 0;
			return new float[]
			{
				self[num++],
				self[num++]
			};
		}

		// Token: 0x06008DB3 RID: 36275 RVA: 0x003B2128 File Offset: 0x003B0528
		public static float[] ToArray(this Vector3 self)
		{
			int num = 0;
			return new float[]
			{
				self[num++],
				self[num++],
				self[num++]
			};
		}

		// Token: 0x06008DB4 RID: 36276 RVA: 0x003B216C File Offset: 0x003B056C
		public static float[] ToArray(this Vector4 self)
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

		// Token: 0x06008DB5 RID: 36277 RVA: 0x003B21BD File Offset: 0x003B05BD
		public static Vector2 Get(this Vector2 self, Vector2 set, bool x = true, bool y = true)
		{
			return new Vector2((!x) ? self.x : set.x, (!y) ? self.y : set.y);
		}

		// Token: 0x06008DB6 RID: 36278 RVA: 0x003B21F8 File Offset: 0x003B05F8
		public static Vector3 Get(this Vector3 self, Vector3 set, bool x = true, bool y = true, bool z = true)
		{
			return new Vector3((!x) ? self.x : set.x, (!y) ? self.y : set.y, (!z) ? self.z : set.z);
		}

		// Token: 0x06008DB7 RID: 36279 RVA: 0x003B2258 File Offset: 0x003B0658
		public static Vector4 Get(this Vector4 self, Vector4 set, bool x = true, bool y = true, bool z = true, bool w = true)
		{
			return new Vector4((!x) ? self.x : set.x, (!y) ? self.y : set.y, (!z) ? self.z : set.z, (!w) ? self.w : set.w);
		}

		// Token: 0x06008DB8 RID: 36280 RVA: 0x003B22D0 File Offset: 0x003B06D0
		public static bool isNaN(this Vector2 self)
		{
			for (int i = 0; i < 2; i++)
			{
				if (float.IsNaN(self[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008DB9 RID: 36281 RVA: 0x003B2304 File Offset: 0x003B0704
		public static bool isNaN(this Vector3 self)
		{
			for (int i = 0; i < 3; i++)
			{
				if (float.IsNaN(self[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008DBA RID: 36282 RVA: 0x003B2338 File Offset: 0x003B0738
		public static bool isNaN(this Vector4 self)
		{
			for (int i = 0; i < 4; i++)
			{
				if (float.IsNaN(self[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008DBB RID: 36283 RVA: 0x003B236C File Offset: 0x003B076C
		public static bool isInfinity(this Vector2 self)
		{
			for (int i = 0; i < 2; i++)
			{
				if (float.IsInfinity(self[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008DBC RID: 36284 RVA: 0x003B23A0 File Offset: 0x003B07A0
		public static bool isInfinity(this Vector3 self)
		{
			for (int i = 0; i < 3; i++)
			{
				if (float.IsInfinity(self[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008DBD RID: 36285 RVA: 0x003B23D4 File Offset: 0x003B07D4
		public static bool isInfinity(this Vector4 self)
		{
			for (int i = 0; i < 4; i++)
			{
				if (float.IsInfinity(self[i]))
				{
					return true;
				}
			}
			return false;
		}
	}
}
