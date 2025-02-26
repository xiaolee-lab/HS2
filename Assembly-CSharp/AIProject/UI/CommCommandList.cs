using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Illusion.Extensions;
using Manager;
using ReMotion;
using UniRx;
using UniRx.Toolkit;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx.Misc;

namespace AIProject.UI
{
	// Token: 0x02000E52 RID: 3666
	public class CommCommandList : MenuUIBehaviour
	{
		// Token: 0x060073AE RID: 29614 RVA: 0x003147A8 File Offset: 0x00312BA8
		private void SetActiveControl(bool isActive)
		{
			IEnumerator coroutine = (!isActive) ? this.Close() : this.Open();
			if (this._fadeSubscriber != null)
			{
				this._fadeSubscriber.Dispose();
			}
			this._fadeSubscriber = Observable.FromCoroutine(() => coroutine, false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
			});
		}

		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x060073AF RID: 29615 RVA: 0x00314847 File Offset: 0x00312C47
		// (set) Token: 0x060073B0 RID: 29616 RVA: 0x0031484F File Offset: 0x00312C4F
		public Action OnOpened { get; set; }

		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x060073B1 RID: 29617 RVA: 0x00314858 File Offset: 0x00312C58
		private MenuUIBehaviour[] MenuUIElements
		{
			[CompilerGenerated]
			get
			{
				MenuUIBehaviour[] result;
				if ((result = this._menuUIElements) == null)
				{
					result = (this._menuUIElements = new MenuUIBehaviour[]
					{
						this
					});
				}
				return result;
			}
		}

		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x060073B2 RID: 29618 RVA: 0x00314885 File Offset: 0x00312C85
		// (set) Token: 0x060073B3 RID: 29619 RVA: 0x00314890 File Offset: 0x00312C90
		public bool Visibled
		{
			get
			{
				return this._visibled;
			}
			set
			{
				if (this._visibled == value)
				{
					return;
				}
				this._visibled = value;
				if (this._fadeSubscriber != null)
				{
					this._fadeSubscriber.Dispose();
				}
				IEnumerator coroutine = (!value) ? this.Vanish(this.CanvasGroup) : this.Display(this.CanvasGroup);
				this._fadeSubscriber = Observable.FromCoroutine(() => coroutine, false).Subscribe(delegate(Unit _)
				{
				}, delegate(Exception ex)
				{
				});
			}
		}

		// Token: 0x060073B4 RID: 29620 RVA: 0x0031494F File Offset: 0x00312D4F
		public IObservable<int> OnSelectIDChagnedAsObservable()
		{
			if (this._selectIDChange == null)
			{
				this._selectIDChange = this._selectedID.TakeUntilDestroy(base.gameObject).Publish<int>();
				this._selectIDChange.Connect();
			}
			return this._selectIDChange;
		}

		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x060073B5 RID: 29621 RVA: 0x0031498A File Offset: 0x00312D8A
		public Text Label
		{
			[CompilerGenerated]
			get
			{
				return this._label;
			}
		}

		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x060073B6 RID: 29622 RVA: 0x00314992 File Offset: 0x00312D92
		// (set) Token: 0x060073B7 RID: 29623 RVA: 0x0031499A File Offset: 0x00312D9A
		public Action CancelEvent
		{
			get
			{
				return this._cancelEvent;
			}
			set
			{
				if (this._cancelEvent == value)
				{
					return;
				}
				this._cancelEvent = value;
				this._cancelGuide.SetActive(value != null);
			}
		}

		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x060073B8 RID: 29624 RVA: 0x003149C7 File Offset: 0x00312DC7
		// (set) Token: 0x060073B9 RID: 29625 RVA: 0x003149D0 File Offset: 0x00312DD0
		public CommCommandList.CommandInfo[] Options
		{
			get
			{
				return this._options;
			}
			set
			{
				this._options = value.ToArray<CommCommandList.CommandInfo>();
				this._commandList.Clear();
				foreach (CommCommandList.CommandInfo commandInfo in this._options)
				{
					Func<bool> condition = commandInfo.Condition;
					bool? flag = (condition != null) ? new bool?(condition()) : null;
					if (flag == null || flag.Value)
					{
						this._commandList.Add(commandInfo);
					}
				}
			}
		}

		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x060073BA RID: 29626 RVA: 0x00314A65 File Offset: 0x00312E65
		// (set) Token: 0x060073BB RID: 29627 RVA: 0x00314A6D File Offset: 0x00312E6D
		public int ID { get; set; }

