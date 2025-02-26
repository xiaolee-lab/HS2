using System;
using UnityEngine;

namespace ADV.Commands.Base
{
	// Token: 0x020006D6 RID: 1750
	public class Clamp : CommandBase
	{
		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x000F4D17 File Offset: 0x000F3117
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Answer",
					"Value",
					"Min",
					"Max"
				};
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060029DC RID: 10716 RVA: 0x000F4D3F File Offset: 0x000F313F
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

		// Token: 0x060029DD RID: 10717 RVA: 0x000F4D67 File Offset: 0x000F3167
		public override void ConvertBeforeArgsProc()
		{
			base.ConvertBeforeArgsProc();
			this.answer = this.args[0];
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000F4D80 File Offset: 0x000F3180
		public override void Do()
		{
			base.Do();
			int num = 1;
			base.scenario.Vars[this.answer] = new ValData(Mathf.Clamp(float.Parse(this.args[num++]), float.Parse(this.args[num++]), float.Parse(this.args[num++])));
		}

		// Token: 0x04002AB8 RID: 10936
		private string answer;
	}
}
