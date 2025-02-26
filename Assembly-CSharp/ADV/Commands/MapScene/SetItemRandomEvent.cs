using System;
using System.Collections.Generic;
using AIProject;
using AIProject.SaveData;
using Manager;

namespace ADV.Commands.MapScene
{
	// Token: 0x0200075F RID: 1887
	public class SetItemRandomEvent : CommandBase
	{
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002C70 RID: 11376 RVA: 0x000FF1C0 File Offset: 0x000FD5C0
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"ID"
				};
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002C71 RID: 11377 RVA: 0x000FF1D0 File Offset: 0x000FD5D0
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

		// Token: 0x06002C72 RID: 11378 RVA: 0x000FF1E0 File Offset: 0x000FD5E0
		public override void Do()
		{
			base.Do();
			int num = 0;
			int id = int.Parse(this.args[num++]);
			Tuple<StuffItemInfo, int> advRandomEventItemInfo = Singleton<Resources>.Instance.GameInfo.GetAdvRandomEventItemInfo(id);
			StuffItemInfo item = advRandomEventItemInfo.Item1;
			int item2 = advRandomEventItemInfo.Item2;
			base.scenario.AddItemVars(item, item2);
			StuffItem addItem = new StuffItem(item.CategoryID, item.ID, item2);
			List<StuffItem> itemList = Singleton<Map>.Instance.Player.PlayerData.ItemList;
			int capacity = Singleton<Map>.Instance.Player.PlayerData.InventorySlotMax;
			if (this.AddItem(itemList, capacity, addItem, item2))
			{
				return;
			}
			itemList = Singleton<Game>.Instance.Environment.ItemListInStorage;
			capacity = Singleton<Resources>.Instance.DefinePack.ItemBoxCapacityDefines.StorageCapacity;
			if (this.AddItem(itemList, capacity, addItem, item2))
			{
				return;
			}
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x000FF2C6 File Offset: 0x000FD6C6
		private bool AddItem(List<StuffItem> itemList, int capacity, StuffItem addItem, int num)
		{
			if (!itemList.CanAddItem(capacity, addItem, num))
			{
				return false;
			}
			itemList.AddItem(addItem);
			return true;
		}
	}
}
