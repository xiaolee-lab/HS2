using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.ColorDefine;
using AIProject.Definitions;
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
	// Token: 0x02000EAF RID: 3759
	public class ScroungeUI : MenuUIBehaviour
	{
		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x06007A97 RID: 31383 RVA: 0x003382A5 File Offset: 0x003366A5
		public PlaySE playSE { get; } = new PlaySE();

		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x06007A98 RID: 31384 RVA: 0x003382AD File Offset: 0x003366AD
		// (set) Token: 0x06007A99 RID: 31385 RVA: 0x003382BA File Offset: 0x003366BA
		public AgentActor agent
		{
			get
			{
				return this._scroungeRequestViewer.agent;
			}
			set
			{
				this._scroungeRequestViewer.agent = value;
			}
		}

		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x06007A9A RID: 31386 RVA: 0x003382C8 File Offset: 0x003366C8
		// (set) Token: 0x06007A9B RID: 31387 RVA: 0x003382D0 File Offset: 0x003366D0
		public System.Action OnClose { get; set; }

		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x06007A9C RID: 31388 RVA: 0x003382D9 File Offset: 0x003366D9
		private MenuUIBehaviour[] MenuUIList
		{
			[CompilerGenerated]
			get
			{
				return this.GetCache(ref this._menuUIList, () => new MenuUIBehaviour[]
				{
					this,
					this._shopInfoPanelUI,
					this._inventoryUI.categoryUI,
					this._inventoryUI.itemListUI,
					this._inventoryUI.itemSortUI
				}.Concat(from p in this.controllers
				select p.itemListUI).ToArray<MenuUIBehaviour>());
			}
		}

		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x06007A9D RID: 31389 RVA: 0x003382F3 File Offset: 0x003366F3
		private ShopViewer.ItemListController[] controllers
		{
			[CompilerGenerated]
			get
			{
				return this.GetCache(ref this._controllers, () => new ShopViewer.ItemListController[]
				{
					this._inventoryUI.controller,
					this._scroungeRequestViewer.controller,
					this._shopSendViewer.controller
				});
			}
		}

		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x06007A9E RID: 31390 RVA: 0x0033830D File Offset: 0x0033670D
		// (set) Token: 0x06007A9F RID: 31391 RVA: 0x00338315 File Offset: 0x00336715
		private bool initialized { get; set; }

		// Token: 0x06007AA0 RID: 31392 RVA: 0x00338320 File Offset: 0x00336720
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

		// Token: 0x06007AA1 RID: 31393 RVA: 0x003383F4 File Offset: 0x003367F4
		private IEnumerator BindingUI()
		{
			yield return base.StartCoroutine(this._inventoryUI.Initialize());
			foreach (ShopViewer.ItemListController itemListController in from p in this.controllers
			where !p.itemListUI.isOptionNode
			select p)
			{
				string text = "shop_";
				if (itemListController == this._scroungeRequestViewer.controller || itemListController == this._shopSendViewer.controller)
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
			this._shopInfoPanelUI.mode = ShopInfoPanelUI.Mode.Inventory;
			Action<ShopViewer.ItemListController> nonSelect = delegate(ShopViewer.ItemListController controller)
			{
				foreach (ShopViewer.ItemListController itemListController3 in from p in this.controllers
				where p != controller
				select p)
				{
					itemListController3.itemListUI.ForceSetNonSelect();
				}
			};
			int sel = -1;
			ShopViewer.ItemListController sender = null;
			ShopViewer.ItemListController receiver = null;
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
			this._closeButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnInputCancel();
			});
			this._sendButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.Send();
			});
			Text tradeText = this._sendButton.GetComponentInChildren<Text>();
			Color baseTextColor = tradeText.color;
			(from _ in this._sendButton.OnPointerEnterAsObservable()
			where this._sendButton.interactable
			select _).Subscribe(delegate(PointerEventData _)
			{
				tradeText.color = Define.Get(Colors.Orange);
			});
			this._sendButton.OnPointerExitAsObservable().Subscribe(delegate(PointerEventData _)
			{
				tradeText.color = baseTextColor;
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
			(from _ in this._scroungeRequestViewer.itemListUI.OnEntered
			where this.IsActiveControl
			select _).Subscribe(delegate(int _)
			{
				this.SetFocusLevel(this._scroungeRequestViewer.itemListUI.FocusLevel);
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
				this._scroungeRequestViewer.itemListUI.OnCancel,
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

		// Token: 0x06007AA2 RID: 31394 RVA: 0x00338410 File Offset: 0x00336810
		private void ItemDecideProc(int count, int sel, ShopViewer.ItemListController sender, ShopViewer.ItemListController receiver)
		{
			ItemNodeUI node = sender.itemListUI.GetNode(sel);
			StuffItem stuffItem = new StuffItem(node.Item);
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
			this.SendCheck();
		}

		// Token: 0x06007AA3 RID: 31395 RVA: 0x003384F0 File Offset: 0x003368F0
		private void ItemReturnProc(int count, int sel, ShopViewer.ItemListController sender, ShopViewer.ItemListController receiver)
		{
			ItemNodeUI node = sender.itemListUI.GetNode(sel);
			StuffItem stuffItem = new StuffItem(node.Item);
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
			this.SendCheck();
		}

		// Token: 0x06007AA4 RID: 31396 RVA: 0x00338630 File Offset: 0x00336A30
		private void SendCheck()
		{
			StuffItem[] itemList = (from x in this._shopSendViewer.itemListUI.optionTable
			select x.Value.Item).ToArray<StuffItem>();
			bool active = this._scroungeRequestViewer.Check(itemList);
			this._sendButton.gameObject.SetActive(active);
		}

		// Token: 0x06007AA5 RID: 31397 RVA: 0x00338694 File Offset: 0x00336A94
		private void Send()
		{
			this.playSE.Play(SoundPack.SystemSE.OK_S);
			var lookup = this._inventoryUI.itemList.Select(delegate(StuffItem item)
			{
				int id2 = -1;
				foreach (KeyValuePair<int, ItemNodeUI> keyValuePair in this._inventoryUI.itemListUI.optionTable)
				{
					if (keyValuePair.Value.Item == item)
					{
						id2 = keyValuePair.Key;
						break;
					}
				}
				return new
				{
					item = item,
					nameHash = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(item.CategoryID, item.ID).nameHash,
					id = id2
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
			this._scroungeRequestViewer.controller.Clear();
			this._shopSendViewer.controller.Clear();
			this._inventoryUI.Refresh();
			this._scroungeRequestViewer.itemScrounge.Reset();
			AgentActor agent = this.agent;
			FlavorSkill.Type[] source = new FlavorSkill.Type[]
			{
				FlavorSkill.Type.Reliability,
				FlavorSkill.Type.Sociability,
				FlavorSkill.Type.Reason
			};
			int num2 = (int)source.Shuffle<FlavorSkill.Type>().First<FlavorSkill.Type>();
			agent.AgentData.SetFlavorSkill(num2, agent.ChaControl.fileGameInfo.flavorState[num2] + 20);
			FlavorSkill.Type[] source2 = new FlavorSkill.Type[]
			{
				FlavorSkill.Type.Darkness,
				FlavorSkill.Type.Wariness,
				FlavorSkill.Type.Instinct
			};
			int num3 = (int)source2.Shuffle<FlavorSkill.Type>().First<FlavorSkill.Type>();
			agent.AgentData.SetFlavorSkill(num3, agent.ChaControl.fileGameInfo.flavorState[num3] - 20);
			int num4 = 1;
			agent.SetStatus(num4, agent.AgentData.StatsTable[num4] + 30f);
			int id = 7;
			agent.SetStatus(id, (float)(agent.ChaControl.fileGameInfo.morality + 5));
			int num5 = 6;
			agent.SetStatus(num5, agent.AgentData.StatsTable[num5] + 30f);
			this.OnInputCancel();
		}

		// Token: 0x06007AA6 RID: 31398 RVA: 0x00338A34 File Offset: 0x00336E34
		private void Reverse()
		{
			this._scroungeRequestViewer.controller.Clear();
			if (this._scroungeRequestViewer.itemScrounge.isEvent)
			{
				foreach (ItemNodeUI itemNodeUI in this._shopSendViewer.itemListUI)
				{
					StuffItem item = itemNodeUI.Item;
					if (this._inventoryUI.itemList.AddItem(item))
					{
						this._inventoryUI.ItemListAddNode(this._inventoryUI.itemListUI.SearchNotUsedIndex, item);
						this._inventoryUI.ItemListNodeFilter(this._inventoryUI.categoryUI.CategoryID, true);
					}
				}
			}
			this._shopSendViewer.controller.Clear();
		}

		// Token: 0x06007AA7 RID: 31399 RVA: 0x00338B18 File Offset: 0x00336F18
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

		// Token: 0x06007AA8 RID: 31400 RVA: 0x00338BE3 File Offset: 0x00336FE3
		private void Close()
		{
			Time.timeScale = 1f;
			this.IsActiveControl = false;
			System.Action onClose = this.OnClose;
			if (onClose != null)
			{
				onClose();
			}
			this.playSE.Play(SoundPack.SystemSE.Cancel);
		}

		// Token: 0x06007AA9 RID: 31401 RVA: 0x00338C18 File Offset: 0x00337018
		private IEnumerator DoOpen()
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			CanvasGroup canvasGroup = this._inventoryUI.canvasGroup;
			canvasGroup.blocksRaycasts = false;
			while (!this.initialized)
			{
				yield return null;
			}
			ShopUI.InventoryUI item2 = this._inventoryUI;
			while (!item2.initialized)
			{
				yield return null;
			}
			item2.slotCounter.y = Singleton<Manager.Map>.Instance.Player.PlayerData.InventorySlotMax;
			item2.itemSortUI.SetDefault();
			item2.itemSortUI.Close();
			item2.ItemListNodeCreate();
			item2.Refresh();
			this._sendButton.gameObject.SetActive(false);
			foreach (var <>__AnonType in this._scroungeRequestViewer.itemScrounge.ItemList.Select((StuffItem item, int index) => new
			{
				item,
				index
			}))
			{
				ItemNodeUI itemNodeUI = this._scroungeRequestViewer.itemListUI.AddItemNode(<>__AnonType.index, <>__AnonType.item);
				if (itemNodeUI != null)
				{
					itemNodeUI.Disabled();
				}
			}
			float startAlpha = canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return fadeInStream.ToYieldInstruction<TimeInterval<float>>();
			canvasGroup.blocksRaycasts = true;
			Singleton<Manager.Input>.Instance.FocusLevel = 0;
			base.EnabledInput = true;
			yield break;
		}

		// Token: 0x06007AAA RID: 31402 RVA: 0x00338C34 File Offset: 0x00337034
		private IEnumerator DoClose()
		{
			base.EnabledInput = false;
			foreach (ShopViewer.ItemListController itemListController in this.controllers)
			{
				itemListController.itemListUI.EnabledInput = false;
			}
			CanvasGroup canvasGroup = this._inventoryUI.canvasGroup;
			canvasGroup.blocksRaycasts = false;
			float startAlpha = canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeOutStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return fadeOutStream.ToYieldInstruction<TimeInterval<float>>();
			this._shopInfoPanelUI.Close();
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
			yield break;
		}

		// Token: 0x06007AAB RID: 31403 RVA: 0x00338C4F File Offset: 0x0033704F
		private new void SetFocusLevel(int level)
		{
			Singleton<Manager.Input>.Instance.FocusLevel = level;
		}

		// Token: 0x06007AAC RID: 31404 RVA: 0x00338C5C File Offset: 0x0033705C
		private void OnInputSubmit()
		{
		}

		// Token: 0x06007AAD RID: 31405 RVA: 0x00338C5E File Offset: 0x0033705E
		private void OnInputCancel()
		{
			this.Close();
		}

		// Token: 0x04006270 RID: 25200
		[Header("ScroungeUI Setting")]
		[SerializeField]
		private ShopUI.InventoryUI _inventoryUI;

		// Token: 0x04006271 RID: 25201
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04006272 RID: 25202
		[SerializeField]
		private ShopInfoPanelUI _shopInfoPanelUI;

		// Token: 0x04006273 RID: 25203
		[SerializeField]
		private ScroungeRequestViewer _scroungeRequestViewer;

		// Token: 0x04006274 RID: 25204
		[SerializeField]
		private ShopSendViewer _shopSendViewer;

		// Token: 0x04006275 RID: 25205
		[SerializeField]
		private Button _sendButton;

		// Token: 0x04006276 RID: 25206
		private IDisposable _fadeDisposable;

		// Token: 0x04006277 RID: 25207
		private MenuUIBehaviour[] _menuUIList;

		// Token: 0x04006278 RID: 25208
		private ShopViewer.ItemListController[] _controllers;
	}
}
