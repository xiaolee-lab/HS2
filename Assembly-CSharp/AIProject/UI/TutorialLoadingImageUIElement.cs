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
	// Token: 0x02000FDF RID: 4063
	[RequireComponent(typeof(CanvasGroup))]
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(Image))]
	public class TutorialLoadingImageUIElement : UIBehaviour
	{
		// Token: 0x17001DA7 RID: 7591
		// (get) Token: 0x060087B2 RID: 34738 RVA: 0x003891D6 File Offset: 0x003875D6
		// (set) Token: 0x060087B3 RID: 34739 RVA: 0x003891DE File Offset: 0x003875DE
		public int Index { get; set; }

		// Token: 0x17001DA8 RID: 7592
		// (get) Token: 0x060087B4 RID: 34740 RVA: 0x003891E7 File Offset: 0x003875E7
		// (set) Token: 0x060087B5 RID: 34741 RVA: 0x003891EF File Offset: 0x003875EF
		public Action<TutorialLoadingImageUIElement> OnOpenEvent { get; set; }

		// Token: 0x17001DA9 RID: 7593
		// (get) Token: 0x060087B6 RID: 34742 RVA: 0x003891F8 File Offset: 0x003875F8
		// (set) Token: 0x060087B7 RID: 34743 RVA: 0x00389200 File Offset: 0x00387600
		public Action<TutorialLoadingImageUIElement> OnCloseEvent { get; set; }

		// Token: 0x17001DAA RID: 7594
		// (get) Token: 0x060087B8 RID: 34744 RVA: 0x00389209 File Offset: 0x00387609
		public bool IsOpening
		{
			[CompilerGenerated]
			get
			{
				return this._openEnumerator != null;
			}
		}

		// Token: 0x17001DAB RID: 7595
		// (get) Token: 0x060087B9 RID: 34745 RVA: 0x00389217 File Offset: 0x00387617
		public bool IsClosing
		{
			[CompilerGenerated]
			get
			{
				return this._closeEnumerator != null;
			}
		}

		// Token: 0x060087BA RID: 34746 RVA: 0x00389228 File Offset: 0x00387628
		protected override void Awake()
		{
			base.Awake();
			if (this._canvasGroup == null)
			{
				this._canvasGroup = base.GetComponent<CanvasGroup>();
			}
			if (this._rectTransform == null)
			{
				this._rectTransform = base.GetComponent<RectTransform>();
			}
			if (this._loadingImage == null)
			{
				this._loadingImage = base.GetComponent<Image>();
			}
		}

		// Token: 0x060087BB RID: 34747 RVA: 0x00389292 File Offset: 0x00387692
		public void Dispose()
		{
			this._allDisposable.Clear();
			this._openEnumerator = null;
			this._closeEnumerator = null;
		}

		// Token: 0x060087BC RID: 34748 RVA: 0x003892AD File Offset: 0x003876AD
		public void ForcedClose()
		{
			this.Dispose();
			this.CanvasAlpha = 0f;
		}

		// Token: 0x17001DAC RID: 7596
		// (get) Token: 0x060087BD RID: 34749 RVA: 0x003892C0 File Offset: 0x003876C0
		// (set) Token: 0x060087BE RID: 34750 RVA: 0x003892E8 File Offset: 0x003876E8
		public float CanvasAlpha
		{
			get
			{
				return (!(this._canvasGroup != null)) ? 0f : this._canvasGroup.alpha;
			}
			set
			{
				if (this._canvasGroup != null)
				{
					this._canvasGroup.alpha = value;
				}
			}
		}

		// Token: 0x17001DAD RID: 7597
		// (get) Token: 0x060087BF RID: 34751 RVA: 0x00389307 File Offset: 0x00387707
		// (set) Token: 0x060087C0 RID: 34752 RVA: 0x00389314 File Offset: 0x00387714
		public Sprite ImageSprite
		{
			get
			{
				return this._loadingImage.sprite;
			}
			set
			{
				this._loadingImage.sprite = value;
			}
		}

		// Token: 0x060087C1 RID: 34753 RVA: 0x00389324 File Offset: 0x00387724
		public void Open(float fadeTime, float moveX)
		{
			if (this.IsOpening)
			{
				return;
			}
			if (this.IsClosing)
			{
				this.Dispose();
			}
			this._openEnumerator = this.OpenCoroutine(fadeTime, moveX);
			Observable.FromCoroutine(() => this._openEnumerator, false).Subscribe<Unit>().AddTo(this._allDisposable);
		}

		// Token: 0x060087C2 RID: 34754 RVA: 0x00389380 File Offset: 0x00387780
		private IEnumerator OpenCoroutine(float fadeTime, float moveX)
		{
			this.SetPos(moveX, 0f);
			float startX = this.GetPosX();
			float startAlpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.EaseOutExpo(fadeTime, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 1f, x.Value);
				this.SetPosX(Mathf.Lerp(startX, 0f, x.Value));
			}).AddTo(this._allDisposable);
			stream.Connect().AddTo(this._allDisposable);
			yield return stream.ToYieldInstruction<TimeInterval<float>>().AddTo(this._allDisposable);
			this.CanvasAlpha = 1f;
			this.SetPos(0f, 0f);
			this._openEnumerator = null;
			if (this.OnOpenEvent != null)
			{
				Action<TutorialLoadingImageUIElement> onOpenEvent = this.OnOpenEvent;
				this.OnOpenEvent = null;
				onOpenEvent(this);
			}
			yield break;
		}

		// Token: 0x060087C3 RID: 34755 RVA: 0x003893AC File Offset: 0x003877AC
		public void Close(float fadeTime, float moveX)
		{
			if (this.IsClosing)
			{
				return;
			}
			if (this.IsOpening)
			{
				this.Dispose();
			}
			this._closeEnumerator = this.CloseCoroutine(fadeTime, moveX);
			Observable.FromCoroutine(() => this._closeEnumerator, false).Subscribe<Unit>().AddTo(this._allDisposable);
		}

		// Token: 0x060087C4 RID: 34756 RVA: 0x00389408 File Offset: 0x00387808
		private IEnumerator CloseCoroutine(float fadeTime, float moveX)
		{
			this.SetPosY(0f);
			float startX = this.GetPosX();
			float startAlpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.EaseOutExpo(fadeTime, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 0f, x.Value);
				this.SetPosX(Mathf.Lerp(startX, -moveX, x.Value));
			}).AddTo(this._allDisposable);
			stream.Connect().AddTo(this._allDisposable);
			yield return stream.ToYieldInstruction<TimeInterval<float>>().AddTo(this._allDisposable);
			this.CanvasAlpha = 0f;
			this._closeEnumerator = null;
			if (this.OnCloseEvent != null)
			{
				Action<TutorialLoadingImageUIElement> onCloseEvent = this.OnCloseEvent;
				this.OnCloseEvent = null;
				onCloseEvent(this);
			}
			yield break;
		}

		// Token: 0x060087C5 RID: 34757 RVA: 0x00389434 File Offset: 0x00387834
		private float GetPosX()
		{
			return this._rectTransform.localPosition.x;
		}

		// Token: 0x060087C6 RID: 34758 RVA: 0x00389454 File Offset: 0x00387854
		private float GetPosY()
		{
			return this._rectTransform.localPosition.y;
		}

		// Token: 0x060087C7 RID: 34759 RVA: 0x00389474 File Offset: 0x00387874
		private void SetPos(float posX, float posY)
		{
			Vector3 localPosition = this._rectTransform.localPosition;
			localPosition.x = posX;
			localPosition.y = posY;
			this._rectTransform.localPosition = localPosition;
		}

		// Token: 0x060087C8 RID: 34760 RVA: 0x003894AC File Offset: 0x003878AC
		private void SetPosX(float posX)
		{
			Vector3 localPosition = this._rectTransform.localPosition;
			localPosition.x = posX;
			this._rectTransform.localPosition = localPosition;
		}

		// Token: 0x060087C9 RID: 34761 RVA: 0x003894DC File Offset: 0x003878DC
		private void SetPosY(float posY)
		{
			Vector3 localPosition = this._rectTransform.localPosition;
			localPosition.y = posY;
			this._rectTransform.localPosition = localPosition;
		}

		// Token: 0x04006E29 RID: 28201
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006E2A RID: 28202
		[SerializeField]
		private RectTransform _rectTransform;

		// Token: 0x04006E2B RID: 28203
		[SerializeField]
		private Image _loadingImage;

		// Token: 0x04006E2F RID: 28207
		private CompositeDisposable _allDisposable = new CompositeDisposable();

		// Token: 0x04006E30 RID: 28208
		private IEnumerator _openEnumerator;

		// Token: 0x04006E31 RID: 28209
		private IEnumerator _closeEnumerator;
	}
}
