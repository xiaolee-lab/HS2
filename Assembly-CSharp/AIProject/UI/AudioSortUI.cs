using System;
using System.Collections;
using System.Runtime.CompilerServices;
using AIProject.Animal;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E2D RID: 3629
	[RequireComponent(typeof(CanvasGroup))]
	[RequireComponent(typeof(RectTransform))]
	public class AudioSortUI : MenuUIBehaviour
	{
		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x06007179 RID: 29049 RVA: 0x00305FC3 File Offset: 0x003043C3
		public Toggle[] Toggles
		{
			[CompilerGenerated]
			get
			{
				return this._toggles;
			}
		}

		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x0600717A RID: 29050 RVA: 0x00305FCB File Offset: 0x003043CB
		// (set) Token: 0x0600717B RID: 29051 RVA: 0x00305FF3 File Offset: 0x003043F3
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

		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x0600717C RID: 29052 RVA: 0x00306012 File Offset: 0x00304412
		public bool InputEnabled
		{
			[CompilerGenerated]
			get
			{
				return base.EnabledInput && this._focusLevel == Singleton<Manager.Input>.Instance.FocusLevel;
			}
		}

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x0600717D RID: 29053 RVA: 0x00306034 File Offset: 0x00304434
		// (set) Token: 0x0600717E RID: 29054 RVA: 0x0030603C File Offset: 0x0030443C
		public Action<int> ToggleIndexChanged { get; set; }

		// Token: 0x0600717F RID: 29055 RVA: 0x00306048 File Offset: 0x00304448
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
		}

		// Token: 0x06007180 RID: 29056 RVA: 0x00306098 File Offset: 0x00304498
		protected override void OnBeforeStart()
		{
			base.OnBeforeStart();
			base.OnActiveChangedAsObservable().TakeUntilDestroy(this).Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			(from _ in this._closeButton.OnClickAsObservable()
			where this.InputEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this.DoClose();
			});
			this._toggles = ((this._toggleGroup != null) ? this._toggleGroup.GetComponentsInChildren<Toggle>(true) : null);
			if (!this._toggles.IsNullOrEmpty<Toggle>())
			{
				int i;
				for (i = 0; i < this._toggles.Length; i++)
				{
					(from flag in this._toggles[i].OnValueChangedAsObservable()
					where flag
					select flag into _
					where this.InputEnabled
					select _).Subscribe(delegate(bool _)
					{
						this.ChangeIndex(i);
					});
				}
			}
		}

		// Token: 0x06007181 RID: 29057 RVA: 0x003061B8 File Offset: 0x003045B8
		public int SortIndex()
		{
			if (!this._toggles.IsNullOrEmpty<Toggle>())
			{
				for (int i = 0; i < this._toggles.Length; i++)
				{
					Toggle toggle = this._toggles[i];
					if (toggle != null && toggle.isOn)
					{
						return i;
					}
				}
			}
			return 0;
		}

		// Token: 0x06007182 RID: 29058 RVA: 0x00306211 File Offset: 0x00304611
		protected override void OnAfterStart()
		{
			base.OnAfterStart();
			this.Hide();
			base.gameObject.SetActiveSelf(false);
		}

		// Token: 0x06007183 RID: 29059 RVA: 0x0030622B File Offset: 0x0030462B
		public void Hide()
		{
			if (this._canvasGroup == null)
			{
				return;
			}
			this._canvasGroup.SetBlocksRaycasts(false);
			this._canvasGroup.SetInteractable(false);
			this._canvasGroup.alpha = 0f;
		}

		// Token: 0x06007184 RID: 29060 RVA: 0x00306269 File Offset: 0x00304669
		private void DoClose()
		{
			this.IsActiveControl = false;
		}

		// Token: 0x06007185 RID: 29061 RVA: 0x00306274 File Offset: 0x00304674
		private void SetActiveControl(bool active)
		{
			this._allDisposable.Clear();
			IEnumerator coroutine = (!active) ? this.CloseCoroutine() : this.OpenCoroutine();
			Observable.FromCoroutine(() => coroutine, false).TakeUntilDestroy(this).Subscribe<Unit>().AddTo(this._allDisposable);
		}

		// Token: 0x06007186 RID: 29062 RVA: 0x003062D8 File Offset: 0x003046D8
		private IEnumerator OpenCoroutine()
		{
			base.gameObject.SetActiveSelf(true);
			this._canvasGroup.SetBlocksRaycasts(true);
			this._canvasGroup.SetInteractable(false);
			float startAlpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).TakeUntilDestroy(this).Publish<TimeInterval<float>>();
			stream.Connect().AddTo(this._allDisposable);
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			}).AddTo(this._allDisposable);
			yield return stream.ToYieldInstruction<TimeInterval<float>>().AddTo(this._allDisposable);
			this._canvasGroup.SetInteractable(true);
			base.EnabledInput = true;
			yield break;
		}

		// Token: 0x06007187 RID: 29063 RVA: 0x003062F4 File Offset: 0x003046F4
		private IEnumerator CloseCoroutine()
		{
			base.EnabledInput = false;
			this._canvasGroup.SetInteractable(false);
			float startAlpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).TakeUntilDestroy(this).Publish<TimeInterval<float>>();
			stream.Connect().AddTo(this._allDisposable);
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			}).AddTo(this._allDisposable);
			yield return stream.ToYieldInstruction<TimeInterval<float>>().AddTo(this._allDisposable);
			this._canvasGroup.SetBlocksRaycasts(false);
			base.gameObject.SetActiveSelf(false);
			yield break;
		}

		// Token: 0x06007188 RID: 29064 RVA: 0x0030630F File Offset: 0x0030470F
		private void ChangeIndex(int index)
		{
			Action<int> toggleIndexChanged = this.ToggleIndexChanged;
			if (toggleIndexChanged != null)
			{
				toggleIndexChanged(index);
			}
		}

		// Token: 0x06007189 RID: 29065 RVA: 0x00306328 File Offset: 0x00304728
		private void PlaySE(SoundPack.SystemSE se)
		{
			SoundPack soundPack = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.SoundPack;
			if (soundPack != null)
			{
				soundPack.Play(se);
			}
		}

		// Token: 0x04005D09 RID: 23817
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04005D0A RID: 23818
		[SerializeField]
		private RectTransform _rectTransform;

		// Token: 0x04005D0B RID: 23819
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04005D0C RID: 23820
		[SerializeField]
		private ToggleGroup _toggleGroup;

		// Token: 0x04005D0D RID: 23821
		[SerializeField]
		private JukeBoxUI _mainUI;

		// Token: 0x04005D0E RID: 23822
		[SerializeField]
		private JukeBoxAudioListUI _listUI;

		// Token: 0x04005D0F RID: 23823
		private Toggle[] _toggles = new Toggle[0];

		// Token: 0x04005D11 RID: 23825
		private CompositeDisposable _allDisposable = new CompositeDisposable();

		// Token: 0x02000E2E RID: 3630
		// (Invoke) Token: 0x0600718F RID: 29071
		private delegate void ChangedFunk(int index);
	}
}
