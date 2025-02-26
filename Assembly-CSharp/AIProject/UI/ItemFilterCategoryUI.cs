using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.UI.Viewer;
using Manager;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI
{
	// Token: 0x02000E86 RID: 3718
	public class ItemFilterCategoryUI : MenuUIBehaviour
	{
		// Token: 0x17001744 RID: 5956
		// (get) Token: 0x060076FC RID: 30460 RVA: 0x00326F5E File Offset: 0x0032535E
		public PlaySE playSE { get; } = new PlaySE(false);

		// Token: 0x17001745 RID: 5957
		// (get) Token: 0x060076FD RID: 30461 RVA: 0x00326F66 File Offset: 0x00325366
		// (set) Token: 0x060076FE RID: 30462 RVA: 0x00326F73 File Offset: 0x00325373
		public int SelectedID
		{
			get
			{
				return this._selectedID.Value;
			}
			set
			{
				this._selectedID.Value = value;
			}
		}

		// Token: 0x17001746 RID: 5958
		// (get) Token: 0x060076FF RID: 30463 RVA: 0x00326F81 File Offset: 0x00325381
		// (set) Token: 0x06007700 RID: 30464 RVA: 0x00326F8E File Offset: 0x0032538E
		public int CategoryID
		{
			get
			{
				return this._categoryID.Value;
			}
			set
			{
				this._categoryID.Value = value;
			}
		}

		// Token: 0x17001747 RID: 5959
		// (get) Token: 0x06007701 RID: 30465 RVA: 0x00326F9C File Offset: 0x0032539C
		public CanvasGroup CursorCanvasGroup
		{
			[CompilerGenerated]
			get
			{
				CanvasGroup result;
				if ((result = this._cursorCanvasGroup) == null)
				{
					result = (this._cursorCanvasGroup = this._cursorFrame.GetComponent<CanvasGroup>());
				}
				return result;
			}
		}

		// Token: 0x17001748 RID: 5960
		// (get) Token: 0x06007702 RID: 30466 RVA: 0x00326FCA File Offset: 0x003253CA
		public Button leftButton
		{
			[CompilerGenerated]
			get
			{
				return this._leftButton;
			}
		}

		// Token: 0x17001749 RID: 5961
		// (get) Token: 0x06007703 RID: 30467 RVA: 0x00326FD2 File Offset: 0x003253D2
		public Button rightButton
		{
			[CompilerGenerated]
			get
			{
				return this._rightButton;
			}
		}

		// Token: 0x1700174A RID: 5962
		// (get) Token: 0x06007704 RID: 30468 RVA: 0x00326FDA File Offset: 0x003253DA
		// (set) Token: 0x06007705 RID: 30469 RVA: 0x00326FE2 File Offset: 0x003253E2
		public bool useCursor
		{
			get
			{
				return this._useCursor;
			}
			set
			{
				this._useCursor = value;
			}
		}

		// Token: 0x1700174B RID: 5963
		// (get) Token: 0x06007706 RID: 30470 RVA: 0x00326FEB File Offset: 0x003253EB
		// (set) Token: 0x06007707 RID: 30471 RVA: 0x00326FF3 File Offset: 0x003253F3
		private bool _useCursor { get; set; }

		// Token: 0x1700174C RID: 5964
		// (get) Token: 0x06007708 RID: 30472 RVA: 0x00326FFC File Offset: 0x003253FC
		// (set) Token: 0x06007709 RID: 30473 RVA: 0x00327004 File Offset: 0x00325404
		public Button SelectedButton { get; private set; }

		// Token: 0x1700174D RID: 5965
		// (get) Token: 0x0600770A RID: 30474 RVA: 0x0032700D File Offset: 0x0032540D
		// (set) Token: 0x0600770B RID: 30475 RVA: 0x00327015 File Offset: 0x00325415
		public Button CurrentCategoryButton { get; private set; }

		// Token: 0x1700174E RID: 5966
		// (get) Token: 0x0600770C RID: 30476 RVA: 0x0032701E File Offset: 0x0032541E
		// (set) Token: 0x0600770D RID: 30477 RVA: 0x00327026 File Offset: 0x00325426
		public Action OnEntered { get; set; }

		// Token: 0x1700174F RID: 5967
		// (get) Token: 0x0600770E RID: 30478 RVA: 0x0032702F File Offset: 0x0032542F
		// (set) Token: 0x0600770F RID: 30479 RVA: 0x00327037 File Offset: 0x00325437
		public UnityEvent OnSubmit { get; private set; } = new UnityEvent();

		// Token: 0x17001750 RID: 5968
		// (get) Token: 0x06007710 RID: 30480 RVA: 0x00327040 File Offset: 0x00325440
		// (set) Token: 0x06007711 RID: 30481 RVA: 0x00327048 File Offset: 0x00325448
		public UnityEvent OnCancel { get; private set; } = new UnityEvent();

		// Token: 0x17001751 RID: 5969
		// (get) Token: 0x06007712 RID: 30482 RVA: 0x00327051 File Offset: 0x00325451
		// (set) Token: 0x06007713 RID: 30483 RVA: 0x00327083 File Offset: 0x00325483
		private Vector2 scrollPos
		{
			get
			{
				return (!(this.scroll.content == null)) ? this.scroll.content.anchoredPosition : Vector2.zero;
			}
			set
			{
				if (this.scroll.content != null)
				{
					this.scroll.content.anchoredPosition = value;
				}
			}
		}

		// Token: 0x17001752 RID: 5970
		// (get) Token: 0x06007714 RID: 30484 RVA: 0x003270AC File Offset: 0x003254AC
		// (set) Token: 0x06007715 RID: 30485 RVA: 0x003270B4 File Offset: 0x003254B4
		private float layoutWidthSpacing { get; set; }

		// Token: 0x06007716 RID: 30486 RVA: 0x003270C0 File Offset: 0x003254C0
		public void SetSelectAndCategory(int value)
		{
			this.playSE.use = false;
			this._selectedID.SetValueAndForceNotify(value);
			this._categoryID.SetValueAndForceNotify(value);
			this.playSE.use = true;
			int num = this.Visibles.Select((int _, int i) => i).FirstOrDefault<int>();
			Vector2 scrollPos = this.scrollPos;
			scrollPos.x = (float)num * this.layoutWidthSpacing;
			this.scrollPos = scrollPos;
			this.ScrollNormalized();
		}

		// Token: 0x06007717 RID: 30487 RVA: 0x00327150 File Offset: 0x00325550
		public void Filter(params int[] visibles)
		{
			bool flag = !visibles.Any<int>();
			foreach (KeyValuePair<int, ItemFilterCategoryUI.LabelAndButton> keyValuePair in this.categorize)
			{
				keyValuePair.Value.Visible = (flag || visibles.Contains(keyValuePair.Key));
			}
			this.SetSelectAndCategory(this.Visibles.FirstOrDefault<int>());
		}

		// Token: 0x06007718 RID: 30488 RVA: 0x003271E4 File Offset: 0x003255E4
		public bool SetButtonListener(UnityAction<int> action)
		{
			if (!this.categorize.Any<KeyValuePair<int, ItemFilterCategoryUI.LabelAndButton>>())
			{
				return false;
			}
			using (Dictionary<int, ItemFilterCategoryUI.LabelAndButton>.Enumerator enumerator = this.categorize.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, ItemFilterCategoryUI.LabelAndButton> item = enumerator.Current;
					item.Value.button.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						action(item.Key);
					});
				}
			}
			return true;
		}

		// Token: 0x17001753 RID: 5971
		// (get) Token: 0x06007719 RID: 30489 RVA: 0x00327294 File Offset: 0x00325694
		public int[] Visibles
		{
			get
			{
				return (from v in this.categorize
				where v.Value.Visible
				select v.Key).ToArray<int>();
			}
		}

		// Token: 0x0600771A RID: 30490 RVA: 0x003272F0 File Offset: 0x003256F0
		private IEnumerator CreateCategoryIcon()
		{
			if (this._rootCategoryButton == null)
			{
				yield break;
			}
			while (!Singleton<Manager.Resources>.Instance.GameInfo.GetItemCategories().Any<int>())
			{
				yield return null;
			}
			Transform parent = this._rootCategoryButton.transform.parent;
			foreach (var <>__AnonType in (from v in Singleton<Manager.Resources>.Instance.itemIconTables.CategoryIcon
			where v.Key == 0 || (Singleton<Manager.Resources>.Instance.GameInfo.GetItemTable(v.Key) ?? new Dictionary<int, StuffItemInfo>()).Any<KeyValuePair<int, StuffItemInfo>>()
			orderby v.Key
			select v).Select(delegate(KeyValuePair<int, Tuple<string, Sprite>> item)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this._rootCategoryButton, parent, false);
				gameObject.SetActive(true);
				gameObject.GetComponent<Image>().sprite = item.Value.Item2;
				Button component = gameObject.GetComponent<Button>();
				return new
				{
					category = item.Key,
					x = new ItemFilterCategoryUI.LabelAndButton(item.Value.Item1, component)
				};
			}))
			{
				this.categorize[<>__AnonType.category] = <>__AnonType.x;
			}
			this._selectedID.Subscribe(delegate(int i)
			{
				ItemFilterCategoryUI.LabelAndButton labelAndButton;
				if (!this.categorize.TryGetValue(i, out labelAndButton))
				{
					return;
				}
				this.SelectedButton = labelAndButton.button;
			});
			this._categoryID.Subscribe(delegate(int i)
			{
				ItemFilterCategoryUI.LabelAndButton labelAndButton;
				if (!this.categorize.TryGetValue(i, out labelAndButton))
				{
					return;
				}
				this.CurrentCategoryButton = labelAndButton.button;
				this.categoryTitle.Value = labelAndButton.label;
				this.playSE.Play(SoundPack.SystemSE.OK_S);
			});
			if (this._CategoryLabel != null)
			{
				this.categoryTitle.SubscribeToText(this._CategoryLabel);
			}
			using (Dictionary<int, ItemFilterCategoryUI.LabelAndButton>.Enumerator enumerator2 = this.categorize.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					KeyValuePair<int, ItemFilterCategoryUI.LabelAndButton> item = enumerator2.Current;
					Button button = item.Value.button;
					button.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						this._categoryID.Value = item.Key;
					});
					PointerEnterTrigger orAddComponent = button.GetOrAddComponent<PointerEnterTrigger>();
					UITrigger.TriggerEvent triggerEvent = orAddComponent.Triggers.GetElement(0);
					if (triggerEvent == null)
					{
						triggerEvent = new UITrigger.TriggerEvent();
						orAddComponent.Triggers.Add(triggerEvent);
					}
					triggerEvent.AddListener(delegate(BaseEventData x)
					{
						this._selectedID.Value = item.Key;
						this._useCursor = true;
						Action onEntered = this.OnEntered;
						if (onEntered != null)
						{
							onEntered();
						}
					});
				}
			}
			this.playSE.use = true;
			yield break;
		}

		// Token: 0x0600771B RID: 30491 RVA: 0x0032730C File Offset: 0x0032570C
		protected override void Start()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			float num = 0f;
			if (this._rootCategoryButton != null)
			{
				num = this._rootCategoryButton.GetComponent<RectTransform>().rect.width;
			}
			float num2 = 0f;
			if (this.scroll.content != null)
			{
				HorizontalOrVerticalLayoutGroup component = this.scroll.content.GetComponent<HorizontalOrVerticalLayoutGroup>();
				if (component != null)
				{
					num2 = component.spacing;
				}
			}
			this.layoutWidthSpacing = num + num2;
			this.scroll.scrollSensitivity = -this.layoutWidthSpacing;
			base.StartCoroutine(this.CreateCategoryIcon());
			Observable.Merge<float>(new IObservable<float>[]
			{
				from __ in this._leftButton.OnClickAsObservable()
				select this.layoutWidthSpacing,
				this._rightButton.OnClickAsObservable().Select((Unit __) => -this.layoutWidthSpacing)
			}).Subscribe(delegate(float x)
			{
				Vector2 scrollPos = this.scrollPos;
				scrollPos.x += x;
				this.scrollPos = scrollPos;
				this.playSE.Play(SoundPack.SystemSE.OK_S);
			});
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
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
			ActionIDDownCommand actionIDDownCommand3 = new ActionIDDownCommand
			{
				ActionID = ActionID.MouseRight
			};
			actionIDDownCommand3.TriggerEvent.AddListener(delegate()
			{
				this.OnInputCancel();
			});
			this._actionCommands.Add(actionIDDownCommand3);
			ActionIDDownCommand actionIDDownCommand4 = new ActionIDDownCommand
			{
				ActionID = ActionID.LeftShoulder1
			};
			actionIDDownCommand4.TriggerEvent.AddListener(delegate()
			{
				this._useCursor = true;
				if (this._leftButton != null)
				{
					Button.ButtonClickedEvent onClick = this._leftButton.onClick;
					if (onClick != null)
					{
						onClick.Invoke();
					}
				}
				if (this._sortCursorFrame != null && this._sortCursorFrame.enabled)
				{
					this._sortCursorFrame.enabled = false;
				}
			});
			this._actionCommands.Add(actionIDDownCommand4);
			ActionIDDownCommand actionIDDownCommand5 = new ActionIDDownCommand
			{
				ActionID = ActionID.RightShoulder1
			};
			actionIDDownCommand5.TriggerEvent.AddListener(delegate()
			{
				this._useCursor = true;
				if (this._rightButton != null)
				{
					Button.ButtonClickedEvent onClick = this._rightButton.onClick;
					if (onClick != null)
					{
						onClick.Invoke();
					}
				}
				if (this._sortCursorFrame != null && this._sortCursorFrame.enabled)
				{
					this._sortCursorFrame.enabled = false;
				}
			});
			this._actionCommands.Add(actionIDDownCommand5);
			base.Start();
		}

		// Token: 0x0600771C RID: 30492 RVA: 0x00327570 File Offset: 0x00325970
		private void OnUpdate()
		{
			this.ScrollNormalized();
			bool flag = base.EnabledInput && base.FocusLevel == Singleton<Manager.Input>.Instance.FocusLevel && this._useCursor;
			float target = (float)((!flag || !(this.SelectedButton != null)) ? 0 : 1);
			this.CursorCanvasGroup.alpha = Smooth.Damp(this.CursorCanvasGroup.alpha, target, ref this._alphaVelocity, this._alphaAccelerationTime);
			if (this.SelectedButton != null)
			{
				CursorFrame.Set(this._cursorFrame.rectTransform, this.SelectedButton.GetComponent<RectTransform>(), ref this._velocity, null, new float?(0.025f));
			}
			if (this.CurrentCategoryButton != null)
			{
				CursorFrame.Set(this._selectedCursorFrame.rectTransform, this.CurrentCategoryButton.GetComponent<RectTransform>(), ref this._selectedVelocity, null, new float?(0.025f));
			}
		}

		// Token: 0x0600771D RID: 30493 RVA: 0x00327680 File Offset: 0x00325A80
		private void ScrollNormalized()
		{
			float num = this.scroll.horizontalNormalizedPosition;
			this._leftButton.interactable = (num > 0f);
			this._rightButton.interactable = (num < 1f);
			if ((double)num < 0.001)
			{
				num = 0f;
			}
			else if ((double)num > 0.999)
			{
				num = 1f;
			}
			this.scroll.horizontalNormalizedPosition = num;
		}

		// Token: 0x0600771E RID: 30494 RVA: 0x003276FC File Offset: 0x00325AFC
		private void OnInputSubmit()
		{
			UnityEvent onSubmit = this.OnSubmit;
			if (onSubmit != null)
			{
				onSubmit.Invoke();
			}
		}

		// Token: 0x0600771F RID: 30495 RVA: 0x00327712 File Offset: 0x00325B12
		private void OnInputCancel()
		{
			UnityEvent onCancel = this.OnCancel;
			if (onCancel != null)
			{
				onCancel.Invoke();
			}
		}

		// Token: 0x040060A6 RID: 24742
		[SerializeField]
		private ScrollRect scroll;

		// Token: 0x040060A7 RID: 24743
		[SerializeField]
		private GameObject _rootCategoryButton;

		// Token: 0x040060A8 RID: 24744
		private readonly Dictionary<int, ItemFilterCategoryUI.LabelAndButton> categorize = new Dictionary<int, ItemFilterCategoryUI.LabelAndButton>();

		// Token: 0x040060A9 RID: 24745
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private IntReactiveProperty _selectedID = new IntReactiveProperty(0);

		// Token: 0x040060AA RID: 24746
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private IntReactiveProperty _categoryID = new IntReactiveProperty(0);

		// Token: 0x040060AB RID: 24747
		[SerializeField]
		private Image _selectedCursorFrame;

		// Token: 0x040060AC RID: 24748
		private CanvasGroup _cursorCanvasGroup;

		// Token: 0x040060AD RID: 24749
		[SerializeField]
		private Image _cursorFrame;

		// Token: 0x040060AE RID: 24750
		[SerializeField]
		private Image _sortCursorFrame;

		// Token: 0x040060AF RID: 24751
		[SerializeField]
		private Text _CategoryLabel;

		// Token: 0x040060B0 RID: 24752
		[SerializeField]
		private Button _leftButton;

		// Token: 0x040060B1 RID: 24753
		[SerializeField]
		private Button _rightButton;

		// Token: 0x040060B8 RID: 24760
		private StringReactiveProperty categoryTitle = new StringReactiveProperty(string.Empty);

		// Token: 0x040060B9 RID: 24761
		private float _alphaVelocity;

		// Token: 0x040060BA RID: 24762
		private Vector3 _velocity = Vector3.zero;

		// Token: 0x040060BB RID: 24763
		private Vector3 _selectedVelocity = Vector3.zero;

		// Token: 0x02000E87 RID: 3719
		private class LabelAndButton
		{
			// Token: 0x0600772D RID: 30509 RVA: 0x00327883 File Offset: 0x00325C83
			public LabelAndButton(string label, Button button)
			{
				this.label = label;
				this.button = button;
				this.gameObject = button.gameObject;
			}

			// Token: 0x17001754 RID: 5972
			// (get) Token: 0x0600772E RID: 30510 RVA: 0x003278A5 File Offset: 0x00325CA5
			// (set) Token: 0x0600772F RID: 30511 RVA: 0x003278C9 File Offset: 0x00325CC9
			public bool Visible
			{
				get
				{
					return !(this.gameObject == null) && this.gameObject.activeSelf;
				}
				set
				{
					if (this.gameObject != null)
					{
						this.gameObject.SetActive(value);
					}
				}
			}

			// Token: 0x040060C0 RID: 24768
			public readonly string label;

			// Token: 0x040060C1 RID: 24769
			public readonly Button button;

			// Token: 0x040060C2 RID: 24770
			private readonly GameObject gameObject;
		}
	}
}
