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
using UnityEngine;
using UnityEx;

namespace AIProject.UI
{
	// Token: 0x02000E7D RID: 3709
	public abstract class InventoryFilterUIController : MenuUIBehaviour
	{
		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x06007652 RID: 30290 RVA: 0x003219B2 File Offset: 0x0031FDB2
		public PlaySE playSE { get; } = new PlaySE(false);

		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x06007653 RID: 30291 RVA: 0x003219BA File Offset: 0x0031FDBA
		// (set) Token: 0x06007654 RID: 30292 RVA: 0x003219C2 File Offset: 0x0031FDC2
		public virtual Func<int> capacity { get; set; }

		// Token: 0x06007655 RID: 30293 RVA: 0x003219CB File Offset: 0x0031FDCB
		public void SetItemFilter(InventoryFacadeViewer.ItemFilter[] itemFilters)
		{
			this._inventoryUI.SetItemFilter(itemFilters);
		}

		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x06007656 RID: 30294 RVA: 0x003219D9 File Offset: 0x0031FDD9
		// (set) Token: 0x06007657 RID: 30295 RVA: 0x003219E1 File Offset: 0x0031FDE1
		public Func<List<StuffItem>> itemList { get; set; }

		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x06007658 RID: 30296 RVA: 0x003219EA File Offset: 0x0031FDEA
		// (set) Token: 0x06007659 RID: 30297 RVA: 0x003219F2 File Offset: 0x0031FDF2
		public Func<List<StuffItem>> itemList_System { get; set; }

		// Token: 0x0600765A RID: 30298 RVA: 0x003219FB File Offset: 0x0031FDFB
		public void CountViewerVisible(bool isVisible)
		{
			this._itemInfoUI.isCountViewerVisible = isVisible;
		}

		// Token: 0x0600765B RID: 30299 RVA: 0x00321A09 File Offset: 0x0031FE09
		public void EmptyTextAutoVisible(bool isVisible)
		{
			this._inventoryUI.viewer.isAutoEmptyText = isVisible;
		}

		// Token: 0x0600765C RID: 30300 RVA: 0x00321A1C File Offset: 0x0031FE1C
		public void DoubleClickAction(Action<InventoryFacadeViewer.DoubleClickData> action)
		{
			this._inventoryUI.ItemNodeOnDoubleClick = delegate(InventoryFacadeViewer.DoubleClickData x)
			{
				Action<InventoryFacadeViewer.DoubleClickData> action2 = action;
				if (action2 != null)
				{
					action2(x);
				}
				this._itemInfoUI.OnSubmitForce();
			};
		}

		// Token: 0x0600765D RID: 30301
		protected abstract void ItemInfoEvent();

		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x0600765E RID: 30302 RVA: 0x00321A54 File Offset: 0x0031FE54
		// (set) Token: 0x0600765F RID: 30303 RVA: 0x00321A5C File Offset: 0x0031FE5C
		public SystemMenuUI Observer { get; set; }

		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x06007660 RID: 30304 RVA: 0x00321A65 File Offset: 0x0031FE65
		// (set) Token: 0x06007661 RID: 30305 RVA: 0x00321A6D File Offset: 0x0031FE6D
		public Action OnClose { get; set; }

		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x06007662 RID: 30306 RVA: 0x00321A76 File Offset: 0x0031FE76
		protected ItemListUI itemListUI
		{
			[CompilerGenerated]
			get
			{
				return this._inventoryUI.viewer.itemListUI;
			}
		}

		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x06007663 RID: 30307 RVA: 0x00321A88 File Offset: 0x0031FE88
		protected MenuUIBehaviour[] MenuUIList
		{
			get
			{
				return this.GetCache(ref this._menuUIList, () => new MenuUIBehaviour[]
				{
					this,
					this._itemInfoUI
				}.Concat(this._inventoryUI.viewer.MenuUIList).ToArray<MenuUIBehaviour>());
			}
		}

		// Token: 0x06007664 RID: 30308 RVA: 0x00321AA4 File Offset: 0x0031FEA4
		private IEnumerator LoadViewer()
		{
			yield return this._inventoryUI.Initialize();
			this._inventoryUI.SetOwner(this);
			this.ItemInfoEvent();
			yield break;
		}

