using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ADV.Commands.Base
{
	// Token: 0x020006D7 RID: 1751
	public class Min : CommandBase
	{
		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060029E0 RID: 10720 RVA: 0x000F4DF5 File Offset: 0x000F31F5
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Answer",
					"A",
					"B"
				};
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060029E1 RID: 10721 RVA: 0x000F4E15 File Offset: 0x000F3215
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"Answer",
					"0",
					"0"
				};
			}
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000F4E35 File Offset: 0x000F3235
		public override void ConvertBeforeArgsProc()
		{
			base.ConvertBeforeArgsProc();
			this.answer = this.args[0];
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x000F4E4C File Offset: 0x000F324C
		public override void Do()
		{
			base.Do();
			int cnt = 1;
			IEnumerable<string> argToSplitLast = base.GetArgToSplitLast(cnt);
			if (Min.<>f__mg$cache0 == null)
			{
				Min.<>f__mg$cache0 = new Func<string, float>(float.Parse);
			}
			float num = Mathf.Min(argToSplitLast.Select(Min.<>f__mg$cache0).ToArray<float>());
			base.scenario.Vars[this.answer] = new ValData(num);
		}

		// Token: 0x04002AB9 RID: 10937
		private string answer;

		// Token: 0x04002ABA RID: 10938
		[CompilerGenerated]
		private static Func<string, float> <>f__mg$cache0;
	}
}
