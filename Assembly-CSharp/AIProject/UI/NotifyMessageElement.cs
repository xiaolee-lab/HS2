using System;
using System.Collections;
using System.Runtime.CompilerServices;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000F16 RID: 3862
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasGroup))]
	public class NotifyMessageElement : UIBehaviour
	{
		// Token: 0x17001907 RID: 6407
		// (get) Token: 0x06007E72 RID: 32370 RVA: 0x0035CB91 File Offset: 0x0035AF91
		// (set) Token: 0x06007E73 RID: 32371 RVA: 0x0035CBB9 File Offset: 0x0035AFB9
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

		// Token: 0x17001908 RID: 6408
		// (get) Token: 0x06007E74 RID: 32372 RVA: 0x0035CBD8 File Offset: 0x0035AFD8
		// (set) Token: 0x06007E75 RID: 32373 RVA: 0x0035CBE5 File Offset: 0x0035AFE5
		public Vector3 LocalPosition
		{
			get
			{
				return this.myTransform.localPosition;
			}
			set
			{
				this.myTransform.localPosition = value;
			}
		}

		// Token: 0x17001909 RID: 6409
		// (get) Token: 0x06007E76 RID: 32374 RVA: 0x0035CBF3 File Offset: 0x0035AFF3
		// (set) Token: 0x06007E77 RID: 32375 RVA: 0x0035CC00 File Offset: 0x0035B000
		public Vector3 LocalScale
		{
			get
			{
				return this.myTransform.localScale;
			}
			set
			{
				this.myTransform.localScale = value;
			}
		}

		// Token: 0x1700190A RID: 6410
		// (get) Token: 0x06007E78 RID: 32376 RVA: 0x0035CC10 File Offset: 0x0035B010
		public float Height
		{
			get
			{
				return this.myTransform.sizeDelta.y;
			}
		}

		// Token: 0x1700190B RID: 6411
		// (get) Token: 0x06007E79 RID: 32377 RVA: 0x0035CC30 File Offset: 0x0035B030
		public float Width
		{
			get
			{
				return this.myTransform.sizeDelta.x;
			}
		}

		// Token: 0x1700190C RID: 6412
		// (get) Token: 0x06007E7A RID: 32378 RVA: 0x0035CC50 File Offset: 0x0035B050
		// (set) Token: 0x06007E7B RID: 32379 RVA: 0x0035CC5D File Offset: 0x0035B05D
		public string MessageText
		{
			get
			{
				return this.messageText.text;
			}
			set
			{
				this.messageText.text = value;
			}
		}

		// Token: 0x1700190D RID: 6413
		// (get) Token: 0x06007E7C RID: 32380 RVA: 0x0035CC6B File Offset: 0x0035B06B
		// (set) Token: 0x06007E7D RID: 32381 RVA: 0x0035CC78 File Offset: 0x0035B078
		public Color MessageColor
		{
			get
			{
				return this.messageText.color;
			}
			set
			{
				this.messageText.color = value;
			}
		}

		// Token: 0x1700190E RID: 6414
		// (get) Token: 0x06007E7E RID: 32382 RVA: 0x0035CC86 File Offset: 0x0035B086
		// (set) Token: 0x06007E7F RID: 32383 RVA: 0x0035CC8E File Offset: 0x0035B08E
		public int NotifyID { get; set; } = -1;

		// Token: 0x1700190F RID: 6415
		// (get) Token: 0x06007E80 RID: 32384 RVA: 0x0035CC97 File Offset: 0x0035B097
		// (set) Token: 0x06007E81 RID: 32385 RVA: 0x0035CC9F File Offset: 0x0035B09F
		public Action<NotifyMessageElement> EndActionEvent { get; set; }

		// Token: 0x17001910 RID: 6416
		// (get) Token: 0x06007E82 RID: 32386 RVA: 0x0035CCA8 File Offset: 0x0035B0A8
		// (set) Token: 0x06007E83 RID: 32387 RVA: 0x0035CCB0 File Offset: 0x0035B0B0
		public NotifyMessageList Root { get; set; }

		// Token: 0x17001911 RID: 6417
		// (get) Token: 0x06007E84 RID: 32388 RVA: 0x0035CCB9 File Offset: 0x0035B0B9
		public bool PlayingFadeIn
		{
			[CompilerGenerated]
			get
			{
				return this.fadeInDisposable != null;
			}
		}

		// Token: 0x17001912 RID: 6418
		// (get) Token: 0x06007E85 RID: 32389 RVA: 0x0035CCC7 File Offset: 0x0035B0C7
		public bool PlayingDisplay
		{
			[CompilerGenerated]
			get
			{
				return this.displayDisposable != null;
			}
		}

		// Token: 0x17001913 RID: 6419
		// (get) Token: 0x06007E86 RID: 32390 RVA: 0x0035CCD5 File Offset: 0x0035B0D5
		public bool PlayingFadeOut
		{
			[CompilerGenerated]
			get
			{
				return this.fadeOutDisposable != null;
			}
		}

		// Token: 0x17001914 RID: 6420
		// (get) Token: 0x06007E87 RID: 32391 RVA: 0x0035CCE3 File Offset: 0x0035B0E3
		public bool PlayingSlidUp
		{
			[CompilerGenerated]
			get
			{
				return this.slidUpDisposable != null;
			}
		}

		// Token: 0x17001915 RID: 6421
		// (get) Token: 0x06007E88 RID: 32392 RVA: 0x0035CCF1 File Offset: 0x0035B0F1
		// (set) Token: 0x06007E89 RID: 32393 RVA: 0x0035CD03 File Offset: 0x0035B103
		public bool SpeechBubbleIconActive
		{
			get
			{
				return this.icon.gameObject.activeSelf;
			}
			set
			{
				if (this.icon.gameObject.activeSelf != value)
				{
					this.icon.gameObject.SetActive(value);
				}
			}
		}

		// Token: 0x06007E8A RID: 32394 RVA: 0x0035CD2C File Offset: 0x0035B12C
		protected override void OnEnable()
		{
			base.OnEnable();
			this.Dispose();
		}

		// Token: 0x06007E8B RID: 32395 RVA: 0x0035CD3A File Offset: 0x0035B13A
		protected override void OnDisable()
		{
			Action<NotifyMessageElement> endActionEvent = this.EndActionEvent;
			if (endActionEvent != null)
			{
				endActionEvent(this);
			}
			this.Dispose();
			base.OnDisable();
		}

		// Token: 0x06007E8C RID: 32396 RVA: 0x0035CD60 File Offset: 0x0035B160
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
			if (this.slidUpDisposable != null)
			{
				this.slidUpDisposable.Dispose();
			}
			this.fadeInDisposable = null;
			this.displayDisposable = null;
			this.fadeOutDisposable = null;
			this.slidUpDisposable = null;
		}

		// Token: 0x06007E8D RID: 32397 RVA: 0x0035CDE9 File Offset: 0x0035B1E9
		public void SetTime(float _fadeInTime, float _displayTime, float _fadeOutTime)
		{
			this.fadeInTime = _fadeInTime;
			this.displayTime = _displayTime;
			this.fadeOutTime = _fadeOutTime;
		}

		// Token: 0x06007E8E RID: 32398 RVA: 0x0035CE00 File Offset: 0x0035B200
		public void SetAlpha(float _startAlpha, float _endAlpha)
		{
			this.startAlpha = _startAlpha;
			this.endAlpha = _endAlpha;
		}

		// Token: 0x06007E8F RID: 32399 RVA: 0x0035CE10 File Offset: 0x0035B210
		public void StartFadeIn(float _posX)
		{
			if (this.fadeInDisposable != null)
			{
				this.fadeInDisposable.Dispose();
			}
			this.fadeInDisposable = new CompositeDisposable();
			IEnumerator _coroutine = this.FadeInCoroutine(_posX);
			Observable.FromCoroutine(() => _coroutine, false).TakeUntilDisable(base.gameObject).Subscribe<Unit>().AddTo(this.fadeInDisposable);
		}

		// Token: 0x06007E90 RID: 32400 RVA: 0x0035CE84 File Offset: 0x0035B284
		private IEnumerator FadeInCoroutine(float _posX)
		{
			IConnectableObservable<TimeInterval<float>> _fadeInStream = ObservableEasing.EaseOutQuint(this.fadeInTime, true).TakeUntilDisable(base.gameObject).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			float _startX = this.LocalPosition.x;
			float _endX = _posX;
			this.CanvasAlpha = this.startAlpha;
			_fadeInStream.Connect();
			_fadeInStream.Subscribe(delegate(TimeInterval<float> x)
			{
				float value = x.Value;
				this.CanvasAlpha = Mathf.Lerp(this.startAlpha, 1f, value);
				Vector3 localPosition = this.LocalPosition;
				localPosition.x = Mathf.Lerp(_startX, _endX, value);
				this.LocalPosition = localPosition;
			}).AddTo(this.fadeInDisposable);
			yield return Observable.WhenAll<TimeInterval<float>>(new IObservable<TimeInterval<float>>[]
			{
				_fadeInStream
			}).TakeUntilDisable(this).ToYieldInstruction<TimeInterval<float>[]>().AddTo(this.fadeInDisposable);
			this.fadeInDisposable = null;
			this.StartDisplay();
			yield break;
		}

		// Token: 0x06007E91 RID: 32401 RVA: 0x0035CEA8 File Offset: 0x0035B2A8
		public void StartDisplay()
		{
			if (this.displayDisposable != null)
			{
				this.displayDisposable.Dispose();
			}
			this.displayDisposable = new CompositeDisposable();
			IEnumerator _coroutine = this.DisplayCoroutine();
			Observable.FromCoroutine(() => _coroutine, false).TakeUntilDisable(base.gameObject).Subscribe<Unit>().AddTo(this.displayDisposable);
		}

		// Token: 0x06007E92 RID: 32402 RVA: 0x0035CF18 File Offset: 0x0035B318
		private IEnumerator DisplayCoroutine()
		{
			yield return Observable.Timer(TimeSpan.FromSeconds((double)this.displayTime), Scheduler.MainThreadIgnoreTimeScale).TakeUntilDisable(this).ToYieldInstruction<long>().AddTo(this.displayDisposable);
			this.displayDisposable = null;
			this.StartFadeOut();
			yield break;
		}

		// Token: 0x06007E93 RID: 32403 RVA: 0x0035CF34 File Offset: 0x0035B334
		public void StartFadeOut()
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
			this.fadeOutDisposable = new CompositeDisposable();
			IEnumerator _coroutine = this.FadeOutCoroutine();
			Observable.FromCoroutine(() => _coroutine, false).TakeUntilDisable(base.gameObject).Subscribe<Unit>().AddTo(this.fadeOutDisposable);
		}

		// Token: 0x06007E94 RID: 32404 RVA: 0x0035CFE4 File Offset: 0x0035B3E4
		private IEnumerator FadeOutCoroutine()
		{
			IConnectableObservable<TimeInterval<float>> _fadeOutStream = ObservableEasing.EaseOutQuint(this.fadeOutTime, true).TakeUntilDisable(this).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			float _startX = this.LocalPosition.x;
			float _endX = 0f;
			float _startAlpha = this.CanvasAlpha;
			_fadeOutStream.Connect();
			_fadeOutStream.Subscribe(delegate(TimeInterval<float> x)
			{
				float value = x.Value;
				this.CanvasAlpha = Mathf.Lerp(_startAlpha, this.endAlpha, value);
				Vector3 localPosition = this.LocalPosition;
				localPosition.x = Mathf.Lerp(_startX, _endX, value);
				this.LocalPosition = localPosition;
			}).AddTo(this.fadeOutDisposable);
			yield return Observable.WhenAll<TimeInterval<float>>(new IObservable<TimeInterval<float>>[]
			{
				_fadeOutStream
			}).TakeUntilDisable(this).ToYieldInstruction<TimeInterval<float>[]>().AddTo(this.fadeOutDisposable);
			this.fadeOutDisposable = null;
			Action<NotifyMessageElement> endActionEvent = this.EndActionEvent;
			if (endActionEvent != null)
			{
				endActionEvent(this);
			}
			yield break;
		}

		// Token: 0x06007E95 RID: 32405 RVA: 0x0035D000 File Offset: 0x0035B400
		public void StartSlidUp(float _posY)
		{
			if (this.slidUpDisposable != null)
			{
				this.slidUpDisposable.Dispose();
			}
			this.slidUpDisposable = new CompositeDisposable();
			IEnumerator _coroutine = this.SlidUpCoroutine(_posY);
			Observable.FromCoroutine(() => _coroutine, false).TakeUntilDisable(base.gameObject).Subscribe<Unit>().AddTo(this.slidUpDisposable);
		}

		// Token: 0x06007E96 RID: 32406 RVA: 0x0035D074 File Offset: 0x0035B474
		private IEnumerator SlidUpCoroutine(float _posY)
		{
			IConnectableObservable<TimeInterval<float>> _slidUpStream = ObservableEasing.EaseOutQuint(this.fadeInTime, true).TakeUntilDisable(base.gameObject).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			float _startY = this.LocalPosition.y;
			_slidUpStream.Connect();
			_slidUpStream.Subscribe(delegate(TimeInterval<float> x)
			{
				float value = x.Value;
				Vector3 localPosition = this.LocalPosition;
				localPosition.y = Mathf.Lerp(_startY, _posY, value);
				this.LocalPosition = localPosition;
			}).AddTo(this.slidUpDisposable);
			yield return Observable.WhenAll<TimeInterval<float>>(new IObservable<TimeInterval<float>>[]
			{
				_slidUpStream
			}).TakeUntilDisable(this).ToYieldInstruction<TimeInterval<float>[]>().AddTo(this.slidUpDisposable);
			this.slidUpDisposable = null;
			yield break;
		}

		// Token: 0x040065FF RID: 26111
		[SerializeField]
		private RectTransform myTransform;

		// Token: 0x04006600 RID: 26112
		[SerializeField]
		private CanvasGroup canvasGroup;

		// Token: 0x04006601 RID: 26113
		[SerializeField]
		private Image backPanel;

		// Token: 0x04006602 RID: 26114
		[SerializeField]
		private Image icon;

		// Token: 0x04006603 RID: 26115
		[SerializeField]
		private Text messageText;

		// Token: 0x04006607 RID: 26119
		private CompositeDisposable fadeInDisposable;

		// Token: 0x04006608 RID: 26120
		private CompositeDisposable displayDisposable;

		// Token: 0x04006609 RID: 26121
		private CompositeDisposable fadeOutDisposable;

		// Token: 0x0400660A RID: 26122
		private CompositeDisposable slidUpDisposable;

		// Token: 0x0400660B RID: 26123
		private float fadeInTime = 0.8f;

		// Token: 0x0400660C RID: 26124
		private float displayTime = 2f;

		// Token: 0x0400660D RID: 26125
		private float fadeOutTime = 0.8f;

		// Token: 0x0400660E RID: 26126
		private float startAlpha = 0.5f;

		// Token: 0x0400660F RID: 26127
		private float endAlpha = 0.5f;
	}
}
