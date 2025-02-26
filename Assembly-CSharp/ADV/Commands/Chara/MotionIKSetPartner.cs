using System;
using System.Linq;

namespace ADV.Commands.Chara
{
	// Token: 0x02000725 RID: 1829
	public class MotionIKSetPartner : CommandBase
	{
		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06002B7E RID: 11134 RVA: 0x000FBBF3 File Offset: 0x000F9FF3
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"Partner"
				};
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06002B7F RID: 11135 RVA: 0x000FBC0C File Offset: 0x000FA00C
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString(),
					"0"
				};
			}
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x000FBC40 File Offset: 0x000FA040
		public override void Do()
		{
			base.Do();
			int cnt = 0;
			int no = int.Parse(this.args[cnt++]);
			CharaData chara = base.scenario.commandController.GetChara(no);
			MotionIK[] partners = (from s in CommandBase.RemoveArgsEmpty(base.GetArgToSplitLast(cnt))
			select base.scenario.commandController.GetChara(int.Parse(s)) into charaData
			select charaData.ikMotion.motionIK).ToArray<MotionIK>();
			chara.ikMotion.motionIK.SetPartners(partners);
		}
	}
}
