using System;

namespace ADV.Commands.Chara
{
	// Token: 0x02000718 RID: 1816
	public class ItemAnime : CommandBase
	{
		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06002B4A RID: 11082 RVA: 0x000FACDD File Offset: 0x000F90DD
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"ItemNo",
					"Bundle",
					"Asset",
					"State"
				};
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06002B4B RID: 11083 RVA: 0x000FAD10 File Offset: 0x000F9110
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString(),
					"0",
					string.Empty,
					string.Empty,
					"Idle"
				};
			}
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000FAD5C File Offset: 0x000F915C
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			int key = int.Parse(this.args[num++]);
			string bundle = this.args[num++];
			string asset = this.args[num++];
			string state = this.args[num++];
			CharaData chara = base.scenario.commandController.GetChara(no);
			chara.itemDic[key].LoadAnimator(bundle, asset, state);
		}
	}
}
