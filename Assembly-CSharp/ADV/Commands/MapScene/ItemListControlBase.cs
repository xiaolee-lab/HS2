using System;
using System.Collections.Generic;
using AIProject;
using AIProject.SaveData;
using Manager;

namespace ADV.Commands.MapScene
{
	// Token: 0x02000759 RID: 1881
	public abstract class ItemListControlBase : CommandBase
	{
		// Token: 0x06002C53 RID: 11347
		protected abstract void ItemListProc(StuffItemInfo itemInfo, List<StuffItem> itemList, StuffItem stuffItem);

		// Token: 0x06002C54 RID: 11348 RVA: 0x000FEB40 File Offset: 0x000FCF40
		protected void SetItem(ref int cnt)
		{
			CharaData chara = this.GetChara(ref cnt);
			if (chara == null)
			{
				return;
			}
			int nameHash = int.Parse(this.args[cnt++]);
			StuffItemInfo stuffItemInfo = Singleton<Resources>.Instance.GameInfo.FindItemInfo(nameHash);
			if (stuffItemInfo == null)
			{
				return;
			}
			int count;
			if (!int.TryParse(this.args[cnt++], out count))
			{
				count = 1;
			}
			this.ItemListProc(stuffItemInfo, chara.data.characterInfo.ItemList, new StuffItem(stuffItemInfo.CategoryID, stuffItemInfo.ID, count));
		}
	}
}
