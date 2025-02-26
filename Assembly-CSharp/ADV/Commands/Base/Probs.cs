using System;
using System.Collections.Generic;
using System.Linq;
using Illusion;

namespace ADV.Commands.Base
{
	// Token: 0x020006EF RID: 1775
	public class Probs : CommandBase
	{
		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06002A4F RID: 10831 RVA: 0x000F6459 File Offset: 0x000F4859
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"ProbTag"
				};
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06002A50 RID: 10832 RVA: 0x000F6469 File Offset: 0x000F4869
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"Prob,Tag"
				};
			}
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x000F647C File Offset: 0x000F487C
		public override void Do()
		{
			base.Do();
			Dictionary<string, int> targetDict = (from s in this.args
			select s.Split(new char[]
			{
				','
			})).ToDictionary((string[] v) => base.scenario.ReplaceVars(v[1]), delegate(string[] v)
			{
				int result;
				int.TryParse(base.scenario.ReplaceVars(v[0]), out result);
				return result;
			});
			string jump = Illusion.Utils.ProbabilityCalclator.DetermineFromDict<string>(targetDict);
			base.scenario.SearchTagJumpOrOpenFile(jump, base.localLine);
		}
	}
}