		// Token: 0x06007665 RID: 30309 RVA: 0x00321AC0 File Offset: 0x0031FEC0
		protected override void OnBeforeStart()
		{
			base.StartCoroutine(this.LoadViewer());
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
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
		}

		// Token: 0x06007666 RID: 30310 RVA: 0x00321B58 File Offset: 0x0031FF58
		protected void SelectItem(StuffItem info)
		{
			this._itemInfoUI.Open(info);
		}

		// Token: 0x06007667 RID: 30311 RVA: 0x00321B66 File Offset: 0x0031FF66
		protected new void SetFocusLevel(int level)
		{
			this._itemInfoUI.EnabledInput = (level == this._itemInfoUI.FocusLevel);
		}

		// Token: 0x06007668 RID: 30312 RVA: 0x00321B84 File Offset: 0x0031FF84
		private void SetActiveControl(bool isActive)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (isActive)
			{
				Time.timeScale = 0f;
				Singleton<Map>.Instance.Player.SetScheduledInteractionState(false);
				Singleton<Map>.Instance.Player.ReleaseInteraction();
				instance.FocusLevel = 0;
				instance.MenuElements = this.MenuUIList;
				coroutine = this.DoOpen();
			}
			else
			{
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

		// Token: 0x06007669 RID: 30313 RVA: 0x00321C78 File Offset: 0x00320078
		private void Close()
		{
			this._inventoryUI.viewer.cursor.enabled = false;
			this._inventoryUI.viewer.itemListUI.ClearItems();
			this._inventoryUI.viewer.sortUI.playSE.use = false;
			this._inventoryUI.viewer.sortUI.SetDefault();
			this._inventoryUI.viewer.sortUI.Close();
			this._inventoryUI.viewer.sortUI.playSE.use = true;
			this._itemInfoUI.Close();
			if (this._emptyTextUpdateDisposable != null)
			{
				this._emptyTextUpdateDisposable.Dispose();
			}
			this._emptyTextUpdateDisposable = null;
			this._inventoryUI.viewer.isAutoEmptyText = false;
			this._inventoryUI.viewer.emptyText.enabled = false;
			this._inventoryUI.ItemNodeOnDoubleClick = null;
			Action onClose = this.OnClose;
			if (onClose != null)
			{
				onClose();
			}
			this.playSE.Play(SoundPack.SystemSE.Cancel);
		}

		// Token: 0x0600766A RID: 30314 RVA: 0x00321D90 File Offset: 0x00320190
		private IEnumerator DoOpen()
		{
			if (this.Observer != null)
			{
				this.Observer.OnClose = delegate()
				{
					this.Close();
				};
			}
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			this._inventoryUI.viewer.slotCounter.y = this.capacity();
			if (!this.itemListUI.isOptionNode)
			{
				string bundle = Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
				GameObject gameObject = CommonLib.LoadAsset<GameObject>(bundle, "ItemOption_new", false, string.Empty);
				if (gameObject == null)
				{
					yield break;
				}
				if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundle))
				{
					MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundle, string.Empty));
				}
				this.itemListUI.SetOptionNode(gameObject);
			}
			this._canvasGroup.blocksRaycasts = false;
			while (!this._inventoryUI.viewer.categoryInitialized || !this._inventoryUI.viewer.sortUIInitialized)
			{
				yield return null;
			}
			this._inventoryUI.ItemListNodeCreate();
			if (this._emptyTextUpdateDisposable != null)
			{
				this._emptyTextUpdateDisposable.Dispose();
			}
			this._emptyTextUpdateDisposable = null;
			if (this._inventoryUI.viewer.isAutoEmptyText)
			{
				this._emptyTextUpdateDisposable = (from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
				where this.isActiveAndEnabled
				select _).Subscribe(delegate(long _)
				{
					this._inventoryUI.viewer.emptyText.enabled = !this._inventoryUI.itemListUI.optionTable.Any((KeyValuePair<int, ItemNodeUI> v) => v.Value.Visible);
				});
			}
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return fadeInStream.ToYieldInstruction<TimeInterval<float>>();
			this._canvasGroup.blocksRaycasts = true;
			base.EnabledInput = true;
			this._inventoryUI.viewer.SetFocusLevel(Singleton<Manager.Input>.Instance.FocusLevel);
			if (this.Observer != null)
			{
				this.Observer.ActiveCloseButton = true;
			}
			this.playSE.use = true;
			yield break;
		}

		// Token: 0x0600766B RID: 30315 RVA: 0x00321DAC File Offset: 0x003201AC
		private IEnumerator DoClose()
		{
			if (this.Observer != null)
			{
				this.Observer.ActiveCloseButton = false;
			}
			MenuUIBehaviour itemListUI = this.itemListUI;
			bool enabledInput = false;
			base.EnabledInput = enabledInput;
			itemListUI.EnabledInput = enabledInput;
			PlayerActor player = Singleton<Map>.Instance.Player;
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
			this._canvasGroup.blocksRaycasts = false;
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeOutStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return fadeOutStream.ToYieldInstruction<TimeInterval<float>>();
			player.SetScheduledInteractionState(true);
			player.ReleaseInteraction();
			Singleton<Manager.Input>.Instance.SetupState();
			this.playSE.use = false;
			yield break;
		}

		// Token: 0x0600766C RID: 30316 RVA: 0x00321DC7 File Offset: 0x003201C7
		private void OnInputSubmit()
		{
		}

		// Token: 0x0600766D RID: 30317 RVA: 0x00321DC9 File Offset: 0x003201C9
		private void OnInputCancel()
		{
			this.Close();
		}

		// Token: 0x04006063 RID: 24675
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006064 RID: 24676
		[SerializeField]
		protected ItemInfoUI _itemInfoUI;

		// Token: 0x04006065 RID: 24677
		[SerializeField]
		protected InventoryFilterUIController.InventoryUI _inventoryUI;

		// Token: 0x04006066 RID: 24678
		private MenuUIBehaviour[] _menuUIList;

		// Token: 0x04006067 RID: 24679
		private IDisposable _fadeDisposable;

		// Token: 0x04006068 RID: 24680
		private IDisposable _emptyTextUpdateDisposable;

		// Token: 0x02000E7E RID: 3710
		[Serializable]
		public class InventoryUI : InventoryFacadeViewer
		{
			// Token: 0x06007675 RID: 30325 RVA: 0x00322E3C File Offset: 0x0032123C
			public void SetOwner(InventoryFilterUIController owner)
			{
				this.owner = owner;
				base.viewer.SetParentMenuUI(owner);
				base.viewer.setFocusLevel = delegate(int level)
				{
					owner.SetFocusLevel(level);
				};
				base.categoryUI.OnCancel.AddListener(delegate()
				{
					owner.OnInputCancel();
				});
				base.itemListUI.OnCancel.AddListener(delegate()
				{
					owner.OnInputCancel();
				});
			}

			// Token: 0x17001714 RID: 5908
			// (get) Token: 0x06007676 RID: 30326 RVA: 0x00322EC2 File Offset: 0x003212C2
			// (set) Token: 0x06007677 RID: 30327 RVA: 0x00322ECA File Offset: 0x003212CA
			public InventoryFilterUIController owner { get; private set; }

			// Token: 0x06007678 RID: 30328 RVA: 0x00322ED4 File Offset: 0x003212D4
			public override void ItemListNodeCreate()
			{
				base.SetItemList(this.owner.itemList());
				Func<List<StuffItem>> itemList_System = this.owner.itemList_System;
				base.SetItemList_System(((itemList_System != null) ? itemList_System() : null) ?? new List<StuffItem>());
				base.ItemListNodeCreate();
				base.viewer.sortUI.SetDefault();
				base.viewer.sortUI.Close();
				base.viewer.sorter.isOn = true;
				base.Refresh();
			}

			// Token: 0x06007679 RID: 30329 RVA: 0x00322F60 File Offset: 0x00321360
			public override void ItemListNodeFilter(int category, bool isSort)
			{
				base.ItemListNodeFilter(category, isSort);
				this.owner._itemInfoUI.Close();
			}
		}
	}
}
