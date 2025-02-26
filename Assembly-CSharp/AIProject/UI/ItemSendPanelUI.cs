using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using AIProject.UI.Viewer;
using Manager;
using ReMotion;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E93 RID: 3731
	public class ItemSendPanelUI : MenuUIBehaviour
	{
		// Token: 0x1700178B RID: 6027
		// (get) Token: 0x060077DE RID: 30686 RVA: 0x0032991D File Offset: 0x00327D1D
		public IObservable<int> SendToInventory
		{
			[CompilerGenerated]
			get
			{
				return from _ in this._sendButton.OnClickAsObservable()
				where this._takeout.Value
				select this.Count;
			}
		}

		// Token: 0x1700178C RID: 6028
		// (get) Token: 0x060077DF RID: 30687 RVA: 0x0032994C File Offset: 0x00327D4C
		public IObservable<int> SendToItemBox
		{
			[CompilerGenerated]
			get
			{
				return from _ in this._sendButton.OnClickAsObservable()
				where !this._takeout.Value
				select this.Count;
			}
		}

		// Token: 0x1700178D RID: 6029
		// (get) Token: 0x060077E0 RID: 30688 RVA: 0x0032997B File Offset: 0x00327D7B
		public IObservable<int> RemoveOnClick
		{
			[CompilerGenerated]
			get
			{
				return from _ in this._removeButton.OnClickAsObservable()
				select this.Count;
			}
		}

		// Token: 0x1700178E RID: 6030
		// (get) Token: 0x060077E1 RID: 30689 RVA: 0x00329999 File Offset: 0x00327D99
		// (set) Token: 0x060077E2 RID: 30690 RVA: 0x003299A6 File Offset: 0x00327DA6
		public bool takeout
		{
			get
			{
				return this._takeout.Value;
			}
			set
			{
				this._takeout.Value = value;
			}
		}

		// Token: 0x1700178F RID: 6031
		// (get) Token: 0x060077E3 RID: 30691 RVA: 0x003299B4 File Offset: 0x00327DB4
		public Image cursor
		{
			[CompilerGenerated]
			get
			{
				return this._cursor;
			}
		}

		// Token: 0x17001790 RID: 6032
		// (get) Token: 0x060077E4 RID: 30692 RVA: 0x003299BC File Offset: 0x00327DBC
		// (set) Token: 0x060077E5 RID: 30693 RVA: 0x003299C4 File Offset: 0x00327DC4
		public int BackFocusLevel { get; set; }

		// Token: 0x17001791 RID: 6033
		// (get) Token: 0x060077E6 RID: 30694 RVA: 0x003299CD File Offset: 0x00327DCD
		public int Count
		{
			[CompilerGenerated]
			get
			{
				return this._countViewer.Count;
			}
		}

		// Token: 0x17001792 RID: 6034
		// (get) Token: 0x060077E7 RID: 30695 RVA: 0x003299DA File Offset: 0x00327DDA
		// (set) Token: 0x060077E8 RID: 30696 RVA: 0x003299E2 File Offset: 0x00327DE2
		public UnityEvent OnCancel { get; private set; } = new UnityEvent();

		// Token: 0x17001793 RID: 6035
		// (get) Token: 0x060077E9 RID: 30697 RVA: 0x003299EB File Offset: 0x00327DEB
		public bool isOpen
		{
			[CompilerGenerated]
			get
			{
				return this.IsActiveControl;
			}
		}

		// Token: 0x060077EA RID: 30698 RVA: 0x003299F3 File Offset: 0x00327DF3
		public void Open(ItemNodeUI node)
		{
			this.selectionItem.Value = node;
		}

		// Token: 0x060077EB RID: 30699 RVA: 0x00329A01 File Offset: 0x00327E01
		public void Close()
		{
			this.selectionItem.SetValueAndForceNotify(null);
		}

		// Token: 0x060077EC RID: 30700 RVA: 0x00329A0F File Offset: 0x00327E0F
		public void Refresh()
		{
			if (this.selectionItem.Value == null)
			{
				return;
			}
			this.Refresh(this.selectionItem.Value.Item);
		}

		// Token: 0x060077ED RID: 30701 RVA: 0x00329A40 File Offset: 0x00327E40
		public void Refresh(StuffItem item)
		{
			StuffItemInfo item2 = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(item.CategoryID, item.ID);
			this._itemName.text = item2.Name;
			this._flavorText.text = item2.Explanation;
			if (this._trashField != null)
			{
				this._trashField.SetActive(item2.isTrash);
			}
			this._countViewer.MaxCount = Mathf.Max(item.Count, 1);
			this._countViewer.ForceCount = Mathf.Clamp(this._countViewer.Count, 1, this._countViewer.MaxCount);
		}

		// Token: 0x060077EE RID: 30702 RVA: 0x00329AEC File Offset: 0x00327EEC
		private IEnumerator SlotBind()
		{
			while (Singleton<Map>.Instance.Player == null || Singleton<Game>.Instance.Environment == null)
			{
				yield return null;
			}
			int capacity = 0;
			IReadOnlyCollection<StuffItem> itemList = null;
			this._takeout.Subscribe(delegate(bool isOn)
			{
				if (isOn)
				{
					itemList = Singleton<Map>.Instance.Player.PlayerData.ItemList;
					capacity = Singleton<Map>.Instance.Player.PlayerData.InventorySlotMax;
				}
				else
				{
					ItemSendPanelUI.StorageType storageType = this._storageType;
					if (storageType != ItemSendPanelUI.StorageType.Storage)
					{
						if (storageType == ItemSendPanelUI.StorageType.Pantry)
						{
							itemList = Singleton<Game>.Instance.Environment.ItemListInPantry;
							capacity = Singleton<Manager.Resources>.Instance.DefinePack.ItemBoxCapacityDefines.PantryCapacity;
						}
					}
					else
					{
						itemList = Singleton<Game>.Instance.Environment.ItemListInStorage;
						capacity = Singleton<Manager.Resources>.Instance.DefinePack.ItemBoxCapacityDefines.StorageCapacity;
					}
				}
			});
			this._countViewer.Counter.Select(delegate(int count)
			{
				StuffItem item = (!(this.selectionItem.Value == null)) ? this.selectionItem.Value.Item : null;
				return itemList.CanAddItem(capacity, item, count);
			}).SubscribeToInteractable(this._sendButton);
			yield break;
		}

		// Token: 0x060077EF RID: 30703 RVA: 0x00329B08 File Offset: 0x00327F08
		private IEnumerator LoadCountViewer()
		{
			yield return CountViewer.Load(this._countViewerLayout, delegate(CountViewer viewer)
			{
				this._countViewer = viewer;
			});
			yield break;
		}

		// Token: 0x060077F0 RID: 30704 RVA: 0x00329B24 File Offset: 0x00327F24
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
			(from isOn in this._takeout
			select (!isOn) ? 0 : 1).Subscribe(delegate(int index)
			{
				this._sendButton.image.sprite = this._sprites.GetElement(index);
			});
			base.StartCoroutine(this.SlotBind());
			IObservable<int>[] array = new IObservable<int>[2];
			array[0] = from _ in this._sendButton.OnPointerEnterAsObservable()
			select 0;
			array[1] = from _ in this._removeButton.OnPointerEnterAsObservable()
			select 1;
			Observable.Merge<int>(array).Subscribe(delegate(int id)
			{
				this._selectID.Value = id;
			});
			(from rt in this._selectID.Select(delegate(int i)
			{
				if (i == 0)
				{
					return this._sendButton.GetComponent<RectTransform>();
				}
				if (i != 1)
				{
					return null;
				}
				return this._removeButton.GetComponent<RectTransform>();
			})
			where rt != null
			select rt).Subscribe(delegate(RectTransform rt)
			{
				CursorFrame.Set(this.cursor.rectTransform, rt, null);
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

		// Token: 0x060077F1 RID: 30705 RVA: 0x00329D30 File Offset: 0x00328130
		private void OnUpdate()
		{
			this.cursor.enabled = (Singleton<Manager.Input>.Instance.FocusLevel == base.FocusLevel && base.EnabledInput);
			if (this._hasItemNode)
			{
				if (this.selectionItem.Value == null)
				{
					this.selectionItem.SetValueAndForceNotify(null);
				}
				else if (!this.selectionItem.Value.Visible)
				{
					this.selectionItem.Value = null;
				}
			}
		}

		// Token: 0x060077F2 RID: 30706 RVA: 0x00329DB9 File Offset: 0x003281B9
		private void OnDestroy()
		{
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = null;
		}

		// Token: 0x060077F3 RID: 30707 RVA: 0x00329DDC File Offset: 0x003281DC
		private void SetActiveControl(bool isActive)
		{
			IEnumerator coroutine = (!isActive) ? this.DoClose() : this.DoOpen();
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x060077F4 RID: 30708 RVA: 0x00329E44 File Offset: 0x00328244
		private IEnumerator DoOpen()
		{
			this._canvasGroup.blocksRaycasts = false;
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return fadeInStream.ToYieldInstruction<TimeInterval<float>>();
			this._canvasGroup.blocksRaycasts = true;
			base.EnabledInput = true;
			yield break;
		}

		// Token: 0x060077F5 RID: 30709 RVA: 0x00329E60 File Offset: 0x00328260
		private IEnumerator DoClose()
		{
			base.EnabledInput = false;
			this._canvasGroup.blocksRaycasts = false;
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeOutStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return fadeOutStream.ToYieldInstruction<TimeInterval<float>>();
			yield break;
		}

		// Token: 0x060077F6 RID: 30710 RVA: 0x00329E7C File Offset: 0x0032827C
		private void OnInputSubmit()
		{
			if (!this.isOpen)
			{
				return;
			}
			int value = this._selectID.Value;
			if (value != 0)
			{
				if (value == 1)
				{
					this._removeButton.onClick.Invoke();
				}
			}
			else
			{
				this._sendButton.onClick.Invoke();
			}
		}

		// Token: 0x060077F7 RID: 30711 RVA: 0x00329EDD File Offset: 0x003282DD
		private void OnInputCancel()
		{
			if (!this.isOpen)
			{
				return;
			}
			this.Close();
			UnityEvent onCancel = this.OnCancel;
			if (onCancel != null)
			{
				onCancel.Invoke();
			}
		}

		// Token: 0x0400611F RID: 24863
		[SerializeField]
		private ItemSendPanelUI.StorageType _storageType;

		// Token: 0x04006120 RID: 24864
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006121 RID: 24865
		[SerializeField]
		private Text _itemName;

		// Token: 0x04006122 RID: 24866
		[SerializeField]
		private Text _flavorText;

		// Token: 0x04006123 RID: 24867
		[SerializeField]
		private Button _removeButton;

		// Token: 0x04006124 RID: 24868
		[SerializeField]
		private GameObject _trashField;

		// Token: 0x04006125 RID: 24869
		[SerializeField]
		private Button _sendButton;

		// Token: 0x04006126 RID: 24870
		[SerializeField]
		private Sprite[] _sprites = new Sprite[2];

		// Token: 0x04006127 RID: 24871
		private BoolReactiveProperty _takeout = new BoolReactiveProperty(false);

		// Token: 0x04006128 RID: 24872
		[SerializeField]
		private Image _cursor;

		// Token: 0x04006129 RID: 24873
		[SerializeField]
		private RectTransform _countViewerLayout;

		// Token: 0x0400612A RID: 24874
		[SerializeField]
		private CountViewer _countViewer;

		// Token: 0x0400612B RID: 24875
		private IntReactiveProperty _selectID = new IntReactiveProperty(0);

		// Token: 0x0400612E RID: 24878
		private bool _hasItemNode;

		// Token: 0x0400612F RID: 24879
		private ReactiveProperty<ItemNodeUI> selectionItem = new ReactiveProperty<ItemNodeUI>();

		// Token: 0x04006130 RID: 24880
		private IDisposable _fadeDisposable;

		// Token: 0x02000E94 RID: 3732
		private enum StorageType
		{
			// Token: 0x04006136 RID: 24886
			Storage,
			// Token: 0x04006137 RID: 24887
			Pantry
		}
	}
}
