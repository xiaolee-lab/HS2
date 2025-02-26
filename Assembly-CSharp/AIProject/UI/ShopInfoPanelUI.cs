using System;
using System.Collections;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using AIProject.UI.Viewer;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000EB0 RID: 3760
	public class ShopInfoPanelUI : MenuUIBehaviour
	{
		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x06007ABE RID: 31422 RVA: 0x0033A0DD File Offset: 0x003384DD
		public IObservable<Unit> Decide
		{
			[CompilerGenerated]
			get
			{
				return from _ in this._decideButton.OnClickAsObservable()
				where this._decide.Value
				select _;
			}
		}

		// Token: 0x17001820 RID: 6176
		// (get) Token: 0x06007ABF RID: 31423 RVA: 0x0033A0FB File Offset: 0x003384FB
		public IObservable<Unit> Return
		{
			[CompilerGenerated]
			get
			{
				return from _ in this._decideButton.OnClickAsObservable()
				where !this._decide.Value
				select _;
			}
		}

		// Token: 0x17001821 RID: 6177
		// (get) Token: 0x06007AC0 RID: 31424 RVA: 0x0033A119 File Offset: 0x00338519
		// (set) Token: 0x06007AC1 RID: 31425 RVA: 0x0033A126 File Offset: 0x00338526
		public ShopInfoPanelUI.Mode mode
		{
			get
			{
				return this._mode.Value;
			}
			set
			{
				this._mode.Value = value;
			}
		}

		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x06007AC2 RID: 31426 RVA: 0x0033A134 File Offset: 0x00338534
		// (set) Token: 0x06007AC3 RID: 31427 RVA: 0x0033A141 File Offset: 0x00338541
		public bool decide
		{
			get
			{
				return this._decide.Value;
			}
			set
			{
				this._decide.Value = value;
			}
		}

		// Token: 0x17001823 RID: 6179
		// (get) Token: 0x06007AC4 RID: 31428 RVA: 0x0033A14F File Offset: 0x0033854F
		public Image cursor
		{
			[CompilerGenerated]
			get
			{
				return this._cursor;
			}
		}

		// Token: 0x17001824 RID: 6180
		// (get) Token: 0x06007AC5 RID: 31429 RVA: 0x0033A157 File Offset: 0x00338557
		// (set) Token: 0x06007AC6 RID: 31430 RVA: 0x0033A15F File Offset: 0x0033855F
		public int BackFocusLevel { get; set; }

		// Token: 0x17001825 RID: 6181
		// (get) Token: 0x06007AC7 RID: 31431 RVA: 0x0033A168 File Offset: 0x00338568
		// (set) Token: 0x06007AC8 RID: 31432 RVA: 0x0033A170 File Offset: 0x00338570
		public Action OnClosed { get; set; }

		// Token: 0x17001826 RID: 6182
		// (get) Token: 0x06007AC9 RID: 31433 RVA: 0x0033A179 File Offset: 0x00338579
		public int Count
		{
			[CompilerGenerated]
			get
			{
				return this._countViewer.Count;
			}
		}

		// Token: 0x17001827 RID: 6183
		// (get) Token: 0x06007ACA RID: 31434 RVA: 0x0033A186 File Offset: 0x00338586
		public int MaxCount
		{
			[CompilerGenerated]
			get
			{
				return this._countViewer.MaxCount;
			}
		}

		// Token: 0x17001828 RID: 6184
		// (get) Token: 0x06007ACB RID: 31435 RVA: 0x0033A193 File Offset: 0x00338593
		// (set) Token: 0x06007ACC RID: 31436 RVA: 0x0033A19B File Offset: 0x0033859B
		public UnityEvent OnCancel { get; private set; } = new UnityEvent();

		// Token: 0x17001829 RID: 6185
		// (get) Token: 0x06007ACD RID: 31437 RVA: 0x0033A1A4 File Offset: 0x003385A4
		public bool isOpen
		{
			[CompilerGenerated]
			get
			{
				return this.IsActiveControl;
			}
		}

		// Token: 0x06007ACE RID: 31438 RVA: 0x0033A1AC File Offset: 0x003385AC
		public void Open(ItemNodeUI node)
		{
			this.selectionItem.Value = node;
		}

		// Token: 0x06007ACF RID: 31439 RVA: 0x0033A1BA File Offset: 0x003385BA
		public void Close()
		{
			this.selectionItem.SetValueAndForceNotify(null);
		}

		// Token: 0x06007AD0 RID: 31440 RVA: 0x0033A1C8 File Offset: 0x003385C8
		public void Refresh()
		{
			if (this.selectionItem.Value == null)
			{
				return;
			}
			this.Refresh(this.selectionItem.Value.Item);
		}

		// Token: 0x06007AD1 RID: 31441 RVA: 0x0033A1F8 File Offset: 0x003385F8
		public void Refresh(StuffItem item)
		{
			StuffItemInfo item2 = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(item.CategoryID, item.ID);
			bool interactable = true;
			if (this.mode == ShopInfoPanelUI.Mode.Inventory && !item2.isTrash)
			{
				interactable = false;
			}
			this._decideButton.interactable = interactable;
			this._itemName.text = item2.Name;
			this._flavorText.text = item2.Explanation;
			this._countViewer.MaxCount = Mathf.Max(item.Count, 1);
			this._countViewer.ForceCount = Mathf.Clamp(this._countViewer.Count, 1, this._countViewer.MaxCount);
		}

		// Token: 0x06007AD2 RID: 31442 RVA: 0x0033A2A8 File Offset: 0x003386A8
		private IEnumerator LoadCountViewer()
		{
			yield return CountViewer.Load(this._countViewerLayout, delegate(CountViewer viewer)
			{
				this._countViewer = viewer;
			});
			yield break;
		}

		// Token: 0x06007AD3 RID: 31443 RVA: 0x0033A2C4 File Offset: 0x003386C4
		protected override void Start()
		{
			if (this._countViewer == null)
			{
				base.StartCoroutine(this.LoadCountViewer());
			}
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			this.selectionItem.Subscribe(delegate(ItemNodeUI node)
			{
				this._hasItemNode = (node != null);
				if (this._hasItemNode)
				{
					this.IsActiveControl = true;
					this.Refresh(node.Item);
				}
				else if (this.isOpen)
				{
					this.IsActiveControl = false;
				}
			});
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
			this._mode.Subscribe(delegate(ShopInfoPanelUI.Mode mode)
			{
				this._backGround.sprite = this._backGrounds.GetElement((int)mode);
			});
			(from isOn in this._decide
			select (!isOn) ? 1 : 0).Subscribe(delegate(int index)
			{
				this._decideButton.image.sprite = this._sprites.GetElement(index);
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
			base.Start();
		}

		// Token: 0x06007AD4 RID: 31444 RVA: 0x0033A410 File Offset: 0x00338810
		private void OnUpdate()
		{
			this.cursor.enabled = (Singleton<Manager.Input>.Instance.FocusLevel == base.FocusLevel && base.EnabledInput);
			if (this._hasItemNode)
			{
				if (this.selectionItem.Value == null)
				{
					this.selectionItem.SetValueAndForceNotify(null);
				}
				else if (!this.selectionItem.Value.Visible || this.selectionItem.Value.Item.Count == 0)
				{
					this.selectionItem.Value = null;
				}
			}
		}

		// Token: 0x06007AD5 RID: 31445 RVA: 0x0033A4B3 File Offset: 0x003388B3
		private void OnDestroy()
		{
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = null;
		}

		// Token: 0x06007AD6 RID: 31446 RVA: 0x0033A4D4 File Offset: 0x003388D4
		private void SetActiveControl(bool isActive)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (isActive)
			{
				coroutine = this.DoOpen();
			}
			else
			{
				coroutine = this.DoClose();
			}
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x06007AD7 RID: 31447 RVA: 0x0033A548 File Offset: 0x00338948
		private IEnumerator DoOpen()
		{
			if (this._canvasGroup.blocksRaycasts)
			{
				this._canvasGroup.blocksRaycasts = false;
			}
			IObservable<TimeInterval<float>> lerpFadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true);
			IConnectableObservable<TimeInterval<float>> lerpConnStream = lerpFadeInStream.Publish<TimeInterval<float>>();
			float startAlpha = this._canvasGroup.alpha;
			lerpConnStream.Connect();
			lerpConnStream.Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return lerpConnStream.ToYieldInstruction<TimeInterval<float>>();
			this._canvasGroup.blocksRaycasts = true;
			base.EnabledInput = true;
			yield break;
		}

		// Token: 0x06007AD8 RID: 31448 RVA: 0x0033A564 File Offset: 0x00338964
		private IEnumerator DoClose()
		{
			base.EnabledInput = false;
			this._canvasGroup.blocksRaycasts = false;
			IObservable<TimeInterval<float>> lerpFadeOutStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true);
			IConnectableObservable<TimeInterval<float>> lerpConnStream = lerpFadeOutStream.Publish<TimeInterval<float>>();
			float startAlpha = this._canvasGroup.alpha;
			lerpConnStream.Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			lerpConnStream.Connect();
			yield return lerpConnStream.ToYieldInstruction<TimeInterval<float>>();
			yield break;
		}

		// Token: 0x06007AD9 RID: 31449 RVA: 0x0033A57F File Offset: 0x0033897F
		private void OnInputSubmit()
		{
			if (!this.isOpen)
			{
				return;
			}
			this._decideButton.onClick.Invoke();
		}

		// Token: 0x06007ADA RID: 31450 RVA: 0x0033A59D File Offset: 0x0033899D
		private void OnInputCancel()
		{
			UnityEvent onCancel = this.OnCancel;
			if (onCancel != null)
			{
				onCancel.Invoke();
			}
		}

		// Token: 0x04006282 RID: 25218
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006283 RID: 25219
		[SerializeField]
		private Image _backGround;

		// Token: 0x04006284 RID: 25220
		[SerializeField]
		private Sprite[] _backGrounds = new Sprite[2];

		// Token: 0x04006285 RID: 25221
		[SerializeField]
		private Text _itemName;

		// Token: 0x04006286 RID: 25222
		[SerializeField]
		private Text _flavorText;

		// Token: 0x04006287 RID: 25223
		[SerializeField]
		private Button _decideButton;

		// Token: 0x04006288 RID: 25224
		[SerializeField]
		private Sprite[] _sprites = new Sprite[2];

		// Token: 0x04006289 RID: 25225
		private ReactiveProperty<ShopInfoPanelUI.Mode> _mode = new ReactiveProperty<ShopInfoPanelUI.Mode>(ShopInfoPanelUI.Mode.Shop);

		// Token: 0x0400628A RID: 25226
		private BoolReactiveProperty _decide = new BoolReactiveProperty(false);

		// Token: 0x0400628B RID: 25227
		[SerializeField]
		private Image _cursor;

		// Token: 0x0400628C RID: 25228
		[SerializeField]
		private RectTransform _countViewerLayout;

		// Token: 0x0400628D RID: 25229
		[SerializeField]
		private CountViewer _countViewer;

		// Token: 0x04006291 RID: 25233
		private bool _hasItemNode;

		// Token: 0x04006292 RID: 25234
		private ReactiveProperty<ItemNodeUI> selectionItem = new ReactiveProperty<ItemNodeUI>();

		// Token: 0x04006293 RID: 25235
		private IDisposable _fadeDisposable;

		// Token: 0x02000EB1 RID: 3761
		public enum Mode
		{
			// Token: 0x04006296 RID: 25238
			Shop,
			// Token: 0x04006297 RID: 25239
			Inventory
		}
	}
}
