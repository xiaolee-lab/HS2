using System;
using System.Collections;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI.Recycling
{
	// Token: 0x02000EA2 RID: 3746
	[RequireComponent(typeof(CanvasGroup))]
	[RequireComponent(typeof(RectTransform))]
	public class RecyclingInfoPanelUI : MenuUIBehaviour
	{
		// Token: 0x170017DC RID: 6108
		// (get) Token: 0x06007984 RID: 31108 RVA: 0x00332296 File Offset: 0x00330696
		// (set) Token: 0x06007985 RID: 31109 RVA: 0x003322BE File Offset: 0x003306BE
		public float CanvasAlpha
		{
			get
			{
				return (!(this._canvasGroup != null)) ? 0f : this._canvasGroup.alpha;
			}
			private set
			{
				if (this._canvasGroup != null)
				{
					this._canvasGroup.alpha = value;
				}
			}
		}

		// Token: 0x170017DD RID: 6109
		// (get) Token: 0x06007986 RID: 31110 RVA: 0x003322DD File Offset: 0x003306DD
		// (set) Token: 0x06007987 RID: 31111 RVA: 0x00332301 File Offset: 0x00330701
		public bool BlockRaycast
		{
			get
			{
				return this._canvasGroup != null && this._canvasGroup.blocksRaycasts;
			}
			private set
			{
				if (this._canvasGroup != null && this._canvasGroup.blocksRaycasts != value)
				{
					this._canvasGroup.blocksRaycasts = value;
				}
			}
		}

		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x06007988 RID: 31112 RVA: 0x00332331 File Offset: 0x00330731
		// (set) Token: 0x06007989 RID: 31113 RVA: 0x00332355 File Offset: 0x00330755
		public bool Interactable
		{
			get
			{
				return this._canvasGroup != null && this._canvasGroup.interactable;
			}
			private set
			{
				if (this._canvasGroup != null && this._canvasGroup.interactable != value)
				{
					this._canvasGroup.interactable = value;
				}
			}
		}

		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x0600798A RID: 31114 RVA: 0x00332385 File Offset: 0x00330785
		public bool IsActiveItemInfo
		{
			[CompilerGenerated]
			get
			{
				return this.ItemInfo.Item1 != null && this.ItemInfo.Item2 != null && this.ItemInfo.Item3 != null;
			}
		}

		// Token: 0x0600798B RID: 31115 RVA: 0x003323BB File Offset: 0x003307BB
		public UnityEx.ValueTuple<ItemListController, ItemListController, ItemNodeUI, int, ButtonType> GetItemInfo()
		{
			return this.ItemInfo;
		}

		// Token: 0x0600798C RID: 31116 RVA: 0x003323C3 File Offset: 0x003307C3
		public void SetItemInfo(ItemListController sender, ItemListController receiver, ItemNodeUI nodeUI, int currentID, ButtonType buttonType)
		{
			this.ItemInfo.Item1 = sender;
			this.ItemInfo.Item2 = receiver;
			this.ItemInfo.Item3 = nodeUI;
			this.ItemInfo.Item4 = currentID;
			this.ItemInfo.Item5 = buttonType;
		}

		// Token: 0x0600798D RID: 31117 RVA: 0x00332403 File Offset: 0x00330803
		public void ClearItemInfo()
		{
			this.ItemInfo.Item1 = null;
			this.ItemInfo.Item2 = null;
			this.ItemInfo.Item3 = null;
			this.ItemInfo.Item4 = -1;
			this.ItemInfo.Item5 = ButtonType.None;
		}

		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x0600798E RID: 31118 RVA: 0x00332441 File Offset: 0x00330841
		// (set) Token: 0x0600798F RID: 31119 RVA: 0x00332449 File Offset: 0x00330849
		public int ItemMaxCount { get; set; }

		// Token: 0x170017E1 RID: 6113
		// (get) Token: 0x06007990 RID: 31120 RVA: 0x00332452 File Offset: 0x00330852
		public IObservable<Unit> ClickDecide
		{
			[CompilerGenerated]
			get
			{
				Button element = this._buttons.GetElement(0);
				return (element != null) ? element.OnClickAsObservable() : null;
			}
		}

		// Token: 0x170017E2 RID: 6114
		// (get) Token: 0x06007991 RID: 31121 RVA: 0x0033246F File Offset: 0x0033086F
		public IObservable<Unit> ClickReturn
		{
			[CompilerGenerated]
			get
			{
				Button element = this._buttons.GetElement(1);
				return (element != null) ? element.OnClickAsObservable() : null;
			}
		}

		// Token: 0x170017E3 RID: 6115
		// (get) Token: 0x06007992 RID: 31122 RVA: 0x0033248C File Offset: 0x0033088C
		public IObservable<Unit> ClickDelete
		{
			[CompilerGenerated]
			get
			{
				Button element = this._buttons.GetElement(2);
				return (element != null) ? element.OnClickAsObservable() : null;
			}
		}

		// Token: 0x170017E4 RID: 6116
		// (get) Token: 0x06007993 RID: 31123 RVA: 0x003324AC File Offset: 0x003308AC
		// (set) Token: 0x06007994 RID: 31124 RVA: 0x003324F8 File Offset: 0x003308F8
		public int InputNumber
		{
			get
			{
				if (this._numInputField == null)
				{
					return 0;
				}
				string s = this._numInputField.text ?? string.Empty;
				int num;
				return (!int.TryParse(s, out num)) ? 0 : num;
			}
			private set
			{
				int num = Mathf.Clamp(value, 0, this.ItemMaxCount);
				this._numInputField.text = string.Format("{0}", num);
				this._numInputField.textComponent.text = string.Format("{0}", num);
			}
		}

		// Token: 0x06007995 RID: 31125 RVA: 0x00332550 File Offset: 0x00330950
		private void SetInputNumber(string numStr)
		{
			int num;
			if (!int.TryParse(numStr ?? string.Empty, out num))
			{
				return;
			}
			this._numInputField.text = string.Format("{0}", num);
			this._numInputField.textComponent.text = string.Format("{0}", num);
		}

		// Token: 0x170017E5 RID: 6117
		// (get) Token: 0x06007996 RID: 31126 RVA: 0x003325B4 File Offset: 0x003309B4
		public bool IsNumberInput
		{
			get
			{
				if (this._numInputField == null)
				{
					return false;
				}
				string s = this._numInputField.text ?? string.Empty;
				int num;
				return int.TryParse(s, out num);
			}
		}

		// Token: 0x06007997 RID: 31127 RVA: 0x003325F4 File Offset: 0x003309F4
		protected override void Awake()
		{
			base.Awake();
			this._numInputField.contentType = InputField.ContentType.IntegerNumber;
			if (this._recyclingUI == null)
			{
				this._recyclingUI = base.GetComponentInParent<RecyclingUI>();
			}
		}

		// Token: 0x06007998 RID: 31128 RVA: 0x00332628 File Offset: 0x00330A28
		protected override void OnBeforeStart()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			}).AddTo(this);
			this._numInputField.OnValueChangedAsObservable().Subscribe(delegate(string str)
			{
				this.OnInputFieldValueChanged(str);
			}).AddTo(this);
			for (int i = 0; i < this._numChangeValues.Length; i++)
			{
				Button element = this._numChangeButtons.GetElement(i);
				if (!(element == null))
				{
					int changeValue = this._numChangeValues[i];
					element.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						this.OnNumChangeClick(changeValue);
					}).AddTo(this);
					element.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						this._recyclingUI.PlaySE(SoundPack.SystemSE.OK_S);
					}).AddTo(this);
				}
			}
			if (!this._buttons.IsNullOrEmpty<Button>())
			{
				foreach (Button button in this._buttons)
				{
					if (!(button == null))
					{
						button.OnClickAsObservable().Subscribe(delegate(Unit _)
						{
							this._recyclingUI.PlaySE(SoundPack.SystemSE.OK_S);
						}).AddTo(this);
					}
				}
			}
			this.ClickDelete.Subscribe(delegate(Unit _)
			{
				this.OnClickDeleteButton();
			}).AddTo(this);
			(from _ in this._recyclingUI.OnInventoryChanged
			where this.ItemInfo.Item5 == ButtonType.Return
			where this.ItemInfo.Item2 != null
			select _).Subscribe(delegate(RecyclingInventoryFacadeViewer receiver)
			{
				this.ItemInfo.Item2 = receiver.ListController;
			}).AddTo(this);
			IObservable<Unit> observable = from _ in this._recyclingUI.OnInventoryChanged
			select Unit.Default;
			IObservable<Unit> observable2 = from _ in this._recyclingUI.DecidedItemSlotUI.DeleteEvent
			where this.ItemInfo.Item5 == ButtonType.Return
			where this.ItemInfo.Item3 != null
			select _ into item
			where item != null
			where this.ItemInfo.Item3.Item == item
			where 0 < item.Count
			select item into _
			select Unit.Default;
			IObservable<Unit> observable3 = from _ in this._recyclingUI.DecidedItemSlotUI.DeleteEvent
			where this.ItemInfo.Item5 == ButtonType.Decide
			where this.ItemInfo.Item3 != null
			select Unit.Default;
			IObservable<Unit> observable4 = (from _ in this._recyclingUI.OnDoubleClicked
			where this.ItemInfo.Item3 != null
			select Unit.Default).DelayFrame(1, FrameCountType.Update);
			IObservable<Unit> observable5 = from item in this._recyclingUI.CreateItemStockUI.OnAddCreateItem
			where item != null
			select item into _
			where this.ItemInfo.Item5 == ButtonType.Delete
			where this.ItemInfo.Item3 != null
			select Unit.Default;
			IObservable<Unit> observable6 = this._recyclingUI.CreateItemStockUI.OnClickDeleteSubmit.DelayFrame(1, FrameCountType.Update);
			(from _ in Observable.Merge<Unit>(new IObservable<Unit>[]
			{
				this.ClickDecide.DelayFrame(1, FrameCountType.Update),
				this.ClickReturn.DelayFrame(1, FrameCountType.Update),
				this.ClickDelete.DelayFrame(1, FrameCountType.Update),
				observable,
				observable3,
				observable2,
				observable4,
				observable5,
				observable6
			})
			where this.ItemInfo.Item3 != null
			select _).Subscribe(delegate(Unit _)
			{
				this.RefreshUI();
			}).AddTo(this);
			(from _ in this._recyclingUI.DecidedItemSlotUI.DeleteEvent
			where base.isActiveAndEnabled
			where this.IsActiveControl
			where this.ItemInfo.Item5 == ButtonType.Return
			select _ into item
			where item != null
			select item into _
			where this.ItemInfo.Item3 != null
			select _ into item
			where this.ItemInfo.Item3.Item == item
			where item.Count <= 0
			select item).Subscribe(delegate(StuffItem _)
			{
				this.DetachItem();
			}).AddTo(this);
		}

		// Token: 0x06007999 RID: 31129 RVA: 0x00332B1C File Offset: 0x00330F1C
		private void OnClickDeleteButton()
		{
			if (!base.isActiveAndEnabled || !this.IsNumberInput)
			{
				return;
			}
			RecyclingItemDeleteRequestUI deleteREquestUI = this._recyclingUI.DeleteREquestUI;
			deleteREquestUI.DoOpen();
		}

		// Token: 0x0600799A RID: 31130 RVA: 0x00332B54 File Offset: 0x00330F54
		private void OnInputFieldValueChanged(string str)
		{
			int num;
			if (!int.TryParse(str ?? string.Empty, out num))
			{
				this.RefreshInputNumberChangeUI();
				return;
			}
			if (this.ItemMaxCount <= 0)
			{
				this._numInputField.text = "0";
				this._numInputField.textComponent.text = "0";
			}
			else if (num < 1)
			{
				this._numInputField.text = "1";
				this._numInputField.textComponent.text = "1";
			}
			else if (this.ItemMaxCount < num)
			{
				this._numInputField.text = string.Format("{0}", this.ItemMaxCount);
				this._numInputField.textComponent.text = string.Format("{0}", this.ItemMaxCount);
			}
			this.RefreshInputNumberChangeUI();
		}

		// Token: 0x0600799B RID: 31131 RVA: 0x00332C40 File Offset: 0x00331040
		protected override void Start()
		{
			base.Start();
			this.CanvasAlpha = 0f;
			bool flag = false;
			this.Interactable = flag;
			this.BlockRaycast = flag;
		}

		// Token: 0x0600799C RID: 31132 RVA: 0x00332C70 File Offset: 0x00331070
		private void RefreshItemMaxCount()
		{
			StuffItem stuffItem = (!(this.ItemInfo.Item3 != null)) ? null : this.ItemInfo.Item3.Item;
			this.ItemMaxCount = ((stuffItem == null) ? 0 : stuffItem.Count);
		}

		// Token: 0x0600799D RID: 31133 RVA: 0x00332CC4 File Offset: 0x003310C4
		private void RefreshButtonInteractable()
		{
			int inputNumber = this.InputNumber;
			if (!this._buttons.IsNullOrEmpty<Button>())
			{
				foreach (Button obj in this._buttons)
				{
					this.SetInteractable(obj, 0 < inputNumber);
				}
			}
			this.RefreshInputNumberChangeUI();
		}

		// Token: 0x0600799E RID: 31134 RVA: 0x00332D18 File Offset: 0x00331118
		private void RefreshInputNumberChangeUI()
		{
			this.RefreshSendButtonInteractable();
			this.RefreshNumChangeButtonInteractable();
		}

		// Token: 0x0600799F RID: 31135 RVA: 0x00332D28 File Offset: 0x00331128
		private void RefreshSendButtonInteractable()
		{
			if (this._buttons.IsNullOrEmpty<Button>())
			{
				return;
			}
			if (!this.IsNumberInput)
			{
				this.SetInteractableSendButton(false);
				return;
			}
			StuffItem stuffItem = (!(this.ItemInfo.Item3 != null)) ? null : this.ItemInfo.Item3.Item;
			ItemListController item = this.ItemInfo.Item2;
			if (stuffItem == null || item == null)
			{
				this.SetInteractableSendButton(false);
				return;
			}
			int inputNumber = this.InputNumber;
			int num = item.PossibleCount(stuffItem);
			this.SetInteractableSendButton(inputNumber <= num);
		}

		// Token: 0x060079A0 RID: 31136 RVA: 0x00332DC4 File Offset: 0x003311C4
		private void SetInteractableSendButton(bool active)
		{
			if (this._buttons.IsNullOrEmpty<Button>())
			{
				return;
			}
			for (int i = 0; i < this._buttons.Length; i++)
			{
				Button element = this._buttons.GetElement(i);
				if (!(element == null))
				{
					bool flag = (i == 2) ? (0 < this.InputNumber) : active;
					this.SetInteractable(element, flag);
				}
			}
		}

		// Token: 0x060079A1 RID: 31137 RVA: 0x00332E38 File Offset: 0x00331238
		private void RefreshNumChangeButtonInteractable()
		{
			if (!this._numChangeButtons.IsNullOrEmpty<Button>())
			{
				if (0 < this.ItemMaxCount)
				{
					if (this.IsNumberInput)
					{
						int inputNumber = this.InputNumber;
						for (int i = 0; i < this._numChangeValues.Length; i++)
						{
							Button element = this._numChangeButtons.GetElement(i);
							if (!(element == null))
							{
								int num = this._numChangeValues[i];
								bool flag = (num >= 0) ? (0 < num && inputNumber < this.ItemMaxCount) : (1 < inputNumber);
								this.SetInteractable(element, flag);
							}
						}
					}
					else
					{
						foreach (Button obj in this._numChangeButtons)
						{
							this.SetInteractable(obj, true);
						}
					}
				}
				else
				{
					foreach (Button obj2 in this._numChangeButtons)
					{
						this.SetInteractable(obj2, false);
					}
				}
			}
		}

		// Token: 0x060079A2 RID: 31138 RVA: 0x00332F50 File Offset: 0x00331350
		private void OnNumChangeClick(int addValue)
		{
			int num = (!this.IsNumberInput) ? 1 : this.InputNumber;
			num += addValue;
			num = Mathf.Max(1, num);
			int b = (!(this.ItemInfo.Item3 != null) || this.ItemInfo.Item3.Item == null) ? 1 : this.ItemInfo.Item3.Item.Count;
			num = Mathf.Min(num, b);
			this._numInputField.text = string.Format("{0}", num);
			this._numInputField.textComponent.text = string.Format("{0}", num);
			this.RefreshInputNumberChangeUI();
		}

		// Token: 0x060079A3 RID: 31139 RVA: 0x00333014 File Offset: 0x00331414
		public void Refresh()
		{
			this.CanvasAlpha = 0f;
			bool flag = false;
			this.Interactable = flag;
			this.BlockRaycast = flag;
			this._itemNameText.text = string.Empty;
			this._flavorText.text = string.Empty;
			this.ClearItemInfo();
			this.RefreshUI();
		}

		// Token: 0x060079A4 RID: 31140 RVA: 0x00333068 File Offset: 0x00331468
		public void RefreshUI()
		{
			this.RefreshItemMaxCount();
			this.RefreshInputFieldUI();
			this.RefreshButtonInteractable();
		}

		// Token: 0x060079A5 RID: 31141 RVA: 0x0033307C File Offset: 0x0033147C
		private void RefreshInputFieldUI()
		{
			int num = Mathf.Max(1, this.InputNumber);
			num = Mathf.Min(num, this.ItemMaxCount);
			ItemNodeUI item = this.ItemInfo.Item3;
			StuffItem stuffItem = (!(item != null)) ? null : item.Item;
			int max = (stuffItem == null) ? 0 : stuffItem.Count;
			num = Mathf.Clamp(num, 0, max);
			this._numInputField.text = string.Format("{0}", num);
			this._numInputField.textComponent.text = string.Format("{0}", num);
		}

		// Token: 0x060079A6 RID: 31142 RVA: 0x00333120 File Offset: 0x00331520
		public void AttachDeleteItem(ItemListController sender, ItemNodeUI itemUI, int currentID)
		{
			this.ClearItemInfo();
			if (itemUI == null || itemUI.Item == null || itemUI.Item.Count <= 0)
			{
				this.IsActiveControl = false;
				return;
			}
			StuffItem item = itemUI.Item;
			StuffItemInfo item2 = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(item.CategoryID, item.ID);
			if (item2 == null)
			{
				this.IsActiveControl = false;
				return;
			}
			ButtonType buttonType = ButtonType.Delete;
			this.SetItemInfo(sender, null, itemUI, currentID, buttonType);
			this.InputNumber = 1;
			this.RefreshUI();
			this._itemNameText.text = item2.Name;
			this._flavorText.text = item2.Explanation;
			int num = (int)buttonType;
			for (int i = 0; i < this._buttons.Length; i++)
			{
				Button element = this._buttons.GetElement(i);
				if (!(element == null))
				{
					this.SetActive(element, i == num);
				}
			}
			this.IsActiveControl = true;
		}

		// Token: 0x060079A7 RID: 31143 RVA: 0x00333224 File Offset: 0x00331624
		public void AttachItem(ItemListController sender, ItemListController receiver, int currentID, ItemNodeUI itemUI, ButtonType buttonType)
		{
			this.ClearItemInfo();
			if (sender == null || receiver == null)
			{
				this.IsActiveControl = false;
				return;
			}
			if (!Singleton<Manager.Resources>.IsInstance() || itemUI == null || itemUI.Item == null || itemUI.Item.Count <= 0)
			{
				this.IsActiveControl = false;
				return;
			}
			StuffItem item = itemUI.Item;
			StuffItemInfo item2 = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(item.CategoryID, item.ID);
			if (item2 == null)
			{
				this.IsActiveControl = false;
				return;
			}
			this.SetItemInfo(sender, receiver, itemUI, currentID, buttonType);
			this.InputNumber = 1;
			this.RefreshUI();
			this._itemNameText.text = item2.Name;
			this._flavorText.text = item2.Explanation;
			for (int i = 0; i < this._buttons.Length; i++)
			{
				Button element = this._buttons.GetElement(i);
				if (!(element == null))
				{
					if (element.gameObject.activeSelf != (i == (int)buttonType))
					{
						element.gameObject.SetActive(i == (int)buttonType);
					}
				}
			}
			this.IsActiveControl = true;
		}

		// Token: 0x060079A8 RID: 31144 RVA: 0x0033335E File Offset: 0x0033175E
		public void DetachItem()
		{
			this.ClearItemInfo();
			this.ItemMaxCount = 0;
			this.IsActiveControl = false;
		}

		// Token: 0x060079A9 RID: 31145 RVA: 0x00333374 File Offset: 0x00331774
		private void SetActiveControl(bool active)
		{
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			IEnumerator coroutine = (!active) ? this.CloseCoroutine() : this.OpenCoroutine();
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>().AddTo(this);
		}

		// Token: 0x060079AA RID: 31146 RVA: 0x003333E0 File Offset: 0x003317E0
		private IEnumerator OpenCoroutine()
		{
			this.SetActive(base.gameObject, true);
			this.BlockRaycast = true;
			bool flag = false;
			this.Interactable = flag;
			base.EnabledInput = flag;
			float startAlpha = this.CanvasAlpha;
			yield return ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			}).ToYieldInstruction<TimeInterval<float>>();
			this.CanvasAlpha = 1f;
			flag = true;
			base.EnabledInput = flag;
			this.Interactable = flag;
			yield break;
		}

		// Token: 0x060079AB RID: 31147 RVA: 0x003333FC File Offset: 0x003317FC
		private IEnumerator CloseCoroutine()
		{
			bool flag = false;
			this.Interactable = flag;
			base.EnabledInput = flag;
			float startAlpha = this.CanvasAlpha;
			yield return ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			}).ToYieldInstruction<TimeInterval<float>>();
			this.CanvasAlpha = 0f;
			this.BlockRaycast = false;
			this.SetActive(base.gameObject, false);
			yield break;
		}

		// Token: 0x060079AC RID: 31148 RVA: 0x00333417 File Offset: 0x00331817
		private void SetActive(GameObject obj, bool flag)
		{
			if (obj != null && obj.activeSelf != flag)
			{
				obj.SetActive(flag);
			}
		}

		// Token: 0x060079AD RID: 31149 RVA: 0x00333438 File Offset: 0x00331838
		private void SetActive(Component com, bool flag)
		{
			if (com != null && com.gameObject != null && com.gameObject.activeSelf != flag)
			{
				com.gameObject.SetActive(flag);
			}
		}

		// Token: 0x060079AE RID: 31150 RVA: 0x00333474 File Offset: 0x00331874
		private void SetInteractable(Selectable obj, bool flag)
		{
			if (obj != null && obj.interactable != flag)
			{
				obj.interactable = flag;
			}
		}

		// Token: 0x040061F4 RID: 25076
		[SerializeField]
		private RecyclingUI _recyclingUI;

		// Token: 0x040061F5 RID: 25077
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x040061F6 RID: 25078
		[SerializeField]
		private RectTransform _rectTransform;

		// Token: 0x040061F7 RID: 25079
		[SerializeField]
		private Text _itemNameText;

		// Token: 0x040061F8 RID: 25080
		[SerializeField]
		private Text _flavorText;

		// Token: 0x040061F9 RID: 25081
		[SerializeField]
		private Button[] _buttons = new Button[3];

		// Token: 0x040061FA RID: 25082
		[SerializeField]
		private Button[] _numChangeButtons = new Button[4];

		// Token: 0x040061FB RID: 25083
		[SerializeField]
		private InputField _numInputField;

		// Token: 0x040061FC RID: 25084
		[NonSerialized]
		private UnityEx.ValueTuple<ItemListController, ItemListController, ItemNodeUI, int, ButtonType> ItemInfo = new UnityEx.ValueTuple<ItemListController, ItemListController, ItemNodeUI, int, ButtonType>(null, null, null, -1, ButtonType.None);

		// Token: 0x040061FE RID: 25086
		private const int ItemMinCount = 0;

		// Token: 0x040061FF RID: 25087
		private int[] _numChangeValues = new int[]
		{
			-10,
			-1,
			1,
			10
		};

		// Token: 0x04006200 RID: 25088
		private IDisposable _fadeDisposable;
	}
}
