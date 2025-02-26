using System;
using System.Collections;
using System.Runtime.CompilerServices;
using ReMotion;
using Sirenix.OdinInspector;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AIProject.UI
{
	// Token: 0x02000FD9 RID: 4057
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasGroup))]
	public class ResultMessageElement : UIBehaviour
	{
		// Token: 0x17001D7C RID: 7548
		// (get) Token: 0x06008719 RID: 34585 RVA: 0x00385D5E File Offset: 0x0038415E
		// (set) Token: 0x0600871A RID: 34586 RVA: 0x00385D86 File Offset: 0x00384186
		public float CanvasAlpha
		{
			get
			{
				return (!(this.canvasGroup != null)) ? 0f : this.canvasGroup.alpha;
			}
			set
			{
				if (this.canvasGroup != null)
				{
					this.canvasGroup.alpha = value;
				}
			}
		}

		// Token: 0x17001D7D RID: 7549
		// (get) Token: 0x0600871B RID: 34587 RVA: 0x00385DA5 File Offset: 0x003841A5
		public string Message
		{
			[CompilerGenerated]
			get
			{
				return this.messageText.text;
			}
		}

		// Token: 0x17001D7E RID: 7550
		// (get) Token: 0x0600871C RID: 34588 RVA: 0x00385DB2 File Offset: 0x003841B2
		// (set) Token: 0x0600871D RID: 34589 RVA: 0x00385DBF File Offset: 0x003841BF
		public Vector3 LocalPosition
		{
			get
			{
				return this.myTransform.localPosition;
			}
			private set
			{
				this.myTransform.localPosition = value;
			}
		}

		// Token: 0x17001D7F RID: 7551
		// (get) Token: 0x0600871E RID: 34590 RVA: 0x00385DCD File Offset: 0x003841CD
		// (set) Token: 0x0600871F RID: 34591 RVA: 0x00385DDA File Offset: 0x003841DA
		public Vector3 LocalScale
		{
			get
			{
				return this.myTransform.localScale;
			}
			private set
			{
				this.myTransform.localScale = value;
			}
		}

		// Token: 0x17001D80 RID: 7552
		// (get) Token: 0x06008720 RID: 34592 RVA: 0x00385DE8 File Offset: 0x003841E8
		// (set) Token: 0x06008721 RID: 34593 RVA: 0x00385DF0 File Offset: 0x003841F0
		public ResultMessageUI Root { get; set; }

		// Token: 0x17001D81 RID: 7553
		// (get) Token: 0x06008722 RID: 34594 RVA: 0x00385DF9 File Offset: 0x003841F9
		// (set) Token: 0x06008723 RID: 34595 RVA: 0x00385E01 File Offset: 0x00384201
		public Action<ResultMessageElement> EndAction { get; set; }

		// Token: 0x17001D82 RID: 7554
		// (get) Token: 0x06008724 RID: 34596 RVA: 0x00385E0A File Offset: 0x0038420A
		public bool PlayingFadeIn
		{
			[CompilerGenerated]
			get
			{
				return this.fadeInDisposable != null;
			}
		}

		// Token: 0x17001D83 RID: 7555
		// (get) Token: 0x06008725 RID: 34597 RVA: 0x00385E18 File Offset: 0x00384218
		public bool PlayingDisplay
		{
			[CompilerGenerated]
			get
			{
				return this.displayDisposable != null;
			}
		}

		// Token: 0x17001D84 RID: 7556
		// (get) Token: 0x06008726 RID: 34598 RVA: 0x00385E26 File Offset: 0x00384226
		public bool PlayingFadeOut
		{
			[CompilerGenerated]
			get
			{
				return this.fadeOutDisposable != null;
			}
		}

		// Token: 0x06008727 RID: 34599 RVA: 0x00385E34 File Offset: 0x00384234
		protected override void Awake()
		{
			base.Awake();
			this.messageText.color = this.Root.White;
			this.OriginPosition = this.myTransform.localPosition;
			this.OriginScale = this.myTransform.localScale;
			this.CanvasAlpha = 0f;
		}

		// Token: 0x06008728 RID: 34600 RVA: 0x00385E8F File Offset: 0x0038428F
		protected override void OnDisable()
		{
			this.Dispose();
			base.OnDisable();
		}

		// Token: 0x17001D85 RID: 7557
		// (get) Token: 0x06008729 RID: 34601 RVA: 0x00385E9D File Offset: 0x0038429D
		// (set) Token: 0x0600872A RID: 34602 RVA: 0x00385EA5 File Offset: 0x003842A5
		public bool IsForcedClose { get; private set; }

		// Token: 0x0600872B RID: 34603 RVA: 0x00385EB0 File Offset: 0x003842B0
		public void ShowMessage(string _mes)
		{
			this.Dispose();
			this.IsForcedClose = false;
			this.messageText.text = _mes;
			if (this.fadeInDisposable != null)
			{
				this.fadeInDisposable.Dispose();
			}
			this.fadeInDisposable = new CompositeDisposable();
			this.CanvasAlpha = 0f;
			IEnumerator _fadeInCoroutine = this.FadeIn();
			Observable.FromCoroutine(() => _fadeInCoroutine, false).TakeUntilDisable(this).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				this.fadeInDisposable = null;
			}).AddTo(this.fadeInDisposable);
		}

		// Token: 0x0600872C RID: 34604 RVA: 0x00385F70 File Offset: 0x00384370
		public void StartDisplay()
		{
			if (this.displayDisposable != null)
			{
				this.displayDisposable.Dispose();
			}
			this.displayDisposable = new CompositeDisposable();
			this.CanvasAlpha = this.displayAlpha;
			IEnumerator _coroutine = this.OnDisplay();
			Observable.FromCoroutine(() => _coroutine, false).TakeUntilDisable(this).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				this.displayDisposable = null;
			}).AddTo(this.displayDisposable);
		}

		// Token: 0x0600872D RID: 34605 RVA: 0x00386018 File Offset: 0x00384418
		public void CloseMessage()
		{
			if (this.PlayingFadeOut)
			{
				return;
			}
			this.Dispose();
			this.fadeOutDisposable = new CompositeDisposable();
			IEnumerator _coroutine = this.FadeOut();
			Observable.FromCoroutine(() => _coroutine, false).TakeUntilDisable(this).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				this.fadeOutDisposable = null;
			}).AddTo(this.fadeOutDisposable);
		}

		// Token: 0x0600872E RID: 34606 RVA: 0x003860AD File Offset: 0x003844AD
		public void CancelMessage()
		{
			this.Dispose();
			Action<ResultMessageElement> endAction = this.EndAction;
			if (endAction != null)
			{
				endAction(this);
			}
		}

		// Token: 0x0600872F RID: 34607 RVA: 0x003860CC File Offset: 0x003844CC
		public void Dispose()
		{
			if (this.fadeInDisposable != null)
			{
				this.fadeInDisposable.Dispose();
			}
			if (this.displayDisposable != null)
			{
				this.displayDisposable.Dispose();
			}
			if (this.fadeOutDisposable != null)
			{
				this.fadeOutDisposable.Dispose();
			}
			this.fadeInDisposable = null;
			this.displayDisposable = null;
			this.fadeOutDisposable = null;
		}

		// Token: 0x06008730 RID: 34608 RVA: 0x00386138 File Offset: 0x00384538
		private IConnectableObservable<TimeInterval<float>> GetObservableEasing(ResultMessageElement.FadeType _fadeType, float _duration)
		{
			switch (_fadeType)
			{
			case ResultMessageElement.FadeType.Linear:
				return ObservableEasing.Linear(_duration, true).TakeUntilDisable(base.gameObject).FrameTimeInterval(false).Publish<TimeInterval<float>>();
			case ResultMessageElement.FadeType.EaseOutQuint:
				return ObservableEasing.EaseOutQuint(_duration, true).TakeUntilDisable(base.gameObject).FrameTimeInterval(false).Publish<TimeInterval<float>>();
			case ResultMessageElement.FadeType.EaseInQuint:
				return ObservableEasing.EaseInQuint(_duration, true).TakeUntilDisable(base.gameObject).FrameTimeInterval(false).Publish<TimeInterval<float>>();
			default:
				return null;
			}
		}

		// Token: 0x06008731 RID: 34609 RVA: 0x003861BC File Offset: 0x003845BC
		private IEnumerator FadeIn()
		{
			ResultMessageElement.<FadeIn>c__Iterator0.<FadeIn>c__AnonStorey6 <FadeIn>c__AnonStorey = new ResultMessageElement.<FadeIn>c__Iterator0.<FadeIn>c__AnonStorey6();
			<FadeIn>c__AnonStorey.<>f__ref$0 = this;
			IConnectableObservable<TimeInterval<float>> _fadeInStream = this.GetObservableEasing(this.fadeInType, this.fadeInTime);
			if (_fadeInStream == null)
			{
				this.fadeInDisposable = null;
				this.LocalScale = Vector3.one;
				this.StartDisplay();
				yield break;
			}
			ResultMessageElement.<FadeIn>c__Iterator0.<FadeIn>c__AnonStorey6 <FadeIn>c__AnonStorey2 = <FadeIn>c__AnonStorey;
			Vector3 localScale = Vector3.one * this.startScale;
			this.LocalScale = localScale;
			<FadeIn>c__AnonStorey2._startScale = localScale;
			Vector3 _startPosition = this.LocalPosition;
			this.CanvasAlpha = this.startAlpha;
			_fadeInStream.Connect();
			_fadeInStream.Subscribe(delegate(TimeInterval<float> x)
			{
				float t = Mathf.InverseLerp(0f, <FadeIn>c__AnonStorey.<>f__ref$0.$this.fadeInTime, x.Value);
				<FadeIn>c__AnonStorey.<>f__ref$0.$this.CanvasAlpha = Mathf.Lerp(<FadeIn>c__AnonStorey.<>f__ref$0.$this.startAlpha, 1f, t);
				<FadeIn>c__AnonStorey.<>f__ref$0.$this.LocalScale = Vector3.Lerp(<FadeIn>c__AnonStorey._startScale, Vector3.one, t);
			}).AddTo(this.fadeInDisposable);
			yield return Observable.WhenAll<TimeInterval<float>>(new IObservable<TimeInterval<float>>[]
			{
				_fadeInStream
			}).TakeUntilDisable(this).ToYieldInstruction<TimeInterval<float>[]>().AddTo(this.fadeInDisposable);
			this.fadeInDisposable = null;
			this.StartDisplay();
			yield break;
		}

		// Token: 0x06008732 RID: 34610 RVA: 0x003861D8 File Offset: 0x003845D8
		private IEnumerator OnDisplay()
		{
			yield return Observable.Timer(TimeSpan.FromSeconds((double)this.displayTime)).TakeUntilDisable(this).ToYieldInstruction<long>().AddTo(this.displayDisposable);
			this.displayDisposable = null;
			this.CloseMessage();
			yield break;
		}

		// Token: 0x06008733 RID: 34611 RVA: 0x003861F4 File Offset: 0x003845F4
		private IEnumerator FadeOut()
		{
			IConnectableObservable<TimeInterval<float>> _fadeOutStream = this.GetObservableEasing(this.fadeOutType, this.fadeOutTime);
			if (_fadeOutStream == null)
			{
				this.fadeOutDisposable = null;
				this.CanvasAlpha = 0f;
				this.LocalScale = Vector3.one * this.startScale;
				yield break;
			}
			float _startAlpha = this.CanvasAlpha;
			Vector3 _startScale = this.LocalScale;
			Vector3 _endScale = Vector3.one * this.startScale;
			_fadeOutStream.Connect();
			_fadeOutStream.Subscribe(delegate(TimeInterval<float> x)
			{
				float t = Mathf.InverseLerp(0f, this.fadeOutTime, x.Value);
				this.CanvasAlpha = Mathf.Lerp(_startAlpha, this.endAlpha, t);
				this.LocalScale = Vector3.Lerp(_startScale, _endScale, t);
			}).AddTo(this.fadeOutDisposable);
			yield return Observable.WhenAll<TimeInterval<float>>(new IObservable<TimeInterval<float>>[]
			{
				_fadeOutStream
			}).TakeUntilDisable(this).ToYieldInstruction<TimeInterval<float>[]>().AddTo(this.fadeOutDisposable);
			this.CanvasAlpha = 0f;
			Action<ResultMessageElement> endAction = this.EndAction;
			if (endAction != null)
			{
				endAction(this);
			}
			this.fadeOutDisposable = null;
			yield break;
		}

		// Token: 0x04006DC7 RID: 28103
		[SerializeField]
		private RectTransform myTransform;

		// Token: 0x04006DC8 RID: 28104
		[SerializeField]
		private CanvasGroup canvasGroup;

		// Token: 0x04006DC9 RID: 28105
		[SerializeField]
		private TextMeshProUGUI messageText;

		// Token: 0x04006DCA RID: 28106
		[SerializeField]
		[Tooltip("表示開始時の大きさ")]
		private float startScale = 0.3f;

		// Token: 0x04006DCB RID: 28107
		[SerializeField]
		[Tooltip("表示時間")]
		private float displayTime = 3f;

		// Token: 0x04006DCC RID: 28108
		[SerializeField]
		[FoldoutGroup("各透明度", 0)]
		private float startAlpha;

		// Token: 0x04006DCD RID: 28109
		[SerializeField]
		[FoldoutGroup("各透明度", 0)]
		private float displayAlpha = 1f;

		// Token: 0x04006DCE RID: 28110
		[SerializeField]
		[FoldoutGroup("各透明度", 0)]
		private float endAlpha;

		// Token: 0x04006DCF RID: 28111
		[SerializeField]
		[Tooltip("フェードインに使用する時間")]
		private float fadeInTime = 1f;

		// Token: 0x04006DD0 RID: 28112
		[SerializeField]
		[Tooltip("フェードインの補間タイプ")]
		private ResultMessageElement.FadeType fadeInType = ResultMessageElement.FadeType.Linear;

		// Token: 0x04006DD1 RID: 28113
		[SerializeField]
		[Tooltip("フェードアウトに使用する時間")]
		private float fadeOutTime = 1f;

		// Token: 0x04006DD2 RID: 28114
		[SerializeField]
		[Tooltip("フェードアウトの補間タイプ")]
		private ResultMessageElement.FadeType fadeOutType = ResultMessageElement.FadeType.Linear;

		// Token: 0x04006DD3 RID: 28115
		private Vector3 OriginPosition = Vector3.zero;

		// Token: 0x04006DD4 RID: 28116
		private Vector3 OriginScale = Vector3.one;

		// Token: 0x04006DD7 RID: 28119
		private CompositeDisposable fadeInDisposable;

		// Token: 0x04006DD8 RID: 28120
		private CompositeDisposable displayDisposable;

		// Token: 0x04006DD9 RID: 28121
		private CompositeDisposable fadeOutDisposable;

		// Token: 0x02000FDA RID: 4058
		public enum FadeType
		{
			// Token: 0x04006DDF RID: 28127
			None,
			// Token: 0x04006DE0 RID: 28128
			Linear,
			// Token: 0x04006DE1 RID: 28129
			EaseOutQuint,
			// Token: 0x04006DE2 RID: 28130
			EaseInQuint
		}
	}
}
