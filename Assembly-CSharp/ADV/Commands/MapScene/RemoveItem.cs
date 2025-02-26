using System;
using System.Collections.Generic;
using AIProject;
using AIProject.SaveData;

namespace ADV.Commands.MapScene
{
	// Token: 0x02000758 RID: 1880
	public class RemoveItem : ItemListControlBase
	{
		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002C4E RID: 11342 RVA: 0x000FEC4D File Offset: 0x000FD04D
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"ItemHash",
					"Num"
				};
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002C4F RID: 11343 RVA: 0x000FEC6D File Offset: 0x000FD06D
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"-1",
					"-1",
					"1"
				};
			}
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x000FEC8D File Offset: 0x000FD08D
		protected override void ItemListProc(StuffItemInfo itemInfo, List<StuffItem> itemList, StuffItem stuffItem)
		{
			itemList.RemoveItem(stuffItem);
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x000FEC98 File Offset: 0x000FD098
		public override void Do()
		{
			base.Do();
			int num = 0;
			base.SetItem(ref num);
		}
	}
}