		// Token: 0x17001650 RID: 5712
		// (get) Token: 0x060073BC RID: 29628 RVA: 0x00314A76 File Offset: 0x00312E76
		// (set) Token: 0x060073BD RID: 29629 RVA: 0x00314A7E File Offset: 0x00312E7E
		public CanvasGroup CanvasGroup { get; private set; }

		// Token: 0x060073BE RID: 29630 RVA: 0x00314A87 File Offset: 0x00312E87
		protected override void Awake()
		{
			base.Awake();
			this.CanvasGroup = base.GetComponentInChildren<CanvasGroup>(true);
		}

		// Token: 0x060073BF RID: 29631 RVA: 0x00314A9C File Offset: 0x00312E9C
		protected override void OnBeforeStart()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this._scrollCylinder.enableInternalScroll = false;
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			this.OnSelectIDChagnedAsObservable().Subscribe(delegate(int x)
			{
				this._selectedCommand = this._commandList.GetElement(x);
				if (this._selectedOption != null)
				{
					this._selectedOption.Sprite = this._normalSprite;
				}
				this._selectedOption = this._optionList.GetElement(x);
				if (this._selectedOption != null)
				{
					if (this._selectedCommand.IsSpecial)
					{
						this._selectedOption.Sprite = this._specialSelectedSprite;
					}
					else
					{
						this._selectedOption.Sprite = this._selectedSprite;
					}
				}
				ScrollCylinderNode element = this._nodeList.GetElement(x);
				if (element != null)
				{
					this._scrollCylinder.SetTarget(element);
				}
			});
			IConnectableObservable<long> connectableObservable = Observable.EveryUpdate().TakeUntilDestroy(base.gameObject).Publish<long>();
			connectableObservable.Connect();
			(from _ in connectableObservable
			where this.isActiveAndEnabled
			select _).Subscribe(delegate(long x)
			{
				this.OnUpdate();
			});
			ActionIDDownCommand actionIDDownCommand = new ActionIDDownCommand
			{
				ActionID = ActionID.Cancel
			};
			actionIDDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.OnInputCancel();
			});
			this._actionCommands.Add(actionIDDownCommand);
			ActionIDDownCommand actionIDDownCommand2 = new ActionIDDownCommand
			{
				ActionID = ActionID.MouseRight
			};
			actionIDDownCommand2.TriggerEvent.AddListener(delegate()
			{
				this.OnInputCancel();
			});
			this._actionCommands.Add(actionIDDownCommand2);
			this._nodePool = new CommCommandList.NodePool
			{
				Source = this._node.gameObject
			};
			Manager.Resources.ItemIconTables iconTable = Singleton<Manager.Resources>.Instance.itemIconTables;
			CommonDefine commonDefine = Singleton<Manager.Resources>.Instance.CommonDefine;
			Singleton<Manager.Resources>.Instance.LoadMapResourceStream.Subscribe(delegate(Unit _)
			{
				this._okGuide = UnityEngine.Object.Instantiate<GuideOption>(this._guideNode, this._guideRoot, false);
				this._okGuide.Icon = iconTable.InputIconTable[commonDefine.Icon.GuideOKID];
				this._okGuide.CaptionText = "決定";
				this._okGuide.SetActive(true);
				this._cancelGuide = UnityEngine.Object.Instantiate<GuideOption>(this._guideNode, this._guideRoot, false);
				this._cancelGuide.Icon = iconTable.InputIconTable[commonDefine.Icon.GuideCancelID];
				this._cancelGuide.CaptionText = "もどる";
				this._cancelGuide.SetActive(true);
				this._scrollGuide = UnityEngine.Object.Instantiate<GuideOption>(this._guideNode, this._guideRoot, false);
				this._scrollGuide.Icon = iconTable.InputIconTable[commonDefine.Icon.GuideScrollID];
				this._scrollGuide.CaptionText = "項目選択";
				this._scrollGuide.SetActive(true);
			});
		}

		// Token: 0x060073C0 RID: 29632 RVA: 0x00314C04 File Offset: 0x00313004
		private void OnUpdate()
		{
			if (this._guideRoot != null)
			{
				this._guideRoot.gameObject.SetActiveIfDifferent(Config.GameData.ActionGuide);
			}
			if (!Singleton<Game>.IsInstance() || !Singleton<Manager.Input>.IsInstance())
			{
				return;
			}
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			Game instance2 = Singleton<Game>.Instance;
			if (instance2.ExitScene != null || instance2.Dialog != null || instance2.Config != null || instance2.MapShortcutUI != null)
			{
				return;
			}
			if (!base.EnabledInput || base.FocusLevel != Singleton<Manager.Input>.Instance.FocusLevel)
			{
				return;
			}
			float num = Singleton<Manager.Input>.Instance.ScrollValue();
			if (num < 0f)
			{
				int num2 = this._selectedID.Value + 1;
				if (num2 < this._nodeList.Count)
				{
					this._selectedID.Value = num2;
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				}
			}
			else if (num > 0f)
			{
				int num3 = this._selectedID.Value - 1;
				if (num3 >= 0)
				{
					this._selectedID.Value = num3;
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				}
			}
			if (this._selectedOption != null && this._selectedCommand != null)
			{
				foreach (CommCommandOption commCommandOption in this._optionList)
				{
					if (commCommandOption == this._selectedOption)
					{
						commCommandOption.SetActiveTimer(this._selectedCommand.Timer != null);
						if (commCommandOption.ActiveTimer)
						{
							float num4 = this._selectedCommand.Timer();
							Color timeColor = (num4 <= (float)this._selectedCommand.TimerRedZoneBorder) ? this._redZoneTimerColor : this._normalTimerColor;
							commCommandOption.SetTimeColor(timeColor);
							commCommandOption.SetTime(num4);
						}
					}
					else
					{
						commCommandOption.SetActiveTimer(false);
					}
				}
			}
		}

		// Token: 0x060073C1 RID: 29633 RVA: 0x00314E4C File Offset: 0x0031324C
		private IEnumerator Open()
		{
			if (this._enabledInput.Value)
			{
				this._enabledInput.Value = false;
			}
			this._visibled = true;
			Singleton<Map>.Instance.Player.SetScheduledInteractionState(false);
			Singleton<Map>.Instance.Player.ReleaseInteraction();
			yield return this.DisplayElement(this.CanvasGroup);
			this.CanvasGroup.blocksRaycasts = true;
			Singleton<Manager.Input>.Instance.MenuElements = this.MenuUIElements;
			Singleton<Manager.Input>.Instance.FocusLevel = 0;
			yield return null;
			this._enabledInput.Value = true;
			Action onOpened = this.OnOpened;
			if (onOpened != null)
			{
				onOpened();
			}
			this.OnOpened = null;
			yield break;
		}

		// Token: 0x060073C2 RID: 29634 RVA: 0x00314E68 File Offset: 0x00313268
		private IEnumerator DisplayElement(CanvasGroup canvasGroup)
		{
			Queue<ScrollCylinderNode> queue = this._nodeList.ToQueue<ScrollCylinderNode>();
			while (queue.Count > 0)
			{
				ScrollCylinderNode scrollCylinderNode = queue.Dequeue();
				scrollCylinderNode.transform.SetParent(this._poolRoot, false);
				this._nodePool.Return(scrollCylinderNode);
			}
			this._nodeList.Clear();
			this._optionList.Clear();
			this._scrollCylinder.ClearBlank();
			this._scrollCylinder.BlankSet(this._node, false);
			for (int i = 0; i < this._commandList.Count; i++)
			{
				int optionID = i;
				ScrollCylinderNode scrollCylinderNode2 = this._nodePool.Rent();
				scrollCylinderNode2.transform.SetParent(this._scrollCylinder.Contents.transform);
				CommCommandOption component = scrollCylinderNode2.GetComponent<CommCommandOption>();
				component.OnClick = delegate()
				{
					this.OnInputSubmitFromButton(component, optionID);
				};
				component.LabelText = this._commandList[i].Name;
				component.Sprite = this._normalSprite;
				this._nodeList.Add(scrollCylinderNode2);
				this._optionList.Add(component);
			}
			this._scrollCylinder.ListNodeSet(this._nodeList);
			this._selectedID.SetValueAndForceNotify(0);
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(1f, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			stream.Connect();
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				canvasGroup.alpha = x.Value;
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			canvasGroup.blocksRaycasts = true;
			yield break;
		}

		// Token: 0x060073C3 RID: 29635 RVA: 0x00314E8C File Offset: 0x0031328C
		private IEnumerator Close()
		{
			this._enabledInput.Value = false;
			this._visibled = false;
			Singleton<Manager.Input>.Instance.ClearMenuElements();
			Singleton<Manager.Input>.Instance.FocusLevel = -1;
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
			Singleton<Map>.Instance.Player.SetScheduledInteractionState(true);
			this.CanvasGroup.blocksRaycasts = false;
			Singleton<Map>.Instance.Player.ReleaseInteraction();
			if (Singleton<Map>.Instance.Player.CurrentInteractionState)
			{
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
			}
			else
			{
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			}
			Singleton<Manager.Input>.Instance.SetupState();
			yield return this.VanishElement(this.CanvasGroup);
			if (this._completeDoStop != null)
			{
				this._completeDoStop.OnNext(Unit.Default);
			}
			yield break;
		}

		// Token: 0x060073C4 RID: 29636 RVA: 0x00314EA8 File Offset: 0x003132A8
		private IEnumerator VanishElement(CanvasGroup canvasGroup)
		{
			canvasGroup.blocksRaycasts = false;
			float startAlpha = canvasGroup.alpha;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			stream.Connect();
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			yield break;
		}

		// Token: 0x060073C5 RID: 29637 RVA: 0x00314EC4 File Offset: 0x003132C4
		private IEnumerator Display(CanvasGroup canvasGroup)
		{
			if (this._enabledInput.Value)
			{
				this._enabledInput.Value = false;
			}
			float startAlpha = canvasGroup.alpha;
			IObservable<TimeInterval<float>> stream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			canvasGroup.blocksRaycasts = true;
			if (Singleton<Manager.Input>.Instance.MenuElements.IsNullOrEmpty<MenuUIBehaviour>())
			{
				Singleton<Manager.Input>.Instance.MenuElements = this.MenuUIElements;
			}
			this._enabledInput.Value = true;
			yield break;
		}

		// Token: 0x060073C6 RID: 29638 RVA: 0x00314EE8 File Offset: 0x003132E8
		private IEnumerator Vanish(CanvasGroup canvasGroup)
		{
			this._enabledInput.Value = false;
			canvasGroup.blocksRaycasts = false;
			float startAlpha = canvasGroup.alpha;
			IObservable<TimeInterval<float>> stream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			yield break;
		}

		// Token: 0x060073C7 RID: 29639 RVA: 0x00314F0C File Offset: 0x0031330C
		public IObservable<Unit> OnCompletedStopAsObservable()
		{
			Subject<Unit> result;
			if ((result = this._completeDoStop) == null)
			{
				result = (this._completeDoStop = new Subject<Unit>());
			}
			return result;
		}

		// Token: 0x060073C8 RID: 29640 RVA: 0x00314F34 File Offset: 0x00313334
		public override void OnInputMoveDirection(MoveDirection moveDir)
		{
			if (moveDir != MoveDirection.Down)
			{
				if (moveDir == MoveDirection.Up)
				{
					int num = this._selectedID.Value - 1;
					if (num >= 0)
					{
						this._selectedID.Value = num;
					}
				}
			}
			else
			{
				int num2 = this._selectedID.Value + 1;
				if (num2 < this._nodeList.Count)
				{
					this._selectedID.Value = num2;
				}
			}
		}

		// Token: 0x060073C9 RID: 29641 RVA: 0x00314FAA File Offset: 0x003133AA
		private void OnInputSubmit()
		{
			if (this._selectedCommand != null)
			{
				Action<int> @event = this._selectedCommand.Event;
				if (@event != null)
				{
					@event(this.ID);
				}
			}
		}

		// Token: 0x060073CA RID: 29642 RVA: 0x00314FD8 File Offset: 0x003133D8
		private void OnInputSubmitFromButton(CommCommandOption option, int optionID)
		{
			if (option == this._selectedOption)
			{
				if (this._selectedCommand != null)
				{
					Action<int> @event = this._selectedCommand.Event;
					if (@event != null)
					{
						@event(this.ID);
					}
				}
			}
			else
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				this._selectedID.Value = optionID;
			}
		}

		// Token: 0x060073CB RID: 29643 RVA: 0x00315043 File Offset: 0x00313443
		private void OnInputCancel()
		{
			Action cancelEvent = this.CancelEvent;
			if (cancelEvent != null)
			{
				cancelEvent();
			}
		}

		// Token: 0x060073CC RID: 29644 RVA: 0x0031505C File Offset: 0x0031345C
		public void Refresh(CommCommandList.CommandInfo[] options, Action onCompleted = null)
		{
			if (this._refreshDisposable != null)
			{
				this._refreshDisposable.Dispose();
			}
			this._refreshDisposable = Observable.FromCoroutine(() => this.Reopen(options, this._panelCanvasGroup), false).DoOnCompleted(delegate
			{
				Action onCompleted2 = onCompleted;
				if (onCompleted2 != null)
				{
					onCompleted2();
				}
			}).Subscribe<Unit>();
		}

		// Token: 0x060073CD RID: 29645 RVA: 0x003150CC File Offset: 0x003134CC
		public void Refresh(CommCommandList.CommandInfo[] options, CanvasGroup cg, Action onCompleted = null)
		{
			if (this._refreshDisposable != null)
			{
				this._refreshDisposable.Dispose();
			}
			this._refreshDisposable = Observable.FromCoroutine(() => this.Reopen(options, cg), false).DoOnCompleted(delegate
			{
				Action onCompleted2 = onCompleted;
				if (onCompleted2 != null)
				{
					onCompleted2();
				}
			}).Subscribe<Unit>();
		}

		// Token: 0x060073CE RID: 29646 RVA: 0x00315144 File Offset: 0x00313544
		private IEnumerator Reopen(CommCommandList.CommandInfo[] options, CanvasGroup canvasGroup)
		{
			if (this._enabledInput.Value)
			{
				this._enabledInput.Value = false;
				yield return this.VanishElement(canvasGroup);
			}
			this._options = options;
			this._commandList.Clear();
			foreach (CommCommandList.CommandInfo commandInfo in this._options)
			{
				Func<bool> condition = commandInfo.Condition;
				bool? flag = (condition != null) ? new bool?(condition()) : null;
				if (flag == null || flag.Value)
				{
					this._commandList.Add(commandInfo);
				}
			}
			yield return this.DisplayElement(canvasGroup);
			this._visibled = true;
			this._enabledInput.Value = true;
			yield break;
		}

		// Token: 0x04005EA2 RID: 24226
		private IDisposable _fadeSubscriber;

		// Token: 0x04005EA4 RID: 24228
		private MenuUIBehaviour[] _menuUIElements;

		// Token: 0x04005EA5 RID: 24229
		private bool _visibled = true;

		// Token: 0x04005EA6 RID: 24230
		private IDisposable _refreshDisposable;

		// Token: 0x04005EA7 RID: 24231
		[SerializeField]
		private IntReactiveProperty _selectedID = new IntReactiveProperty(0);

		// Token: 0x04005EA8 RID: 24232
		private IConnectableObservable<int> _selectIDChange;

		// Token: 0x04005EA9 RID: 24233
		[SerializeField]
		private ScrollCylinder _scrollCylinder;

		// Token: 0x04005EAA RID: 24234
		[SerializeField]
		private Text _label;

		// Token: 0x04005EAB RID: 24235
		[SerializeField]
		private RectTransform _guideRoot;

		// Token: 0x04005EAC RID: 24236
		[SerializeField]
		private RectTransform _poolRoot;

		// Token: 0x04005EAD RID: 24237
		[SerializeField]
		private ScrollCylinderNode _node;

		// Token: 0x04005EAE RID: 24238
		[SerializeField]
		private GuideOption _guideNode;

		// Token: 0x04005EAF RID: 24239
		[SerializeField]
		private Sprite _normalSprite;

		// Token: 0x04005EB0 RID: 24240
		[SerializeField]
		private Sprite _selectedSprite;

		// Token: 0x04005EB1 RID: 24241
		[SerializeField]
		private Sprite _specialSelectedSprite;

		// Token: 0x04005EB2 RID: 24242
		[SerializeField]
		private Color _normalTimerColor = Color.white;

		// Token: 0x04005EB3 RID: 24243
		[SerializeField]
		private Color _redZoneTimerColor = Color.white;

		// Token: 0x04005EB4 RID: 24244
		private CommCommandList.CommandInfo _selectedCommand;

		// Token: 0x04005EB5 RID: 24245
		private CommCommandOption _selectedOption;

		// Token: 0x04005EB6 RID: 24246
		private Action _cancelEvent;

		// Token: 0x04005EB7 RID: 24247
		private List<CommCommandList.CommandInfo> _commandList = new List<CommCommandList.CommandInfo>();

		// Token: 0x04005EB8 RID: 24248
		private CommCommandList.CommandInfo[] _options;

		// Token: 0x04005EB9 RID: 24249
		private CommCommandList.NodePool _nodePool;

		// Token: 0x04005EBC RID: 24252
		[SerializeField]
		private CanvasGroup _panelCanvasGroup;

		// Token: 0x04005EBD RID: 24253
		private List<ScrollCylinderNode> _nodeList = new List<ScrollCylinderNode>();

		// Token: 0x04005EBE RID: 24254
		private List<CommCommandOption> _optionList = new List<CommCommandOption>();

		// Token: 0x04005EBF RID: 24255
		private GuideOption _okGuide;

		// Token: 0x04005EC0 RID: 24256
		private GuideOption _cancelGuide;

		// Token: 0x04005EC1 RID: 24257
		private GuideOption _scrollGuide;

		// Token: 0x04005EC2 RID: 24258
		private Subject<Unit> _completeDoStop;

		// Token: 0x02000E53 RID: 3667
		public class NodePool : ObjectPool<ScrollCylinderNode>
		{
			// Token: 0x17001651 RID: 5713
			// (get) Token: 0x060073D4 RID: 29652 RVA: 0x0031517D File Offset: 0x0031357D
			// (set) Token: 0x060073D5 RID: 29653 RVA: 0x00315185 File Offset: 0x00313585
			public GameObject Source { get; set; }

			// Token: 0x060073D6 RID: 29654 RVA: 0x00315190 File Offset: 0x00313590
			protected override ScrollCylinderNode CreateInstance()
			{
				return UnityEngine.Object.Instantiate<GameObject>(this.Source).GetComponent<ScrollCylinderNode>();
			}
		}

		// Token: 0x02000E54 RID: 3668
		[Serializable]
		public class CommandInfo
		{
			// Token: 0x060073D7 RID: 29655 RVA: 0x003151AF File Offset: 0x003135AF
			public CommandInfo()
			{
			}

			// Token: 0x060073D8 RID: 29656 RVA: 0x003151B7 File Offset: 0x003135B7
			public CommandInfo(string name)
			{
				this.Name = name;
			}

			// Token: 0x060073D9 RID: 29657 RVA: 0x003151C6 File Offset: 0x003135C6
			public CommandInfo(string name, Func<bool> cond, Action<int> action)
			{
				this.Name = name;
				this.Condition = cond;
				this.Event = action;
			}

			// Token: 0x17001652 RID: 5714
			// (get) Token: 0x060073DA RID: 29658 RVA: 0x003151E3 File Offset: 0x003135E3
			// (set) Token: 0x060073DB RID: 29659 RVA: 0x003151EB File Offset: 0x003135EB
			public string Name { get; set; }

			// Token: 0x17001653 RID: 5715
			// (get) Token: 0x060073DC RID: 29660 RVA: 0x003151F4 File Offset: 0x003135F4
			// (set) Token: 0x060073DD RID: 29661 RVA: 0x003151FC File Offset: 0x003135FC
			public Func<bool> Condition { get; set; }

			// Token: 0x17001654 RID: 5716
			// (get) Token: 0x060073DE RID: 29662 RVA: 0x00315205 File Offset: 0x00313605
			// (set) Token: 0x060073DF RID: 29663 RVA: 0x0031520D File Offset: 0x0031360D
			public Action<int> Event { get; set; }

			// Token: 0x17001655 RID: 5717
			// (get) Token: 0x060073E0 RID: 29664 RVA: 0x00315216 File Offset: 0x00313616
			// (set) Token: 0x060073E1 RID: 29665 RVA: 0x0031521E File Offset: 0x0031361E
			public bool IsSpecial { get; set; }

			// Token: 0x17001656 RID: 5718
			// (get) Token: 0x060073E2 RID: 29666 RVA: 0x00315227 File Offset: 0x00313627
			// (set) Token: 0x060073E3 RID: 29667 RVA: 0x0031522F File Offset: 0x0031362F
			public Func<float> Timer { get; set; }

			// Token: 0x17001657 RID: 5719
			// (get) Token: 0x060073E4 RID: 29668 RVA: 0x00315238 File Offset: 0x00313638
			// (set) Token: 0x060073E5 RID: 29669 RVA: 0x00315240 File Offset: 0x00313640
			public int TimerRedZoneBorder { get; set; }
		}
	}
}
