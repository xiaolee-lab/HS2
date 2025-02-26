using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using AIProject.Scene;
using AIProject.UI.Viewer;
using Illusion.Game.Extensions;
using Manager;
using ReMotion;
using Sirenix.OdinInspector;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI
{
	// Token: 0x02000E83 RID: 3715
	public class ItemBoxUI : MenuUIBehaviour
	{
		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x060076BA RID: 30394 RVA: 0x003244A8 File Offset: 0x003228A8
		public PlaySE playSE { get; } = new PlaySE(false);

		// Token: 0x060076BB RID: 30395 RVA: 0x003244B0 File Offset: 0x003228B0
		public virtual IEnumerator SetStorage(ItemBoxUI.ItemBoxDataPack pack, Action<List<StuffItem>> action)
		{
			ItemBoxUI.SelectedElement sel = pack.sel;
			if (sel != ItemBoxUI.SelectedElement.Inventory)
			{
				if (sel == ItemBoxUI.SelectedElement.ItemBox)
				{
					while (Singleton<Game>.Instance.Environment == null)
					{
						yield return null;
					}
					action(Singleton<Game>.Instance.Environment.ItemListInStorage);
				}
			}
			else
			{
				while (Singleton<Map>.Instance.Player == null)
				{
					yield return null;
				}
				action(Singleton<Map>.Instance.Player.PlayerData.ItemList);
			}
			yield break;
		}

		// Token: 0x060076BC RID: 30396 RVA: 0x003244D2 File Offset: 0x003228D2
		public virtual void ViewCategorize(out int[] categorize, out List<StuffItem> viewList, List<StuffItem> itemList)
		{
			categorize = new int[0];
			viewList = itemList;
		}

		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x060076BD RID: 30397 RVA: 0x003244DF File Offset: 0x003228DF
		// (set) Token: 0x060076BE RID: 30398 RVA: 0x003244E7 File Offset: 0x003228E7
		private int _selectedIndexOf { get; set; }

		// Token: 0x17001734 RID: 5940
		// (get) Token: 0x060076BF RID: 30399 RVA: 0x003244F0 File Offset: 0x003228F0
		// (set) Token: 0x060076C0 RID: 30400 RVA: 0x003244F8 File Offset: 0x003228F8
		private bool isSendAll { get; set; }

		// Token: 0x17001735 RID: 5941
		// (get) Token: 0x060076C1 RID: 30401 RVA: 0x00324501 File Offset: 0x00322901
		private ItemBoxUI.ItemBoxDataPack[] itemPacks
		{
			[CompilerGenerated]
			get
			{
				return this.GetCache(ref this._itemPacks, () => new ItemBoxUI.ItemBoxDataPack[]
				{
					this._inventoryUI,
					this._itemBoxUI
				});
			}
		}

		// Token: 0x17001736 RID: 5942
		// (get) Token: 0x060076C2 RID: 30402 RVA: 0x0032451B File Offset: 0x0032291B
		private ItemBoxUI.ItemBoxDataPack currentPack
		{
			[CompilerGenerated]
			get
			{
				return this.itemPacks[(int)this.currentSelected];
			}
		}

		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x060076C3 RID: 30403 RVA: 0x0032452A File Offset: 0x0032292A
		private MenuUIBehaviour[] MenuUIList
		{
			get
			{
				return this.GetCache(ref this._menuUIList, () => (from p in new MenuUIBehaviour[]
				{
					this,
					this._itemSendPanel
				}.Concat(this.itemPacks.SelectMany((ItemBoxUI.ItemBoxDataPack p) => new MenuUIBehaviour[]
				{
					p.categoryUI,
					p.itemListUI,
					p.itemSortUI
				}))
				where p != null
				select p).ToArray<MenuUIBehaviour>());
			}
		}

		// Token: 0x060076C4 RID: 30404 RVA: 0x00324544 File Offset: 0x00322944
		private void ViewerCursorOFF()
		{
			foreach (ItemBoxUI.ItemBoxDataPack itemBoxDataPack in this.itemPacks)
			{
				itemBoxDataPack.cursor.enabled = false;
			}
		}

		// Token: 0x060076C5 RID: 30405 RVA: 0x0032457C File Offset: 0x0032297C
		private void SortUIClose()
		{
			foreach (ItemBoxUI.ItemBoxDataPack itemBoxDataPack in this.itemPacks)
			{
				itemBoxDataPack.itemSortUI.Close();
			}
		}

		// Token: 0x060076C6 RID: 30406 RVA: 0x003245B4 File Offset: 0x003229B4
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

		// Token: 0x060076C7 RID: 30407 RVA: 0x00324688 File Offset: 0x00322A88
		private IEnumerator BindingUI()
		{
			using (var enumerator = this.itemPacks.Select((ItemBoxUI.ItemBoxDataPack p, int i) => new
			{
				p = p,
				sel = (ItemBoxUI.SelectedElement)i
			}).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					<>__AnonType21<ItemBoxUI.ItemBoxDataPack, ItemBoxUI.SelectedElement> item = enumerator.Current;
					item.p.ItemNodeOnDoubleClick += delegate(int id, ItemNodeUI option, ItemBoxUI.SelectedElement element)
					{
						this._selectedIndexOf = id;
						this.isSendAll = true;
						this.Send(option.Item.Count, element);
					};
					yield return base.StartCoroutine(item.p.Initialize(this, item.sel));
					item.p.itemListUI.CurrentChanged += delegate(int id, ItemNodeUI option)
					{
						if (option == null)
						{
							return;
						}
						this.currentSelected = item.p.sel;
						this.Select(item.p);
					};
				}
			}
			this._itemSendPanel.SendToInventory.Subscribe(delegate(int count)
			{
				this.playSE.Play(SoundPack.SystemSE.OK_S);
				this.SendToInventory(count);
			});
			this._itemSendPanel.SendToItemBox.Subscribe(delegate(int count)
			{
				this.playSE.Play(SoundPack.SystemSE.OK_S);
				this.SendToItemBox(count);
			});
			this._itemSendPanel.RemoveOnClick.Subscribe(delegate(int count)
			{
				this.playSE.Play(SoundPack.SystemSE.OK_S);
				ConfirmScene.Sentence = "削除してもよろしいですか？";
				ConfirmScene.BackAlpha = new float?(0f);
				ConfirmScene.Offset = new Vector2?(new Vector2(0f, -150f));
				ItemBoxUI.ItemBoxDataPack current = this.currentPack;
				ConfirmScene.OnClickedYes = delegate()
				{
					StuffItem item = current.itemListUI.GetNode(this._selectedIndexOf).Item;
					item.Count -= count;
					if (item.Count <= 0)
					{
						current.itemList.Remove(item);
						current.itemListUI.RemoveItemNode(this._selectedIndexOf);
						current.itemListUI.ForceSetNonSelect();
					}
					current.Refresh();
					this._itemSendPanel.Refresh();
					this.playSE.Play(SoundPack.SystemSE.OK_L);
				};
				ConfirmScene.OnClickedNo = delegate()
				{
					this.playSE.Play(SoundPack.SystemSE.Cancel);
				};
				Singleton<Game>.Instance.LoadDialog();
				this.SetFocusLevel(current.itemListUI.FocusLevel);
			});
			this._closeButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnInputCancel();
			});
			this._allSendButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.playSE.Play(SoundPack.SystemSE.OK_S);
				this.CategoryAllSend(this._inventoryUI, this._itemBoxUI);
			});
			ItemBoxUI.ItemBoxDataPack[] itemPacks = this.itemPacks;
			for (int j = 0; j < itemPacks.Length; j++)
			{
				ItemBoxUI.<BindingUI>c__Iterator1.<BindingUI>c__AnonStorey6 <BindingUI>c__AnonStorey2 = new ItemBoxUI.<BindingUI>c__Iterator1.<BindingUI>c__AnonStorey6();
				<BindingUI>c__AnonStorey2.item = itemPacks[j];
				<BindingUI>c__AnonStorey2.item.categoryUI.OnSubmit.AddListener(delegate()
				{
					if (<BindingUI>c__AnonStorey2.item.categoryUI.useCursor)
					{
						<BindingUI>c__AnonStorey2.item.categoryUI.SelectedButton.onClick.Invoke();
					}
					else if (<BindingUI>c__AnonStorey2.item.cursor.enabled)
					{
						if (<BindingUI>c__AnonStorey2.item.cursor.transform.position == <BindingUI>c__AnonStorey2.item.sorter.transform.position)
						{
							<BindingUI>c__AnonStorey2.item.sorter.isOn = !<BindingUI>c__AnonStorey2.item.sorter.isOn;
						}
						else if (<BindingUI>c__AnonStorey2.item.cursor.transform.position == <BindingUI>c__AnonStorey2.item.sortButton.transform.position)
						{
							<BindingUI>c__AnonStorey2.item.sortButton.onClick.Invoke();
						}
					}
				});
				<BindingUI>c__AnonStorey2.item.categoryUI.OnCancel.AddListener(delegate()
				{
					this.OnInputCancel();
				});
				<BindingUI>c__AnonStorey2.item.categoryUI.OnEntered = delegate()
				{
					this._focusElement.Value = <BindingUI>c__AnonStorey2.item.sel;
					this.ViewerCursorOFF();
					this.SetFocusLevel(<BindingUI>c__AnonStorey2.item.categoryUI.FocusLevel);
				};
				Observable.Merge<PointerEventData>(new IObservable<PointerEventData>[]
				{
					<BindingUI>c__AnonStorey2.item.categoryUI.leftButton.OnPointerEnterAsObservable(),
					<BindingUI>c__AnonStorey2.item.categoryUI.rightButton.OnPointerEnterAsObservable()
				}).Subscribe(delegate(PointerEventData _)
				{
					<BindingUI>c__AnonStorey2.item.categoryUI.OnEntered();
				}).AddTo(this);
				<BindingUI>c__AnonStorey2.item.itemListUI.OnCancel.AddListener(delegate()
				{
					this.OnInputCancel();
				});
				(from _ in <BindingUI>c__AnonStorey2.item.itemListUI.OnEntered
				where this.IsActiveControl
				select _).Subscribe(delegate(int _)
				{
					if (<BindingUI>c__AnonStorey2.item.itemListUI.ItemVisibles.Any<ItemNodeUI>())
					{
						this._focusElement.Value = <BindingUI>c__AnonStorey2.item.sel;
						this.ViewerCursorOFF();
						this.SetFocusLevel(<BindingUI>c__AnonStorey2.item.itemListUI.FocusLevel);
					}
				});
				<BindingUI>c__AnonStorey2.item.itemListUI.OnSubmit.AddListener(delegate()
				{
					if (this._focusElement.Value == <BindingUI>c__AnonStorey2.item.sel)
					{
						<BindingUI>c__AnonStorey2.item.itemListUI.CurrentID = <BindingUI>c__AnonStorey2.item.itemListUI.SelectedID;
					}
				});
				<BindingUI>c__AnonStorey2.item.sorter.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
				{
					this._focusElement.Value = <BindingUI>c__AnonStorey2.item.sel;
					this.SetCursorFocus(<BindingUI>c__AnonStorey2.item.cursor, <BindingUI>c__AnonStorey2.item.categoryUI, <BindingUI>c__AnonStorey2.item.sorter);
				}).AddTo(this);
				<BindingUI>c__AnonStorey2.item.sorter.OnPointerExitAsObservable().Subscribe(delegate(PointerEventData _)
				{
					<BindingUI>c__AnonStorey2.item.cursor.enabled = false;
				}).AddTo(this);
				ItemSortUI sortUI = <BindingUI>c__AnonStorey2.item.itemSortUI;
				sortUI.OnCancel.AddListener(delegate()
				{
					this.SetFocusLevel(<BindingUI>c__AnonStorey2.item.categoryUI.FocusLevel);
					<BindingUI>c__AnonStorey2.item.categoryUI.SelectedID = <BindingUI>c__AnonStorey2.item.categoryUI.CategoryID;
					<BindingUI>c__AnonStorey2.item.categoryUI.useCursor = true;
				});
				sortUI.OnEntered += delegate()
				{
					if (this.IsActiveControl && sortUI.isOpen)
					{
						this.ViewerCursorOFF();
						this._focusElement.Value = <BindingUI>c__AnonStorey2.item.sel;
						this.SetFocusLevel(sortUI.FocusLevel);
					}
				};
				<BindingUI>c__AnonStorey2.item.sortButton.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					if (sortUI.isOpen)
					{
						sortUI.Close();
						this.SetFocusLevel(<BindingUI>c__AnonStorey2.item.categoryUI.FocusLevel);
					}
					else
					{
						this.SortUIClose();
						sortUI.Open();
						this.SetFocusLevel(sortUI.FocusLevel);
						this.ViewerCursorOFF();
						this._itemSendPanel.Close();
						<BindingUI>c__AnonStorey2.item.itemListUI.ForceSetNonSelect();
					}
				});
				<BindingUI>c__AnonStorey2.item.sortButton.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
				{
					this._focusElement.Value = <BindingUI>c__AnonStorey2.item.sel;
					if (sortUI.isOpen)
					{
						this.SetFocusLevel(sortUI.FocusLevel);
					}
					else if (Singleton<Manager.Input>.Instance.FocusLevel != sortUI.FocusLevel)
					{
						this.SetFocusLevel(Singleton<Manager.Input>.Instance.FocusLevel);
					}
					else
					{
						this.SetFocusLevel(<BindingUI>c__AnonStorey2.item.categoryUI.FocusLevel);
					}
				}).AddTo(this);
			}
			yield break;
		}

		// Token: 0x060076C8 RID: 30408 RVA: 0x003246A4 File Offset: 0x00322AA4
		private void SetActiveControl(bool isActive)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (isActive)
			{
				MapUIContainer.SetVisibleHUD(false);
				Time.timeScale = 0f;
				Singleton<Map>.Instance.Player.SetScheduledInteractionState(false);
				Singleton<Map>.Instance.Player.ReleaseInteraction();
				instance.FocusLevel = 0;
				instance.MenuElements = this.MenuUIList;
				coroutine = this.DoOpen();
			}
			else
			{
				MapUIContainer.SetVisibleHUD(true);
				instance.ClearMenuElements();
				instance.FocusLevel = -1;
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

		// Token: 0x060076C9 RID: 30409 RVA: 0x003247A4 File Offset: 0x00322BA4
		private void Select(ItemBoxUI.ItemBoxDataPack itemPack)
		{
			ItemNodeUI selectedOption = itemPack.itemListUI.SelectedOption;
			if (selectedOption == null)
			{
				return;
			}
			if (!selectedOption.IsInteractable)
			{
				return;
			}
			this._selectedIndexOf = itemPack.itemListUI.CurrentID;
			if (selectedOption.Item == null)
			{
				return;
			}
			this._itemSendPanel.takeout = (itemPack.sel == ItemBoxUI.SelectedElement.ItemBox);
			foreach (ItemBoxUI.ItemBoxDataPack itemBoxDataPack in from p in this.itemPacks
			where p != itemPack
			select p)
			{
				itemBoxDataPack.itemListUI.ForceSetNonSelect();
			}
			this.SortUIClose();
			this._itemSendPanel.Open(selectedOption);
		}

		// Token: 0x060076CA RID: 30410 RVA: 0x003248A0 File Offset: 0x00322CA0
		private void Close()
		{
			Time.timeScale = 1f;
			this.IsActiveControl = false;
			this.playSE.Play(SoundPack.SystemSE.BoxClose);
		}

		// Token: 0x060076CB RID: 30411 RVA: 0x003248C0 File Offset: 0x00322CC0
		private IEnumerator DoOpen()
		{
			if (base.GetType() == typeof(ItemBoxUI))
			{
				yield return MapUIContainer.DrawOnceTutorialUI(11, null);
			}
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			ItemBoxUI.ItemBoxDataPack[] itemPacks = this.itemPacks;
			int i = 0;
			while (i < itemPacks.Length)
			{
				ItemBoxUI.ItemBoxDataPack item = itemPacks[i];
				ItemBoxUI.SelectedElement sel = item.sel;
				int slotMax;
				if (sel == ItemBoxUI.SelectedElement.Inventory)
				{
					while (Singleton<Map>.Instance.Player == null)
					{
						yield return null;
					}
					slotMax = Singleton<Map>.Instance.Player.PlayerData.InventorySlotMax;
					goto IL_1A2;
				}
				if (sel == ItemBoxUI.SelectedElement.ItemBox)
				{
					while (Singleton<Game>.Instance.Environment == null)
					{
						yield return null;
					}
					slotMax = Singleton<Manager.Resources>.Instance.DefinePack.ItemBoxCapacityDefines.StorageCapacity;
					goto IL_1A2;
				}
				IL_1B8:
				i++;
				continue;
				IL_1A2:
				item.slotCounter.y = slotMax;
				goto IL_1B8;
			}
			if (this.itemPacks.Any((ItemBoxUI.ItemBoxDataPack p) => !p.itemListUI.isOptionNode))
			{
				string bundle = Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
				GameObject gameObject = CommonLib.LoadAsset<GameObject>(bundle, "ItemOption_ItemBox", false, string.Empty);
				if (gameObject == null)
				{
					yield break;
				}
				if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundle))
				{
					MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundle, string.Empty));
				}
				foreach (ItemBoxUI.ItemBoxDataPack itemBoxDataPack in this.itemPacks)
				{
					itemBoxDataPack.itemListUI.SetOptionNode(gameObject);
				}
			}
			this._canvasGroup.blocksRaycasts = false;
			foreach (ItemBoxUI.ItemBoxDataPack item2 in this.itemPacks)
			{
				while (!item2.initialized)
				{
					yield return null;
				}
				item2.itemSortUI.SetDefault();
				item2.itemSortUI.Close();
				item2.ItemListNodeCreate();
				item2.Refresh();
			}
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return fadeInStream.ToYieldInstruction<TimeInterval<float>>();
			this._canvasGroup.blocksRaycasts = true;
			this._focusElement.Value = ItemBoxUI.SelectedElement.Inventory;
			this.currentSelected = this._focusElement.Value;
			this.SetFocusLevel(0);
			base.EnabledInput = true;
			this.playSE.use = true;
			yield break;
		}

		// Token: 0x060076CC RID: 30412 RVA: 0x003248DC File Offset: 0x00322CDC
		private IEnumerator DoClose()
		{
			base.EnabledInput = false;
			foreach (ItemBoxUI.ItemBoxDataPack itemBoxDataPack in this.itemPacks)
			{
				itemBoxDataPack.itemListUI.EnabledInput = false;
			}
			PlayerActor player = Singleton<Map>.Instance.Player;
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
			this._canvasGroup.blocksRaycasts = false;
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeOutStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return fadeOutStream.ToYieldInstruction<TimeInterval<float>>();
			this._itemSendPanel.Close();
			foreach (ItemBoxUI.ItemBoxDataPack itemBoxDataPack2 in this.itemPacks)
			{
				itemBoxDataPack2.cursor.enabled = false;
				itemBoxDataPack2.itemListUI.ClearItems();
				itemBoxDataPack2.itemSortUI.SetDefault();
				itemBoxDataPack2.itemSortUI.Close();
			}
			player.SetScheduledInteractionState(true);
			player.ReleaseInteraction();
			Singleton<Manager.Input>.Instance.SetupState();
			this.playSE.use = false;
			yield break;
		}

		// Token: 0x060076CD RID: 30413 RVA: 0x003248F7 File Offset: 0x00322CF7
		private void Send(int count, ItemBoxUI.SelectedElement element)
		{
			if (element != ItemBoxUI.SelectedElement.ItemBox)
			{
				if (element == ItemBoxUI.SelectedElement.Inventory)
				{
					this.SendToItemBox(count);
				}
			}
			else
			{
				this.SendToInventory(count);
			}
		}

		// Token: 0x060076CE RID: 30414 RVA: 0x00324923 File Offset: 0x00322D23
		private void SendToItemBox(int count)
		{
			this.Send(this._inventoryUI, this._itemBoxUI, count);
		}

		// Token: 0x060076CF RID: 30415 RVA: 0x00324938 File Offset: 0x00322D38
		private void SendToInventory(int count)
		{
			this.Send(this._itemBoxUI, this._inventoryUI, count);
		}

		// Token: 0x060076D0 RID: 30416 RVA: 0x00324950 File Offset: 0x00322D50
		private void Send(ItemBoxUI.ItemBoxDataPack sender, ItemBoxUI.ItemBoxDataPack receiver, int count)
		{
			ItemNodeUI node = sender.itemListUI.GetNode(this._selectedIndexOf);
			StuffItem item = new StuffItem(node.Item);
			int num;
			if (!receiver.itemList.CanAddItem(receiver.slotCounter.y, item, count, out num))
			{
				count = (this.isSendAll ? num : 0);
			}
			this.isSendAll = false;
			if (count <= 0)
			{
				return;
			}
			if (receiver.itemList.AddItem(item, count))
			{
				receiver.ItemListAddNode(receiver.itemListUI.SearchNotUsedIndex, item);
				receiver.ItemListNodeFilter(receiver.categoryUI.CategoryID, true);
			}
			node.Item.Count -= count;
			if (node.Item.Count <= 0)
			{
				count = Mathf.Abs(node.Item.Count);
				sender.itemList.Remove(node.Item);
				sender.itemListUI.RemoveItemNode(this._selectedIndexOf);
				List<StuffItem> list = new List<StuffItem>();
				while (count > 0)
				{
					StuffItem[] array = (from x in sender.itemList.FindItems(item)
					orderby x.Count
					select x).ToArray<StuffItem>();
					if (!array.Any<StuffItem>())
					{
						break;
					}
					foreach (StuffItem stuffItem in array)
					{
						int count2 = stuffItem.Count;
						stuffItem.Count -= count;
						count -= count2;
						if (stuffItem.Count <= 0)
						{
							list.Add(stuffItem);
						}
						if (count <= 0)
						{
							break;
						}
					}
				}
				foreach (StuffItem stuffItem2 in list)
				{
					sender.itemList.Remove(stuffItem2);
					foreach (KeyValuePair<int, ItemNodeUI> keyValuePair in sender.itemListUI.optionTable)
					{
						if (stuffItem2 == keyValuePair.Value.Item)
						{
							sender.itemListUI.RemoveItemNode(keyValuePair.Key);
							break;
						}
					}
				}
				this.SetFocusLevel(sender.itemListUI.FocusLevel);
				sender.itemListUI.ForceSetNonSelect();
			}
			receiver.Refresh();
			sender.Refresh();
			this._itemSendPanel.Refresh();
		}

		// Token: 0x060076D1 RID: 30417 RVA: 0x00324C08 File Offset: 0x00323008
		private void CategoryAllSend(ItemBoxUI.ItemBoxDataPack sender, ItemBoxUI.ItemBoxDataPack receiver)
		{
			List<ItemNodeUI> list = new List<ItemNodeUI>();
			foreach (ItemNodeUI itemNodeUI in sender.itemListUI.ItemVisibles)
			{
				StuffItem stuffItem = new StuffItem(itemNodeUI.Item);
				int num = stuffItem.Count;
				int num2;
				if (!receiver.itemList.CanAddItem(receiver.slotCounter.y, stuffItem, num, out num2))
				{
					num = num2;
				}
				if (num > 0)
				{
					if (receiver.itemList.AddItem(stuffItem, num))
					{
						receiver.ItemListAddNode(receiver.itemListUI.SearchNotUsedIndex, stuffItem);
						receiver.ItemListNodeFilter(receiver.categoryUI.CategoryID, true);
					}
					itemNodeUI.Item.Count -= num;
					if (itemNodeUI.Item.Count <= 0)
					{
						itemNodeUI.Item.Count = 0;
						list.Add(itemNodeUI);
					}
				}
			}
			foreach (ItemNodeUI itemNodeUI2 in list)
			{
				sender.itemList.Remove(itemNodeUI2.Item);
				sender.itemListUI.RemoveItemNode(sender.itemListUI.SearchIndex(itemNodeUI2));
			}
			this.SetFocusLevel(sender.itemListUI.FocusLevel);
			sender.itemListUI.ForceSetNonSelect();
			receiver.Refresh();
			sender.Refresh();
			this._itemSendPanel.Refresh();
		}

		// Token: 0x060076D2 RID: 30418 RVA: 0x00324DA0 File Offset: 0x003231A0
		private void OnUpdate()
		{
			if (this._canvasGroup.alpha == 0f)
			{
				return;
			}
			if (!this._itemSendPanel.isOpen)
			{
				if (!this.itemPacks.Any((ItemBoxUI.ItemBoxDataPack item) => item.itemSortUI.isOpen))
				{
					bool flag = false;
					if (Singleton<Manager.Input>.Instance.IsPressedAxis(ActionID.LeftShoulder2))
					{
						this._focusElement.Value = ItemBoxUI.SelectedElement.Inventory;
						flag = true;
					}
					else if (Singleton<Manager.Input>.Instance.IsPressedAxis(ActionID.RightShoulder2))
					{
						this._focusElement.Value = ItemBoxUI.SelectedElement.ItemBox;
						flag = true;
					}
					if (flag)
					{
						this.ViewerCursorOFF();
						ItemFilterCategoryUI categoryUI = this.currentPack.categoryUI;
						this.SetFocusLevel(categoryUI.FocusLevel);
						categoryUI.SelectedID = categoryUI.CategoryID;
						categoryUI.useCursor = true;
					}
				}
			}
		}

		// Token: 0x060076D3 RID: 30419 RVA: 0x00324E7C File Offset: 0x0032327C
		private void SetCursorFocus(Image cursor, ItemFilterCategoryUI categoryUI, Selectable selectable)
		{
			categoryUI.useCursor = false;
			CursorFrame.Set(cursor.rectTransform, selectable.GetComponent<RectTransform>(), null);
			foreach (ItemBoxUI.ItemBoxDataPack itemBoxDataPack in from p in this.itemPacks
			where p.cursor != cursor
			select p)
			{
				itemBoxDataPack.cursor.enabled = false;
			}
			cursor.enabled = true;
			this.SetFocusLevel(categoryUI.FocusLevel);
		}

		// Token: 0x060076D4 RID: 30420 RVA: 0x00324F30 File Offset: 0x00323330
		private new void SetFocusLevel(int level)
		{
			Singleton<Manager.Input>.Instance.FocusLevel = level;
			foreach (ItemBoxUI.ItemBoxDataPack itemBoxDataPack in this.itemPacks)
			{
				if (itemBoxDataPack.sel == this._focusElement.Value)
				{
					itemBoxDataPack.itemSortUI.EnabledInput = (level == itemBoxDataPack.itemSortUI.FocusLevel);
					itemBoxDataPack.itemListUI.EnabledInput = (level == itemBoxDataPack.itemListUI.FocusLevel);
					itemBoxDataPack.categoryUI.EnabledInput = (level == itemBoxDataPack.categoryUI.FocusLevel);
				}
				else
				{
					itemBoxDataPack.itemSortUI.EnabledInput = false;
					itemBoxDataPack.itemListUI.EnabledInput = false;
					itemBoxDataPack.categoryUI.EnabledInput = false;
				}
			}
			this._itemSendPanel.EnabledInput = (level == this._itemSendPanel.FocusLevel);
			this._itemSendPanel.cursor.enabled = this._itemSendPanel.EnabledInput;
		}

		// Token: 0x060076D5 RID: 30421 RVA: 0x00325025 File Offset: 0x00323425
		private void OnInputSubmit()
		{
		}

		// Token: 0x060076D6 RID: 30422 RVA: 0x00325027 File Offset: 0x00323427
		private void OnInputCancel()
		{
			this.Close();
		}

		// Token: 0x04006085 RID: 24709
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006086 RID: 24710
		[SerializeField]
		private ItemBoxUI.ItemBoxDataPack _inventoryUI;

		// Token: 0x04006087 RID: 24711
		[SerializeField]
		private ItemBoxUI.ItemBoxDataPack _itemBoxUI;

		// Token: 0x04006088 RID: 24712
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04006089 RID: 24713
		[SerializeField]
		private ItemSendPanelUI _itemSendPanel;

		// Token: 0x0400608A RID: 24714
		[SerializeField]
		private Button _allSendButton;

		// Token: 0x0400608D RID: 24717
		private ItemBoxUI.ItemBoxDataPack[] _itemPacks;

		// Token: 0x0400608E RID: 24718
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private ReactiveProperty<ItemBoxUI.SelectedElement> _focusElement = new ReactiveProperty<ItemBoxUI.SelectedElement>(ItemBoxUI.SelectedElement.Inventory);

		// Token: 0x0400608F RID: 24719
		private ItemBoxUI.SelectedElement currentSelected;

		// Token: 0x04006090 RID: 24720
		private IDisposable _fadeDisposable;

		// Token: 0x04006091 RID: 24721
		private MenuUIBehaviour[] _menuUIList;

		// Token: 0x02000E84 RID: 3716
		[Serializable]
		public class ItemBoxDataPack
		{
			// Token: 0x140000C6 RID: 198
			// (add) Token: 0x060076E4 RID: 30436 RVA: 0x00325130 File Offset: 0x00323530
			// (remove) Token: 0x060076E5 RID: 30437 RVA: 0x00325168 File Offset: 0x00323568
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			public event Action<int, ItemNodeUI, ItemBoxUI.SelectedElement> ItemNodeOnDoubleClick;

			// Token: 0x17001738 RID: 5944
			// (get) Token: 0x060076E6 RID: 30438 RVA: 0x0032519E File Offset: 0x0032359E
			public ItemFilterCategoryUI categoryUI
			{
				[CompilerGenerated]
				get
				{
					return this._viewer.categoryUI;
				}
			}

			// Token: 0x17001739 RID: 5945
			// (get) Token: 0x060076E7 RID: 30439 RVA: 0x003251AB File Offset: 0x003235AB
			public ItemListUI itemListUI
			{
				[CompilerGenerated]
				get
				{
					return this._viewer.itemListUI;
				}
			}

			// Token: 0x1700173A RID: 5946
			// (get) Token: 0x060076E8 RID: 30440 RVA: 0x003251B8 File Offset: 0x003235B8
			public ItemSortUI itemSortUI
			{
				[CompilerGenerated]
				get
				{
					return this._viewer.sortUI;
				}
			}

			// Token: 0x1700173B RID: 5947
			// (get) Token: 0x060076E9 RID: 30441 RVA: 0x003251C5 File Offset: 0x003235C5
			public Toggle sorter
			{
				[CompilerGenerated]
				get
				{
					return this._viewer.sorter;
				}
			}

			// Token: 0x1700173C RID: 5948
			// (get) Token: 0x060076EA RID: 30442 RVA: 0x003251D2 File Offset: 0x003235D2
			public Image cursor
			{
				[CompilerGenerated]
				get
				{
					return this._viewer.cursor;
				}
			}

			// Token: 0x1700173D RID: 5949
			// (get) Token: 0x060076EB RID: 30443 RVA: 0x003251DF File Offset: 0x003235DF
			public Button sortButton
			{
				[CompilerGenerated]
				get
				{
					return this._viewer.sortButton;
				}
			}

			// Token: 0x1700173E RID: 5950
			// (get) Token: 0x060076EC RID: 30444 RVA: 0x003251EC File Offset: 0x003235EC
			public ConditionalTextXtoYViewer slotCounter
			{
				[CompilerGenerated]
				get
				{
					return this._viewer.slotCounter;
				}
			}

			// Token: 0x1700173F RID: 5951
			// (get) Token: 0x060076ED RID: 30445 RVA: 0x003251F9 File Offset: 0x003235F9
			// (set) Token: 0x060076EE RID: 30446 RVA: 0x00325201 File Offset: 0x00323601
			public bool initialized { get; private set; }

			// Token: 0x17001740 RID: 5952
			// (get) Token: 0x060076EF RID: 30447 RVA: 0x0032520A File Offset: 0x0032360A
			// (set) Token: 0x060076F0 RID: 30448 RVA: 0x00325212 File Offset: 0x00323612
			public List<StuffItem> itemList { get; private set; }

			// Token: 0x17001741 RID: 5953
			// (get) Token: 0x060076F1 RID: 30449 RVA: 0x0032521B File Offset: 0x0032361B
			// (set) Token: 0x060076F2 RID: 30450 RVA: 0x00325223 File Offset: 0x00323623
			public ItemBoxUI.SelectedElement sel { get; private set; }

			// Token: 0x17001742 RID: 5954
			// (get) Token: 0x060076F3 RID: 30451 RVA: 0x0032522C File Offset: 0x0032362C
			public Image itemListPanel
			{
				[CompilerGenerated]
				get
				{
					return this._itemListPanel;
				}
			}

			// Token: 0x17001743 RID: 5955
			// (get) Token: 0x060076F4 RID: 30452 RVA: 0x00325234 File Offset: 0x00323634
			// (set) Token: 0x060076F5 RID: 30453 RVA: 0x0032523C File Offset: 0x0032363C
			private ItemBoxUI itemBoxUI { get; set; }

			// Token: 0x060076F6 RID: 30454 RVA: 0x00325248 File Offset: 0x00323648
			public IEnumerator Initialize(ItemBoxUI itemBoxUI, ItemBoxUI.SelectedElement sel)
			{
				this.itemBoxUI = itemBoxUI;
				this.sel = sel;
				if (this._viewer == null)
				{
					Transform parent = this._itemListPanel.transform;
					if (parent.childCount >= 1)
					{
						parent = parent.GetChild(0);
					}
					yield return InventoryViewer.Load(parent, delegate(InventoryViewer viewer)
					{
						this._viewer = viewer;
					});
				}
				this._viewer.ChangeTitleIcon(this._iconSprite);
				this._viewer.ChangeTitleText(this._iconText);
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
				this._viewer.sortUI.TypeChanged += delegate(int type)
				{
					this._viewer.SortType = type;
				};
				yield return itemBoxUI.SetStorage(this, delegate(List<StuffItem> x)
				{
					this.itemList = x;
				});
				yield return this._viewer.CategoryButtonAddEvent(delegate(int i)
				{
					this.ItemListNodeFilter(i, false);
				});
				this.initialized = true;
				yield break;
			}

			// Token: 0x060076F7 RID: 30455 RVA: 0x00325271 File Offset: 0x00323671
			public void Refresh()
			{
				this._viewer.itemListUI.Refresh();
				this._viewer.slotCounter.x = this.itemList.Count;
			}

			// Token: 0x060076F8 RID: 30456 RVA: 0x003252A0 File Offset: 0x003236A0
			public void ItemListNodeCreate()
			{
				int[] categorize;
				List<StuffItem> viewList;
				this.itemBoxUI.ViewCategorize(out categorize, out viewList, this.itemList);
				this._viewer.itemListUI.ClearItems();
				this._viewer.sortUI.SetDefault();
				this._viewer.sortUI.Close();
				this._viewer.sorter.isOn = true;
				this._viewer.categoryUI.Filter(categorize);
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
				int num = this._viewer.categoryUI.Visibles.FirstOrDefault<int>();
				this._viewer.categoryUI.SetSelectAndCategory(num);
				this.ItemListNodeFilter(num, true);
			}

			// Token: 0x060076F9 RID: 30457 RVA: 0x003253DC File Offset: 0x003237DC
			public ItemNodeUI ItemListAddNode(int index, StuffItem item)
			{
				ItemNodeUI node = this._viewer.itemListUI.AddItemNode(index, item);
				if (node != null)
				{
					node.OnClick.AsObservable().DoubleInterval(250f, false).Subscribe(delegate(IList<double> _)
					{
						if (this.ItemNodeOnDoubleClick != null)
						{
							this.ItemNodeOnDoubleClick(index, node, this.sel);
						}
					}).AddTo(node);
				}
				return node;
			}

			// Token: 0x060076FA RID: 30458 RVA: 0x00325469 File Offset: 0x00323869
			public void ItemListNodeFilter(int category, bool isSort)
			{
				this._viewer.itemListUI.Filter(category);
				this._viewer.itemListUI.ForceSetNonSelect();
				if (isSort)
				{
					this._viewer.SortItemList();
				}
			}

			// Token: 0x0400609C RID: 24732
			[SerializeField]
			private Image _itemListPanel;

			// Token: 0x0400609D RID: 24733
			[SerializeField]
			private Transform _sortUIPanel;

			// Token: 0x0400609E RID: 24734
			[SerializeField]
			private Sprite _iconSprite;

			// Token: 0x0400609F RID: 24735
			[SerializeField]
			private string _iconText;

			// Token: 0x040060A0 RID: 24736
			[SerializeField]
			private InventoryViewer _viewer;
		}

		// Token: 0x02000E85 RID: 3717
		public enum SelectedElement
		{
			// Token: 0x040060A3 RID: 24739
			Inventory,
			// Token: 0x040060A4 RID: 24740
			ItemBox
		}
	}
}
