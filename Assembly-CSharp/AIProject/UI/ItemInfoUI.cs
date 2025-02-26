using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using AIProject.UI.Viewer;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E8A RID: 3722
	public abstract class ItemInfoUI : MenuUIBehaviour
	{
		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x06007734 RID: 30516 RVA: 0x0031811C File Offset: 0x0031651C
		// (remove) Token: 0x06007735 RID: 30517 RVA: 0x00318154 File Offset: 0x00316554
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action OnSubmit;

		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x06007736 RID: 30518 RVA: 0x0031818C File Offset: 0x0031658C
		// (remove) Token: 0x06007737 RID: 30519 RVA: 0x003181C4 File Offset: 0x003165C4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action OnCancel;

		// Token: 0x06007738 RID: 30520 RVA: 0x003181FA File Offset: 0x003165FA
		public void OnSubmitForce()
		{
			this.OnInputSubmit();
		}

		// Token: 0x17001755 RID: 5973
		// (get) Token: 0x06007739 RID: 30521 RVA: 0x00318202 File Offset: 0x00316602
		public string ConfirmLabel
		{
			[CompilerGenerated]
			get
			{
				return this._confirmLabel;
			}
		}

		// Token: 0x17001756 RID: 5974
		// (get) Token: 0x0600773A RID: 30522 RVA: 0x0031820A File Offset: 0x0031660A
		public int Count
		{
			[CompilerGenerated]
			get
			{
				return this._countViewer.Count;
			}
		}

		// Token: 0x17001757 RID: 5975
		// (set) Token: 0x0600773B RID: 30523 RVA: 0x00318217 File Offset: 0x00316617
		public bool isCountViewerVisible
		{
			set
			{
				this._isCountViewerVisible.Value = value;
			}
		}

		// Token: 0x17001758 RID: 5976
		// (get) Token: 0x0600773C RID: 30524 RVA: 0x00318225 File Offset: 0x00316625
		private BoolReactiveProperty _isCountViewerVisible { get; } = new BoolReactiveProperty(true);

		// Token: 0x17001759 RID: 5977
		// (get) Token: 0x0600773D RID: 30525 RVA: 0x0031822D File Offset: 0x0031662D
		public bool isOpen
		{
			[CompilerGenerated]
			get
			{
				return this.IsActiveControl;
			}
		}

		// Token: 0x0600773E RID: 30526 RVA: 0x00318235 File Offset: 0x00316635
		public virtual void Open(StuffItem item)
		{
			this.Refresh(item);
			this.IsActiveControl = true;
		}

		// Token: 0x0600773F RID: 30527 RVA: 0x00318245 File Offset: 0x00316645
		public virtual void Close()
		{
			if (this.isOpen)
			{
				this.IsActiveControl = false;
			}
		}

		// Token: 0x06007740 RID: 30528 RVA: 0x00318259 File Offset: 0x00316659
		public virtual void Refresh(int count)
		{
			this._countViewer.MaxCount = Mathf.Max(count, 1);
			this._countViewer.ForceCount = 1;
		}

		// Token: 0x06007741 RID: 30529 RVA: 0x0031827C File Offset: 0x0031667C
		public virtual void Refresh(StuffItem item)
		{
			StuffItemInfo itemInfo = this.GetItemInfo(item);
			this._itemName.text = itemInfo.Name;
			this._flavorText.text = itemInfo.Explanation;
			this.Refresh(item.Count);
		}

		// Token: 0x06007742 RID: 30530 RVA: 0x003182C0 File Offset: 0x003166C0
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
			if (this._submitButton != null)
			{
				this._submitButton.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.OnInputSubmit();
				});
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

		// Token: 0x06007743 RID: 30531 RVA: 0x00318403 File Offset: 0x00316803
		protected virtual void OnDestroy()
		{
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = null;
		}

		// Token: 0x06007744 RID: 30532 RVA: 0x00318424 File Offset: 0x00316824
		private StuffItemInfo GetItemInfo(StuffItem item)
		{
			if (item == null)
			{
				return null;
			}
			return Singleton<Manager.Resources>.Instance.GameInfo.GetItem(item.CategoryID, item.ID) ?? Singleton<Manager.Resources>.Instance.GameInfo.GetItem_System(item.CategoryID, item.ID);
		}

		// Token: 0x06007745 RID: 30533 RVA: 0x00318478 File Offset: 0x00316878
		private void SetActiveControl(bool isActive)
		{
			IEnumerator coroutine = (!isActive) ? this.DoClose() : this.DoOpen();
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x06007746 RID: 30534 RVA: 0x003184E0 File Offset: 0x003168E0
		private IEnumerator LoadCountViewer()
		{
			GameObject countViewerObject = this._countViewerLayout.gameObject;
			this._isCountViewerVisible.Subscribe(delegate(bool visible)
			{
				countViewerObject.SetActive(visible);
			});
			yield return CountViewer.Load(this._countViewerLayout, delegate(CountViewer viewer)
			{
				this._countViewer = viewer;
			});
			yield break;
		}

		// Token: 0x06007747 RID: 30535 RVA: 0x003184FC File Offset: 0x003168FC
		private IEnumerator DoOpen()
		{
			if (this._canvasGroup.blocksRaycasts)
			{
				this._canvasGroup.blocksRaycasts = false;
			}
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

		// Token: 0x06007748 RID: 30536 RVA: 0x00318518 File Offset: 0x00316918
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

		// Token: 0x06007749 RID: 30537 RVA: 0x00318533 File Offset: 0x00316933
		private void OnInputSubmit()
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
			if (this.OnSubmit != null)
			{
				this.OnSubmit();
			}
		}

		// Token: 0x0600774A RID: 30538 RVA: 0x0031855D File Offset: 0x0031695D
		private void OnInputCancel()
		{
			if (this.OnCancel != null)
			{
				this.OnCancel();
			}
		}

		// Token: 0x040060C5 RID: 24773
		[SerializeField]
		private string _confirmLabel = string.Empty;

		// Token: 0x040060C6 RID: 24774
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x040060C7 RID: 24775
		[SerializeField]
		protected Text _itemName;

		// Token: 0x040060C8 RID: 24776
		[SerializeField]
		protected Text _flavorText;

		// Token: 0x040060C9 RID: 24777
		[SerializeField]
		protected Button _submitButton;

		// Token: 0x040060CA RID: 24778
		[SerializeField]
		protected GameObject _infoLayout;

		// Token: 0x040060CB RID: 24779
		[SerializeField]
		private RectTransform _countViewerLayout;

		// Token: 0x040060CC RID: 24780
		[SerializeField]
		protected CountViewer _countViewer;

		// Token: 0x040060CE RID: 24782
		private IDisposable _fadeDisposable;
	}
}
