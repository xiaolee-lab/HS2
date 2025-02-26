using System;

namespace ADV.Commands.Chara
{
	// Token: 0x0200071A RID: 1818
	public class LookEyes : CommandBase
	{
		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06002B52 RID: 11090 RVA: 0x000FAF19 File Offset: 0x000F9319
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"Ptn"
				};
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002B53 RID: 11091 RVA: 0x000FAF34 File Offset: 0x000F9334
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

		// Token: 0x06002B54 RID: 11092 RVA: 0x000FAF68 File Offset: 0x000F9368
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			int ptn = int.Parse(this.args[num++]);
			CharaData chara = base.scenario.commandController.GetChara(no);
			chara.chaCtrl.ChangeLookEyesPtn(ptn);
		}
	}
}
