using System;
using UnityEngine;

namespace ADV.Commands.Base
{
	// Token: 0x020006E4 RID: 1764
	public class LerpAngle : CommandBase
	{
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06002A22 RID: 10786 RVA: 0x000F5CAA File Offset: 0x000F40AA
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Answer",
					"A",
					"B",
					"T"
				};
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06002A23 RID: 10787 RVA: 0x000F5CD2 File Offset: 0x000F40D2
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"Answer",
					"0",
					"0",
					"0"
				};
			}
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x000F5CFA File Offset: 0x000F40FA
		public override void ConvertBeforeArgsProc()
		{
			base.ConvertBeforeArgsProc();
			this.answer = this.args[0];
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x000F5D10 File Offset: 0x000F4110
		public override void Do()
		{
			base.Do();
			int num = 1;
			base.scenario.Vars[this.answer] = new ValData(Mathf.LerpAngle(float.Parse(this.args[num++]), float.Parse(this.args[num++]), float.Parse(this.args[num++])));
		}

		// Token: 0x04002AD0 RID: 10960
		private string answer;
	}
}
