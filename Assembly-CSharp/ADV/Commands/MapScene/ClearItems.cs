using System;

namespace ADV.Commands.MapScene
{
	// Token: 0x02000755 RID: 1877
	public class ClearItems : CommandBase
	{
		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002C40 RID: 11328 RVA: 0x000FE9A1 File Offset: 0x000FCDA1
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No"
				};
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002C41 RID: 11329 RVA: 0x000FE9B1 File Offset: 0x000FCDB1
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"0"
				};
			}
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000FE9C4 File Offset: 0x000FCDC4
		public override void Do()
		{
			base.Do();
			int num = 0;
			CharaData chara = this.GetChara(ref num);
			if (chara == null)
			{
				return;
			}
			chara.data.actor.ClearItems();
		}
	}
}
