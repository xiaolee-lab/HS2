using System;
using System.Collections;
using System.Runtime.CompilerServices;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.Animal
{
	// Token: 0x02000B8A RID: 2954
	public class AnimalNicknameUI : MonoBehaviour
	{
		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x060057F0 RID: 22512 RVA: 0x0025E7C4 File Offset: 0x0025CBC4
		public CanvasGroup CanvasGroup
		{
			[CompilerGenerated]
			get
			{
				return this._canvasGroup;
			}
		}

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x060057F1 RID: 22513 RVA: 0x0025E7CC File Offset: 0x0025CBCC
		public Text NicknameText
		{
			[CompilerGenerated]
			get
			{
				return this._nicknameText;
			}
		}

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x060057F2 RID: 22514 RVA: 0x0025E7D4 File Offset: 0x0025CBD4
		public Image BackImage
		{
			[CompilerGenerated]
			get
			{
				return this._backImage;
			}
		}

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x060057F3 RID: 22515 RVA: 0x0025E7DC File Offset: 0x0025CBDC
		public Transform RotateRoot
		{
			[CompilerGenerated]
			get
			{
				return this._rotateRoot;
			}
		}

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x060057F4 RID: 22516 RVA: 0x0025E7E4 File Offset: 0x0025CBE4
		// (set) Token: 0x060057F5 RID: 22517 RVA: 0x0025E80C File Offset: 0x0025CC0C
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

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x060057F6 RID: 22518 RVA: 0x0025E82B File Offset: 0x0025CC2B
		// (set) Token: 0x060057F7 RID: 22519 RVA: 0x0025E838 File Offset: 0x0025CC38
		public string Nickname
		{
			get
			{
				return this._nicknameText.text;
			}
			set
			{
				this._nicknameText.text = value;
			}
		}

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x060057F8 RID: 22520 RVA: 0x0025E846 File Offset: 0x0025CC46
		// (set) Token: 0x060057F9 RID: 22521 RVA: 0x0025E853 File Offset: 0x0025CC53
		public Color TextColor
		{
			get
			{
				return this._nicknameText.color;
			}
			set
			{
				this._nicknameText.color = value;
			}
		}

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x060057FA RID: 22522 RVA: 0x0025E864 File Offset: 0x0025CC64
		// (set) Token: 0x060057FB RID: 22523 RVA: 0x0025E884 File Offset: 0x0025CC84
		public float BackImageAlpha
		{
			get
			{
				return this._backImage.color.a;
			}
			set
			{
				Color color = this._backImage.color;
				color.a = value;
				this._backImage.color = color;
			}
		}

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x060057FC RID: 22524 RVA: 0x0025E8B1 File Offset: 0x0025CCB1
		// (set) Token: 0x060057FD RID: 22525 RVA: 0x0025E8B9 File Offset: 0x0025CCB9
		public Transform TargetObject { get; set; }

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x060057FE RID: 22526 RVA: 0x0025E8C2 File Offset: 0x0025CCC2
		// (set) Token: 0x060057FF RID: 22527 RVA: 0x0025E8CA File Offset: 0x0025CCCA
		public Camera TargetCamera { get; set; }

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x06005800 RID: 22528 RVA: 0x0025E8D3 File Offset: 0x0025CCD3
		// (set) Token: 0x06005801 RID: 22529 RVA: 0x0025E8DB File Offset: 0x0025CCDB
		public bool EnabledLateUpdate { get; private set; }

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x06005802 RID: 22530 RVA: 0x0025E8E4 File Offset: 0x0025CCE4
		public bool PlayingFade
		{
			[CompilerGenerated]
			get
			{
				return 0 < this._fadeDisposable.Count;
			}
		}

		// Token: 0x06005803 RID: 22531 RVA: 0x0025E8F4 File Offset: 0x0025CCF4
		public void PlayFadeIn(float fadeTime)
		{
			this._fadeDisposable.Clear();
			IEnumerator coroutine = this.FadeIn(fadeTime);
			Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>().AddTo(this._fadeDisposable);
		}

		// Token: 0x06005804 RID: 22532 RVA: 0x0025E944 File Offset: 0x0025CD44
		private IEnumerator FadeIn(float fadeTime)
		{
			this.EnabledLateUpdate = true;
			float startAlpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(fadeTime, true).FrameTimeInterval(true).TakeUntilDestroy(this).Publish<TimeInterval<float>>();
			stream.Connect().AddTo(this._fadeDisposable);
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			}).AddTo(this._fadeDisposable);
			yield return stream.ToYieldInstruction<TimeInterval<float>>().AddTo(this._fadeDisposable);
			this.CanvasAlpha = 1f;
			this._fadeDisposable.Clear();
			yield break;
		}

		// Token: 0x06005805 RID: 22533 RVA: 0x0025E968 File Offset: 0x0025CD68
		public void PlayFadeOut(float fadeTime)
		{
			this._fadeDisposable.Clear();
			IEnumerator coroutine = this.FadeOut(fadeTime);
			Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>().AddTo(this._fadeDisposable);
		}

		// Token: 0x06005806 RID: 22534 RVA: 0x0025E9B8 File Offset: 0x0025CDB8
		private IEnumerator FadeOut(float fadeTime)
		{
			float startAlpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(fadeTime, true).FrameTimeInterval(true).TakeUntilDestroy(this).Publish<TimeInterval<float>>();
			stream.Connect().AddTo(this._fadeDisposable);
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>().AddTo(this._fadeDisposable);
			this.CanvasAlpha = 0f;
			this._fadeDisposable.Clear();
			this.EnabledLateUpdate = false;
			yield break;
		}

		// Token: 0x06005807 RID: 22535 RVA: 0x0025E9DC File Offset: 0x0025CDDC
		private void LateUpdate()
		{
			if (!this.EnabledLateUpdate)
			{
				return;
			}
			if (this.TargetCamera == null || this.TargetObject == null)
			{
				return;
			}
			base.transform.position = this.TargetCamera.WorldToScreenPoint(this.TargetObject.position);
		}

		// Token: 0x040050D5 RID: 20693
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x040050D6 RID: 20694
		[SerializeField]
		private Text _nicknameText;

		// Token: 0x040050D7 RID: 20695
		[SerializeField]
		private Image _backImage;

		// Token: 0x040050D8 RID: 20696
		[SerializeField]
		private Transform _rotateRoot;

		// Token: 0x040050DC RID: 20700
		private CompositeDisposable _fadeDisposable = new CompositeDisposable();
	}
}
