using System;
using System.Collections;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI
{
	// Token: 0x02000FEC RID: 4076
	public class NotifyingUI : MenuUIBehaviour
	{
		// Token: 0x17001E04 RID: 7684
		// (get) Token: 0x06008920 RID: 35104 RVA: 0x00390D7D File Offset: 0x0038F17D
		// (set) Token: 0x06008921 RID: 35105 RVA: 0x00390D85 File Offset: 0x0038F185
		public PointerClickTrigger ClickTrigger { get; private set; }

		// Token: 0x06008922 RID: 35106 RVA: 0x00390D90 File Offset: 0x0038F190
		protected override void Start()
		{
			this.ClickTrigger = this._raycastTarget.gameObject.AddComponent<PointerClickTrigger>();
			this.ClickTrigger.enabled = false;
			UITrigger.TriggerEvent triggerEvent = new UITrigger.TriggerEvent();
			triggerEvent.AddListener(delegate(BaseEventData _)
			{
				this.OnClick();
			});
			this.ClickTrigger.Triggers.Add(triggerEvent);
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			base.Start();
			base.gameObject.SetActive(false);
		}

		// Token: 0x06008923 RID: 35107 RVA: 0x00390E12 File Offset: 0x0038F212
		private void OnClick()
		{
			this._isActive.Value = false;
		}

		// Token: 0x06008924 RID: 35108 RVA: 0x00390E20 File Offset: 0x0038F220
		public void Display(string text)
		{
			this._contentText.text = text;
			this._isActive.Value = true;
		}

		// Token: 0x06008925 RID: 35109 RVA: 0x00390E3A File Offset: 0x0038F23A
		public void Hide()
		{
			this._isActive.Value = false;
		}

		// Token: 0x06008926 RID: 35110 RVA: 0x00390E48 File Offset: 0x0038F248
		private void SetActiveControl(bool isActive)
		{
			if (isActive)
			{
				base.gameObject.SetActive(isActive);
			}
			IEnumerator coroutine;
			if (isActive)
			{
				coroutine = this.DoOpen();
			}
			else
			{
				coroutine = this.DoClose();
			}
			this._fadeStream = Observable.FromCoroutine(() => coroutine, false).PublishLast<Unit>();
			this._fadeStream.Connect();
		}

		// Token: 0x06008927 RID: 35111 RVA: 0x00390EBC File Offset: 0x0038F2BC
		private IEnumerator DoOpen()
		{
			if (base.EnabledInput)
			{
				base.EnabledInput = false;
			}
			if (this.PouchOpen.image.raycastTarget)
			{
				this.PouchOpen.image.raycastTarget = false;
			}
			if (this.NotGet.image.raycastTarget)
			{
				this.NotGet.image.raycastTarget = false;
			}
			if (!this._canvasGroup.blocksRaycasts)
			{
				this._canvasGroup.blocksRaycasts = true;
			}
			IObservable<TimeInterval<float>> lerpFadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true);
			IConnectableObservable<TimeInterval<float>> lerpConnSteram = lerpFadeInStream.Publish<TimeInterval<float>>();
			float startAlpha = this._canvasGroup.alpha;
			lerpConnSteram.Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			lerpConnSteram.Connect();
			yield return lerpConnSteram.ToYieldInstruction<TimeInterval<float>>();
			this.PouchOpen.image.raycastTarget = true;
			this.NotGet.image.raycastTarget = true;
			this.ClickTrigger.enabled = true;
			base.EnabledInput = true;
			yield break;
		}

		// Token: 0x06008928 RID: 35112 RVA: 0x00390ED8 File Offset: 0x0038F2D8
		private IEnumerator DoClose()
		{
			base.EnabledInput = false;
			this.ClickTrigger.enabled = false;
			this.PouchOpen.image.raycastTarget = false;
			this.NotGet.image.raycastTarget = false;
			IObservable<TimeInterval<float>> lerpFadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true);
			IConnectableObservable<TimeInterval<float>> lerpConnStream = lerpFadeInStream.Publish<TimeInterval<float>>();
			float startAlpha = this._canvasGroup.alpha;
			lerpConnStream.Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			lerpConnStream.Connect();
			yield return lerpConnStream.ToYieldInstruction<TimeInterval<float>>();
			this._canvasGroup.blocksRaycasts = false;
			base.gameObject.SetActive(false);
			yield break;
		}

		// Token: 0x04006F04 RID: 28420
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006F05 RID: 28421
		public Button PouchOpen;

		// Token: 0x04006F06 RID: 28422
		public Button NotGet;

		// Token: 0x04006F07 RID: 28423
		[SerializeField]
		private Text _contentText;

		// Token: 0x04006F08 RID: 28424
		[SerializeField]
		private Image _raycastTarget;

		// Token: 0x04006F09 RID: 28425
		private IConnectableObservable<Unit> _fadeStream;
	}
}
