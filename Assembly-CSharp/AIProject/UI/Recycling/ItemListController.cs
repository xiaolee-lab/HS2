using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using Illusion.Game.Extensions;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.UI.Recycling
{
	// Token: 0x02000EAC RID: 3756
	public class ItemListController
	{
		// Token: 0x06007A65 RID: 31333 RVA: 0x003374D9 File Offset: 0x003358D9
		public ItemListController()
		{
		}

		// Token: 0x06007A66 RID: 31334 RVA: 0x003374E1 File Offset: 0x003358E1
		public ItemListController(PanelType panelType)
		{
			this.PanelType = panelType;
		}

		// Token: 0x06007A67 RID: 31335 RVA: 0x003374F0 File Offset: 0x003358F0
		public void SetPanelType(PanelType panelType)
		{
			this.PanelType = panelType;
		}

		// Token: 0x06007A68 RID: 31336 RVA: 0x003374F9 File Offset: 0x003358F9
		public void SetItemList(List<StuffItem> itemList)
		{
			this.ItemList = itemList;
		}

		// Token: 0x06007A69 RID: 31337 RVA: 0x00337502 File Offset: 0x00335902
		public void SetInventoryUI(RecyclingInventoryFacadeViewer inventoryUI)
		{
			this.InventoryUI = inventoryUI;
		}

		// Token: 0x1700180E RID: 6158
		// (get) Token: 0x06007A6A RID: 31338 RVA: 0x0033750B File Offset: 0x0033590B
		// (set) Token: 0x06007A6B RID: 31339 RVA: 0x00337513 File Offset: 0x00335913
		public PanelType PanelType { get; private set; }

		// Token: 0x1700180F RID: 6159
		// (get) Token: 0x06007A6C RID: 31340 RVA: 0x0033751C File Offset: 0x0033591C
		// (set) Token: 0x06007A6D RID: 31341 RVA: 0x00337524 File Offset: 0x00335924
		public List<StuffItem> ItemList { get; private set; }

		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x06007A6E RID: 31342 RVA: 0x0033752D File Offset: 0x0033592D
		// (set) Token: 0x06007A6F RID: 31343 RVA: 0x00337535 File Offset: 0x00335935
		public RecyclingInventoryFacadeViewer InventoryUI { get; private set; }

		// Token: 0x140000CD RID: 205
		// (add) Token: 0x06007A70 RID: 31344 RVA: 0x00337540 File Offset: 0x00335940
		// (remove) Token: 0x06007A71 RID: 31345 RVA: 0x00337578 File Offset: 0x00335978
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int, ItemNodeUI> DoubleClick;

		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x06007A72 RID: 31346 RVA: 0x003375AE File Offset: 0x003359AE
		public ItemListUI itemListUI
		{
			[CompilerGenerated]
			get
			{
				return this._itemListUI;
			}
		}

		// Token: 0x06007A73 RID: 31347 RVA: 0x003375B6 File Offset: 0x003359B6
		public void Bind(ItemListUI itemListUI)
		{
			this._itemListUI = itemListUI;
		}

		// Token: 0x06007A74 RID: 31348 RVA: 0x003375BF File Offset: 0x003359BF
		public void Clear()
		{
			this._itemListUI.ClearItems();
			if (this.RefreshEvent != null)
			{
				this.RefreshEvent();
			}
		}

		// Token: 0x06007A75 RID: 31349 RVA: 0x003375E4 File Offset: 0x003359E4
		public void Create(IReadOnlyCollection<StuffItem> itemCollection)
		{
			this._itemListUI.ClearItems();
			foreach (StuffItem item in itemCollection)
			{
				this.ItemListAddNode(item, new ExtraPadding(item, this));
			}
			if (this.RefreshEvent != null)
			{
				this.RefreshEvent();
			}
		}

		// Token: 0x06007A76 RID: 31350 RVA: 0x00337664 File Offset: 0x00335A64
		public int EmptySlotNum()
		{
			int num;
			switch (this.PanelType)
			{
			case PanelType.Pouch:
			{
				WorldData worldData = (!Singleton<Game>.IsInstance()) ? null : Singleton<Game>.Instance.WorldData;
				num = ((worldData == null) ? 0 : worldData.PlayerData.InventorySlotMax);
				goto IL_E1;
			}
			case PanelType.Chest:
			{
				Manager.Resources resources = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance;
				num = ((!(resources != null)) ? 0 : resources.DefinePack.ItemBoxCapacityDefines.StorageCapacity);
				goto IL_E1;
			}
			case PanelType.CreatedItem:
			{
				Manager.Resources resources2 = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance;
				num = ((!(resources2 != null)) ? 0 : resources2.DefinePack.Recycling.CreateItemCapacity);
				goto IL_E1;
			}
			}
			return 0;
			IL_E1:
			if (num <= 0)
			{
				return 0;
			}
			if (this.ItemList.IsNullOrEmpty<StuffItem>())
			{
				return num;
			}
			return Mathf.Max(0, num - this.ItemList.Count);
		}

		// Token: 0x06007A77 RID: 31351 RVA: 0x00337780 File Offset: 0x00335B80
		public int PossibleCount(StuffItem item)
		{
			if (item == null)
			{
				return 0;
			}
			int num = 0;
			switch (this.PanelType)
			{
			case PanelType.Pouch:
			{
				WorldData worldData = (!Singleton<Game>.IsInstance()) ? null : Singleton<Game>.Instance.WorldData;
				num = ((worldData == null) ? 0 : worldData.PlayerData.InventorySlotMax);
				goto IL_12A;
			}
			case PanelType.Chest:
			{
				Manager.Resources resources = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance;
				num = ((!(resources != null)) ? 0 : resources.DefinePack.ItemBoxCapacityDefines.StorageCapacity);
				goto IL_12A;
			}
			case PanelType.DecidedItem:
			{
				Manager.Resources resources2 = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance;
				num = ((!(resources2 != null)) ? 0 : resources2.DefinePack.Recycling.DecidedItemCapacity);
				goto IL_12A;
			}
			case PanelType.CreatedItem:
			{
				Manager.Resources resources3 = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance;
				num = ((!(resources3 != null)) ? 0 : resources3.DefinePack.Recycling.CreateItemCapacity);
				goto IL_12A;
			}
			}
			return 0;
			IL_12A:
			int num2 = 0;
			if (this.PanelType == PanelType.DecidedItem)
			{
				int num3 = 0;
				foreach (StuffItem stuffItem in this.ItemList)
				{
					if (stuffItem != null)
					{
						num3 += stuffItem.Count;
					}
				}
				num2 = num - num3;
				num2 = Mathf.Min(num2, item.Count);
			}
			else
			{
				int count = item.Count;
				num2 = ((!this.ItemList.CanAddItem(num, item, count, out num2)) ? num2 : count);
			}
			return Mathf.Max(num2, 0);
		}

		// Token: 0x06007A78 RID: 31352 RVA: 0x00337974 File Offset: 0x00335D74
		public int AddItem(StuffItem item)
		{
			if (this.InventoryUI != null)
			{
				return this.InventoryUI.AddItem(item);
			}
			if (this.ItemList == null)
			{
				return 0;
			}
			int num = this.PossibleCount(item);
			if (num <= 0)
			{
				return 0;
			}
			this.ItemList.AddItem(new StuffItem(item), num);
			item.Count -= num;
			List<StuffItem> list = this.ItemList.FindItems(item).ToList<StuffItem>();
			foreach (ItemNodeUI itemNodeUI in this._itemListUI)
			{
				list.Remove(itemNodeUI.Item);
			}
			foreach (StuffItem item2 in list)
			{
				this.ItemListAddNode(item2, new ExtraPadding(item2, this));
			}
			if (this.RefreshEvent != null)
			{
				this.RefreshEvent();
			}
			return num;
		}

		// Token: 0x06007A79 RID: 31353 RVA: 0x00337AA8 File Offset: 0x00335EA8
		public int RemoveItem(int sel, StuffItem item)
		{
			ItemNodeUI node = this._itemListUI.GetNode(sel);
			node.Item.Count -= item.Count;
			int count = node.Item.Count;
			if (count <= 0)
			{
				this.ItemList.Remove(node.Item);
				this._itemListUI.RemoveItemNode(sel);
				this._itemListUI.ForceSetNonSelect();
			}
			if (this.RefreshEvent != null)
			{
				this.RefreshEvent();
			}
			return count;
		}

		// Token: 0x06007A7A RID: 31354 RVA: 0x00337B30 File Offset: 0x00335F30
		private ItemNodeUI ItemListAddNode(StuffItem item, ExtraPadding padding)
		{
			int index = this._itemListUI.SearchNotUsedIndex;
			ItemNodeUI node = this._itemListUI.AddItemNode(index, item);
			if (node != null)
			{
				node.extraData = padding;
				node.OnClick.AsObservable().DoubleInterval(250f, false).Subscribe(delegate(IList<double> _)
				{
					if (this.DoubleClick != null)
					{
						this.DoubleClick(index, node);
					}
				}).AddTo(node);
			}
			return node;
		}

		// Token: 0x04006260 RID: 25184
		private ItemListUI _itemListUI;

		// Token: 0x04006261 RID: 25185
		[NonSerialized]
		public Action RefreshEvent;
	}
}
