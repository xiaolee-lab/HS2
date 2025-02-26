using System;

namespace ADV.Commands.Chara
{
	// Token: 0x0200071D RID: 1821
	public class LookNeck : CommandBase
	{
		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06002B5E RID: 11102 RVA: 0x000FB341 File Offset: 0x000F9741
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"Ptn",
					"Rate"
				};
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06002B5F RID: 11103 RVA: 0x000FB364 File Offset: 0x000F9764
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString(),
					"0",
					"1"
				};
			}
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x000FB3A0 File Offset: 0x000F97A0
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			int ptn = int.Parse(this.args[num++]);
			float rate = 1f;
			this.args.SafeProc(num++, delegate(string s)
			{
				rate = float.Parse(s);
			});
			CharaData chara = base.scenario.commandController.GetChara(no);
			chara.chaCtrl.ChangeLookNeckPtn(ptn, rate);
		}
	}
}
