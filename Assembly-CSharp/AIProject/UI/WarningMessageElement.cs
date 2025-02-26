using System;
using System.Collections;
using System.Runtime.CompilerServices;
using AIProject.UI.Popup;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000FE2 RID: 4066
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasGroup))]
	public class WarningMessageElement : UIBehaviour
	{
		// Token: 0x17001DBF RID: 7615
		// (get) Token: 0x0600881E RID: 34846 RVA: 0x0038BBCC File Offset: 0x00389FCC
		// (set) Token: 0x0600881F RID: 34847 RVA: 0x0038BBD4 File Offset: 0x00389FD4
		public WarningMessageUI Root { get; set; }

		// Token: 0x17001DC0 RID: 7616
		// (get) Token: 0x06008820 RID: 34848 RVA: 0x0038BBDD File Offset: 0x00389FDD
		// (set) Token: 0x06008821 RID: 34849 RVA: 0x0038BBE5 File Offset: 0x00389FE5
		public Action<WarningMessageElement> EndAction { get; set; }

		// Token: 0x17001DC1 RID: 7617
		// (get) Token: 0x06008822 RID: 34850 RVA: 0x0038BBEE File Offset: 0x00389FEE
		// (set) Token: 0x06008823 RID: 34851 RVA: 0x0038BBF6 File Offset: 0x00389FF6
		public Action ClosedAction { get; set; }

		// Token: 0x17001DC2 RID: 7618
		// (get) Token: 0x06008824 RID: 34852 RVA: 0x0038BBFF File Offset: 0x00389FFF
		public bool PlayingFadeIn
		{
			[CompilerGenerated]
			get
			{
				return this.fadeInDisposable != null;
			}
		}

		// Token: 0x17001DC3 RID: 7619
		// (get) Token: 0x06008825 RID: 34853 RVA: 0x0038BC0D File Offset: 0x0038A00D
		public bool PlayingDisplay
		{
			[CompilerGenerated]
			get
			{
				return this.displayDisposable != null;
			}
		}

		// Token: 0x17001DC4 RID: 7620
		// (get) Token: 0x06008826 RID: 34854 RVA: 0x0038BC1B File Offset: 0x0038A01B
		public bool PlayingFadeOut
		{
			[CompilerGenerated]
			get
			{
				return this.fadeOutDisposable != null;
			}
		}

		// Token: 0x17001DC5 RID: 7621
		// (get) Token: 0x06008827 RID: 34855 RVA: 0x0038BC29 File Offset: 0x0038A029
		// (set) Token: 0x06008828 RID: 34856 RVA: 0x0038BC31 File Offset: 0x0038A031
		public bool isFadeInForOutWait { get; set; }

		// Token: 0x06008829 RID: 34857 RVA: 0x0038BC3A File Offset: 0x0038A03A
		protected override void OnEnable()
		{
			this.Dispose();
			base.OnEnable();
		}

		// Token: 0x0600882A RID: 34858 RVA: 0x0038BC48 File Offset: 0x0038A048
		protected override void OnDisable()
		{
			this.Dispose();
			base.OnDisable();
		}

		// Token: 0x17001DC6 RID: 7622
		// (get) Token: 0x0600882B RID: 34859 RVA: 0x0038BC56 File Offset: 0x0038A056
		// (set) Token: 0x0600882C RID: 34860 RVA: 0x0038BC63 File Offset: 0x0038A063
		public string Text
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

		// Token: 0x17001DC7 RID: 7623
		// (set) Token: 0x0600882D RID: 34861 RVA: 0x0038BC71 File Offset: 0x0038A071
		public Color Color
		{
			set
			{
				this.messageText.color = value;
			}
		}

		// Token: 0x17001DC8 RID: 7624
		// (get) Token: 0x0600882E RID: 34862 RVA: 0x0038BC7F File Offset: 0x0038A07F
		// (set) Token: 0x0600882F RID: 34863 RVA: 0x0038BCA7 File Offset: 0x0038A0A7
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

		// Token: 0x17001DC9 RID: 7625
		// (get) Token: 0x06008830 RID: 34864 RVA: 0x0038BCC6 File Offset: 0x0038A0C6
		// (set) Token: 0x06008831 RID: 34865 RVA: 0x0038BCD3 File Offset: 0x0038A0D3
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

		// Token: 0x06008832 RID: 34866 RVA: 0x0038BCE1 File Offset: 0x0038A0E1
		public void SetFadeInfo(FadeInfo _fadeInInfo, FadeInfo _displayInfo, FadeInfo _fadeOutInfo)
		{
			this.fadeInInfo = _fadeInInfo;
			this.displayInfo = _displayInfo;
			this.fadeOutInfo = _fadeOutInfo;
		}

		// Token: 0x06008833 RID: 34867 RVA: 0x0038BCF8 File Offset: 0x0038A0F8
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

		// Token: 0x06008834 RID: 34868 RVA: 0x0038BD64 File Offset: 0x0038A164
		public void StartFadeIn()
		{
			this.Dispose();
			this.fadeInDisposable = new CompositeDisposable();
			IEnumerator _coroutine = this.FadeInCoroutine();
			Observable.FromCoroutine(() => _coroutine, false).TakeUntilDisable(this).Subscribe<Unit>().AddTo(this.fadeInDisposable);
		}

		// Token: 0x06008835 RID: 34869 RVA: 0x0038BDC0 File Offset: 0x0038A1C0
		private IEnumerator FadeInCoroutine()
		{
			WarningMessageElement.<FadeInCoroutine>c__Iterator0.<FadeInCoroutine>c__AnonStorey4 <FadeInCoroutine>c__AnonStorey = new WarningMessageElement.<FadeInCoroutine>c__Iterator0.<FadeInCoroutine>c__AnonStorey4();
			<FadeInCoroutine>c__AnonStorey.<>f__ref$0 = this;
			IConnectableObservable<TimeInterval<float>> _fadeInStream = ObservableEasing.EaseOutQuint(this.fadeInInfo.Time, true).TakeUntilDisable(this).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			<FadeInCoroutine>c__AnonStorey._startScale = Vector3.one * this.fadeInInfo.Scale;
			<FadeInCoroutine>c__AnonStorey._endScale = Vector3.one * this.displayInfo.Scale;
			WarningMessageElement.<FadeInCoroutine>c__Iterator0.<FadeInCoroutine>c__AnonStorey4 <FadeInCoroutine>c__AnonStorey2 = <FadeInCoroutine>c__AnonStorey;
			float alpha = this.fadeInInfo.Alpha;
			this.CanvasAlpha = alpha;
			<FadeInCoroutine>c__AnonStorey2._startAlpha = alpha;
			<FadeInCoroutine>c__AnonStorey._endAlpha = this.displayInfo.Alpha;
			_fadeInStream.Connect();
			_fadeInStream.Subscribe(delegate(TimeInterval<float> x)
			{
				float value = x.Value;
				<FadeInCoroutine>c__AnonStorey.<>f__ref$0.$this.CanvasAlpha = Mathf.Lerp(<FadeInCoroutine>c__AnonStorey._startAlpha, <FadeInCoroutine>c__AnonStorey._endAlpha, value);
				<FadeInCoroutine>c__AnonStorey.<>f__ref$0.$this.LocalScale = Vector3.Lerp(<FadeInCoroutine>c__AnonStorey._startScale, <FadeInCoroutine>c__AnonStorey._endScale, value);
			}).AddTo(this.fadeInDisposable);
			yield return Observable.WhenAll<TimeInterval<float>>(new IObservable<TimeInterval<float>>[]
			{
				_fadeInStream
			}).TakeUntilDisable(this).ToYieldInstruction<TimeInterval<float>[]>().AddTo(this.fadeInDisposable);
			this.fadeInDisposable = null;
			this.StartDisplay();
			yield break;
		}

		// Token: 0x06008836 RID: 34870 RVA: 0x0038BDDC File Offset: 0x0038A1DC
		public void StartDisplay()
		{
			this.Dispose();
			this.displayDisposable = new CompositeDisposable();
			IEnumerator _coroutine = this.DisplayCoroutine();
			Observable.FromCoroutine(() => _coroutine, false).TakeUntilDisable(this).Subscribe<Unit>().AddTo(this.displayDisposable);
		}

		// Token: 0x06008837 RID: 34871 RVA: 0x0038BE38 File Offset: 0x0038A238
		private IEnumerator DisplayCoroutine()
		{
			yield return Observable.Timer(TimeSpan.FromSeconds((double)this.displayInfo.Time), Scheduler.MainThreadIgnoreTimeScale).TakeUntilDisable(this).ToYieldInstruction<long>().AddTo(this.displayDisposable);
			this.displayDisposable = null;
			while (this.isFadeInForOutWait)
			{
				yield return null;
			}
			this.StartFadeOut();
			yield break;
		}

		// Token: 0x06008838 RID: 34872 RVA: 0x0038BE54 File Offset: 0x0038A254
		public void StartFadeOut()
		{
			this.Dispose();
			this.fadeOutDisposable = new CompositeDisposable();
			IEnumerator _coroutine = this.FadeOutCoroutine();
			Observable.FromCoroutine(() => _coroutine, false).TakeUntilDisable(this).Subscribe<Unit>().AddTo(this.fadeOutDisposable);
		}

		// Token: 0x06008839 RID: 34873 RVA: 0x0038BEB0 File Offset: 0x0038A2B0
		private IEnumerator FadeOutCoroutine()
		{
			IConnectableObservable<TimeInterval<float>> _fadeOutStream = ObservableEasing.EaseInQuint(this.fadeOutInfo.Time, true).TakeUntilDisable(this).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			Vector3 _startScale = this.LocalScale;
			Vector3 _endScale = Vector3.one * this.fadeOutInfo.Scale;
			float _startAlpha = this.CanvasAlpha;
			float _endAlpha = this.fadeOutInfo.Alpha;
			_fadeOutStream.Connect();
			_fadeOutStream.Subscribe(delegate(TimeInterval<float> x)
			{
				float value = x.Value;
				this.CanvasAlpha = Mathf.Lerp(_startAlpha, _endAlpha, value);
				this.LocalScale = Vector3.Lerp(_startScale, _endScale, value);
			}).AddTo(this.fadeOutDisposable);
			yield return Observable.WhenAll<TimeInterval<float>>(new IObservable<TimeInterval<float>>[]
			{
				_fadeOutStream
			}).TakeUntilDisable(this).ToYieldInstruction<TimeInterval<float>[]>().AddTo(this.fadeOutDisposable);
			this.fadeOutDisposable = null;
			Action<WarningMessageElement> endAction = this.EndAction;
			if (endAction != null)
			{
				endAction(this);
			}
			Action closedAction = this.ClosedAction;
			if (closedAction != null)
			{
				closedAction();
			}
			yield break;
		}

		// Token: 0x04006E5F RID: 28255
		[SerializeField]
		private RectTransform myTransform;

		// Token: 0x04006E60 RID: 28256
		[SerializeField]
		private CanvasGroup canvasGroup;

		// Token: 0x04006E61 RID: 28257
		[SerializeField]
		private Text messageText;

		// Token: 0x04006E63 RID: 28259
		private FadeInfo fadeInInfo = default(FadeInfo);

		// Token: 0x04006E64 RID: 28260
		private FadeInfo displayInfo = default(FadeInfo);

		// Token: 0x04006E65 RID: 28261
		private FadeInfo fadeOutInfo = default(FadeInfo);

		// Token: 0x04006E68 RID: 28264
		private CompositeDisposable fadeInDisposable;

		// Token: 0x04006E69 RID: 28265
		private CompositeDisposable displayDisposable;

		// Token: 0x04006E6A RID: 28266
		private CompositeDisposable fadeOutDisposable;
	}
}
