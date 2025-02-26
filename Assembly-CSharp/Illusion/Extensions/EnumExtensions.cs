using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

namespace Illusion.Extensions
{
	// Token: 0x0200107A RID: 4218
	public static class EnumExtensions
	{
		// Token: 0x06008D4D RID: 36173 RVA: 0x003B0F55 File Offset: 0x003AF355
		[Conditional("UNITY_ASSERTIONS")]
		private static void Check(Enum self, Enum flag)
		{
		}

		// Token: 0x06008D4E RID: 36174 RVA: 0x003B0F58 File Offset: 0x003AF358
		public static bool HasFlag(this Enum self, Enum flag)
		{
			ulong num = Convert.ToUInt64(flag);
			return self.AND(num) == num;
		}

		// Token: 0x06008D4F RID: 36175 RVA: 0x003B0F76 File Offset: 0x003AF376
		public static ulong Add(this Enum self, Enum flag)
		{
			return self.OR(flag);
		}

		// Token: 0x06008D50 RID: 36176 RVA: 0x003B0F7F File Offset: 0x003AF37F
		public static ulong Sub(this Enum self, Enum flag)
		{
			return Convert.ToUInt64(self) & flag.NOT();
		}

		// Token: 0x06008D51 RID: 36177 RVA: 0x003B0F8E File Offset: 0x003AF38E
		public static ulong Get(this Enum self, Enum flag)
		{
			return self.AND(flag);
		}

		// Token: 0x06008D52 RID: 36178 RVA: 0x003B0F97 File Offset: 0x003AF397
		public static ulong Reverse(this Enum self, Enum flag)
		{
			return self.XOR(flag);
		}

		// Token: 0x06008D53 RID: 36179 RVA: 0x003B0FA0 File Offset: 0x003AF3A0
		public static ulong NOT(this Enum self)
		{
			return ~Convert.ToUInt64(self);
		}

		// Token: 0x06008D54 RID: 36180 RVA: 0x003B0FA9 File Offset: 0x003AF3A9
		public static ulong AND(this Enum self, Enum flag)
		{
			return Convert.ToUInt64(self) & Convert.ToUInt64(flag);
		}

		// Token: 0x06008D55 RID: 36181 RVA: 0x003B0FB8 File Offset: 0x003AF3B8
		public static ulong AND(this Enum self, ulong flag)
		{
			return Convert.ToUInt64(self) & flag;
		}

		// Token: 0x06008D56 RID: 36182 RVA: 0x003B0FC2 File Offset: 0x003AF3C2
		public static ulong OR(this Enum self, Enum flag)
		{
			return Convert.ToUInt64(self) | Convert.ToUInt64(flag);
		}

		// Token: 0x06008D57 RID: 36183 RVA: 0x003B0FD1 File Offset: 0x003AF3D1
		public static ulong OR(this Enum self, ulong flag)
		{
			return Convert.ToUInt64(self) | flag;
		}

		// Token: 0x06008D58 RID: 36184 RVA: 0x003B0FDB File Offset: 0x003AF3DB
		public static ulong XOR(this Enum self, Enum flag)
		{
			return Convert.ToUInt64(self) ^ Convert.ToUInt64(flag);
		}

		// Token: 0x06008D59 RID: 36185 RVA: 0x003B0FEA File Offset: 0x003AF3EA
		public static ulong XOR(this Enum self, ulong flag)
		{
			return Convert.ToUInt64(self) ^ flag;
		}

		// Token: 0x06008D5A RID: 36186 RVA: 0x003B0FF4 File Offset: 0x003AF3F4
		public static string LabelFormat(this Enum self, string label)
		{
			StringBuilder stringBuilder = new StringBuilder();
			IEnumerator enumerator = Enum.GetValues(self.GetType()).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					if (self.HasFlag((Enum)obj))
					{
						stringBuilder.AppendFormat("{0} | ", obj);
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return (stringBuilder.Length != 0) ? (label + stringBuilder) : string.Empty;
		}

		// Token: 0x06008D5B RID: 36187 RVA: 0x003B1094 File Offset: 0x003AF494
		public static bool All(this Enum self)
		{
			return self.Reverse(self.Everything()) == 0UL;
		}

		// Token: 0x06008D5C RID: 36188 RVA: 0x003B10A6 File Offset: 0x003AF4A6
		public static bool Any(this Enum self)
		{
			return Convert.ToUInt64(self) != self.Nothing();
		}

		// Token: 0x06008D5D RID: 36189 RVA: 0x003B10BC File Offset: 0x003AF4BC
		public static Enum Everything(this Enum self)
		{
			ulong num = 0UL;
			IEnumerator enumerator = Enum.GetValues(self.GetType()).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object value = enumerator.Current;
					num += Convert.ToUInt64(value);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return (Enum)Enum.ToObject(self.GetType(), num);
		}

		// Token: 0x06008D5E RID: 36190 RVA: 0x003B1138 File Offset: 0x003AF538
		public static ulong Nothing(this Enum self)
		{
			return 0UL;
		}

		// Token: 0x06008D5F RID: 36191 RVA: 0x003B113C File Offset: 0x003AF53C
		public static ulong Normalize(this Enum self)
		{
			return (ulong)Enum.ToObject(self.GetType(), Convert.ToInt64(self) & Convert.ToInt64(self.Everything()));
		}
	}
}
