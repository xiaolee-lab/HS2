using System;
using Illusion;
using Illusion.Extensions;

namespace ADV.Commands.Base
{
	// Token: 0x020006DE RID: 1758
	public class IF : CommandBase
	{
		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06002A05 RID: 10757 RVA: 0x000F54F4 File Offset: 0x000F38F4
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Left",
					"Center",
					"Right",
					"True",
					"False"
				};
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06002A06 RID: 10758 RVA: 0x000F5524 File Offset: 0x000F3924
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"a",
					string.Empty,
					"b",
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x000F5554 File Offset: 0x000F3954
		public override void Convert(string fileName, ref string[] args)
		{
			if (!IF.Cast(ref args[1]))
			{
			}
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x000F5568 File Offset: 0x000F3968
		private static bool Cast(ref string arg)
		{
			int num = Illusion.Utils.Comparer.STR.Check(arg);
			bool result = true;
			if (num == -1)
			{
				if (arg.Compare("check", true))
				{
					num = Illusion.Utils.Enum<Illusion.Utils.Comparer.Type>.Length;
				}
				else
				{
					num = 0;
					result = false;
				}
			}
			arg = num.ToString();
			return result;
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x000F55BC File Offset: 0x000F39BC
		public override void ConvertBeforeArgsProc()
		{
			base.ConvertBeforeArgsProc();
			int num = 0;
			this.left = this.args[num++];
			this.center = this.args[num++];
			this.right = this.args[num++];
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x000F5608 File Offset: 0x000F3A08
		public override void Do()
		{
			base.Do();
			ValData valData = null;
			ValData valData2 = null;
			int num = int.Parse(this.center);
			if (num < Illusion.Utils.Enum<Illusion.Utils.Comparer.Type>.Length)
			{
				if (!base.scenario.Vars.TryGetValue(this.left, out valData))
				{
					valData = new ValData(VAR.CheckLiterals(this.left));
				}
				if (!base.scenario.Vars.TryGetValue(this.right, out valData2))
				{
					valData2 = new ValData(valData.Convert(this.right));
				}
			}
			bool flag;
			switch (num)
			{
			case 0:
				flag = valData.o.Equals(valData2.o);
				break;
			case 1:
				flag = !valData.o.Equals(valData2.o);
				break;
			case 2:
				flag = (valData >= valData2);
				break;
			case 3:
				flag = (valData <= valData2);
				break;
			case 4:
				flag = (valData > valData2);
				break;
			case 5:
				flag = (valData < valData2);
				break;
			default:
				flag = base.scenario.Vars.ContainsKey(this.left);
				break;
			}
			this.jumpTrue = this.args[3];
			this.jumpFalse = this.args[4];
			string text = (!flag) ? this.jumpFalse : this.jumpTrue;
			if (!text.IsNullOrEmpty())
			{
				base.scenario.SearchTagJumpOrOpenFile(text, base.localLine);
			}
		}

		// Token: 0x04002AC8 RID: 10952
		private const string compStr = "check";

		// Token: 0x04002AC9 RID: 10953
		private string left;

		// Token: 0x04002ACA RID: 10954
		private string center;

		// Token: 0x04002ACB RID: 10955
		private string right;

		// Token: 0x04002ACC RID: 10956
		private string jumpTrue;

		// Token: 0x04002ACD RID: 10957
		private string jumpFalse;
	}
}
