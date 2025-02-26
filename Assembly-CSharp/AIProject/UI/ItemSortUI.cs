using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.ColorDefine;
using AIProject.UI.Viewer;
using ReMotion;
using Sirenix.OdinInspector;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E95 RID: 3733
	public class ItemSortUI : MenuUIBehaviour
	{
		// Token: 0x17001794 RID: 6036
		// (get) Token: 0x0600780C RID: 30732 RVA: 0x0032A670 File Offset: 0x00328A70
		public PlaySE playSE { get; } = new PlaySE(false);

		// Token: 0x140000CB RID: 203
		// (add) Token: 0x0600780D RID: 30733 RVA: 0x0032A678 File Offset: 0x00328A78
		// (remove) Token: 0x0600780E RID: 30734 RVA: 0x0032A6B0 File Offset: 0x00328AB0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int> TypeChanged;

		// Token: 0x0600780F RID: 30735 RVA: 0x0032A6E8 File Offset: 0x00328AE8
		public void SetDefault()
		{
			foreach (Toggle toggle in this._toggles)
			{
				toggle.isOn = false;
			}
			this._toggles[0].isOn = false;
		}

		// Token: 0x140000CC RID: 204
		// (add) Token: 0x06007810 RID: 30736 RVA: 0x0032A72C File Offset: 0x00328B2C
		// (remove) Token: 0x06007811 RID: 30737 RVA: 0x0032A764 File Offset: 0x00328B64
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action OnEntered;

		// Token: 0x17001795 RID: 6037
		// (get) Token: 0x06007812 RID: 30738 RVA: 0x0032A79A File Offset: 0x00328B9A
		// (set) Token: 0x06007813 RID: 30739 RVA: 0x0032A7A2 File Offset: 0x00328BA2
		public UnityEvent OnSubmit { get; private set; } = new UnityEvent();

		// Token: 0x17001796 RID: 6038
		// (get) Token: 0x06007814 RID: 30740 RVA: 0x0032A7AB File Offset: 0x00328BAB
		// (set) Token: 0x06007815 RID: 30741 RVA: 0x0032A7B3 File Offset: 0x00328BB3
		public UnityEvent OnCancel { get; private set; } = new UnityEvent();

		// Token: 0x06007816 RID: 30742 RVA: 0x0032A7BC File Offset: 0x00328BBC
		public void Open()
		{
			this._isOpen.Value = true;
		}

		// Token: 0x06007817 RID: 30743 RVA: 0x0032A7CA File Offset: 0x00328BCA
		public void Close()
		{
			this._isOpen.Value = false;
		}

		// Token: 0x17001797 RID: 6039
		// (get) Token: 0x06007818 RID: 30744 RVA: 0x0032A7D8 File Offset: 0x00328BD8
		public bool isOpen
		{
			[CompilerGenerated]
			get
			{
				return this._isOpen.Value;
			}
		}

		// Token: 0x17001798 RID: 6040
		// (get) Token: 0x06007819 RID: 30745 RVA: 0x0032A7E5 File Offset: 0x00328BE5
		// (set) Token: 0x0600781A RID: 30746 RVA: 0x0032A7ED File Offset: 0x00328BED
		private Toggle[] _toggles { get; set; }

		// Token: 0x17001799 RID: 6041
		// (get) Token: 0x0600781B RID: 30747 RVA: 0x0032A7F6 File Offset: 0x00328BF6
		private RectTransform closeSize
		{
			get
			{
				return this.GetCacheObject(ref this._closeSize, () => (this._close.transform.childCount < 1) ? this._close.GetComponent<RectTransform>() : (this._close.transform.GetChild(0) as RectTransform));
			}
		}

		// Token: 0x1700179A RID: 6042
		// (get) Token: 0x0600781C RID: 30748 RVA: 0x0032A810 File Offset: 0x00328C10
		private CanvasGroup cursorCG
		{
			[CompilerGenerated]
			get
			{
				return this.GetCacheObject(ref this._cursorCG, () => this._cursor.GetComponentCache(ref this._cursorCG));
			}
		}

		// Token: 0x1700179B RID: 6043
		// (get) Token: 0x0600781D RID: 30749 RVA: 0x0032A82A File Offset: 0x00328C2A
		private CanvasGroup sortPanelCG
		{
			[CompilerGenerated]
			get
			{
				return this.GetComponentCache(ref this._sortPanelCG);
			}
		}

		// Token: 0x0600781E RID: 30750 RVA: 0x0032A838 File Offset: 0x00328C38
		private void OpenCloseAnimation(bool isOpen)
		{
			this.sortPanelCG.blocksRaycasts = isOpen;
			if (this._subscriber != null)
			{
				this._subscriber.Dispose();
			}
			this._subscriber = (from x in ObservableEasing.EaseOutQuint(0.3f, true).FrameTimeInterval(true)
			select (!isOpen) ? (1f - x.Value) : x.Value).Subscribe(delegate(float alpha)
			{
				this.sortPanelCG.alpha = alpha;
			}, delegate(Exception ex)
			{
			});
		}

		// Token: 0x0600781F RID: 30751 RVA: 0x0032A8D8 File Offset: 0x00328CD8
		protected override void Start()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this._toggles = base.GetComponentsInChildren<Toggle>(true);
			this._isOpen.Subscribe(delegate(bool isOn)
			{
				if (!isOn)
				{
					this._selectedID.Value = -1;
					this.playSE.Play(SoundPack.SystemSE.Cancel);
				}
				this.OpenCloseAnimation(isOn);
			});
			this._selectedID.Subscribe(delegate(int x)
			{
				Toggle element = this._toggles.GetElement(x);
				if (element == null)
				{
					this._selectedOptionInstance = null;
				}
				else
				{
					this._selectedOptionInstance = element;
				}
			});
			this.cursorCG.alpha = 1f;
			this._cursor.enabled = false;
			ColorBlock colors = this._close.colors;
			colors.highlightedColor = Define.Get(Colors.Green);
			this._close.colors = colors;
			this._toggles.BindToEnter(true, this._cursor).AddTo(this);
			this._toggles.BindToGroup(delegate(int type)
			{
				this._sortType.Value = type;
			}).AddTo(this);
			this._sortType.Subscribe(delegate(int type)
			{
				if (this.TypeChanged != null)
				{
					this.TypeChanged(type);
				}
				this.playSE.Play(SoundPack.SystemSE.OK_S);
			});
			this._close.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnInputCancel();
			});
			Image component = base.GetComponent<Image>();
			if (component != null)
			{
				(from _ in component.OnPointerEnterAsObservable()
				where this._isOpen.Value
				select _).Subscribe(delegate(PointerEventData _)
				{
					if (this.OnEntered != null)
					{
						this.OnEntered();
					}
				});
			}
			using (var enumerator = new Selectable[]
			{
				this._close
			}.Concat(this._toggles).Select((Selectable o, int index) => new
			{
				o = o,
				index = index - 1
			}).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					<>__AnonType23<Selectable, int> item = enumerator.Current;
					ItemSortUI $this = this;
					(from _ in item.o.OnPointerEnterAsObservable()
					select item.index).Subscribe(delegate(int index)
					{
						$this._selectedID.Value = index;
						if ($this.OnEntered != null)
						{
							$this.OnEntered();
						}
					});
				}
			}
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
			base.Start();
			this.playSE.use = true;
		}

		// Token: 0x06007820 RID: 30752 RVA: 0x0032AB94 File Offset: 0x00328F94
		private void OnInputSubmit()
		{
			if (!this._isOpen.Value)
			{
				return;
			}
			if (this._selectedOptionInstance != null)
			{
				this._selectedOptionInstance.OnSubmit(null);
			}
			else
			{
				this._close.OnSubmit(null);
			}
			UnityEvent onSubmit = this.OnSubmit;
			if (onSubmit != null)
			{
				onSubmit.Invoke();
			}
		}

		// Token: 0x06007821 RID: 30753 RVA: 0x0032ABF4 File Offset: 0x00328FF4
		private void OnInputCancel()
		{
			if (!this._isOpen.Value)
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

		// Token: 0x0400613D RID: 24893
		private BoolReactiveProperty _isOpen = new BoolReactiveProperty(false);

		// Token: 0x0400613E RID: 24894
		private Toggle _selectedOptionInstance;

		// Token: 0x0400613F RID: 24895
		private IntReactiveProperty _sortType = new IntReactiveProperty(0);

		// Token: 0x04006140 RID: 24896
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private IntReactiveProperty _selectedID = new IntReactiveProperty(0);

		// Token: 0x04006141 RID: 24897
		[SerializeField]
		private Image _cursor;

		// Token: 0x04006142 RID: 24898
		[SerializeField]
		private Button _close;

		// Token: 0x04006144 RID: 24900
		private RectTransform _closeSize;

		// Token: 0x04006145 RID: 24901
		private CanvasGroup _cursorCG;

		// Token: 0x04006146 RID: 24902
		private CanvasGroup _sortPanelCG;

		// Token: 0x04006147 RID: 24903
		private IDisposable _subscriber;

		// Token: 0x04006148 RID: 24904
		private Vector3 _velocity = Vector3.zero;

		// Token: 0x04006149 RID: 24905
		private float _alphaVelocity;
	}
}
