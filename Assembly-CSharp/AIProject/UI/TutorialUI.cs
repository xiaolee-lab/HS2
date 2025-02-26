using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using AIProject.Animal;
using AIProject.Definitions;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI
{
	// Token: 0x02000FE0 RID: 4064
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasGroup))]
	public class TutorialUI : MenuUIBehaviour
	{
		// Token: 0x17001DAE RID: 7598
		// (get) Token: 0x060087CD RID: 34765 RVA: 0x003899CE File Offset: 0x00387DCE
		// (set) Token: 0x060087CE RID: 34766 RVA: 0x003899D6 File Offset: 0x00387DD6
		public int OpenElementNumber { get; protected set; } = -1;

		// Token: 0x17001DAF RID: 7599
		// (get) Token: 0x060087CF RID: 34767 RVA: 0x003899DF File Offset: 0x00387DDF
		// (set) Token: 0x060087D0 RID: 34768 RVA: 0x003899E7 File Offset: 0x00387DE7
		public int OpenIndex { get; private set; }

		// Token: 0x17001DB0 RID: 7600
		// (get) Token: 0x060087D1 RID: 34769 RVA: 0x003899F0 File Offset: 0x00387DF0
		// (set) Token: 0x060087D2 RID: 34770 RVA: 0x003899F8 File Offset: 0x00387DF8
		public bool OpenGroupEnabled { get; protected set; }

		// Token: 0x17001DB1 RID: 7601
		// (get) Token: 0x060087D3 RID: 34771 RVA: 0x00389A01 File Offset: 0x00387E01
		// (set) Token: 0x060087D4 RID: 34772 RVA: 0x00389A09 File Offset: 0x00387E09
		public float TimeScale { get; set; }

		// Token: 0x17001DB2 RID: 7602
		// (get) Token: 0x060087D5 RID: 34773 RVA: 0x00389A12 File Offset: 0x00387E12
		// (set) Token: 0x060087D6 RID: 34774 RVA: 0x00389A1A File Offset: 0x00387E1A
		public float PrevTimeScale { get; private set; }

		// Token: 0x17001DB3 RID: 7603
		// (get) Token: 0x060087D7 RID: 34775 RVA: 0x00389A23 File Offset: 0x00387E23
		// (set) Token: 0x060087D8 RID: 34776 RVA: 0x00389A2B File Offset: 0x00387E2B
		public System.Action ClosedEvent { get; set; }

		// Token: 0x17001DB4 RID: 7604
		// (get) Token: 0x060087D9 RID: 34777 RVA: 0x00389A34 File Offset: 0x00387E34
		// (set) Token: 0x060087DA RID: 34778 RVA: 0x00389A41 File Offset: 0x00387E41
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

		// Token: 0x17001DB5 RID: 7605
		// (get) Token: 0x060087DB RID: 34779 RVA: 0x00389A61 File Offset: 0x00387E61
		// (set) Token: 0x060087DC RID: 34780 RVA: 0x00389A89 File Offset: 0x00387E89
		protected float CanvasAlpha
		{
			get
			{
				return (!(this._rootCanvas != null)) ? 0f : this._rootCanvas.alpha;
			}
			set
			{
				if (this._rootCanvas != null)
				{
					this._rootCanvas.alpha = value;
				}
			}
		}

		// Token: 0x17001DB6 RID: 7606
		// (get) Token: 0x060087DD RID: 34781 RVA: 0x00389AA8 File Offset: 0x00387EA8
		public bool InputEnabled
		{
			[CompilerGenerated]
			get
			{
				return base.EnabledInput && Singleton<Manager.Input>.Instance.FocusLevel == this._focusLevel;
			}
		}

		// Token: 0x17001DB7 RID: 7607
		// (get) Token: 0x060087DE RID: 34782 RVA: 0x00389ACC File Offset: 0x00387ECC
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
						this,
						this._listUI
					});
				}
				return result;
			}
		}

		// Token: 0x060087DF RID: 34783 RVA: 0x00389B04 File Offset: 0x00387F04
		protected override void Awake()
		{
			if (this._rootCanvas == null)
			{
				this._rootCanvas = base.GetComponent<CanvasGroup>();
			}
			if (this._rectTransform == null)
			{
				this._rectTransform = base.GetComponent<RectTransform>();
			}
			if (this._listUI == null)
			{
				this._listUI = base.GetComponentInChildren<TutorialGroupListUI>(true);
			}
			if (this._loadingImageUI == null)
			{
				this._loadingImageUI = base.GetComponentInChildren<TutorialLoadingImageUI>(true);
			}
		}

		// Token: 0x060087E0 RID: 34784 RVA: 0x00389B88 File Offset: 0x00387F88
		protected override void OnBeforeStart()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			ActionIDDownCommand actionIDDownCommand = new ActionIDDownCommand
			{
				ActionID = ActionID.Cancel
			};
			actionIDDownCommand.TriggerEvent.AddListener(delegate()
			{
				if (this.OpenGroupEnabled)
				{
					this.DoClose();
				}
			});
			this._actionCommands.Add(actionIDDownCommand);
			KeyCodeDownCommand keyCodeDownCommand = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Mouse1
			};
			keyCodeDownCommand.TriggerEvent.AddListener(delegate()
			{
				if (this.OpenGroupEnabled)
				{
					this.DoClose();
				}
			});
			this._keyCommands.Add(keyCodeDownCommand);
			(from _ in this._rightButton.OnClickAsObservable()
			where this.InputEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this.PageMove(1);
			});
			(from _ in this._leftButton.OnClickAsObservable()
			where this.InputEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this.PageMove(-1);
			});
			(from _ in this._closeButton.OnClickAsObservable()
			where this.InputEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this.DoClose();
			});
		}

		// Token: 0x060087E1 RID: 34785 RVA: 0x00389CA0 File Offset: 0x003880A0
		public void DoClose()
		{
			this.IsActiveControl = false;
		}

		// Token: 0x060087E2 RID: 34786 RVA: 0x00389CAC File Offset: 0x003880AC
		protected override void OnAfterStart()
		{
			if (this._canvasGroup.blocksRaycasts)
			{
				this._canvasGroup.blocksRaycasts = false;
			}
			if (this._canvasGroup.interactable)
			{
				this._canvasGroup.interactable = false;
			}
			this.CanvasAlpha = 0f;
			this.SetActive(this._canvasGroup, false);
			IEnumerator coroutine = this.CreateElements();
			Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>().AddTo(this);
		}

		// Token: 0x060087E3 RID: 34787 RVA: 0x00389D3C File Offset: 0x0038813C
		private IEnumerator CreateElements()
		{
			if (this._initialize)
			{
				yield break;
			}
			this._initialize = true;
			while (!Singleton<Manager.Resources>.IsInstance())
			{
				yield return null;
			}
			while (Singleton<Manager.Resources>.Instance.PopupInfo.TutorialPrefabTable.IsNullOrEmpty<int, UnityEx.ValueTuple<string, GameObject[]>>())
			{
				yield return null;
			}
			ReadOnlyDictionary<int, UnityEx.ValueTuple<string, GameObject[]>> prefabTable = Singleton<Manager.Resources>.Instance.PopupInfo.TutorialPrefabTable;
			foreach (KeyValuePair<int, UnityEx.ValueTuple<string, GameObject[]>> keyValuePair in prefabTable)
			{
				int key = keyValuePair.Key;
				Transform transform = new GameObject(string.Format("Group [{0:00}]", key), new Type[]
				{
					typeof(CanvasGroup)
				}).transform;
				transform.SetParent(this._elementRoot, false);
				CanvasGroup i = (transform != null) ? transform.GetOrAddComponent<CanvasGroup>() : null;
				this.ElementRootTable[key] = new AIProject.Animal.Tuple<int, CanvasGroup, IDisposable>(key, i, null);
				List<TutorialUIElement> list = new List<TutorialUIElement>();
				if (!keyValuePair.Value.Item2.IsNullOrEmpty<GameObject>())
				{
					int maxPageNum = keyValuePair.Value.Item2.Length;
					int num = 0;
					foreach (GameObject gameObject in keyValuePair.Value.Item2)
					{
						if (!(gameObject == null))
						{
							GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, transform, false);
							TutorialUIElement component = gameObject2.GetComponent<TutorialUIElement>();
							if (!(component == null))
							{
								num = (component.MyPageNum = num + 1);
								component.MaxPageNum = maxPageNum;
								component.CloseForced();
								list.Add(component);
							}
						}
					}
				}
				List<TutorialUIElement> list2;
				if (this.ElementTable.TryGetValue(key, out list2) && !list2.IsNullOrEmpty<TutorialUIElement>())
				{
					foreach (TutorialUIElement tutorialUIElement in list2)
					{
						if (tutorialUIElement != null && tutorialUIElement.gameObject != null)
						{
							UnityEngine.Object.Destroy(tutorialUIElement.gameObject);
						}
					}
				}
				this.ElementTable[key] = list;
			}
			yield return null;
			yield break;
		}

		// Token: 0x060087E4 RID: 34788 RVA: 0x00389D58 File Offset: 0x00388158
		private void SetActiveControl(bool active)
		{
			IEnumerator coroutine = (!active) ? this.CloseCoroutine() : this.OpenCoroutine();
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).TakeUntilDestroy(this).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
				if (global::Debug.isDebugBuild)
				{
				}
			});
		}

		// Token: 0x060087E5 RID: 34789 RVA: 0x00389DFD File Offset: 0x003881FD
		public void SetCondition(int index, bool openGroup = false)
		{
			this.OpenElementNumber = index;
			this.OpenGroupEnabled = openGroup;
		}

		// Token: 0x060087E6 RID: 34790 RVA: 0x00389E0D File Offset: 0x0038820D
		public void SetCondition(Popup.Tutorial.Type type, bool openGroup = false)
		{
			this.OpenElementNumber = (int)type;
			this.OpenGroupEnabled = openGroup;
		}

		// Token: 0x17001DB8 RID: 7608
		// (get) Token: 0x060087E7 RID: 34791 RVA: 0x00389E1D File Offset: 0x0038821D
		// (set) Token: 0x060087E8 RID: 34792 RVA: 0x00389E2A File Offset: 0x0038822A
		public bool BlockRaycastEnabled
		{
			get
			{
				return this._backgroundCanvas.blocksRaycasts;
			}
			set
			{
				this._backgroundCanvas.blocksRaycasts = value;
			}
		}

		// Token: 0x060087E9 RID: 34793 RVA: 0x00389E38 File Offset: 0x00388238
		private IEnumerator OpenCoroutine()
		{
			this.SetActive(this._canvasGroup, true);
			this.AllClose();
			this.PrevTimeScale = Time.timeScale;
			Time.timeScale = this.TimeScale;
			Transform transform = this._rightButton.transform;
			Vector3 vector = Vector3.one;
			this._closeButton.transform.localScale = vector;
			vector = vector;
			this._leftButton.transform.localScale = vector;
			transform.localScale = vector;
			this._prevCommandAcceptionState = MapUIContainer.CommandLabel.Acception;
			if (this._prevCommandAcceptionState != CommandLabel.AcceptionState.None)
			{
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			}
			this.OpenIndex = 0;
			this.SetupElement(this.OpenElementNumber);
			this.SetupGroupListUI(this.OpenElementNumber);
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			this._prevPlayerScheduledInteractionState = player.CurrentInteractionState;
			if (this._prevPlayerScheduledInteractionState)
			{
				player.SetScheduledInteractionState(false);
				player.ReleaseInteraction();
			}
			Manager.Input input = Singleton<Manager.Input>.Instance;
			this._prevInputState = input.State;
			if (this._prevInputState != Manager.Input.ValidType.UI)
			{
				input.ReserveState(Manager.Input.ValidType.UI);
				input.SetupState();
			}
			this._prevMenuUIBehaviour = input.MenuElements;
			input.FocusLevel++;
			this.SetAllFocusLevel(input.FocusLevel);
			input.MenuElements = this.MenuUIElements;
			this.BlockRaycastEnabled = true;
			if (!this._canvasGroup.blocksRaycasts)
			{
				this._canvasGroup.blocksRaycasts = true;
			}
			if (this._canvasGroup.interactable)
			{
				this._canvasGroup.interactable = false;
			}
			float startAlpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).TakeUntilDestroy(this).Publish<TimeInterval<float>>();
			stream.Connect();
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			this._canvasGroup.interactable = true;
			this.SetAllEnabledInput(true);
			base.EnabledInput = true;
			yield break;
		}

		// Token: 0x060087EA RID: 34794 RVA: 0x00389E54 File Offset: 0x00388254
		private IEnumerator CloseCoroutine()
		{
			this.PlaySE(SoundPack.SystemSE.Cancel);
			this.SetAllEnabledInput(false);
			Manager.Input input = Singleton<Manager.Input>.Instance;
			input.ClearMenuElements();
			this._canvasGroup.interactable = false;
			bool prevDiffInputState = this._prevInputState != input.State;
			if (prevDiffInputState)
			{
				input.ReserveState(this._prevInputState);
			}
			float startAlpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).TakeUntilDestroy(this).Publish<TimeInterval<float>>();
			stream.Connect();
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			if (this._prevPlayerScheduledInteractionState)
			{
				player.SetScheduledInteractionState(true);
				player.ReleaseInteraction();
			}
			if (prevDiffInputState)
			{
				input.SetupState();
			}
			input.FocusLevel--;
			input.MenuElements = this._prevMenuUIBehaviour;
			this._canvasGroup.blocksRaycasts = false;
			this.SetActive(this._canvasGroup, false);
			this.BlockRaycastEnabled = false;
			Time.timeScale = this.PrevTimeScale;
			if (this._prevCommandAcceptionState != MapUIContainer.CommandLabel.Acception)
			{
				MapUIContainer.SetCommandLabelAcception(this._prevCommandAcceptionState);
			}
			this.SetActive(this._listUI, false);
			this.AllClose();
			System.Action closedEvent = this.ClosedEvent;
			if (closedEvent != null)
			{
				closedEvent();
			}
			this.ClosedEvent = null;
			yield break;
		}

		// Token: 0x060087EB RID: 34795 RVA: 0x00389E70 File Offset: 0x00388270
		private void SetupElement(int openGroup)
		{
			bool flag = this.CurrentElementRoot == null;
			if (this.CurrentElementRoot != null && this.CurrentElementRoot.Item1 != openGroup)
			{
				this.FadeOutRoot(this.CurrentElementRoot);
				this.CurrentElementRoot = null;
			}
			List<TutorialUIElement> currentElementList;
			if (!this.ElementTable.TryGetValue(openGroup, out currentElementList))
			{
				this.SetActive(this._rightButton, false);
				this.SetActive(this._leftButton, false);
				this.SetActive(this._closeButton, !this.OpenGroupEnabled);
				return;
			}
			this.ElementRootTable.TryGetValue(openGroup, out this.CurrentElementRoot);
			if (this.CurrentElementRoot != null)
			{
				if (flag)
				{
					IDisposable item = this.CurrentElementRoot.Item3;
					if (item != null)
					{
						item.Dispose();
					}
					this.CurrentElementRoot.Item2.alpha = 1f;
				}
				else
				{
					this.FadeInRoot(this.CurrentElementRoot);
				}
				CanvasGroup item2 = this.CurrentElementRoot.Item2;
				if (item2 != null)
				{
					Transform transform = item2.transform;
					if (transform != null)
					{
						transform.SetAsLastSibling();
					}
				}
			}
			this._currentElementList = currentElementList;
			if (!this._currentElementList.IsNullOrEmpty<TutorialUIElement>())
			{
				for (int i = 0; i < this._currentElementList.Count; i++)
				{
					TutorialUIElement element = this._currentElementList.GetElement(i);
					if (!(element == null))
					{
						element.CanvasAlpha = ((i != 0 || flag) ? 0f : 1f);
						if (element.BackImage != null)
						{
							Color color = element.BackImage.color;
							color.a = (float)this._elementBackImageAlpha / 255f;
							element.BackImage.color = color;
						}
					}
				}
			}
			this.PageMove(0);
		}

		// Token: 0x060087EC RID: 34796 RVA: 0x0038A03C File Offset: 0x0038843C
		private void SetupGroupListUI(int groupID)
		{
			if (this.OpenGroupEnabled)
			{
				this.SetActive(this._listUI, true);
				this._listUI.RefreshElements();
				this._listUI.SelectButton(groupID);
				this._loadingImageUI.PageIndex = 0;
			}
			else
			{
				this.SetActive(this._listUI, false);
			}
		}

		// Token: 0x060087ED RID: 34797 RVA: 0x0038A098 File Offset: 0x00388498
		private void FadeInRoot(AIProject.Animal.Tuple<int, CanvasGroup, IDisposable> root)
		{
			if (root == null)
			{
				return;
			}
			IDisposable item = root.Item3;
			if (item != null)
			{
				item.Dispose();
			}
			float startAlpha = root.Item2.alpha;
			root.Item3 = ObservableEasing.Linear(this._groupFadeTime, true).FrameTimeInterval(true).TakeUntilDestroy(root.Item2).Subscribe(delegate(TimeInterval<float> x)
			{
				root.Item2.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
		}

		// Token: 0x060087EE RID: 34798 RVA: 0x0038A12C File Offset: 0x0038852C
		private void FadeOutRoot(AIProject.Animal.Tuple<int, CanvasGroup, IDisposable> root)
		{
			if (root == null)
			{
				return;
			}
			IDisposable item = root.Item3;
			if (item != null)
			{
				item.Dispose();
			}
			float startAlpha = root.Item2.alpha;
			root.Item3 = ObservableEasing.Linear(this._groupFadeTime, true).FrameTimeInterval(true).TakeUntilDestroy(root.Item2).Subscribe(delegate(TimeInterval<float> x)
			{
				root.Item2.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
		}

		// Token: 0x060087EF RID: 34799 RVA: 0x0038A1C0 File Offset: 0x003885C0
		private void CloseAllRoot()
		{
			if (!this.ElementRootTable.IsNullOrEmpty<int, AIProject.Animal.Tuple<int, CanvasGroup, IDisposable>>())
			{
				foreach (KeyValuePair<int, AIProject.Animal.Tuple<int, CanvasGroup, IDisposable>> keyValuePair in this.ElementRootTable)
				{
					AIProject.Animal.Tuple<int, CanvasGroup, IDisposable> value = keyValuePair.Value;
					IDisposable item = value.Item3;
					if (item != null)
					{
						item.Dispose();
					}
					if (value.Item2 != null)
					{
						value.Item2.alpha = 0f;
					}
				}
			}
			this.CurrentElementRoot = null;
		}

		// Token: 0x060087F0 RID: 34800 RVA: 0x0038A26C File Offset: 0x0038866C
		private void AllClose()
		{
			this.CloseAllRoot();
			foreach (KeyValuePair<int, List<TutorialUIElement>> keyValuePair in this.ElementTable)
			{
				if (!keyValuePair.Value.IsNullOrEmpty<TutorialUIElement>())
				{
					foreach (TutorialUIElement tutorialUIElement in keyValuePair.Value)
					{
						tutorialUIElement.CloseForced();
					}
				}
			}
			this._currentElement = null;
		}

		// Token: 0x060087F1 RID: 34801 RVA: 0x0038A330 File Offset: 0x00388730
		private void PageMove(int moveIndex)
		{
			int openIndex = this.OpenIndex;
			int num = this.OpenIndex + moveIndex;
			List<TutorialUIElement> currentElementList = this._currentElementList;
			if (!this.ListRange<TutorialUIElement>(currentElementList, num))
			{
				return;
			}
			this.OpenIndex = num;
			if (openIndex == this.OpenIndex)
			{
				if (this._currentElement != null)
				{
					this._currentElement.Close(this._pageFadeTime, 0f);
				}
				this._currentElement = currentElementList.GetElement(this.OpenIndex);
				if (this._currentElement != null)
				{
					this._currentElement.Open(this._pageFadeTime, 0f);
				}
			}
			else
			{
				this.PlaySE(SoundPack.SystemSE.Page);
				float moveX = this._pageMoveX * Mathf.Sign((float)moveIndex);
				if (this._currentElement != null)
				{
					this._currentElement.Close(this._pageFadeTime, moveX);
				}
				this._currentElement = currentElementList.GetElement(this.OpenIndex);
				if (this._currentElement != null)
				{
					this._currentElement.Open(this._pageFadeTime, moveX);
				}
			}
			bool flag = this.ListRange<TutorialUIElement>(currentElementList, this.OpenIndex - 1);
			bool flag2 = this.ListRange<TutorialUIElement>(currentElementList, this.OpenIndex + 1);
			if (!flag && !flag2)
			{
				this.SetActive(this._rightButton, false);
				this.SetActive(this._leftButton, false);
				this.SetActive(this._closeButton, !this.OpenGroupEnabled);
			}
			else if (flag && flag2)
			{
				this.SetActive(this._rightButton, true);
				this.SetActive(this._leftButton, true);
				this.SetActive(this._closeButton, false);
			}
			else if (flag2)
			{
				this.SetActive(this._rightButton, true);
				this.SetActive(this._leftButton, false);
				this.SetActive(this._closeButton, false);
			}
			else if (flag)
			{
				this.SetActive(this._rightButton, false);
				this.SetActive(this._leftButton, true);
				this.SetActive(this._closeButton, !this.OpenGroupEnabled);
			}
		}

		// Token: 0x060087F2 RID: 34802 RVA: 0x0038A540 File Offset: 0x00388940
		public void ChangeUIGroup(int groupIndex)
		{
			if (this.OpenElementNumber == groupIndex)
			{
				return;
			}
			this.OpenIndex = 0;
			this.OpenElementNumber = groupIndex;
			this.SetupElement(groupIndex);
		}

		// Token: 0x060087F3 RID: 34803 RVA: 0x0038A574 File Offset: 0x00388974
		private void SetAllEnabledInput(bool active)
		{
			if (!this.MenuUIElements.IsNullOrEmpty<MenuUIBehaviour>())
			{
				foreach (MenuUIBehaviour menuUIBehaviour in this.MenuUIElements)
				{
					if (menuUIBehaviour != null && menuUIBehaviour.EnabledInput != active)
					{
						menuUIBehaviour.EnabledInput = active;
					}
				}
			}
		}

		// Token: 0x060087F4 RID: 34804 RVA: 0x0038A5D0 File Offset: 0x003889D0
		private void SetAllFocusLevel(int level)
		{
			if (!this.MenuUIElements.IsNullOrEmpty<MenuUIBehaviour>())
			{
				foreach (MenuUIBehaviour menuUIBehaviour in this.MenuUIElements)
				{
					if (menuUIBehaviour != null)
					{
						menuUIBehaviour.SetFocusLevel(level);
					}
				}
			}
		}

		// Token: 0x060087F5 RID: 34805 RVA: 0x0038A620 File Offset: 0x00388A20
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

		// Token: 0x060087F6 RID: 34806 RVA: 0x0038A657 File Offset: 0x00388A57
		private bool ListRange<T>(List<T> list, int idx)
		{
			return !list.IsNullOrEmpty<T>() && 0 <= idx && idx < list.Count;
		}

		// Token: 0x060087F7 RID: 34807 RVA: 0x0038A679 File Offset: 0x00388A79
		private bool ArrayRange<T>(T[] array, int idx)
		{
			return !array.IsNullOrEmpty<T>() && 0 <= idx && idx < array.Length;
		}

		// Token: 0x060087F8 RID: 34808 RVA: 0x0038A698 File Offset: 0x00388A98
		private void SetActive(GameObject obj, bool active)
		{
			if (obj == null)
			{
				return;
			}
			if (obj.activeSelf != active)
			{
				obj.SetActive(active);
			}
		}

		// Token: 0x060087F9 RID: 34809 RVA: 0x0038A6BA File Offset: 0x00388ABA
		private void SetActive(Component com, bool active)
		{
			if (com == null || com.gameObject == null)
			{
				return;
			}
			if (com.gameObject.activeSelf != active)
			{
				com.gameObject.SetActive(active);
			}
		}

		// Token: 0x04006E32 RID: 28210
		[SerializeField]
		private CanvasGroup _rootCanvas;

		// Token: 0x04006E33 RID: 28211
		[SerializeField]
		private RectTransform _rectTransform;

		// Token: 0x04006E34 RID: 28212
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006E35 RID: 28213
		[SerializeField]
		private CanvasGroup _backgroundCanvas;

		// Token: 0x04006E36 RID: 28214
		[SerializeField]
		private Transform _elementRoot;

		// Token: 0x04006E37 RID: 28215
		private TutorialUIElement _currentElement;

		// Token: 0x04006E38 RID: 28216
		[SerializeField]
		private int _elementBackImageAlpha = 245;

		// Token: 0x04006E39 RID: 28217
		[SerializeField]
		private float _pageFadeTime = 0.5f;

		// Token: 0x04006E3A RID: 28218
		[SerializeField]
		private float _pageMoveX = 20f;

		// Token: 0x04006E3B RID: 28219
		[SerializeField]
		private float _groupFadeTime = 0.5f;

		// Token: 0x04006E3C RID: 28220
		[SerializeField]
		private Button _leftButton;

		// Token: 0x04006E3D RID: 28221
		[SerializeField]
		private Button _rightButton;

		// Token: 0x04006E3E RID: 28222
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04006E3F RID: 28223
		[SerializeField]
		private TutorialGroupListUI _listUI;

		// Token: 0x04006E40 RID: 28224
		[SerializeField]
		private TutorialLoadingImageUI _loadingImageUI;

		// Token: 0x04006E47 RID: 28231
		private MenuUIBehaviour[] uiElements;

		// Token: 0x04006E48 RID: 28232
		private Dictionary<int, List<TutorialUIElement>> ElementTable = new Dictionary<int, List<TutorialUIElement>>();

		// Token: 0x04006E49 RID: 28233
		private AIProject.Animal.Tuple<int, CanvasGroup, IDisposable> CurrentElementRoot;

		// Token: 0x04006E4A RID: 28234
		private Dictionary<int, AIProject.Animal.Tuple<int, CanvasGroup, IDisposable>> ElementRootTable = new Dictionary<int, AIProject.Animal.Tuple<int, CanvasGroup, IDisposable>>();

		// Token: 0x04006E4B RID: 28235
		private List<TutorialUIElement> _currentElementList;

		// Token: 0x04006E4C RID: 28236
		private Dictionary<int, CanvasGroup> _elementRootTable = new Dictionary<int, CanvasGroup>();

		// Token: 0x04006E4D RID: 28237
		private bool _initialize;

		// Token: 0x04006E4E RID: 28238
		private IDisposable _fadeDisposable;

		// Token: 0x04006E4F RID: 28239
		private CommandLabel.AcceptionState _prevCommandAcceptionState = CommandLabel.AcceptionState.None;

		// Token: 0x04006E50 RID: 28240
		private bool _prevPlayerScheduledInteractionState;

		// Token: 0x04006E51 RID: 28241
		private Manager.Input.ValidType _prevInputState;

		// Token: 0x04006E52 RID: 28242
		private MenuUIBehaviour[] _prevMenuUIBehaviour;
	}
}
