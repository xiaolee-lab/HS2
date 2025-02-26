using System;
using System.Collections;
using System.Collections.Generic;
using ConfigScene;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000FD1 RID: 4049
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasGroup))]
	public class PhotoShotUI : MenuUIBehaviour
	{
		// Token: 0x17001D5E RID: 7518
		// (get) Token: 0x0600869B RID: 34459 RVA: 0x00383971 File Offset: 0x00381D71
		// (set) Token: 0x0600869C RID: 34460 RVA: 0x0038397E File Offset: 0x00381D7E
		public override bool IsActiveControl
		{
			get
			{
				return this._isActive.Value;
			}
			set
			{
				if (this._isActive.Value == value)
				{
					return;
				}
				this._isActive.Value = value;
			}
		}

		// Token: 0x0600869D RID: 34461 RVA: 0x0038399E File Offset: 0x00381D9E
		protected override void OnBeforeStart()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			this._lerpStream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).TakeUntilDestroy(this);
		}

		// Token: 0x0600869E RID: 34462 RVA: 0x003839D6 File Offset: 0x00381DD6
		protected override void OnAfterStart()
		{
			if (this._canvasGroup.blocksRaycasts)
			{
				this._canvasGroup.blocksRaycasts = false;
			}
			if (this._canvasGroup.interactable)
			{
				this._canvasGroup.interactable = false;
			}
		}

		// Token: 0x0600869F RID: 34463 RVA: 0x00383A10 File Offset: 0x00381E10
		private void SetActiveControl(bool active)
		{
			if (active)
			{
				this.UISetting();
			}
			IEnumerator fadeCoroutine = (!active) ? this.CloseCoroutine() : this.OpenCoroutine();
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = Observable.FromCoroutine(() => fadeCoroutine, false).TakeUntilDestroy(this).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
				if (global::Debug.isDebugBuild)
				{
				}
			});
		}

		// Token: 0x060086A0 RID: 34464 RVA: 0x00383AC4 File Offset: 0x00381EC4
		private IEnumerator OpenCoroutine()
		{
			float startAlpha = this._canvasGroup.alpha;
			IConnectableObservable<TimeInterval<float>> stream = this._lerpStream.Publish<TimeInterval<float>>();
			stream.Connect();
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			yield break;
		}

		// Token: 0x060086A1 RID: 34465 RVA: 0x00383AE0 File Offset: 0x00381EE0
		private IEnumerator CloseCoroutine()
		{
			float startAlpha = this._canvasGroup.alpha;
			IConnectableObservable<TimeInterval<float>> stream = this._lerpStream.Publish<TimeInterval<float>>();
			stream.Connect();
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			yield break;
		}

		// Token: 0x060086A2 RID: 34466 RVA: 0x00383AFC File Offset: 0x00381EFC
		private void ElementSetting(PhotoShotUI.GuidObject obj, Sprite icon, string text)
		{
			if (obj == null)
			{
				return;
			}
			if (obj.image != null && obj.image.gameObject != null)
			{
				obj.image.sprite = icon;
				this.SetActive(obj.image, this._displayGuide);
			}
			if (obj.text != null && obj.text.gameObject != null)
			{
				obj.text.text = text;
				this.SetActive(obj.text, this._displayGuide);
			}
		}

		// Token: 0x060086A3 RID: 34467 RVA: 0x00383B9C File Offset: 0x00381F9C
		private void UISetting()
		{
			GameConfigSystem gameData = Config.GameData;
			this._displayGuide = (gameData == null || gameData.ActionGuide);
			if (Singleton<Manager.Resources>.IsInstance())
			{
				Dictionary<int, Sprite> inputIconTable = Singleton<Manager.Resources>.Instance.itemIconTables.InputIconTable;
				Sprite icon;
				inputIconTable.TryGetValue(0, out icon);
				this.ElementSetting(this._guidElements.GetElement(0), icon, "移動");
				inputIconTable.TryGetValue(2, out icon);
				this.ElementSetting(this._guidElements.GetElement(1), icon, "ズーム");
				inputIconTable.TryGetValue(6, out icon);
				this.ElementSetting(this._guidElements.GetElement(2), icon, "撮影");
				inputIconTable.TryGetValue(1, out icon);
				this.ElementSetting(this._guidElements.GetElement(3), icon, "終了");
			}
		}

		// Token: 0x060086A4 RID: 34468 RVA: 0x00383C69 File Offset: 0x00382069
		public void SetZoomValue(float value)
		{
			if (this._zoomValueBar == null)
			{
				return;
			}
			this._zoomValueBar.value = Mathf.Clamp01(value);
		}

		// Token: 0x060086A5 RID: 34469 RVA: 0x00383C8E File Offset: 0x0038208E
		private void SetActive(GameObject obj, bool active)
		{
			if (obj == null)
			{
				return;
			}
			if (obj.activeSelf != active)
			{
				obj.SetActive(active);
			}
		}

		// Token: 0x060086A6 RID: 34470 RVA: 0x00383CB0 File Offset: 0x003820B0
		private void SetActive(Component com, bool active)
		{
			if (com == null || com.gameObject == null)
			{
				return;
			}
			if (com.gameObject.activeSelf != active)
			{
				com.gameObject.SetActive(active);
			}
		}

		// Token: 0x04006D79 RID: 28025
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006D7A RID: 28026
		[SerializeField]
		private PhotoShotUI.GuidObject[] _guidElements = new PhotoShotUI.GuidObject[0];

		// Token: 0x04006D7B RID: 28027
		[SerializeField]
		private Scrollbar _zoomValueBar;

		// Token: 0x04006D7C RID: 28028
		private bool _displayGuide = true;

		// Token: 0x04006D7D RID: 28029
		private IObservable<TimeInterval<float>> _lerpStream;

		// Token: 0x04006D7E RID: 28030
		private IDisposable _fadeDisposable;

		// Token: 0x02000FD2 RID: 4050
		[Serializable]
		public class GuidObject
		{
			// Token: 0x04006D81 RID: 28033
			public Text text;

			// Token: 0x04006D82 RID: 28034
			public Image image;
		}
	}
}
