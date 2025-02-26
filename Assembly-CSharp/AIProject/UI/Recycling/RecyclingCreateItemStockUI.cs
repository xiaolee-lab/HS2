using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.ColorDefine;
using AIProject.SaveData;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI.Recycling
{
	// Token: 0x02000E9F RID: 3743
	public class RecyclingCreateItemStockUI : UIBehaviour
	{
		// Token: 0x170017C1 RID: 6081
		// (get) Token: 0x0600790E RID: 30990 RVA: 0x003303FC File Offset: 0x0032E7FC
		public ItemListUI ItemListUI
		{
			[CompilerGenerated]
			get
			{
				return this._itemListUI;
			}
		}

		// Token: 0x170017C2 RID: 6082
		// (get) Token: 0x0600790F RID: 30991 RVA: 0x00330404 File Offset: 0x0032E804
		public ItemListController ListController { get; } = new ItemListController(PanelType.CreatedItem);

		// Token: 0x170017C3 RID: 6083
		// (get) Token: 0x06007910 RID: 30992 RVA: 0x0033040C File Offset: 0x0032E80C
		// (set) Token: 0x06007911 RID: 30993 RVA: 0x00330414 File Offset: 0x0032E814
		public Action<bool> ChangeCreateableEvent { get; set; }

		// Token: 0x170017C4 RID: 6084
		// (get) Token: 0x06007912 RID: 30994 RVA: 0x0033041D File Offset: 0x0032E81D
		// (set) Token: 0x06007913 RID: 30995 RVA: 0x00330425 File Offset: 0x0032E825
		public int ItemSlotMax { get; private set; }

		// Token: 0x170017C5 RID: 6085
		// (get) Token: 0x06007914 RID: 30996 RVA: 0x0033042E File Offset: 0x0032E82E
		public Button AllGetButton
		{
			[CompilerGenerated]
			get
			{
				return this._allGetButton;
			}
		}

		// Token: 0x170017C6 RID: 6086
		// (get) Token: 0x06007915 RID: 30997 RVA: 0x00330436 File Offset: 0x0032E836
		public IObservable<Unit> OnClickDeleteSubmit
		{
			[CompilerGenerated]
			get
			{
				return this._deleteSubmitButton.OnClickAsObservable();
			}
		}

		// Token: 0x170017C7 RID: 6087
		// (get) Token: 0x06007916 RID: 30998 RVA: 0x00330443 File Offset: 0x0032E843
		public Subject<StuffItem> OnAddCreateItem { get; } = new Subject<StuffItem>();

		// Token: 0x170017C8 RID: 6088
		// (get) Token: 0x06007917 RID: 30999 RVA: 0x0033044B File Offset: 0x0032E84B
		public bool IsCreateable
		{
			get
			{
				return this.ItemList != null && this.ItemList.Count < this.ItemSlotMax;
			}
		}

		// Token: 0x170017C9 RID: 6089
		// (get) Token: 0x06007918 RID: 31000 RVA: 0x0033046D File Offset: 0x0032E86D
		public bool IsCurrentNode
		{
			[CompilerGenerated]
			get
			{
				return this._itemListUI.CurrentOption != null;
			}
		}

		// Token: 0x170017CA RID: 6090
		// (get) Token: 0x06007919 RID: 31001 RVA: 0x00330480 File Offset: 0x0032E880
		// (set) Token: 0x0600791A RID: 31002 RVA: 0x00330488 File Offset: 0x0032E888
		public List<StuffItem> ItemList { get; private set; } = new List<StuffItem>();

		// Token: 0x0600791B RID: 31003 RVA: 0x00330491 File Offset: 0x0032E891
		protected override void Awake()
		{
			base.Awake();
			if (this._recyclingUI == null)
			{
				this._recyclingUI = base.GetComponentInParent<RecyclingUI>();
			}
			this.ListController.Bind(this._itemListUI);
		}

		// Token: 0x0600791C RID: 31004 RVA: 0x003304C8 File Offset: 0x0032E8C8
		protected override void Start()
		{
			base.Start();
			if (Singleton<Manager.Resources>.IsInstance())
			{
				DefinePack.RecyclingSetting recycling = Singleton<Manager.Resources>.Instance.DefinePack.Recycling;
				this.ItemSlotMax = recycling.CreateItemCapacity;
			}
			else
			{
				this.ItemSlotMax = 0;
			}
			(from _ in this._deleteSubmitButton.OnClickAsObservable()
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this.OnDeleteOKClick();
				this._recyclingUI.PlaySE(SoundPack.SystemSE.OK_L);
			}).AddTo(this);
			(from _ in this._deleteCancelButton.OnClickAsObservable()
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this.OnDeleteCancelClick();
				this._recyclingUI.PlaySE(SoundPack.SystemSE.Cancel);
			}).AddTo(this);
			IObservable<Unit> source = from _ in this._oneGetButton.OnClickAsObservable()
			where base.isActiveAndEnabled
			select _;
			IObservable<Unit> source2 = from _ in this._allGetButton.OnClickAsObservable()
			where base.isActiveAndEnabled
			select _;
			Observable.Merge<Unit>(new IObservable<Unit>[]
			{
				source.Do(delegate(Unit _)
				{
					this.OnClickOneGet();
				}),
				source2.Do(delegate(Unit _)
				{
					this.OnClickAllGet();
				})
			}).Subscribe(delegate(Unit _)
			{
				this._recyclingUI.PlaySE(SoundPack.SystemSE.OK_S);
			}).AddTo(this);
			ItemListController listController = this.ListController;
			listController.RefreshEvent = (Action)Delegate.Combine(listController.RefreshEvent, new Action(delegate()
			{
				this.RefreshUI();
			}));
			this._itemListUI.CurrentChanged += delegate(int currentID, ItemNodeUI nodeUI)
			{
				this.RefreshOneGetButtonInteractable(nodeUI);
			};
		}

		// Token: 0x0600791D RID: 31005 RVA: 0x0033063A File Offset: 0x0032EA3A
		public void SettingUI(List<StuffItem> itemList)
		{
			this.ItemList = itemList;
			this.ListController.SetItemList(itemList);
			this.ListController.Create(itemList);
		}

		// Token: 0x0600791E RID: 31006 RVA: 0x0033065B File Offset: 0x0032EA5B
		public void RefreshUI()
		{
			this._itemListUI.Refresh();
			this.SetActiveNotCreateableText(!this.IsCreateable);
			this.RefreshCountText();
		}

		// Token: 0x0600791F RID: 31007 RVA: 0x0033067D File Offset: 0x0032EA7D
		public void SetActiveNotCreateableText(bool flag)
		{
			if (this.SetActive(this._notCreateableText, flag))
			{
				Action<bool> changeCreateableEvent = this.ChangeCreateableEvent;
				if (changeCreateableEvent != null)
				{
					changeCreateableEvent(!flag);
				}
			}
		}

		// Token: 0x06007920 RID: 31008 RVA: 0x003306AC File Offset: 0x0032EAAC
		public void RefreshCountText()
		{
			this._maxCountText.text = string.Format("{0}", this.ItemSlotMax);
			int count = this.ItemList.Count;
			this._countText.text = string.Format("{0}", count);
			Colors colors = (count >= this.ItemSlotMax) ? Colors.Red : Colors.White;
			this._countText.color = Define.Get(colors);
		}

		// Token: 0x06007921 RID: 31009 RVA: 0x00330725 File Offset: 0x0032EB25
		public void RefreshOneGetButtonInteractable()
		{
			this.RefreshOneGetButtonInteractable(this._itemListUI.CurrentOption);
		}

		// Token: 0x06007922 RID: 31010 RVA: 0x00330738 File Offset: 0x0032EB38
		public void RefreshOneGetButtonInteractable(ItemNodeUI nodeUI)
		{
			bool active = !(nodeUI == null) && this.AddableItemToInventory(nodeUI.Item);
			this.Setinteractable(this._oneGetButton, active);
		}

		// Token: 0x06007923 RID: 31011 RVA: 0x00330771 File Offset: 0x0032EB71
		public void AddItem(StuffItem item)
		{
			this.ListController.AddItem(item);
			this.OnAddCreateItem.OnNext(item);
		}

		// Token: 0x06007924 RID: 31012 RVA: 0x0033078C File Offset: 0x0032EB8C
		private void OnDeleteOKClick()
		{
			if (this._deleteRequestUI.IsActiveControl)
			{
				this._deleteRequestUI.DoClose();
			}
			RecyclingInfoPanelUI infoPanelUI = this._recyclingUI.InfoPanelUI;
			if (infoPanelUI == null || !infoPanelUI.IsNumberInput)
			{
				return;
			}
			UnityEx.ValueTuple<ItemListController, ItemListController, ItemNodeUI, int, ButtonType> itemInfo = infoPanelUI.GetItemInfo();
			ItemNodeUI item = itemInfo.Item3;
			StuffItem stuffItem = (!(item != null)) ? null : item.Item;
			if (item == null || stuffItem == null)
			{
				return;
			}
			int item2 = itemInfo.Item4;
			StuffItem stuffItem2 = new StuffItem(stuffItem);
			stuffItem2.Count = Mathf.Min(stuffItem.Count, infoPanelUI.InputNumber);
			bool flag = stuffItem.Count <= stuffItem2.Count;
			this.ListController.RemoveItem(item2, stuffItem2);
			if (flag)
			{
				this._itemListUI.ForceSetNonSelect();
				infoPanelUI.DetachItem();
			}
		}

		// Token: 0x06007925 RID: 31013 RVA: 0x00330878 File Offset: 0x0032EC78
		private void OnDeleteCancelClick()
		{
			if (this._deleteRequestUI.IsActiveControl)
			{
				this._deleteRequestUI.DoClose();
			}
		}

		// Token: 0x170017CB RID: 6091
		// (get) Token: 0x06007926 RID: 31014 RVA: 0x00330895 File Offset: 0x0032EC95
		private List<StuffItem> AddFailedList { get; } = new List<StuffItem>();

		// Token: 0x06007927 RID: 31015 RVA: 0x003308A0 File Offset: 0x0032ECA0
		private bool AddableItemToInventory(StuffItem item)
		{
			if (item == null)
			{
				return false;
			}
			List<StuffItem> list = ListPool<StuffItem>.Get();
			list.Add(item);
			bool result = this.AddableItemToInventory(list);
			ListPool<StuffItem>.Release(list);
			return result;
		}

		// Token: 0x06007928 RID: 31016 RVA: 0x003308D4 File Offset: 0x0032ECD4
		private bool AddableItemToInventory(List<StuffItem> itemList)
		{
			if (itemList.IsNullOrEmpty<StuffItem>())
			{
				return false;
			}
			List<RecyclingInventoryFacadeViewer> vieweres = this.GetVieweres();
			if (vieweres.IsNullOrEmpty<RecyclingInventoryFacadeViewer>())
			{
				this.ReturnVieweres(vieweres);
				return false;
			}
			bool flag = false;
			foreach (RecyclingInventoryFacadeViewer recyclingInventoryFacadeViewer in vieweres)
			{
				if (recyclingInventoryFacadeViewer != null)
				{
					foreach (StuffItem stuffItem in this.ItemList)
					{
						if (stuffItem != null)
						{
							if (flag = (0 < recyclingInventoryFacadeViewer.ListController.PossibleCount(stuffItem)))
							{
								break;
							}
						}
					}
					if (flag)
					{
						break;
					}
				}
			}
			this.ReturnVieweres(vieweres);
			return flag;
		}

		// Token: 0x06007929 RID: 31017 RVA: 0x003309DC File Offset: 0x0032EDDC
		public bool CheckInventoryEmpty()
		{
			if (this.ItemList.IsNullOrEmpty<StuffItem>())
			{
				return true;
			}
			RecyclingInventoryFacadeViewer[] inventoryUIs = this._recyclingUI.InventoryUIs;
			foreach (RecyclingInventoryFacadeViewer recyclingInventoryFacadeViewer in inventoryUIs)
			{
				if (recyclingInventoryFacadeViewer != null && 0 < recyclingInventoryFacadeViewer.ListController.EmptySlotNum())
				{
					return true;
				}
			}
			return this.AddableItemToInventory(this.ItemList);
		}

		// Token: 0x0600792A RID: 31018 RVA: 0x00330A48 File Offset: 0x0032EE48
		private void OnClickOneGet()
		{
			if (this.ItemList.IsNullOrEmpty<StuffItem>())
			{
				return;
			}
			ItemNodeUI currentOption = this._itemListUI.CurrentOption;
			StuffItem stuffItem = (!(currentOption != null)) ? null : currentOption.Item;
			if (stuffItem == null)
			{
				return;
			}
			List<StuffItem> list = ListPool<StuffItem>.Get();
			list.Add(stuffItem);
			List<RecyclingInventoryFacadeViewer> vieweres = this.GetVieweres();
			foreach (RecyclingInventoryFacadeViewer viewer in vieweres)
			{
				this.GetItemAction(list, viewer);
				if (!this.AddFailedList.Contains(stuffItem))
				{
					this.ItemList.Remove(stuffItem);
					this._itemListUI.RemoveItemNode(this._itemListUI.CurrentID);
					this._itemListUI.ForceSetNonSelect();
					stuffItem = null;
					this._recyclingUI.InfoPanelUI.DetachItem();
				}
				if (stuffItem == null)
				{
					break;
				}
			}
			this.AddFailedList.Clear();
			this.ReturnVieweres(vieweres);
			ListPool<StuffItem>.Release(list);
			Action refreshEvent = this.ListController.RefreshEvent;
			if (refreshEvent != null)
			{
				refreshEvent();
			}
			this.RefreshOneGetButtonInteractable();
		}

		// Token: 0x0600792B RID: 31019 RVA: 0x00330B88 File Offset: 0x0032EF88
		private void OnClickAllGet()
		{
			if (this.ItemList.IsNullOrEmpty<StuffItem>())
			{
				return;
			}
			List<RecyclingInventoryFacadeViewer> vieweres = this.GetVieweres();
			foreach (RecyclingInventoryFacadeViewer viewer in vieweres)
			{
				this.GetItemAction(this.ItemList, viewer);
				for (int i = 0; i < this.ItemList.Count; i++)
				{
					StuffItem item = this.ItemList[i];
					if (!this.AddFailedList.Contains(item))
					{
						this.ItemList.Remove(item);
						this._itemListUI.RemoveItemNode(this._itemListUI.SearchIndex(item));
						i--;
					}
				}
				if (this.ItemList.IsNullOrEmpty<StuffItem>())
				{
					break;
				}
			}
			Action refreshEvent = this.ListController.RefreshEvent;
			if (refreshEvent != null)
			{
				refreshEvent();
			}
			if (this._itemListUI.SearchIndex(this._itemListUI.CurrentOption) == -1)
			{
				this._itemListUI.ForceSetNonSelect();
				this._recyclingUI.InfoPanelUI.DetachItem();
			}
			this.AddFailedList.Clear();
			this.ReturnVieweres(vieweres);
		}

		// Token: 0x0600792C RID: 31020 RVA: 0x00330CDC File Offset: 0x0032F0DC
		public void GetItemAction(List<StuffItem> itemList, RecyclingInventoryFacadeViewer viewer)
		{
			this.AddFailedList.Clear();
			if (itemList.IsNullOrEmpty<StuffItem>() || viewer == null)
			{
				return;
			}
			foreach (StuffItem item in itemList)
			{
				if (!viewer.AddItemCondition(item))
				{
					this.AddFailedList.Add(item);
				}
			}
			Action refreshEvent = viewer.ListController.RefreshEvent;
			if (refreshEvent != null)
			{
				refreshEvent();
			}
		}

		// Token: 0x0600792D RID: 31021 RVA: 0x00330D7C File Offset: 0x0032F17C
		private List<RecyclingInventoryFacadeViewer> GetVieweres()
		{
			List<RecyclingInventoryFacadeViewer> list = ListPool<RecyclingInventoryFacadeViewer>.Get();
			list.AddRange(this._recyclingUI.InventoryUIs);
			RecyclingInventoryFacadeViewer selectedInventoryUI = this._recyclingUI.SelectedInventoryUI;
			if (selectedInventoryUI != null)
			{
				list.Remove(selectedInventoryUI);
				list.Insert(0, selectedInventoryUI);
			}
			list.RemoveAll((RecyclingInventoryFacadeViewer x) => x == null);
			return list;
		}

		// Token: 0x0600792E RID: 31022 RVA: 0x00330DE7 File Offset: 0x0032F1E7
		private void ReturnVieweres(List<RecyclingInventoryFacadeViewer> list)
		{
			ListPool<RecyclingInventoryFacadeViewer>.Release(list);
		}

		// Token: 0x0600792F RID: 31023 RVA: 0x00330DEF File Offset: 0x0032F1EF
		private bool SetActive(GameObject obj, bool flag)
		{
			if (obj != null && obj.activeSelf != flag)
			{
				obj.SetActive(flag);
				return true;
			}
			return false;
		}

		// Token: 0x06007930 RID: 31024 RVA: 0x00330E13 File Offset: 0x0032F213
		private bool SetActive(Component com, bool flag)
		{
			if (com != null && com.gameObject != null && com.gameObject.activeSelf != flag)
			{
				com.gameObject.SetActive(flag);
				return true;
			}
			return false;
		}

		// Token: 0x06007931 RID: 31025 RVA: 0x00330E52 File Offset: 0x0032F252
		private void Setinteractable(Selectable obj, bool active)
		{
			if (obj != null && obj.interactable != active)
			{
				obj.interactable = active;
			}
		}

		// Token: 0x040061C8 RID: 25032
		[SerializeField]
		private RecyclingUI _recyclingUI;

		// Token: 0x040061C9 RID: 25033
		[SerializeField]
		private RecyclingItemDeleteRequestUI _deleteRequestUI;

		// Token: 0x040061CA RID: 25034
		[SerializeField]
		private Text _countText;

		// Token: 0x040061CB RID: 25035
		[SerializeField]
		private Text _maxCountText;

		// Token: 0x040061CC RID: 25036
		[SerializeField]
		private Text _notCreateableText;

		// Token: 0x040061CD RID: 25037
		[SerializeField]
		private Button _allGetButton;

		// Token: 0x040061CE RID: 25038
		[SerializeField]
		private Button _oneGetButton;

		// Token: 0x040061CF RID: 25039
		[SerializeField]
		private Button _deleteSubmitButton;

		// Token: 0x040061D0 RID: 25040
		[SerializeField]
		private Button _deleteCancelButton;

		// Token: 0x040061D1 RID: 25041
		[SerializeField]
		private ItemListUI _itemListUI;
	}
}
