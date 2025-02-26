using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject;
using AIProject.SaveData;
using AIProject.UI;
using AIProject.UI.Viewer;
using Manager;

namespace ADV.Commands.MapScene
{
	// Token: 0x0200075C RID: 1884
	public abstract class InventoryBase : CommandBase
	{
		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06002C60 RID: 11360 RVA: 0x000FED40 File Offset: 0x000FD140
		// (set) Token: 0x06002C61 RID: 11361 RVA: 0x000FED48 File Offset: 0x000FD148
		private protected bool isClose { protected get; private set; }

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06002C62 RID: 11362 RVA: 0x000FED51 File Offset: 0x000FD151
		// (set) Token: 0x06002C63 RID: 11363 RVA: 0x000FED59 File Offset: 0x000FD159
		private protected StuffItem Item { protected get; private set; }

		// Token: 0x06002C64 RID: 11364 RVA: 0x000FED64 File Offset: 0x000FD164
		protected void OpenInventory(int cnt, List<StuffItem> itemList)
		{
			MapUIContainer.ReserveSystemMenuMode(SystemMenuUI.MenuMode.InventoryEnter);
			SystemMenuUI systemUI = MapUIContainer.SystemMenuUI;
			InventoryUIController inventoryUI = systemUI.InventoryEnterUI;
			inventoryUI.isConfirm = true;
			inventoryUI.CountViewerVisible(true);
			inventoryUI.itemList = (() => itemList);
			inventoryUI.SetItemFilter(InventoryBase.ToFilter(base.GetArgToSplitLastTable(cnt)));
			inventoryUI.OnSubmit = delegate(StuffItem item)
			{
				this.Item = item;
				InventoryUIController inventoryUI = inventoryUI;
				if (inventoryUI != null)
				{
					inventoryUI.OnClose();
				}
			};
			inventoryUI.OnClose = delegate()
			{
				inventoryUI.OnSubmit = null;
				inventoryUI.IsActiveControl = false;
				systemUI.IsActiveControl = false;
				Singleton<Input>.Instance.FocusLevel = 0;
				Singleton<Input>.Instance.ReserveState(Input.ValidType.UI);
				this.isClose = true;
				inventoryUI.OnClose = null;
			};
			MapUIContainer.SetActiveSystemMenuUI(true);
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x000FEE22 File Offset: 0x000FD222
		private static InventoryFacadeViewer.ItemFilter[] ToFilter(string[][] arrays)
		{
			return arrays.Select(delegate(string[] x)
			{
				int category = int.Parse(x[0]);
				IEnumerable<string> source = x.Skip(1);
				if (InventoryBase.<>f__mg$cache0 == null)
				{
					InventoryBase.<>f__mg$cache0 = new Func<string, int>(int.Parse);
				}
				return new InventoryFacadeViewer.ItemFilter(category, source.Select(InventoryBase.<>f__mg$cache0).ToArray<int>());
			}).ToArray<InventoryFacadeViewer.ItemFilter>();
		}

		// Token: 0x04002B56 RID: 11094
		[CompilerGenerated]
		private static Func<string, int> <>f__mg$cache0;
	}
}
