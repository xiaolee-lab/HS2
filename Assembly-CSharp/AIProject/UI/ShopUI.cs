using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.ColorDefine;
using AIProject.SaveData;
using AIProject.Scene;
using AIProject.UI.Viewer;
using Manager;
using ReMotion;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI
{
	// Token: 0x02000EB6 RID: 3766
	public class ShopUI : MenuUIBehaviour
	{
		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x06007B07 RID: 31495 RVA: 0x0033B020 File Offset: 0x00339420
		public PlaySE playSE { get; } = new PlaySE(false);

		// Token: 0x1700183C RID: 6204
		// (get) Token: 0x06007B08 RID: 31496 RVA: 0x0033B028 File Offset: 0x00339428
		// (set) Token: 0x06007B09 RID: 31497 RVA: 0x0033B030 File Offset: 0x00339430
		public Action OnClose { get; set; }

		// Token: 0x06007B0A RID: 31498 RVA: 0x0033B03C File Offset: 0x0033943C
		public static bool RemoveItem(int count, int sel, StuffItem item, ShopViewer.ItemListController sender, ShopUI.InventoryUI inventoryUI)
		{
			ItemNodeUI node = sender.itemListUI.GetNode(sel);
			if (sender != inventoryUI.controller)
			{
				sender.RemoveItem(sel, item);
				return false;
			}
			node.Item.Count -= count;
			if (node.Item.Count > 0)
			{
				return false;
			}
			int i = Mathf.Abs(node.Item.Count);
			List<StuffItem> itemList = inventoryUI.itemList;
			itemList.Remove(node.Item);
			sender.itemListUI.RemoveItemNode(sel);
			List<StuffItem> list = new List<StuffItem>();
			while (i > 0)
			{
				StuffItem[] array = (from x in itemList.FindItems(item)
				orderby x.Count
				select x).ToArray<StuffItem>();
				if (!array.Any<StuffItem>())
				{
					break;
				}
				foreach (StuffItem stuffItem in array)
				{
					int count2 = stuffItem.Count;
					stuffItem.Count -= i;
					i -= count2;
					if (stuffItem.Count <= 0)
					{
						list.Add(stuffItem);
					}
					if (i <= 0)
					{
						break;
					}
				}
			}
			foreach (StuffItem stuffItem2 in list)
			{
				itemList.Remove(stuffItem2);
				foreach (KeyValuePair<int, ItemNodeUI> keyValuePair in sender.itemListUI.optionTable)
				{
					if (stuffItem2 == keyValuePair.Value.Item)
					{
						sender.itemListUI.RemoveItemNode(keyValuePair.Key);
						break;
					}
				}
			}
			sender.itemListUI.ForceSetNonSelect();
			return true;
		}

		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x06007B0B RID: 31499 RVA: 0x0033B24C File Offset: 0x0033964C
		private MenuUIBehaviour[] MenuUIList
		{
			[CompilerGenerated]
			get
			{
				return this.GetCache(ref this._menuUIList, () => new MenuUIBehaviour[]
				{
					this,
					this._shopInfoPanelUI
				}.Concat(this.shopUIList).Concat(this.inventoryUIList).Concat(this.subUIList).ToArray<MenuUIBehaviour>());
			}
		}

		// Token: 0x1700183E RID: 6206
		// (get) Token: 0x06007B0C RID: 31500 RVA: 0x0033B266 File Offset: 0x00339666
		private MenuUIBehaviour[] shopUIList
		{
			[CompilerGenerated]
			get
			{
				return this.GetCache(ref this._shopUIList, () => new MenuUIBehaviour[]
				{
					this._shopViewer.controllers[0].itemListUI,
					this._shopViewer.controllers[1].itemListUI
				});
			}
		}

		// Token: 0x1700183F RID: 6207
		// (get) Token: 0x06007B0D RID: 31501 RVA: 0x0033B280 File Offset: 0x00339680
		private MenuUIBehaviour[] inventoryUIList
		{
			[CompilerGenerated]
			get
			{
				return this.GetCache(ref this._inventoryUIList, () => new MenuUIBehaviour[]
				{
					this._inventoryUI.categoryUI,
					this._inventoryUI.itemListUI,
					this._inventoryUI.itemSortUI
				});
			}
		}

		// Token: 0x17001840 RID: 6208
		// (get) Token: 0x06007B0E RID: 31502 RVA: 0x0033B29A File Offset: 0x0033969A
		private MenuUIBehaviour[] subUIList
		{
			[CompilerGenerated]
			get
			{
				return this.GetCache(ref this._subUIList, () => new MenuUIBehaviour[]
				{
					this._shopRateViewer.controller.itemListUI,
					this._shopSendViewer.controller.itemListUI
				});
			}
		}

		// Token: 0x17001841 RID: 6209
		// (get) Token: 0x06007B0F RID: 31503 RVA: 0x0033B2B4 File Offset: 0x003396B4
		private ShopViewer.ItemListController[] controllers
		{
			[CompilerGenerated]
			get
			{
				return this.GetCache(ref this._controllers, () => new ShopViewer.ItemListController[]
				{
					this._shopViewer.controllers[0],
					this._shopViewer.controllers[1],
					this._inventoryUI.controller,
					this._shopRateViewer.controller,
					this._shopSendViewer.controller
				});
			}
		}

		// Token: 0x17001842 RID: 6210
		// (get) Token: 0x06007B10 RID: 31504 RVA: 0x0033B2CE File Offset: 0x003396CE
		private int openAreaID
		{
			[CompilerGenerated]
			get
			{
				return this.merchant.OpenAreaID;
			}
		}

		// Token: 0x17001843 RID: 6211
		// (get) Token: 0x06007B11 RID: 31505 RVA: 0x0033B2DB File Offset: 0x003396DB
		private MerchantActor merchant
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Map>.IsInstance() ? Singleton<Map>.Instance.Merchant : null;
			}
		}

		// Token: 0x17001844 RID: 6212
		// (get) Token: 0x06007B12 RID: 31506 RVA: 0x0033B2F7 File Offset: 0x003396F7
		// (set) Token: 0x06007B13 RID: 31507 RVA: 0x0033B2FF File Offset: 0x003396FF
		private bool initialized { get; set; }

		// Token: 0x17001845 RID: 6213
		// (get) Token: 0x06007B14 RID: 31508 RVA: 0x0033B308 File Offset: 0x00339708
		// (set) Token: 0x06007B15 RID: 31509 RVA: 0x0033B310 File Offset: 0x00339710
		private bool isTradeCheckActive { get; set; }

		// Token: 0x06007B16 RID: 31510 RVA: 0x0033B31C File Offset: 0x0033971C
		protected override void Start()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			base.StartCoroutine(this.BindingUI());
			ActionIDDownCommand actionIDDownCommand = new ActionIDDownCommand
			{
				ActionID = ActionID.Submit
			};
			actionIDDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.OnInputSubmit();
			});
			this._actionCommands.Add(actionIDDownCommand);
			ActionIDDownCommand actionIDDownCommand2 = new ActionIDDownCommand
			{
				ActionID = ActionID.Cancel
			};
			actionIDDownCommand2.TriggerEvent.AddListener(delegate()
			{
				this.OnInputCancel();
			});
			this._actionCommands.Add(actionIDDownCommand2);
			ActionIDDownCommand actionIDDownCommand3 = new ActionIDDownCommand
			{
				ActionID = ActionID.MouseRight
			};
			actionIDDownCommand3.TriggerEvent.AddListener(delegate()
			{
				this.OnInputCancel();
			});
			this._actionCommands.Add(actionIDDownCommand3);
			base.Start();
		}

		// Token: 0x06007B17 RID: 31511 RVA: 0x0033B3F0 File Offset: 0x003397F0
		private IEnumerator BindingUI()
		{
			yield return base.StartCoroutine(this._inventoryUI.Initialize());
			foreach (ShopViewer.ItemListController itemListController in from p in this.controllers
			where !p.itemListUI.isOptionNode
			select p)
			{
				string text = "shop_";
				if (itemListController == this._shopRateViewer.controller || itemListController == this._shopSendViewer.controller)
				{
					text += "sub";
				}
				else if (itemListController == this._inventoryUI.controller)
				{
					text = "new";
				}
				else
				{
					text += "main";
				}
				string bundle = Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
				GameObject gameObject = CommonLib.LoadAsset<GameObject>(bundle, string.Format("ItemOption_{0}", text), false, string.Empty);
				if (!(gameObject == null))
				{
					if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundle))
					{
						MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundle, string.Empty));
					}
					itemListController.itemListUI.SetOptionNode(gameObject);
				}
			}
			GameObject[] array = new GameObject[]
			{
				this._shopVisible,
				this._inventoryVisible
			};
			int sel;
			this._shopTagSelection.Selection = delegate(int sel)
			{
				this._shopInfoPanelUI.Close();
				this._inventoryUI.itemSortUI.Close();
				for (int j = 0; j < array.Length; j++)
				{
					array[j].SetActive(j == sel);
				}
				foreach (ItemListUI itemListUI in this.shopUIList.OfType<ItemListUI>())
				{
					itemListUI.ForceSetNonSelect();
				}
				foreach (ItemListUI itemListUI2 in this.inventoryUIList.OfType<ItemListUI>())
				{
					itemListUI2.ForceSetNonSelect();
				}
				this.playSE.Play(SoundPack.SystemSE.OK_S);
			};
			Action<ShopViewer.ItemListController> nonSelect = delegate(ShopViewer.ItemListController controller)
			{
				foreach (ShopViewer.ItemListController itemListController3 in from p in this.controllers
				where p != controller
				select p)
				{
					itemListController3.itemListUI.ForceSetNonSelect();
				}
			};
			sel = -1;
			ShopViewer.ItemListController sender = null;
			ShopViewer.ItemListController receiver = null;
			this._shopViewer.normals.CurrentChanged += delegate(int id, ItemNodeUI node)
			{
				if (node == null)
				{
					return;
				}
				ShopViewer.ItemListController itemListController3 = this._shopViewer.controllers[0];
				sender = itemListController3;
				receiver = this._shopRateViewer.controller;
				sel = id;
				this._shopInfoPanelUI.mode = ShopInfoPanelUI.Mode.Shop;
				this._shopInfoPanelUI.decide = true;
				this._shopInfoPanelUI.Open(node);
				nonSelect(itemListController3);
			};
			this._shopViewer.specials.CurrentChanged += delegate(int id, ItemNodeUI node)
			{
				if (node == null)
				{
					return;
				}
				ShopViewer.ItemListController itemListController3 = this._shopViewer.controllers[1];
				sender = itemListController3;
				receiver = this._shopRateViewer.controller;
				sel = id;
				this._shopInfoPanelUI.mode = ShopInfoPanelUI.Mode.Shop;
				this._shopInfoPanelUI.decide = true;
				this._shopInfoPanelUI.Open(node);
				nonSelect(itemListController3);
			};
			this._inventoryUI.itemListUI.CurrentChanged += delegate(int id, ItemNodeUI node)
			{
				if (node == null)
				{
					return;
				}
				ShopViewer.ItemListController controller = this._inventoryUI.controller;
				sender = controller;
				receiver = this._shopSendViewer.controller;
				sel = id;
				this._shopInfoPanelUI.mode = ShopInfoPanelUI.Mode.Inventory;
				this._shopInfoPanelUI.decide = true;
				this._shopInfoPanelUI.Open(node);
				nonSelect(controller);
			};
			this._shopRateViewer.itemListUI.CurrentChanged += delegate(int id, ItemNodeUI node)
			{
				if (node == null)
				{
					return;
				}
				ShopViewer.ItemListController controller = this._shopRateViewer.controller;
				sender = controller;
				receiver = null;
				sel = id;
				this._shopInfoPanelUI.mode = ShopInfoPanelUI.Mode.Shop;
				this._shopInfoPanelUI.decide = false;
				this._shopInfoPanelUI.Open(node);
				nonSelect(controller);
			};
			this._shopSendViewer.itemListUI.CurrentChanged += delegate(int id, ItemNodeUI node)
			{
				if (node == null)
				{
					return;
				}
				ShopViewer.ItemListController controller = this._shopSendViewer.controller;
				sender = controller;
				receiver = null;
				sel = id;
				this._shopInfoPanelUI.mode = ShopInfoPanelUI.Mode.Inventory;
				this._shopInfoPanelUI.decide = false;
				this._shopInfoPanelUI.Open(node);
				nonSelect(controller);
			};
			this._inventoryUI.ItemNodeOnDoubleClick = delegate(InventoryFacadeViewer.DoubleClickData x)
			{
				if (!x.node.isTrash)
				{
					return;
				}
				this.ItemDecideProc(this._shopInfoPanelUI.MaxCount, x.ID, sender, receiver);
			};
			foreach (ShopViewer.ItemListController itemListController2 in from p in this.controllers
			where p != this._inventoryUI.controller
			select p)
			{
				itemListController2.DoubleClick += delegate(int id, ItemNodeUI node)
				{
					if (this._shopInfoPanelUI.decide)
					{
						this.ItemDecideProc(this._shopInfoPanelUI.MaxCount, id, sender, receiver);
					}
					else
					{
						this.ItemReturnProc(this._shopInfoPanelUI.MaxCount, id, sender, receiver);
					}
				};
			}
			this._shopInfoPanelUI.Decide.Subscribe(delegate(Unit _)
			{
				this.playSE.Play(SoundPack.SystemSE.OK_S);
				this.ItemDecideProc(this._shopInfoPanelUI.Count, sel, sender, receiver);
			});
			this._shopInfoPanelUI.Return.Subscribe(delegate(Unit _)
			{
				this.playSE.Play(SoundPack.SystemSE.OK_S);
				this.ItemReturnProc(this._shopInfoPanelUI.Count, sel, sender, receiver);
			});
			while (!Singleton<Manager.Resources>.IsInstance())
			{
				yield return null;
			}
			Manager.Resources.GameInfoTables gameInfo = Singleton<Manager.Resources>.Instance.GameInfo;
			while (!gameInfo.VendItemInfoTable.Any<KeyValuePair<int, Dictionary<int, List<VendItemInfo>>>>())
			{
				yield return null;
			}
			while (gameInfo.VendItemInfoSpecialTable == null)
			{
				yield return null;
			}
			while (this.merchant == null)
			{
				yield return null;
			}
			this.merchant.MerchantData.ResetSpecialVendor(gameInfo.VendItemInfoSpecialTable);
			while (!Singleton<Map>.IsInstance() || Singleton<Map>.Instance.Simulator == null)
			{
				yield return null;
			}
			while (!Singleton<MapScene>.IsInstance() || !Singleton<MapScene>.Instance.isLoadEnd)
			{
				yield return null;
			}
			bool dayChanged = !Singleton<Map>.Instance.IsHour7After;
			Action resetVendor = delegate()
			{
				dayChanged = false;
				this.merchant.MerchantData.ResetVendor(gameInfo.VendItemInfoTable[this.openAreaID]);
			};
			if (!this.merchant.MerchantData.vendorItemList.Any<MerchantData.VendorItem>())
			{
				resetVendor();
			}
			Singleton<Map>.Instance.Simulator.OnDayAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(TimeSpan _)
			{
				dayChanged = true;
			});
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where dayChanged
			select _).Subscribe(delegate(long _)
			{
				if (Singleton<Map>.Instance.IsHour7After)
				{
					resetVendor();
				}
			});
			if (this._warningViewer != null)
			{
				yield break;
			}
			yield return WarningViewer.Load(this._warningViewerLayout, delegate(WarningViewer warningViewer)
			{
				this._warningViewer = warningViewer;
			});
			this._warningViewer.msgID = 0;
			this._closeButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnInputCancel();
			});
			this._tradeButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.Trade();
			});
			Text tradeText = this._tradeButton.GetComponentInChildren<Text>();
			Color baseTextColor = tradeText.color;
			(from _ in this._tradeButton.OnPointerEnterAsObservable()
			where this._tradeButton.interactable
			select _).Subscribe(delegate(PointerEventData _)
			{
				tradeText.color = Define.Get(Colors.Orange);
			});
			this._tradeButton.OnPointerExitAsObservable().Subscribe(delegate(PointerEventData _)
			{
				tradeText.color = baseTextColor;
			});
			(from _ in this._shopRateViewer.rateCounter.X
			where this.isTradeCheckActive
			select _).Subscribe(delegate(int _)
			{
				bool active = this._shopRateViewer.isTrade && this._shopSendViewer.itemListUI.optionTable.Count > 0;
				this._tradeButton.gameObject.SetActive(active);
				bool flag = this.InInventoryPossibleCheck();
				this._warningViewer.visible = !flag;
				this._tradeButton.interactable = flag;
			});
			(from _ in this._shopViewer.normals.OnEntered
			where this.IsActiveControl
			select _).Subscribe(delegate(int _)
			{
				this.SetFocusLevel(this._shopViewer.normals.FocusLevel);
			});
			(from _ in this._shopViewer.specials.OnEntered
			where this.IsActiveControl
			select _).Subscribe(delegate(int _)
			{
				this.SetFocusLevel(this._shopViewer.specials.FocusLevel);
			});
			ShopUI.InventoryUI item = this._inventoryUI;
			item.categoryUI.OnSubmit.AddListener(delegate()
			{
				if (item.categoryUI.useCursor)
				{
					item.categoryUI.SelectedButton.onClick.Invoke();
				}
				else if (item.cursor.enabled)
				{
					if (item.cursor.transform.position == item.sorter.transform.position)
					{
						item.sorter.isOn = !item.sorter.isOn;
					}
					else if (item.cursor.transform.position == item.sortButton.transform.position)
					{
						item.sortButton.onClick.Invoke();
					}
				}
			});
			item.categoryUI.OnCancel.AddListener(delegate()
			{
				this.OnInputCancel();
			});
			ItemSortUI sortUI = item.itemSortUI;
			sortUI.OnCancel.AddListener(delegate()
			{
				this.SetFocusLevel(item.categoryUI.FocusLevel);
				item.categoryUI.SelectedID = item.categoryUI.CategoryID;
				item.categoryUI.useCursor = true;
			});
			item.sortButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				if (!sortUI.isOpen)
				{
					this._shopInfoPanelUI.Close();
					item.itemListUI.ForceSetNonSelect();
				}
			});
			this._shopRateViewer.GetComponent<Image>().OnPointerDownAsObservable().Subscribe(delegate(PointerEventData _)
			{
				this._shopTagSelection[0].isOn = true;
			});
			this._shopSendViewer.GetComponent<Image>().OnPointerDownAsObservable().Subscribe(delegate(PointerEventData _)
			{
				this._shopTagSelection[1].isOn = true;
			});
			(from _ in this._shopRateViewer.itemListUI.OnEntered
			where this.IsActiveControl
			select _).Subscribe(delegate(int _)
			{
				this.SetFocusLevel(this._shopRateViewer.itemListUI.FocusLevel);
			});
			(from _ in this._shopSendViewer.itemListUI.OnEntered
			where this.IsActiveControl
			select _).Subscribe(delegate(int _)
			{
				this.SetFocusLevel(this._shopSendViewer.itemListUI.FocusLevel);
			});
			foreach (UnityEvent unityEvent in new UnityEvent[]
			{
				this._inventoryUI.itemListUI.OnCancel,
				this._shopViewer.normals.OnCancel,
				this._shopViewer.specials.OnCancel,
				this._shopRateViewer.itemListUI.OnCancel,
				this._shopSendViewer.itemListUI.OnCancel,
				this._shopInfoPanelUI.OnCancel
			})
			{
				unityEvent.AddListener(delegate()
				{
					this.OnInputCancel();
				});
			}
			this.initialized = true;
			yield break;
		}

		// Token: 0x06007B18 RID: 31512 RVA: 0x0033B40C File Offset: 0x0033980C
		private void ItemDecideProc(int count, int sel, ShopViewer.ItemListController sender, ShopViewer.ItemListController receiver)
		{
			ItemNodeUI node = sender.itemListUI.GetNode(sel);
			MerchantData.VendorItem source = node.Item as MerchantData.VendorItem;
			StuffItem stuffItem;
			if (this._shopInfoPanelUI.mode == ShopInfoPanelUI.Mode.Shop)
			{
				stuffItem = new MerchantData.VendorItem(source);
			}
			else
			{
				stuffItem = new StuffItem(node.Item);
			}
			stuffItem.Count = count;
			receiver.AddItem(stuffItem, new ShopViewer.ExtraPadding(node.Item, sender));
			if (ShopUI.RemoveItem(count, sel, stuffItem, sender, this._inventoryUI))
			{
				this.SetFocusLevel(sender.itemListUI.FocusLevel);
			}
			bool flag = this._inventoryUI.itemListUI == receiver.itemListUI;
			if (!flag)
			{
				receiver.itemListUI.Refresh();
			}
			bool flag2 = this._inventoryUI.itemListUI == sender.itemListUI;
			if (!flag2)
			{
				sender.itemListUI.Refresh();
			}
			if (flag || flag2)
			{
				this._inventoryUI.Refresh();
			}
			this._shopInfoPanelUI.Refresh();
			int num = node.Rate * count;
			if (receiver == this._shopSendViewer.controller)
			{
				this._shopRateViewer.rateCounter.x += num;
			}
			else if (receiver == this._shopRateViewer.controller)
			{
				this._shopRateViewer.rateCounter.y += num;
			}
		}

		// Token: 0x06007B19 RID: 31513 RVA: 0x0033B578 File Offset: 0x00339978
		private void ItemReturnProc(int count, int sel, ShopViewer.ItemListController sender, ShopViewer.ItemListController receiver)
		{
			ItemNodeUI node = sender.itemListUI.GetNode(sel);
			MerchantData.VendorItem source = node.Item as MerchantData.VendorItem;
			StuffItem stuffItem;
			if (this._shopInfoPanelUI.mode == ShopInfoPanelUI.Mode.Shop)
			{
				stuffItem = new MerchantData.VendorItem(source);
			}
			else
			{
				stuffItem = new StuffItem(node.Item);
			}
			stuffItem.Count = count;
			sender.RemoveItem(sel, stuffItem);
			ShopViewer.ExtraPadding extraPadding = node.extraData as ShopViewer.ExtraPadding;
			receiver = extraPadding.source;
			if (receiver != this._inventoryUI.controller)
			{
				receiver.AddItem(stuffItem, new ShopViewer.ExtraPadding(extraPadding.item, sender));
			}
			else if (this._inventoryUI.itemList.AddItem(stuffItem))
			{
				this._inventoryUI.ItemListAddNode(this._inventoryUI.itemListUI.SearchNotUsedIndex, stuffItem);
				this._inventoryUI.ItemListNodeFilter(this._inventoryUI.categoryUI.CategoryID, true);
			}
			bool flag = this._inventoryUI.itemListUI == receiver.itemListUI;
			if (!flag)
			{
				receiver.itemListUI.Refresh();
			}
			bool flag2 = this._inventoryUI.itemListUI == sender.itemListUI;
			if (!flag2)
			{
				sender.itemListUI.Refresh();
			}
			if (flag || flag2)
			{
				this._inventoryUI.Refresh();
			}
			this._shopInfoPanelUI.Refresh();
			int num = node.Rate * count;
			if (sender == this._shopSendViewer.controller)
			{
				this._shopRateViewer.rateCounter.x -= num;
			}
			else if (sender == this._shopRateViewer.controller)
			{
				this._shopRateViewer.rateCounter.y -= num;
			}
		}

		// Token: 0x06007B1A RID: 31514 RVA: 0x0033B740 File Offset: 0x00339B40
		private bool InInventoryPossibleCheck()
		{
			List<StuffItem> list = (from item in this._inventoryUI.itemList
			select new StuffItem(item)).ToList<StuffItem>();
			int y = this._inventoryUI.slotCounter.y;
			Manager.Resources.GameInfoTables gameInfo = Singleton<Manager.Resources>.Instance.GameInfo;
			var lookup = (from item in list
			select new
			{
				item,
				gameInfo.GetItem(item.CategoryID, item.ID).nameHash
			}).ToLookup(p => p.nameHash);
			int itemSlotMax = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.ItemSlotMax;
			List<StuffItem> list2 = new List<StuffItem>();
			foreach (var grouping in lookup)
			{
				int num = grouping.Sum(x => x.item.Count);
				foreach (var <>__AnonType in grouping)
				{
					if (num > itemSlotMax)
					{
						<>__AnonType.item.Count = itemSlotMax;
						num -= itemSlotMax;
					}
					else
					{
						<>__AnonType.item.Count = num;
						num = 0;
					}
				}
				list2.AddRange(from x in grouping
				where x.item.Count <= 0
				select x.item);
			}
			foreach (StuffItem item3 in list2)
			{
				list.Remove(item3);
			}
			foreach (ItemNodeUI itemNodeUI in this._shopRateViewer.itemListUI)
			{
				StuffItem item2 = itemNodeUI.Item;
				if (!list.CanAddItem(y, item2))
				{
					return false;
				}
				list.AddItem(new StuffItem(item2));
			}
			return y >= list.Count;
		}

		// Token: 0x06007B1B RID: 31515 RVA: 0x0033B9FC File Offset: 0x00339DFC
		private void Trade()
		{
			foreach (ItemNodeUI itemNodeUI in this._shopRateViewer.itemListUI)
			{
				StuffItem item3 = new StuffItem(itemNodeUI.Item);
				if (this._inventoryUI.itemList.AddItem(item3))
				{
					this._inventoryUI.ItemListAddNode(this._inventoryUI.itemListUI.SearchNotUsedIndex, item3);
					this._inventoryUI.ItemListNodeFilter(this._inventoryUI.categoryUI.CategoryID, true);
				}
			}
			var lookup = this._inventoryUI.itemList.Select(delegate(StuffItem item)
			{
				int id = -1;
				foreach (KeyValuePair<int, ItemNodeUI> keyValuePair in this._inventoryUI.itemListUI.optionTable)
				{
					if (keyValuePair.Value.Item == item)
					{
						id = keyValuePair.Key;
						break;
					}
				}
				return new
				{
					item,
					Singleton<Manager.Resources>.Instance.GameInfo.GetItem(item.CategoryID, item.ID).nameHash,
					id
				};
			}).ToLookup(p => p.nameHash);
			int itemSlotMax = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.ItemSlotMax;
			List<StuffItem> list = new List<StuffItem>();
			foreach (var grouping in lookup)
			{
				int num = grouping.Sum(x => x.item.Count);
				foreach (var <>__AnonType in grouping)
				{
					if (num > itemSlotMax)
					{
						<>__AnonType.item.Count = itemSlotMax;
						num -= itemSlotMax;
					}
					else
					{
						<>__AnonType.item.Count = num;
						num = 0;
					}
				}
				var array = (from x in grouping
				where x.item.Count <= 0
				select x).ToArray();
				var array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					var <>__AnonType2 = array2[i];
					this._inventoryUI.itemListUI.RemoveItemNode(<>__AnonType2.id);
				}
				list.AddRange(from x in array
				select x.item);
			}
			foreach (StuffItem item2 in list)
			{
				this._inventoryUI.itemList.Remove(item2);
			}
			Singleton<Map>.Instance.Player.PlayerData.SpendMoney += this._shopRateViewer.rateCounter.x;
			this._shopRateViewer.controller.Clear();
			this._shopSendViewer.controller.Clear();
			this._shopRateViewer.rateCounter.x = 0;
			this._shopRateViewer.rateCounter.y = 0;
			this._inventoryUI.Refresh();
			this.playSE.Play(SoundPack.SystemSE.Shop);
		}

		// Token: 0x06007B1C RID: 31516 RVA: 0x0033BD84 File Offset: 0x0033A184
		private void Reverse()
		{
			foreach (ItemNodeUI itemNodeUI in this._shopRateViewer.itemListUI)
			{
				ShopViewer.ExtraPadding extraPadding = itemNodeUI.extraData as ShopViewer.ExtraPadding;
				ShopViewer.ItemListController source = extraPadding.source;
				StuffItem item = itemNodeUI.Item;
				if (source == this._shopViewer.controllers[0])
				{
					this._shopViewer.controllers[0].AddItem(item, extraPadding);
				}
				else if (source == this._shopViewer.controllers[1])
				{
					this._shopViewer.controllers[1].AddItem(item, extraPadding);
				}
			}
			this._shopRateViewer.controller.Clear();
			foreach (ItemNodeUI itemNodeUI2 in this._shopSendViewer.itemListUI)
			{
				StuffItem item2 = itemNodeUI2.Item;
				if (this._inventoryUI.itemList.AddItem(item2))
				{
					this._inventoryUI.ItemListAddNode(this._inventoryUI.itemListUI.SearchNotUsedIndex, item2);
					this._inventoryUI.ItemListNodeFilter(this._inventoryUI.categoryUI.CategoryID, true);
				}
			}
			this._shopSendViewer.controller.Clear();
			this._shopRateViewer.rateCounter.x = 0;
			this._shopRateViewer.rateCounter.y = 0;
		}

		// Token: 0x06007B1D RID: 31517 RVA: 0x0033BF3C File Offset: 0x0033A33C
		private void SetActiveControl(bool isActive)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (isActive)
			{
				instance.FocusLevel = 0;
				instance.MenuElements = this.MenuUIList;
				coroutine = this.DoOpen();
			}
			else
			{
				instance.ClearMenuElements();
				instance.FocusLevel = 0;
				coroutine = this.DoClose();
			}
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
			});
		}

		// Token: 0x06007B1E RID: 31518 RVA: 0x0033C007 File Offset: 0x0033A407
		private void Close()
		{
			Time.timeScale = 1f;
			this.IsActiveControl = false;
			Action onClose = this.OnClose;
			if (onClose != null)
			{
				onClose();
			}
			this.playSE.Play(SoundPack.SystemSE.Cancel);
		}

		// Token: 0x06007B1F RID: 31519 RVA: 0x0033C03C File Offset: 0x0033A43C
		private IEnumerator DoOpen()
		{
			yield return MapUIContainer.DrawOnceTutorialUI(10, null);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			this._canvasGroup.blocksRaycasts = false;
			while (!this.initialized)
			{
				yield return null;
			}
			this._shopTagSelection[0].isOn = true;
			ShopUI.InventoryUI item = this._inventoryUI;
			while (!item.initialized)
			{
				yield return null;
			}
			item.slotCounter.y = Singleton<Map>.Instance.Player.PlayerData.InventorySlotMax;
			item.itemSortUI.SetDefault();
			item.itemSortUI.Close();
			item.ItemListNodeCreate();
			item.Refresh();
			this.isTradeCheckActive = true;
			this._shopRateViewer.rateCounter.Refresh();
			while (this.merchant == null)
			{
				yield return null;
			}
			while (!Singleton<Manager.Resources>.IsInstance() || Singleton<Manager.Resources>.Instance.GameInfo == null || !Singleton<Manager.Resources>.Instance.GameInfo.initialized)
			{
				yield return null;
			}
			this.merchant.MerchantData.ResetSpecialVendor(Singleton<Manager.Resources>.Instance.GameInfo.VendItemInfoSpecialTable);
			foreach (ShopViewer.ItemListController itemListController in this.controllers)
			{
				if (itemListController == this._shopViewer.controllers[0])
				{
					itemListController.Create(this.merchant.MerchantData.vendorItemList);
				}
				else if (itemListController == this._shopViewer.controllers[1])
				{
					itemListController.Create(this.merchant.MerchantData.vendorSpItemTable.Values);
				}
			}
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return fadeInStream.ToYieldInstruction<TimeInterval<float>>();
			this._canvasGroup.blocksRaycasts = true;
			Singleton<Manager.Input>.Instance.FocusLevel = 0;
			base.EnabledInput = true;
			this.playSE.use = true;
			yield break;
		}

		// Token: 0x06007B20 RID: 31520 RVA: 0x0033C058 File Offset: 0x0033A458
		private IEnumerator DoClose()
		{
			base.EnabledInput = false;
			foreach (ShopViewer.ItemListController itemListController in this.controllers)
			{
				itemListController.itemListUI.EnabledInput = false;
			}
			this._canvasGroup.blocksRaycasts = false;
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeOutStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return fadeOutStream.ToYieldInstruction<TimeInterval<float>>();
			this._shopInfoPanelUI.Close();
			this.isTradeCheckActive = false;
			this.Reverse();
			foreach (ShopViewer.ItemListController itemListController2 in this.controllers)
			{
				itemListController2.Clear();
			}
			ShopUI.InventoryUI inventoryUI = this._inventoryUI;
			inventoryUI.cursor.enabled = false;
			inventoryUI.itemSortUI.SetDefault();
			inventoryUI.itemSortUI.Close();
			Singleton<Manager.Input>.Instance.SetupState();
			this.playSE.use = false;
			yield break;
		}

		// Token: 0x06007B21 RID: 31521 RVA: 0x0033C074 File Offset: 0x0033A474
		private new void SetFocusLevel(int level)
		{
			Singleton<Manager.Input>.Instance.FocusLevel = level;
			MenuUIBehaviour[] array = null;
			MenuUIBehaviour[] array2 = null;
			int index = this._shopTagSelection.Index;
			if (index != 0)
			{
				if (index == 1)
				{
					array = this.inventoryUIList;
					array2 = this.shopUIList;
				}
			}
			else
			{
				array = this.shopUIList;
				array2 = this.inventoryUIList;
			}
			if (array == null || array2 == null)
			{
				return;
			}
			foreach (MenuUIBehaviour menuUIBehaviour in array)
			{
				menuUIBehaviour.EnabledInput = true;
			}
			foreach (MenuUIBehaviour menuUIBehaviour2 in array2)
			{
				menuUIBehaviour2.EnabledInput = false;
			}
			foreach (MenuUIBehaviour menuUIBehaviour3 in this.subUIList)
			{
				menuUIBehaviour3.EnabledInput = true;
			}
		}

		// Token: 0x06007B22 RID: 31522 RVA: 0x0033C16A File Offset: 0x0033A56A
		private void OnInputSubmit()
		{
		}

		// Token: 0x06007B23 RID: 31523 RVA: 0x0033C16C File Offset: 0x0033A56C
		private void OnInputCancel()
		{
			this.Close();
		}

		// Token: 0x040062AA RID: 25258
		[Header("ShopUI Setting")]
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x040062AB RID: 25259
		[SerializeField]
		private ShopTagSelectionUI _shopTagSelection;

		// Token: 0x040062AC RID: 25260
		[Header("ShopViewer")]
		[SerializeField]
		private GameObject _shopVisible;

		// Token: 0x040062AD RID: 25261
		[SerializeField]
		private ShopViewer _shopViewer;

		// Token: 0x040062AE RID: 25262
		[Header("InventoryViewer")]
		[SerializeField]
		private GameObject _inventoryVisible;

		// Token: 0x040062AF RID: 25263
		[SerializeField]
		private ShopUI.InventoryUI _inventoryUI;

		// Token: 0x040062B0 RID: 25264
		[SerializeField]
		private Button _closeButton;

		// Token: 0x040062B1 RID: 25265
		[SerializeField]
		private ShopInfoPanelUI _shopInfoPanelUI;

		// Token: 0x040062B2 RID: 25266
		[SerializeField]
		private ShopRateViewer _shopRateViewer;

		// Token: 0x040062B3 RID: 25267
		[SerializeField]
		private ShopSendViewer _shopSendViewer;

		// Token: 0x040062B4 RID: 25268
		[SerializeField]
		private Button _tradeButton;

		// Token: 0x040062B5 RID: 25269
		[Header("WarningViewer")]
		[SerializeField]
		private RectTransform _warningViewerLayout;

		// Token: 0x040062B6 RID: 25270
		[SerializeField]
		private WarningViewer _warningViewer;

		// Token: 0x040062B7 RID: 25271
		private IDisposable _fadeDisposable;

		// Token: 0x040062B8 RID: 25272
		private MenuUIBehaviour[] _menuUIList;

		// Token: 0x040062B9 RID: 25273
		private MenuUIBehaviour[] _shopUIList;

		// Token: 0x040062BA RID: 25274
		private MenuUIBehaviour[] _inventoryUIList;

		// Token: 0x040062BB RID: 25275
		private MenuUIBehaviour[] _subUIList;

		// Token: 0x040062BC RID: 25276
		private ShopViewer.ItemListController[] _controllers;

		// Token: 0x02000EB7 RID: 3767
		[Serializable]
		public class InventoryUI : InventoryFacadeViewer
		{
			// Token: 0x17001846 RID: 6214
			// (get) Token: 0x06007B3B RID: 31547 RVA: 0x0033C3EF File Offset: 0x0033A7EF
			public ItemSortUI itemSortUI
			{
				[CompilerGenerated]
				get
				{
					return base.viewer.sortUI;
				}
			}

			// Token: 0x17001847 RID: 6215
			// (get) Token: 0x06007B3C RID: 31548 RVA: 0x0033C3FC File Offset: 0x0033A7FC
			public Toggle sorter
			{
				[CompilerGenerated]
				get
				{
					return base.viewer.sorter;
				}
			}

			// Token: 0x17001848 RID: 6216
			// (get) Token: 0x06007B3D RID: 31549 RVA: 0x0033C409 File Offset: 0x0033A809
			public Button sortButton
			{
				[CompilerGenerated]
				get
				{
					return base.viewer.sortButton;
				}
			}

			// Token: 0x17001849 RID: 6217
			// (get) Token: 0x06007B3E RID: 31550 RVA: 0x0033C416 File Offset: 0x0033A816
			public ShopViewer.ItemListController controller { get; } = new ShopViewer.ItemListController();

			// Token: 0x06007B3F RID: 31551 RVA: 0x0033C420 File Offset: 0x0033A820
			public override IEnumerator Initialize()
			{
				while (Singleton<Map>.Instance.Player == null)
				{
					yield return null;
				}
				base.SetItemList(Singleton<Map>.Instance.Player.PlayerData.ItemList);
				yield return base.Initialize();
				Vector3 position = base.viewer.sortButton.transform.position;
				position.x -= 50f;
				base.viewer.sortButton.transform.position = position;
				this.controller.Bind(base.viewer.itemListUI);
				yield break;
			}

			// Token: 0x06007B40 RID: 31552 RVA: 0x0033C43C File Offset: 0x0033A83C
			public override void ItemListNodeCreate()
			{
				int[] categorize = new int[0];
				base.viewer.itemListUI.ClearItems();
				base.viewer.sortUI.SetDefault();
				base.viewer.sortUI.Close();
				base.viewer.sorter.isOn = true;
				base.viewer.categoryUI.Filter(categorize);
				List<StuffItem> viewList = (from item in base.itemList
				select new
				{
					item = item,
					info = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(item.CategoryID, item.ID)
				} into p
				select p.item).ToList<StuffItem>();
				foreach (var <>__AnonType in (from i in Enumerable.Range(0, viewList.Count)
				where !categorize.Any<int>() || categorize.Contains(viewList[i].CategoryID)
				select i).Select((int i, int index) => new
				{
					item = viewList[i],
					index = index
				}))
				{
					this.ItemListAddNode(<>__AnonType.index, <>__AnonType.item);
				}
				int num = 0;
				base.viewer.categoryUI.SetSelectAndCategory(num);
				this.ItemListNodeFilter(num, true);
			}

			// Token: 0x06007B41 RID: 31553 RVA: 0x0033C5A8 File Offset: 0x0033A9A8
			protected override void CanvasGroupSetting()
			{
			}
		}
	}
}
