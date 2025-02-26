using System;
using UniRx;

namespace ReMotion
{
	// Token: 0x020004EB RID: 1259
	public class ObservableEasing
	{
		// Token: 0x06001787 RID: 6023 RVA: 0x00094174 File Offset: 0x00092574
		public static IObservable<float> Create(EasingFunction easing, float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(easing, duration, ignoreTimeScale);
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0009417E File Offset: 0x0009257E
		public static IObservable<float> Linear(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.Linear, duration, ignoreTimeScale);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0009418C File Offset: 0x0009258C
		public static IObservable<float> EaseInSine(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInSine, duration, ignoreTimeScale);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0009419A File Offset: 0x0009259A
		public static IObservable<float> EaseOutSine(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseOutSine, duration, ignoreTimeScale);
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x000941A8 File Offset: 0x000925A8
		public static IObservable<float> EaseInOutSine(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInOutSine, duration, ignoreTimeScale);
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x000941B6 File Offset: 0x000925B6
		public static IObservable<float> EaseInQuad(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInQuad, duration, ignoreTimeScale);
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x000941C4 File Offset: 0x000925C4
		public static IObservable<float> EaseOutQuad(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseOutQuad, duration, ignoreTimeScale);
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x000941D2 File Offset: 0x000925D2
		public static IObservable<float> EaseInOutQuad(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInOutQuad, duration, ignoreTimeScale);
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x000941E0 File Offset: 0x000925E0
		public static IObservable<float> EaseInCubic(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInCubic, duration, ignoreTimeScale);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x000941EE File Offset: 0x000925EE
		public static IObservable<float> EaseOutCubic(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseOutCubic, duration, ignoreTimeScale);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x000941FC File Offset: 0x000925FC
		public static IObservable<float> EaseInOutCubic(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInOutCubic, duration, ignoreTimeScale);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0009420A File Offset: 0x0009260A
		public static IObservable<float> EaseInQuart(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInQuart, duration, ignoreTimeScale);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00094218 File Offset: 0x00092618
		public static IObservable<float> EaseOutQuart(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseOutQuart, duration, ignoreTimeScale);
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00094226 File Offset: 0x00092626
		public static IObservable<float> EaseInOutQuart(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInOutQuart, duration, ignoreTimeScale);
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00094234 File Offset: 0x00092634
		public static IObservable<float> EaseInQuint(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInQuint, duration, ignoreTimeScale);
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00094242 File Offset: 0x00092642
		public static IObservable<float> EaseOutQuint(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseOutQuint, duration, ignoreTimeScale);
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00094250 File Offset: 0x00092650
		public static IObservable<float> EaseInOutQuint(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInOutQuint, duration, ignoreTimeScale);
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0009425E File Offset: 0x0009265E
		public static IObservable<float> EaseInExpo(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInExpo, duration, ignoreTimeScale);
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0009426C File Offset: 0x0009266C
		public static IObservable<float> EaseOutExpo(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseOutExpo, duration, ignoreTimeScale);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0009427A File Offset: 0x0009267A
		public static IObservable<float> EaseInOutExpo(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInOutExpo, duration, ignoreTimeScale);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00094288 File Offset: 0x00092688
		public static IObservable<float> EaseInCirc(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInCirc, duration, ignoreTimeScale);
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00094296 File Offset: 0x00092696
		public static IObservable<float> EaseOutCirc(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseOutCirc, duration, ignoreTimeScale);
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x000942A4 File Offset: 0x000926A4
		public static IObservable<float> EaseInOutCirc(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInOutCirc, duration, ignoreTimeScale);
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x000942B2 File Offset: 0x000926B2
		public static IObservable<float> EaseInBack(float duration, float overshoot = 1.70158f, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInBack(overshoot), duration, ignoreTimeScale);
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x000942C1 File Offset: 0x000926C1
		public static IObservable<float> EaseOutBack(float duration, float overshoot = 1.70158f, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseOutBack(overshoot), duration, ignoreTimeScale);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x000942D0 File Offset: 0x000926D0
		public static IObservable<float> EaseInOutBack(float duration, float overshoot = 1.70158f, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInOutBack(overshoot), duration, ignoreTimeScale);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x000942DF File Offset: 0x000926DF
		public static IObservable<float> EaseInElastic(float duration, float amplitude = 1.70158f, float period = 0f, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInElastic(amplitude, period), duration, ignoreTimeScale);
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x000942EF File Offset: 0x000926EF
		public static IObservable<float> EaseOutElastic(float duration, float amplitude = 1.70158f, float period = 0f, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseOutElastic(amplitude, period), duration, ignoreTimeScale);
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x000942FF File Offset: 0x000926FF
		public static IObservable<float> EaseInOutElastic(float duration, float amplitude = 1.70158f, float period = 0f, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInOutElastic(amplitude, period), duration, ignoreTimeScale);
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0009430F File Offset: 0x0009270F
		public static IObservable<float> EaseInBounce(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInBounce, duration, ignoreTimeScale);
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0009431D File Offset: 0x0009271D
		public static IObservable<float> EaseOutBounce(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseOutBounce, duration, ignoreTimeScale);
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0009432B File Offset: 0x0009272B
		public static IObservable<float> EaseInOutBounce(float duration, bool ignoreTimeScale = false)
		{
			return ObservableEasing.ObservableTween.CreateObservable(EasingFunctions.EaseInOutBounce, duration, ignoreTimeScale);
		}

		// Token: 0x04001AF8 RID: 6904
		private const float DefaultOvershoot = 1.70158f;

		// Token: 0x04001AF9 RID: 6905
		private const float DefaultAmplitude = 1.70158f;

		// Token: 0x04001AFA RID: 6906
		private const float DefaultPeriod = 0f;

		// Token: 0x020004EC RID: 1260
		private class ObservableTween : ITween
		{
			// Token: 0x060017A7 RID: 6055 RVA: 0x00094339 File Offset: 0x00092739
			private ObservableTween(IObserver<float> observer, BooleanDisposable cancellation, EasingFunction easing, float duration, bool ignoreTimeScale)
			{
				this.observer = observer;
				this.cancellation = cancellation;
				this.easing = easing;
				this.ignoreTimeScale = ignoreTimeScale;
				this.duration = duration;
			}

			// Token: 0x060017A8 RID: 6056 RVA: 0x00094368 File Offset: 0x00092768
			public static IObservable<float> CreateObservable(EasingFunction easing, float duration, bool ignoreTimeScale)
			{
				return Observable.CreateSafe<float>(delegate(IObserver<float> observer)
				{
					observer.OnNext(0f);
					BooleanDisposable result = new BooleanDisposable();
					ObservableEasing.ObservableTween tween = new ObservableEasing.ObservableTween(observer, result, easing, duration, ignoreTimeScale);
					TweenEngine.AddTween(tween);
					return result;
				}, false);
			}

			// Token: 0x060017A9 RID: 6057 RVA: 0x000943A4 File Offset: 0x000927A4
			public bool MoveNext(ref float deltaTime, ref float unscaledDeltaTime)
			{
				if (this.cancellation.IsDisposed)
				{
					return false;
				}
				float num = (!this.ignoreTimeScale) ? deltaTime : unscaledDeltaTime;
				if (num == 0f)
				{
					return true;
				}
				this.currentTime += num;
				bool flag = false;
				if (this.currentTime >= this.duration)
				{
					this.currentTime = this.duration;
					flag = true;
				}
				this.observer.OnNext(this.easing(this.currentTime, this.duration));
				if (flag)
				{
					this.observer.OnCompleted();
					return false;
				}
				return true;
			}

			// Token: 0x04001AFB RID: 6907
			private readonly IObserver<float> observer;

			// Token: 0x04001AFC RID: 6908
			private readonly BooleanDisposable cancellation;

			// Token: 0x04001AFD RID: 6909
			private readonly EasingFunction easing;

			// Token: 0x04001AFE RID: 6910
			private readonly bool ignoreTimeScale;

			// Token: 0x04001AFF RID: 6911
			private readonly float duration;

			// Token: 0x04001B00 RID: 6912
			private float currentTime;
		}
	}
}
