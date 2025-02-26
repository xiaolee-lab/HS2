using System;
using System.Collections.Generic;
using UnityEngine;

namespace ADV.Commands.Base
{
	// Token: 0x020006E6 RID: 1766
	public class LerpV3 : CommandBase
	{
		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06002A2C RID: 10796 RVA: 0x000F5E61 File Offset: 0x000F4261
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

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06002A2D RID: 10797 RVA: 0x000F5E89 File Offset: 0x000F4289
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"Answer",
					"0",
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x000F5EB1 File Offset: 0x000F42B1
		public override void ConvertBeforeArgsProc()
		{
			base.ConvertBeforeArgsProc();
			this.answer = this.args[0];
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x000F5EC8 File Offset: 0x000F42C8
		public override void Do()
		{
			base.Do();
			int num = 1;
			float shape = float.Parse(this.args[num++]);
			Dictionary<string, Vector3> v3Dic = base.scenario.commandController.V3Dic;
			Vector3 min = v3Dic[this.args[num++]];
			Vector3 max = v3Dic[this.args[num++]];
			v3Dic[this.answer] = MathfEx.GetShapeLerpPositionValue(shape, min, max);
		}

		// Token: 0x04002AD2 RID: 10962
		private string answer;
	}
}
