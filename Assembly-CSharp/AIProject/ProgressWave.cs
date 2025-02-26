using System;
using System.Collections;
using System.Collections.Generic;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x02000FE4 RID: 4068
	public class ProgressWave : MonoBehaviour
	{
		// Token: 0x06008857 RID: 34903 RVA: 0x0038CF84 File Offset: 0x0038B384
		public void PlayAnim(bool isLoop)
		{
			this._disposable = Observable.FromCoroutine(() => this.WaveAnimCoroutine(isLoop), false).Subscribe<Unit>();
		}

		// Token: 0x06008858 RID: 34904 RVA: 0x0038CFC2 File Offset: 0x0038B3C2
		public void Stop()
		{
			if (this._disposable != null)
			{
				this._disposable.Dispose();
			}
		}

		// Token: 0x06008859 RID: 34905 RVA: 0x0038CFDC File Offset: 0x0038B3DC
		private IEnumerator WaveAnimCoroutine(bool isLoop)
		{
			while (isLoop)
			{
				List<IConnectableObservable<Unit>> list = ListPool<IConnectableObservable<Unit>>.Get();
				Image[] images = this._images;
				for (int i = 0; i < images.Length; i++)
				{
					Image image = images[i];
					IConnectableObservable<Unit> observable = Observable.FromCoroutine(() => this.WaveCoroutine(image), false).Publish<Unit>();
					observable.Connect();
					list.Add(observable);
					yield return Observable.Timer(TimeSpan.FromMilliseconds(100.0), Scheduler.MainThreadIgnoreTimeScale).ToYieldInstruction<long>();
				}
				yield return list.WhenAll().ToYieldInstruction<Unit>();
				yield return Observable.Timer(TimeSpan.FromMilliseconds(600.0), Scheduler.MainThreadIgnoreTimeScale).ToYieldInstruction<long>();
				ListPool<IConnectableObservable<Unit>>.Release(list);
			}
			yield break;
		}

		// Token: 0x0600885A RID: 34906 RVA: 0x0038D000 File Offset: 0x0038B400
		private IEnumerator WaveCoroutine(Image image)
		{
			Vector2 startSizeDelta = image.rectTransform.sizeDelta;
			this._inEndSize = new Vector2(startSizeDelta.x, this._maxSize);
			yield return ObservableEasing.EaseOutSine(this._duration, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				image.rectTransform.sizeDelta = Vector2.Lerp(startSizeDelta, this._inEndSize, x.Value);
			}).ToYieldInstruction<TimeInterval<float>>();
			startSizeDelta = image.rectTransform.sizeDelta;
			this._outEndSize = new Vector2(startSizeDelta.x, this._minSize);
			yield return ObservableEasing.EaseOutSine(this._duration, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				image.rectTransform.sizeDelta = Vector2.Lerp(startSizeDelta, this._outEndSize, x.Value);
			}).ToYieldInstruction<TimeInterval<float>>();
			yield break;
		}

		// Token: 0x04006E82 RID: 28290
		[SerializeField]
		private Image[] _images;

		// Token: 0x04006E83 RID: 28291
		private IDisposable _disposable;

		// Token: 0x04006E84 RID: 28292
		[SerializeField]
		private float _minSize;

		// Token: 0x04006E85 RID: 28293
		[SerializeField]
		private float _maxSize;

		// Token: 0x04006E86 RID: 28294
		private Vector2 _inEndSize = Vector2.zero;

		// Token: 0x04006E87 RID: 28295
		private Vector2 _outEndSize = Vector2.zero;

		// Token: 0x04006E88 RID: 28296
		[SerializeField]
		private float _duration;

		// Token: 0x04006E89 RID: 28297
		[SerializeField]
		private float _delayDuration;

		// Token: 0x04006E8A RID: 28298
		[SerializeField]
		private float _waitDuration;
	}
}
