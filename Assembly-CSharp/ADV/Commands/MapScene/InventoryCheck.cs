using System;
using System.Collections.Generic;
using AIProject;
using AIProject.SaveData;
using Manager;

namespace ADV.Commands.MapScene
{
	// Token: 0x02000751 RID: 1873
	public class InventoryCheck : CommandBase
	{
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06002C2F RID: 11311 RVA: 0x000FE6A0 File Offset: 0x000FCAA0
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"True",
					"False",
					"Type"
				};
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06002C30 RID: 11312 RVA: 0x000FE6C0 File Offset: 0x000FCAC0
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					string.Empty,
					"0"
				};
			}
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000FE6E0 File Offset: 0x000FCAE0
		public override void Do()
		{
			base.Do();
			List<StuffItem> self;
			int capacity;
			switch (int.Parse(this.args[2]))
			{
			case 0:
				self = Singleton<Map>.Instance.Player.PlayerData.ItemList;
				capacity = Singleton<Map>.Instance.Player.PlayerData.InventorySlotMax;
				break;
			case 1:
				self = Singleton<Game>.Instance.Environment.ItemListInStorage;
				capacity = Singleton<Resources>.Instance.DefinePack.ItemBoxCapacityDefines.StorageCapacity;
				break;
			case 2:
				self = Singleton<Game>.Instance.Environment.ItemListInPantry;
				capacity = Singleton<Resources>.Instance.DefinePack.ItemBoxCapacityDefines.PantryCapacity;
				break;
			default:
				return;
			}
			bool flag = self.CanAddItem(capacity, null, 1);
			string text = (!flag) ? this.args[1] : this.args[0];
			if (!text.IsNullOrEmpty())
			{
				base.scenario.SearchTagJumpOrOpenFile(text, base.localLine);
			}
		}
	}
}
