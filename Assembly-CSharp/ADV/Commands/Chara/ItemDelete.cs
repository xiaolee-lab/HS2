using System;

namespace ADV.Commands.Chara
{
	// Token: 0x02000717 RID: 1815
	public class ItemDelete : CommandBase
	{
		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06002B46 RID: 11078 RVA: 0x000FAC1D File Offset: 0x000F901D
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"ItemNo"
				};
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06002B47 RID: 11079 RVA: 0x000FAC38 File Offset: 0x000F9038
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

		// Token: 0x06002B48 RID: 11080 RVA: 0x000FAC6C File Offset: 0x000F906C
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			int key = int.Parse(this.args[num++]);
			CharaData chara = base.scenario.commandController.GetChara(no);
			chara.itemDic[key].Delete();
			chara.itemDic.Remove(key);
		}
	}
}
