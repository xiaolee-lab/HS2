using System;
using ADV.Commands.Base;
using Illusion;

namespace ADV
{
	// Token: 0x02000780 RID: 1920
	[Serializable]
	public class ValData
	{
		// Token: 0x06002CE8 RID: 11496 RVA: 0x00100E9C File Offset: 0x000FF29C
		public ValData(object o)
		{
			this.o = o;
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x00100EAB File Offset: 0x000FF2AB
		// (set) Token: 0x06002CEA RID: 11498 RVA: 0x00100EB3 File Offset: 0x000FF2B3
		public object o { get; private set; }

		// Token: 0x06002CEB RID: 11499 RVA: 0x00100EBC File Offset: 0x000FF2BC
		public object Convert(object val)
		{
			return ValData.Convert(val, this.o.GetType());
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x00100ECF File Offset: 0x000FF2CF
		public static object Convert(object val, Type type)
		{
			return System.Convert.ChangeType(val, type);
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x00100ED8 File Offset: 0x000FF2D8
		public static object Cast(object o, Type type)
		{
			if (o == null)
			{
				return ValData.Convert(o, type);
			}
			if (type == typeof(int))
			{
				int? num = null;
				int value;
				if (o is int || o is float)
				{
					num = new int?((int)o);
				}
				else if (o is bool)
				{
					num = new int?((!(bool)o) ? 0 : 1);
				}
				else if (int.TryParse(o.ToString(), out value))
				{
					num = new int?(value);
				}
				return (num == null) ? 0 : num.Value;
			}
			if (type == typeof(float))
			{
				float? num2 = null;
				float value2;
				if (o is float)
				{
					num2 = new float?((float)o);
				}
				else if (float.TryParse(o.ToString(), out value2))
				{
					num2 = new float?(value2);
				}
				return (num2 == null) ? 0f : num2.Value;
			}
			if (type == typeof(bool))
			{
				bool? flag = null;
				bool value3;
				if (o is bool)
				{
					flag = new bool?((bool)o);
				}
				else if (o is int || o is float)
				{
					flag = new bool?((int)o > 0);
				}
				else if (bool.TryParse(o.ToString(), out value3))
				{
					flag = new bool?(value3);
				}
				return flag != null && flag.Value;
			}
			return o.ToString();
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x001010BB File Offset: 0x000FF4BB
		public static bool operator <(ValData a, ValData b)
		{
			return ValData.IF(Illusion.Utils.Comparer.Type.Lesser, a.o, b.o);
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x001010CF File Offset: 0x000FF4CF
		public static bool operator >(ValData a, ValData b)
		{
			return ValData.IF(Illusion.Utils.Comparer.Type.Greater, a.o, b.o);
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x001010E3 File Offset: 0x000FF4E3
		public static bool operator <=(ValData a, ValData b)
		{
			return ValData.IF(Illusion.Utils.Comparer.Type.Under, a.o, b.o);
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x001010F7 File Offset: 0x000FF4F7
		public static bool operator >=(ValData a, ValData b)
		{
			return ValData.IF(Illusion.Utils.Comparer.Type.Over, a.o, b.o);
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x0010110B File Offset: 0x000FF50B
		public static ValData operator +(ValData a, ValData b)
		{
			return ValData.Calculate(Calc.Formula1.PlusEqual, a.o, b.o);
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x0010111F File Offset: 0x000FF51F
		public static ValData operator -(ValData a, ValData b)
		{
			return ValData.Calculate(Calc.Formula1.MinusEqual, a.o, b.o);
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x00101133 File Offset: 0x000FF533
		public static ValData operator *(ValData a, ValData b)
		{
			return ValData.Calculate(Calc.Formula1.AstaEqual, a.o, b.o);
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x00101147 File Offset: 0x000FF547
		public static ValData operator /(ValData a, ValData b)
		{
			return ValData.Calculate(Calc.Formula1.SlashEqual, a.o, b.o);
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x0010115B File Offset: 0x000FF55B
		private static bool IF(Illusion.Utils.Comparer.Type type, object a, object b)
		{
			return Illusion.Utils.Comparer.Check<IComparable>((IComparable)a, type, (IComparable)b);
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x00101170 File Offset: 0x000FF570
		private static ValData Calculate(Calc.Formula1 numerical, object a, object b)
		{
			if (a is int)
			{
				int num = (int)a;
				int num2 = (int)ValData.Cast(b, typeof(int));
				switch (numerical)
				{
				case Calc.Formula1.PlusEqual:
					return new ValData(num + num2);
				case Calc.Formula1.MinusEqual:
					return new ValData(num - num2);
				case Calc.Formula1.AstaEqual:
					return new ValData(num * num2);
				case Calc.Formula1.SlashEqual:
					return new ValData(num / num2);
				}
			}
			else if (a is float)
			{
				float num3 = (float)a;
				float num4 = (float)ValData.Cast(b, typeof(float));
				switch (numerical)
				{
				case Calc.Formula1.PlusEqual:
					return new ValData(num3 + num4);
				case Calc.Formula1.MinusEqual:
					return new ValData(num3 - num4);
				case Calc.Formula1.AstaEqual:
					return new ValData(num3 * num4);
				case Calc.Formula1.SlashEqual:
					return new ValData(num3 / num4);
				}
			}
			else if (a is bool)
			{
				bool flag = (bool)a;
				bool flag2 = (bool)ValData.Cast(b, typeof(bool));
				switch (numerical)
				{
				case Calc.Formula1.PlusEqual:
					return new ValData(((!flag) ? 0 : 1) + ((!flag2) ? 0 : 1) > 0);
				case Calc.Formula1.MinusEqual:
					return new ValData(((!flag) ? 0 : 1) - ((!flag2) ? 0 : 1) > 0);
				case Calc.Formula1.AstaEqual:
					return new ValData(flag || flag2);
				case Calc.Formula1.SlashEqual:
					return new ValData(flag && flag2);
				}
			}
			else
			{
				string text = a.ToString();
				string text2 = b.ToString();
				if (numerical == Calc.Formula1.PlusEqual)
				{
					return new ValData(text + text2);
				}
				if (numerical == Calc.Formula1.MinusEqual)
				{
					return new ValData(text.Replace(text2, string.Empty));
				}
			}
			return new ValData(null);
		}
	}
}
