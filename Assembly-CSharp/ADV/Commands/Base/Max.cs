using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ADV.Commands.Base
{
	// Token: 0x020006D8 RID: 1752
	public class Max : CommandBase
	{
		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060029E5 RID: 10725 RVA: 0x000F4EBE File Offset: 0x000F32BE
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

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060029E6 RID: 10726 RVA: 0x000F4EDE File Offset: 0x000F32DE
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

		// Token: 0x060029E7 RID: 10727 RVA: 0x000F4EFE File Offset: 0x000F32FE
		public override void ConvertBeforeArgsProc()
		{
			base.ConvertBeforeArgsProc();
			this.answer = this.args[0];
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x000F4F14 File Offset: 0x000F3314
		public override void Do()
		{
			base.Do();
			int cnt = 1;
			IEnumerable<string> argToSplitLast = base.GetArgToSplitLast(cnt);
			if (Max.<>f__mg$cache0 == null)
			{
				Max.<>f__mg$cache0 = new Func<string, float>(float.Parse);
			}
			float num = Mathf.Max(argToSplitLast.Select(Max.<>f__mg$cache0).ToArray<float>());
			base.scenario.Vars[this.answer] = new ValData(num);
		}

		// Token: 0x04002ABB RID: 10939
		private string answer;

		// Token: 0x04002ABC RID: 10940
		[CompilerGenerated]
		private static Func<string, float> <>f__mg$cache0;
	}
}
