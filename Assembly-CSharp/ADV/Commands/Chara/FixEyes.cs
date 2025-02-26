using System;

namespace ADV.Commands.Chara
{
	// Token: 0x02000714 RID: 1812
	public class FixEyes : CommandBase
	{
		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x000FA763 File Offset: 0x000F8B63
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"Fix"
				};
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x000FA77C File Offset: 0x000F8B7C
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

		// Token: 0x06002B3C RID: 11068 RVA: 0x000FA7B0 File Offset: 0x000F8BB0
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			CharaData chara = base.scenario.commandController.GetChara(no);
			this.args.SafeProc(num++, delegate(string s)
			{
				chara.chaCtrl.ChangeEyesBlinkFlag(!bool.Parse(s));
			});
		}
	}
}
