using System;
using System.Collections.Generic;
using AIProject;
using AIProject.SaveData;
using Manager;

namespace ADV.Commands.MapScene
{
	// Token: 0x02000756 RID: 1878
	public class AddItemInPlayer : CommandBase
	{
		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06002C44 RID: 11332 RVA: 0x000FEA01 File Offset: 0x000FCE01
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"ItemHash",
					"Num"
				};
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06002C45 RID: 11333 RVA: 0x000FEA19 File Offset: 0x000FCE19
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"-1",
					"1"
				};
			}
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x000FEA34 File Offset: 0x000FCE34
		public override void Do()
		{
			base.Do();
			int num = 0;
			int nameHash = int.Parse(this.args[num++]);
			StuffItemInfo stuffItemInfo = Singleton<Resources>.Instance.GameInfo.FindItemInfo(nameHash);
			if (stuffItemInfo == null)
			{
				return;
			}
			int num2;
			if (!int.TryParse(this.args[num++], out num2))
			{
				num2 = 1;
			}
			StuffItem addItem = new StuffItem(stuffItemInfo.CategoryID, stuffItemInfo.ID, num2);
			List<StuffItem> itemList = Singleton<Map>.Instance.Player.PlayerData.ItemList;
			int capacity = Singleton<Map>.Instance.Player.PlayerData.InventorySlotMax;
			if (this.AddItem(itemList, capacity, addItem, num2))
			{
				return;
			}
			itemList = Singleton<Game>.Instance.Environment.ItemListInStorage;
			capacity = Singleton<Resources>.Instance.DefinePack.ItemBoxCapacityDefines.StorageCapacity;
			if (this.AddItem(itemList, capacity, addItem, num2))
			{
				return;
			}
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x000FEB1A File Offset: 0x000FCF1A
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
