using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using AIProject.SaveData;
using AIProject.UI.Popup;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000FD7 RID: 4055
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasGroup))]
	public class RequestUI : MenuUIBehaviour
	{
		// Token: 0x17001D71 RID: 7537
		// (get) Token: 0x060086EE RID: 34542 RVA: 0x00384C8B File Offset: 0x0038308B
		// (set) Token: 0x060086EF RID: 34543 RVA: 0x00384C93 File Offset: 0x00383093
		public System.Action SubmitEvent { get; set; }

		// Token: 0x17001D72 RID: 7538
		// (get) Token: 0x060086F0 RID: 34544 RVA: 0x00384C9C File Offset: 0x0038309C
		// (set) Token: 0x060086F1 RID: 34545 RVA: 0x00384CA4 File Offset: 0x003830A4
		public bool Submit { get; private set; }

		// Token: 0x17001D73 RID: 7539
		// (get) Token: 0x060086F2 RID: 34546 RVA: 0x00384CAD File Offset: 0x003830AD
		// (set) Token: 0x060086F3 RID: 34547 RVA: 0x00384CB5 File Offset: 0x003830B5
		public bool IsImpossible { get; private set; } = true;

		// Token: 0x17001D74 RID: 7540
		// (get) Token: 0x060086F4 RID: 34548 RVA: 0x00384CBE File Offset: 0x003830BE
		// (set) Token: 0x060086F5 RID: 34549 RVA: 0x00384CC6 File Offset: 0x003830C6
		public Func<bool> SubmitCondition { get; set; }

		// Token: 0x17001D75 RID: 7541
		// (get) Token: 0x060086F6 RID: 34550 RVA: 0x00384CCF File Offset: 0x003830CF
		// (set) Token: 0x060086F7 RID: 34551 RVA: 0x00384CD7 File Offset: 0x003830D7
		public System.Action ClosedEvent { get; set; }

		// Token: 0x17001D76 RID: 7542
		// (get) Token: 0x060086F8 RID: 34552 RVA: 0x00384CE0 File Offset: 0x003830E0
		// (set) Token: 0x060086F9 RID: 34553 RVA: 0x00384CE8 File Offset: 0x003830E8
		public System.Action CancelEvent { get; set; }

		// Token: 0x17001D77 RID: 7543
		// (get) Token: 0x060086FA RID: 34554 RVA: 0x00384CF1 File Offset: 0x003830F1
		// (set) Token: 0x060086FB RID: 34555 RVA: 0x00384CF9 File Offset: 0x003830F9
		public bool Cancel { get; private set; }

		// Token: 0x17001D78 RID: 7544
		// (get) Token: 0x060086FC RID: 34556 RVA: 0x00384D02 File Offset: 0x00383102
		// (set) Token: 0x060086FD RID: 34557 RVA: 0x00384D0A File Offset: 0x0038310A
		public int RequestID { get; set; }

		// Token: 0x17001D79 RID: 7545
		// (get) Token: 0x060086FE RID: 34558 RVA: 0x00384D13 File Offset: 0x00383113
		// (set) Token: 0x060086FF RID: 34559 RVA: 0x00384D1B File Offset: 0x0038311B
		public RequestInfo OpenInfo { get; set; }

		// Token: 0x17001D7A RID: 7546
		// (get) Token: 0x06008700 RID: 34560 RVA: 0x00384D24 File Offset: 0x00383124
		// (set) Token: 0x06008701 RID: 34561 RVA: 0x00384D31 File Offset: 0x00383131
		public override bool IsActiveControl
		{
			get
			{
				return this._isActive.Value;
			}
			set
			{
				if (this._isActive.Value == value)
				{
					return;
				}
				this._isActive.Value = value;
			}
		}

		// Token: 0x17001D7B RID: 7547
		// (get) Token: 0x06008702 RID: 34562 RVA: 0x00384D54 File Offset: 0x00383154
		private MenuUIBehaviour[] MenuUIElements
		{
			[CompilerGenerated]
			get
			{
				MenuUIBehaviour[] result;
				if ((result = this.uiElements) == null)
				{
					result = (this.uiElements = new MenuUIBehaviour[]
					{
						this
					});
				}
				return result;
			}
		}

		// Token: 0x06008703 RID: 34563 RVA: 0x00384D84 File Offset: 0x00383184
		protected override void OnBeforeStart()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			this.lerpStream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).TakeUntilDestroy(this);
			if (this.submitButton != null)
			{
				this.submitButton.onClick.AddListener(delegate()
				{
					this.DoSubmit();
				});
			}
			if (this.cancelButton != null)
			{
				this.cancelButton.onClick.AddListener(delegate()
				{
					this.DoCancel();
				});
			}
			ActionIDDownCommand actionIDDownCommand = new ActionIDDownCommand
			{
				ActionID = ActionID.Cancel
			};
			actionIDDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.DoCancel();
			});
			this._actionCommands.Add(actionIDDownCommand);
			KeyCodeDownCommand keyCodeDownCommand = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Mouse1
			};
			keyCodeDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.DoCancel();
			});
			this._keyCommands.Add(keyCodeDownCommand);
		}

		// Token: 0x06008704 RID: 34564 RVA: 0x00384E82 File Offset: 0x00383282
		private void DoSubmit()
		{
			if (!this.IsActiveControl)
			{
				return;
			}
			this.PlaySubmitSE();
			this.Submit = true;
			this.IsActiveControl = false;
		}

		// Token: 0x06008705 RID: 34565 RVA: 0x00384EA4 File Offset: 0x003832A4
		private void DoCancel()
		{
			if (!this.IsActiveControl)
			{
				return;
			}
			this.PlayCancelSE();
			this.Cancel = true;
			this.IsActiveControl = false;
		}

		// Token: 0x06008706 RID: 34566 RVA: 0x00384EC8 File Offset: 0x003832C8
		protected override void OnAfterStart()
		{
			if (this.canvasGroup.blocksRaycasts)
			{
				this.canvasGroup.blocksRaycasts = false;
			}
			if (this.canvasGroup.interactable)
			{
				this.canvasGroup.interactable = false;
			}
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06008707 RID: 34567 RVA: 0x00384F2C File Offset: 0x0038332C
		private void SetActiveControl(bool _active)
		{
			if (_active)
			{
				this.UISetting();
			}
			IEnumerator _coroutine = (!_active) ? this.CloseCoroutine() : this.OpenCoroutine();
			if (this.fadeDiposable != null)
			{
				this.fadeDiposable.Dispose();
			}
			this.fadeDiposable = Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(this).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
				if (global::Debug.isDebugBuild)
				{
				}
			});
		}

		// Token: 0x06008708 RID: 34568 RVA: 0x00384FDD File Offset: 0x003833DD
		protected override void Awake()
		{
			base.Awake();
			this.Submit = false;
			this.Cancel = false;
		}

		// Token: 0x06008709 RID: 34569 RVA: 0x00384FF4 File Offset: 0x003833F4
		private void HideElements()
		{
			this.messageText.text = string.Empty;
			CanvasGroup canvasGroup = this.requestCanvasGroup0;
			float num = 0f;
			this.requestCanvasGroup2.alpha = num;
			num = num;
			this.requestCanvasGroup1.alpha = num;
			canvasGroup.alpha = num;
			this.IsImpossible = true;
			if (this.submitButton.gameObject.activeSelf)
			{
				this.submitButton.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600870A RID: 34570 RVA: 0x0038506C File Offset: 0x0038346C
		private void UISetting()
		{
			this.IsImpossible = true;
			this.usedItems.Clear();
			ReadOnlyDictionary<int, RequestInfo> requestTable = Singleton<Manager.Resources>.Instance.PopupInfo.RequestTable;
			this.requestInfo = null;
			RequestInfo requestInfo;
			if (!requestTable.TryGetValue(this.RequestID, out requestInfo))
			{
				this.HideElements();
				return;
			}
			this.requestInfo = requestInfo;
			int index = (!Singleton<GameSystem>.IsInstance()) ? 0 : Singleton<GameSystem>.Instance.languageInt;
			this.messageText.text = (requestInfo.Title.GetElement(index) ?? string.Empty);
			switch (requestInfo.Type)
			{
			case 0:
			{
				this.requestCanvasGroup0.alpha = 1f;
				this.requestCanvasGroup1.alpha = 0f;
				this.requestCanvasGroup2.alpha = 0f;
				this.requestText.text = string.Empty;
				Manager.Resources.GameInfoTables gameInfo = Singleton<Manager.Resources>.Instance.GameInfo;
				List<StuffItem> itemList = Singleton<Manager.Map>.Instance.Player.PlayerData.ItemList;
				int itemSlotMax = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.ItemSlotMax;
				bool flag = !requestInfo.Items.IsNullOrEmpty<Tuple<int, int, int>>();
				for (int i = 0; i < this.requestElements.Length; i++)
				{
					bool flag2 = requestInfo.Items.Length <= i;
					this.requestElements[i].canvas.alpha = ((!flag2) ? 1f : 0f);
					if (!flag2)
					{
						int item = requestInfo.Items[i].Item1;
						int item2 = requestInfo.Items[i].Item2;
						int item3 = requestInfo.Items[i].Item3;
						this.usedItems.Add(new StuffItem(item, item2, item3));
						StuffItemInfo item4 = gameInfo.GetItem(item, item2);
						this.requestElements[i].name.text = item4.Name;
						int num = 0;
						foreach (StuffItem stuffItem in itemList)
						{
							if (stuffItem.CategoryID == item && stuffItem.ID == item2)
							{
								num += stuffItem.Count;
							}
						}
						this.requestElements[i].count.text = ((itemSlotMax >= num) ? string.Format("{0}", num) : string.Format("{0}+", itemSlotMax));
						this.requestElements[i].need.text = item3.ToString();
						bool flag3 = num < item3;
						this.requestElements[i].count.color = ((!flag3) ? this.whiteColor : this.redColor);
						if (flag3)
						{
							flag = false;
						}
					}
				}
				if (this.submitButton.gameObject.activeSelf != flag)
				{
					this.submitButton.gameObject.SetActive(flag);
				}
				this.IsImpossible = !flag;
				break;
			}
			case 1:
				this.requestCanvasGroup0.alpha = 0f;
				this.requestCanvasGroup1.alpha = 1f;
				this.requestCanvasGroup2.alpha = 0f;
				this.requestText.text = (requestInfo.Message.GetElement(index) ?? string.Empty);
				if (this.submitButton.gameObject.activeSelf)
				{
					this.submitButton.gameObject.SetActive(false);
				}
				break;
			case 2:
			{
				this.requestCanvasGroup0.alpha = 0f;
				this.requestCanvasGroup1.alpha = 0f;
				this.requestCanvasGroup2.alpha = 1f;
				this.requestText.text = string.Empty;
				int num2 = Manager.Map.GetTotalAgentFlavorAdditionAmount();
				Tuple<int, int, int> element = requestInfo.Items.GetElement(0);
				int? num3 = (element != null) ? new int?(element.Item1) : null;
				int num4 = (num3 == null) ? 9999 : num3.Value;
				if (Singleton<Manager.Resources>.IsInstance())
				{
					Dictionary<int, int> requestFlavorAdditionBorderTable = Singleton<Manager.Resources>.Instance.PopupInfo.RequestFlavorAdditionBorderTable;
					if (!requestFlavorAdditionBorderTable.IsNullOrEmpty<int, int>())
					{
						foreach (KeyValuePair<int, int> keyValuePair in requestFlavorAdditionBorderTable)
						{
							if (keyValuePair.Key >= this.RequestID)
							{
								break;
							}
							num2 -= keyValuePair.Value;
						}
						if (num2 < 0)
						{
							num2 = 0;
						}
					}
				}
				this.leftText_Type2.text = string.Format("{0}", num2);
				this.rightText_Type2.text = string.Format("{0}", num4);
				this.leftText_Type2.color = ((num2 >= num4) ? this.whiteColor : this.redColor);
				if (this.submitButton.gameObject.activeSelf)
				{
					this.submitButton.gameObject.SetActive(false);
				}
				break;
			}
			default:
				this.HideElements();
				break;
			}
		}

		// Token: 0x0600870B RID: 34571 RVA: 0x00385614 File Offset: 0x00383A14
		public void Open(Popup.Request.Type _type)
		{
			if (this.IsActiveControl)
			{
				return;
			}
			this.RequestID = (int)_type;
			this.IsActiveControl = true;
		}

		// Token: 0x0600870C RID: 34572 RVA: 0x00385630 File Offset: 0x00383A30
		private IEnumerator OpenCoroutine()
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			this.Submit = false;
			this.Cancel = false;
			Time.timeScale = 0f;
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			PlayerActor _player = Singleton<Manager.Map>.Instance.Player;
			_player.SetScheduledInteractionState(false);
			_player.ReleaseInteraction();
			MapUIContainer.SetVisibleHUD(false);
			Manager.Input _input = Singleton<Manager.Input>.Instance;
			_input.ReserveState(Manager.Input.ValidType.UI);
			_input.SetupState();
			_input.FocusLevel = 0;
			_input.MenuElements = this.MenuUIElements;
			if (!this.canvasGroup.blocksRaycasts)
			{
				this.canvasGroup.blocksRaycasts = true;
			}
			if (this.canvasGroup.interactable)
			{
				this.canvasGroup.interactable = false;
			}
			float _startAlpha = this.canvasGroup.alpha;
			IConnectableObservable<TimeInterval<float>> _stream = this.lerpStream.Publish<TimeInterval<float>>();
			_stream.Connect();
			_stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.canvasGroup.alpha = Mathf.Lerp(_startAlpha, 1f, x.Value);
			});
			yield return _stream.ToYieldInstruction<TimeInterval<float>>();
			base.EnabledInput = true;
			this.canvasGroup.interactable = true;
			yield break;
		}

		// Token: 0x0600870D RID: 34573 RVA: 0x0038564C File Offset: 0x00383A4C
		private IEnumerator CloseCoroutine()
		{
			Manager.Input _input = Singleton<Manager.Input>.Instance;
			_input.ClearMenuElements();
			_input.ReserveState(Manager.Input.ValidType.Action);
			base.EnabledInput = false;
			this.canvasGroup.interactable = false;
			float _startAlpha = this.canvasGroup.alpha;
			IConnectableObservable<TimeInterval<float>> _stream = this.lerpStream.Publish<TimeInterval<float>>();
			_stream.Connect();
			_stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.canvasGroup.alpha = Mathf.Lerp(_startAlpha, 0f, x.Value);
			});
			yield return _stream.ToYieldInstruction<TimeInterval<float>>();
			PlayerActor _player = Singleton<Manager.Map>.Instance.Player;
			_player.SetScheduledInteractionState(true);
			_player.ReleaseInteraction();
			_input.FocusLevel = -1;
			_input.SetupState();
			this.canvasGroup.blocksRaycasts = false;
			base.gameObject.SetActive(false);
			Time.timeScale = 1f;
			MapUIContainer.SetVisibleHUD(true);
			if (this.Submit)
			{
				Func<bool> submitCondition = this.SubmitCondition;
				bool? flag = (submitCondition != null) ? new bool?(submitCondition()) : null;
				if ((flag == null || flag.Value) && this.requestInfo != null && this.requestInfo.Type == 0)
				{
					List<StuffItem> itemList = Singleton<Manager.Map>.Instance.Player.PlayerData.ItemList;
					for (int i = 0; i < this.usedItems.Count; i++)
					{
						itemList.RemoveItem(this.usedItems[i]);
					}
				}
				System.Action submitEvent = this.SubmitEvent;
				if (submitEvent != null)
				{
					submitEvent();
				}
			}
			this.SubmitEvent = null;
			this.SubmitCondition = null;
			this.Submit = false;
			this.IsImpossible = true;
			if (MapUIContainer.CommandLabel.Acception != CommandLabel.AcceptionState.InvokeAcception)
			{
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
			}
			System.Action closedEvent = this.ClosedEvent;
			if (closedEvent != null)
			{
				closedEvent();
			}
			this.ClosedEvent = null;
			if (this.Cancel)
			{
				if (this.CancelEvent != null)
				{
					this.CancelEvent();
					this.CancelEvent = null;
				}
			}
			else if (this.CancelEvent != null)
			{
				this.CancelEvent = null;
			}
			this.Cancel = false;
			yield break;
		}

		// Token: 0x0600870E RID: 34574 RVA: 0x00385667 File Offset: 0x00383A67
		private void PlaySubmitSE()
		{
			this.PlaySE(SoundPack.SystemSE.OK_L);
		}

		// Token: 0x0600870F RID: 34575 RVA: 0x00385670 File Offset: 0x00383A70
		private void PlayCancelSE()
		{
			this.PlaySE(SoundPack.SystemSE.Cancel);
		}

		// Token: 0x06008710 RID: 34576 RVA: 0x0038567C File Offset: 0x00383A7C
		private void PlaySE(SoundPack.SystemSE se)
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			SoundPack soundPack = Singleton<Manager.Resources>.Instance.SoundPack;
			if (soundPack == null)
			{
				return;
			}
			soundPack.Play(se);
		}

		// Token: 0x04006DA4 RID: 28068
		[SerializeField]
		private CanvasGroup canvasGroup;

		// Token: 0x04006DA5 RID: 28069
		[SerializeField]
		private Text messageText;

		// Token: 0x04006DA6 RID: 28070
		[SerializeField]
		private CanvasGroup requestCanvasGroup0;

		// Token: 0x04006DA7 RID: 28071
		[SerializeField]
		private RequestUI.RequestElement[] requestElements = new RequestUI.RequestElement[0];

		// Token: 0x04006DA8 RID: 28072
		[SerializeField]
		private CanvasGroup requestCanvasGroup1;

		// Token: 0x04006DA9 RID: 28073
		[SerializeField]
		private Text requestText;

		// Token: 0x04006DAA RID: 28074
		[SerializeField]
		private CanvasGroup requestCanvasGroup2;

		// Token: 0x04006DAB RID: 28075
		[SerializeField]
		private Text leftText_Type2;

		// Token: 0x04006DAC RID: 28076
		[SerializeField]
		private Text rightText_Type2;

		// Token: 0x04006DAD RID: 28077
		[SerializeField]
		private Button submitButton;

		// Token: 0x04006DAE RID: 28078
		[SerializeField]
		private Text submitText;

		// Token: 0x04006DAF RID: 28079
		[SerializeField]
		private Button cancelButton;

		// Token: 0x04006DB0 RID: 28080
		[SerializeField]
		private Text cancelText;

		// Token: 0x04006DB1 RID: 28081
		[SerializeField]
		private Color whiteColor = default(Color);

		// Token: 0x04006DB2 RID: 28082
		[SerializeField]
		private Color redColor = default(Color);

		// Token: 0x04006DBC RID: 28092
		private IObservable<TimeInterval<float>> lerpStream;

		// Token: 0x04006DBD RID: 28093
		private MenuUIBehaviour[] uiElements;

		// Token: 0x04006DBE RID: 28094
		private IDisposable fadeDiposable;

		// Token: 0x04006DBF RID: 28095
		private List<StuffItem> usedItems = new List<StuffItem>();

		// Token: 0x04006DC0 RID: 28096
		private RequestInfo requestInfo;

		// Token: 0x02000FD8 RID: 4056
		[Serializable]
		public struct RequestElement
		{
			// Token: 0x04006DC3 RID: 28099
			public CanvasGroup canvas;

			// Token: 0x04006DC4 RID: 28100
			public Text name;

			// Token: 0x04006DC5 RID: 28101
			public Text count;

			// Token: 0x04006DC6 RID: 28102
			public Text need;
		}
	}
}
