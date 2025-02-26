using System;
using System.Collections.Generic;
using AIProject;
using AIProject.SaveData;

namespace ADV.Commands.MapScene
{
	// Token: 0x02000757 RID: 1879
	public class AddItem : ItemListControlBase
	{
		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06002C49 RID: 11337 RVA: 0x000FEBDB File Offset: 0x000FCFDB
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

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002C4A RID: 11338 RVA: 0x000FEBFB File Offset: 0x000FCFFB
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

		// Token: 0x06002C4B RID: 11339 RVA: 0x000FEC1B File Offset: 0x000FD01B
		protected override void ItemListProc(StuffItemInfo itemInfo, List<StuffItem> itemList, StuffItem stuffItem)
		{
			itemList.AddItem(stuffItem);
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x000FEC28 File Offset: 0x000FD028
		public override void Do()
		{
			base.Do();
			int num = 0;
			base.SetItem(ref num);
		}
	}
}
