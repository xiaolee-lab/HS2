using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ReMotion;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000E72 RID: 3698
	[RequireComponent(typeof(CanvasGroup))]
	public class FadeCanvas : SerializedMonoBehaviour
	{
		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x060075B4 RID: 30132 RVA: 0x0031DFE1 File Offset: 0x0031C3E1
		public Dictionary<FadeCanvas.PanelType, FadeItem> Table
		{
			[CompilerGenerated]
			get
			{
				return this._table;
			}
		}

		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x060075B5 RID: 30133 RVA: 0x0031DFE9 File Offset: 0x0031C3E9
		public bool IsFading
		{
			get
			{
				return this._stream != null;
			}
		}

		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x060075B6 RID: 30134 RVA: 0x0031DFF7 File Offset: 0x0031C3F7
		public bool IsFadeIn
		{
			get
			{
				return this.IsFading && this._fadeType == FadeType.In;
			}
		}

		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x060075B7 RID: 30135 RVA: 0x0031E010 File Offset: 0x0031C410
		public bool IsFadeOut
		{
			get
			{
				return this.IsFading && this._fadeType == FadeType.Out;
			}
		}

		// Token: 0x060075B8 RID: 30136 RVA: 0x0031E02C File Offset: 0x0031C42C
		public FadeItem GetPanel(FadeCanvas.PanelType type)
		{
			FadeItem result = null;
			foreach (FadeCanvas.PanelType panelType in FadeCanvas._typeKeys)
			{
				FadeItem fadeItem;
				if (this._table.TryGetValue(panelType, out fadeItem))
				{
					fadeItem.Graphic.gameObject.SetActive(panelType == type);
					if (fadeItem.Graphic.gameObject.activeSelf)
					{
						result = fadeItem;
					}
				}
			}
			return result;
		}

		// Token: 0x060075B9 RID: 30137 RVA: 0x0031E0A0 File Offset: 0x0031C4A0
		public IObservable<Unit> StartFade(FadeCanvas.PanelType type, FadeType fade, float duration, bool ignoreTimeScale)
		{
			if (this.IsFading)
			{
				return null;
			}
			this._fadeType = fade;
			FadeItem fadeItem = null;
			foreach (FadeCanvas.PanelType panelType in FadeCanvas._typeKeys)
			{
				FadeItem fadeItem2;
				if (this._table.TryGetValue(panelType, out fadeItem2))
				{
					fadeItem2.Graphic.gameObject.SetActive(panelType == type);
					if (fadeItem2.Graphic.gameObject.activeSelf)
					{
						fadeItem = fadeItem2;
					}
				}
			}
			if (fadeItem == null)
			{
				return null;
			}
			this._currentPanel = fadeItem;
			IConnectableObservable<Unit> connectableObservable = Observable.FromCoroutine(() => this.FadeCoroutine(duration, ignoreTimeScale), false).Publish<Unit>();
			this._disposable = connectableObservable.Connect();
			return this._stream = connectableObservable;
		}

		// Token: 0x060075BA RID: 30138 RVA: 0x0031E190 File Offset: 0x0031C590
		private IEnumerator FadeCoroutine(float duration, bool ignoreTimeScale)
		{
			IObservable<TimeInterval<float>> stream = ObservableEasing.Linear(duration, ignoreTimeScale).FrameTimeInterval(ignoreTimeScale).Do(delegate(TimeInterval<float> x)
			{
				this.ChangeFade(x.Value);
			}, delegate(Exception ex)
			{
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			yield return null;
			this.Complete();
			yield break;
		}

		// Token: 0x060075BB RID: 30139 RVA: 0x0031E1BC File Offset: 0x0031C5BC
		public void SkipFade()
		{
			if (this._disposable != null)
			{
				this._disposable.Dispose();
			}
			this._disposable = null;
			this._stream = null;
			if (this._fadeType == FadeType.In)
			{
				if (this._currentPanel != null)
				{
					this._currentPanel.CanvasGroup.alpha = 1f;
				}
			}
			else if (this._currentPanel != null)
			{
				this._currentPanel.CanvasGroup.alpha = 0f;
			}
			this.Complete();
		}

		// Token: 0x060075BC RID: 30140 RVA: 0x0031E254 File Offset: 0x0031C654
		private void ChangeFade(float value)
		{
			if (this._fadeType == FadeType.In)
			{
				this._currentPanel.CanvasGroup.alpha = Mathf.Lerp(0f, 1f, value);
			}
			else
			{
				this._currentPanel.CanvasGroup.alpha = Mathf.Lerp(1f, 0f, value);
			}
		}

		// Token: 0x060075BD RID: 30141 RVA: 0x0031E2B1 File Offset: 0x0031C6B1
		private void Complete()
		{
			if (this._fadeType == FadeType.Out && this._currentPanel != null)
			{
				this._currentPanel.Graphic.gameObject.SetActive(false);
			}
			this._stream = null;
		}

		// Token: 0x04005FEC RID: 24556
		[SerializeField]
		private Dictionary<FadeCanvas.PanelType, FadeItem> _table = new Dictionary<FadeCanvas.PanelType, FadeItem>();

		// Token: 0x04005FED RID: 24557
		private FadeType _fadeType;

		// Token: 0x04005FEE RID: 24558
		private FadeItem _currentPanel;

		// Token: 0x04005FEF RID: 24559
		private static readonly FadeCanvas.PanelType[] _typeKeys = Enum.GetValues(typeof(FadeCanvas.PanelType)).Cast<FadeCanvas.PanelType>().ToArray<FadeCanvas.PanelType>();

		// Token: 0x04005FF0 RID: 24560
		private IObservable<Unit> _stream;

		// Token: 0x04005FF1 RID: 24561
		private IDisposable _disposable;

		// Token: 0x02000E73 RID: 3699
		public enum PanelType
		{
			// Token: 0x04005FF3 RID: 24563
			NowLoading,
			// Token: 0x04005FF4 RID: 24564
			Blackout,
			// Token: 0x04005FF5 RID: 24565
			Transition
		}
	}
}
