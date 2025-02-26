using System;
using System.Collections;
using AIProject.SaveData;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI.Recycling
{
	// Token: 0x02000EA0 RID: 3744
	[RequireComponent(typeof(CanvasGroup))]
	[RequireComponent(typeof(RectTransform))]
	public class RecyclingCreatePanelUI : MenuUIBehaviour
	{
		// Token: 0x170017CC RID: 6092
		// (get) Token: 0x0600793F RID: 31039 RVA: 0x00330EF8 File Offset: 0x0032F2F8
		// (set) Token: 0x06007940 RID: 31040 RVA: 0x00330F20 File Offset: 0x0032F320
		public float CanvasAlpha
		{
			get
			{
				return (!(this._canvasGroup != null)) ? 0f : this._canvasGroup.alpha;
			}
			private set
			{
				if (this._canvasGroup != null)
				{
					this._canvasGroup.alpha = value;
				}
			}
		}

		// Token: 0x170017CD RID: 6093
		// (get) Token: 0x06007941 RID: 31041 RVA: 0x00330F3F File Offset: 0x0032F33F
		// (set) Token: 0x06007942 RID: 31042 RVA: 0x00330F63 File Offset: 0x0032F363
		public bool BlockRaycast
		{
			get
			{
				return this._canvasGroup != null && this._canvasGroup.blocksRaycasts;
			}
			private set
			{
				if (this._canvasGroup != null && this._canvasGroup.blocksRaycasts != value)
				{
					this._canvasGroup.blocksRaycasts = value;
				}
			}
		}

		// Token: 0x170017CE RID: 6094
		// (get) Token: 0x06007943 RID: 31043 RVA: 0x00330F93 File Offset: 0x0032F393
		// (set) Token: 0x06007944 RID: 31044 RVA: 0x00330FB7 File Offset: 0x0032F3B7
		public bool Interactable
		{
			get
			{
				return this._canvasGroup != null && this._canvasGroup.interactable;
			}
			private set
			{
				if (this._canvasGroup != null && this._canvasGroup.interactable != value)
				{
					this._canvasGroup.interactable = value;
				}
			}
		}

		// Token: 0x06007945 RID: 31045 RVA: 0x00330FE8 File Offset: 0x0032F3E8
		protected override void Start()
		{
			base.Start();
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			}).AddTo(this);
			(from _ in Observable.EveryLateUpdate()
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUIUpdate();
			}).AddTo(this);
		}

		// Token: 0x06007946 RID: 31046 RVA: 0x00331047 File Offset: 0x0032F447
		public void DoOpen()
		{
			this.IsActiveControl = true;
		}

		// Token: 0x06007947 RID: 31047 RVA: 0x00331050 File Offset: 0x0032F450
		public void DoClose()
		{
			this.IsActiveControl = false;
		}

		// Token: 0x06007948 RID: 31048 RVA: 0x0033105C File Offset: 0x0032F45C
		public void DoForceOpen()
		{
			this.IsActiveControl = true;
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this.CanvasAlpha = 1f;
			bool flag = true;
			this.Interactable = flag;
			flag = flag;
			base.EnabledInput = flag;
			this.BlockRaycast = flag;
		}

		// Token: 0x06007949 RID: 31049 RVA: 0x003310AC File Offset: 0x0032F4AC
		public void DoForceClose()
		{
			this.IsActiveControl = false;
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this.CanvasAlpha = 0f;
			bool flag = false;
			this.Interactable = flag;
			flag = flag;
			base.EnabledInput = flag;
			this.BlockRaycast = flag;
		}

		// Token: 0x0600794A RID: 31050 RVA: 0x003310FC File Offset: 0x0032F4FC
		private void SetActiveControl(bool active)
		{
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			IEnumerator coroutine = (!active) ? this.CloseCoroutine() : this.OpenCoroutine();
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>().AddTo(this);
		}

		// Token: 0x0600794B RID: 31051 RVA: 0x00331168 File Offset: 0x0032F568
		private IEnumerator OpenCoroutine()
		{
			this.SetActive(base.gameObject, true);
			this.BlockRaycast = true;
			bool flag = false;
			this.Interactable = flag;
			base.EnabledInput = flag;
			float startAlpha = this.CanvasAlpha;
			yield return ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			}).ToYieldInstruction<TimeInterval<float>>();
			this.CanvasAlpha = 1f;
			flag = true;
			base.EnabledInput = flag;
			this.Interactable = flag;
			yield break;
		}

		// Token: 0x0600794C RID: 31052 RVA: 0x00331184 File Offset: 0x0032F584
		private IEnumerator CloseCoroutine()
		{
			bool flag = false;
			this.Interactable = flag;
			base.EnabledInput = flag;
			float startAlpha = this.CanvasAlpha;
			yield return ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			}).ToYieldInstruction<TimeInterval<float>>();
			this.CanvasAlpha = 0f;
			this.BlockRaycast = false;
			this.SetActive(base.gameObject, false);
			yield break;
		}

		// Token: 0x0600794D RID: 31053 RVA: 0x003311A0 File Offset: 0x0032F5A0
		private void OnUIUpdate()
		{
			RecyclingData recyclingData = this._recyclingUI.RecyclingData;
			if (recyclingData == null)
			{
				return;
			}
			float countLimit = this._recyclingUI.DecidedItemSlotUI.CountLimit;
			if (!recyclingData.CreateCountEnabled)
			{
				Vector3 localScale = this._countBarImage.transform.localScale;
				localScale.x = 0f;
				this._countBarImage.transform.localScale = localScale;
			}
			else if (countLimit <= 0f)
			{
				Vector3 localScale2 = this._countBarImage.transform.localScale;
				localScale2.x = 1f;
				this._countBarImage.transform.localScale = localScale2;
			}
			else
			{
				Vector3 localScale3 = this._countBarImage.transform.localScale;
				localScale3.x = Mathf.Clamp01(recyclingData.CreateCounter / countLimit);
				this._countBarImage.transform.localScale = localScale3;
			}
		}

		// Token: 0x0600794E RID: 31054 RVA: 0x00331285 File Offset: 0x0032F685
		private void SetActive(GameObject obj, bool flag)
		{
			if (obj != null && obj.activeSelf != flag)
			{
				obj.SetActive(flag);
			}
		}

		// Token: 0x040061D9 RID: 25049
		[SerializeField]
		private RecyclingUI _recyclingUI;

		// Token: 0x040061DA RID: 25050
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x040061DB RID: 25051
		[SerializeField]
		private RectTransform _rectTransform;

		// Token: 0x040061DC RID: 25052
		[SerializeField]
		private Image _countBarImage;

		// Token: 0x040061DD RID: 25053
		private IDisposable _fadeDisposable;
	}
}
