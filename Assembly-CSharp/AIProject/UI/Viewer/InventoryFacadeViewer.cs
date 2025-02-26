using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using Illusion.Game.Extensions;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000EC6 RID: 3782
	[Serializable]
	public class InventoryFacadeViewer
	{
		// Token: 0x1700186F RID: 6255
		// (set) Token: 0x06007BE0 RID: 31712 RVA: 0x00321E56 File Offset: 0x00320256
		public bool sorterVisible
		{
			set
			{
				this._sorterVisible = value;
			}
		}

		// Token: 0x17001870 RID: 6256
		// (set) Token: 0x06007BE1 RID: 31713 RVA: 0x00321E5F File Offset: 0x0032025F
		public bool counterVisible
		{
			set
			{
				this._counterVisible = value;
			}
		}

		// Token: 0x17001871 RID: 6257
		// (get) Token: 0x06007BE2 RID: 31714 RVA: 0x00321E68 File Offset: 0x00320268
		// (set) Token: 0x06007BE3 RID: 31715 RVA: 0x00321E70 File Offset: 0x00320270
		public Action<InventoryFacadeViewer.DoubleClickData> ItemNodeOnDoubleClick { get; set; }

		// Token: 0x17001872 RID: 6258
		// (get) Token: 0x06007BE4 RID: 31716 RVA: 0x00321E79 File Offset: 0x00320279
		public InventoryViewer viewer
		{
			[CompilerGenerated]
			get
			{
				return this._viewer;
			}
		}

		// Token: 0x17001873 RID: 6259
		// (get) Token: 0x06007BE5 RID: 31717 RVA: 0x00321E81 File Offset: 0x00320281
		public ItemFilterCategoryUI categoryUI
		{
			[CompilerGenerated]
			get
			{
				return this._viewer.categoryUI;
			}
		}

		// Token: 0x17001874 RID: 6260
		// (get) Token: 0x06007BE6 RID: 31718 RVA: 0x00321E8E File Offset: 0x0032028E
		public ItemListUI itemListUI
		{
			[CompilerGenerated]
			get
			{
				return this._viewer.itemListUI;
			}
		}

		// Token: 0x17001875 RID: 6261
		// (get) Token: 0x06007BE7 RID: 31719 RVA: 0x00321E9B File Offset: 0x0032029B
		public Image cursor
		{
			[CompilerGenerated]
			get
			{
				return this._viewer.cursor;
			}
		}

		// Token: 0x17001876 RID: 6262
		// (get) Token: 0x06007BE8 RID: 31720 RVA: 0x00321EA8 File Offset: 0x003202A8
		public ConditionalTextXtoYViewer slotCounter
		{
			[CompilerGenerated]
			get
			{
				return this._viewer.slotCounter;
			}
		}

		// Token: 0x17001877 RID: 6263
		// (get) Token: 0x06007BE9 RID: 31721 RVA: 0x00321EB5 File Offset: 0x003202B5
		// (set) Token: 0x06007BEA RID: 31722 RVA: 0x00321EBD File Offset: 0x003202BD
		public bool initialized { get; private set; }

		// Token: 0x06007BEB RID: 31723 RVA: 0x00321EC6 File Offset: 0x003202C6
		public void SetItemList(List<StuffItem> itemList)
		{
			this.itemList = itemList;
		}

		// Token: 0x17001878 RID: 6264
		// (get) Token: 0x06007BEC RID: 31724 RVA: 0x00321ECF File Offset: 0x003202CF
		// (set) Token: 0x06007BED RID: 31725 RVA: 0x00321ED7 File Offset: 0x003202D7
		public List<StuffItem> itemList { get; private set; }

		// Token: 0x06007BEE RID: 31726 RVA: 0x00321EE0 File Offset: 0x003202E0
		public void SetItemList_System(List<StuffItem> itemList_System)
		{
			this.itemList_System = itemList_System;
		}

		// Token: 0x17001879 RID: 6265
		// (get) Token: 0x06007BEF RID: 31727 RVA: 0x00321EE9 File Offset: 0x003202E9
		// (set) Token: 0x06007BF0 RID: 31728 RVA: 0x00321EF1 File Offset: 0x003202F1
		public List<StuffItem> itemList_System { get; private set; } = new List<StuffItem>();

		// Token: 0x1700187A RID: 6266
		// (get) Token: 0x06007BF1 RID: 31729 RVA: 0x00321EFA File Offset: 0x003202FA
		// (set) Token: 0x06007BF2 RID: 31730 RVA: 0x00321F07 File Offset: 0x00320307
		public bool Visible
		{
			get
			{
				return this.visible.Value;
			}
			set
			{
				this.visible.Value = value;
			}
		}

		// Token: 0x06007BF3 RID: 31731 RVA: 0x00321F15 File Offset: 0x00320315
		public void SetItemFilter(InventoryFacadeViewer.ItemFilter[] itemFilter)
		{
			this._itemFilter = itemFilter;
		}

		// Token: 0x06007BF4 RID: 31732 RVA: 0x00321F1E File Offset: 0x0032031E
		public void SetParent(RectTransform parent)
		{
			this._parent = parent;
		}

		// Token: 0x1700187B RID: 6267
		// (get) Token: 0x06007BF5 RID: 31733 RVA: 0x00321F27 File Offset: 0x00320327
		public CanvasGroup canvasGroup
		{
			[CompilerGenerated]
			get
			{
				return this._canvasGroup;
			}
		}

		// Token: 0x1700187C RID: 6268
		// (get) Token: 0x06007BF6 RID: 31734 RVA: 0x00321F2F File Offset: 0x0032032F
		private BoolReactiveProperty visible { get; } = new BoolReactiveProperty(true);

		// Token: 0x06007BF7 RID: 31735 RVA: 0x00321F38 File Offset: 0x00320338
		public virtual IEnumerator Initialize()
		{
			this.CanvasGroupSetting();
			if (this._viewer == null)
			{
				yield return InventoryViewer.Load(this._parent, delegate(InventoryViewer viewer)
				{
					this._viewer = viewer;
				});
			}
			if (this._viewer != null)
			{
				this._viewer.ChangeTitleIcon(this._iconSprite);
				this._viewer.ChangeTitleText(this._iconText);
				if (this._sorterVisible && this._sortUIPanel != null)
				{
					ItemSortUI itemSortUI = this._sortUIPanel.GetComponentInChildren<ItemSortUI>();
					if (itemSortUI != null)
					{
						this._viewer.SortUIBind(itemSortUI);
					}
					else
					{
						yield return this._viewer.LoadSortUI(delegate(ItemSortUI sortUI)
						{
							sortUI.transform.SetParent(this._sortUIPanel, false);
						});
					}
				}
				yield return this._viewer.CategoryButtonAddEvent(delegate(int i)
				{
					this.ItemListNodeFilter(i, false);
				});
			}
			this._viewer.slotCounter.visible = this._counterVisible;
			this._viewer.categoryUI.OnSubmit.AddListener(delegate()
			{
				if (this._viewer.categoryUI.SelectedButton != null)
				{
					this._viewer.categoryUI.SelectedButton.onClick.Invoke();
				}
				else
				{
					if (!this._viewer.cursor.enabled)
					{
						return;
					}
					if (this._sorterVisible)
					{
						Vector3 position = this._viewer.cursor.transform.position;
						if (position == this._viewer.sorter.transform.position)
						{
							this._viewer.sorter.isOn = !this._viewer.sorter.isOn;
						}
						else if (position == this._viewer.sortButton.transform.position)
						{
							this._viewer.sortButton.onClick.Invoke();
						}
					}
				}
			});
			if (this._viewer.sortUI != null)
			{
				this._viewer.sortUI.OnCancel.AddListener(delegate()
				{
					if (this._viewer.itemListUI.ItemVisibles.Any<ItemNodeUI>())
					{
						this._viewer.SetFocusLevel(this._viewer.itemListUI.FocusLevel);
					}
					else
					{
						this._viewer.SetFocusLevel(this._viewer.categoryUI.FocusLevel);
						this._viewer.categoryUI.SelectedID = 0;
						this._viewer.categoryUI.useCursor = true;
					}
				});
				this._viewer.sortButton.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					if (!this._viewer.sortUI.isOpen)
					{
						this._viewer.sortUI.Open();
						this._viewer.SetFocusLevel(this._viewer.sortUI.FocusLevel);
					}
					else
					{
						this._viewer.sortUI.Close();
					}
				});
				this._viewer.sortUI.TypeChanged += delegate(int type)
				{
					this._viewer.SortType = type;
				};
			}
			this._viewer.itemListUI.OnSubmit.AddListener(delegate()
			{
				this._viewer.itemListUI.CurrentID = this._viewer.itemListUI.SelectedID;
			});
			if (!this._sorterVisible)
			{
				this._viewer.sorter.isOn = true;
				this._viewer.sorter.gameObject.SetActive(false);
				this._viewer.sortButton.gameObject.SetActive(false);
			}
			this._viewer.categoryUI.OnEntered = delegate()
			{
				this._viewer.SetFocusLevel(this._viewer.categoryUI.FocusLevel);
			};
			Observable.Merge<PointerEventData>(new IObservable<PointerEventData>[]
			{
				this._viewer.categoryUI.leftButton.OnPointerEnterAsObservable(),
				this._viewer.categoryUI.rightButton.OnPointerEnterAsObservable()
			}).Subscribe(delegate(PointerEventData _)
			{
				this._viewer.categoryUI.OnEntered();
			}).AddTo(this._viewer);
			(from _ in this._viewer.itemListUI.OnEntered
			where this._viewer.IsActiveControl
			select _).Subscribe(delegate(int _)
			{
				if (this._viewer.itemListUI.ItemVisibles.Any<ItemNodeUI>())
				{
					this._viewer.SetFocusLevel(this._viewer.itemListUI.FocusLevel);
				}
			});
			if (this._viewer.sortUI != null)
			{
				this._viewer.sortUI.OnEntered += delegate()
				{
					if (this._viewer.IsActiveControl && this._viewer.sortUI.isOpen)
					{
						this._viewer.SetFocusLevel(this._viewer.sortUI.FocusLevel);
					}
				};
				this._viewer.sorter.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
				{
					this._viewer.SetCursorFocus(this._viewer.sorter);
				}).AddTo(this._viewer);
				this._viewer.sorter.OnPointerExitAsObservable().Subscribe(delegate(PointerEventData _)
				{
					this._viewer.cursor.enabled = false;
				}).AddTo(this._viewer);
			}
			this.initialized = true;
			yield break;
		}

		// Token: 0x06007BF8 RID: 31736 RVA: 0x00321F53 File Offset: 0x00320353
		public void Refresh()
		{
			this._viewer.itemListUI.Refresh();
			this._viewer.slotCounter.x = this.itemList.Count;
		}

		// Token: 0x06007BF9 RID: 31737 RVA: 0x00321F80 File Offset: 0x00320380
		public virtual void ItemListNodeCreate()
		{
			Dictionary<int, int[]> table = this._itemFilter.ToLookup((InventoryFacadeViewer.ItemFilter v) => v.category, (InventoryFacadeViewer.ItemFilter v) => v.IDs).ToDictionary((IGrouping<int, int[]> v) => v.Key, (IGrouping<int, int[]> v) => v.SelectMany((int[] x) => x).ToArray<int>());
			IEnumerable<StuffItem> source = this.itemList_System.Concat(this.itemList);
			List<StuffItem> viewList;
			if (!table.Any<KeyValuePair<int, int[]>>() || (table.Count == 1 && table.First<KeyValuePair<int, int[]>>().Key == 0))
			{
				viewList = source.ToList<StuffItem>();
			}
			else
			{
				viewList = source.Where(delegate(StuffItem item)
				{
					int[] source2;
					return item.CategoryID <= 0 || (table.TryGetValue(item.CategoryID, out source2) && (!source2.Any<int>() || source2.Contains(item.ID)));
				}).ToList<StuffItem>();
			}
			this._viewer.itemListUI.ClearItems();
			this._viewer.categoryUI.Filter(table.Keys.ToArray<int>());
			foreach (var <>__AnonType in Enumerable.Range(0, viewList.Count).Select((int i, int index) => new
			{
				item = viewList[i],
				index = index
			}))
			{
				this.ItemListAddNode(<>__AnonType.index, <>__AnonType.item);
			}
			int num = this._viewer.categoryUI.Visibles.FirstOrDefault<int>();
			this._viewer.categoryUI.SetSelectAndCategory(num);
			this.ItemListNodeFilter(num, true);
		}

		// Token: 0x06007BFA RID: 31738 RVA: 0x00322174 File Offset: 0x00320574
		public virtual ItemNodeUI ItemListAddNode(int index, StuffItem item)
		{
			ItemNodeUI node = this._viewer.itemListUI.AddItemNode(index, item);
			if (node != null && this.ItemNodeOnDoubleClick != null)
			{
				node.OnClick.AsObservable().DoubleInterval(250f, false).Subscribe(delegate(IList<double> _)
				{
					Action<InventoryFacadeViewer.DoubleClickData> itemNodeOnDoubleClick = this.ItemNodeOnDoubleClick;
					if (itemNodeOnDoubleClick != null)
					{
						itemNodeOnDoubleClick(new InventoryFacadeViewer.DoubleClickData(index, node));
					}
				}).AddTo(node);
			}
			return node;
		}

		// Token: 0x06007BFB RID: 31739 RVA: 0x0032220C File Offset: 0x0032060C
		public virtual void ItemListNodeFilter(int category, bool isSort)
		{
			this._viewer.itemListUI.Filter(category);
			this._viewer.itemListUI.ForceSetNonSelect();
			if (isSort)
			{
				this._viewer.SortItemList();
			}
		}

		// Token: 0x06007BFC RID: 31740 RVA: 0x00322240 File Offset: 0x00320640
		public bool AddItem(StuffItem item)
		{
			int num = item.Count;
			int num2;
			if (!this.itemList.CanAddItem(this.slotCounter.y, item, num, out num2))
			{
				num = num2;
			}
			if (num <= 0)
			{
				return false;
			}
			this.itemList.AddItem(new StuffItem(item), num);
			item.Count -= num;
			List<StuffItem> list = this.itemList.FindItems(item).ToList<StuffItem>();
			foreach (ItemNodeUI itemNodeUI in this.itemListUI)
			{
				list.Remove(itemNodeUI.Item);
			}
			foreach (StuffItem item2 in list)
			{
				this.ItemListAddNode(this.itemListUI.SearchNotUsedIndex, item2);
			}
			this.ItemListNodeFilter(this.categoryUI.CategoryID, true);
			return item.Count <= 0;
		}

		// Token: 0x06007BFD RID: 31741 RVA: 0x0032237C File Offset: 0x0032077C
		protected virtual void CanvasGroupSetting()
		{
			if (this._canvasGroup == null)
			{
				this.visible.Dispose();
			}
			else
			{
				this.visible.Value = !Mathf.Approximately(this._canvasGroup.alpha, 0f);
				this.visible.Subscribe(delegate(bool isOn)
				{
					this._canvasGroup.alpha = (float)((!isOn) ? 0 : 1);
					this._canvasGroup.blocksRaycasts = isOn;
				});
			}
		}

		// Token: 0x0400636D RID: 25453
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x0400636E RID: 25454
		[SerializeField]
		private RectTransform _parent;

		// Token: 0x0400636F RID: 25455
		[SerializeField]
		private Sprite _iconSprite;

		// Token: 0x04006370 RID: 25456
		[SerializeField]
		private string _iconText;

		// Token: 0x04006371 RID: 25457
		[SerializeField]
		private InventoryViewer _viewer;

		// Token: 0x04006372 RID: 25458
		[SerializeField]
		private Transform _sortUIPanel;

		// Token: 0x04006373 RID: 25459
		[SerializeField]
		private bool _sorterVisible = true;

		// Token: 0x04006374 RID: 25460
		[SerializeField]
		private bool _counterVisible = true;

		// Token: 0x04006375 RID: 25461
		[Header("Filter Setting")]
		[SerializeField]
		private InventoryFacadeViewer.ItemFilter[] _itemFilter = new InventoryFacadeViewer.ItemFilter[0];

		// Token: 0x02000EC7 RID: 3783
		[Serializable]
		public class ItemFilter
		{
			// Token: 0x06007C04 RID: 31748 RVA: 0x00322451 File Offset: 0x00320851
			public ItemFilter()
			{
			}

			// Token: 0x06007C05 RID: 31749 RVA: 0x00322465 File Offset: 0x00320865
			public ItemFilter(int category)
			{
				this._category = category;
			}

			// Token: 0x06007C06 RID: 31750 RVA: 0x00322480 File Offset: 0x00320880
			public ItemFilter(int category, int[] IDs)
			{
				this._category = category;
				this._IDs = IDs;
			}

			// Token: 0x1700187D RID: 6269
			// (get) Token: 0x06007C07 RID: 31751 RVA: 0x003224A2 File Offset: 0x003208A2
			public int category
			{
				[CompilerGenerated]
				get
				{
					return this._category;
				}
			}

			// Token: 0x1700187E RID: 6270
			// (get) Token: 0x06007C08 RID: 31752 RVA: 0x003224AA File Offset: 0x003208AA
			public int[] IDs
			{
				[CompilerGenerated]
				get
				{
					return this._IDs;
				}
			}

			// Token: 0x0400637C RID: 25468
			[SerializeField]
			private int _category;

			// Token: 0x0400637D RID: 25469
			[SerializeField]
			private int[] _IDs = new int[0];
		}

		// Token: 0x02000EC8 RID: 3784
		public struct DoubleClickData
		{
			// Token: 0x06007C09 RID: 31753 RVA: 0x003224B2 File Offset: 0x003208B2
			public DoubleClickData(int ID, ItemNodeUI node)
			{
				this.ID = ID;
				this.node = node;
			}

			// Token: 0x1700187F RID: 6271
			// (get) Token: 0x06007C0A RID: 31754 RVA: 0x003224C2 File Offset: 0x003208C2
			public int ID { get; }

			// Token: 0x17001880 RID: 6272
			// (get) Token: 0x06007C0B RID: 31755 RVA: 0x003224CA File Offset: 0x003208CA
			public ItemNodeUI node { get; }
		}
	}
}
