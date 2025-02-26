using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Illusion;

namespace ADV.Commands.Base
{
	// Token: 0x020006D3 RID: 1747
	public class Calc : CommandBase
	{
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060029CD RID: 10701 RVA: 0x000F4536 File Offset: 0x000F2936
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Answer",
					"Formula",
					"Value"
				};
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060029CE RID: 10702 RVA: 0x000F4556 File Offset: 0x000F2956
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					string.Empty,
					"0"
				};
			}
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x000F4578 File Offset: 0x000F2978
		public override void ConvertBeforeArgsProc()
		{
			base.ConvertBeforeArgsProc();
			int i = 0;
			this.answer = this.args[i++];
			this.refCnt = VAR.RefCheck(ref this.answer);
			this.arg1 = this.args[i++];
			while (i < this.args.Length)
			{
				this.argsList.Add(this.args[i++]);
			}
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x000F45EC File Offset: 0x000F29EC
		public static string RefGet(int refCount, string variable, Dictionary<string, ValData> Vars)
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
			return (valData != null) ? valData.o.ToString() : null;
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x000F4654 File Offset: 0x000F2A54
		public override void Do()
		{
			base.Do();
			Dictionary<string, ValData> Vars = base.scenario.Vars;
			Calc.RefGet(this.refCnt, this.answer, Vars).SafeProc(delegate(string s)
			{
				this.answer = s;
			});
			ValData answerVal;
			if (!Vars.TryGetValue(this.answer, out answerVal))
			{
				string text = this.argsList[0];
				ValData valData;
				if (Vars.TryGetValue(text, out valData))
				{
					text = valData.o.ToString();
				}
				int num;
				float num2;
				bool flag;
				if (int.TryParse(text, out num))
				{
					answerVal = new ValData(0);
				}
				else if (float.TryParse(text, out num2))
				{
					answerVal = new ValData(0f);
				}
				else if (bool.TryParse(text, out flag))
				{
					answerVal = new ValData(false);
				}
				else
				{
					answerVal = new ValData(string.Empty);
				}
			}
			Func<string, ValData> func = delegate(string s)
			{
				ValData valData3;
				return new ValData(ValData.Cast((!Vars.TryGetValue(s, out valData3)) ? s : valData3.o, answerVal.o.GetType()));
			};
			int i = 0;
			ValData valData2 = func(this.argsList[i++]);
			while (i < this.argsList.Count)
			{
				valData2 = Calc.Calculate(valData2, Calc.Formula2to1((Calc.Formula2)int.Parse(this.argsList[i++])), func(this.argsList[i++]));
			}
			answerVal = Calc.Calculate(answerVal, (Calc.Formula1)int.Parse(this.arg1), valData2);
			Vars[this.answer] = answerVal;
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x000F4829 File Offset: 0x000F2C29
		private static Calc.Formula1 Formula2to1(Calc.Formula2 f2)
		{
			switch (f2)
			{
			case Calc.Formula2.Plus:
				return Calc.Formula1.PlusEqual;
			case Calc.Formula2.Minus:
				return Calc.Formula1.MinusEqual;
			case Calc.Formula2.Asta:
				return Calc.Formula1.AstaEqual;
			case Calc.Formula2.Slash:
				return Calc.Formula1.SlashEqual;
			default:
				return Calc.Formula1.Equal;
			}
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x000F4850 File Offset: 0x000F2C50
		private static ValData Calculate(ValData a, Calc.Formula1 f1, ValData b)
		{
			ValData valData = a;
			switch (f1)
			{
			case Calc.Formula1.Equal:
				valData = b;
				break;
			case Calc.Formula1.PlusEqual:
				valData += b;
				break;
			case Calc.Formula1.MinusEqual:
				valData -= b;
				break;
			case Calc.Formula1.AstaEqual:
				valData *= b;
				break;
			case Calc.Formula1.SlashEqual:
				valData /= b;
				break;
			}
			return valData;
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x000F48BC File Offset: 0x000F2CBC
		[Conditional("ADV_DEBUG")]
		private void DBTEST(ValData answerVal)
		{
			Dictionary<string, ValData> Vars = base.scenario.Vars;
			ValData dbOutputVal = answerVal;
			List<string> list = new List<string>();
			Func<string, ValData> func = delegate(string s)
			{
				ValData valData3;
				return (!Vars.TryGetValue(s, out valData3)) ? new ValData(dbOutputVal.Convert(s)) : new ValData(valData3.o);
			};
			int i = 0;
			ValData valData = func(this.argsList[i++]);
			list.Add(valData.o.ToString());
			while (i < this.argsList.Count)
			{
				Calc.Formula2 formula = (Calc.Formula2)int.Parse(this.argsList[i++]);
				ValData valData2 = func(this.argsList[i++]);
				list.Add(Calc.Cast(formula));
				list.Add(valData2.o.ToString());
				valData = Calc.Calculate(valData, Calc.Formula2to1(formula), valData2);
			}
			dbOutputVal = Calc.Calculate(dbOutputVal, (Calc.Formula1)int.Parse(this.arg1), valData);
			Calc.Formula1 formula2 = (Calc.Formula1)int.Parse(this.arg1);
			int num = 0;
			if (formula2 != Calc.Formula1.Equal)
			{
				list.Insert(num++, answerVal.o.ToString());
			}
			list.Insert(num++, Calc.Cast(formula2));
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x000F4A00 File Offset: 0x000F2E00
		public static string Cast(Calc.Formula1 formula)
		{
			switch (formula)
			{
			case Calc.Formula1.Equal:
				return "=";
			case Calc.Formula1.PlusEqual:
				return "+=";
			case Calc.Formula1.MinusEqual:
				return "-=";
			case Calc.Formula1.AstaEqual:
				return "*=";
			case Calc.Formula1.SlashEqual:
				return "/=";
			default:
				return string.Empty;
			}
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x000F4A4F File Offset: 0x000F2E4F
		public static string Cast(Calc.Formula2 formula)
		{
			switch (formula)
			{
			case Calc.Formula2.Plus:
				return "+";
			case Calc.Formula2.Minus:
				return "-";
			case Calc.Formula2.Asta:
				return "*";
			case Calc.Formula2.Slash:
				return "/";
			default:
				return string.Empty;
			}
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x000F4A8C File Offset: 0x000F2E8C
		public override void Convert(string fileName, ref string[] args)
		{
			int cnt = 0;
			StringBuilder stringBuilder = new StringBuilder();
			if (args.IsNullOrEmpty(cnt++))
			{
				stringBuilder.AppendLine("Answer none");
			}
			if (!Calc.Formula1Cast(ref args[cnt]))
			{
				stringBuilder.AppendLine("Formula1 Cast Error");
			}
			Action action = delegate()
			{
				cnt += 2;
			};
			action();
			while (!args.IsNullOrEmpty(cnt))
			{
				if (!Calc.Formula2Cast(ref args[cnt]))
				{
					stringBuilder.AppendLine("Formula2 Cast Error");
				}
				action();
				if (args.IsNullOrEmpty(cnt - 1))
				{
					stringBuilder.AppendLine("Formula2 Value none");
				}
			}
			if (stringBuilder.Length > 0)
			{
			}
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x000F4B78 File Offset: 0x000F2F78
		private static bool Formula1Cast(ref string arg)
		{
			string temp = arg;
			int num = Illusion.Utils.Value.Check(Illusion.Utils.Enum<Calc.Formula1>.Length, (int index) => temp == Calc.Cast((Calc.Formula1)index));
			bool result = true;
			if (num == -1)
			{
				num = 0;
				result = false;
			}
			arg = num.ToString();
			return result;
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x000F4BC8 File Offset: 0x000F2FC8
		private static bool Formula2Cast(ref string arg)
		{
			string temp = arg;
			int num = Illusion.Utils.Value.Check(Illusion.Utils.Enum<Calc.Formula2>.Length, (int index) => temp == Calc.Cast((Calc.Formula2)index));
			bool result = true;
			if (num == -1)
			{
				num = 0;
				result = false;
			}
			arg = num.ToString();
			return result;
		}

		// Token: 0x04002AA9 RID: 10921
		private int refCnt;

		// Token: 0x04002AAA RID: 10922
		private string answer;

		// Token: 0x04002AAB RID: 10923
		private string arg1;

		// Token: 0x04002AAC RID: 10924
		private List<string> argsList = new List<string>();

		// Token: 0x020006D4 RID: 1748
		public enum Formula1
		{
			// Token: 0x04002AAE RID: 10926
			Equal,
			// Token: 0x04002AAF RID: 10927
			PlusEqual,
			// Token: 0x04002AB0 RID: 10928
			MinusEqual,
			// Token: 0x04002AB1 RID: 10929
			AstaEqual,
			// Token: 0x04002AB2 RID: 10930
			SlashEqual
		}

		// Token: 0x020006D5 RID: 1749
		public enum Formula2
		{
			// Token: 0x04002AB4 RID: 10932
			Plus,
			// Token: 0x04002AB5 RID: 10933
			Minus,
			// Token: 0x04002AB6 RID: 10934
			Asta,
			// Token: 0x04002AB7 RID: 10935
			Slash
		}
	}
}
