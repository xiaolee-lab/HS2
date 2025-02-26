using System;
using System.Runtime.CompilerServices;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;

namespace AIProject.CaptionScript
{
	// Token: 0x02000E2F RID: 3631
	[RequireComponent(typeof(CaptionSystem))]
	[RequireComponent(typeof(CommandSystem))]
	public class Captions : MonoBehaviour
	{
		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x06007193 RID: 29075 RVA: 0x00306731 File Offset: 0x00304B31
		// (set) Token: 0x06007194 RID: 29076 RVA: 0x00306739 File Offset: 0x00304B39
		public bool Active { get; private set; }

		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x06007195 RID: 29077 RVA: 0x00306742 File Offset: 0x00304B42
		// (set) Token: 0x06007196 RID: 29078 RVA: 0x0030674A File Offset: 0x00304B4A
		public CaptionSystem CaptionSystem
		{
			get
			{
				return this._captionSystem;
			}
			set
			{
				this._captionSystem = value;
			}
		}

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x06007197 RID: 29079 RVA: 0x00306753 File Offset: 0x00304B53
		// (set) Token: 0x06007198 RID: 29080 RVA: 0x0030675B File Offset: 0x00304B5B
		public CommandSystem CommandSystem
		{
			get
			{
				return this._commandSystem;
			}
			set
			{
				this._commandSystem = value;
			}
		}

		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x06007199 RID: 29081 RVA: 0x00306764 File Offset: 0x00304B64
		public bool IsProcEndADV
		{
			[CompilerGenerated]
			get
			{
				return this._subscriber != null;
			}
		}

		// Token: 0x0600719A RID: 29082 RVA: 0x00306772 File Offset: 0x00304B72
		private void Start()
		{
			Singleton<ADV>.Instance.Captions = this;
		}

		// Token: 0x0600719B RID: 29083 RVA: 0x0030677F File Offset: 0x00304B7F
		private void OnDestroy()
		{
			base.StopAllCoroutines();
			if (Singleton<Voice>.IsInstance())
			{
				Singleton<Voice>.Instance.StopAll(true);
			}
		}

		// Token: 0x0600719C RID: 29084 RVA: 0x0030679C File Offset: 0x00304B9C
		public void EndADV(Action onCompleted = null)
		{
			this._canvasGroup.blocksRaycasts = false;
			if (this._subscriber != null)
			{
				this._subscriber.Dispose();
			}
			IObservable<TimeInterval<float>> source = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true);
			float startAlpha = this._canvasGroup.alpha;
			if (onCompleted != null)
			{
				this._subscriber = source.DoOnCompleted(delegate
				{
					this.Active = false;
					this._subscriber = null;
				}).Subscribe(delegate(TimeInterval<float> x)
				{
					this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
				}, delegate(Exception ex)
				{
				}, onCompleted);
			}
			else
			{
				this._subscriber = source.DoOnCompleted(delegate
				{
					this.Active = false;
					this._subscriber = null;
				}).Subscribe(delegate(TimeInterval<float> x)
				{
					this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
				}, delegate(Exception ex)
				{
				});
			}
		}

		// Token: 0x0600719D RID: 29085 RVA: 0x00306895 File Offset: 0x00304C95
		public void CanvasGroupON()
		{
			this.Active = true;
			this._canvasGroup.alpha = 1f;
			this._canvasGroup.blocksRaycasts = true;
		}

		// Token: 0x0600719E RID: 29086 RVA: 0x003068BA File Offset: 0x00304CBA
		public void CanvasGroupOFF()
		{
			this.Active = false;
			this._canvasGroup.alpha = 0f;
			this._canvasGroup.blocksRaycasts = false;
		}

		// Token: 0x04005D14 RID: 23828
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04005D15 RID: 23829
		[SerializeField]
		private CaptionSystem _captionSystem;

		// Token: 0x04005D16 RID: 23830
		[SerializeField]
		private CommandSystem _commandSystem;

		// Token: 0x04005D17 RID: 23831
		private IDisposable _subscriber;
	}
}
