using System;
using System.Collections.Generic;

namespace ADV.Commands.Base
{
	// Token: 0x020006DD RID: 1757
	public class FormatVAR : CommandBase
	{
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06002A00 RID: 10752 RVA: 0x000F53F2 File Offset: 0x000F37F2
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

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06002A01 RID: 10753 RVA: 0x000F5412 File Offset: 0x000F3812
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

		// Token: 0x06002A02 RID: 10754 RVA: 0x000F5434 File Offset: 0x000F3834
		public override void ConvertBeforeArgsProc()
		{
			base.ConvertBeforeArgsProc();
			this.name = this.args[0];
			string[] argToSplitLast = base.GetArgToSplitLast(2);
			Dictionary<string, ValData> vars = base.scenario.Vars;
			int num = -1;
			while (++num < argToSplitLast.Length)
			{
				ValData valData;
				if (vars.TryGetValue(argToSplitLast[num], out valData))
				{
					this.parameters.Add(valData.o);
				}
				else
				{
					this.parameters.Add(argToSplitLast[num]);
				}
			}
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x000F54B1 File Offset: 0x000F38B1
		public override void Do()
		{
			base.Do();
			base.scenario.Vars[this.name] = new ValData(string.Format(this.args[1], this.parameters.ToArray()));
		}

		// Token: 0x04002AC6 RID: 10950
		public string name;

		// Token: 0x04002AC7 RID: 10951
		private List<object> parameters = new List<object>();
	}
}
