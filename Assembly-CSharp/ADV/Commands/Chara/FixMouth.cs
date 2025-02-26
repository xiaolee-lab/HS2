using System;

namespace ADV.Commands.Chara
{
	// Token: 0x02000715 RID: 1813
	public class FixMouth : CommandBase
	{
		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x000FA83C File Offset: 0x000F8C3C
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

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06002B3F RID: 11071 RVA: 0x000FA854 File Offset: 0x000F8C54
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

		// Token: 0x06002B40 RID: 11072 RVA: 0x000FA888 File Offset: 0x000F8C88
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			CharaData chara = base.scenario.commandController.GetChara(no);
			this.args.SafeProc(num++, delegate(string s)
			{
				chara.chaCtrl.ChangeMouthFixed(bool.Parse(s));
			});
		}
	}
}
