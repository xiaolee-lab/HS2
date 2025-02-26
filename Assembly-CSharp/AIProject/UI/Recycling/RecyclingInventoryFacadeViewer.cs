using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using AIProject.UI.Viewer;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI.Recycling
{
	// Token: 0x02000EA4 RID: 3748
	[Serializable]
	public class RecyclingInventoryFacadeViewer : InventoryFacadeViewer
	{
		// Token: 0x170017E6 RID: 6118
		// (get) Token: 0x060079D1 RID: 31185 RVA: 0x0033396D File Offset: 0x00331D6D
		public PanelType PanelType
		{
			[CompilerGenerated]
			get
			{
				return this._panelType;
			}
		}

		// Token: 0x170017E7 RID: 6119
		// (get) Token: 0x060079D2 RID: 31186 RVA: 0x00333975 File Offset: 0x00331D75
		public InventoryType InventoryType
		{
			[CompilerGenerated]
			get
			{
				return this._inventoryType;
			}
		}

		// Token: 0x170017E8 RID: 6120
		// (get) Token: 0x060079D3 RID: 31187 RVA: 0x0033397D File Offset: 0x00331D7D
		public CanvasGroup RootCanvasGroup
		{
			[CompilerGenerated]
			get
			{
				return this._rootCanvasGroup;
			}
		}

		// Token: 0x170017E9 RID: 6121
		// (get) Token: 0x060079D4 RID: 31188 RVA: 0x00333985 File Offset: 0x00331D85
		public ItemSortUI ItemSortUI
		{
			[CompilerGenerated]
			get
			{
				return base.viewer.sortUI;
			}
		}

		// Token: 0x170017EA RID: 6122
		// (get) Token: 0x060079D5 RID: 31189 RVA: 0x00333992 File Offset: 0x00331D92
		public Toggle Sorter
		{
			[CompilerGenerated]
			get
			{
				return base.viewer.sorter;
			}
		}

		// Token: 0x170017EB RID: 6123
		// (get) Token: 0x060079D6 RID: 31190 RVA: 0x0033399F File Offset: 0x00331D9F
		public Button SortButton
		{
			[CompilerGenerated]
			get
			{
				return base.viewer.sortButton;
			}
		}

		// Token: 0x170017EC RID: 6124
		// (get) Token: 0x060079D7 RID: 31191 RVA: 0x003339AC File Offset: 0x00331DAC
		public ItemListController ListController { get; } = new ItemListController();

		// Token: 0x060079D8 RID: 31192 RVA: 0x003339B4 File Offset: 0x00331DB4
		public override IEnumerator Initialize()
		{
			this.ListController.SetPanelType(this._panelType);
			List<StuffItem> itemList = null;
			InventoryType inventoryType = this._inventoryType;
			if (inventoryType != InventoryType.Pouch)
			{
				if (inventoryType == InventoryType.Chest)
				{
					List<StuffItem> list;
					if (Singleton<Game>.IsInstance())
					{
						AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
						list = ((environment != null) ? environment.ItemListInStorage : null);
					}
					else
					{
						list = null;
					}
					itemList = list;
				}
			}
			else
			{
				List<StuffItem> list2;
				if (Singleton<Game>.IsInstance())
				{
					WorldData worldData = Singleton<Game>.Instance.WorldData;
					if (worldData == null)
					{
						list2 = null;
					}
					else
					{
						PlayerData playerData = worldData.PlayerData;
						list2 = ((playerData != null) ? playerData.ItemList : null);
					}
				}
				else
				{
					list2 = null;
				}
				itemList = list2;
			}
			if (itemList != null)
			{
				base.SetItemList(itemList);
			}
			yield return base.Initialize();
			Vector3 position = base.viewer.sortButton.transform.position;
			position.x -= 30f;
			base.viewer.sortButton.transform.position = position;
			this.ListController.Bind(base.viewer.itemListUI);
			this.ListController.SetItemList(itemList);
			yield break;
		}

		// Token: 0x060079D9 RID: 31193 RVA: 0x003339D0 File Offset: 0x00331DD0
		public new int AddItem(StuffItem item)
		{
			int num = item.Count;
			int num2;
			if (!base.itemList.CanAddItem(base.slotCounter.y, item, num, out num2))
			{
				num = num2;
			}
			if (num <= 0)
			{
				return 0;
			}
			base.itemList.AddItem(new StuffItem(item), num);
			item.Count -= num;
			List<StuffItem> list = base.itemList.FindItems(item).ToList<StuffItem>();
			foreach (ItemNodeUI itemNodeUI in base.itemListUI)
			{
				list.Remove(itemNodeUI.Item);
			}
			foreach (StuffItem item2 in list)
			{
				this.ItemListAddNode(base.itemListUI.SearchNotUsedIndex, item2);
			}
			this.ItemListNodeFilter(base.categoryUI.CategoryID, true);
			return num;
		}

		// Token: 0x060079DA RID: 31194 RVA: 0x00333B00 File Offset: 0x00331F00
		public bool AddItemCondition(StuffItem item)
		{
			int num = item.Count;
			int num2;
			if (!base.itemList.CanAddItem(base.slotCounter.y, item, num, out num2))
			{
				num = num2;
			}
			if (num <= 0)
			{
				return false;
			}
			base.itemList.AddItem(new StuffItem(item), num);
			item.Count -= num;
			List<StuffItem> list = base.itemList.FindItems(item).ToList<StuffItem>();
			foreach (ItemNodeUI itemNodeUI in base.itemListUI)
			{
				list.Remove(itemNodeUI.Item);
			}
			foreach (StuffItem item2 in list)
			{
				this.ItemListAddNode(base.itemListUI.SearchNotUsedIndex, item2);
			}
			this.ItemListNodeFilter(base.categoryUI.CategoryID, true);
			return item.Count <= 0;
		}

		// Token: 0x060079DB RID: 31195 RVA: 0x00333C3C File Offset: 0x0033203C
		public void SetVisible(bool visible)
		{
			if (this._rootCanvasGroup != null)
			{
				this._rootCanvasGroup.alpha = ((!visible) ? 0f : 1f);
				this._rootCanvasGroup.blocksRaycasts = visible;
				this._rootCanvasGroup.interactable = visible;
			}
			else if (base.viewer != null && base.viewer.gameObject.activeSelf != visible)
			{
				base.viewer.gameObject.SetActive(visible);
			}
			if (!visible && base.viewer != null && base.viewer.sortUI != null)
			{
				base.viewer.sortUI.Close();
			}
		}

		// Token: 0x04006210 RID: 25104
		[SerializeField]
		private InventoryType _inventoryType;

		// Token: 0x04006211 RID: 25105
		[SerializeField]
		private CanvasGroup _rootCanvasGroup;

		// Token: 0x04006212 RID: 25106
		[SerializeField]
		private PanelType _panelType;
	}
}
