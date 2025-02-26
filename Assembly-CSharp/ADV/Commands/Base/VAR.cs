using System;
using System.Collections.Generic;
using System.Linq;
using Illusion.Extensions;
using UnityEngine;

namespace ADV.Commands.Base
{
	// Token: 0x02000707 RID: 1799
	public class VAR : CommandBase
	{
		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06002AFD RID: 11005 RVA: 0x000F9283 File Offset: 0x000F7683
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Type",
					"Variable",
					"Value"
				};
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x000F92A3 File Offset: 0x000F76A3
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"int",
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x000F92C4 File Offset: 0x000F76C4
		public static int RefCheck(ref string variable)
		{
			int num = 0;
			while (!variable.IsNullOrEmpty() && variable[0] == '*')
			{
				num++;
				variable = variable.Remove(0, 1);
			}
			return num;
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000F9304 File Offset: 0x000F7704
		public static string RefGet(Type type, int refCount, string variable, Dictionary<string, ValData> Vars)
		{
			ValData valData = null;
			if (refCount-- > 0)
			{
				valData = new ValData(Vars[variable].o);
			}
			while (refCount-- > 0)
			{
				valData = Vars[valData.o.ToString()];
			}
			return (valData != null) ? ValData.Cast(valData.o, type).ToString() : null;
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x000F9374 File Offset: 0x000F7774
		public override void ConvertBeforeArgsProc()
		{
			base.ConvertBeforeArgsProc();
			int num = 0;
			this.type = Type.GetType(this.args[num++]);
			this.name = this.args[num++];
			this.value = this.args.Skip(num++).Shuffle<string>().FirstOrDefault<string>();
			this.refCnt = VAR.RefCheck(ref this.value);
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000F93E4 File Offset: 0x000F77E4
		public override void Do()
		{
			base.Do();
			Dictionary<string, ValData> vars = base.scenario.Vars;
			VAR.RefGet(this.type, this.refCnt, this.value, vars).SafeProc(delegate(string s)
			{
				this.value = s;
			});
			vars[this.name] = new ValData(ValData.Cast(this.value, this.type));
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000F9450 File Offset: 0x000F7850
		public override void Convert(string fileName, ref string[] args)
		{
			string text = args[0];
			if (text != null)
			{
				Type typeFromHandle;
				if (!(text == "int"))
				{
					if (!(text == "float"))
					{
						if (!(text == "string"))
						{
							if (!(text == "bool"))
							{
								goto IL_92;
							}
							typeFromHandle = typeof(bool);
						}
						else
						{
							typeFromHandle = typeof(string);
						}
					}
					else
					{
						typeFromHandle = typeof(float);
					}
				}
				else
				{
					typeFromHandle = typeof(int);
				}
				args[0] = typeFromHandle.ToString();
				return;
			}
			IL_92:
			object obj = VAR.CheckLiterals(args[1]);
			args = new string[]
			{
				obj.GetType().ToString(),
				args[0],
				obj.ToString()
			};
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x000F952C File Offset: 0x000F792C
		public static object CheckLiterals(object o)
		{
			string text = o.ToString();
			if (text.Check(true, new string[]
			{
				bool.TrueString,
				bool.FalseString
			}) != -1)
			{
				return bool.Parse(text);
			}
			int num;
			if (VAR.CheckFirst(text, "0x", out num) == 0)
			{
				return System.Convert.ToInt32(VAR.Convert(text, num + 1), 16);
			}
			if (VAR.CheckFirst(text, "0b", out num) == 0)
			{
				return System.Convert.ToInt32(VAR.Convert(text, num + 1), 2);
			}
			if (VAR.CheckFirst(text, "0o", out num) == 0)
			{
				return System.Convert.ToInt32(VAR.Convert(text, num + 1), 8);
			}
			if (VAR.CheckFirst(text, ".", out num) != -1)
			{
				int num2 = text.Length;
				bool flag = false;
				int length;
				if (VAR.CheckLast(text, "d", out length) != -1)
				{
					num2--;
					flag = true;
				}
				else if (VAR.CheckLast(text, "f", out length) != -1)
				{
					num2--;
				}
				if (VAR.CheckLast(text, "e", out num) != -1)
				{
					string text2 = VAR.Convert(text, num);
					if (num2 != text.Length)
					{
						text2 = text2.Substring(0, text2.Length - 1);
					}
					float num3 = Mathf.Pow(10f, (float)int.Parse(text2));
					if (flag)
					{
						return double.Parse(text.Substring(0, num)) * (double)num3;
					}
					return float.Parse(text.Substring(0, num)) * num3;
				}
				else
				{
					if (flag)
					{
						return double.Parse((num2 != text.Length) ? text.Substring(0, length) : text);
					}
					return float.Parse((num2 != text.Length) ? text.Substring(0, length) : text);
				}
			}
			else if (VAR.CheckLast(text, "d", out num) != -1)
			{
				int num4 = text.Length;
				num4--;
				string text3 = text.Substring(0, num);
				if (VAR.CheckLast(text, "e", out num) != -1)
				{
					text3 = VAR.Convert(text, num);
					text3 = text3.Substring(0, text3.Length - 1);
					return double.Parse(text.Substring(0, num)) * (double)Mathf.Pow(10f, (float)int.Parse(text3));
				}
				return double.Parse(text3);
			}
			else if (VAR.CheckLast(text, "f", out num) != -1)
			{
				int num5 = text.Length;
				num5--;
				string text4 = text.Substring(0, num);
				if (VAR.CheckLast(text, "e", out num) != -1)
				{
					text4 = VAR.Convert(text, num);
					text4 = text4.Substring(0, text4.Length - 1);
					return float.Parse(text.Substring(0, num)) * Mathf.Pow(10f, (float)int.Parse(text4));
				}
				return float.Parse(text4);
			}
			else
			{
				if (VAR.CheckLastLiterals(text, "ul", out num))
				{
					return ulong.Parse(text.Substring(0, num));
				}
				if (VAR.CheckLastLiterals(text, "l", out num))
				{
					return long.Parse(text.Substring(0, num));
				}
				if (VAR.CheckLastLiterals(text, "u", out num))
				{
					return uint.Parse(text.Substring(0, num));
				}
				if (VAR.CheckLastLiterals(text, "m", out num))
				{
					return decimal.Parse(text.Substring(0, num));
				}
				if (VAR.CheckLast(text, "e", out num) != -1)
				{
					return float.Parse(text.Substring(0, num)) * Mathf.Pow(10f, (float)int.Parse(VAR.Convert(text, num)));
				}
				if (text[0] == '"' && text[text.Length - 1] == '"')
				{
					return text.Remove(text.Length - 1, 1).Remove(0, 1);
				}
				return (!int.TryParse(text, out num)) ? o : num;
			}
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000F994C File Offset: 0x000F7D4C
		private static string Convert(string s, int n)
		{
			int num = n + 1;
			return s.Substring(num, s.Length - num);
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x000F996C File Offset: 0x000F7D6C
		private static int CheckFirst(string s, string c, out int n)
		{
			return n = s.ToLower().IndexOf(c);
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x000F998C File Offset: 0x000F7D8C
		private static int CheckLast(string s, string c, out int n)
		{
			return n = s.ToLower().LastIndexOf(c);
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000F99AA File Offset: 0x000F7DAA
		private static bool CheckLastLiterals(string s, string c, out int n)
		{
			return VAR.CheckLast(s, c, out n) != -1 && s.Substring(n, s.Length - n).ToLower() == c;
		}

		// Token: 0x04002B13 RID: 11027
		private Type type;

		// Token: 0x04002B14 RID: 11028
		private string name;

		// Token: 0x04002B15 RID: 11029
		private string value;

		// Token: 0x04002B16 RID: 11030
		private int refCnt;
	}
}
