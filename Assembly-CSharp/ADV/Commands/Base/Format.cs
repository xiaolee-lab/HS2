using System;
using System.Collections.Generic;

namespace ADV.Commands.Base
{
	// Token: 0x020006DC RID: 1756
	public class Format : CommandBase
	{
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060029FB RID: 10747 RVA: 0x000F52CA File Offset: 0x000F36CA
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Variable",
					"Format",
					"Args"
				};
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060029FC RID: 10748 RVA: 0x000F52EA File Offset: 0x000F36EA
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					"{0:00}",
					"1"
				};
			}
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x000F530C File Offset: 0x000F370C
		public override void ConvertBeforeArgsProc()
		{
			base.ConvertBeforeArgsProc();
			int num = 0;
			this.name = this.args[num++];
			this.format = this.args[num++];
			string[] argToSplitLast = base.GetArgToSplitLast(num++);
			Dictionary<string, ValData> vars = base.scenario.Vars;
			int num2 = -1;
			while (++num2 < argToSplitLast.Length)
			{
				ValData valData;
				if (vars.TryGetValue(argToSplitLast[num2], out valData))
				{
					this.parameters.Add(valData.o);
				}
				else
				{
					this.parameters.Add(argToSplitLast[num2]);
				}
			}
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x000F53A6 File Offset: 0x000F37A6
		public override void Do()
		{
			base.Do();
			base.scenario.Vars[this.name] = new ValData(string.Format(this.format, this.parameters.ToArray()));
		}

		// Token: 0x04002AC3 RID: 10947
		public string name;

		// Token: 0x04002AC4 RID: 10948
		public string format;

		// Token: 0x04002AC5 RID: 10949
		private List<object> parameters = new List<object>();
	}
}
