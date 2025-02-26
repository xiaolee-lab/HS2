using System;
using Illusion;

namespace ADV.Commands.Base
{
	// Token: 0x020006EE RID: 1774
	public class Prob : CommandBase
	{
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06002A4B RID: 10827 RVA: 0x000F63A5 File Offset: 0x000F47A5
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Prob",
					"True",
					"False"
				};
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06002A4C RID: 10828 RVA: 0x000F63C5 File Offset: 0x000F47C5
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"100",
					"tagA",
					"tagB"
				};
			}
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000F63E8 File Offset: 0x000F47E8
		public override void Do()
		{
			base.Do();
			int num = 0;
			float percent = float.Parse(this.args[num++]);
			string text = this.args[num++];
			string text2 = this.args[num++];
			string jump = (!Illusion.Utils.ProbabilityCalclator.DetectFromPercent(percent)) ? text2 : text;
			base.scenario.SearchTagJumpOrOpenFile(jump, base.localLine);
		}
	}
}
