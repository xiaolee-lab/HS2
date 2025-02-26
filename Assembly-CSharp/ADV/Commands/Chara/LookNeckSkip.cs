using System;

namespace ADV.Commands.Chara
{
	// Token: 0x02000720 RID: 1824
	public class LookNeckSkip : CommandBase
	{
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06002B6A RID: 11114 RVA: 0x000FB7C5 File Offset: 0x000F9BC5
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"isSkip"
				};
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06002B6B RID: 11115 RVA: 0x000FB7E0 File Offset: 0x000F9BE0
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString(),
					bool.FalseString
				};
			}
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x000FB814 File Offset: 0x000F9C14
		public override void Do()
		{
			base.Do();
			int num = 0;
			CharaData chara = base.scenario.commandController.GetChara(int.Parse(this.args[num++]));
			chara.chaCtrl.neckLookCtrl.neckLookScript.skipCalc = bool.Parse(this.args[num++]);
		}
	}
}
