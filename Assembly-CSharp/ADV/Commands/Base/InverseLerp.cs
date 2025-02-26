using System;
using UnityEngine;

namespace ADV.Commands.Base
{
	// Token: 0x020006E5 RID: 1765
	public class InverseLerp : CommandBase
	{
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06002A27 RID: 10791 RVA: 0x000F5D85 File Offset: 0x000F4185
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Answer",
					"A",
					"B",
					"Value"
				};
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06002A28 RID: 10792 RVA: 0x000F5DAD File Offset: 0x000F41AD
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

		// Token: 0x06002A29 RID: 10793 RVA: 0x000F5DD5 File Offset: 0x000F41D5
		public override void ConvertBeforeArgsProc()
		{
			base.ConvertBeforeArgsProc();
			this.answer = this.args[0];
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x000F5DEC File Offset: 0x000F41EC
		public override void Do()
		{
			base.Do();
			int num = 1;
			base.scenario.Vars[this.answer] = new ValData(Mathf.InverseLerp(float.Parse(this.args[num++]), float.Parse(this.args[num++]), float.Parse(this.args[num++])));
		}

		// Token: 0x04002AD1 RID: 10961
		private string answer;
	}
}
