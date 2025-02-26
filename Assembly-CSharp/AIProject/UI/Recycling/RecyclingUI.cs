using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using AIProject.Scene;
using AIProject.UI.Viewer;
using Manager;
using ReMotion;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI.Recycling
{
	// Token: 0x02000EA6 RID: 3750
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasGroup))]
	public class RecyclingUI : MenuUIBehaviour
	{
		// Token: 0x170017F2 RID: 6130
		// (get) Token: 0x060079F3 RID: 31219 RVA: 0x00334690 File Offset: 0x00332A90
		// (set) Token: 0x060079F4 RID: 31220 RVA: 0x00334698 File Offset: 0x00332A98
		public WarningViewer[] WarningUIs { get; private set; } = new WarningViewer[0];

		// Token: 0x170017F3 RID: 6131
		// (get) Token: 0x060079F5 RID: 31221 RVA: 0x003346A1 File Offset: 0x00332AA1
		public RecyclingInfoPanelUI InfoPanelUI
		{
			[CompilerGenerated]
			get
			{
				return this._infoPanelUI;
			}
		}

		// Token: 0x170017F4 RID: 6132
		// (get) Token: 0x060079F6 RID: 31222 RVA: 0x003346A9 File Offset: 0x00332AA9
		public RecyclingDecidedItemSlotUI DecidedItemSlotUI
		{
			[CompilerGenerated]
			get
			{
				return this._decidedItemSlotUI;
			}
		}

		// Token: 0x170017F5 RID: 6133
		// (get) Token: 0x060079F7 RID: 31223 RVA: 0x003346B1 File Offset: 0x00332AB1
		public RecyclingCreateItemStockUI CreateItemStockUI
		{
			[CompilerGenerated]
			get
			{
				return this._createItemStockUI;
			}
		}

		// Token: 0x170017F6 RID: 6134
		// (get) Token: 0x060079F8 RID: 31224 RVA: 0x003346B9 File Offset: 0x00332AB9
		public RecyclingCreatePanelUI CreatePanelUI
		{
			[CompilerGenerated]
			get
			{
				return this._createPanelUI;
			}
		}

		// Token: 0x170017F7 RID: 6135
		// (get) Token: 0x060079F9 RID: 31225 RVA: 0x003346C1 File Offset: 0x00332AC1
		public RecyclingItemDeleteRequestUI DeleteREquestUI
		{
			[CompilerGenerated]
			get
			{
				return this._deleteRequestUI;
			}
		}

		// Token: 0x060079FA RID: 31226 RVA: 0x003346C9 File Offset: 0x00332AC9
		private void CursorOff(RecyclingInventoryFacadeViewer viewer)
		{
			viewer.cursor.enabled = false;
		}

		// Token: 0x170017F8 RID: 6136
		// (get) Token: 0x060079FB RID: 31227 RVA: 0x003346D7 File Offset: 0x00332AD7
		public RecyclingInventoryFacadeViewer[] InventoryUIs
		{
			[CompilerGenerated]
			get
			{
				return this._vieweres;
			}
		}

		// Token: 0x170017F9 RID: 6137
		// (get) Token: 0x060079FC RID: 31228 RVA: 0x003346DF File Offset: 0x00332ADF
		// (set) Token: 0x060079FD RID: 31229 RVA: 0x003346E7 File Offset: 0x00332AE7
		public RecyclingInventoryFacadeViewer SelectedInventoryUI { get; private set; }

		// Token: 0x170017FA RID: 6138
		// (get) Token: 0x060079FE RID: 31230 RVA: 0x003346F0 File Offset: 0x00332AF0
		// (set) Token: 0x060079FF RID: 31231 RVA: 0x00334718 File Offset: 0x00332B18
		public float CanvasAlpha
		{
			get
			{
				return (!(this._rootCanvasGroup != null)) ? 0f : this._rootCanvasGroup.alpha;
			}
			private set
			{
				if (this._rootCanvasGroup != null)
				{
					this._rootCanvasGroup.alpha = value;
				}
			}
		}

		// Token: 0x170017FB RID: 6139
		// (get) Token: 0x06007A00 RID: 31232 RVA: 0x00334737 File Offset: 0x00332B37
		// (set) Token: 0x06007A01 RID: 31233 RVA: 0x0033475B File Offset: 0x00332B5B
		public bool BlockRaycast
		{
			get
			{
				return this._rootCanvasGroup != null && this._rootCanvasGroup.blocksRaycasts;
			}
			private set
			{
				if (this._rootCanvasGroup != null && this._rootCanvasGroup.blocksRaycasts != value)
				{
					this._rootCanvasGroup.blocksRaycasts = value;
				}
			}
		}

		// Token: 0x170017FC RID: 6140
		// (get) Token: 0x06007A02 RID: 31234 RVA: 0x0033478B File Offset: 0x00332B8B
		// (set) Token: 0x06007A03 RID: 31235 RVA: 0x003347AF File Offset: 0x00332BAF
		public bool Interactable
		{
			get
			{
				return this._rootCanvasGroup != null && this._rootCanvasGroup.interactable;
			}
			private set
			{
				if (this._rootCanvasGroup != null && this._rootCanvasGroup.interactable != value)
				{
					this._rootCanvasGroup.interactable = value;
				}
			}
		}

		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x06007A04 RID: 31236 RVA: 0x003347DF File Offset: 0x00332BDF
		// (set) Token: 0x06007A05 RID: 31237 RVA: 0x003347E7 File Offset: 0x00332BE7
		public MenuUIBehaviour[] MenuUIBehaviours { get; private set; } = new MenuUIBehaviour[0];

		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x06007A06 RID: 31238 RVA: 0x003347F0 File Offset: 0x00332BF0
		// (set) Token: 0x06007A07 RID: 31239 RVA: 0x003347F8 File Offset: 0x00332BF8
		public ItemListUI[] ItemListUIs { get; private set; } = new ItemListUI[0];

		// Token: 0x170017FF RID: 6143
		// (get) Token: 0x06007A08 RID: 31240 RVA: 0x00334801 File Offset: 0x00332C01
		// (set) Token: 0x06007A09 RID: 31241 RVA: 0x00334809 File Offset: 0x00332C09
		public MenuUIBehaviour[] ItemListUIBehaviours { get; private set; } = new MenuUIBehaviour[0];

		// Token: 0x17001800 RID: 6144
		// (get) Token: 0x06007A0A RID: 31242 RVA: 0x00334812 File Offset: 0x00332C12
		// (set) Token: 0x06007A0B RID: 31243 RVA: 0x0033481A File Offset: 0x00332C1A
		public ItemListController[] ListControllers { get; private set; } = new ItemListController[0];

		// Token: 0x17001801 RID: 6145
		// (get) Token: 0x06007A0C RID: 31244 RVA: 0x00334823 File Offset: 0x00332C23
		// (set) Token: 0x06007A0D RID: 31245 RVA: 0x0033482B File Offset: 0x00332C2B
		public InventoryFacadeViewer.ItemFilter[] _itemFilter { get; private set; }

		// Token: 0x17001802 RID: 6146
		// (get) Token: 0x06007A0E RID: 31246 RVA: 0x00334834 File Offset: 0x00332C34
		// (set) Token: 0x06007A0F RID: 31247 RVA: 0x0033483C File Offset: 0x00332C3C
		public bool Initialized { get; private set; }

		// Token: 0x17001803 RID: 6147
		// (get) Token: 0x06007A10 RID: 31248 RVA: 0x00334845 File Offset: 0x00332C45
		// (set) Token: 0x06007A11 RID: 31249 RVA: 0x0033484D File Offset: 0x00332C4D
		public Action OnClosedEvent { get; set; }

		// Token: 0x17001804 RID: 6148
		// (get) Token: 0x06007A12 RID: 31250 RVA: 0x00334856 File Offset: 0x00332C56
		// (set) Token: 0x06007A13 RID: 31251 RVA: 0x0033485E File Offset: 0x00332C5E
		public RecyclingData RecyclingData { get; private set; }

		// Token: 0x17001805 RID: 6149
		// (get) Token: 0x06007A14 RID: 31252 RVA: 0x00334867 File Offset: 0x00332C67
		// (set) Token: 0x06007A15 RID: 31253 RVA: 0x0033486F File Offset: 0x00332C6F
		public int CraftPointID { get; private set; } = -1;

		// Token: 0x17001806 RID: 6150
		// (get) Token: 0x06007A16 RID: 31254 RVA: 0x00334878 File Offset: 0x00332C78
		public Subject<RecyclingInventoryFacadeViewer> OnInventoryChanged { get; } = new Subject<RecyclingInventoryFacadeViewer>();

		// Token: 0x17001807 RID: 6151
		// (get) Token: 0x06007A17 RID: 31255 RVA: 0x00334880 File Offset: 0x00332C80
		public Subject<StuffItem> OnSendItem { get; } = new Subject<StuffItem>();

		// Token: 0x17001808 RID: 6152
		// (get) Token: 0x06007A18 RID: 31256 RVA: 0x00334888 File Offset: 0x00332C88
		public Subject<PanelType> OnDoubleClicked { get; } = new Subject<PanelType>();

		// Token: 0x06007A19 RID: 31257 RVA: 0x00334890 File Offset: 0x00332C90
		protected override void Awake()
		{
			base.Awake();
			if (this._rootCanvasGroup == null)
			{
				this._rootCanvasGroup = base.GetComponent<CanvasGroup>();
			}
			if (this._rootRectTransform == null)
			{
				this._rootRectTransform = base.GetComponent<RectTransform>();
			}
			if (this._infoPanelUI == null)
			{
				this._infoPanelUI = base.GetComponentInChildren<RecyclingInfoPanelUI>(true);
			}
			if (this._decidedItemSlotUI == null)
			{
				this._decidedItemSlotUI = base.GetComponentInChildren<RecyclingDecidedItemSlotUI>(true);
			}
			if (this._createItemStockUI == null)
			{
				this._createItemStockUI = base.GetComponentInChildren<RecyclingCreateItemStockUI>(true);
			}
			if (this._createPanelUI == null)
			{
				this._createPanelUI = base.GetComponentInChildren<RecyclingCreatePanelUI>(true);
			}
			if (this._rootCanvasGroup == null || this._rootRectTransform == null || this._infoPanelUI == null || this._decidedItemSlotUI == null || this._createItemStockUI == null || this._createPanelUI == null)
			{
			}
			if (Singleton<Manager.Resources>.IsInstance())
			{
				Singleton<Manager.Resources>.Instance.LoadMapResourceStream.Subscribe(delegate(Unit __)
				{
					IEnumerator settingCoroutine = this.ItemFilterSettingCoroutine();
					Observable.FromCoroutine(() => settingCoroutine, false).Subscribe(delegate(Unit _)
					{
					}, delegate(Exception _)
					{
					}, delegate()
					{
						this.InventoryUISetting();
					}).AddTo(this);
				}).AddTo(this);
			}
		}

		// Token: 0x06007A1A RID: 31258 RVA: 0x003349E8 File Offset: 0x00332DE8
		protected override void Start()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			}).AddTo(this);
			this._closeButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.DoClose();
			}).AddTo(this);
			ActionIDDownCommand actionIDDownCommand = new ActionIDDownCommand
			{
				ActionID = ActionID.Cancel
			};
			actionIDDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.DoClose();
			});
			this._actionCommands.Add(actionIDDownCommand);
			KeyCodeDownCommand keyCodeDownCommand = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Mouse1
			};
			keyCodeDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.DoClose();
			});
			this._keyCommands.Add(keyCodeDownCommand);
			if (!this._toggleElements.IsNullOrEmpty<ToggleElement>())
			{
				for (int i = 0; i < this._toggleElements.Count; i++)
				{
					ToggleElement element = this._toggleElements.GetElement(i);
					Toggle toggle = (element == null) ? null : element.Toggle;
					if (!(toggle == null))
					{
						element.Index = i;
						element.Start();
						IObservable<bool> source = from flag in toggle.OnValueChangedAsObservable().DistinctUntilChanged<bool>()
						where flag
						select flag;
						source.Subscribe(delegate(bool _)
						{
							this.ChangedSelecteInventory(element.Index);
							this.PlaySE(SoundPack.SystemSE.OK_S);
						}).AddTo(toggle);
					}
				}
			}
			(from item in this._decidedItemSlotUI.CreateEvent
			where item != null
			select item).Subscribe(delegate(StuffItem item)
			{
				this._createItemStockUI.AddItem(item);
			}).AddTo(this);
			base.Start();
			this.CanvasAlpha = 0f;
			bool flag2 = false;
			this.Interactable = flag2;
			this.BlockRaycast = flag2;
			this.SetEnabledInputAll(false);
		}

		// Token: 0x06007A1B RID: 31259 RVA: 0x00334BFC File Offset: 0x00332FFC
		private IEnumerator ItemFilterSettingCoroutine()
		{
			if (this._itemFilter != null)
			{
				yield break;
			}
			while (!Singleton<Manager.Resources>.IsInstance())
			{
				yield return null;
			}
			List<InventoryFacadeViewer.ItemFilter> filterList = ListPool<InventoryFacadeViewer.ItemFilter>.Get();
			filterList.Add(new InventoryFacadeViewer.ItemFilter(0));
			foreach (KeyValuePair<int, Tuple<string, Sprite>> keyValuePair in from x in Singleton<Manager.Resources>.Instance.itemIconTables.CategoryIcon
			where 0 < x.Key
			select x)
			{
				int key = keyValuePair.Key;
				if (Game.isAdd01 || !this._adultCategoryIDList.Contains(key))
				{
					Dictionary<int, StuffItemInfo> itemTable = Singleton<Manager.Resources>.Instance.GameInfo.GetItemTable(key);
					if (!itemTable.IsNullOrEmpty<int, StuffItemInfo>())
					{
						List<int> list = ListPool<int>.Get();
						foreach (KeyValuePair<int, StuffItemInfo> keyValuePair2 in itemTable)
						{
							StuffItemInfo value = keyValuePair2.Value;
							if (value != null && value.isTrash)
							{
								if (!list.Contains(value.ID))
								{
									list.Add(value.ID);
								}
							}
						}
						int[] array = new int[list.Count];
						for (int i = 0; i < list.Count; i++)
						{
							array[i] = list[i];
						}
						ListPool<int>.Release(list);
						if (!array.IsNullOrEmpty<int>())
						{
							InventoryFacadeViewer.ItemFilter item = new InventoryFacadeViewer.ItemFilter(key, array);
							filterList.Add(item);
						}
					}
				}
			}
			this._itemFilter = new InventoryFacadeViewer.ItemFilter[filterList.Count];
			for (int j = 0; j < filterList.Count; j++)
			{
				this._itemFilter[j] = filterList[j];
			}
			ListPool<InventoryFacadeViewer.ItemFilter>.Release(filterList);
			yield break;
		}

		// Token: 0x06007A1C RID: 31260 RVA: 0x00334C18 File Offset: 0x00333018
		private void InventoryUISetting()
		{
			IEnumerator pouchCoroutine = this.InventoryUISettingCoroutine(this._pouchInventoryUI);
			IEnumerator chestCoroutine = this.InventoryUISettingCoroutine(this._chestInventoryUI);
			IConnectableObservable<Unit> connectableObservable = Observable.FromCoroutine(() => pouchCoroutine, false).Publish<Unit>();
			IConnectableObservable<Unit> connectableObservable2 = Observable.FromCoroutine(() => chestCoroutine, false).Publish<Unit>();
			connectableObservable.Connect();
			connectableObservable2.Connect();
			IEnumerator decidedCoroutine = this.ItemListUISettingCoroutine(this._decidedItemSlotUI.ListController, PanelType.DecidedItem);
			IEnumerator createdCoroutine = this.ItemListUISettingCoroutine(this._createItemStockUI.ListController, PanelType.CreatedItem);
			IConnectableObservable<Unit> connectableObservable3 = Observable.FromCoroutine(() => decidedCoroutine, false).Publish<Unit>();
			IConnectableObservable<Unit> connectableObservable4 = Observable.FromCoroutine(() => createdCoroutine, false).Publish<Unit>();
			connectableObservable3.Connect();
			connectableObservable4.Connect();
			IEnumerator warningCoroutine = this.LoadWarningViewerCoroutine();
			IConnectableObservable<Unit> connectableObservable5 = Observable.FromCoroutine(() => warningCoroutine, false).Publish<Unit>();
			connectableObservable5.Connect();
			Observable.WhenAll(new IObservable<Unit>[]
			{
				connectableObservable,
				connectableObservable2,
				connectableObservable3,
				connectableObservable4,
				connectableObservable5
			}).Subscribe(delegate(Unit _)
			{
				this.FinishedInventoryUISetting();
			}, delegate(Exception ex)
			{
			}).AddTo(this);
		}

		// Token: 0x06007A1D RID: 31261 RVA: 0x00334D88 File Offset: 0x00333188
		private IEnumerator InventoryUISettingCoroutine(RecyclingInventoryFacadeViewer viewer)
		{
			if (viewer == null)
			{
				yield break;
			}
			yield return Observable.FromCoroutine(() => viewer.Initialize(), false).ToYieldInstruction<Unit>().AddTo(this);
			viewer.ListController.SetInventoryUI(viewer);
			ItemListController listController = viewer.ListController;
			listController.RefreshEvent = (Action)Delegate.Combine(listController.RefreshEvent, new Action(delegate()
			{
				viewer.Refresh();
			}));
			viewer.categoryUI.OnSubmit.AddListener(delegate()
			{
				if (viewer.categoryUI.useCursor)
				{
					viewer.categoryUI.SelectedButton.onClick.Invoke();
				}
			});
			viewer.categoryUI.OnCancel.AddListener(delegate()
			{
				this.DoClose();
			});
			viewer.categoryUI.OnEntered = delegate()
			{
				this.CursorOff(viewer);
			};
			Observable.Merge<PointerEventData>(new IObservable<PointerEventData>[]
			{
				viewer.categoryUI.leftButton.OnPointerEnterAsObservable(),
				viewer.categoryUI.rightButton.OnPointerEnterAsObservable()
			}).Subscribe(delegate(PointerEventData _)
			{
				viewer.categoryUI.OnEntered();
			}).AddTo(this);
			(from _ in viewer.itemListUI.OnEntered
			where this.IsActiveControl
			select _).Subscribe(delegate(int _)
			{
				if (viewer.itemListUI.ItemVisibles.Any<ItemNodeUI>())
				{
					this.CursorOff(viewer);
				}
			}).AddTo(this);
			viewer.itemListUI.OnCancel.AddListener(delegate()
			{
				this.DoClose();
			});
			viewer.SetItemFilter(this._itemFilter);
			if (!viewer.itemListUI.isOptionNode)
			{
				DefinePack definePack = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.DefinePack;
				string text = (!(definePack != null)) ? string.Empty : definePack.ABPaths.MapScenePrefab;
				GameObject gameObject = CommonLib.LoadAsset<GameObject>(text, "ItemOption_ItemBox", false, string.Empty);
				if (gameObject != null)
				{
					MapScene.AddAssetBundlePath(text);
					viewer.itemListUI.SetOptionNode(gameObject);
				}
			}
			viewer.itemListUI.ForceSetNonSelect();
			yield break;
		}

		// Token: 0x06007A1E RID: 31262 RVA: 0x00334DAC File Offset: 0x003331AC
		private IEnumerator ItemListUISettingCoroutine(ItemListController listCon, PanelType panelType)
		{
			ItemListUI itemListUI = (listCon != null) ? listCon.itemListUI : null;
			if (itemListUI == null)
			{
				yield break;
			}
			if (!itemListUI.isOptionNode)
			{
				DefinePack definePack = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.DefinePack;
				string text = (!(definePack != null)) ? string.Empty : definePack.ABPaths.MapScenePrefabAdd12;
				string text2 = (!(definePack != null)) ? string.Empty : definePack.ABManifests.Add12;
				string assetBundleName = text;
				string assetName = "ItemOption_recycling_sub";
				string manifestName = text2;
				GameObject gameObject = CommonLib.LoadAsset<GameObject>(assetBundleName, assetName, false, manifestName);
				if (gameObject != null)
				{
					MapScene.AddAssetBundlePath(text);
					itemListUI.SetOptionNode(gameObject);
				}
			}
			itemListUI.ForceSetNonSelect();
			yield break;
		}

		// Token: 0x06007A1F RID: 31263 RVA: 0x00334DC8 File Offset: 0x003331C8
		private IEnumerator LoadWarningViewerCoroutine()
		{
			if (this._pouchWarningViewer == null)
			{
				yield return WarningViewer.Load(this._warningViewerLayout, delegate(WarningViewer newObj)
				{
					this._pouchWarningViewer = newObj;
				});
			}
			if (this._chestWarningViewer == null)
			{
				yield return WarningViewer.Load(this._warningViewerLayout, delegate(WarningViewer newObj)
				{
					this._chestWarningViewer = newObj;
				});
			}
			if (this._pouchAndChestWarningViewer == null)
			{
				yield return WarningViewer.Load(this._warningViewerLayout, delegate(WarningViewer newObj)
				{
					this._pouchAndChestWarningViewer = newObj;
				});
			}
			int lngIdx = (!Singleton<GameSystem>.IsInstance()) ? 0 : Singleton<GameSystem>.Instance.languageInt;
			if (this._pouchWarningViewer != null)
			{
				this._pouchWarningViewer.langage = lngIdx;
				this._pouchWarningViewer.msgID = 0;
			}
			if (this._chestWarningViewer != null)
			{
				this._chestWarningViewer.langage = lngIdx;
				this._chestWarningViewer.msgID = 1;
			}
			if (this._pouchAndChestWarningViewer != null)
			{
				this._pouchAndChestWarningViewer.langage = lngIdx;
				this._pouchAndChestWarningViewer.msgID = 15;
			}
			yield break;
		}

		// Token: 0x06007A20 RID: 31264 RVA: 0x00334DE4 File Offset: 0x003331E4
		private void NonSelection(ItemListController con)
		{
			if (con == null || this.ListControllers.IsNullOrEmpty<ItemListController>())
			{
				return;
			}
			foreach (ItemListController itemListController in this.ListControllers)
			{
				if (itemListController != null && itemListController != con)
				{
					itemListController.itemListUI.ForceSetNonSelect();
				}
			}
		}

		// Token: 0x06007A21 RID: 31265 RVA: 0x00334E40 File Offset: 0x00333240
		private void NonEnabledInput(ItemListUI con)
		{
			if (con == null || this.ItemListUIs.IsNullOrEmpty<ItemListUI>())
			{
				return;
			}
			con.EnabledInput = true;
			foreach (ItemListUI itemListUI in this.ItemListUIs)
			{
				if (itemListUI != null && itemListUI != con)
				{
					itemListUI.EnabledInput = false;
				}
			}
		}

		// Token: 0x06007A22 RID: 31266 RVA: 0x00334EB0 File Offset: 0x003332B0
		private void FinishedInventoryUISetting()
		{
			this._vieweres = new RecyclingInventoryFacadeViewer[]
			{
				this._pouchInventoryUI,
				this._chestInventoryUI
			};
			if (this.WarningUIs.IsNullOrEmpty<WarningViewer>())
			{
				List<WarningViewer> list = ListPool<WarningViewer>.Get();
				list.Add(this._pouchWarningViewer);
				list.Add(this._chestWarningViewer);
				list.Add(this._pouchAndChestWarningViewer);
				list.RemoveAll((WarningViewer x) => x == null);
				this.WarningUIs = new WarningViewer[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					this.WarningUIs[i] = list[i];
				}
				ListPool<WarningViewer>.Release(list);
			}
			if (this.MenuUIBehaviours.IsNullOrEmpty<MenuUIBehaviour>())
			{
				List<MenuUIBehaviour> list2 = ListPool<MenuUIBehaviour>.Get();
				list2.Add(this);
				list2.Add(this._infoPanelUI);
				list2.Add(this._deleteRequestUI);
				list2.AddRange(this._pouchInventoryUI.viewer.MenuUIList);
				list2.AddRange(this._chestInventoryUI.viewer.MenuUIList);
				list2.Add(this._decidedItemSlotUI.ItemListUI);
				list2.Add(this._createItemStockUI.ItemListUI);
				list2.Add(this._createPanelUI);
				list2.RemoveAll((MenuUIBehaviour x) => x == null);
				this.MenuUIBehaviours = new MenuUIBehaviour[list2.Count];
				for (int j = 0; j < list2.Count; j++)
				{
					this.MenuUIBehaviours[j] = list2[j];
				}
				ListPool<MenuUIBehaviour>.Release(list2);
			}
			if (this.ItemListUIBehaviours.IsNullOrEmpty<MenuUIBehaviour>())
			{
				List<MenuUIBehaviour> list3 = ListPool<MenuUIBehaviour>.Get();
				list3.Add(this._pouchInventoryUI.itemListUI);
				list3.Add(this._chestInventoryUI.itemListUI);
				list3.Add(this._decidedItemSlotUI.ItemListUI);
				list3.Add(this._createItemStockUI.ItemListUI);
				list3.RemoveAll((MenuUIBehaviour x) => x == null);
				this.ItemListUIBehaviours = new MenuUIBehaviour[list3.Count];
				for (int k = 0; k < list3.Count; k++)
				{
					this.ItemListUIBehaviours[k] = list3[k];
				}
				ListPool<MenuUIBehaviour>.Release(list3);
			}
			if (this.ItemListUIs.IsNullOrEmpty<ItemListUI>())
			{
				List<ItemListUI> list4 = ListPool<ItemListUI>.Get();
				list4.Add(this._pouchInventoryUI.itemListUI);
				list4.Add(this._chestInventoryUI.itemListUI);
				list4.Add(this._decidedItemSlotUI.ItemListUI);
				list4.Add(this._createItemStockUI.ItemListUI);
				list4.RemoveAll((ItemListUI x) => x == null);
				this.ItemListUIs = new ItemListUI[list4.Count];
				for (int l = 0; l < list4.Count; l++)
				{
					this.ItemListUIs[l] = list4[l];
				}
				ListPool<ItemListUI>.Release(list4);
			}
			if (this.ListControllers.IsNullOrEmpty<ItemListController>())
			{
				List<ItemListController> list5 = ListPool<ItemListController>.Get();
				list5.Add(this._pouchInventoryUI.ListController);
				list5.Add(this._chestInventoryUI.ListController);
				list5.Add(this._decidedItemSlotUI.ListController);
				list5.Add(this._createItemStockUI.ListController);
				list5.RemoveAll((ItemListController x) => x == null);
				this.ListControllers = new ItemListController[list5.Count];
				for (int m = 0; m < list5.Count; m++)
				{
					this.ListControllers[m] = list5[m];
				}
				ListPool<ItemListController>.Release(list5);
			}
			this._infoPanelUI.ClickDecide.Subscribe(delegate(Unit _)
			{
				this.SendItem(this._infoPanelUI);
			}).AddTo(this);
			this._infoPanelUI.ClickReturn.Subscribe(delegate(Unit _)
			{
				this.SendItem(this._infoPanelUI);
			}).AddTo(this);
			this._pouchInventoryUI.itemListUI.CurrentChanged += delegate(int currentID, ItemNodeUI node)
			{
				this.CurrentChanged(PanelType.Pouch, currentID, node);
			};
			this._chestInventoryUI.itemListUI.CurrentChanged += delegate(int currentID, ItemNodeUI node)
			{
				this.CurrentChanged(PanelType.Chest, currentID, node);
			};
			this._decidedItemSlotUI.ItemListUI.CurrentChanged += delegate(int currentID, ItemNodeUI node)
			{
				this.CurrentChanged(PanelType.DecidedItem, currentID, node);
			};
			this._createItemStockUI.ItemListUI.CurrentChanged += delegate(int currentID, ItemNodeUI node)
			{
				if (node == null)
				{
					return;
				}
				this.NonSelection(this._createItemStockUI.ListController);
				this._infoPanelUI.AttachDeleteItem(this._createItemStockUI.ListController, node, currentID);
			};
			this._pouchInventoryUI.ItemNodeOnDoubleClick = delegate(InventoryFacadeViewer.DoubleClickData data)
			{
				this.OnDoubleClick(PanelType.Pouch, data.ID, data.node);
			};
			this._chestInventoryUI.ItemNodeOnDoubleClick = delegate(InventoryFacadeViewer.DoubleClickData data)
			{
				this.OnDoubleClick(PanelType.Chest, data.ID, data.node);
			};
			this._decidedItemSlotUI.ListController.DoubleClick += delegate(int currentID, ItemNodeUI nodeUI)
			{
				this.OnDoubleClick(PanelType.DecidedItem, currentID, nodeUI);
			};
			this._createItemStockUI.ListController.DoubleClick += delegate(int currentID, ItemNodeUI nodeUI)
			{
				this.OnDoubleClick(PanelType.CreatedItem, currentID, nodeUI);
			};
			if (!this.ItemListUIs.IsNullOrEmpty<ItemListUI>())
			{
				ItemListUI[] itemListUIs = this.ItemListUIs;
				for (int n = 0; n < itemListUIs.Length; n++)
				{
					ItemListUI ui = itemListUIs[n];
					RecyclingUI $this = this;
					if (!(ui == null))
					{
						(from _ in ui.OnEntered
						where $this.isActiveAndEnabled
						select _).Subscribe(delegate(int _)
						{
							$this.NonEnabledInput(ui);
						}).AddTo(this);
					}
				}
			}
			if (!this.ListControllers.IsNullOrEmpty<ItemListController>())
			{
				int num = -1;
				foreach (ItemListController itemListController in this.ListControllers)
				{
					num++;
					if (itemListController != null)
					{
						ItemListController itemListController2 = itemListController;
						itemListController2.RefreshEvent = (Action)Delegate.Combine(itemListController2.RefreshEvent, new Action(delegate()
						{
							this.UpdateWarningUI();
						}));
					}
				}
			}
			this.SetActive(false, base.gameObject);
			this.Initialized = true;
		}

		// Token: 0x06007A23 RID: 31267 RVA: 0x00335508 File Offset: 0x00333908
		private void RefreshInventoryUI()
		{
			if (this._toggleElements.IsNullOrEmpty<ToggleElement>())
			{
				return;
			}
			int index = -1;
			for (int i = 0; i < this._toggleElements.Count; i++)
			{
				ToggleElement element = this._toggleElements.GetElement(i);
				Toggle toggle = (element != null) ? element.Toggle : null;
				if (toggle != null && toggle.isOn)
				{
					index = i;
					break;
				}
			}
			if (!this._vieweres.IsNullOrEmpty<RecyclingInventoryFacadeViewer>())
			{
				WorldData worldData = (!Singleton<Game>.IsInstance()) ? null : Singleton<Game>.Instance.WorldData;
				if (worldData != null)
				{
					for (int j = 0; j < this._vieweres.Length; j++)
					{
						RecyclingInventoryFacadeViewer element2 = this._vieweres.GetElement(j);
						if (element2 != null)
						{
							InventoryType inventoryType = (InventoryType)j;
							int y = 0;
							if (inventoryType != InventoryType.Pouch)
							{
								if (inventoryType == InventoryType.Chest)
								{
									y = ((!Singleton<Manager.Resources>.IsInstance()) ? 0 : Singleton<Manager.Resources>.Instance.DefinePack.ItemBoxCapacityDefines.StorageCapacity);
								}
							}
							else
							{
								PlayerData playerData = worldData.PlayerData;
								int? num = (playerData != null) ? new int?(playerData.InventorySlotMax) : null;
								y = ((num == null) ? 0 : num.Value);
							}
							element2.slotCounter.y = y;
							element2.ItemListNodeCreate();
						}
					}
				}
			}
			if (this.RecyclingData != null)
			{
				RecyclingDecidedItemSlotUI decidedItemSlotUI = this._decidedItemSlotUI;
				decidedItemSlotUI.SettingUI(this.RecyclingData.DecidedItemList);
				RecyclingCreateItemStockUI createItemStockUI = this._createItemStockUI;
				createItemStockUI.SettingUI(this.RecyclingData.CreatedItemList);
			}
			this.ChangedSelecteInventory(index);
		}

		// Token: 0x06007A24 RID: 31268 RVA: 0x003356D4 File Offset: 0x00333AD4
		private void ChangedSelecteInventory(int index)
		{
			if (this._vieweres.IsNullOrEmpty<RecyclingInventoryFacadeViewer>())
			{
				return;
			}
			bool flag = this.SelectedInventoryUI != null && this.SelectedInventoryUI.ItemSortUI.isOpen;
			RecyclingInventoryFacadeViewer recyclingInventoryFacadeViewer = null;
			for (int i = 0; i < this._vieweres.Length; i++)
			{
				RecyclingInventoryFacadeViewer recyclingInventoryFacadeViewer2 = this._vieweres[i];
				if (recyclingInventoryFacadeViewer2 != null)
				{
					bool visible = i == index;
					recyclingInventoryFacadeViewer2.SetVisible(visible);
					recyclingInventoryFacadeViewer2.Visible = visible;
					if (i == index)
					{
						recyclingInventoryFacadeViewer = recyclingInventoryFacadeViewer2;
					}
					else
					{
						this.CursorOff(recyclingInventoryFacadeViewer2);
					}
				}
			}
			if (recyclingInventoryFacadeViewer == null)
			{
				return;
			}
			recyclingInventoryFacadeViewer.Refresh();
			if (flag)
			{
				recyclingInventoryFacadeViewer.ItemSortUI.Open();
			}
			bool flag2 = recyclingInventoryFacadeViewer != this.SelectedInventoryUI;
			this.SelectedInventoryUI = recyclingInventoryFacadeViewer;
			if (flag2)
			{
				this.OnInventoryChanged.OnNext(recyclingInventoryFacadeViewer);
			}
		}

		// Token: 0x06007A25 RID: 31269 RVA: 0x003357BC File Offset: 0x00333BBC
		private void CurrentChanged(PanelType panelType, int currentID, ItemNodeUI nodeUI)
		{
			if (this._infoPanelUI == null)
			{
				return;
			}
			if (nodeUI == null || currentID < 0)
			{
				return;
			}
			ItemListController listController;
			ItemListController itemListController;
			ButtonType buttonType;
			switch (panelType)
			{
			case PanelType.Pouch:
				listController = this._pouchInventoryUI.ListController;
				itemListController = this._decidedItemSlotUI.ListController;
				buttonType = ButtonType.Decide;
				goto IL_CD;
			case PanelType.Chest:
				listController = this._chestInventoryUI.ListController;
				itemListController = this._decidedItemSlotUI.ListController;
				buttonType = ButtonType.Decide;
				goto IL_CD;
			case PanelType.DecidedItem:
			{
				listController = this._decidedItemSlotUI.ListController;
				RecyclingInventoryFacadeViewer selectedInventoryUI = this.SelectedInventoryUI;
				itemListController = ((selectedInventoryUI != null) ? selectedInventoryUI.ListController : null);
				buttonType = ButtonType.Return;
				goto IL_CD;
			}
			}
			this._infoPanelUI.DetachItem();
			this.NonSelection(this._createItemStockUI.ListController);
			return;
			IL_CD:
			if (listController == null || itemListController == null)
			{
				this._infoPanelUI.DetachItem();
				return;
			}
			this.NonSelection(listController);
			this._infoPanelUI.AttachItem(listController, itemListController, currentID, nodeUI, buttonType);
		}

		// Token: 0x06007A26 RID: 31270 RVA: 0x003358C8 File Offset: 0x00333CC8
		private void OnDoubleClick(PanelType panelType, int currnetID, ItemNodeUI nodeUI)
		{
			if (nodeUI == null)
			{
				return;
			}
			ItemListController listController;
			ItemListController receiver;
			switch (panelType)
			{
			case PanelType.Pouch:
				listController = this._pouchInventoryUI.ListController;
				receiver = this._decidedItemSlotUI.ListController;
				goto IL_B9;
			case PanelType.Chest:
				listController = this._chestInventoryUI.ListController;
				receiver = this._decidedItemSlotUI.ListController;
				goto IL_B9;
			case PanelType.DecidedItem:
			{
				listController = this._decidedItemSlotUI.ListController;
				RecyclingInventoryFacadeViewer selectedInventoryUI = this.SelectedInventoryUI;
				receiver = ((selectedInventoryUI != null) ? selectedInventoryUI.ListController : null);
				goto IL_B9;
			}
			case PanelType.CreatedItem:
			{
				listController = this._createItemStockUI.ListController;
				RecyclingInventoryFacadeViewer selectedInventoryUI2 = this.SelectedInventoryUI;
				receiver = ((selectedInventoryUI2 != null) ? selectedInventoryUI2.ListController : null);
				goto IL_B9;
			}
			}
			return;
			IL_B9:
			this.OnDoubleClicked.OnNext(panelType);
			this.SendItem(nodeUI.Item.Count, panelType, listController, receiver, currnetID, nodeUI);
		}

		// Token: 0x06007A27 RID: 31271 RVA: 0x003359B0 File Offset: 0x00333DB0
		private void SendItem(RecyclingInfoPanelUI panelUI)
		{
			if (panelUI == null || !panelUI.IsActiveItemInfo || !panelUI.IsNumberInput)
			{
				return;
			}
			UnityEx.ValueTuple<ItemListController, ItemListController, ItemNodeUI, int, ButtonType> itemInfo = panelUI.GetItemInfo();
			int inputNumber = panelUI.InputNumber;
			this.SendItem(inputNumber, itemInfo.Item1.PanelType, itemInfo.Item1, itemInfo.Item2, itemInfo.Item4, itemInfo.Item3);
		}

		// Token: 0x06007A28 RID: 31272 RVA: 0x00335A20 File Offset: 0x00333E20
		public void SendItem(int count, PanelType panelType, ItemListController sender, ItemListController receiver, int currentID, ItemNodeUI nodeUI)
		{
			if (sender == null || receiver == null || nodeUI == null || nodeUI.Item == null || count <= 0 || nodeUI.Item.Count <= 0)
			{
				return;
			}
			StuffItem stuffItem = new StuffItem(nodeUI.Item);
			stuffItem.Count = Mathf.Min(count, nodeUI.Item.Count);
			int num = receiver.AddItem(stuffItem);
			if (num <= 0)
			{
				return;
			}
			switch (panelType)
			{
			case PanelType.Pouch:
			case PanelType.Chest:
				if (this.SelectedInventoryUI != null)
				{
					List<StuffItem> itemList = this.SelectedInventoryUI.itemList;
					ItemListUI itemListUI = this.SelectedInventoryUI.itemListUI;
					StuffItem stuffItem2 = itemList.Find((StuffItem x) => x == nodeUI.Item);
					if (stuffItem2 != null)
					{
						stuffItem2.Count -= num;
						if (stuffItem2.Count <= 0)
						{
							itemList.Remove(stuffItem2);
							itemListUI.RemoveItemNode(currentID);
							itemListUI.ForceSetNonSelect();
							this._infoPanelUI.DetachItem();
						}
						Action refreshEvent = sender.RefreshEvent;
						if (refreshEvent != null)
						{
							refreshEvent();
						}
					}
				}
				break;
			case PanelType.DecidedItem:
			case PanelType.CreatedItem:
				if (sender.RemoveItem(currentID, new StuffItem(nodeUI.Item)
				{
					Count = num
				}) <= 0)
				{
					this._infoPanelUI.DetachItem();
				}
				if (this.SelectedInventoryUI != null && this.SelectedInventoryUI.itemListUI != null)
				{
					Action refreshEvent2 = this.SelectedInventoryUI.ListController.RefreshEvent;
					if (refreshEvent2 != null)
					{
						refreshEvent2();
					}
				}
				break;
			}
			this.OnSendItem.OnNext(stuffItem);
		}

		// Token: 0x06007A29 RID: 31273 RVA: 0x00335C04 File Offset: 0x00334004
		private void UpdateWarningUI()
		{
			bool flag = this._createItemStockUI.CheckInventoryEmpty();
			if (flag)
			{
				this.HideAllWarning();
			}
			else
			{
				this.ShowWarning(WarningType.PouchAndChest);
			}
			bool active = flag && !this._createItemStockUI.ItemList.IsNullOrEmpty<StuffItem>();
			this.SetInteractable(this._createItemStockUI.AllGetButton, active);
		}

		// Token: 0x06007A2A RID: 31274 RVA: 0x00335C64 File Offset: 0x00334064
		public void ShowWarning(WarningType warningType)
		{
			if (this.WarningUIs.IsNullOrEmpty<WarningViewer>())
			{
				return;
			}
			for (int i = 0; i < this.WarningUIs.Length; i++)
			{
				WarningViewer element = this.WarningUIs.GetElement(i);
				if (!(element == null))
				{
					bool flag = warningType == (WarningType)i;
					if (element.visible != flag)
					{
						element.visible = flag;
					}
				}
			}
		}

		// Token: 0x06007A2B RID: 31275 RVA: 0x00335CD4 File Offset: 0x003340D4
		public void HideAllWarning()
		{
			if (this.WarningUIs.IsNullOrEmpty<WarningViewer>())
			{
				return;
			}
			foreach (WarningViewer warningViewer in this.WarningUIs)
			{
				if (warningViewer != null || warningViewer.visible)
				{
					warningViewer.visible = false;
				}
			}
		}

		// Token: 0x06007A2C RID: 31276 RVA: 0x00335D30 File Offset: 0x00334130
		private void SetRecyclingData()
		{
			this.RecyclingData = null;
			int craftPointID = -1;
			this.CraftPointID = craftPointID;
			this._craftPointID = craftPointID;
			PlayerActor player = Map.GetPlayer();
			CraftPoint craftPoint = (player != null) ? player.CurrentCraftPoint : null;
			if (craftPoint != null)
			{
				int key = this._craftPointID = craftPoint.RegisterID;
				WorldData worldData = (!Singleton<Game>.IsInstance()) ? null : Singleton<Game>.Instance.WorldData;
				AIProject.SaveData.Environment environment = (worldData == null) ? null : worldData.Environment;
				Dictionary<int, RecyclingData> dictionary = (environment == null) ? null : environment.RecyclingDataTable;
				if (dictionary != null)
				{
					RecyclingData recyclingData = null;
					if (dictionary.TryGetValue(key, out recyclingData) && recyclingData != null)
					{
						this.RecyclingData = recyclingData;
					}
					else
					{
						RecyclingData recyclingData2 = new RecyclingData();
						dictionary[key] = recyclingData2;
						this.RecyclingData = recyclingData2;
					}
				}
			}
		}

		// Token: 0x06007A2D RID: 31277 RVA: 0x00335E10 File Offset: 0x00334210
		private void ActiveInitialize()
		{
			this.HideAllWarning();
			this.RefreshInventoryUI();
			this._infoPanelUI.Refresh();
			Action refreshEvent = this._decidedItemSlotUI.ListController.RefreshEvent;
			if (refreshEvent != null)
			{
				refreshEvent();
			}
			Action refreshEvent2 = this._createItemStockUI.ListController.RefreshEvent;
			if (refreshEvent2 != null)
			{
				refreshEvent2();
			}
		}

		// Token: 0x06007A2E RID: 31278 RVA: 0x00335E70 File Offset: 0x00334270
		private void SetActiveControl(bool active)
		{
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			IEnumerator coroutine = (!active) ? this.CloseCoroutine() : this.OpenCoroutine();
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
			}).AddTo(this);
		}

		// Token: 0x06007A2F RID: 31279 RVA: 0x00335F15 File Offset: 0x00334315
		public void DoClose()
		{
			this.PlaySE(SoundPack.SystemSE.Cancel);
			this.IsActiveControl = false;
		}

		// Token: 0x06007A30 RID: 31280 RVA: 0x00335F28 File Offset: 0x00334328
		private IEnumerator OpenCoroutine()
		{
			while (!this.Initialized || !this._pouchInventoryUI.initialized || !this._chestInventoryUI.initialized)
			{
				yield return null;
			}
			this.SetRecyclingData();
			this.ActiveInitialize();
			this.CraftPointID = this._craftPointID;
			this.SetActive(true, base.gameObject);
			MapUIContainer.SetVisibleHUD(false);
			this.BlockRaycast = true;
			bool flag = false;
			base.EnabledInput = flag;
			this.Interactable = flag;
			if (Singleton<Manager.Input>.IsInstance())
			{
				Manager.Input instance = Singleton<Manager.Input>.Instance;
				this._prevFocusLevel = instance.FocusLevel;
				int num = instance.FocusLevel + 1;
				int num2 = num;
				instance.FocusLevel = num2;
				this.SetFocusLevelAll(num2);
				instance.MenuElements = this.MenuUIBehaviours;
				instance.ReserveState(Manager.Input.ValidType.UI);
				instance.SetupState();
			}
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			float startAlpha = this.CanvasAlpha;
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			}).AddTo(this);
			stream.Connect();
			yield return stream.ToYieldInstruction<TimeInterval<float>>().AddTo(this);
			this.CanvasAlpha = 1f;
			flag = true;
			base.EnabledInput = flag;
			this.Interactable = flag;
			this.SetEnabledInputAll(true);
			yield break;
		}

		// Token: 0x06007A31 RID: 31281 RVA: 0x00335F44 File Offset: 0x00334344
		private IEnumerator CloseCoroutine()
		{
			this.SetEnabledInputAll(false);
			this.Interactable = false;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			float startAlpha = this.CanvasAlpha;
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			}).AddTo(this);
			stream.Connect();
			yield return stream.ToYieldInstruction<TimeInterval<float>>().AddTo(this);
			this.CanvasAlpha = 0f;
			this.BlockRaycast = false;
			this.SetActive(false, base.gameObject);
			MapUIContainer.SetVisibleHUD(true);
			if (Singleton<Manager.Input>.IsInstance())
			{
				Manager.Input instance = Singleton<Manager.Input>.Instance;
				instance.ClearMenuElements();
				instance.FocusLevel = this._prevFocusLevel;
			}
			this.RecyclingData = null;
			int craftPointID = -1;
			this.CraftPointID = craftPointID;
			this._craftPointID = craftPointID;
			this._infoPanelUI.DetachItem();
			this._deleteRequestUI.ForcedClose();
			if (this.OnClosedEvent != null)
			{
				Action onClosedEvent = this.OnClosedEvent;
				this.OnClosedEvent = null;
				onClosedEvent();
			}
			yield break;
		}

		// Token: 0x06007A32 RID: 31282 RVA: 0x00335F60 File Offset: 0x00334360
		private void SetFocusLevelAll(int level)
		{
			if (this.MenuUIBehaviours.IsNullOrEmpty<MenuUIBehaviour>())
			{
				return;
			}
			foreach (MenuUIBehaviour menuUIBehaviour in this.MenuUIBehaviours)
			{
				if (menuUIBehaviour != null)
				{
					menuUIBehaviour.SetFocusLevel(level);
				}
			}
		}

		// Token: 0x06007A33 RID: 31283 RVA: 0x00335FB0 File Offset: 0x003343B0
		private void SetEnabledInputAll(bool flag)
		{
			if (this.MenuUIBehaviours.IsNullOrEmpty<MenuUIBehaviour>())
			{
				return;
			}
			foreach (MenuUIBehaviour menuUIBehaviour in this.MenuUIBehaviours)
			{
				if (menuUIBehaviour != null)
				{
					menuUIBehaviour.EnabledInput = flag;
				}
			}
		}

		// Token: 0x06007A34 RID: 31284 RVA: 0x00336000 File Offset: 0x00334400
		private void SetFocusLevelAll(int level, MenuUIBehaviour[] uis)
		{
			if (uis.IsNullOrEmpty<MenuUIBehaviour>())
			{
				return;
			}
			foreach (MenuUIBehaviour menuUIBehaviour in uis)
			{
				if (menuUIBehaviour != null)
				{
					menuUIBehaviour.SetFocusLevel(level);
				}
			}
		}

		// Token: 0x06007A35 RID: 31285 RVA: 0x00336048 File Offset: 0x00334448
		private void SetEnabledInputAll(bool flag, MenuUIBehaviour[] uis)
		{
			if (uis.IsNullOrEmpty<MenuUIBehaviour>())
			{
				return;
			}
			foreach (MenuUIBehaviour menuUIBehaviour in uis)
			{
				if (menuUIBehaviour != null)
				{
					menuUIBehaviour.EnabledInput = flag;
				}
			}
		}

		// Token: 0x06007A36 RID: 31286 RVA: 0x0033608E File Offset: 0x0033448E
		private void SetActive(bool flag, GameObject obj)
		{
			if (obj != null && obj.activeSelf != flag)
			{
				obj.SetActive(flag);
			}
		}

		// Token: 0x06007A37 RID: 31287 RVA: 0x003360AF File Offset: 0x003344AF
		private void SetActive(bool flag, Component com)
		{
			if (com != null && com.gameObject.activeSelf != flag)
			{
				com.gameObject.SetActive(flag);
			}
		}

		// Token: 0x06007A38 RID: 31288 RVA: 0x003360DA File Offset: 0x003344DA
		private void SetInteractable(Selectable obj, bool active)
		{
			if (obj != null && obj.interactable != active)
			{
				obj.interactable = active;
			}
		}

		// Token: 0x06007A39 RID: 31289 RVA: 0x003360FC File Offset: 0x003344FC
		public void PlaySE(SoundPack.SystemSE se)
		{
			if (!this.Initialized || !this.IsActiveControl)
			{
				return;
			}
			SoundPack soundPack = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.SoundPack;
			if (soundPack == null)
			{
				return;
			}
			soundPack.Play(se);
		}

		// Token: 0x0400621C RID: 25116
		[SerializeField]
		private CanvasGroup _rootCanvasGroup;

		// Token: 0x0400621D RID: 25117
		[SerializeField]
		private RectTransform _rootRectTransform;

		// Token: 0x0400621E RID: 25118
		[SerializeField]
		private Button _closeButton;

		// Token: 0x0400621F RID: 25119
		[SerializeField]
		private List<int> _adultCategoryIDList = new List<int>();

		// Token: 0x04006220 RID: 25120
		[SerializeField]
		private List<ToggleElement> _toggleElements = new List<ToggleElement>();

		// Token: 0x04006221 RID: 25121
		[SerializeField]
		private RecyclingInventoryFacadeViewer _pouchInventoryUI;

		// Token: 0x04006222 RID: 25122
		[SerializeField]
		private RecyclingInventoryFacadeViewer _chestInventoryUI;

		// Token: 0x04006223 RID: 25123
		[SerializeField]
		private RecyclingInfoPanelUI _infoPanelUI;

		// Token: 0x04006224 RID: 25124
		[SerializeField]
		private RecyclingDecidedItemSlotUI _decidedItemSlotUI;

		// Token: 0x04006225 RID: 25125
		[SerializeField]
		private RecyclingCreateItemStockUI _createItemStockUI;

		// Token: 0x04006226 RID: 25126
		[SerializeField]
		private RecyclingCreatePanelUI _createPanelUI;

		// Token: 0x04006227 RID: 25127
		[SerializeField]
		private RecyclingItemDeleteRequestUI _deleteRequestUI;

		// Token: 0x04006228 RID: 25128
		[SerializeField]
		private RectTransform _warningViewerLayout;

		// Token: 0x04006229 RID: 25129
		[SerializeField]
		private WarningViewer _pouchWarningViewer;

		// Token: 0x0400622A RID: 25130
		[SerializeField]
		private WarningViewer _chestWarningViewer;

		// Token: 0x0400622B RID: 25131
		[SerializeField]
		private WarningViewer _pouchAndChestWarningViewer;

		// Token: 0x0400622D RID: 25133
		private RecyclingInventoryFacadeViewer[] _vieweres;

		// Token: 0x04006235 RID: 25141
		private int _prevFocusLevel;

		// Token: 0x04006239 RID: 25145
		private int _craftPointID = -1;

		// Token: 0x0400623D RID: 25149
		private IDisposable _fadeDisposable;
	}
}
