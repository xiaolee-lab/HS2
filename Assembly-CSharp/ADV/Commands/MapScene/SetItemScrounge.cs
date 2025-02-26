using System;
using AIProject;
using AIProject.SaveData;
using Manager;

namespace ADV.Commands.MapScene
{
	// Token: 0x0200075D RID: 1885
	public class SetItemScrounge : CommandBase
	{
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002C68 RID: 11368 RVA: 0x000FF078 File Offset: 0x000FD478
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

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002C69 RID: 11369 RVA: 0x000FF088 File Offset: 0x000FD488
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

		// Token: 0x06002C6A RID: 11370 RVA: 0x000FF098 File Offset: 0x000FD498
		public override void Do()
		{
			base.Do();
			int num = 0;
			CharaData chara = this.GetChara(ref num);
			if (chara == null)
			{
				return;
			}
			Tuple<StuffItemInfo, int> advScroungeInfo = Singleton<Resources>.Instance.GameInfo.GetAdvScroungeInfo(chara.chaCtrl);
			StuffItemInfo item = advScroungeInfo.Item1;
			int item2 = advScroungeInfo.Item2;
			base.scenario.AddItemVars(item, item2);
			chara.data.agentActor.AgentData.ItemScrounge.Set(new StuffItem[]
			{
				new StuffItem(item.CategoryID, item.ID, item2)
			});
		}
	}
}
