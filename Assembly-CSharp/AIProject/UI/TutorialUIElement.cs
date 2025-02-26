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
	// Token: 0x02000FE1 RID: 4065
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasGroup))]
	[RequireComponent(typeof(Image))]
	public class TutorialUIElement : UIBehaviour
	{
		// Token: 0x17001DB9 RID: 7609
		// (get) Token: 0x06008806 RID: 34822 RVA: 0x0038B208 File Offset: 0x00389608
		public Image BackImage
		{
			[CompilerGenerated]
			get
			{
				return this._backImage;
			}
		}

		// Token: 0x17001DBA RID: 7610
		// (get) Token: 0x06008807 RID: 34823 RVA: 0x0038B210 File Offset: 0x00389610
		// (set) Token: 0x06008808 RID: 34824 RVA: 0x0038B218 File Offset: 0x00389618
		public int MyPageNum
		{
			get
			{
				return this._myPageNum;
			}
			set
			{
				this._myPageNum = value;
				this._myPageText.text = string.Format("{0}", this._myPageNum);
			}
		}

		// Token: 0x17001DBB RID: 7611
		// (get) Token: 0x06008809 RID: 34825 RVA: 0x0038B241 File Offset: 0x00389641
		// (set) Token: 0x0600880A RID: 34826 RVA: 0x0038B249 File Offset: 0x00389649
		public int MaxPageNum
		{
			get
			{
				return this._maxPageNum;
			}
			set
			{
				this._maxPageNum = value;
				this._pageMaxText.text = string.Format("{0}", this._maxPageNum);
			}
		}

		// Token: 0x17001DBC RID: 7612
		// (get) Token: 0x0600880B RID: 34827 RVA: 0x0038B272 File Offset: 0x00389672
		// (set) Token: 0x0600880C RID: 34828 RVA: 0x0038B29A File Offset: 0x0038969A
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

		// Token: 0x0600880D RID: 34829 RVA: 0x0038B2BC File Offset: 0x003896BC
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
			if (this._backImage == null)
			{
				this._backImage = base.GetComponent<Image>();
			}
			this._canvasGroup.blocksRaycasts = false;
		}

		// Token: 0x0600880E RID: 34830 RVA: 0x0038B332 File Offset: 0x00389732
		public void CloseForced()
		{
			this.Dispose();
			this.CanvasAlpha = 0f;
		}

		// Token: 0x17001DBD RID: 7613
		// (get) Token: 0x0600880F RID: 34831 RVA: 0x0038B345 File Offset: 0x00389745
		public bool IsOpening
		{
			[CompilerGenerated]
			get
			{
				return this._openEnumerator != null;
			}
		}

		// Token: 0x17001DBE RID: 7614
		// (get) Token: 0x06008810 RID: 34832 RVA: 0x0038B353 File Offset: 0x00389753
		public bool IsClosing
		{
			[CompilerGenerated]
			get
			{
				return this._closeEnumerator != null;
			}
		}

		// Token: 0x06008811 RID: 34833 RVA: 0x0038B361 File Offset: 0x00389761
		public void Dispose()
		{
			this._allDisposable.Clear();
			this._openEnumerator = null;
			this._closeEnumerator = null;
		}

		// Token: 0x06008812 RID: 34834 RVA: 0x0038B37C File Offset: 0x0038977C
		public void Open(float fadeTime, float moveX)
		{
			if (this.IsOpening)
			{
				return;
			}
			IEnumerator openEnumerator;
			if (this.IsClosing)
			{
				this.Dispose();
				openEnumerator = this.ReOpenCoroutine(fadeTime);
			}
			else
			{
				openEnumerator = this.OpenCoroutine(fadeTime, moveX);
			}
			this._openEnumerator = openEnumerator;
			Observable.FromCoroutine(() => this._openEnumerator, false).Subscribe<Unit>().AddTo(this._allDisposable);
		}

		// Token: 0x06008813 RID: 34835 RVA: 0x0038B3E8 File Offset: 0x003897E8
		private IEnumerator OpenCoroutine(float fadeTime, float moveX)
		{
			this.SetPosY(0f);
			this.SetPosX(moveX);
			float startX = this.GetPosX();
			float alpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.EaseOutExpo(fadeTime, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(alpha, 1f, x.Value);
				this.SetPosX(Mathf.Lerp(startX, 0f, x.Value));
			}).AddTo(this._allDisposable);
			stream.Connect().AddTo(this._allDisposable);
			yield return stream.ToYieldInstruction<TimeInterval<float>>().AddTo(this._allDisposable);
			this.CanvasAlpha = 1f;
			this.SetPosX(0f);
			this.SetPosY(0f);
			this._openEnumerator = null;
			this._allDisposable.Clear();
			yield break;
		}

		// Token: 0x06008814 RID: 34836 RVA: 0x0038B414 File Offset: 0x00389814
		private IEnumerator ReOpenCoroutine(float fadeTime)
		{
			this.SetPosY(0f);
			float startX = this.GetPosX();
			float alpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.EaseOutExpo(fadeTime, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(alpha, 1f, x.Value);
				this.SetPosX(Mathf.Lerp(startX, 0f, x.Value));
			}).AddTo(this._allDisposable);
			stream.Connect().AddTo(this._allDisposable);
			yield return stream.ToYieldInstruction<TimeInterval<float>>().AddTo(this._allDisposable);
			this.CanvasAlpha = 1f;
			this.SetPosX(0f);
			this.SetPosY(0f);
			this._openEnumerator = null;
			this._allDisposable.Clear();
			yield break;
		}

		// Token: 0x06008815 RID: 34837 RVA: 0x0038B438 File Offset: 0x00389838
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

		// Token: 0x06008816 RID: 34838 RVA: 0x0038B494 File Offset: 0x00389894
		private IEnumerator CloseCoroutine(float fadeTime, float moveX)
		{
			this.SetPosY(0f);
			float startX = this.GetPosX();
			float alpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.EaseOutExpo(fadeTime, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(alpha, 0f, x.Value);
				this.SetPosX(Mathf.Lerp(startX, -moveX, x.Value));
			}).AddTo(this._allDisposable);
			stream.Connect().AddTo(this._allDisposable);
			yield return stream.ToYieldInstruction<TimeInterval<float>>().AddTo(this._allDisposable);
			this.CanvasAlpha = 0f;
			this._closeEnumerator = null;
			this._allDisposable.Clear();
			yield break;
		}

		// Token: 0x06008817 RID: 34839 RVA: 0x0038B4C0 File Offset: 0x003898C0
		private float GetPosX()
		{
			return this._rectTransform.localPosition.x;
		}

		// Token: 0x06008818 RID: 34840 RVA: 0x0038B4E0 File Offset: 0x003898E0
		private void SetPosX(float posX)
		{
			Vector3 localPosition = this._rectTransform.localPosition;
			localPosition.x = posX;
			this._rectTransform.localPosition = localPosition;
		}

		// Token: 0x06008819 RID: 34841 RVA: 0x0038B510 File Offset: 0x00389910
		private float GetPosY()
		{
			return this._rectTransform.localPosition.y;
		}

		// Token: 0x0600881A RID: 34842 RVA: 0x0038B530 File Offset: 0x00389930
		private void SetPosY(float posY)
		{
			Vector3 localPosition = this._rectTransform.localPosition;
			localPosition.y = posY;
			this._rectTransform.localPosition = localPosition;
		}

		// Token: 0x04006E55 RID: 28245
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006E56 RID: 28246
		[SerializeField]
		private RectTransform _rectTransform;

		// Token: 0x04006E57 RID: 28247
		[SerializeField]
		private Image _backImage;

		// Token: 0x04006E58 RID: 28248
		[SerializeField]
		private Text _myPageText;

		// Token: 0x04006E59 RID: 28249
		[SerializeField]
		private Text _pageMaxText;

		// Token: 0x04006E5A RID: 28250
		private CompositeDisposable _allDisposable = new CompositeDisposable();

		// Token: 0x04006E5B RID: 28251
		private int _myPageNum = -1;

		// Token: 0x04006E5C RID: 28252
		private int _maxPageNum;

		// Token: 0x04006E5D RID: 28253
		private IEnumerator _openEnumerator;

		// Token: 0x04006E5E RID: 28254
		private IEnumerator _closeEnumerator;
	}
}
