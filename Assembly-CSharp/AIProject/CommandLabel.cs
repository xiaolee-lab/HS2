using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.UI;
using Illusion.Extensions;
using Manager;
using ReMotion;
using UniRx;
using UniRx.Toolkit;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000E4D RID: 3661
	public class CommandLabel : MonoBehaviour, IActionCommand
	{
		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x0600735F RID: 29535 RVA: 0x0031294C File Offset: 0x00310D4C
		// (set) Token: 0x06007360 RID: 29536 RVA: 0x00312954 File Offset: 0x00310D54
		public CommandLabel.CommandInfo SubjectCommand { get; set; }

		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x06007361 RID: 29537 RVA: 0x0031295D File Offset: 0x00310D5D
		// (set) Token: 0x06007362 RID: 29538 RVA: 0x00312965 File Offset: 0x00310D65
		public CommandLabel.CommandInfo[] ObjectCommands
		{
			get
			{
				return this._objectCommands;
			}
			set
			{
				this._objectCommands = value;
				this.RefreshCommands();
			}
		}

		// Token: 0x06007363 RID: 29539 RVA: 0x00312974 File Offset: 0x00310D74
		public void RefreshCommands()
		{
			foreach (CommandLabelOption commandLabelOption in this._commandLabels)
			{
				commandLabelOption.Visibility = 0;
			}
			this._commandLabels.Clear();
			this._commands.Clear();
			if (this._acception == CommandLabel.AcceptionState.InvokeAcception)
			{
				if (this._objectCommands.IsNullOrEmpty<CommandLabel.CommandInfo>() && this.SubjectCommand != null)
				{
					this._commands.Add(this.SubjectCommand);
				}
				for (int i = 0; i < this._objectCommands.Length; i++)
				{
					CommandLabel.CommandInfo item = this._objectCommands[i];
					this._commands.Add(item);
				}
				List<CommandLabel.CommandInfo> commands = this._commands;
				if (CommandLabel.<>f__mg$cache0 == null)
				{
					CommandLabel.<>f__mg$cache0 = new Comparison<CommandLabel.CommandInfo>(CommandLabel.CompareDistance);
				}
				commands.Sort(CommandLabel.<>f__mg$cache0);
				for (int j = 0; j < this._commands.Count; j++)
				{
					CommandLabel.CommandInfo commandInfo = this._commands[j];
					if (commandInfo.Transform != null)
					{
						CommandLabelOption commandLabelOption2 = this._objectPool.Rent();
						commandLabelOption2.transform.SetParent(this._labelRoot, false);
						commandLabelOption2.transform.localPosition = Vector3.zero;
						commandLabelOption2.CommandInfo = commandInfo;
						this._commandLabels.Add(commandLabelOption2);
					}
					else
					{
						CommandLabelOption commandLabelOption3 = this._subjectPool.Rent();
						if (commandLabelOption3.transform.parent != this._subjectRoot)
						{
							commandLabelOption3.transform.SetParent(this._subjectRoot, false);
						}
						commandLabelOption3.CommandInfo = commandInfo;
						this._commandLabels.Add(commandLabelOption3);
					}
				}
				this.CommandID = 0;
			}
			else if (this._acception == CommandLabel.AcceptionState.CancelAcception)
			{
				if (this._cancelCommand != null)
				{
					CommandLabelOption commandLabelOption4 = this._subjectPool.Rent();
					if (commandLabelOption4.transform.parent != this._subjectRoot)
					{
						commandLabelOption4.transform.SetParent(this._subjectRoot, false);
					}
					commandLabelOption4.CommandInfo = this._cancelCommand;
					this._commandLabels.Add(commandLabelOption4);
				}
				this.CommandID = 0;
			}
		}

		// Token: 0x06007364 RID: 29540 RVA: 0x00312BD4 File Offset: 0x00310FD4
		private static int CompareDistance(CommandLabel.CommandInfo a, CommandLabel.CommandInfo b)
		{
			if (Singleton<Map>.IsInstance())
			{
				Vector3 position = Singleton<Map>.Instance.Player.Position;
				float num = Vector3.Distance(a.Position, position);
				float num2 = Vector3.Distance(b.Position, position);
				if (num > num2)
				{
					return 1;
				}
				if (num2 < num)
				{
					return -1;
				}
			}
			return 0;
		}

		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x06007365 RID: 29541 RVA: 0x00312C28 File Offset: 0x00311028
		// (set) Token: 0x06007366 RID: 29542 RVA: 0x00312C30 File Offset: 0x00311030
		public CommandLabel.CommandInfo CancelCommand
		{
			get
			{
				return this._cancelCommand;
			}
			set
			{
				this._cancelCommand = value;
				this.RefreshCommands();
			}
		}

		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x06007367 RID: 29543 RVA: 0x00312C3F File Offset: 0x0031103F
		// (set) Token: 0x06007368 RID: 29544 RVA: 0x00312C48 File Offset: 0x00311048
		private int CommandID
		{
			get
			{
				return this._commandID;
			}
			set
			{
				this._commandID = value;
				if (this._commandID >= this._commands.Count)
				{
					this._commandID = 0;
				}
				else if (this._commandID < 0)
				{
					this._commandID = this._commands.Count - 1;
				}
				if (this._commandLabels.Count == 0)
				{
					return;
				}
				CommandLabelOption commandLabelOption = (this._commandLabels != null) ? this._commandLabels.GetElement(this._commandID) : null;
				commandLabelOption.Visibility = 2;
				foreach (CommandLabelOption commandLabelOption2 in this._commandLabels)
				{
					if (!(commandLabelOption2 == commandLabelOption))
					{
						commandLabelOption2.Visibility = 1;
					}
				}
			}
		}

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x06007369 RID: 29545 RVA: 0x00312D38 File Offset: 0x00311138
		// (set) Token: 0x0600736A RID: 29546 RVA: 0x00312D40 File Offset: 0x00311140
		public Tuple<CommandType, int> EventTypeTuple { get; set; }

		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x0600736B RID: 29547 RVA: 0x00312D49 File Offset: 0x00311149
		// (set) Token: 0x0600736C RID: 29548 RVA: 0x00312D54 File Offset: 0x00311154
		public CommandLabel.AcceptionState Acception
		{
			get
			{
				return this._acception;
			}
			set
			{
				this._acception = value;
				if (this._disposable != null)
				{
					this._disposable.Dispose();
				}
				float startValueA = this._optionsCanvasGroup.alpha;
				if (value == CommandLabel.AcceptionState.None)
				{
					this._disposable = ObservableEasing.EaseOutQuint(0.5f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
					{
						this._optionsCanvasGroup.alpha = Mathf.Lerp(startValueA, 0f, x.Value);
					}, delegate()
					{
						this._disposable = null;
					});
				}
				else if (value == CommandLabel.AcceptionState.InvokeAcception)
				{
					this._disposable = ObservableEasing.EaseOutQuint(0.5f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
					{
						this._optionsCanvasGroup.alpha = Mathf.Lerp(startValueA, 1f, x.Value);
					}, delegate()
					{
						this._disposable = null;
					});
				}
				else if (value == CommandLabel.AcceptionState.CancelAcception)
				{
					this._disposable = ObservableEasing.EaseOutQuint(0.5f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
					{
						this._optionsCanvasGroup.alpha = Mathf.Lerp(startValueA, 0f, x.Value);
					}, delegate()
					{
						this._disposable = null;
					});
				}
			}
		}

		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x0600736D RID: 29549 RVA: 0x00312E56 File Offset: 0x00311256
		// (set) Token: 0x0600736E RID: 29550 RVA: 0x00312E5E File Offset: 0x0031125E
		public bool EnabledInput { get; set; }

		// Token: 0x0600736F RID: 29551 RVA: 0x00312E68 File Offset: 0x00311268
		private void Start()
		{
			this._objectPool.Source = this._labelNode;
			this._subjectPool.Source = this._subjectLabelNode;
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
			ActionIDDownCommand actionIDDownCommand = new ActionIDDownCommand
			{
				ActionID = ActionID.Action
			};
			actionIDDownCommand.TriggerEvent.AddListener(delegate()
			{
				if (this._acception != CommandLabel.AcceptionState.None && this._disposable == null)
				{
					this._isPressedAction = true;
				}
			});
			this._actionTriggerCommands.Add(actionIDDownCommand);
			ActionIDCommand actionIDCommand = new ActionIDCommand
			{
				ActionID = ActionID.Action
			};
			actionIDCommand.TriggerEvent.AddListener(delegate(bool x)
			{
				this.OnInputAction(x);
			});
			this._actionCommands.Add(actionIDCommand);
		}

		// Token: 0x06007370 RID: 29552 RVA: 0x00312F30 File Offset: 0x00311330
		private void OnUpdate()
		{
			this._subjectRoot.gameObject.SetActiveIfDifferent(Config.GameData.ActionGuide);
			if (this._commands.IsNullOrEmpty<CommandLabel.CommandInfo>())
			{
				return;
			}
			float value = (!Mathf.Approximately(this._holdDuration, 0f)) ? (this._holdElapsedTime / this._holdDuration) : 0f;
			this._commands[this._commandID].FillRate = Mathf.Clamp01(value);
		}

		// Token: 0x06007371 RID: 29553 RVA: 0x00312FB2 File Offset: 0x003113B2
		public void NextCommandID()
		{
			this.CommandID++;
		}

		// Token: 0x06007372 RID: 29554 RVA: 0x00312FC2 File Offset: 0x003113C2
		public void PrevCommandID()
		{
			this.CommandID--;
		}

		// Token: 0x06007373 RID: 29555 RVA: 0x00312FD4 File Offset: 0x003113D4
		public void OnUpdateInput()
		{
			CommandLabel.AcceptionState acception = this._acception;
			if (acception != CommandLabel.AcceptionState.InvokeAcception)
			{
				if (acception != CommandLabel.AcceptionState.CancelAcception)
				{
					if (acception == CommandLabel.AcceptionState.None)
					{
						return;
					}
				}
				else if (this._cancelCommand == null)
				{
					return;
				}
			}
			else if (this._commands.IsNullOrEmpty<CommandLabel.CommandInfo>())
			{
				return;
			}
			if (!Singleton<Manager.Input>.IsInstance())
			{
				return;
			}
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			foreach (ActionIDDownCommand actionIDDownCommand in this._actionTriggerCommands)
			{
				actionIDDownCommand.Invoke(instance);
			}
			foreach (ActionIDCommand actionIDCommand in this._actionCommands)
			{
				actionIDCommand.Invoke(instance);
			}
			CommandLabel.AcceptionState acception2 = this._acception;
			if (acception2 == CommandLabel.AcceptionState.InvokeAcception)
			{
				float num = instance.ScrollValue();
				if (num > 0f)
				{
					this.PrevCommandID();
				}
				else if (num < 0f)
				{
					this.NextCommandID();
				}
			}
		}

		// Token: 0x06007374 RID: 29556 RVA: 0x00313124 File Offset: 0x00311524
		private void OnInputAction(bool isDown)
		{
			if (this._acception == CommandLabel.AcceptionState.None || this._disposable != null)
			{
				return;
			}
			if (isDown)
			{
				CommandLabel.AcceptionState acception = this._acception;
				if (acception != CommandLabel.AcceptionState.InvokeAcception)
				{
					if (acception == CommandLabel.AcceptionState.CancelAcception)
					{
						if (this._cancelCommand != null)
						{
							Action @event = this._cancelCommand.Event;
							if (@event != null)
							{
								@event();
							}
							this._cancelCommand = null;
						}
					}
				}
				else
				{
					CommandLabel.CommandInfo element = this._commands.GetElement(this._commandID);
					if (element != null)
					{
						if (element.IsHold)
						{
							if (this._holdElapsedTime < this._holdDuration && this._isPressedAction && Singleton<Map>.IsInstance() && Singleton<Map>.Instance.Player != null)
							{
								CommandLabel.AcceptionState acception2 = this._acception;
								if (acception2 == CommandLabel.AcceptionState.InvokeAcception)
								{
									this._holdElapsedTime += Time.deltaTime;
								}
							}
							if (this._holdElapsedTime >= this._holdDuration)
							{
								PlayerActor player = Singleton<Map>.Instance.Player;
								Func<PlayerActor, bool> condition = element.Condition;
								bool? flag = (condition != null) ? new bool?(condition(player)) : null;
								if (flag == null || flag.Value)
								{
									Action event2 = element.Event;
									if (event2 != null)
									{
										event2();
									}
								}
								else if (element.ErrorText != null)
								{
									string mes = element.ErrorText(player);
									MapUIContainer.PushMessageUI(mes, 2, 0, null);
								}
								this._holdElapsedTime = 0f;
								this._isPressedAction = false;
							}
						}
						else if (this._isPressedAction && Singleton<Map>.IsInstance() && Singleton<Map>.Instance.Player != null)
						{
							PlayerActor player2 = Singleton<Map>.Instance.Player;
							Func<PlayerActor, bool> condition2 = element.Condition;
							bool? flag2 = (condition2 != null) ? new bool?(condition2(player2)) : null;
							if (flag2 == null || flag2.Value)
							{
								Action event3 = element.Event;
								if (event3 != null)
								{
									event3();
								}
							}
							else if (element.ErrorText != null)
							{
								string mes2 = element.ErrorText(player2);
								MapUIContainer.PushMessageUI(mes2, 2, 0, null);
							}
							this._isPressedAction = false;
						}
					}
				}
			}
			else
			{
				this._isPressedAction = false;
				this._holdElapsedTime = 0f;
			}
		}

		// Token: 0x06007375 RID: 29557 RVA: 0x003133AD File Offset: 0x003117AD
		private void OnInputLeftShoulder()
		{
			this.PrevCommandID();
		}

		// Token: 0x06007376 RID: 29558 RVA: 0x003133B5 File Offset: 0x003117B5
		private void OnInputRightShoulder()
		{
			this.NextCommandID();
		}

		// Token: 0x04005E56 RID: 24150
		[SerializeField]
		private Transform _labelRoot;

		// Token: 0x04005E57 RID: 24151
		[SerializeField]
		private Transform _subjectRoot;

		// Token: 0x04005E58 RID: 24152
		[SerializeField]
		private CanvasGroup _optionsCanvasGroup;

		// Token: 0x04005E59 RID: 24153
		[SerializeField]
		private float _holdDuration = 1f;

		// Token: 0x04005E5A RID: 24154
		[SerializeField]
		private GameObject _labelNode;

		// Token: 0x04005E5B RID: 24155
		[SerializeField]
		private GameObject _subjectLabelNode;

		// Token: 0x04005E5C RID: 24156
		private List<CommandLabel.CommandInfo> _commands = new List<CommandLabel.CommandInfo>();

		// Token: 0x04005E5E RID: 24158
		private CommandLabel.CommandInfo[] _objectCommands = new CommandLabel.CommandInfo[0];

		// Token: 0x04005E5F RID: 24159
		private List<CommandLabelOption> _commandLabels = new List<CommandLabelOption>();

		// Token: 0x04005E60 RID: 24160
		private CommandLabel.LabelPool _objectPool = new CommandLabel.LabelPool();

		// Token: 0x04005E61 RID: 24161
		private CommandLabel.LabelPool _subjectPool = new CommandLabel.LabelPool();

		// Token: 0x04005E62 RID: 24162
		private List<ActionIDCommand> _actionCommands = new List<ActionIDCommand>();

		// Token: 0x04005E63 RID: 24163
		private List<ActionIDDownCommand> _actionTriggerCommands = new List<ActionIDDownCommand>();

		// Token: 0x04005E64 RID: 24164
		private CommandLabel.CommandInfo _cancelCommand;

		// Token: 0x04005E65 RID: 24165
		private int _commandID;

		// Token: 0x04005E67 RID: 24167
		private CommandLabel.AcceptionState _acception;

		// Token: 0x04005E68 RID: 24168
		private IDisposable _disposable;

		// Token: 0x04005E6A RID: 24170
		private bool _isPressedAction;

		// Token: 0x04005E6B RID: 24171
		private float _holdElapsedTime;

		// Token: 0x04005E6C RID: 24172
		[CompilerGenerated]
		private static Comparison<CommandLabel.CommandInfo> <>f__mg$cache0;

		// Token: 0x02000E4E RID: 3662
		public enum AcceptionState
		{
			// Token: 0x04005E6E RID: 24174
			InvokeAcception,
			// Token: 0x04005E6F RID: 24175
			CancelAcception,
			// Token: 0x04005E70 RID: 24176
			None
		}

		// Token: 0x02000E4F RID: 3663
		public class CommandInfo
		{
			// Token: 0x17001636 RID: 5686
			// (get) Token: 0x0600737C RID: 29564 RVA: 0x003133FE File Offset: 0x003117FE
			// (set) Token: 0x0600737D RID: 29565 RVA: 0x00313406 File Offset: 0x00311806
			public EventType EventType { get; set; }

			// Token: 0x17001637 RID: 5687
			// (get) Token: 0x0600737E RID: 29566 RVA: 0x0031340F File Offset: 0x0031180F
			// (set) Token: 0x0600737F RID: 29567 RVA: 0x00313417 File Offset: 0x00311817
			public Func<string> OnText { get; set; }

			// Token: 0x17001638 RID: 5688
			// (get) Token: 0x06007380 RID: 29568 RVA: 0x00313420 File Offset: 0x00311820
			// (set) Token: 0x06007381 RID: 29569 RVA: 0x00313428 File Offset: 0x00311828
			public string Text { get; set; }

			// Token: 0x17001639 RID: 5689
			// (get) Token: 0x06007382 RID: 29570 RVA: 0x00313431 File Offset: 0x00311831
			// (set) Token: 0x06007383 RID: 29571 RVA: 0x00313439 File Offset: 0x00311839
			public Sprite Icon { get; set; }

			// Token: 0x1700163A RID: 5690
			// (get) Token: 0x06007384 RID: 29572 RVA: 0x00313442 File Offset: 0x00311842
			// (set) Token: 0x06007385 RID: 29573 RVA: 0x0031344A File Offset: 0x0031184A
			public CommandTargetSpriteInfo TargetSpriteInfo { get; set; }

			// Token: 0x1700163B RID: 5691
			// (get) Token: 0x06007386 RID: 29574 RVA: 0x00313453 File Offset: 0x00311853
			// (set) Token: 0x06007387 RID: 29575 RVA: 0x0031345B File Offset: 0x0031185B
			public bool IsHold { get; set; }

			// Token: 0x1700163C RID: 5692
			// (get) Token: 0x06007388 RID: 29576 RVA: 0x00313464 File Offset: 0x00311864
			// (set) Token: 0x06007389 RID: 29577 RVA: 0x0031346C File Offset: 0x0031186C
			public float FillRate { get; set; }

			// Token: 0x1700163D RID: 5693
			// (get) Token: 0x0600738A RID: 29578 RVA: 0x00313475 File Offset: 0x00311875
			// (set) Token: 0x0600738B RID: 29579 RVA: 0x0031347D File Offset: 0x0031187D
			public Func<float> CoolTimeFillRate { get; set; }

			// Token: 0x1700163E RID: 5694
			// (get) Token: 0x0600738C RID: 29580 RVA: 0x00313486 File Offset: 0x00311886
			// (set) Token: 0x0600738D RID: 29581 RVA: 0x0031348E File Offset: 0x0031188E
			public Vector3 Position { get; set; }

			// Token: 0x1700163F RID: 5695
			// (get) Token: 0x0600738E RID: 29582 RVA: 0x00313497 File Offset: 0x00311897
			// (set) Token: 0x0600738F RID: 29583 RVA: 0x0031349F File Offset: 0x0031189F
			public Transform Transform { get; set; }

			// Token: 0x17001640 RID: 5696
			// (get) Token: 0x06007390 RID: 29584 RVA: 0x003134A8 File Offset: 0x003118A8
			// (set) Token: 0x06007391 RID: 29585 RVA: 0x003134B0 File Offset: 0x003118B0
			public bool ReferenceCanvasPosition { get; set; }

			// Token: 0x17001641 RID: 5697
			// (get) Token: 0x06007392 RID: 29586 RVA: 0x003134B9 File Offset: 0x003118B9
			// (set) Token: 0x06007393 RID: 29587 RVA: 0x003134C1 File Offset: 0x003118C1
			public Func<PlayerActor, bool> Condition { get; set; }

			// Token: 0x17001642 RID: 5698
			// (get) Token: 0x06007394 RID: 29588 RVA: 0x003134CA File Offset: 0x003118CA
			// (set) Token: 0x06007395 RID: 29589 RVA: 0x003134D2 File Offset: 0x003118D2
			public Func<PlayerActor, string> ErrorText { get; set; }

			// Token: 0x17001643 RID: 5699
			// (get) Token: 0x06007396 RID: 29590 RVA: 0x003134DB File Offset: 0x003118DB
			// (set) Token: 0x06007397 RID: 29591 RVA: 0x003134E3 File Offset: 0x003118E3
			public Func<bool> DisplayCondition { get; set; }

			// Token: 0x17001644 RID: 5700
			// (get) Token: 0x06007398 RID: 29592 RVA: 0x003134EC File Offset: 0x003118EC
			// (set) Token: 0x06007399 RID: 29593 RVA: 0x003134F4 File Offset: 0x003118F4
			public Action Event { get; set; }
		}

		// Token: 0x02000E50 RID: 3664
		public class LabelPool : ObjectPool<CommandLabelOption>
		{
			// Token: 0x17001645 RID: 5701
			// (get) Token: 0x0600739B RID: 29595 RVA: 0x00313505 File Offset: 0x00311905
			// (set) Token: 0x0600739C RID: 29596 RVA: 0x0031350D File Offset: 0x0031190D
			public GameObject Source { get; set; }

			// Token: 0x0600739D RID: 29597 RVA: 0x00313518 File Offset: 0x00311918
			protected override CommandLabelOption CreateInstance()
			{
				CommandLabelOption component = UnityEngine.Object.Instantiate<GameObject>(this.Source).GetComponent<CommandLabelOption>();
				component.PoolSource = this;
				return component;
			}
		}
	}
}
