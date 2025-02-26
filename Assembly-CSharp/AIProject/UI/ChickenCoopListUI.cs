using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.Animal;
using AIProject.SaveData;
using AIProject.UI.Viewer;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E46 RID: 3654
	public class ChickenCoopListUI : MenuUIBehaviour
	{
		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x060072BF RID: 29375 RVA: 0x0030EC4D File Offset: 0x0030D04D
		public PlaySE playSE { get; } = new PlaySE();

		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x060072C0 RID: 29376 RVA: 0x0030EC58 File Offset: 0x0030D058
		// (remove) Token: 0x060072C1 RID: 29377 RVA: 0x0030EC90 File Offset: 0x0030D090
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int> IconChanged;

		// Token: 0x17001612 RID: 5650
		// (get) Token: 0x060072C2 RID: 29378 RVA: 0x0030ECC6 File Offset: 0x0030D0C6
		public IObservable<Unit> Escape
		{
			[CompilerGenerated]
			get
			{
				return this._escapeButton.OnClickAsObservable();
			}
		}

		// Token: 0x140000BA RID: 186
		// (add) Token: 0x060072C3 RID: 29379 RVA: 0x0030ECD4 File Offset: 0x0030D0D4
		// (remove) Token: 0x060072C4 RID: 29380 RVA: 0x0030ED0C File Offset: 0x0030D10C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action OnSubmit;

		// Token: 0x140000BB RID: 187
		// (add) Token: 0x060072C5 RID: 29381 RVA: 0x0030ED44 File Offset: 0x0030D144
		// (remove) Token: 0x060072C6 RID: 29382 RVA: 0x0030ED7C File Offset: 0x0030D17C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action OnCancel;

		// Token: 0x17001613 RID: 5651
		// (get) Token: 0x060072C7 RID: 29383 RVA: 0x0030EDB2 File Offset: 0x0030D1B2
		public int currentIndex
		{
			[CompilerGenerated]
			get
			{
				return this._currentIndex.Value;
			}
		}

		// Token: 0x17001614 RID: 5652
		// (get) Token: 0x060072C8 RID: 29384 RVA: 0x0030EDC0 File Offset: 0x0030D1C0
		public bool currentActive
		{
			[CompilerGenerated]
			get
			{
				ChickenCoopListUI.Chicken chicken = this._chickens.SafeGet(this._currentIndex.Value);
				bool? flag = (chicken != null) ? new bool?(chicken.isActive) : null;
				return flag != null && flag.Value;
			}
		}

		// Token: 0x17001615 RID: 5653
		// (get) Token: 0x060072C9 RID: 29385 RVA: 0x0030EE19 File Offset: 0x0030D219
		public int Length
		{
			[CompilerGenerated]
			get
			{
				return this._chickens.Length;
			}
		}

		// Token: 0x060072CA RID: 29386 RVA: 0x0030EE24 File Offset: 0x0030D224
		public void SetToggle(int index, bool isOn)
		{
			ChickenCoopListUI.Chicken chicken = this._chickens.SafeGet(index);
			if (chicken != null)
			{
				chicken.toggle.isOn = isOn;
			}
		}

		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x060072CB RID: 29387 RVA: 0x0030EE50 File Offset: 0x0030D250
		private IntReactiveProperty _currentIndex { get; } = new IntReactiveProperty(-1);

		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x060072CC RID: 29388 RVA: 0x0030EE58 File Offset: 0x0030D258
		// (set) Token: 0x060072CD RID: 29389 RVA: 0x0030EE60 File Offset: 0x0030D260
		private int _nameChangeIndex { get; set; } = -1;

		// Token: 0x060072CE RID: 29390 RVA: 0x0030EE69 File Offset: 0x0030D269
		private AIProject.SaveData.Environment.ChickenInfo GetChickenInfo(int index)
		{
			return this._chickenCoopUI.currentChickens.SafeGet(index);
		}

		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x060072CF RID: 29391 RVA: 0x0030EE7C File Offset: 0x0030D27C
		public bool isOpen
		{
			[CompilerGenerated]
			get
			{
				return this.IsActiveControl;
			}
		}

		// Token: 0x060072D0 RID: 29392 RVA: 0x0030EE84 File Offset: 0x0030D284
		public virtual void Open()
		{
			this.IsActiveControl = true;
		}

		// Token: 0x060072D1 RID: 29393 RVA: 0x0030EE8D File Offset: 0x0030D28D
		public virtual void Close()
		{
			if (this.isOpen)
			{
				this.IsActiveControl = false;
				this._nameChangeUI.Close();
			}
		}

		// Token: 0x060072D2 RID: 29394 RVA: 0x0030EEAC File Offset: 0x0030D2AC
		public void Refresh(int index)
		{
			this._chickens[index].Set(this._noneStr, this.GetChickenInfo(index));
			this._currentIndex.SetValueAndForceNotify(index);
		}

		// Token: 0x060072D3 RID: 29395 RVA: 0x0030EED4 File Offset: 0x0030D2D4
		public virtual void Refresh()
		{
			foreach (var <>__AnonType in this._chickens.Select((ChickenCoopListUI.Chicken p, int i) => new
			{
				p,
				i
			}))
			{
				<>__AnonType.p.Set(this._noneStr, this.GetChickenInfo(<>__AnonType.i));
			}
			this.SelectDefault();
		}

		// Token: 0x060072D4 RID: 29396 RVA: 0x0030EF6C File Offset: 0x0030D36C
		public void SelectDefault()
		{
			this.ToggleOFF();
			this._currentIndex.SetValueAndForceNotify(0);
		}

		// Token: 0x060072D5 RID: 29397 RVA: 0x0030EF80 File Offset: 0x0030D380
		public void SelectNone()
		{
			this.ToggleOFF();
			this._currentIndex.SetValueAndForceNotify(-1);
		}

		// Token: 0x060072D6 RID: 29398 RVA: 0x0030EF94 File Offset: 0x0030D394
		public void ToggleOFF()
		{
			foreach (Toggle toggle in from p in this._chickens
			select p.toggle)
			{
				toggle.isOn = false;
			}
		}

		// Token: 0x060072D7 RID: 29399 RVA: 0x0030F010 File Offset: 0x0030D410
		protected override void Start()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			if (this._closeButton != null)
			{
				this._closeButton.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.playSE.Play(SoundPack.SystemSE.Cancel);
					this.OnInputCancel();
				});
			}
			this._currentIndex.Subscribe(delegate(int x)
			{
				bool flag = x >= 0;
				this._currentCursor.gameObject.SetActive(flag);
				if (flag)
				{
					CursorFrame.Set(this._currentCursor.GetComponent<RectTransform>(), this._chickens[x].toggle.GetComponent<RectTransform>(), null);
					if (this.IconChanged != null)
					{
						this.IconChanged(x);
					}
				}
				flag = (this.GetChickenInfo(x) != null);
				this._escapeButton.interactable = flag;
				if (!flag)
				{
					this._nameChangeUI.Close();
				}
			});
			(from p in this._chickens
			select p.toggle).BindToEnter(true, this._selectCursor).AddTo(this);
			(from p in this._chickens
			select p.toggle).BindToGroup(delegate(int sel)
			{
				this._currentIndex.Value = sel;
			}).AddTo(this);
			this._nameChangeUI.OnSubmit += delegate()
			{
				this.playSE.Play(SoundPack.SystemSE.OK_S);
				AIProject.SaveData.Environment.ChickenInfo chickenInfo = this.GetChickenInfo(this._nameChangeIndex);
				this._chickens[this._nameChangeIndex].name.text = (((chickenInfo != null) ? chickenInfo.name : null) ?? string.Empty);
				this.NameChanged(chickenInfo);
			};
			this._nameChangeUI.OnCancel += delegate()
			{
				this.playSE.Play(SoundPack.SystemSE.Cancel);
			};
			using (var enumerator = this._chickens.Select((ChickenCoopListUI.Chicken p, int i) => new
			{
				p,
				i
			}).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					<>__AnonType18<ChickenCoopListUI.Chicken, int> item = enumerator.Current;
					ChickenCoopListUI $this = this;
					(from _ in item.p.NameChange
					select item.i).Subscribe(delegate(int index)
					{
						$this.playSE.Play(SoundPack.SystemSE.OK_S);
						$this._nameChangeIndex = index;
						$this._nameChangeUI.Open($this.GetChickenInfo(index));
					}).AddTo(this);
				}
			}
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
			ActionIDDownCommand actionIDDownCommand3 = new ActionIDDownCommand
			{
				ActionID = ActionID.SquareX
			};
			actionIDDownCommand3.TriggerEvent.AddListener(delegate()
			{
				this.OnInputSubmit();
			});
			this._actionCommands.Add(actionIDDownCommand3);
			actionIDDownCommand3 = new ActionIDDownCommand
			{
				ActionID = ActionID.Submit
			};
			actionIDDownCommand3.TriggerEvent.AddListener(delegate()
			{
				this.OnInputSubmit();
			});
			this._actionCommands.Add(actionIDDownCommand3);
			base.Start();
		}

		// Token: 0x060072D8 RID: 29400 RVA: 0x0030F2B4 File Offset: 0x0030D6B4
		protected virtual void OnDestroy()
		{
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = null;
		}

		// Token: 0x060072D9 RID: 29401 RVA: 0x0030F2D8 File Offset: 0x0030D6D8
		private void SetActiveControl(bool isActive)
		{
			IEnumerator coroutine = (!isActive) ? this.DoClose() : this.DoOpen();
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x060072DA RID: 29402 RVA: 0x0030F340 File Offset: 0x0030D740
		private IEnumerator DoOpen()
		{
			if (this._canvasGroup.blocksRaycasts)
			{
				this._canvasGroup.blocksRaycasts = false;
			}
			this.Refresh();
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

		// Token: 0x060072DB RID: 29403 RVA: 0x0030F35C File Offset: 0x0030D75C
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

		// Token: 0x060072DC RID: 29404 RVA: 0x0030F377 File Offset: 0x0030D777
		private void OnInputSubmit()
		{
			if (!this.isOpen)
			{
				return;
			}
			if (this.OnSubmit != null)
			{
				this.OnSubmit();
			}
		}

		// Token: 0x060072DD RID: 29405 RVA: 0x0030F39D File Offset: 0x0030D79D
		private void OnInputCancel()
		{
			if (!this.isOpen)
			{
				return;
			}
			this.Close();
			if (this.OnCancel != null)
			{
				this.OnCancel();
			}
		}

		// Token: 0x060072DE RID: 29406 RVA: 0x0030F3CC File Offset: 0x0030D7CC
		private void NameChanged(AIProject.SaveData.Environment.ChickenInfo info)
		{
			if (info == null || info.AnimalData == null)
			{
				return;
			}
			AIProject.SaveData.AnimalData animalData = info.AnimalData;
			ReadOnlyDictionary<int, AnimalBase> readOnlyDictionary = (!Singleton<AnimalManager>.IsInstance()) ? null : Singleton<AnimalManager>.Instance.AnimalTable;
			AnimalBase animalBase;
			if (!readOnlyDictionary.IsNullOrEmpty<int, AnimalBase>() && readOnlyDictionary.TryGetValue(animalData.AnimalID, out animalBase) && animalBase is PetChicken)
			{
				(animalBase as PetChicken).Nickname = info.name;
			}
		}

		// Token: 0x04005E02 RID: 24066
		[Header("Infomation Setting")]
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04005E03 RID: 24067
		[SerializeField]
		private ChickenCoopUI _chickenCoopUI;

		// Token: 0x04005E04 RID: 24068
		[SerializeField]
		private Image _currentCursor;

		// Token: 0x04005E05 RID: 24069
		[SerializeField]
		private Image _selectCursor;

		// Token: 0x04005E06 RID: 24070
		[SerializeField]
		private Button _escapeButton;

		// Token: 0x04005E07 RID: 24071
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04005E08 RID: 24072
		[SerializeField]
		private string _noneStr = string.Empty;

		// Token: 0x04005E09 RID: 24073
		[SerializeField]
		private ChickenNameChangeUI _nameChangeUI;

		// Token: 0x04005E0A RID: 24074
		[SerializeField]
		private ChickenCoopListUI.Chicken[] _chickens;

		// Token: 0x04005E0D RID: 24077
		private IDisposable _fadeDisposable;

		// Token: 0x02000E47 RID: 3655
		[Serializable]
		private class Chicken
		{
			// Token: 0x17001619 RID: 5657
			// (get) Token: 0x060072EF RID: 29423 RVA: 0x0030F5CA File Offset: 0x0030D9CA
			public bool isActive
			{
				[CompilerGenerated]
				get
				{
					return this._info != null;
				}
			}

			// Token: 0x1700161A RID: 5658
			// (get) Token: 0x060072F0 RID: 29424 RVA: 0x0030F5D8 File Offset: 0x0030D9D8
			public Toggle toggle
			{
				[CompilerGenerated]
				get
				{
					return this._toggle;
				}
			}

			// Token: 0x1700161B RID: 5659
			// (get) Token: 0x060072F1 RID: 29425 RVA: 0x0030F5E0 File Offset: 0x0030D9E0
			public Image icon
			{
				[CompilerGenerated]
				get
				{
					return this._icon;
				}
			}

			// Token: 0x1700161C RID: 5660
			// (get) Token: 0x060072F2 RID: 29426 RVA: 0x0030F5E8 File Offset: 0x0030D9E8
			public Text name
			{
				[CompilerGenerated]
				get
				{
					return this._name;
				}
			}

			// Token: 0x1700161D RID: 5661
			// (get) Token: 0x060072F3 RID: 29427 RVA: 0x0030F5F0 File Offset: 0x0030D9F0
			public IObservable<Unit> NameChange
			{
				[CompilerGenerated]
				get
				{
					return this._nameChange.OnClickAsObservable();
				}
			}

			// Token: 0x1700161E RID: 5662
			// (get) Token: 0x060072F4 RID: 29428 RVA: 0x0030F5FD File Offset: 0x0030D9FD
			public Button nameChange
			{
				[CompilerGenerated]
				get
				{
					return this._nameChange;
				}
			}

			// Token: 0x1700161F RID: 5663
			// (get) Token: 0x060072F5 RID: 29429 RVA: 0x0030F605 File Offset: 0x0030DA05
			// (set) Token: 0x060072F6 RID: 29430 RVA: 0x0030F60D File Offset: 0x0030DA0D
			private AIProject.SaveData.Environment.ChickenInfo _info { get; set; }

			// Token: 0x060072F7 RID: 29431 RVA: 0x0030F618 File Offset: 0x0030DA18
			public void Set(string noneStr, AIProject.SaveData.Environment.ChickenInfo info)
			{
				this._info = info;
				this._icon.enabled = this.isActive;
				this._name.text = (((info != null) ? info.name : null) ?? noneStr);
				this._nameChange.gameObject.SetActive(this.isActive);
			}

			// Token: 0x04005E13 RID: 24083
			[SerializeField]
			private Toggle _toggle;

			// Token: 0x04005E14 RID: 24084
			[SerializeField]
			private Image _icon;

			// Token: 0x04005E15 RID: 24085
			[SerializeField]
			private Text _name;

			// Token: 0x04005E16 RID: 24086
			[SerializeField]
			private Button _nameChange;
		}
	}
}
