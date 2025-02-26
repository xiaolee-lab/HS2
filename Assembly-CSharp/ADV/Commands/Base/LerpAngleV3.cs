using System;
using System.Collections.Generic;
using UnityEngine;

namespace ADV.Commands.Base
{
	// Token: 0x020006E7 RID: 1767
	public class LerpAngleV3 : CommandBase
	{
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06002A31 RID: 10801 RVA: 0x000F5F44 File Offset: 0x000F4344
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

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06002A32 RID: 10802 RVA: 0x000F5F6C File Offset: 0x000F436C
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

		// Token: 0x06002A33 RID: 10803 RVA: 0x000F5F94 File Offset: 0x000F4394
		public override void ConvertBeforeArgsProc()
		{
			base.ConvertBeforeArgsProc();
			this.answer = this.args[0];
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x000F5FAC File Offset: 0x000F43AC
		public override void Do()
		{
			base.Do();
			int num = 1;
			float shape = float.Parse(this.args[num++]);
			Dictionary<string, Vector3> v3Dic = base.scenario.commandController.V3Dic;
			Vector3 min = v3Dic[this.args[num++]];
			Vector3 max = v3Dic[this.args[num++]];
			v3Dic[this.answer] = MathfEx.GetShapeLerpAngleValue(shape, min, max);
		}

		// Token: 0x04002AD3 RID: 10963
		private string answer;
	}
}
